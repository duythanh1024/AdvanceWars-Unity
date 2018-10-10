using System;
using UnityEngine;
public class LayerIM : MonoBehaviour {
    DisplayIM[,] display;
    public float[,] red;
    public float[,] blue;
    public float[,] terrain;
    public float[,] value;
    void Start()
    {
        //Create();
        //New();
        //print(Mathf.InverseLerp(0, 15, 12.5f));
    }
    public void MyAwake()
    {
        red = new float[15, 10];
        blue = new float[15, 10];
        terrain = new float[15, 10];
        value = new float[15, 10];
        display = new DisplayIM[15, 10];
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                display[r, c] = Instantiate(Resources.Load("DisplayIM") as GameObject, transform).GetComponent<DisplayIM>();
                display[r, c].transform.position = new Vector2(r, c);                
            }
        }
        gameObject.SetActive(false);
    }
    public void ShowInluenceMap()
    {
        
    }
    private void InverLerp()//-1 to 1
    {

    }
    public void Print()
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                print(value[r, c]);
            }
        }
    }
    public void Normalize()//chuyen ve 0->1
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                red[r, c] = Mathf.InverseLerp(0, 90, Mathf.Abs(red[r, c])); //print(red[r, c]);
                blue[r, c] = Mathf.InverseLerp(0, 90, Mathf.Abs(blue[r, c])); //print(blue[r, c]);
                terrain[r, c] = Mathf.InverseLerp(0, 4, Mathf.Abs(terrain[r, c])); //print(terrain[r, c]);
                value[r, c] = Mathf.InverseLerp(0, 3, (red[r, c] + terrain[r, c] + blue[r, c])); //print("value " + (red[r, c] + terrain[r, c] + blue[r, c]));
            }
        }
    }
    public void CombineRedBlueTerr()
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                value[r, c] = red[r, c] + terrain[r, c] + blue[r, c]; //print("value " + (red[r, c] + terrain[r, c] + blue[r, c]));
            }
        }
    }
    public float[] GetIM5x5(int xCenter, int yCenter)
    {
        float[,] t = new float[5, 5];
        float[] result = new float[25];
        for (int x = xCenter - 2, xx =0; x < xCenter + 2; x++, xx++)
        {
            for (int y = yCenter - 2, yy =0; y < yCenter + 2; y++, yy++)
            {
                if (x >= 0 && x <= 14 && y >= 0 && y <= 9)
                {
                    t[xx, yy] = value[x, y];
                }
                else
                {
                    t[xx, yy] = 0;
                }
            }
        }
        for (int i = 0; i < 25; i++)
        {
            result[i] = t[i % 5, i / 5];
        }
        return result;
    }
    public void Nagetive(Manager_.OnMove oM)
    {
        
        if (oM == Manager_.OnMove.Red)
        {
            for (int c = 0; c < 10; c++)
            {
                for (int r = 0; r < 15; r++)
                {
                    if (blue[r, c] > 0)
                    {
                        value[r, c] = -(float)Math.Round(Mathf.InverseLerp(0, 3, (red[r, c] + terrain[r, c] + blue[r, c])), 1, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        value[r, c] = (float)Math.Round(Mathf.InverseLerp(0, 3, (red[r, c] + terrain[r, c] + blue[r, c])), 1, MidpointRounding.AwayFromZero);
                    }  
                }
            }
        }
        else
        {
            for (int c = 0; c < 10; c++)
            {
                for (int r = 0; r < 15; r++)
                {
                    if (red[r, c] > 0)
                    {
                        value[r, c] = -(float)Math.Round(Mathf.InverseLerp(0, 3, (red[r, c] + terrain[r, c] + blue[r, c])), 1, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        value[r, c] = (float)Math.Round(Mathf.InverseLerp(0, 3, (red[r, c] + terrain[r, c] + blue[r, c])), 1, MidpointRounding.AwayFromZero);
                    }
                }
            }
        }
        ////test
        //for (int c = 0; c < 10; c++)
        //{
        //    for (int r = 0; r < 15; r++)
        //    {
        //        print(value[r, c]);
        //    }
        //}
    }
    public void SetTerraint(int r, int c, int value)
    {
        if (red[r, c] <= 0)
        {
            terrain[r, c] -= value;// print(terrain[r, c]);            
        }
        else
        {
            terrain[r, c] += value;// print(terrain[r, c]);            
        }
        if (blue[r, c] <= 0)
        {
            terrain[r, c] -= value;// print(terrain[r, c]);            
        }
        else
        {
            terrain[r, c] += value;// print(terrain[r, c]);            
        }
    }
    public void SetRed(Manager_.OnMove onMove, int r, int c, int value)
    {
        if (onMove == Manager_.OnMove.Red)
        {
            red[r, c] += value; //print(red[r, c] + "~");
        }
        else
        {
            red[r, c] -= value;
        }
    }
    public void SetBlue(Manager_.OnMove onMove, int r, int c, int value)
    {
        if (onMove == Manager_.OnMove.Red)
        {
            blue[r, c] += value;
        }
        else
        {
            blue[r, c] -= value;
        }
    }
    //private float Math(int min, int max, float value)
    //{
    //    if (value <= min)
    //        return -1;
    //    if (value >= max)
    //        return 1;
    //    float t = value - min;
    //    float per = max - min;
        
    //}
    public void ValueToRender()
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                display[r, c].SetDisplay((int)(10 * value[r, c]), red[r, c], terrain[r, c] / 5, blue[r, c]);
            }
        }
    }
    private void ResetTerrain()
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                terrain[r, c] = 0;
            }
        }
    }
    private void ResetRed()
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                red[r, c] = 0;
            }
        }
    }
    private void ResetBlue()
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                blue[r, c] = 0;
            }
        }
    }
    public void ResetAllZero()
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                display[r, c].Reset();
                blue[r, c] = 0;
                red[r, c] = 0;
                terrain[r, c] = 0;             
            }
        }
    }
    public void LableIM()
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                display[r, c].ShowLabel();
            }
        }
    }
    
    public void New(int value)
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                //cellIm[r, c].Set(/*value*/Random.Range(0,11), r, c);
            }
        }
    }
}
