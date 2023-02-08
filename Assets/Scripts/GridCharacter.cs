using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that defines characters on the grid, including the player and enemies
/// </summary>
public class GridCharacter : GridEntity
{
    [SerializeField] private int x;
    [SerializeField] private int z;
    [Space(10)]

    [SerializeField] private float transitionDuration = 0.25f;

    public void MoveUp()
    {
        // Don't allow movement whilst moving
        if (!IsIdle()) { return; }

        // Reset the grid position the entity is moving from
        SetEntityOnGrid(x, z, null);
        // Update the coresponding grid coordinate respective to movement direction
        z = z + 1;
        UpdatePosition();
    }

    public void MoveDown()
    {
        // Don't allow movement whilst moving
        if (!IsIdle()) { return; }

        // Reset the grid position the entity is moving from
        SetEntityOnGrid(x, z, null);
        // Update the coresponding grid coordinate respective to movement direction
        z = z - 1;
        UpdatePosition();
    }

    public void MoveLeft()
    {
        // Don't allow movement whilst moving
        if (!IsIdle()) { return; }

        // Reset the grid position the entity is moving from
        SetEntityOnGrid(x, z, null);
        // Update the coresponding grid coordinate respective to movement direction
        x = x - 1;
        UpdatePosition();
    }

    public void MoveRight()
    {
        // Don't allow movement whilst moving
        if (!IsIdle()) { return; }

        // Reset the grid position the entity is moving from
        SetEntityOnGrid(x, z, null);
        // Update the coresponding grid coordinate respective to movement direction
        x = x + 1;
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        // Make sure the target grid position is on the grid
        if (!myGrid.IsValidGridPosition(x, z))
        {
            Debug.Log("Target grid position is invalid");
            myGrid.GetXZ(transform.position, out x, out z);
            SetEntityOnGrid(x, z, this.gameObject.GetComponent<GridEntity>());
            // Exit this function if the target grid position is invalid
            return;
        }
        // Make sure the target grid position is empty before moving
        if (myGrid.GetValue(x, z) != null)
        {
            Debug.LogWarning("Cannot move to " + x + ", " + z);
            myGrid.GetXZ(transform.position, out x, out z);
            SetEntityOnGrid(x, z, this.gameObject.GetComponent<GridEntity>());
            // Exit this function if the target grid position is not empty
            return;
        }

        SetEntityOnGrid(x, z, this.gameObject.GetComponent<GridEntity>());
        Vector3 newPosition = myGrid.GetWorldPosition(x, z);

        StartCoroutine(LerpPosition(newPosition + new Vector3(1.0f, 0.0f, 1.0f) * 0.5f, transitionDuration));
    }

    public bool IsIdle()
    {
        if (transform.position != myGrid.GetWorldPosition(x, z) + new Vector3(1.0f, 0.0f, 1.0f) * 0.5f)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0.0f;
        Vector3 startPosition = transform.position;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
}
