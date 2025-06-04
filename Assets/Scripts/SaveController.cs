using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private SpriteRenderer _sprite;

    private void Start()
    {
        _sprite = gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet"))
        {
            Debug.Log(collision.name + "Save");
            GameManager.Instance._lastPlayerPosition = new Vector2(GameObject.Find("Player").transform.position.x, GameObject.Find("Player").transform.position.y);
            collision.gameObject.SetActive(false);
            StartCoroutine(ChangeColor());
            //GameManager.Instance._lastPlayerRb;
        }
        if (collision.CompareTag("Player"))
        {
            Debug.Log(collision.name + "Save");
            GameManager.Instance._lastPlayerPosition = new Vector2(collision.transform.position.x, collision.transform.position.y);
            StartCoroutine(ChangeColor());
        }
    }
    
    private IEnumerator ChangeColor()
    {
        _sprite.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        _sprite.color = Color.white;
    }
}
