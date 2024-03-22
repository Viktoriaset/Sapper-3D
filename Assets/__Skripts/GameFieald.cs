using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameFieald : MonoBehaviour
{
    #region Properties
    [Header("Set in inspector")]
    [SerializeField] private GameObject facePrefab;
    public List<Transform> tFaces;
    private List<Face> faces = new List<Face>();
    [Inject] private DiContainer diContainer;
    #endregion

    #region Methods

    private void CreateGameField()
    {
        foreach (Transform tF in tFaces)
        {
            facePrefab.transform.position = tF.position;
            GameObject tFP = diContainer.InstantiatePrefab(facePrefab);
            tFP.transform.localScale = tF.localScale;
            tFP.transform.localRotation = tF.localRotation;

            tFP.transform.SetParent(transform, false);
            tFP.GetComponent<Face>().CreateCells();
            faces.Add(tFP.GetComponent<Face>());
        }
    }

    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        CreateGameField();
    }
    #endregion

    
}
