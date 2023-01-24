using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonEvents : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isPressing;

    public UnityEvent onHold = new UnityEvent();


    private void Update()
    {
        if (isPressing)
        {
            onHold?.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressing = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressing = false;
    }
}