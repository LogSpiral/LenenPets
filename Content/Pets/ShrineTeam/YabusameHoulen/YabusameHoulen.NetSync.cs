using System.IO;

namespace LenenPets.Content.Pets.ShrineTeam.YabusameHoulen;

public partial class YabusameHoulen
{
    public override void SendExtraAI(BinaryWriter writer)
    {
        writer.Write(CharacterAnimation.GetType().Name);
        if (CharacterAnimation == AnnoyingAnimation)
            AnnoyingAnimation.NetSend(writer);
    }

    public override void ReceiveExtraAI(BinaryReader reader)
    {
        string currentAnimation = reader.ReadString();
        switch (currentAnimation)
        {
            case nameof(IdleAnimation):
                {
                    CharacterAnimation = IdleAnimation;
                    IdleAnimation.SetActive();
                    break;
                }
            case nameof(AnnoyingAnimation):
                {
                    CharacterAnimation = AnnoyingAnimation;
                    AnnoyingStart();
                    AnnoyingAnimation.NetReceive(reader);
                    break;
                }
        }
    }
}