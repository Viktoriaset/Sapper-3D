using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameFieald : MonoBehaviour
{
    [Header("Set in inspector")]
    public List<Transform> tFaces;
    private List<Face> faces = new List<Face>();
    private IFaceFactory _faceFactory;

    [Inject]
    private void Constructor(IFaceFactory faceFactory)
    {
        _faceFactory = faceFactory;
    }

    private void Awake()
    {
        CreateGameField();
    }

    private void CreateGameField()
    {
        _faceFactory.Load();
        foreach (Transform tF in tFaces)
        {
            GameObject tFP = _faceFactory.Create(tF, transform);
            tFP.transform.localPosition = tF.position;
            tFP.transform.localScale = tF.localScale;
            tFP.transform.localRotation = tF.localRotation;

            tFP.GetComponent<Face>().FillCells();
            faces.Add(tFP.GetComponent<Face>());
        }
    }
}
