using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlayer : GridCharacter
{
    private void Start()
    {
        // Get the grid
        myGrid = GridManager.instance.characterGrid;

        // Get this player's position on the grid
        myGrid.GetXZ(transform.position, out x, out z);

        // Initialise the position of the player on the grid
        UpdatePosition();
        BreatheEffect();
    }

    private void Update()
    {
        // Get the input of the player
        GetInput();

        Debug.Log(IsIdle().ToString());
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveUp();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            MoveDown();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            MoveRight();
        }
    }
}
