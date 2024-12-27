using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform tf_MouseCursor;

    // Update is called once per frame
    void Update()
    {
        MouseCursorMoving();
    }

    private void MouseCursorMoving()
    {
        tf_MouseCursor.localPosition = new Vector2(Input.mousePosition.x - (Screen.width / 2), Input.mousePosition.y - (Screen.height / 2));

        float t_cursorPosX = tf_MouseCursor.localPosition.x;
        float t_cursorPosY = tf_MouseCursor.localPosition.y;

        t_cursorPosX = Mathf.Clamp(t_cursorPosX, (-Screen.width / 2), (Screen.width / 2));
        t_cursorPosY = Mathf.Clamp(t_cursorPosY, (-Screen.height / 2), (Screen.height / 2));

        tf_MouseCursor.localPosition = new Vector2(t_cursorPosX, t_cursorPosY);
    }
}
