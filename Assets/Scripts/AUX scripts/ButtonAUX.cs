using UnityEngine;
using TMPro;
using MoreMountains;
using MoreMountains.Feedbacks;
using UnityEngine.UI;

public class ButtonAUX : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private MMF_Player _correctFeel, wrongFeel;
    private Button _button;

    public MMF_Player CorrectFeel { get => _correctFeel; set => _correctFeel = value; }
    public MMF_Player WrongFeel { get => wrongFeel; set => wrongFeel = value; }
    public TextMeshProUGUI Text { get => _text; set => _text = value; }

    public void CanPress(bool state)
    {
        _button = GetComponent<Button>();
        _button.interactable = state;
    }
}