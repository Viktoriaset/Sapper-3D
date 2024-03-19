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

    #region Properties

    [Header("Set in inspector")]
    [SerializeField] private List<TextMeshPro> numbers;
    [SerializeField] private List<SpriteRenderer> flagRenderers;
    [SerializeField] private float bombSpawnChance = 0.05f;
    [SerializeField] private Color closeColor = Color.gray;
    [SerializeField] private Color openColor = Color.green;
    [SerializeField] private Color bombColor = Color.red;

    [Header("Set dynamically")]
    public bool IsBomb;
    public int NeiboringBombNumber; //{get; private set;}
    public static bool FLAG_MODE = false;

    [SerializeField] private List<Cell> neighboringCells;

    public eStates state {get; private set;} = eStates.close;
    private Face parentFace;
    private Material mat;
    private Animator animator;
    private static int bombCount = 0;

    private List<Vector3> orthogonalVectors = new List<Vector3>
    {
        Vector3.back, Vector3.forward, Vector3.down, Vector3.up, Vector3.left, Vector3.right
    };

    #endregion

    #region Methods

    public eStates GetState()
    {
        return state;
    }

    public Face GetParentFace()
    {
        return parentFace;
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
                    if (parentFace.IsCellOnFace(neigboringCell) && !neighboringCells.Contains(neigboringCell))
                    {
                        neighboringCells.Add(neigboringCell);
                    }
                }
                else
                {
                    if (!neighboringCells.Contains(neigboringCell))
                        neighboringCells.Add(neigboringCell);
                }
            }
        }
    }

    public void CountNeiboringBombs()
    {
        NeiboringBombNumber = 0;
        foreach (Cell cell in neighboringCells)
        {
            if (cell.IsBomb == true)
                NeiboringBombNumber++;
        }
    }

    private void SetFlag()
    {
        if (state != eStates.close) return;

        foreach (SpriteRenderer sR in flagRenderers)
        {
            sR.enabled = true;
        }
        state = eStates.flag;
    }

    private void UnSetFlag()
    {
        if (state != eStates.flag) return;

        foreach (SpriteRenderer sR in flagRenderers)
        {
            sR.enabled = false;
        }
        state = eStates.close;
    }

    public void ChangeFlag()
    {
        if (state == eStates.flag)
        {
            UnSetFlag();
        } 
        else 
        {
            SetFlag();
        }
    }

    public void Open()
    {
        if (state != eStates.close) return;
        state = eStates.open;

        if (IsBomb)
        {
            OpenBomb();
            return;
        }

        CountNeiboringBombs();
        
        if (NeiboringBombNumber == 0)
        {
            foreach (Cell nC in neighboringCells)
            {

                if (nC.GetState() == eStates.close)
                {
                    nC.Opening();
                }
            }
            mat.color = openColor;
        }
        else
        {
            ActivateNumbers(NeiboringBombNumber.ToString());
        }
    }

    public void Opening()
    {
        if (state != eStates.close) return;
        animator.SetBool("IsOpening", true);
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

    public void DiactivateBombAndOpen(int maxDepth, int depth)
    {
        if (depth > maxDepth) return;

        IsBomb = false;

        foreach(Cell cell in neighboringCells)
        {
            cell.DiactivateBombAndOpen(maxDepth, depth +  1);
        }

        if (depth == 1) Opening();
    }

    #endregion

    #region MonoBehaviour Methods
    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        animator = GetComponent<Animator>();

        Close();
        if (Random.value < bombSpawnChance && bombCount < 80)
        {
            IsBomb = true;
            bombCount++;
        }

        transform.rotation = Quaternion.identity;
    }

    #endregion
}