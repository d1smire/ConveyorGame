using System;
using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform targetPosition;
    [SerializeField] private float duration = 2f;

    [Header("Smooth settings")]
    [SerializeField] private AnimationCurve startMovementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    [SerializeField] private AnimationCurve endMovementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);


    public event Action<bool, TypeOfAnimationEnd> onAnimationEnd; 

    public void EndMovement() 
    {
        StartCoroutine(MoveToTarget(startPosition.position, duration, endMovementCurve));
    }

    public void StartMovement() 
    {
        StartCoroutine(MoveToTarget(targetPosition.position, duration, startMovementCurve));
    }

    private IEnumerator MoveToTarget(Vector3 target, float time, AnimationCurve movementCurve)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            float linearProgress = elapsedTime / time; // calculating linear progress time from 0 to 1
            float easedProgress = movementCurve.Evaluate(linearProgress); // converting linear progress on a smooth move progress
            transform.position = Vector3.Lerp(startPosition, target, easedProgress); // moving our obj.

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = target; // fix final position
        if (transform.position == targetPosition.position) 
            onAnimationEnd?.Invoke(true, TypeOfAnimationEnd.StartUpMenu);
        else
            onAnimationEnd?.Invoke(true, TypeOfAnimationEnd.LeaveStartUp);
    }
}
