using LenenPets.Content.Pets;
using Microsoft.Xna.Framework;
using TouhouPets;

namespace LenenPets.Content.PetsAnimations.Core;

public abstract class CharacterAnimation : IPetAnimation
{
    public virtual void Draw(BasicLenenPet pet, Color lightColor)
    {
        DefaultDraw(pet, lightColor);
    }
    void IPetAnimation.Update(BasicLenenPet pet)
    {
        if (_pendingSwitchActive is true)
        {
            OnActive(pet);
            _pendingSwitchActive = null;
        }
        else if (_pendingSwitchActive is false)
        {
            OnDeactive(pet);
            _pendingSwitchActive = null;
        }
        if (!IsActive || IsFinished) return;
        Update(pet);
    }
    public virtual void Update(BasicLenenPet pet)
    {
        DefaultUpdate(pet);
    }
    protected virtual void OnActive(BasicLenenPet pet)
    {

    }
    protected virtual void OnDeactive(BasicLenenPet pet)
    {

    }
    public int CharacterAnimationRow { get; set; } = 0;

    public required DrawPetConfig DrawConfig { get; set; }

    public required int FrameIndexMin { get; set; }

    public required int FrameIndexMax { get; set; }

    public required int FrameRate { get; set; }

    protected bool IsActive
    {
        get;
        private set;
    }

    public bool IsFinished
    {
        get;
        protected set;
    }

    private bool? _pendingSwitchActive = null;

    public void SetActive()
    {
        IsActive = true;
        IsFinished = false;
        _pendingSwitchActive = true;
    }

    public void SetDeactive()
    {
        IsActive = false;
        IsFinished = false;
        _pendingSwitchActive = false;
    }

    protected void DefaultDraw(BasicLenenPet pet, Color lightColor)
    {
        if (!IsActive) return;
        pet.Projectile.DrawPet(
            pet.Projectile.frame,
            lightColor,
            DrawConfig,
            CharacterAnimationRow);
    }

    protected void DefaultUpdate(BasicLenenPet pet)
    {
        pet.Projectile.frameCounter++;
        if (pet.Projectile.frameCounter > FrameRate)
        {
            pet.Projectile.frameCounter = 0;
            pet.Projectile.frame++;
        }
        if (pet.Projectile.frame > FrameIndexMax)
        {
            IsFinished = true;
            pet.Projectile.frame = FrameIndexMin;
        }
    }
}
