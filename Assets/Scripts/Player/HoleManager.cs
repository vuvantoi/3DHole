using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class HoleManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private PointManager pointManager;
    [SerializeField] private PlayerLevel playerLevel; // để biết ngưỡng level tiếp theo

    [Header("UI")]
    [SerializeField] private Image progressBar; // optional, kéo vào Inspector

    private void Awake()
    {
        if (pointManager == null) Debug.LogError("[HoleManager] pointManager chưa được gán!");
        if (playerLevel == null) Debug.LogError("[HoleManager] playerLevel chưa được gán!");
        // đăng ký event để update UI khi điểm thay đổi
        if (pointManager != null)
            pointManager.OnPointsChanged += UpdateProgressUI;
    }

    private void OnDestroy()
    {
        if (pointManager != null)
            pointManager.OnPointsChanged -= UpdateProgressUI;
    }

    private void Start()
    {
        if (progressBar != null) progressBar.fillAmount = 0f;
        // initial update (nếu đã có điểm từ trước)
        if (pointManager != null)
            UpdateProgressUI(pointManager.TotalPoints);
    }

    private void OnTriggerEnter(Collider other)
    {
        // chỉ xử lý tag "Object"
        if (!other.CompareTag("Object")) return;

        // lấy ObjectBehaviour (bridge tới ObjectData)
        var objectBehaviour = other.GetComponent<ObjectManager>();
        if (objectBehaviour == null)
        {
            Debug.LogWarning("[HoleManager] Object bị hút không có ObjectBehaviour!");
            other.gameObject.SetActive(false);
            return;
        }

        int pointToAdd = objectBehaviour.ObjectData != null ? objectBehaviour.ObjectData.point : 0;

        // 1) Thêm điểm
        if (pointManager != null && pointToAdd > 0)
        {
            pointManager.AddPoints(pointToAdd);
        }

        objectBehaviour.OnEaten();
    }

    private void UpdateProgressUI(int totalPoints)
    {
        if (progressBar == null || playerLevel == null || playerLevel.LevelDatabase == null)
            return;

        int currentLevel = playerLevel.CurrentLevel;
        int nextLevel = currentLevel + 1;

        // Nếu đã max level
        if (nextLevel > playerLevel.LevelDatabase.MaxLevel)
        {
            progressBar.fillAmount = 1f;
            return;
        }

        int requiredCurrent = playerLevel.LevelDatabase.GetLevelData(currentLevel).pointsToNextLevel;
        int requiredNext = playerLevel.LevelDatabase.GetLevelData(nextLevel).pointsToNextLevel;

        // Tính tiến trình trong cấp hiện tại
        float progress = (float)(totalPoints - requiredCurrent) / (requiredNext - requiredCurrent);
        progressBar.fillAmount = Mathf.Clamp01(progress);
    }

}
