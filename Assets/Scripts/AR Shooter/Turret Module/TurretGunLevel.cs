using System.Collections;
using System.Collections.Generic;
using _Scripts_.Bullets_Projectiles;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Gun Config SO", menuName = "SO Turret/Gun level", order = 3)]
public class TurretGunLevel: ScriptableObject
{
    public SimpleBullet simpleBulletPrefab;
    public float cooldown = 0.1f;
}
