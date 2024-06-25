using System.Collections.Generic;

[System.Serializable]
public class AbilityCharacter
{
    public List<AbilityDataSO> currentAbilities = new List<AbilityDataSO>();

    public void ChangeAbility(int slotIndex, AbilityDataSO newAbility)
    {
        if (slotIndex >= 0 && slotIndex < currentAbilities.Count)
        {
            currentAbilities[slotIndex] = newAbility;
        }
    }

}