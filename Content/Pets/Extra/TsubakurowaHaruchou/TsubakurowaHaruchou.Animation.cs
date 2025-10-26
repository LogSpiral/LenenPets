using LenenPets.Content.PetsAnimations.Core;
using LenenPets.Content.PetsAnimations.ExtraAnimations;
using LenenPets.Content.PetsAnimations.PetCharacterAnimations;
using System.Collections.Generic;
using TouhouPets;

namespace LenenPets.Content.Pets.Extra.TsubakurowaHaruchou;


public partial class TsubakurowaHaruchou
{
    protected override IReadOnlyList<IPetAnimation> PetAnimations => [CharacterAnimation, ClothAnimation, BlinkAnimation, HatAnimation];

    private static DrawPetConfig DrawConfig { get; } = new(4);

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
            DrawConfig = DrawConfig,
            ExtraAnimationRow = 1,
            FrameRate = 4,
            FrameIndexMin = 0,
            FrameIndexMax = 3
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
            DrawConfig = DrawConfig,
            ExtraAnimationRow = 3,
            FrameRate = 4,
            FrameIndexMin = 0,
            FrameIndexMax = 2
        };
}
