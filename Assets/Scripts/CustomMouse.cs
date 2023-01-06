using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomMouse : MonoBehaviour
{
    public enum MOUSESTATE {IDLE,IMPORTANT,CLICK };
    public MOUSESTATE mouseState;
    public List<Sprite> spriteInstances;
    private Image ImageComponent;


    [Header("Follow Mouse Settings")]
    public RectTransform rectTransform;
    public float smooth = 0.5f;
    public Vector2 offset;
    public Canvas myCanvas;
    private Vector2 targetPos;
    
    void Start()
    {
        mouseState = MOUSESTATE.IDLE;
        ImageComponent = this.gameObject.GetComponent<Image>();
    }

    void Update()
    {
        FollowMouse();
        switch (mouseState)
        {
            case MOUSESTATE.IDLE:
                {
                    ImageComponent.sprite = spriteInstances[0];
                }
                break;
            case MOUSESTATE.IMPORTANT:
                {
                    ImageComponent.sprite = spriteInstances[1];
                }
                break;
            case MOUSESTATE.CLICK:
                {
                    ImageComponent.sprite = spriteInstances[2];
                    Invoke("SetIdle", 0.25f);
                }
                break;
            default:
                break;
        }
    }
    private void FollowMouse()
    {

        // Get the mouse position in screen coordinates
        Vector2 mousePos ;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out mousePos);
        // Add the offset to the mouse position to calculate the target position
        targetPos = mousePos + offset;
        //Use lerp to move in a smooth way
        rectTransform.anchoredPosition = Vector3.Lerp(rectTransform.anchoredPosition, targetPos, smooth * Time.deltaTime);

    }
    public void SetImportant()
    {
        mouseState = MOUSESTATE.IMPORTANT;
        GameManager.Instance.soundManager.TriggerSound(1, 0.3f);
    }
    public void SetClick()
    {
        mouseState = MOUSESTATE.CLICK;
        GameManager.Instance.soundManager.TriggerSound(0, 0.3f);
    }

    public void SetIdle()
    {
        mouseState = MOUSESTATE.IDLE;
    }
}


