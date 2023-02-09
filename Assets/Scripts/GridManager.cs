using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public static GridManager instance;

    public Grid<GridEntity> characterGrid;
    public Grid<GridEntity> objectGrid;

    [SerializeField] private Vector2Int gridSize;

    private void Awake()
    {
        // If there is no instance of the grid manager, assign the singelton as this instance
        if (instance == null) { instance = this; }
    }

    private void Start()
    {
        // Create the character grid
        characterGrid = CreateGrid(gridSize.x, gridSize.y, 1.0f, transform.position);
        // Create the object grid
        objectGrid = CreateGrid(gridSize.x, gridSize.y, 1.0f, transform.position);
    }

    private Grid<GridEntity> CreateGrid(int sizeX, int sizeY, float cellSize, Vector3 origin)
    {
        return new Grid<GridEntity>(sizeX, sizeY, cellSize, origin);
    }
}
