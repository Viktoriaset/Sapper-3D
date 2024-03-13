using System.Collections.Generic;
using UnityEngine;

public class GameFieald : MonoBehaviour
{
    [Header("Set in inspector")]
    [SerializeField]
    private GameObject facePrefab;

    public List<Transform> tFaces;

    private List<Face> faces = new List<Face>();

    private void Awake()
    {
        CreateGameField();
        print("Don't correct negbor: " + Vector3.Distance(new Vector3(95.80f, -0.43f, -3f), new Vector3(97f, -1.63f, -3f)));
        print("Correct neigbor: " + Vector3.Distance(new Vector3(95.80f, -0.43f, -3f), new Vector3(95.8f, 0.77f, -1.8f)))   ;
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

    private void Start()
    {
        Invoke("test", 5f);
    }

    private void test()
    {
        foreach (Face f in faces)
            f.StartFindNeiborings();
    }
}
