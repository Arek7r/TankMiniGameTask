using System.Collections;
using System.Collections.Generic;
using AR_Ability_Module;
using TriInspector;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "Ability BaseSO", menuName = "SO Ability/Ability Base", order = 3)]

public class AbilityBaseSO : ScriptableObject
{
   #region Variables
   // [DisableInEditMode]
   // public string waveName;

   [Space]
   //[Multiline(2)]
   public string description;

   [ReadOnly] 
   public AbilityGroup AbilityGroup;
   public AssetReference assetReference;
   public AbilityInstance prefab;
   
   [Space]
   public Sprite icon;
   public float price;

   [Space]
   public float cooldown;
   public float duration;
   public TypeActivation typeActivation; 
   
   // [Space] 
   // public bool multiTarget;
   // [ShowIf(nameof(multiTarget), true)]
   // public float multiTarget_range;
   // [ShowIf(nameof(multiTarget), true)]
   // public int multiTarget_maxTargets;
   //
   // [Space] 
   // public bool area;
   // public float area_range;

   #endregion

   #region Set
   public void SetAbilityGroup(AbilityGroup newAbilityGroup)
   {
       this.AbilityGroup = newAbilityGroup;
   }

   public virtual void Cast(){}

   public virtual void Use(AbilityStruct abilityStruct, out AbilityInstance abilityInstance)
   {
       abilityInstance = null;
   }
   
   public virtual void Use()
   {}

   public virtual void Activate(AbilityStruct abilityStruct)
   {}
   
   #endregion

}
