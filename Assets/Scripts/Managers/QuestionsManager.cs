using System;
using System.Collections.Generic;
using System.IO;
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
    private DateTime _startTime;
    private DateTime _endTime;
    private List<int> _questionScores = new List<int>();
    private static int _sessionCount = 1;

    [SerializeField] private TextMeshProUGUI _questionTitle;
    [SerializeField] private TextMeshProUGUI _questionText;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private ButtonAUX[] _answers;

    public int Score { get => _score; set => _score = value; }
    public DateTime EndTime { get => _endTime; set => _endTime = value; }

    private void Awake()
    {
        for (int i = 5; i > _questionContainer.NumberOfAnswers - 1; i--)
            _answers[i].gameObject.SetActive(false);

        _questionContainer.Perguntas.Sort((a, b) => UnityEngine.Random.Range(-1, 2));
    }

    void Start()
    {
        _startTime = DateTime.Now;
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
            _endTime = DateTime.Now;
            SaveResultsToCSV();
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
        int questionScore = 0;

        if (_questionContainer.Perguntas[_questionNumber].RespostaCerta == button.Text.text)
        {
            _questionNumber++;
            questionScore = 10;
            Score += questionScore;
            button.CorrectFeel.PlayFeedbacks();
            CheckCombo();
            Managers.Instance.FeelManager.PlayFeelAddPoints();
            Managers.Instance.SoundManager.PlayCorrect();

            for (int i = 0; i < _answers.Length; i++)
                _answers[i].CanPress(false);
        }
        else
        {
            questionScore = Score >= 2 ? -2 : 0;
            Score += questionScore;
            _comboCount = 0;
            button.WrongFeel.PlayFeedbacks();

            Managers.Instance.FeelManager.PlayFeelRemovePoints();
            Managers.Instance.SoundManager.PlayWrong();
        }

        _questionScores.Add(questionScore);
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

    public void SaveResultsToCSV()
    {
        string path = Path.Combine(Application.dataPath, "results.csv");
        string delimiter = ",";

        bool fileExists = File.Exists(path);

        using (StreamWriter writer = new StreamWriter(path, true))
        {
            if (!fileExists)
            {
                writer.WriteLine("Session" + delimiter + "StartTime" + delimiter + "EndTime" + delimiter + "TotalScore" + delimiter + string.Join(delimiter, Enumerable.Range(1, _questionScores.Count).Select(i => "Question" + i + "Score")));
            }

            string[] results = new string[_questionScores.Count + 4];
            results[0] = _sessionCount.ToString();
            results[1] = _startTime.ToString("o");
            results[2] = _endTime.ToString("o");
            results[3] = Score.ToString();
            for (int i = 0; i < _questionScores.Count; i++)
            {
                results[i + 4] = _questionScores[i].ToString();
            }

            writer.WriteLine(string.Join(delimiter, results));
        }

        Debug.Log($"Resultados salvos em {path}");
        _sessionCount++;
    }
}
