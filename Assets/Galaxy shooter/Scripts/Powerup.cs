using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    
    [SerializeField]
    private int powerupID; 

    [SerializeField]
    private AudioClip _clip;

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7)
        {
            Destroy(this.gameObject); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            Debug.Log("Collided with: " + other.name);
            //acces player

            Player player = other.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);


            if (player != null)
            {
                
                //enable triple shot
                if (powerupID == 0)
                {
                    player.TripleShotPowerupOn();
                }
                else if (powerupID == 1)
                {
                    //enable speed bopst here
                    player.SpeedBoostPowerOn(); 
                }
                else if (powerupID == 2)
                {
                    //enable shield here
                    player.EnableShields();
                }
                
            }                

            //destroy our selves 
            Destroy(this.gameObject);
        }

    }
}