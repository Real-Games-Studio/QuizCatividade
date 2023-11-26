[System.Serializable]
public class QuestionAnswer
{
    private string respostaCerta;
    private string pergunta;
    private string[] respostas;

    public string Pergunta { get => pergunta; set => pergunta = value; }
    public string[] Respostas { get => respostas; set => respostas = value; }
    public string RespostaCerta { get => respostaCerta; set => respostaCerta = value; }
}

[System.Serializable]
public class ContainerQuestions
{
    private QuestionAnswer[] perguntas;
    private int numberOfAnswers;

    public QuestionAnswer[] Perguntas { get => perguntas; set => perguntas = value; }
    public int NumberOfAnswers { get => numberOfAnswers; set => numberOfAnswers = value; }
}