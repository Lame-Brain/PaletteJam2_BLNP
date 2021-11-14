using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("THESE HAVE TO BE ODD VALUES")]
    public int horizontal_BoardSize;
    public int vertical_BoardSize;

    [Header("    ")]
    public int Number_of_Holes; 
    public int Number_of_LavaPools;
    public float line_ratio, box_ratio, ell_ratio, bolt_ratio, tee_ratio;
    
    [Header("    ")]
    public List<GameObject> floortiles = new List<GameObject>();
    public List<GameObject> Walls = new List<GameObject>();
    public List<GameObject> Holes = new List<GameObject>();
    public List<GameObject> Pools = new List<GameObject>();
    public GameObject[,] floor_map;

    [Header("    ")]
    public Transform Floors_transform;
    public Transform Walls_transform; 
    public Transform Waves_transform;
    public Transform Holes_transform;
    public Transform Pools_transform;

    private int Number_of_Waves, current_Wave, half_vert, half_horz;


    private void Start()
    {
        half_vert = Mathf.RoundToInt((vertical_BoardSize / 2));
        half_horz = Mathf.RoundToInt((horizontal_BoardSize / 2));
        GameManager.PUZZLE = this;
        floor_map = new GameObject[horizontal_BoardSize, vertical_BoardSize];
        for (int _y = 0; _y < vertical_BoardSize; _y++)
            for (int _x = 0; _x < horizontal_BoardSize; _x++)
            {
                Quaternion _angle = Quaternion.identity;
                int _r = Random.Range(0, 4);
                if (_r == 0) _angle.eulerAngles = new Vector3(0, 0, 0);
                if (_r == 1) _angle.eulerAngles = new Vector3(0, 0, 90);
                if (_r == 2) _angle.eulerAngles = new Vector3(0, 0, 180);
                if (_r == 3) _angle.eulerAngles = new Vector3(0, 0, 270);
                floor_map[_x, _y] = Instantiate(floortiles[Random.Range(0, floortiles.Count)],
                                                new Vector3(_x - (horizontal_BoardSize / 2), _y - (vertical_BoardSize / 2), 0),
                                                _angle, Floors_transform);
                floor_map[_x, _y].name = "floor_tile_" + _x + "_" + _y;
            }

        for(int _y = -1; _y < vertical_BoardSize + 1; _y++)
        {
            Instantiate(Walls[Random.Range(0, Walls.Count)], new Vector3((half_horz * -1) - 1, _y - half_vert, 0), Quaternion.identity, Walls_transform);
            Instantiate(Walls[Random.Range(0, Walls.Count)], new Vector3(half_horz + 1, _y - half_vert, 0), Quaternion.identity, Walls_transform);
        }
        for (int _x = 0; _x < horizontal_BoardSize; _x++)
        {
            Instantiate(Walls[Random.Range(0, Walls.Count)], new Vector3(_x - half_horz, (half_vert * -1) - 1, 0), Quaternion.identity, Walls_transform);
            Instantiate(Walls[Random.Range(0, Walls.Count)], new Vector3(_x - half_horz, half_vert + 1, 0), Quaternion.identity, Walls_transform);
        }

        Number_of_Waves = Waves_transform.childCount;
        current_Wave = 1;

        if (Number_of_Holes > 0)
            for (int _i = 0; _i < Number_of_Holes; _i++)
                Instantiate(Holes[Random.Range(0, Holes.Count)],
                            new Vector3(Random.Range(-half_horz, half_horz), Random.Range(-half_vert, half_vert), 0),
                            Quaternion.identity, Holes_transform);

        if (Number_of_LavaPools > 0)
            for (int _i = 0; _i < Number_of_LavaPools; _i++)
                Instantiate(Pools[Random.Range(0, Pools.Count)],
                            new Vector3(Random.Range(-half_horz, half_horz), Random.Range(-half_vert, half_vert), 0),
                            Quaternion.identity, Pools_transform);

    }

    private void SpawnWave()
    {
        if(current_Wave < Number_of_Waves)
        {
            current_Wave++;
            //generate rubble drops. Rotate them randomly
            //generate bomb drops.
        }
        else
        {
            //Finish level
        }
    }
}
