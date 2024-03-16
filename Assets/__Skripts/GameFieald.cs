using System.Collections.Generic;
using UnityEngine;

public class GameFieald : MonoBehaviour
{
    [Header("Set in inspector")]
    [SerializeField] private GameObject facePrefab;
    public List<Transform> tFaces;
    private List<Face> faces = new List<Face>();

    private int lateUpdateCounter = 0;

    private void Awake()
    {
        CreateGameField();
    }

    private void CreateGameField()
    {
        foreach (Transform tF in tFaces)
        {
            facePrefab.transform.position = tF.position;
            GameObject tFP = Instantiate(facePrefab);
            tFP.transform.localScale = tF.localScale;
            tFP.transform.localRotation = tF.localRotation;

            tFP.transform.SetParent(transform, false);
            tFP.GetComponent<Face>().CreateCells();
            faces.Add(tFP.GetComponent<Face>());
        }

        
    }

    private void LateUpdate() 
    {
        if (lateUpdateCounter == 0)
        {
            foreach (Face f in faces)
                f.StartFindNeiborings();    
        }
        lateUpdateCounter++;
    }
}
