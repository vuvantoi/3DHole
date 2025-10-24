using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GamePlay");

        SceneManager.sceneLoaded += OnGamePlayLoaded;
    }

    private void OnGamePlayLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GamePlay")
        {
            // Load thêm Map dạng additive
            SceneManager.LoadScene("SCN_Map", LoadSceneMode.Additive);

            // Gỡ sự kiện để tránh gọi lại nhiều lần
            SceneManager.sceneLoaded -= OnGamePlayLoaded;
        }
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        // Thoát play mode khi đang chạy trong Editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Thoát ứng dụng thật (khi build)
        Application.Quit();
#endif
    }
}
