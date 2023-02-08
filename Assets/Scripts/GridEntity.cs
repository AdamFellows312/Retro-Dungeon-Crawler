using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEntity : MonoBehaviour
{
    // Set in the classes that inherit from this class
    public Grid<GridEntity> myGrid;

    public void SetEntityOnGrid(int x, int z, GridEntity entity)
    {
        // Set this objects position on the grid
        if (entity == null)
        {
            myGrid.SetValue(x, z, null);
        }
        else
        {
            myGrid.SetValue(x, z, entity);
        }
    }

    public Vector3 GetEntityGridPosition()
    {
        int x, z;
        // Get the grid x and z, outputting them into temporary ints 'x' and 'z'
        myGrid.GetXZ(transform.position, out x, out z);
        // Return the x and z values as a Vector3Int
        return new Vector3Int(x, 0, z);
    }
}
