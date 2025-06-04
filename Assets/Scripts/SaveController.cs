using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            Debug.Log(collision.name + "Save");
            GameManager.Instance._lastPlayerPosition = new Vector2(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y);
            //GameManager.Instance._lastPlayerRb;
        }
        if (collision.CompareTag("Player"))
        {
            Debug.Log(collision.name + "Save");
            GameManager.Instance._lastPlayerPosition = new Vector2(collision.transform.position.x, collision.transform.position.y);
        }
    }
}
