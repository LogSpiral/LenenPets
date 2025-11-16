using LenenPets.Content.PetsAnimations.Core;
using LenenPets.Content.PetsAnimations.ExtraAnimations;
using LenenPets.Content.PetsAnimations.PetCharacterAnimations;
using System.Collections.Generic;
using TouhouPets;

namespace LenenPets.Content.Pets.Shion;

public partial class Shion
{
    protected override IReadOnlyList<IPetAnimation> PetAnimations => [WingAnimation, CharacterAnimation,CharacterClothAnimation, ClothAnimation, BlinkAnimation, HitaikakushiAnimation];

    private static DrawPetConfig DrawConfig { get; } = new(6);

    private CharacterAnimation CharacterAnimation { get; set; }

    private IdleAnimation IdleAnimation { get; } =
        new()
        {
            DrawConfig = DrawConfig,
            FrameRate = 100,
            FrameIndexMin = 0,
            FrameIndexMax = 0
        };
    private ShionWingAnimation WingAnimation { get; } =
        new()
        {
            DrawConfig = DrawConfig with { ShouldUseEntitySpriteDraw = true},
            ExtraAnimationRow = 4,
            FrameRate = 6,
            FrameIndexMin = 0,
            FrameIndexMax = 4
        };
    private ClothAnimation ClothAnimation { get; } =
        new()
        {
            DrawConfig = DrawConfig with { ShouldUseEntitySpriteDraw = true },
            ExtraAnimationRow = 1,
            FrameRate = 4,
            FrameIndexMin = 0,
            FrameIndexMax = 3
        };
    private ExternalControlAnimations CharacterClothAnimation { get; } =
        new()
        {
            DrawConfig = DrawConfig with { ShouldUseEntitySpriteDraw = true },
            Row = 5
        };
    private BlinkAnimation BlinkAnimation { get; } =
        new()
        {
            DrawConfig = DrawConfig,
            ExtraAnimationRow = 2,
            FrameRate = 3,
            FrameIndexMin = 0,
            FrameIndexMax = 2
        };

    private ShionHitaikakushiAnimation HitaikakushiAnimation { get; } =
        new()
        {
            DrawConfig = DrawConfig with { ShouldUseEntitySpriteDraw = true },
            ExtraAnimationRow = 3,
            FrameRate = 4,
            FrameIndexMin = 0,
            FrameIndexMax = 2
        };
}
