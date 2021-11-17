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

    [Header("---<<< Ignore Below This Line >>>---")]
    public List<GameObject> floortiles = new List<GameObject>();
    public GameObject Holes, UL_Chasm_PF, U_Chasm_PF, UR_Chasm_PF, L_Chasm_PF, C_Chasm_PF, R_Chasm_PF, DL_Chasm_PF, D_Chasm_PF, DR_Chasm_PF, UL_ChasmCorner_PF, UR_ChasmCorner_PF, DL_ChasmCorner_PF, DR_ChasmCorner_PF, Solo_Chasm_PF;
    public GameObject Pools, UL_Lava_PF, U_Lava_PF, UR_Lava_PF, L_Lava_PF, C1_Lava_PF, C2_Lava_PF, C3_Lava_PF, R_Lava_PF, DL_Lava_PF, D_Lava_PF, DR_Lava_PF, UL_LavaCorner_PF, UR_LavaCorner_PF, DL_LavaCorner_PF, DR_LavaCorner_PF, Solo_Lava_PF;
    public GameObject[,] floor_map;
    public GameObject UL_Wall_PF, U_Wall_PF, UR_Wall_PF, L_Wall_PF, R_Wall_PF, DL_Wall_PF, D_Wall_PF, DR_Wall_PF;
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
