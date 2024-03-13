using System.Collections.Generic;
using UnityEngine;

public class Face : MonoBehaviour
{
    static private Dictionary<Vector3, Cell> employedBoundaryPositions = new Dictionary<Vector3, Cell>();

    [Header("Set in inspector")]
    [SerializeField]
    private GameObject cellPrefab;
    [SerializeField]
    private int cellInWidth = 8;
    [SerializeField]
    private int cellInHeight = 8;
    [SerializeField]
    private float horizontalOffset;
    [SerializeField]
    private float verticalOffset;

    private List<List<Cell>> cells = new List<List<Cell>>();

    static private Cell GetCellByPos(Vector3 position)
    {
        foreach(Vector3 tV in employedBoundaryPositions.Keys)
        {
            if (tV == position)
                return employedBoundaryPositions[tV];
        }

        return null;
    }

    public float GetHorizontalOffset()
    {
        return horizontalOffset;
    }

    public float GetVerticalOffset()
    {
        return verticalOffset;
    }

    public void CreateCells()
    {
        float width = cellInWidth * cellPrefab.transform.localScale.x + (cellInWidth - 1) * horizontalOffset;
        float height = cellInHeight * cellPrefab.transform.localScale.z + (cellInHeight - 1) * verticalOffset;
        float hStartPos = -(width / 2) + cellPrefab.transform.localScale.x / 2;
        float vStartPos = -(height / 2) + cellPrefab.transform.localScale.z / 2;

        for (int i = 0; i < cellInHeight; i++)
        {
            cells.Add(new List<Cell>());
            float vPos = vStartPos + cellPrefab.transform.localScale.y * i + i * verticalOffset;
            for (int j = 0; j < cellInWidth; j++)
            {
                float hPos = hStartPos + cellPrefab.transform.localScale.x * j + j * horizontalOffset;
                Vector3 localPos = new Vector3(hPos, 0, vPos);
                
                if (i == 0 || j == 0 || i == cellInHeight - 1 || j == cellInWidth - 1)
                {
                    Vector3 tPos = transform.TransformPoint(localPos);
                    Cell existCell = GetCellByPos(tPos);
                    if (existCell != null)
                    {
                        cells[i].Add(existCell);
                    } else
                    {
                        employedBoundaryPositions[tPos] = CreateCell(localPos);
                    }
                } else
                {
                    CreateCell(localPos);
                }
            }
        }
    }

    private Cell CreateCell(Vector3 localPos)
    {
        GameObject cellGo = Instantiate(cellPrefab);
        cellGo.name = (transform.TransformPoint(localPos)).ToString();
        cellGo.transform.SetParent(transform, false);
        cellGo.transform.localPosition = localPos;
        cells[cells.Count - 1].Add(cellGo.GetComponent<Cell>());

        return cellGo.GetComponent<Cell>();
    }

    public bool IsCellOnFace(Cell cell)
    {
        foreach(List<Cell> cL in cells)
        {
            if (cL.Contains(cell)) return true;
        }

        return false;
    }

    public void StartFindNeiborings()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            for (int j = 0; j < cells[i].Count; j++)
            {
                if (i > 0 && j > 0 && i < cells.Count - 1 && j < cells[i].Count - 1)
                {
                    cells[i][j].FindNeigbors(true);
                }
                else
                {
                    cells[i][j].FindNeigbors();
                }
            }
        }
    }
}
