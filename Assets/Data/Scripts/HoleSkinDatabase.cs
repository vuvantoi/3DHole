using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HoleSkin", menuName = "Game Data/HoleSkin Database")]
public class HoleSkinDatabase : ScriptableObject
{
    public HoleSkin[] holeSkins;

    public int HoleSkinCount
    {
        get { 
            return holeSkins.Length;
        }
    }

    public HoleSkin GetHoleSkin(int index)
    {
        return holeSkins[index];
    }
}
