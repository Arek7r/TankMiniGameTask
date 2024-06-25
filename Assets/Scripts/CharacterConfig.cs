using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterConfig", menuName = "SO/CharacterConfig", order = 3)]

public class CharacterConfig : ScriptableObject
{
   public EnemyType enemyType;
   public float HpMax;
   public float SpeedMax;
   
}
