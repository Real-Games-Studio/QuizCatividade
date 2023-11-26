using System.Collections.Generic;
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

    }

    void Start()
    {
        if (_questionNumberSorteds.Count < _questionContainer.Perguntas.Length)
            do
                randomIndex = Random.Range(0, _questionContainer.Perguntas.Length);
            while (_questionNumberSorteds.Contains(randomIndex));

        // _questionTitle.text = $"Pergunta {_questionNumber + 1}";
        lastRandomIndex = randomIndex;

        _questionText.text = _questionContainer.Perguntas[lastRandomIndex].Pergunta;
        UpdatePoints();

        for (int i = 0; i < _answers.Length; i++)
            _answers[i].Text.text = _questionContainer.Perguntas[randomIndex].Respostas[i];

        _currentComboNumber = _comboContainer.Combos[_comboIndex];
        Managers.Instance.FeelManager.PlayFeelButton();

        for (int i = 0; i < _answers.Length; i++)
            _answers[i].CanPress(true);

    }

    public void CheckAnswer(ButtonAUX button)
    {
        if (_questionContainer.Perguntas[lastRandomIndex].RespostaCerta == button.Text.text)
        {
            _questionNumber++;
            Score += 10;
            button.CorrectFeel.PlayFeedbacks();
            CheckCombo();
            Managers.Instance.FeelManager.PlayFeelAddPoints();
            Managers.Instance.SoundManager.PlayCorrect();

            for (int i = 0; i < _answers.Length; i++)
                _answers[i].CanPress(false);


            Debug.Log("certa!");
        }
        else
        {
            if (Score >= 2) Score -= 2;
            _comboCount = 0;
            Managers.Instance.FeelManager.PlayFeelRemovePoints();
            button.WrongFeel.PlayFeedbacks();
            Managers.Instance.SoundManager.PlayWrong();

            Debug.Log("errada!");
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
        Start();
    }
}

