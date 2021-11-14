using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

    [HideInInspector]
    public int Number_of_Waves, current_Wave, half_vert, half_horz;
    

    private void Start()
    {
        GameManager.PUZZLE = this;
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
