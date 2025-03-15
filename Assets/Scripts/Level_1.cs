using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_1 : MonoBehaviour
{
    //public GameObject player;
    //public string levelName;
    private bool door;


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E) && door == true)
        {
            SceneController.instance.LoadScene("World 1");
            Debug.Log("Enter Level 2");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            door = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision2)
    {
        if (collision2.gameObject.tag == "Player")
        {
            door = false;
        }

    }
}