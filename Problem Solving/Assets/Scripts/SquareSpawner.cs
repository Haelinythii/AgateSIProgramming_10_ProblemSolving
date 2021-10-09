using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSpawner : Spawner
{
    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private int maxSquareCount = 5;

    [SerializeField] private Vector2 minSquareScale;
    [SerializeField] private Vector2 maxSquareScale;

    public bool SquareCanRespawn;

    protected override void Start()
    {
        base.Start();

        SpawnSquares();
    }

    private void SpawnSquares()
    {
        //randomize spawned square count
        float squareSpawnCount = Random.Range(1, maxSquareCount + 1);
        for (int i = 0; i < squareSpawnCount; i++)
        {
            //create new square
            GameObject newSquare = Instantiate(squarePrefab, squarePrefab.transform.position, Quaternion.identity);
            newSquare.transform.localScale = new Vector3(Random.Range(minSquareScale.x, maxSquareScale.x), Random.Range(minSquareScale.y, maxSquareScale.y), 1f);

            //set action delegate function setelah square dicollect
            if (SquareCanRespawn)
            {
                SquareController squareController = newSquare.GetComponent<SquareController>();
                squareController.OnSquareDestroyed += RespawnSquare;
            }

            //randomize its position
            SetRandomSquarePosition(newSquare);
        }
    }

    public void RespawnSquare(SquareController square)
    {
        StartCoroutine(RespawnSquareAfterSeconds(square));
    }

    private IEnumerator RespawnSquareAfterSeconds(SquareController square)
    {
        yield return new WaitForSeconds(3f);
        square.gameObject.SetActive(true);
        SetRandomSquarePosition(square.gameObject);
    }

    private void SetRandomSquarePosition(GameObject squareGO)
    {
        squareGO.transform.position = GetRandomSpawnPosition();
    }
}
