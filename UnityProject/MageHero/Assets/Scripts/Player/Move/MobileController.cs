using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MobileController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{

    private Image joystickBG;
    [SerializeField]
    private Image joystic;

    private Vector2 inputVector;


    private void Start()
    {

        joystickBG = GetComponent<Image>();
        joystic = transform.GetChild(0).GetComponent<Image>();

    }

    public void OnPointerUp(PointerEventData eventData)
    {

        inputVector = Vector2.zero;
        joystic.rectTransform.anchoredPosition = Vector2.zero;
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        OnDrag(eventData);

    }

    public void OnDrag(PointerEventData eventData)
    {

        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBG.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {

            pos.x = (pos.x / joystickBG.rectTransform.sizeDelta.x);
            pos.y = (pos.y / joystickBG.rectTransform.sizeDelta.y);

            inputVector = new Vector2(pos.x * 2, pos.y * 2);
            inputVector = (inputVector.magnitude > 1f) ? inputVector.normalized : inputVector;

            joystic.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystickBG.rectTransform.sizeDelta.x / 2), inputVector.y * (joystickBG.rectTransform.sizeDelta.y / 2));

        }

    }

    public float Horizontal()
    {
        if (inputVector.x != 0) return inputVector.x;
        else return Input.GetAxis("Horizontal");
    }

    public float Vertival()
    {
        if (inputVector.y != 0) return inputVector.y;
        else return Input.GetAxis("Vertical");
    }

}
