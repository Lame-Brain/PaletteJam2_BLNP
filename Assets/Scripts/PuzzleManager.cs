using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    public List<GameObject> floortiles = new List<GameObject>();
    public GameObject[,] floor_map;

    private void Start()
    {
        GameManager.PUZZLE = this;
        floor_map = new GameObject[64, 64];
        for (int _y = 0; _y < 64; _y++)
            for (int _x = 0; _x < 64; _x++)
                floor_map[_x, _y] = Instantiate(floortiles[Random.Range(0, floortiles.Count)],
                                                new Vector3(_x - 32, _y - 32, 0),
                                                Quaternion.identity);
    }
}
