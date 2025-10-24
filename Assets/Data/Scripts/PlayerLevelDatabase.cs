using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerLevelDatabase" , menuName = "Game Data/Player Level Database")]
public class PlayerLevelDatabase : ScriptableObject
{
    public PlayerLevelData[] levels;

    public PlayerLevelData GetLevelData(int level)
    {
        if (level <= 0) level = 1;
        if (level > levels.Length) level = levels.Length;
        return levels[level - 1];
    }
    
    public int MaxLevel => levels != null ? levels.Length : 0;

}
