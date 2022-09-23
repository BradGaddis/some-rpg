using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    [SerializeField]
    Vector3 offset; 
    private void Start() {
        if(target == null)
        {
            target = GameObject.FindWithTag("Player");
        }
    }

    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer() {
        transform.position = target.transform.position + offset;      
    }

    void LookAhead(float amount) {
        
    }

    void DeadZone(float x, float y) {
        
    }
}
