using UnityEngine;
public class LayerIM : MonoBehaviour {
    CellIM[,] cellIm;
    public int[,] red;
    public int[,] blue;
    public int[,] terrain;
    void Start()
    {
        //Create();
        //New();
        //print(Mathf.InverseLerp(0, 15, 12.5f));
    }
    public void Awake()
    {
        red = new int[15, 10];
        blue = new int[15, 10];
        terrain = new int[15, 10];
        cellIm = new CellIM[15, 10];
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                cellIm[r, c] = Instantiate(Resources.Load("CellIM") as GameObject, transform).GetComponent<CellIM>();
                cellIm[r, c].transform.position = new Vector2(r, c);                
            }
        }
        gameObject.SetActive(false);
    }
    public void SetTerraint(int r, int c, int value)
    {
        //terrain[r, c] += value;
        if (red[r, c] != 0)
        {
            red[r, c] += value;
        }
        if (blue[r, c] != 0)
        {
            blue[r, c] += value;
        }
    }
    public void SetRed(int r, int c, int value)
    {
        red[r, c] += value;
    }
    public void SetBlue(int r, int c, int value)
    {
        blue[r, c] += value;
    }
    public void ValueToRender()
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                cellIm[r, c].SetValue(red[r, c], blue[r, c], terrain[r, c]);
            }
        }
    }
    public void ResetTerrain()
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                terrain[r, c] = 0;
            }
        }
    }
    public void ResetRed()
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                red[r, c] = 0;
            }
        }
    }
    public void ResetBlue()
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
                cellIm[r, c].Reset();
                blue[r, c] = 0;
                red[r, c] = 0;                
            }
        }
    }
    public void LableIM()
    {
        for (int c = 0; c < 10; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                cellIm[r, c].ShowLabel();
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
