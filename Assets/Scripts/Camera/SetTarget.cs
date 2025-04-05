using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;


public class SetTarget : MonoBehaviour
{
    private CinemachineTargetGroup targets;
    private void Awake()
    {
        GameObject groupObj = GameObject.Find("Target Group");
        if (groupObj != null)
        {
            targets = groupObj.GetComponent<CinemachineTargetGroup>();
            if (targets != null)
            {
                targets.AddMember(transform, 1f, 15f);
            }
        }
    }

    private void OnDestroy()
    {
        if (targets != null)
        {
            targets.RemoveMember(transform);
        }
        
    }
}
