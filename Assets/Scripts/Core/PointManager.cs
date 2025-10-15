using System;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public event Action<int> OnPointsChanged;

    private int totalPoints = 0;
    public int TotalPoints => totalPoints;

    
    public void AddPoints(int amount)
    {
        if (amount <= 0) return;

        totalPoints += amount;
        Debug.Log($"[PointManager] +{amount} => total = {totalPoints}");

        OnPointsChanged?.Invoke(totalPoints);
    }

    
    public void ResetPoints()
    {
        totalPoints = 0;
        OnPointsChanged?.Invoke(totalPoints);
    }
}
