using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyTank : MonoBehaviour {

    private float moveSpeed;
    private float step;
    private float stepCounter;
    private GameObject bullet;
    public GameObject enemyBullet;
    public GameObject sensor;
    public GameObject explosion;
    public LayerMask whatIsObsticle;
    public int turnChance;
    private int shotChance;
    private bool obsticle;
    private bool maxSpeed;
    private int direction;
    private Rigidbody2D myRigBody;

    private GameControl gameControl;
    private EnemyBullet[] bulletsShot;

    // Use this for initialization
    void Start()
    {
        gameControl = FindObjectOfType<GameControl>();
        myRigBody = GetComponent<Rigidbody2D>();
        direction = 2;
        stepCounter = step;
        moveSpeed = gameControl.GetMoveSpeed();
        shotChance = gameControl.GetShotChance();
        step = gameControl.GetStep();
        bullet = enemyBullet;
    }

    // Update is called once per frame
    void Update()
    {

        moveSpeed = gameControl.GetMoveSpeed();
        step = gameControl.GetStep();

        shotChance = gameControl.GetShotChance();

        maxSpeed = (direction == 8 && myRigBody.velocity.y > moveSpeed) ||
            (direction == 2 && myRigBody.velocity.y < -moveSpeed) ||
            (direction == 4 && myRigBody.velocity.x < -moveSpeed) ||
            (direction == 6 && myRigBody.velocity.x > moveSpeed);

        obsticle = Physics2D.OverlapCircle(sensor.transform.position, 0.1f, whatIsObsticle);

        Decide();

        Move();

        stepCounter -= Time.deltaTime;
    }

    void Decide()
    {
        if (obsticle)
        {
            Stop();
            Turn();
        }
        if (stepCounter <= 0)
        {
            int i = Random.Range(1, 101);
            if (i <= turnChance)
                Turn();
            int j = Random.Range(1, 101);
            if (j <= shotChance)
            {
                Shot();
            }
            stepCounter = step;
        }
    }

    void Shot()
    {
        bulletsShot = GameObject.FindObjectsOfType<EnemyBullet>();

        if (bulletsShot.Length <= 10)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            SoundHandler.PlaySound(SoundHandler.shotSound);
        }
    }

    void Move()
    {
        if (!maxSpeed)
        {
            myRigBody.AddRelativeForce(Vector2.up * moveSpeed, ForceMode2D.Impulse);
        }
        else if (maxSpeed)
        {
            myRigBody.velocity = new Vector2(myRigBody.velocity.x,
                                              myRigBody.velocity.y);
        }

        if (direction == 4 || direction == 6)
        {
            myRigBody.velocity = new Vector2(myRigBody.velocity.x, 0);
        }
        else if (direction == 2 || direction == 8)
        {
            myRigBody.velocity = new Vector2(0, myRigBody.velocity.y);
        }
    }

    void Turn()
    {
        int i = Random.Range(0, 4);
        if (i == 0)
            direction = 2;
        if (i == 1)
            direction = 4;
        if (i == 2)
            direction = 6;
        if (i == 3)
            direction = 8;

        if (direction == 4)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x,
                                                  transform.rotation.y, 90);
        }
        if (direction == 8)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x,
                                                  transform.rotation.y, 0);
        }
        if (direction == 2)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x,
                                                  transform.rotation.y, 180);
        }
        if (direction == 6)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x,
                                                  transform.rotation.y, 270);
        }
        transform.position = new Vector2(Mathf.Round(transform.position.x),
                                              Mathf.Round(transform.position.y));
    }

    public void Stop()
    {
        myRigBody.velocity = new Vector2(0, 0);
    }

    public void Kill()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        SoundHandler.PlaySound(SoundHandler.explosionSound);
        GameControl.addScore(100);
        Destroy(gameObject);
    }
}
