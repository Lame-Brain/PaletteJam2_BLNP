using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PuzzleManager : MonoBehaviour
{
    [Header("NAME OF LEVEL")]
    public string LevelName;

    [Header("THESE HAVE TO BE ODD VALUES")]
    public int horizontal_BoardSize;
    public int vertical_BoardSize;

    [Header("    ")]
    public int Number_of_Holes;
    public int Number_of_LavaPools;


    //public float line_ratio, box_ratio, ell_ratio, bolt_ratio, tee_ratio;

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
    public int[,] TileGrid;


    private void Start()
    {
        GameManager.PUZZLE = this;
        Number_of_Waves = Waves_transform.childCount;
        current_Wave = 0;
        half_vert = Mathf.RoundToInt(vertical_BoardSize / 2);
        half_horz = Mathf.RoundToInt(horizontal_BoardSize / 2);

        TileGrid = new int[horizontal_BoardSize, vertical_BoardSize];
        for (int _y = 0; _y < vertical_BoardSize; _y++)
            for (int _x = 0; _x < horizontal_BoardSize; _x++)
            {
                TileGrid[_x, _y] = 0; //default TileGrid to empty floor tiles
            }
        for (int _i = 0; _i < Holes_transform.childCount; _i++) //place pits in TileGrid
            TileGrid[(int)Holes_transform.GetChild(_i).position.x + half_horz,
                     ((int)Holes_transform.GetChild(_i).position.y - half_vert) * -1] = 1;
        for (int _i = 0; _i < Pools_transform.childCount; _i++) //Place Lava in TileGrid (overwrites pits)
            TileGrid[(int)Pools_transform.GetChild(_i).position.x + half_horz,
                     ((int)Pools_transform.GetChild(_i).position.y - half_vert) * -1] = 2;

        //Resolve The grid
        GameObject _go = null;
        bool[] _adj = new bool[8]; 
        int mx = horizontal_BoardSize - 1, my = vertical_BoardSize - 1;
        for (int _y = 0; _y < vertical_BoardSize; _y++)
            for (int _x = 0; _x < horizontal_BoardSize; _x++)
            {
                if (TileGrid[_x, _y] == 1)
                {
                    for (int _i = 0; _i < 8; _i++) _adj[_i] = false;
                    if (_y > 0 && TileGrid[_x, _y - 1] == 1) _adj[0] = true;
                    if (_x < mx &&_y > 0   && TileGrid[_x + 1, _y - 1] == 1) _adj[1] = true;
                    if (_x < mx && TileGrid[_x + 1, _y] == 1) _adj[2] = true;
                    if (_x < mx && _y < my && TileGrid[_x + 1, _y + 1] == 1) _adj[3] = true;
                    if (_y < my && TileGrid[_x, _y + 1] == 1) _adj[4] = true;
                    if (_x > 0 && _y < my && TileGrid[_x-1, _y + 1] == 1) _adj[5] = true;
                    if (_x > 0 && TileGrid[_x-1, _y] == 1) _adj[6] = true;
                    if (_x > 0 && _y > 0 && TileGrid[_x - 1, _y - 1] == 1) _adj[7] = true;

                    _go = Solo_Chasm_PF;

                    if (_adj[0] == false && _adj[2] == true && _adj[3] == true && _adj[4] == true && _adj[6] == false) _go = UL_Chasm_PF;
                    if (_adj[0] == false && _adj[2] == true && _adj[3] == true && _adj[4] == true && _adj[5] == true && _adj[6] == true) _go = U_Chasm_PF;
                    if (_adj[0] == false && _adj[2] == false && _adj[4] == true && _adj[5] == true) _go = UR_Chasm_PF;
                    if (_adj[0] == true && _adj[2] == false && _adj[4] == true && _adj[5] == true && _adj[6] == true && _adj[7] == true) _go = R_Chasm_PF;
                    if (_adj[0] == true && _adj[2] == false && _adj[4] == false && _adj[6] == true && _adj[7] == true) _go = DR_Chasm_PF;
                    if (_adj[0] == true && _adj[1] == true && _adj[2] == true && _adj[4] == false && _adj[6] == true && _adj[7] == true) _go = D_Chasm_PF;
                    if (_adj[0] == true && _adj[1] == true && _adj[2] == true && _adj[4] == false && _adj[6] == false) _go = DL_Chasm_PF;
                    if (_adj[0] == true && _adj[1] == true && _adj[2] == true && _adj[3] == true && _adj[4] == true && _adj[6] == false) _go = L_Chasm_PF;
                    if (_adj[0] == true && _adj[1] == true && _adj[2] == true && _adj[3] == true && _adj[4] == true && _adj[5] == true && _adj[6] == true && _adj[7] == true) _go = C_Chasm_PF;
                    if (_adj[0] == true && _adj[1] == true && _adj[2] == true && _adj[3] == false && _adj[4] == true && _adj[5] == true && _adj[6] == true && _adj[7] == true) _go = DR_ChasmCorner_PF;
                    if (_adj[0] == true && _adj[1] == true && _adj[2] == true && _adj[3] == true && _adj[4] == true && _adj[5] == false && _adj[6] == true && _adj[7] == true) _go = DL_ChasmCorner_PF;
                    if (_adj[0] == true && _adj[1] == true && _adj[2] == true && _adj[3] == true && _adj[4] == true && _adj[5] == true && _adj[6] == true && _adj[7] == false) _go = UL_ChasmCorner_PF;
                    if (_adj[0] == true && _adj[1] == false && _adj[2] == true && _adj[3] == true && _adj[4] == true && _adj[5] == true && _adj[6] == true && _adj[7] == true) _go = UR_ChasmCorner_PF;

                    Instantiate(_go, new Vector2(_x - half_horz, (_y - half_vert) * -1), Quaternion.identity, Holes_transform);
                }

                if (TileGrid[_x, _y] == 2)
                {
                    for (int _i = 0; _i < 8; _i++) _adj[_i] = false;
                    if (_y > 0 && TileGrid[_x, _y - 1] == 2) _adj[0] = true;
                    if (_x < mx && _y > 0 && TileGrid[_x + 1, _y - 1] == 2) _adj[1] = true;
                    if (_x < mx && TileGrid[_x + 1, _y] == 2) _adj[2] = true;
                    if (_x < mx && _y < my && TileGrid[_x + 1, _y + 1] == 2) _adj[3] = true;
                    if (_y < my && TileGrid[_x, _y + 1] == 2) _adj[4] = true;
                    if (_x > 0 && _y < my && TileGrid[_x - 1, _y + 1] == 2) _adj[5] = true;
                    if (_x > 0 && TileGrid[_x - 1, _y] == 2) _adj[6] = true;
                    if (_x > 0 && _y > 0 && TileGrid[_x - 1, _y - 1] == 2) _adj[7] = true;

                    _go = Solo_Lava_PF;

                    if (_adj[0] == false && _adj[2] == true && _adj[3] == true && _adj[4] == true && _adj[6] == false) _go = UL_Lava_PF;
                    if (_adj[0] == false && _adj[2] == true && _adj[3] == true && _adj[4] == true && _adj[5] == true && _adj[6] == true) _go = U_Lava_PF;
                    if (_adj[0] == false && _adj[2] == false && _adj[4] == true && _adj[5] == true) _go = UR_Lava_PF;
                    if (_adj[0] == true && _adj[2] == false && _adj[4] == true && _adj[5] == true && _adj[6] == true && _adj[7] == true) _go = R_Lava_PF;
                    if (_adj[0] == true && _adj[2] == false && _adj[4] == false && _adj[6] == true && _adj[7] == true) _go = DR_Lava_PF;
                    if (_adj[0] == true && _adj[1] == true && _adj[2] == true && _adj[4] == false && _adj[6] == true && _adj[7] == true) _go = D_Lava_PF;
                    if (_adj[0] == true && _adj[1] == true && _adj[2] == true && _adj[4] == false && _adj[6] == false) _go = DL_Lava_PF;
                    if (_adj[0] == true && _adj[1] == true && _adj[2] == true && _adj[3] == true && _adj[4] == true && _adj[6] == false) _go = L_Lava_PF;
                    if (_adj[0] == true && _adj[1] == true && _adj[2] == true && _adj[3] == false && _adj[4] == true && _adj[5] == true && _adj[6] == true && _adj[7] == true) _go = DR_LavaCorner_PF;
                    if (_adj[0] == true && _adj[1] == true && _adj[2] == true && _adj[3] == true && _adj[4] == true && _adj[5] == false && _adj[6] == true && _adj[7] == true) _go = DL_LavaCorner_PF;
                    if (_adj[0] == true && _adj[1] == true && _adj[2] == true && _adj[3] == true && _adj[4] == true && _adj[5] == true && _adj[6] == true && _adj[7] == false) _go = UL_LavaCorner_PF;
                    if (_adj[0] == true && _adj[1] == false && _adj[2] == true && _adj[3] == true && _adj[4] == true && _adj[5] == true && _adj[6] == true && _adj[7] == true) _go = UR_LavaCorner_PF;

                    int _r = Random.Range(0, 3);
                    if (_adj[0] == true && _adj[1] == true && _adj[2] == true && _adj[3] == true && _adj[4] == true && _adj[5] == true && _adj[6] == true && _adj[7] == true && _r == 0) _go = C1_Lava_PF;
                    if (_adj[0] == true && _adj[1] == true && _adj[2] == true && _adj[3] == true && _adj[4] == true && _adj[5] == true && _adj[6] == true && _adj[7] == true && _r == 1) _go = C2_Lava_PF;
                    if (_adj[0] == true && _adj[1] == true && _adj[2] == true && _adj[3] == true && _adj[4] == true && _adj[5] == true && _adj[6] == true && _adj[7] == true && _r == 2) _go = C3_Lava_PF;

                    Instantiate(_go, new Vector2(_x - half_horz, (_y - half_vert) * -1), Quaternion.identity, Pools_transform);
                }
            }

        foreach (GameObject _gob in GameObject.FindGameObjectsWithTag("HoleSymbol")) DestroyImmediate(_gob);
        foreach (GameObject _gob in GameObject.FindGameObjectsWithTag("PoolSymbol")) DestroyImmediate(_gob);
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
