using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuCarousel : MonoBehaviour
{
    public TextMeshProUGUI modeText;
    public Button leftButton;
    public Button rightButton;
    public Button goButton;

    string[] modes = { "Mode1", "Mode2", "Mode3", "Mode4", "Mode5" };
    int currentIndex = 0;

    void Start()
    {
        modeText.text = modes[currentIndex];
        leftButton.onClick.AddListener(OnLeft);
        rightButton.onClick.AddListener(OnRight);
        goButton.onClick.AddListener(turnMode);
    }
    void OnLeft()
    {
        Debug.Log("◀ Left Button Clicked");
        currentIndex = (currentIndex - 1 + modes.Length) % modes.Length;
        modeText.text = modes[currentIndex];
    }

    void OnRight()
    {
        Debug.Log("▶ Right Button Clicked");
        currentIndex = (currentIndex + 1) % modes.Length;
        modeText.text = modes[currentIndex];
    }

    void turnMode()
    {
        string target = modes[currentIndex];
        Debug.Log($"[MainMenu] Try load: '{target}'");
        if (!Application.CanStreamedLevelBeLoaded(target))
        {
            Debug.LogError($"[MainMenu] Scene '{target}' 로드 불가! Build Settings에 추가됐는지/이름이 정확한지 확인.");
            return;
        }
        SceneManager.LoadScene(target, LoadSceneMode.Single);
    }

}
