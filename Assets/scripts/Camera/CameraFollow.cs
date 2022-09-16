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
        if(!target)
        {
            target = GameObject.FindWithTag("Player");
        }
    }

    void Update()
    {
        transform.position = target.transform.position + offset;      
    }
}
