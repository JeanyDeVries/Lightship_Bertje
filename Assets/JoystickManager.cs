using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class JoystickManager : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image bgJoystickImg;
    private Image joystickImg;
    private Vector2 posInput;

    // Start is called before the first frame update
    void Start()
    {
        bgJoystickImg= GetComponent<Image>();
        joystickImg= transform.GetChild(0).GetComponent<Image>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(
            bgJoystickImg.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out posInput))
        {
            posInput.x = posInput.x / (bgJoystickImg.rectTransform.sizeDelta.x);
            posInput.y = posInput.y / (bgJoystickImg.rectTransform.sizeDelta.y);
        }

        //normalize input
        if(posInput.magnitude > 1.0f)
        {
            posInput = posInput.normalized;
        }

        //movement joystick
        joystickImg.rectTransform.anchoredPosition = new Vector2(
            posInput.x * (bgJoystickImg.rectTransform.sizeDelta.x / 2), 
            posInput.y * (bgJoystickImg.rectTransform.sizeDelta.y / 2));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        posInput = Vector2.zero;
        joystickImg.rectTransform.anchoredPosition = Vector2.zero;
    }

    public float InputHorizontal()
    {
        if (posInput.x != 0)
            return posInput.x;
        else
            return Input.GetAxis("horizontal");
    }    
    
    public float InputVertical()
    {
        if (posInput.y != 0)
            return posInput.y;
        else
            return Input.GetAxis("vertical");
    }
}
