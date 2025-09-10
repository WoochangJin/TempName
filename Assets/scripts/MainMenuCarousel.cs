using UnityEngine;
using UnityEngine.UI;   // UnityEngine.UI 꼭 필요!
using TMPro;

public class MainMenuCarousel : MonoBehaviour
{
    public TextMeshProUGUI modeText;
    public Button leftButton;
    public Button rightButton;

    string[] modes = { "Tarot", "Cookie", "Zodiac" };
    int currentIndex = 0;

    void Start()
    {
        modeText.text = modes[currentIndex];
        leftButton.onClick.AddListener(OnLeft);
        rightButton.onClick.AddListener(OnRight);
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
}
