using System.Collections.Generic;
using UnityEngine;

public class GameFieald : MonoBehaviour
{
    [Header("Set in inspector")]
    [SerializeField]
    private GameObject facePrefab;

    public List<Transform> faces;

    [SerializeField]
    private float createFaceDelay = 0.2f;

    private void Awake()
    {
        CreateGameField();
    }

    private void CreateGameField()
    {
        foreach (Transform tF in faces)
        {
            facePrefab.transform.position = tF.position;
            GameObject tFP = Instantiate(facePrefab);
            tFP.transform.localScale = tF.localScale;
            tFP.transform.rotation = tF.rotation;

            tFP.transform.SetParent(transform, false);
        }

    }
}
