using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEnemy : GridCharacter
{
    private void Start()
    {
        // Get the grid
        myGrid = GridManager.instance.characterGrid;

        // Get this enemy's position on the grid
        myGrid.GetXZ(transform.position, out x, out z);

        // Initialise the position of the enemy on the grid
        UpdatePosition();
        BreatheEffect();
    }
}
