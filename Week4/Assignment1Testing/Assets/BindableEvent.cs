using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BindableEvent : MonoBehaviour
{
    public enum Event
    {
        OnClick = 0
    }
    [System.Serializable]
    public class Binding
    {
        public Event eventType;
        public UnityEvent onEvent;
    }
    public List<Binding> bindings = new List<Binding>();
    bool _mouseIn = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    void HandleDispatch(Event e)
    {
        Binding bind = bindings.Find((Binding b) => b.eventType == e);
        if (bind == null) return;

        bind.onEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleDispatch(Event.OnClick);
        }
    }
    void OnMouseEnter()
    {
        _mouseIn = true;
    }

    void OnMouseOver()
    {
    }
    void OnMouseExit()
    {
        _mouseIn = false;
    }
}
