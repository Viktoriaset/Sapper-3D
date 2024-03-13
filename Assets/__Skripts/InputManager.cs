using UnityEngine;

public abstract class InputManager : MonoBehaviour
{
    [Header("Set in inspector: InputManger")]
    [SerializeField]
    private float angleRotationForHalfScreen = 90;

    [SerializeField]
    public GameObject gameField;
    [SerializeField]
    public GameObject mainCamera;

    private float width = Screen.width / 2;
    private float height = Screen.height / 2;

    public void RotateOnHorizontal(float distance)
    {
        float angle = CountHorizontalAngle(distance);
        Quaternion q = Quaternion.Euler(0, angle, 0);
        gameField.transform.rotation = q * gameField.transform.rotation;
    }

    public void RotateOnVertical(float distance)
    {
        float angle = CountVerticalAngle(distance);
        Quaternion q = Quaternion.Euler(angle, 0, 0);
        gameField.transform.rotation = q * gameField.transform.rotation;
    }

    public void ChangeCameraDistance(float distance)
    {
        Vector3 pos = mainCamera.transform.position;
        pos.z += distance;
        mainCamera.transform.position = pos;
    }

    private float CountHorizontalAngle(float distance)
    {
        float angle = distance / width * angleRotationForHalfScreen;
        return angle;
    }

    private float CountVerticalAngle(float distance)
    {
        float angle = distance / height * angleRotationForHalfScreen;
        return angle;
    }
}
