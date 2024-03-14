using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controll : MonoBehaviour
{
   public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        if (audioSource == null)
        {
            audioSource = FindObjectOfType<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public GUIStyle boxStyle;
    public GUIStyle btnStyle;
    public GUIStyle btnTamDung;
    public GUIStyle btnTatNhac;
    public GUIStyle btnExit;
    public Texture2D playTexture;

    public Texture2D pauseTexture;

    // Biến flag để kiểm soát trạng thái hiển thị của box
    private bool isBoxVisible = false;
    private bool isGamePaused = false;

    private void OnGUI()
    {
        float W = Screen.width;
        float H = Screen.height;

        if (GUI.Button(new Rect(120, 30, 60, 55), "", btnStyle))
        {
            // Khi button được click, đảo ngược trạng thái hiển thị của box
            isBoxVisible = !isBoxVisible;
        }

        // Nếu biến flag là true, hiển thị box
        if (isBoxVisible)
        {
            // Hiển thị hình ảnh tương ứng với trạng thái (play hoặc pause)
            Texture2D currentTexture = isGamePaused ? playTexture : pauseTexture;
            GUI.Box(new Rect((W - 300) / 2, (H - 200) / 2, 300, 300), "", boxStyle);
            if (GUI.Button(new Rect((W - 180) / 2, (H - 70) / 2, 60, 55), currentTexture, btnTamDung))
            {
                if (isGamePaused)
                {
                    // Tiếp tục hoạt động khi đã tạm dừng
                    Time.timeScale = 1f;
                    Debug.Log("Tiếp tục game");
                }
                else
                {
                    // Dừng game
                    Time.timeScale = 0f;
                    Debug.Log("Dừng game");
                }

                // Đảo ngược trạng thái tạm dừng
                isGamePaused = !isGamePaused;
            }


            if (GUI.Button(new Rect((W + 60) / 2, (H - 70) / 2, 60, 55), "", btnTatNhac))
            {
                if (audioSource != null)
                {
                    // Đảo ngược trạng thái mute của AudioListener để tắt/bật âm thanh
                    AudioListener.pause = !AudioListener.pause;

                    // Nếu bạn muốn tắt cả audioSource, bạn có thể sử dụng:
                    // audioSource.mute = !audioSource.mute;
                }
            }

            if (GUI.Button(new Rect((W - 70) / 2, (H + 150) / 2, 60, 55), "", btnExit))
            {
                Debug.Log("Exit");
                Time.timeScale = 1f;
                SceneManager.LoadScene("Manchoi1");
            }
        }
    }
}
