using LenenPets.Content.PetsAnimations.Core;
using LenenPets.Content.PetsStates;
using LenenPets.Content.PetsStates.Core;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Localization;
using TouhouPets;

namespace LenenPets.Content.Pets;

/// <summary>
/// 连缘宠物基类
/// <br/>
/// 使用了很多螺线子自己重新组织的组件
/// </summary>
public abstract class BasicLenenPet : BasicTouhouPet
{
    protected override string ChatKeyToRegister(string name, int index)
    {
        var result = this.GetLocalizationKey($"Regular_{index}");
        Language.GetOrRegister(result);
        return result;
    }

    protected abstract IReadOnlyList<IPetAnimation> PetAnimations { get; }

    protected abstract IReadOnlyList<IPetState> PetStates { get; }

    protected IPetState CurrentState => PetStates[PetState];

    protected IdleState IdleState { get; } = new IdleState()
    {
        PositionOffset = new Vector2(56, -34),
        RotationFactor = 0.01f,
        SpeedFactor = 13f
    };

    public override bool DrawPetSelf(ref Color lightColor)
    {
        bool lastShaderRequired = false;
        foreach (var animation in PetAnimations)
        {
            if (lastShaderRequired && !animation.ShaderRequired)
                Projectile.ResetDrawStateForPet();
            animation.Draw(this, lightColor);
            lastShaderRequired = animation.ShaderRequired;
        }
        return false;
    }

    public override void AI()
    {
        if (!CheckActive())
        {
            currentChatRoom?.CloseChatRoom();
            return;
        }
        UpdateStatus();
        CurrentState?.Update(this);
    }

    public override void VisualEffectForPreview()
    {
        foreach (var animation in PetAnimations)
            animation.Update(this);
    }

    protected abstract bool CheckActive();

    protected virtual void UpdateStatus()
    { }
}