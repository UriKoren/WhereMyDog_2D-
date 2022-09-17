using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    private Animator anim;
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private LayerMask wallLayer; 
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;
    private float horizontalInput;


    private void Awake(){
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        //change player direction right or left
        if (horizontalInput > 0.01f) {
            transform.localScale = new Vector3(2.7f,2.7f,2.7f);
        }
        else if (horizontalInput < -0.01f) {

            transform.localScale = new Vector3(-2.7f,2.7f,2.7f);

        }

        anim.SetBool("walk", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());
        

        //wall jump
        if (wallJumpCooldown > 0.2f) {
            
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
            if (onWall() && !isGrounded()) {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else {
                body.gravityScale = 7;
            }
            if(Input.GetKey(KeyCode.Space)) {
              Jump();
            }
        }
        else {wallJumpCooldown += Time.deltaTime;}
}

    private void Jump() {
        if (isGrounded()) {
        body.velocity = new Vector2(body.velocity.x, jumpPower);
        anim.SetTrigger("jump");}
        else if (onWall() && !isGrounded()) {
            if (horizontalInput==0) {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpCooldown = 0; 
        }
}


    public bool isGrounded () {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer); //check if under the player = layer = grounded
        return raycastHit.collider != null;
    }

    public bool onWall () {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAtt () { //cant attack on wall
        return !onWall();
    }

      void OnCollisionEnter2D(Collision2D other) {
      if (other.gameObject.tag == "doggo") {
            SceneManager.LoadScene("Game_Over");
      }
  }
}