using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] Camera cam;
    RaycastHit2D hitInfo;

    [SerializeField] GameObject mousePoinerUI;
    private RectTransform rect;
    private Vector2 originalSize;

    private bool isContact = false;
    private bool isInteract = false;

    private DialogueManager theDM;

    private void Start()
    {
        rect = mousePoinerUI.GetComponent<RectTransform>();
        originalSize = rect.sizeDelta;
        theDM = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        if (!isInteract)
        {
            CheckObject();
            ClickLeftButton();
        }
    }

    public void SettingMousePoinerUI(bool p_flag)
    {
        mousePoinerUI.SetActive(p_flag);
        isInteract = !p_flag;
    }

    private void CheckObject()
    {
        Vector3 t_mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        t_mousePos.z = 0;

        hitInfo = Physics2D.Raycast(t_mousePos, Vector2.zero);

        if (hitInfo.collider != null)
        {
            Contact();
        }
        else
        {
            NotContact();
        }
    }

    private void Contact()
    {
        if (hitInfo.transform.CompareTag("Interaction"))
        {
            if (!isContact)
                isContact = true;

            rect.sizeDelta = originalSize * 1.5f;
        }
        else
        {
            NotContact();
        }

    }

    private void NotContact()
    {
        if (isContact)
            isContact = false;

        rect.sizeDelta = originalSize;
    }

    private void ClickLeftButton()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(isContact)
            {
                Interact();
            }
        }
    }

    private void Interact()
    {
        isInteract = true;
        // 상호작용가능오브젝트를 클릭했을때

        theDM.ShowDialogue(hitInfo.transform.GetComponent<InteractionEvent>().GetDialogue()); // 부딪힌 객체가 갖고있는 InteractionEvent값을 가져온다
    }
}
