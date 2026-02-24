using LenenPets.Content.PetsAnimations.Core;
using LenenPets.Content.PetsAnimations.ExtraAnimations;
using LenenPets.Content.PetsAnimations.PetCharacterAnimations;
using System.Collections.Generic;
using TouhouPets;

namespace LenenPets.Content.Pets.Extra.TsubakurowaHaruchou;

public partial class TsubakurowaHaruchou
{
    protected override IReadOnlyList<IPetAnimation> PetAnimations => [CharacterAnimation, CharacterClothAnimation, ClothAnimation, BlinkAnimation, HatAnimation];

    private static DrawPetConfig DrawConfig { get; } = new(5);

    private CharacterAnimation CharacterAnimation { get; set; }

    private IdleAnimation IdleAnimation { get; } =
        new()
        {
            DrawConfig = DrawConfig,
            FrameRate = 100,
            FrameIndexMin = 0,
            FrameIndexMax = 0
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
            Row = 4
        };

    private BlinkAnimation BlinkAnimation { get; } =
        new()
        {
            DrawConfig = DrawConfig,
            ExtraAnimationRow = 2,
            FrameRate = 3,
            FrameIndexMin = 0,
            FrameIndexMax = 3
        };

    private ClothAnimation HatAnimation { get; } =
        new()
        {
            DrawConfig = DrawConfig with { ShouldUseEntitySpriteDraw = true },
            ExtraAnimationRow = 3,
            FrameRate = 4,
            FrameIndexMin = 0,
            FrameIndexMax = 2
        };
}