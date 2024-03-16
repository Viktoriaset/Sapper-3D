using ExtensionMethods;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public enum eStates
    {
        flag,
        close,
        open
    }

    [Header("Set in inspector")]
    [SerializeField] private List<TextMeshPro> numbers;
    [SerializeField] private List<SpriteRenderer> flagRenderers;
    [SerializeField] private float bombSpawnChance = 0.05f;
    [SerializeField] private Color closeColor = Color.gray;
    [SerializeField] private Color openColor = Color.green;
    [SerializeField] private Color bombColor = Color.red;

    [Header("Set dynamically")]
    public bool IsBomb;
    public int NeiboringBombNumber {get; private set;}
    public static bool FLAG_MODE = false;

    [SerializeField] private List<Cell> neigboringCells;

    private eStates state = eStates.close;
    private Face parentFace;
    private Material mat;

    private List<Vector3> orthogonalVectors = new List<Vector3>
    {
        Vector3.back, Vector3.forward, Vector3.down, Vector3.up, Vector3.left, Vector3.right
    };

    public eStates GetState()
    {
        return state;
    }

    public Face GetParentFace()
    {
        return parentFace;
    }

    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;

        Close();
        if (Random.value < bombSpawnChance)
        {
            IsBomb = true;
            mat.color = Color.red;
        }

        transform.rotation = Quaternion.identity;
    }

    public void FindNeigbors(bool findOnlyParentPlane = false)
    {
        parentFace = transform.parent.GetComponent<Face>();
        float radius = 1f + Mathf.Max(parentFace.GetHorizontalOffset(), parentFace.GetVerticalOffset());
        Collider[] neigborColliders = Physics.OverlapSphere(transform.position, radius);

        Cell neigboringCell;
        foreach (Collider col in neigborColliders)
        {
            if (col.TryGetComponent(out neigboringCell) && col.transform.position != transform.position)
            {
                if (findOnlyParentPlane)
                {
                    if (parentFace.IsCellOnFace(neigboringCell) && !neigboringCells.Contains(neigboringCell))
                    {
                        neigboringCells.Add(neigboringCell);
                    }
                }
                else
                {
                    if (!neigboringCells.Contains(neigboringCell))
                        neigboringCells.Add(neigboringCell);
                }
            }
        }

        CountNeiboringBombs();
    }

    public void CountNeiboringBombs()
    {
        foreach (Cell cell in neigboringCells)
        {
            if (cell.IsBomb == true)
                NeiboringBombNumber++;
        }
    }

    public void SetFlag()
    {
        if (state != eStates.close) return;

        foreach (SpriteRenderer sR in flagRenderers)
        {
            sR.enabled = true;
        }
        state = eStates.flag;
    }

    public void UnSetFlag()
    {
        if (state != eStates.flag) return;

        foreach (SpriteRenderer sR in flagRenderers)
        {
            sR.enabled = false;
        }
        state = eStates.close;
    }

    public void Open()
    {
        if (state != eStates.close) return;

        if (IsBomb)
        {
            OpenBomb();
            return;
        }

        state = eStates.open;

        if (NeiboringBombNumber == 0)
        {
            foreach (Cell nC in neigboringCells)
            {

                if (nC.GetState() == eStates.close)
                {
                    nC.Open();
                }
            }
            mat.color = openColor;
        }
        else
        {
            ActivateNumbers(NeiboringBombNumber.ToString());
        }
    }

    private void OpenBomb()
    {
        mat.color = bombColor;
    }

    private void Close()
    {
        state = eStates.close;

        foreach (SpriteRenderer sR in flagRenderers)
        {
            sR.enabled = false;
        }

        foreach (TextMeshPro tMP in numbers)
        {
            tMP.enabled = false;
        }

        mat.color = closeColor;
    }

    private void ActivateNumbers(string text)
    {
        foreach (TextMeshPro tMP in numbers)
        {
            tMP.text = text;
            tMP.enabled = true;
        }
    }

    private void OnMouseUp()
    {
        if (!FLAG_MODE)
        {
            Open();
        }
        else
        {
            if (state == eStates.flag)
                UnSetFlag();
            else
                SetFlag();
        }

    }
}