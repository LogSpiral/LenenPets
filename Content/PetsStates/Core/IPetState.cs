using LenenPets.Content.Pets;

namespace LenenPets.Content.PetsStates.Core;

public interface IPetState
{
    public void Update(BasicLenenPet pet);
}