using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : BaseSingleton<GameDataManager>
{
    public int selectedHoleSkinIndex = 0;

    public void SetSelectedHoleSkin(int index)
    {
        selectedHoleSkinIndex = index;
    }

    public int GetSelectedHoleSkin()
    {
        return selectedHoleSkinIndex;
    }

    protected override void Awake()
    {
        base.Awake();
        // Có thể thêm các bước khởi tạo khác ở đây sau này (load dữ liệu, audio...)
    }
}
