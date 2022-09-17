using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class restart : MonoBehaviour
{
    public bool playerdead = false;

    void Update()
    {
        if (playerdead && SceneManager.GetActiveScene().name == "Level_01") {
            if (Input.GetKey(KeyCode.Space)) {
                SceneManager.LoadScene("Level_01");
            }
        }
        if (playerdead && SceneManager.GetActiveScene().name == "Level_02") {
            if (Input.GetKey(KeyCode.Space)) {
                SceneManager.LoadScene("Level_02");
            }
        }
        if (playerdead && SceneManager.GetActiveScene().name == "Level_03") {
            if (Input.GetKey(KeyCode.Space)) {
                SceneManager.LoadScene("Level_03");
            }
        }
        if (playerdead && SceneManager.GetActiveScene().name == "Level_03_Boss") {
            if (Input.GetKey(KeyCode.Space)) {
                SceneManager.LoadScene("Level_03_Boss");
            }
        }
    }
}
