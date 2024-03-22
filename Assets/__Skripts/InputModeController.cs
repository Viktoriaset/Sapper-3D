using UnityEngine;
using Zenject;

public class InputModeController : MonoBehaviour
{
    #region Properties
    [Header("Set in inspector")]
    [SerializeField] private RectTransform switchBG;
    [SerializeField] private Vector2 minePos;
    [SerializeField] private Vector2 flagPos;
    [SerializeField] private float smoothing = 4f;
    private Vector2 targetPos;
    [Inject] private InputManager inputManager;
    #endregion

    #region Methods
    public void TurnFlagMode()
    {
        targetPos = flagPos;
        inputManager.clickType = InputManager.eClickType.flag;
    }

    public void TurnMineMode()
    {
        targetPos = minePos;
        inputManager.clickType = InputManager.eClickType.open;
    }
    #endregion

    #region MonoBehavior Methods
    void Start()
    {
        targetPos = minePos;
    }
    private void Update()
    {
        if (switchBG.anchoredPosition != targetPos)
        {
            switchBG.anchoredPosition =
                Vector3.Lerp(switchBG.anchoredPosition, targetPos, smoothing*Time.fixedDeltaTime);
        }
    }
    #endregion
}
