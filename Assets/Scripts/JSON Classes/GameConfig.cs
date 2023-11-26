public class GameConfig
{
    private float tempo;
    private float tempoParaReniciar;
    private string tituloJogo;
    private string textoVitoria;

    public float Tempo { get => tempo; set => tempo = value; }
    public string TituloJogo { get => tituloJogo; set => tituloJogo = value; }
    public string TextoVitoria { get => textoVitoria; set => textoVitoria = value; }
    public float TempoParaReniciar { get => tempoParaReniciar; set => tempoParaReniciar = value; }
}