using UnityEngine;

public class InputManagerPC: InputManager
{
    private Vector3 startMousePos;
    private Vector3 currentMousePos;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Input.mousePosition;
        }

        if (Input.GetMouseButton(0))
        {
            currentMousePos = Input.mousePosition;

            RotateOnHorizontal(currentMousePos.x - startMousePos.x);
            RotateOnVertical(currentMousePos.y - startMousePos.y);

            startMousePos = currentMousePos;
        }
        
        if (Mathf.Abs(Input.mouseScrollDelta.y) > 0)
        {
            ChangeCameraDistance(Input.mouseScrollDelta.y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            currentMousePos = Input.mousePosition;
            if (currentMousePos == startMousePos)
                ClickRayCast(currentMousePos);
        }
    }
}
