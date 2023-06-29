using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class SkillImpactPrefabArrow : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 endPos;
    private UnityAction onComplete;
    private float speed = 6f;

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, endPos, Time.deltaTime * speed);
        float stopDistance = 0.01f;
        if (Vector2.Distance(transform.position, endPos) < stopDistance)
        {
            onComplete?.Invoke();
            PoolMgr.Instance.PushObj("SkillImpactPrefab/Arrow", gameObject);
        }
    }

    public void Setup(Vector2 startPos, Vector2 endPos, UnityAction onComplete)
    {
        this.startPos = startPos;
        this.endPos = endPos;
        this.onComplete = onComplete;

        transform.position = startPos;
        Vector2 direction = (startPos - endPos).normalized;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
