using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{   
    [Range(1,10)]
    [SerializeField] float scaleChange;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // FauxJump();
    }

    void FauxJump() {
    //     transform.position = new Vector2(transform.position.x, scaleChange * 1.1f);
    //     transform.localScale = new Vector2(scaleChange, scaleChange);
    }
}
