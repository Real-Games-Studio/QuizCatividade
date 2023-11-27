using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Button _initialScreenbutton;
    [SerializeField] private Button[] _button;
    [SerializeField] private float requiredHoldTime;

    private bool canPress = false;
    private float timer;
    private float timerRestart;

    public bool CanPress { get => canPress; set => canPress = value; }

    private void Update()
    {
        if ((CheckKeyPressed() || CheckOldButtonPressedWithTimer()) && _initialScreenbutton.enabled)
        {
            _initialScreenbutton.onClick.Invoke();
            _initialScreenbutton.enabled = false;
        }

        if (CheckRestartSceneInput())
        {
            Managers.Instance.SceneManagement.RestartScene();
        }


        if (CanPress)
        {
            InputForKeyboard(0, KeyCode.Alpha1);
            InputForKeyboard(1, KeyCode.Alpha2);
            InputForKeyboard(2, KeyCode.Alpha3);
            InputForKeyboard(3, KeyCode.Alpha4);
            InputForKeyboard(4, KeyCode.Alpha5);

            InputForControllerOldNew(0, "Fire1");
            InputForControllerOldNew(1, "Fire2");
            InputForControllerOldNew(2, "Fire3");
            InputForControllerOldNew(3, "Jump");
            InputForLastButton();
        }

        if (Managers.Instance.GameManager.FinalScreen && CheckOldButtonPressed())
        {
            Managers.Instance.SceneManagement.RestartScene();
            Managers.Instance.GameManager.FinalScreen = false;
        }
    }

    private bool CheckKeyPressed()
    {
        if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Alpha5)) return true;
        else return false;
    }
    private bool CheckRestartSceneInput()
    {
        if (Input.GetButton("Fire1"))
        {
            timerRestart += Time.deltaTime;
            if (timerRestart > 5f)
            {
                timerRestart = 0f;
                return true;
            }
        }
        else return false;

        return false;
    }
    private bool CheckOldButtonPressedWithTimer()
    {
        if (Input.GetButton("Fire1") || Input.GetButton("Fire2") || Input.GetButton("Fire3") || Input.GetButton("Jump") || Input.GetButton("SLT") || Input.GetButton("SRT") || Input.GetButton("SLT2") || Input.GetButton("SRT2") || Input.GetButton("SRT3"))
        {
            timer += Time.deltaTime;
            if (timer > requiredHoldTime)
            {
                timer = 0f;
                return true;
            }
        }
        else return false;

        return false;
    }
    private bool CheckOldButtonPressed()
    {
        if (Input.GetButton("Fire1") || Input.GetButton("Fire2") || Input.GetButton("Fire3") || Input.GetButton("Jump") || Input.GetButton("SLT") || Input.GetButton("SRT") || Input.GetButton("SLT2") || Input.GetButton("SRT2") || Input.GetButton("SRT3"))
        {
            return true;
        }
        else return false;
    }

    private void InputForKeyboard(int bntIndex, KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
        {
            if (_button[bntIndex].interactable) ExecuteEvents.Execute(_button[bntIndex].gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
        }
        if (Input.GetKeyUp(keyCode))
        {
            if (_button[bntIndex].interactable)
            {
                _button[bntIndex].onClick.Invoke();
                ExecuteEvents.Execute(_button[bntIndex].gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
            }
        }

    }
    private void InputForControllerOldNew(int buttonIndex, string buttonName)
    {
        if (Input.GetButtonDown(buttonName))
        {
            if (_button[buttonIndex].interactable) ExecuteEvents.Execute(_button[buttonIndex].gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
        }
        if (Input.GetButtonUp(buttonName))
        {
            if (_button[buttonIndex].interactable)
            {
                _button[buttonIndex].onClick.Invoke();
                ExecuteEvents.Execute(_button[buttonIndex].gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
            }
        }
    }
    private void InputForLastButton()
    {
        if (Input.GetButtonDown("SLT") || Input.GetButtonDown("SRT") || Input.GetButtonDown("SLT2") || Input.GetButtonDown("SRT2") || Input.GetButtonDown("SRT3") || Input.GetButtonDown("Start"))
        {
            if (_button[4].interactable) ExecuteEvents.Execute(_button[4].gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
        }
        if (Input.GetButtonUp("SLT") || Input.GetButtonUp("SRT") || Input.GetButtonUp("SLT2") || Input.GetButtonUp("SRT2") || Input.GetButtonUp("SRT3") || Input.GetButtonUp("Start"))
        {
            if (_button[4].interactable)
            {
                _button[4].onClick.Invoke();
                ExecuteEvents.Execute(_button[4].gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
            }
        }

    }
}