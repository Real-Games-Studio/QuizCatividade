
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
#if UNITY_EDITOR
    private GameConfig _gameConfig = JsonManagers.Instance.GetJson<GameConfig>(Application.streamingAssetsPath + "/gameconfig.json");
#else
        private GameConfig _gameConfig = JsonManagers.Instance.GetJson<GameConfig>("contents/gameconfig.json");
#endif

    [SerializeField] private TextMeshProUGUI _timerText;
    private float timer;

    [SerializeField] private Color _inicialColor, _midColor, _endColor;
    [SerializeField] private Image _clockSprite;


    private void Start()
    {
        timer = _gameConfig.Tempo;
        _clockSprite.color = _inicialColor;
    }

    void Update()
    {
        if (Managers.Instance.GameManager.CanStartTimer)
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
                UpdateTextTime();
            }
            else
            {
                Managers.Instance.GameManager.SetWinState();
            }
    }

    void UpdateTextTime()
    {
        int seconds = Mathf.RoundToInt(timer);
        _timerText.text = seconds.ToString();

        float remainingTime = Mathf.Clamp01(timer / _gameConfig.Tempo);
        Color newColor;


        if (remainingTime > .5f)
        {
            newColor = Color.Lerp(_inicialColor, _midColor, 2.5f - remainingTime * 3f);
            _clockSprite.color = newColor;
            _timerText.color = newColor;
        }
        else
        {
            newColor = Color.Lerp(_midColor, _endColor, 1 - remainingTime * 3f);
            _clockSprite.color = newColor;
            _timerText.color = newColor;
        }
    }
}