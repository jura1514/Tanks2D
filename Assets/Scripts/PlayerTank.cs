using UnityEngine;
using System.Collections;

public class PlayerTank : MonoBehaviour {

    private bool up, down, left, right, shot;

    public float speed;
    public int direction;
    private int previousDirection;
    private bool moving;
    private bool maxSpeed;
    private bool obsticle;
    private Rigidbody2D myRigBody;
    public LayerMask whatIsObsticle;
    public GameObject sensor;
    public GameObject bullet;
    private Animator animator;

    void Start()
    {
        direction = 8;
        myRigBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (FindObjectOfType<GameControl>().gameOver)
        {
            myRigBody.velocity = new Vector2(0, 0);
            return;
        }

        GetInput();

        obsticle = Physics2D.OverlapCircle(sensor.transform.position, 0.1f, whatIsObsticle);

        moving = left || right || up || down;

        maxSpeed = (direction == 8 && myRigBody.velocity.y > speed) ||
            (direction == 2 && myRigBody.velocity.y < -speed) ||
            (direction == 4 && myRigBody.velocity.x < -speed) ||
            (direction == 6 && myRigBody.velocity.x > speed);

        previousDirection = direction;

        Turn();

        Move();

        ChangeAnime();

        Shot();

    }
 
    void ChangeAnime()
    {
        animator.SetBool("Moving", moving);
    }

    void Turn()
    {
        if (left)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x,
                                                  transform.rotation.y,
                                                  90);
            direction = 4;
        }
        if (up)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x,
                                                  transform.rotation.y,
                                                  0);
            direction = 8;
        }
        if (down)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x,
                                                  transform.rotation.y,
                                                  180);
            direction = 2;
        }
        if (right)
        {
            transform.rotation = Quaternion.Euler(transform.rotation.x,
                                                  transform.rotation.y,
                                                  270);
            direction = 6;
        }
        if (direction != previousDirection)
        {
            transform.position = new Vector2(Mathf.Round(transform.position.x),
                                              Mathf.Round(transform.position.y));
        }
    }

    void Move()
    {
        if (obsticle)
        {
            myRigBody.velocity = new Vector2(0, 0);
        }
        if (moving && !maxSpeed && !obsticle)
        {
            myRigBody.AddRelativeForce(Vector2.up * speed, ForceMode2D.Impulse);
        }
        else if (moving && maxSpeed)
        {
            myRigBody.velocity = new Vector2(myRigBody.velocity.x,
                                              myRigBody.velocity.y);
        }
        else if (!moving )
        {
			    myRigBody.velocity = new Vector2(0, 0);
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

    void GetInput()
    {
        left = Input.GetKey(KeyCode.LeftArrow) || Input.GetAxisRaw("Horizontal") < 0;
        right = Input.GetKey(KeyCode.RightArrow) || Input.GetAxisRaw("Horizontal") > 0;
        up = Input.GetKey(KeyCode.UpArrow) || Input.GetAxisRaw("Vertical") > 0;
        down = Input.GetKey(KeyCode.DownArrow) || Input.GetAxisRaw("Vertical") < 0;
        shot = Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1");
    }

    void Shot()
    {
        if ( shot )
            {
                Instantiate(bullet, transform.position, transform.rotation);
                SoundHandler.PlaySound(SoundHandler.shotSound);
        }
    }
}
