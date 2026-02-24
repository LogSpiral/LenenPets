using LenenPets.Content.Pets;
using LenenPets.Content.PetsAnimations.Core;
using System.IO;

namespace LenenPets.Content.PetsAnimations.PetCharacterAnimations;

public class AnnoyingAnimation : CharacterAnimation
{
    public int AnnoyingCountMin { get; init; }
    public int AnnoyingCountMax { get; init; }
    private int _annoyingCount;
    private bool _netLock;

    public override void Update(BasicLenenPet pet)
    {
        pet.Projectile.frameCounter++;
        if (pet.Projectile.frameCounter > FrameRate)
        {
            pet.Projectile.frameCounter = 0;
            pet.Projectile.frame++;
        }
        if (pet.Projectile.frame > FrameIndexMax - 1 && _annoyingCount > 0)
        {
            _annoyingCount--;
            pet.Projectile.frame = FrameIndexMin + 1;
        }
        if (pet.Projectile.frame > FrameIndexMax)
        {
            pet.Projectile.frame = FrameIndexMin;
            IsFinished = true;
        }
    }

    protected override void OnActive(BasicLenenPet pet)
    {
        if (!_netLock)
            _annoyingCount = Main.rand.Next(AnnoyingCountMin, AnnoyingCountMax + 1);
        pet.Projectile.frame = FrameIndexMin;
    }

    public void NetSend(BinaryWriter writer)
    {
        writer.Write((byte)_annoyingCount);
    }

    public void NetReceive(BinaryReader reader)
    {
        _annoyingCount = reader.ReadByte();
        _netLock = true;
    }
}