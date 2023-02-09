using System;
using UnityEngine;
using CodeMonkey.Utils;

public class Grid<TGridObject>
{
    private int width;
    private int height;
    private TGridObject[,] gridArray;

    private TextMesh[,] debugTextArray;

    private float cellSize;

    private Vector3 originPosition;

    public event EventHandler<OnGridValueChangedEventArgs> OnGridValueChanged; // Only for debugging, to update world space text
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int z;
    }

    public Grid(int width, int height, float cellSize, Vector3 originPosition)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];
        debugTextArray = new TextMesh[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                // Create text displaying what content is held by grid cell
                //debugTextArray[x, z] = UtilsClass.CreateWorldText(gridArray[x, z]?.ToString(),
                //    null, GetWorldPosition(x, z) + new Vector3(cellSize, cellSize) * 0.5f, 10, Color.white, TextAnchor.MiddleCenter);
                // Draw the grid
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.white, 100.0f);
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.white, 100.0f);
            }
        }
        // Finish drawing the top and right side of the grid
        Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100.0f);
        Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100.0f);

        OnGridValueChanged += (object sender, OnGridValueChangedEventArgs eventArgs) =>
        {
           // debugTextArray[eventArgs.x, eventArgs.z].text = gridArray[eventArgs.x, eventArgs.z]?.ToString();
        };
    }

    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, originPosition.y, z) * cellSize + originPosition;
    }

    public void GetXZ(Vector3 worldPosition, out int x, out int z)
    {
        // Get the grid X and Y off the world position vector
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        z = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
    }

    public void SetValue(int x, int z, TGridObject value)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            // Update the value at these grid coordinates
            gridArray[x, z] = value;
            //debugTextArray[x, z].text = gridArray[x, z]?.ToString();
            // Denote grid has been changed
            if (OnGridValueChanged != null) { OnGridValueChanged(this, new OnGridValueChangedEventArgs { x = x, z = z }); }
        }
        else
        {
            Debug.Log("Position not within grid bounds");
            return;
        }
    }

    public void SetValue(Vector3 worldPosition, TGridObject value)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        SetValue(x, z, value);
    }

    public TGridObject GetValue(int x, int z)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            // Get the value at these grid coordinates
            return gridArray[x, z];
        }
        else
        {
            return default(TGridObject);
        }
    }

    public TGridObject GetValue(Vector3 worldPosition)
    {
        int x, z;
        GetXZ(worldPosition, out x, out z);
        return GetValue(x, z);
    }
    
    public bool IsValidGridPosition(int x, int z)
    {
        return x >= 0.0f && z >= 0.0f && x < width && z < height;
    }

    public float GetWidth()
    {
        return width;
    }

    public float GetHeight()
    {
        return height;
    }
}

