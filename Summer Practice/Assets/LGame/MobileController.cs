using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// управление для джойстика (без возравта обратно)

public class MobileController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private Image joystickPl;
    [SerializeField]
    private Image joystick;
    private Vector2 inputVector;

    private void Start() {
        joystickPl = GetComponent<Image>();
        joystick = transform.GetChild(0).GetComponent<Image>();
    }

    public virtual void OnPointerDown(PointerEventData ped)
    {

    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector2.zero;
        joystick.rectTransform.anchoredPosition = Vector2.zero; // возврат в центр
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        Vector2 pos;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickPl.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            // получение координат при смещение
            pos.x = (pos.x / joystickPl.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joystickPl.rectTransform.sizeDelta.y);

            inputVector = new Vector2(pos.x * 1.7f - 1, pos.y * 1.7f - 1);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            joystick.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystickPl.rectTransform.sizeDelta.x / 3),
                                                                    inputVector.y * (joystickPl.rectTransform.sizeDelta.y / 3));
        }
    }

    public float Horizontal()
    {
        if (inputVector.x != 0) return inputVector.x;
        else return Input.GetAxis("Horizontal");
    }

    public float Vertical()
    {
        if (inputVector.y != 0) return inputVector.y;
        else return Input.GetAxis("Vertical");
    }
}

