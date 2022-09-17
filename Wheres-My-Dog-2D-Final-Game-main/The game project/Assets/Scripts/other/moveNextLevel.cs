using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveNextLevel : MonoBehaviour
{
    public int enemyKilled = 0;
    public bool end = false;
    
    void Update()
    {
        if (end) {
            Destroy(gameObject);
        }
    }

    
  void OnTriggerEnter2D(Collider2D other) {
      if ((other.gameObject.tag == "Player" || other.gameObject.tag == "temp") && enemyKilled == 11){
          SceneManager.LoadScene("Level_02");
      }
      if ((other.gameObject.tag == "Player" || other.gameObject.tag == "temp") && enemyKilled == 15){
          SceneManager.LoadScene("Level_03");
      }
    if ((other.gameObject.tag == "Player" || other.gameObject.tag == "temp") && enemyKilled == 12){
          SceneManager.LoadScene("Level_03_Boss");
      }
  }
}
