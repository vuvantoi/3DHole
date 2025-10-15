using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] private ObjectData objectData;
    public ObjectData ObjectData => objectData;

    public void OnEaten()
    {
        // Thêm hiệu ứng, âm thanh, trả về pool ở đây
        gameObject.SetActive(false);
    }
}
