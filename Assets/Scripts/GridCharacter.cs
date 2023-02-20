using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// A class that defines characters on the grid, including the player and enemies
/// </summary>
public class GridCharacter : GridEntity
{

    public bool spriteBreathe = true;
    [Space(10)]

    [SerializeField] private float transitionDuration = 0.25f;
    [SerializeField] private float scaleSpeed = 2.5f;
    [Space(10)]

    public List<GridManager.Node> currentPath = null;

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

    public void BreatheEffect()
    {
        // Don't use the breathing effect if sprite breathing is off
        if (!spriteBreathe) { return; }

        Vector3 targetScale = new Vector3(transform.GetChild(0).localScale.x, 1.15f, transform.GetChild(0).localScale.z);

        var sequence = DOTween.Sequence()
            .Append(transform.GetChild(0).DOScale(targetScale, scaleSpeed))
            .Append(transform.GetChild(0).DOScale(Vector3.one, scaleSpeed));

        //sequence.SetEase(Ease.Linear);
        sequence.SetLoops(-1, LoopType.Restart);
    }
}
