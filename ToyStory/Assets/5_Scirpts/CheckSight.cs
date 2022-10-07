using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSight : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public bool isDetected;

    [HideInInspector]
    public List<Transform> visibleTarget = new List<Transform>();
    
    void Start()
    {
        StartCoroutine("Finding");
        isDetected = false;
    }

    IEnumerator Finding()
    {
        while(true)
        {
            yield return new WaitForSeconds(.2f);
            FindTarget();
        }
    }

    void FindTarget()
    {
        visibleTarget.Clear();
        isDetected = false;
        Collider[] targetInView = Physics.OverlapSphere (transform.position, viewRadius, targetMask);
        for(int i = 0; i < targetInView.Length; i++)
        {
            Transform target = targetInView[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if(!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTarget.Add(target);
                    isDetected = true;
                }
            }
        }
        

    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) //각도변환
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
