using LenenPets.Content.Pets;
using Microsoft.Xna.Framework;
using TouhouPets;

namespace LenenPets.Content.PetsAnimations.Core;

public abstract class PetExtraAnimation : IPetAnimation
{
    bool IPetAnimation.ShaderRequired => DrawConfig.ShouldUseEntitySpriteDraw;
    public virtual void Draw(BasicLenenPet pet, Color lightColor)
    {
        DefaultDraw(pet, lightColor);
    }
    public virtual void Update(BasicLenenPet pet)
    {
        DefaultUpdate(pet);
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
        if (!IsActive) return;
        Update(pet);
    }
    protected virtual void OnActive(BasicLenenPet pet)
    {

    }
    protected virtual void OnDeactive(BasicLenenPet pet)
    {

    }
    public int ExtraAnimationRow { get; set; } = 1;

    public required DrawPetConfig DrawConfig { get; set; }

    public required int FrameIndexMin { get; set; }

    public required int FrameIndexMax { get; set; }

    public required int FrameRate { get; set; }

    protected int frameCounter;

    protected int frameIndex;

    protected bool IsActive
    {
        get;
        private set;
    }

    private bool? _pendingSwitchActive = null;

    public void SetActive()
    {
        IsActive = true;
        _pendingSwitchActive = true;
    }

    public void SetDeactive()
    {
        IsActive = false;
        _pendingSwitchActive = false;
    }

    protected void DefaultDraw(BasicLenenPet pet, Color lightColor)
    {
        if (!IsActive) return;
        pet.Projectile.DrawPet(
            frameIndex,
            lightColor,
            DrawConfig,
            ExtraAnimationRow);
    }

    protected void DefaultUpdate(BasicLenenPet pet)
    {
        frameCounter++;
        if (frameCounter > FrameRate)
        {
            frameCounter = 0;
            frameIndex++;
        }
        if (frameIndex > FrameIndexMax)
            frameIndex = FrameIndexMin;
    }
}
