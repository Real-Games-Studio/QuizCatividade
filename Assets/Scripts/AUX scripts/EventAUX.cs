using UnityEngine;
using UnityEngine.EventSystems;

public class EventAUX : MonoBehaviour
{
    private EventSystem _eventSystem;

    private void Start()
    {
        _eventSystem = GetComponent<EventSystem>();
    }

    private void Update()
    {
        if (_eventSystem.currentSelectedGameObject != null) _eventSystem.SetSelectedGameObject(null);
    }
}