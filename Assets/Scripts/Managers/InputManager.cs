using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputAction _anyButtonsController;
    [SerializeField] private InputAction[] _buttonsControllers;

    [SerializeField] private Button _initialScreenbutton;
    [SerializeField] private Button[] _button;
    private bool canPress = false;
    public bool CanPress { get => canPress; set => canPress = value; }

    private void Awake()
    {
        _anyButtonsController.Enable();

        foreach (InputAction input in _buttonsControllers)
            input.Enable();
    }

    private void Update()
    {
        if ((CheckKeyPressed() || CheckButtonPressed()) && _initialScreenbutton.enabled)
        {
            _initialScreenbutton.onClick.Invoke();
            _initialScreenbutton.enabled = false;
        }

        if (CanPress)
        {
            InputForKeyboard(0, KeyCode.Alpha1);
            InputForKeyboard(1, KeyCode.Alpha2);
            InputForKeyboard(2, KeyCode.Alpha3);
            InputForKeyboard(3, KeyCode.Alpha4);
            InputForKeyboard(4, KeyCode.Alpha5);

            // InputForController(0, 0);
            // InputForController(1, 1);
            // InputForController(2, 2);
            // InputForController(3, 3);
            // InputForController(4, 4);

            InputForControllerOld(0, "Fire1");
            InputForControllerOld(1, "Fire2");
            InputForControllerOld(2, "Fire3");
            InputForControllerOld(3, "Jump");

        }



    }

    private bool CheckKeyPressed()
    {
        if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Alpha5)) return true;
        else return false;
    }

    private bool CheckButtonPressed()
    {
        if (_anyButtonsController.ReadValue<float>() > .5f) return true;
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

    private void InputForController(int bntIndex, int controllBntOrder)
    {
        if (_buttonsControllers[controllBntOrder].WasPressedThisFrame())
        {
            if (_button[bntIndex].interactable) ExecuteEvents.Execute(_button[bntIndex].gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
        }
        if (_buttonsControllers[controllBntOrder].WasReleasedThisFrame())
        {
            if (_button[bntIndex].interactable)
            {
                _button[bntIndex].onClick.Invoke();
                ExecuteEvents.Execute(_button[bntIndex].gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
            }
        }

    }
    private void InputForControllerOld(int bntIndex, string controllBntOrder)
    {
        if (Input.GetButtonDown(name))
        {
            if (_button[bntIndex].interactable) ExecuteEvents.Execute(_button[bntIndex].gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerDownHandler);
        }
        if (Input.GetButtonUp(name))
        {
            if (_button[bntIndex].interactable)
            {
                _button[bntIndex].onClick.Invoke();
                ExecuteEvents.Execute(_button[bntIndex].gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerUpHandler);
            }
        }

    }
}