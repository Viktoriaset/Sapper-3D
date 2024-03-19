using UnityEngine;

public abstract class InputManager : MonoBehaviour
{
    public enum eClickType
    {
        open,
        flag
    }

    #region Properties
    [Header("Set in inspector: InputManger")]
    [SerializeField] private float angleRotationForHalfScreen = 90;
    public eClickType clickType = eClickType.open;

    private GameObject gameField;
    private float width = Screen.width / 2;
    private float height = Screen.height / 2;
    private int clickNumber = 0;
    #endregion

    #region Methods
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
        Vector3 pos = Camera.main.transform.position;
        pos.z += distance;
        Camera.main.transform.position = pos;
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

    public void ClickRayCast(Vector3 mousePosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Cell cell;
            if (hit.collider.TryGetComponent(out cell))
            {
                switch(clickType)
                {
                    case eClickType.open:
                        if (clickNumber == 0)
                            cell.DiactivateBombAndOpen(3, 1);
                        else 
                            cell.Opening();
                        clickNumber++;
                        break;

                    case eClickType.flag:
                        cell.ChangeFlag();
                        break;
                }
            }
        }
    }
    #endregion

    #region MonoBehavior Methods

    void Start()
    {
        gameField = GameObject.Find("GameField");
    }

    #endregion
}
