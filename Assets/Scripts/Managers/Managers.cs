using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers Instance;

    [SerializeField] private QuestionsManager _questionsManager;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private JsonManagers _jsonManager;
    [SerializeField] private SceneManagement _sceneManagement;
    [SerializeField] private FeelManager _feelManager;
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private InputManager _inputManager;

    public QuestionsManager QuestionsManager { get => _questionsManager; set => _questionsManager = value; }
    public GameManager GameManager { get => _gameManager; set => _gameManager = value; }
    public JsonManagers JsonManager { get => _jsonManager; set => _jsonManager = value; }
    public SceneManagement SceneManagement { get => _sceneManagement; set => _sceneManagement = value; }
    public FeelManager FeelManager { get => _feelManager; set => _feelManager = value; }
    public SoundManager SoundManager { get => _soundManager; set => _soundManager = value; }
    public InputManager InputManager { get => _inputManager; set => _inputManager = value; }

    private void Awake()
    {
        Instance = this;
    }

}