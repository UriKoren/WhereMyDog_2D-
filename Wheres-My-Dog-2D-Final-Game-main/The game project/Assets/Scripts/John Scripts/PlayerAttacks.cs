using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : MonoBehaviour
{
    private Animator anim; 
    private PlayerMovement playerMovement;
    private float cdTimer = 10000;
    private float cdTimer2 = 10000;

    public Transform attackPoint; 
    public Transform attackPoint2; 
    public LayerMask enemyLayers; 
    public LayerMask trapLayers; 
    public float attackRange = 0.5f;
    [SerializeField] private float attackCD;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F)&& cdTimer > attackCD && playerMovement.canAtt()) {
            AttackA();
        }
        if (Input.GetKey(KeyCode.G)&& cdTimer2 > attackCD*10 && playerMovement.canAtt()) { //special attack
            AttackB();
        }
        cdTimer += Time.deltaTime;
        cdTimer2 += Time.deltaTime;
    }

    

    void OnDrawGizmosSelected() { //for simple attack
        if (attackPoint == null) {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void AttackA() {
        anim.SetTrigger("attackA");
        cdTimer = 0;
    }

    private void AttackHelperA() { //execute based on animation frame
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies) {
            if (transform.position.x > enemy.transform.position.x && enemy.transform.localScale.x < 0) {
                enemy.GetComponent<Health>().TakeDamage(1, false, 0.4f);
            }
            if (transform.position.x > enemy.transform.position.x && enemy.transform.localScale.x > 0) {
                enemy.GetComponent<Health>().TakeDamage(1, true, 0.4f);
            }
            if (transform.position.x < enemy.transform.position.x && enemy.transform.localScale.x < 0) {
                enemy.GetComponent<Health>().TakeDamage(1, true, 0.4f);
            }
            if (transform.position.x < enemy.transform.position.x && enemy.transform.localScale.x > 0) {
                enemy.GetComponent<Health>().TakeDamage(1, false, 0.4f);
            }
        }
        Collider2D[] traps = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, trapLayers);
        foreach(Collider2D trap in traps) {
            trap.GetComponent<trap_f>().destroy();
        }
    }
    private void AttackB() {
        anim.SetTrigger("attackB");
        cdTimer2 = 0;
    }

    private void AttackHelperB() { //execute based on animation frame
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint2.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hitEnemies) { 
            if (transform.position.x > enemy.transform.position.x && enemy.transform.localScale.x < 0) {
                enemy.GetComponent<Health>().TakeDamage(5, false, 0.8f); //we wants attack b to knock enemies to a bigger distance than attack a
            }
            if (transform.position.x > enemy.transform.position.x && enemy.transform.localScale.x > 0) {
                enemy.GetComponent<Health>().TakeDamage(5, true, 0.8f);
            }
            if (transform.position.x < enemy.transform.position.x && enemy.transform.localScale.x < 0) {
                enemy.GetComponent<Health>().TakeDamage(5, true, 0.8f);
            }
            if (transform.position.x < enemy.transform.position.x && enemy.transform.localScale.x > 0) {
                enemy.GetComponent<Health>().TakeDamage(5, false, 0.8f);
            }
        }

        Collider2D[] traps = Physics2D.OverlapCircleAll(attackPoint2.position, attackRange, trapLayers);
        foreach(Collider2D trap in traps) {
            trap.GetComponent<trap_f>().destroy();
        }
    }}

