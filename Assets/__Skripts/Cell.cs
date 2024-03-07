using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public List<Cell> neigboringCells;

    private Face parentFace;

    private void Start()
    {
        parentFace = transform.parent.GetComponent<Face>();

        FindNeigbors();
    }

    private void FindNeigbors()
    {
        float radius = 1 + Mathf.Max(parentFace.GetHorizontalOffset(), parentFace.GetVerticalOffset());
        Collider[] neigborColliders = Physics.OverlapSphere(transform.position, radius);

        Cell neigboringCell;
        foreach (Collider col in neigborColliders)
        {
            if (col.TryGetComponent(out neigboringCell) && col.transform.position != transform.position)
            {
                neigboringCells.Add(neigboringCell);
            }
        }
    }


}
