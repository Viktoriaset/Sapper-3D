using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ExtensionMethods;

public class Cell : MonoBehaviour
{
    public enum eStates
    {
        flag,
        close,
        open
    }

    [Header("Set in inspector")]
    [SerializeField]
    private List<TextMeshPro> numbers;
    [SerializeField]
    private List<SpriteRenderer> flagRenderers;
    [SerializeField]
    private float bombSpawnChance = 0.3f;
    [SerializeField]
    private Color closeColor = Color.gray;
    private Color openColor = Color.green;
    private Color bombColor = Color.red;

    [Header("Set dynamically")]
    public bool IsBomb;

    [SerializeField]
    private List<Cell> neigboringCells;
    [SerializeField]
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

    private void Start()
    {
        parentFace = transform.parent.GetComponent<Face>();
        mat = GetComponent<MeshRenderer>().material;

        Close();
        if (Random.value < bombSpawnChance)
            IsBomb = true;
        FindNeigbors();
        transform.rotation = Quaternion.identity;
    }

    private void FindNeigbors()
    {
        int matchingByX = 0;
        int matchingByY = 0;
        int matchingByZ = 0;
        float radius = 1f + Mathf.Max(parentFace.GetHorizontalOffset(), parentFace.GetVerticalOffset());
        Collider[] neigborColliders = Physics.OverlapSphere(transform.position, radius);

        Cell neigboringCell;
        foreach (Collider col in neigborColliders)
        {
            if (col.TryGetComponent(out neigboringCell) && col.transform.position != transform.position)
            {
                Vector3 pos = neigboringCell.transform.position;
                if (pos.x == transform.position.x) matchingByX++;
                if (pos.y == transform.position.y) matchingByY++;
                if (pos.z == transform.position.z) matchingByZ++;
                neigboringCells.Add(neigboringCell);
            }
        }

        // Select cells that cannot contain neighbors from different planes.
        if (!(matchingByX == matchingByY || matchingByY == matchingByZ || matchingByX == matchingByZ))
        {
            // Delete cells that are not in the same plane with the rest of their neighbors and this cell. 
            int maxMathing = Mathf.Max(matchingByX, matchingByY, matchingByZ);
            if (maxMathing == matchingByX) RemoveCellInOutAxis('x');
            if (maxMathing == matchingByY) RemoveCellInOutAxis('y');
            if (maxMathing == matchingByZ) RemoveCellInOutAxis('z');
        }
    }

    private void RemoveCellInOutAxis(char axis)
    {
        List<int> cellsForRemoving = new List<int>();
        for(int i = 0; i < neigboringCells.Count; i++)
        {
            Cell cell = neigboringCells[i];
            if (cell.transform.position.GetVectorValueByChar(axis) != transform.position.GetVectorValueByChar(axis))
            {
                cellsForRemoving.Add(i);
            }
        }

        for (int i = cellsForRemoving.Count - 1; i > -1; i--)
        {
            neigboringCells.RemoveAt(cellsForRemoving[i]);
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

        foreach(SpriteRenderer sR in flagRenderers)
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

        int countNearingBomb = 0;
        foreach(Cell nC in neigboringCells)
        {
            if (nC.IsBomb) countNearingBomb++;
        }

        if (countNearingBomb == 0)
        {
            ActivateNumbers("");

            foreach (Cell nC in neigboringCells)
            {
                if (nC.GetState() == eStates.close && CheckOrthogonality(transform.position, nC.transform.position))
                    nC.Open();
            }

            mat.color = openColor;

        } else
        {
            ActivateNumbers(countNearingBomb.ToString());
        }
    }

    private void OpenBomb()
    {
        mat.color = Color.red;
    }

    private void Close()
    {
        state = eStates.close;

        foreach(SpriteRenderer sR in flagRenderers)
        {
            sR.enabled = false;
        }
        
        foreach(TextMeshPro tMP in numbers)
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

    private bool CheckOrthogonality(Vector3 v1, Vector3 v2)
    {
        Vector3 direction = (v1 - v2).normalized;

        foreach (Vector3 oV in orthogonalVectors)
        {
            if (direction == oV) return true;
        }

        return false;

    }

    private void OnMouseUp()
    {
        Open();
    }
}
