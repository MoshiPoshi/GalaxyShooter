using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyExplosionPrefab;
    
    //variable for ur speed 
 
    private float _speed = 5.5f;
    [SerializeField]
    private AudioClip _clip;
    private UIManager _uiManager;
 

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>(); 
    }
    // Update is called once per frame
    void Update()
    {
        //move down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -7)
        {
            Destroy(this.gameObject); 
        }
        //when off the screen on the bottom
        //respawn back on top with a new x position between the bounds of the screen 

        if (transform.position.y < -7)
        {
            float randomX = Random.Range(-7f,7f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
          if (other.tag == "Laser")
          {
            Destroy(other.gameObject);
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            _uiManager.UpdateScore();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject); 
          }
          else if (other.tag == "Player")
          {
                Player player = other.GetComponent<Player>();
                
                if (player != null)
                { 
                    player.Damage();
                }
            Instantiate(_enemyExplosionPrefab, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            Destroy(this.gameObject);
          }
    }
}