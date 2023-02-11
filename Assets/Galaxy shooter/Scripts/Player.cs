  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    //public or private identify
    //data type ( int, floats, bool, strings )
    //every variable has a NAME
    //option value assigned



    public bool canTripleShot = false; 
    public bool isSpeedBoostActive = false;
    public bool shieldsActive = false;
       
    public int lives = 3;

    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private GameObject laserPrefab;
    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = 0.0f;
    
    [SerializeField]
    private GameObject _shieldGameObject ;

    [SerializeField]
    private GameObject[] _engines;  



    [SerializeField]
    private float _speed = 5.0f;

    private UIManager _uiManager;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    private int hitCount = 0;




    private void Start()
    {
        //current pos = new position
        transform.position = new Vector3(0, 0, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(lives);
        } 

        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        if(_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutine(); 
        }

        _audioSource = GetComponent<AudioSource>();

        hitCount = 0;

    }    

    // Update is called once per frame
    private void Update()
    {
        Movement();

        //if space key pressed
        //spawn laster at player position 

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }
    }

        //if triple shot
        //shoot 3 lasers
        //else
        //shoot 1



    private void Shoot()
        {
            if (Time.time > _canFire)
            {
                _audioSource.Play();
                if (canTripleShot == true)
                {
                    //left 
                    Instantiate(laserPrefab, transform.position + new Vector3(-0.95f, 0.05f, 0), Quaternion.identity);
                    //right
                    Instantiate(laserPrefab, transform.position + new Vector3(1.04f, 0.05f, 0), Quaternion.identity);
                    //centre
                    Instantiate(laserPrefab, transform.position + new Vector3(0, 1.16f, 0), Quaternion.identity);
                }
                else
                {
                    Instantiate(laserPrefab, transform.position + new Vector3(0, 1.16f, 0), Quaternion.identity);
                }
                
                _canFire = Time.time + _fireRate;
            }
    
        }

    private void Movement()
        {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //if speed boost enabled 
        //move 1.5x the normal speed
        //else
        //move normal speed

        if (isSpeedBoostActive == true)
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed * 1.8f * horizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * _speed * 1.8f * verticalInput);
        }
        else 
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);
            transform.Translate(Vector3.up * Time.deltaTime * _speed * verticalInput);
 
        }

        //if the player on the y is greater than 0 
        //set player position to 0 

        if(transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -3.36f)
        {
            transform.position = new Vector3(transform.position.x, -3.36f, 0);
        }

        //if the player on the x is greater than 11.18605
        //position on the x needs to be -11.18605

        if(transform.position.x > 11.18605f)
        {
            transform.position = new Vector3(-11.18605f, transform.position.y, 0);
        }
        else if(transform.position.x < -11.18605f)
        {
            transform.position = new Vector3(11.18605f, transform.position.y, 0);
        }
  
        }

    public void Damage()
    {
        //substract 1 life from the player
        //if player has shields do nothing 
        
        if (shieldsActive == true)
        {
            shieldsActive = false;
            _shieldGameObject.SetActive(false);

            return;
        }

        hitCount++;

        if(hitCount == 1)
        {
           //turn left_engine failure on
           _engines[0].SetActive(true);
        }
        else
        {
            //turn right_engine failure on
            _engines[1].SetActive(true); 
        }

        lives--;
        _uiManager.UpdateLives(lives);


        //if  live = 0
        //destroy the object 
        if (lives < 1)
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _gameManager.gameOver = true;
            _uiManager.ShowTitleScreen();
            Destroy(this.gameObject);
        }
    }    

    public void TripleShotPowerupOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedBoostPowerOn()
    {
        isSpeedBoostActive = true;
        StartCoroutine(SpeedBoostDownRoutine());
    }

    public void EnableShields()
    {
        shieldsActive = true;
        _shieldGameObject.SetActive(true);
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public IEnumerator SpeedBoostDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        isSpeedBoostActive = false;
    }
}