using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] public float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    [SerializeField] private restart res;


    [Header("iFrames")]
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    public int tempForLayer;
    public string tempForTag;
    [SerializeField] private moveNextLevel count;


    //private PlayerMovement john;
    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage, bool checkIfBackAttack, float dis)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        if (currentHealth > 0)
        {
            if (gameObject.tag == "enemy") { //if we attack an enemy we wants to knock him in the appropriate direction, first we check if we attacked enemy from the front or the back than we move his position based on the logic.
                    if (checkIfBackAttack == false) {
                        if (transform.localScale.x > 0) {
                        transform.position = transform.position + new Vector3(dis, 0, 0);
                        }
                        if (transform.localScale.x < 0) {
                        transform.position = transform.position + new Vector3(-1*dis, 0, 0);            
                        }}
                    else {
                        if (transform.localScale.x > 0) {
                        transform.position = transform.position + new Vector3(-1*dis, 0, 0);            
                        }
                        if (transform.localScale.x < 0) {
                        transform.position = transform.position + new Vector3(dis, 0, 0);            
                        }
                        }        
                }     
            anim.SetTrigger("hurt");
            if (gameObject.tag == "Player" || gameObject.tag == "temp")
                StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                if (gameObject.tag == "Player" || gameObject.tag == "temp") {
                    res.GetComponent<restart>().playerdead  = true;
                }
                else if (SceneManager.GetActiveScene().name != "Level_03_Boss"){
                        count.enemyKilled += 1;
                }
                anim.SetTrigger("die"); //disable player
                if (GetComponent<PlayerMovement>() != null)  {
                    GetComponent<PlayerMovement>().enabled = false;
                    GetComponent<PlayerAttacks>().enabled = false;
                    gameObject.layer = LayerMask.NameToLayer("Default");
                    gameObject.tag = "temp";
                    }
                if (GetComponentInParent<EnemyPatrol>() != null)  { //disable enemy
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                    GetComponent<MeleeEnemy>().enabled = false;}

                if (GetComponent<BossFollow>() != null) { //disable boss
                    count.end = true;//free the doggo
                    GetComponent<BossFollow>().enabled = false;
                }

            }
            dead = true;
        }}

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        tempForLayer = gameObject.layer;
        gameObject.layer = LayerMask.NameToLayer("Default");
        tempForTag = gameObject.tag;
        gameObject.tag = "temp";
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(1);
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(1);
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
            gameObject.layer = LayerMask.NameToLayer("player");
            gameObject.tag = "Player";
    }
}