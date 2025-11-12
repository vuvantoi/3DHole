using UnityEngine;

public class HoleSkinManager : MonoBehaviour
{
    [Header("Database")]
    public HoleSkinDatabase holeSkinDB;

    [Header("References")]
    public Transform holeParent; // Gán = object "Hole" trong Inspector
    private GameObject currentSkin; // Lưu skin đang hiển thị

    private int selectedIndex = 0;

    private void Start()
    {
        selectedIndex = GameDataManager.Instance.GetSelectedHoleSkin();
        UpdateHoleSkin(selectedIndex);
    }

    public void NextOption()
    {
        selectedIndex++;
        if (selectedIndex >= holeSkinDB.HoleSkinCount)
            selectedIndex = 0;

        UpdateHoleSkin(selectedIndex);
    }

    public void BackOption()
    {
        selectedIndex--;
        if (selectedIndex < 0)
            selectedIndex = holeSkinDB.HoleSkinCount - 1;

        UpdateHoleSkin(selectedIndex);
    }

    private void UpdateHoleSkin(int index)
    {
        // Xóa skin cũ (nếu có)
        if (currentSkin != null)
            Destroy(currentSkin);

        // Lấy skin mới từ database
        HoleSkin newSkin = holeSkinDB.GetHoleSkin(index);

        // Tạo skin mới làm con của Hole
        currentSkin = Instantiate(newSkin.holeSkin, holeParent);
        currentSkin.transform.localPosition = Vector3.zero;
        currentSkin.transform.localRotation = Quaternion.identity;

        // Lưu lựa chọn vào GameDataManager
        GameDataManager.Instance.SetSelectedHoleSkin(index);

    }
}
