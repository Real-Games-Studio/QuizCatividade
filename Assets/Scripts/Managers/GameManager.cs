using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
#if UNITY_EDITOR
    private GameConfig _gameConfig = JsonManagers.Instance.GetJson<GameConfig>(Application.streamingAssetsPath + "/gameconfig.json");
#else
    private GameConfig _gameConfig = JsonManagers.Instance.GetJson<GameConfig>("contents/gameconfig.json");
#endif

    [SerializeField] private TextMeshProUGUI _titleGameText;
    [SerializeField] private TextMeshProUGUI _winText;
    [SerializeField] private MMF_Player _reloadSceneFeel;

    private bool canStartTimer = false;
    private bool finalScreen = false;
    public bool CanStartTimer { get => canStartTimer; set => canStartTimer = value; }
    public bool FinalScreen { get => finalScreen; set => finalScreen = value; }

    void Start()
    {
        _titleGameText.text = _gameConfig.TituloJogo;
        _reloadSceneFeel.GetFeedbacksOfType<MMF_Events>()[0].SetInitialDelay(_gameConfig.TempoParaReniciar);
    }

    public void UpdateWinText()
    {
        _winText.text = _gameConfig.TextoVitoria + Managers.Instance.QuestionsManager.Score + " pontos!";
    }

    public void TurnOnFinalScreen()
    {
        finalScreen = true;
    }
}
