using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PuzzleManager))]
public class PuzzleMaker : Editor
{
    private List<Vector2> ReservedPos = new List<Vector2>();

    public override void OnInspectorGUI()
    {
        PuzzleManager puzzle = (PuzzleManager)target;
        
        if (GUILayout.Button("Generate Play Area"))
        {
            foreach (GameObject _go in GameObject.FindGameObjectsWithTag("Terrain")) DestroyImmediate(_go);
            foreach (GameObject _go in GameObject.FindGameObjectsWithTag("Hole")) DestroyImmediate(_go);
            foreach (GameObject _go in GameObject.FindGameObjectsWithTag("Pool")) DestroyImmediate(_go);
            foreach (GameObject _go in GameObject.FindGameObjectsWithTag("HoleSymbol")) DestroyImmediate(_go);
            foreach (GameObject _go in GameObject.FindGameObjectsWithTag("PoolSymbol")) DestroyImmediate(_go);

            puzzle.half_vert = Mathf.RoundToInt((puzzle.vertical_BoardSize / 2));
            puzzle.half_horz = Mathf.RoundToInt((puzzle.horizontal_BoardSize / 2));

            puzzle.floor_map = new GameObject[puzzle.horizontal_BoardSize, puzzle.vertical_BoardSize];
            for (int _y = 0; _y < puzzle.vertical_BoardSize; _y++)
                for (int _x = 0; _x < puzzle.horizontal_BoardSize; _x++)
                {
                    Quaternion _angle = Quaternion.identity;
                    int _r = Random.Range(0, 4);
                    if (_r == 0) _angle.eulerAngles = new Vector3(0, 0, 0);
                    if (_r == 1) _angle.eulerAngles = new Vector3(0, 0, 90);
                    if (_r == 2) _angle.eulerAngles = new Vector3(0, 0, 180);
                    if (_r == 3) _angle.eulerAngles = new Vector3(0, 0, 270);
                    puzzle.floor_map[_x, _y] = Instantiate(puzzle.floortiles[Random.Range(0, puzzle.floortiles.Count)],
                                                    new Vector3(_x - (puzzle.horizontal_BoardSize / 2), _y - (puzzle.vertical_BoardSize / 2), 0),
                                                    _angle, puzzle.Floors_transform);
                    puzzle.floor_map[_x, _y].name = "floor_tile_" + _x + "_" + _y;
                }

            for (int _y = 0; _y < puzzle.vertical_BoardSize; _y++)
            {
                Instantiate(puzzle.L_Wall_PF, new Vector3((puzzle.half_horz * -1) - 1, _y - puzzle.half_vert, 0), Quaternion.identity, puzzle.Walls_transform);
                Instantiate(puzzle.R_Wall_PF, new Vector3(puzzle.half_horz + 1, _y - puzzle.half_vert, 0), Quaternion.identity, puzzle.Walls_transform);
            }
            for (int _x = 0; _x < puzzle.horizontal_BoardSize; _x++)
            {
                Instantiate(puzzle.U_Wall_PF, new Vector3(_x - puzzle.half_horz, (puzzle.half_vert * -1) - 1, 0), Quaternion.identity, puzzle.Walls_transform);
                Instantiate(puzzle.D_Wall_PF, new Vector3(_x - puzzle.half_horz, puzzle.half_vert + 1, 0), Quaternion.identity, puzzle.Walls_transform);
            }
            Instantiate(puzzle.UL_Wall_PF, new Vector3((puzzle.half_horz * -1) - 1, puzzle.half_vert + 1, 0), Quaternion.identity, puzzle.Walls_transform);
            Instantiate(puzzle.UR_Wall_PF, new Vector3((puzzle.half_horz + 1), puzzle.half_vert + 1, 0), Quaternion.identity, puzzle.Walls_transform);
            Instantiate(puzzle.DL_Wall_PF, new Vector3((puzzle.half_horz * -1) - 1, (puzzle.half_vert * -1) - 1, 0), Quaternion.identity, puzzle.Walls_transform);
            Instantiate(puzzle.DR_Wall_PF, new Vector3((puzzle.half_horz + 1),      (puzzle.half_vert * -1) - 1, 0), Quaternion.identity, puzzle.Walls_transform);

            //            puzzle.Number_of_Waves = puzzle.Waves_transform.childCount;
            //            puzzle.current_Wave = 1;
            ReservedPos.Clear();
            ReservedPos.Add(new Vector2(0, 0));
            if (puzzle.Number_of_Holes > 0)
                for (int _i = 0; _i < puzzle.Number_of_Holes; _i++)
                {
                    Instantiate(puzzle.Holes,
                              FindValidPos(puzzle.half_horz, puzzle.half_vert),
                              Quaternion.identity, puzzle.Holes_transform);                    
                }
//                    Instantiate(puzzle.Holes,
//                                new Vector3(Random.Range(-puzzle.half_horz, puzzle.half_horz), Random.Range(-puzzle.half_vert, puzzle.half_vert), 0),
//                                Quaternion.identity, puzzle.Holes_transform);

            if (puzzle.Number_of_LavaPools > 0)
                for (int _i = 0; _i < puzzle.Number_of_LavaPools; _i++)
                {
                    Instantiate(puzzle.Pools,
                              FindValidPos(puzzle.half_horz, puzzle.half_vert),
                              Quaternion.identity, puzzle.Pools_transform);
                }
        }

        if (GUILayout.Button("Clear Play Area"))
        {
            foreach (GameObject _go in GameObject.FindGameObjectsWithTag("Terrain")) DestroyImmediate(_go);
            foreach (GameObject _go in GameObject.FindGameObjectsWithTag("Hole")) DestroyImmediate(_go);
            foreach (GameObject _go in GameObject.FindGameObjectsWithTag("Pool")) DestroyImmediate(_go);
            foreach (GameObject _go in GameObject.FindGameObjectsWithTag("HoleSymbol")) DestroyImmediate(_go);
            foreach (GameObject _go in GameObject.FindGameObjectsWithTag("PoolSymbol")) DestroyImmediate(_go);
        }

        base.OnInspectorGUI();

    }

    private Vector3 FindValidPos(int bx, int by)
    {
        bool _done = false;
        Vector3 canidatePos = Vector3.up;
        while (!_done)
        {
            canidatePos = new Vector3(Random.Range(-bx, bx), Random.Range(-by, by), 0);
            if (!ReservedPos.Contains(canidatePos))
            {
                _done = true;
                ReservedPos.Add(canidatePos);
            }
        }
        return canidatePos;
    }
}
