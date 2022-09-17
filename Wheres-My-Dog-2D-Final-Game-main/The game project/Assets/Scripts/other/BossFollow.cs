using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFollow : MonoBehaviour
{
 //set the values in the inspector
    public Transform target; //drag and stop player object in the inspector
    public float speed;
    public Animator anim;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x - target.transform.position.x > 3 || transform.position.x - target.transform.position.x < -3)  {
            anim.SetBool("moving", true);
            transform.position = Vector2.MoveTowards (transform.position, new Vector2(target.position.x+3, transform.position.y), speed * Time.deltaTime);    
        }
        else {
            anim.SetBool("moving", false);
        }
    
    if (target.transform.position.x > transform.position.x) {
        transform.localScale = new Vector3(-5,5,1);
    }
    else {
        transform.localScale = new Vector3(5,5,1);
    }
    }
    
    }
