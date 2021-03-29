using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldOfView : MonoBehaviour {

    [Range(0, 10)]
	public float viewRadius;
	[Range(0, 360)]
	public float viewAngle;

	public LayerMask targetMask;
	public LayerMask obstacleMask;
    public float chaseSpeed;
	public List<Transform> visibleTargets = new List<Transform>();

	void Start()
    {
		StartCoroutine ("FindTargetsWithDelay", .2f);
	}

	IEnumerator FindTargetsWithDelay(float delay)
    {
		while (true) {
			yield return new WaitForSeconds (delay);
			FindVisibleTargets ();
		}
	}

	void FindVisibleTargets()
    {
        visibleTargets.Clear();
        Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), viewRadius , targetMask);
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if (Vector3.Angle(transform.right, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
				if (!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

	public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
			angleInDegrees += transform.eulerAngles.z;
		}
		return new Vector2(Mathf.Cos(angleInDegrees * Mathf.Deg2Rad),Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
	}
}