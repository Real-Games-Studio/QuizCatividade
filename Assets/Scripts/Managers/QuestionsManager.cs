using System.Collections.Generic;
using System.Linq;
using MoreMountains.Feedbacks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class QuestionsManager : MonoBehaviour
{
#if UNITY_EDITOR
    private ContainerQuestions _questionContainer = JsonManagers.Instance.GetJson<ContainerQuestions>(Application.streamingAssetsPath + "/questions.json");
    private ComboData _comboContainer = JsonManagers.Instance.GetJson<ComboData>(Application.streamingAssetsPath + "/gameconfig.json");
#else
    private ContainerQuestions _questionContainer = JsonManagers.Instance.GetJson<ContainerQuestions>("contents/questions.json");
    private ComboData _comboContainer = JsonManagers.Instance.GetJson<ComboData>("contents/gameconfig.json");
#endif

    private int _questionNumber = 0;
    private List<int> _questionNumberSorteds = new List<int>();
    private int randomIndex;
    private int lastRandomIndex = -1;
    private int _comboCount;
    private int _currentComboNumber, _comboIndex;
    private int _score = 0;

    [SerializeField] private TextMeshProUGUI _questionTitle;
    [SerializeField] private TextMeshProUGUI _questionText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private ButtonAUX[] _answers;

    public int Score { get => _score; set => _score = value; }

    private void Awake()
    {
        for (int i = 5; i > _questionContainer.NumberOfAnswers - 1; i--)
            _answers[i].gameObject.SetActive(false);

        _questionContainer.Perguntas.Sort((a, b) => Random.Range(-1, 2));
    }

    void Start()
    {
        UpdateQuestion();
    }

    public void UpdateQuestion()
    {
        for (int i = 0; i < _questionContainer.Perguntas.Count; i++)
        {
            Debug.Log(_questionContainer.Perguntas[i].Pergunta);
        }

        if (_questionNumber == _questionContainer.Perguntas.Count)
        {
            Managers.Instance.GameManager.SetWinState();
            return;
        }

        _questionText.text = _questionContainer.Perguntas[_questionNumber].Pergunta;

        for (int i = 0; i < _answers.Length; i++)
            _answers[i].Text.text = _questionContainer.Perguntas[_questionNumber].Respostas[i];

        _currentComboNumber = _comboContainer.Combos[_comboIndex];

        UpdatePoints();
        Managers.Instance.FeelManager.PlayFeelButton();

        for (int i = 0; i < _answers.Length; i++)
            _answers[i].CanPress(true);
    }

    public void CheckAnswer(ButtonAUX button)
    {
        Managers.Instance.InputManager.UnlockAllButtons();

        if (_questionContainer.Perguntas[_questionNumber].RespostaCerta == button.Text.text)
        {
            _questionNumber++;
            Score += 10;
            button.CorrectFeel.PlayFeedbacks();
            CheckCombo();
            Managers.Instance.FeelManager.PlayFeelAddPoints();
            Managers.Instance.SoundManager.PlayCorrect();

            for (int i = 0; i < _answers.Length; i++)
                _answers[i].CanPress(false);

        }
        else
        {
            if (Score >= 2) Score -= 2;
            _comboCount = 0;
            button.WrongFeel.PlayFeedbacks();

            Managers.Instance.FeelManager.PlayFeelRemovePoints();
            Managers.Instance.SoundManager.PlayWrong();
        }

        UpdatePoints();
    }

    private void CheckCombo()
    {
        _comboCount++;

        if (_comboCount == _currentComboNumber)
        {
            _comboIndex++;
            _currentComboNumber = _comboContainer.Combos[_comboIndex];
            Score += 25 * _comboIndex;
            Debug.Log($"Combo {Score}");
        }

    }

    private void UpdatePoints()
    {
        _scoreText.text = Score.ToString();
    }
    public void NextQuestion()
    {
        UpdateQuestion();
    }
}

