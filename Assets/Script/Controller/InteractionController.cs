using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        rect = mousePoinerUI.GetComponent<RectTransform>();
        originalSize = rect.sizeDelta;
    }

    private void Update()
    {
        CheckObject();
        ClickLeftButton();
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
    }
}
