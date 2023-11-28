using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Button _initialScreenbutton;
    [SerializeField] private Button[] _buttons;
    [SerializeField] private float requiredHoldTime;

    private bool canPress = false;
    private float timer;
    private float timerRestart;

    public bool CanPress { get => canPress; set => canPress = value; }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if ((CheckKeyPressed() || CheckOldButtonPressedWithTimer()) && _initialScreenbutton.enabled)
        {
            _initialScreenbutton.onClick.Invoke();
            _initialScreenbutton.enabled = false;
            Debug.Log("rodei");
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

            InputForController(0, "FireOne");
            InputForController(1, "Fire2");
            InputForController(2, "Fire3");
            InputForController(3, "Jump");
            InputForLastButton();
        }

        if (Managers.Instance.GameManager.FinalScreen && CheckOldButtonPressed())
        {
            Managers.Instance.SceneManagement.RestartScene();
            Managers.Instance.GameManager.FinalScreen = false;
        }
    }

    private void InputForKeyboard(int bntIndex, KeyCode keyCode)
    {
        if (Input.GetKeyDown(keyCode))
        {
            if (_buttons[bntIndex].interactable) ExecuteEvents.Execute(_buttons[bntIndex].gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
        }
        if (Input.GetKeyUp(keyCode))
        {
            if (_buttons[bntIndex].interactable)
            {
                // _buttons[bntIndex].onClick.Invoke();
                ExecuteEvents.Execute(_buttons[bntIndex].gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
            }
        }

    }
    private void InputForController(int buttonIndex, string buttonName)
    {
        if (Input.GetButtonDown(buttonName))
        {
            if (_buttons[buttonIndex].interactable) ExecuteEvents.Execute(_buttons[buttonIndex].gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
        }
        if (Input.GetButtonUp(buttonName))
        {
            if (_buttons[buttonIndex].interactable)
            {
                // _buttons[buttonIndex].onClick.Invoke();
                ExecuteEvents.Execute(_buttons[buttonIndex].gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
            }
        }
    }
    private void InputForLastButton()
    {
        if (Input.GetButtonDown("SLT") || Input.GetButtonDown("SRT") || Input.GetButtonDown("SLT2") || Input.GetButtonDown("SRT2") || Input.GetButtonDown("SRT3") || Input.GetButtonDown("Start"))
        {
            if (_buttons[4].interactable) ExecuteEvents.Execute(_buttons[4].gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
        }
        if (Input.GetButtonUp("SLT") || Input.GetButtonUp("SRT") || Input.GetButtonUp("SLT2") || Input.GetButtonUp("SRT2") || Input.GetButtonUp("SRT3") || Input.GetButtonUp("Start"))
        {
            if (_buttons[4].interactable)
            {
                _buttons[4].onClick.Invoke();
                ExecuteEvents.Execute(_buttons[4].gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
            }
        }

    }
    private bool CheckKeyPressed()
    {
        if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Alpha5)) return true;
        else return false;
    }
    private bool CheckRestartSceneInput()
    {
        if (Input.GetButton("FireOne"))
        {
            timerRestart += Time.deltaTime;
            if (timerRestart > 5f)
            {
                timerRestart = 0f;
                return true;
            }
        }
        else
        {
            timerRestart = 0f;
            return false;
        }
        return false;
    }
    private bool CheckOldButtonPressedWithTimer()
    {
        if (Input.GetButton("FireOne") || Input.GetButton("Fire2") || Input.GetButton("Fire3") || Input.GetButton("Jump") || Input.GetButton("SLT") || Input.GetButton("SRT") || Input.GetButton("SLT2") || Input.GetButton("SRT2") || Input.GetButton("SRT3"))
        {
            timer += Time.deltaTime;
            if (timer > requiredHoldTime)
            {
                timer = 0f;
                return true;
            }
        }
        else
        {
            timer = 0f;
            return false;
        }

        return false;
    }
    private bool CheckOldButtonPressed()
    {
        if (Input.GetButton("FireOne") || Input.GetButton("Fire2") || Input.GetButton("Fire3") || Input.GetButton("Jump") || Input.GetButton("SLT") || Input.GetButton("SRT") || Input.GetButton("SLT2") || Input.GetButton("SRT2") || Input.GetButton("SRT3"))
        {
            return true;
        }
        else return false;
    }

}