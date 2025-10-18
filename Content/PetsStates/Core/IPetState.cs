using LenenPets.Content.Pets;
using TouhouPets;

namespace LenenPets.Content.PetsStates.Core;
public interface IPetState
{
    public void Update(BasicLenenPet pet);
}
