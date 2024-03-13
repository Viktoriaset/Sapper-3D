using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputModeController : MonoBehaviour
{
    [Header("Set in inspector")]
    [SerializeField]
    private GameObject switchBG;
    [SerializeField] 
    private Vector3 minePos;
    [SerializeField]
    private Vector3 flagPos;

    private Vector3 targetPos;

    public void TurnFlagMode()
    {
        targetPos = flagPos;
        Cell.FLAG_MODE = true;
    }

    public void TurnMineMode()
    {
        targetPos = minePos;
        Cell.FLAG_MODE = false;
    }

    private void Update()
    {
        if (switchBG.transform.localPosition != targetPos)
        {
            switchBG.transform.localPosition
                = Vector3.Lerp(switchBG.transform.localPosition, targetPos, Time.deltaTime);
        }
    }
}
