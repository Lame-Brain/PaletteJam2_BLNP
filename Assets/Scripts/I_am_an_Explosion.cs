using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_am_an_Explosion : MonoBehaviour
{
    public int radius;
    public GameObject centerPF, horzPF, vertPF;

    private void Start()
    {
        float _x = transform.position.x;
        float _y = transform.position.y;
        Instantiate(centerPF, new Vector2(_x, _y), Quaternion.identity, transform);

        Collider2D[] col_up = new Collider2D[5],
            col_right = new Collider2D[5],
            col_down = new Collider2D[5],
            col_left = new Collider2D[5];
        bool _goUp = true, _goRight = true, _goDown = true, _goLeft = true;
        for(int _i = 1; _i < radius; _i++)
        {
            if (_goUp) Instantiate(vertPF, new Vector2(_x, _y - _i), Quaternion.identity, transform);
            if (_goRight) Instantiate(horzPF, new Vector2(_x + _i, _y), Quaternion.identity, transform);
            if (_goDown) Instantiate(vertPF, new Vector2(_x, _y + _i), Quaternion.identity, transform);
            if (_goLeft) Instantiate(horzPF, new Vector2(_x - _i, _y), Quaternion.identity, transform);
            
            if(_goUp) col_up = Physics2D.OverlapCircleAll(new Vector2(_x, _y - _i), 0.0f);
            if (_goRight) col_right = Physics2D.OverlapCircleAll(new Vector2(_x + _i, _y), 0.0f);
            if (_goDown) col_down = Physics2D.OverlapCircleAll(new Vector2(_x, _y + _i), 0.0f);
            if (_goLeft) col_left = Physics2D.OverlapCircleAll(new Vector2(_x - _i, _y), 0.0f);

            for (int _c = 0; _c < col_up.Length; _c++) if (col_up[_c].CompareTag("Block"))
                {
                    _goUp = false;
                    col_up[_c].GetComponent<I_am_an_Object>().BlastMe();
                }
            for (int _c = 0; _c < col_right.Length; _c++) if (col_right[_c].CompareTag("Block"))
                {
                    _goRight = false;
                    col_right[_c].GetComponent<I_am_an_Object>().BlastMe();
                }
            for (int _c = 0; _c < col_down.Length; _c++) if (col_down[_c].CompareTag("Block"))
                {
                    _goDown = false;
                    col_down[_c].GetComponent<I_am_an_Object>().BlastMe();
                }
            for (int _c = 0; _c < col_left.Length; _c++) if (col_left[_c].CompareTag("Block"))
                {
                    _goLeft = false;
                    col_left[_c].GetComponent<I_am_an_Object>().BlastMe();
                }
        }
    }
}
