using System;
using UnityEngine;

[RequireComponent(typeof(Transform))]
public class PlayerLevel : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private PlayerLevelDatabase levelDatabase;
    public PlayerLevelDatabase LevelDatabase => levelDatabase;

    [Header("References")]
    [SerializeField] private PointManager pointManager; // gán trong Inspector

    public int CurrentLevel { get; private set; } = 1;
    public PlayerLevelData CurrentData { get; private set; }

    // Sự kiện thông báo đã lên cấp (có thể dùng cho UI, FX...)
    public event Action<int> OnLevelUp;

    private void Awake()
    {
        // Kiểm tra cấu hình bắt đầu để tránh NullReference
        if (levelDatabase == null)
            Debug.LogError("[PlayerLevel] levelDatabase chưa được gán trong Inspector!");

        if (pointManager == null)
            Debug.LogError("[PlayerLevel] pointManager chưa được gán trong Inspector!");

        // Thiết lập initial data
        SetLevel(CurrentLevel);
    }

    private void OnEnable()
    {
        // Đăng ký lắng nghe thay đổi điểm
        if (pointManager != null)
            pointManager.OnPointsChanged += HandlePointsChanged;
    }

    private void OnDisable()
    {
        if (pointManager != null)
            pointManager.OnPointsChanged -= HandlePointsChanged;
    }

    private void HandlePointsChanged(int totalPoints)
    {
        TryLevelUpByTotalPoints(totalPoints);
    }

    
    private void TryLevelUpByTotalPoints(int totalPoints)
    {
        if (levelDatabase == null) return;

        // Có thể lên nhiều cấp nếu totalPoints đủ lớn
        while (CurrentLevel < levelDatabase.MaxLevel)
        {
            int nextLevel = CurrentLevel + 1;
            int requiredTotal = levelDatabase.GetLevelData(nextLevel).pointsToNextLevel;

            if (totalPoints >= requiredTotal)
            {
                CurrentLevel = nextLevel;
                SetLevel(CurrentLevel);
                OnLevelUp?.Invoke(CurrentLevel);
                Debug.Log($"[PlayerLevel] LevelUp -> {CurrentLevel} (requiredTotal was {requiredTotal})");
                // tiếp vòng while để kiểm tra có lên tiếp không
            }
            else
            {
                break;
            }
        }
    }

    
    private void SetLevel(int level)
    {
        if (levelDatabase == null) return;

        CurrentData = levelDatabase.GetLevelData(level);

        transform.localScale = Vector3.one * CurrentData.holeScale;
    }
}
