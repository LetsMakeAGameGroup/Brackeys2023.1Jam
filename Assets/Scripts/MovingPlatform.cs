using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : Listener, IPuzzleSuccessReceptor 
{
    [SerializeField] Transform platform;

    public float movingPlatformSpeed;
    public bool canLoop = true;
    public bool isActiveAtStart = true;

    public List<Vector3> positionPoints = new List<Vector3>();
    int currentPositionIndex;

    private Coroutine moveToNextPointCoroutine;

    //[SerializeField] private PlatformColliderTrigger trigger;

    private void Start()
    {
        if (isActiveAtStart)
        {
            ResumeMovingPlatform();
        }
    }

    public void StopMovingPlatform() 
    {
        StopAllCoroutines();
        moveToNextPointCoroutine = null;
    }

    public void ResumeMovingPlatform() 
    {
        if (moveToNextPointCoroutine == null) 
        {
            moveToNextPointCoroutine = StartCoroutine(MoveToNextPoint(GetPositionByIndex(currentPositionIndex)));
        }
    }

    IEnumerator MoveToNextPoint(Vector3 positionToMove)
    {
        float timeElapsed = 0;

        Vector3 currentPos = platform.localPosition;
        Vector3 nextPoint = positionToMove;

        //trigger.movement = (nextPoint - currentPos).normalized;

        while (platform.localPosition != nextPoint)
        {
            platform.localPosition = Vector3.MoveTowards(currentPos, nextPoint, timeElapsed);
            timeElapsed += movingPlatformSpeed * Time.deltaTime;

            yield return null;
        }

        platform.localPosition = nextPoint;
        OnPointReached();

        yield return null;
    }

    private Vector3 GetPositionByIndex(int index)
    {
        if (positionPoints.Count == 0)
        {
            Debug.LogWarning(transform.name + " : Moving platform doesn't have any position points. Returning Vector3.zero.");
            return Vector3.zero;
        }

        if (currentPositionIndex >= positionPoints.Count)
        {
            Debug.LogWarning(transform.name + " : Index out of range, returning Vector3.zero.");
            return Vector3.zero;
        }

        return positionPoints[index];
    }

    private Vector3 GetNextPoint()
    {
        if (positionPoints.Count == 0)
        {
            Debug.LogWarning(transform.name + " : Moving platform doesn't have any position points. Returning Vector3.zero.");
            return Vector3.zero;
        }

        currentPositionIndex++;

        if (currentPositionIndex >= positionPoints.Count)
        {
            currentPositionIndex = 0;
        }

        return positionPoints[currentPositionIndex];
    }

    void OnPointReached() 
    {
        if (moveToNextPointCoroutine != null)
        {
            StopCoroutine(moveToNextPointCoroutine);
        }

        //If we on the final point
        if (currentPositionIndex == positionPoints.Count - 1) 
        {
            if (!canLoop) 
            {
                return;
            }
        }

        moveToNextPointCoroutine = StartCoroutine(MoveToNextPoint(GetNextPoint()));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        foreach (Vector3 pos in positionPoints) 
        {
            Gizmos.DrawSphere(transform.position + pos, 0.25f);
        }
    }

    public override void ReceiveMessage(Subject Invoker)
    {
        int ammountOfCompleted = 0;

        for (int i = 0; i < subjects.Count; i++)
        {
            if (subjects[i].compleated)
            {
                ammountOfCompleted++;
            }
        }

        if (ammountOfCompleted == subjects.Count)
        {
            OnPuzzleSuccess();
        }
        else
        {
            OnPuzzleFailure();
        }
    }

    public void OnPuzzleSuccess()
    {
        if (isActiveAtStart)
        {
            StopMovingPlatform();
        }
        else 
        {
            ResumeMovingPlatform();
        }
    }

    public void OnPuzzleFailure()
    {
        if (isActiveAtStart)
        {
            ResumeMovingPlatform();
        }
        else
        {
            StopMovingPlatform();
        }
    }
}
