using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap_f : MonoBehaviour
{
 //set the values in the inspector
    public Transform target; //drag and stop player object in the inspector
    public float speed;


    // Update is called once per frame
    void Update() {
            transform.position = Vector2.MoveTowards (transform.position, new Vector2(target.position.x, target.position.y-0.3f), speed * Time.deltaTime);    
    }

    public void destroy () { 
         Destroy(gameObject);
         }

     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(1, true, 0);
        }
    }
}