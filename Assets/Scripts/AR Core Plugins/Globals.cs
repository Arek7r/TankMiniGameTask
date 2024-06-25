using System;
using System.Collections.Generic;
using UnityEngine;

public enum SideType
{
    right,
    left,
    up,
    down,
    none,
}
public enum AbilityGroup
{
    Heal,
    Attack,
    Buff,
}

public enum ColorType
{
    Yellow,
    Red,
    Green,
    Blue,
}

public enum TimeScaleType
{
    None,
    Normal,
}

public enum WindowType
{
    Start,
    End,
    Win,
    Fail,
}


public enum ButtonState
{
    None,
    Normal,
    Pressed,
    PressedDown,
    PressedUp,
    Disabled,
}

public enum ButtonPressState
{
    Down,
    Up
}

public enum ColliderType
{
    Mesh,
    MeshConvex,
    Box,
    Sphere,
    None,
}

public enum EnemyType
{
    Cube,
    CubeBig,
    Ball,
    BallBig,
}


public enum AIState
{
    normal,
    dead,
}

public class LayerMaskID
{
    public static readonly int Default = LayerMask.NameToLayer("Default");

    public struct BitLayer
    {
        public static readonly int Default = 1 << LayerMask.NameToLayer("Default");
    }
}

public static class Globals
{
    private static readonly Color yellowIcon = new Color(255,220,0);
    private static readonly Color redIcon = new Color(255,0,0);
    public static Color black => new Color(0.0f, 0.0f, 0.0f, 0f);


    public static Color GetIconColor(ColorType colorType)
    {
        switch (colorType)
        {
            case ColorType.Yellow:
                return yellowIcon;
            case ColorType.Red:
                return redIcon;
                break;
            case ColorType.Green:
                break;
            case ColorType.Blue:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(colorType), colorType, null);
        }

        return default;
    }
}

public static class GlobalString
{
    public static string Horizontal = "Horizontal";
    public static string IsReloading = "IsReloading";
    public const string Player = "Player";
    public const string Bot = "Bot";
    public const string Track = "Track";
    public const string Obstacle = "Obstacle";
    public const string Untagged = "Untagged";
    public const string Enemy = "Enemy";
    public const string OnlyCollide = "OnlyCollide";
    public const string Bonus = "Bonus";
    public const string Attacker = "Attacker";
    public const string LEVEL = "LEVEL ";
    public const string EmptyString = "";
    public const string PlayerSpawn = "PlayerSpawn";
    public const string Special = "Special";
    public const string Attack = "Attack";
    public const string Magazine = "Magazine";
    public const string Ammo = "Ammo";
    public const string Bullet = "Bullet";
    public const string Icon = "Icon";
    public const string Cost = "Cost";
    public const string Locked = "Locked";
    public const string Gold = "Gold";
    public const string SimpleBullet = "SimpleBullet";
    public const string SimpleRocket = "SimpleRocket";
    public const string Debug = "Debug";
    public const string QualitySettingsIdentifier = "DefaultQualitySettings";
    
    // Sound
    // HCSDK
    // FIREBASE
    // ADS MANAGER
    // PREFS
    // PREFS TUTORIAL
    // Animator

    //numbers
    public const string zeroNumber = "0";
    public const string oneNumber = "1";
    public const string twoNumber = "2";
    public const string threeNumber = "3";
    public const string fourNumber = "4";
    public const string fiveNumber = "5";
    public const string sixNumber = "6";
    public const string sevenNumber = "7";
    public const string eightNumber = "8";
    public const string nineNumber = "9";

    
    public static string GetNumber(int number)
    {
        switch (number)
        {
            case 0:
                return zeroNumber;
            case 1:
                return oneNumber;
            case 2:
                return twoNumber;
            case 3:
                return threeNumber;
            case 4:
                return fourNumber;
            case 5:
                return fiveNumber;
            case 6:
                return sixNumber;
            case 7:
                return sevenNumber;
            case 8:
                return eightNumber;
            case 9:
                return nineNumber;
            default:
                return number.ToString();
        }
    }
}


public static class AnimatorTagHash
{
    public static readonly int Enable = Animator.StringToHash("Enable");
    public static readonly int Enabling = Animator.StringToHash("Enabling");
    public static readonly int Disable = Animator.StringToHash("Disable");
    public static readonly int Disabling= Animator.StringToHash("Disabling");
}

