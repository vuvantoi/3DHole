using UnityEngine;

public class PlayerSkinLoader : MonoBehaviour
{
    [Header("Database")]
    public HoleSkinDatabase holeSkinDB;

    [Header("References")]
    public Transform holeParent;

    private GameObject currentSkin;

    private void Start()
    {
        // 🔹 Xóa skin mặc định trong Hole (nếu có)
        foreach (Transform child in holeParent)
        {
            Destroy(child.gameObject);
        }

        // 🔹 Load skin đã chọn
        int index = GameDataManager.Instance.GetSelectedHoleSkin();
        GameObject skinPrefab = holeSkinDB.GetHoleSkin(index).holeSkin;

        currentSkin = Instantiate(skinPrefab, holeParent);
        currentSkin.transform.localPosition = Vector3.zero;
        currentSkin.transform.localRotation = Quaternion.identity;
    }
}
