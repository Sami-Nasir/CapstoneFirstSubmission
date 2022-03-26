using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 20;
    public float jumpForce=10;
    private float horizontal;
    private float vertical;
    private Rigidbody rigidBody;
    private bool isGround=true;
    private bool atHeight = false;
    private Animator anim;
    private float Speed = 0;
    public float health=3;
    private float mouseX;
    private float angle=180;
    private float Score;
    private CharacterController player;
    public CapsuleCollider col;
    public LayerMask groundLayer;
    public Transform players;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        anim.SetFloat("Blend", 0f, 1f, Time.deltaTime);
        player = GetComponent<CharacterController>();
        Score = 0;
        //angle = players.transform.rotation.y;
    }
    // Update is called once per frame
    void Update()
    {
        AxisMovement();
        HandleRotation();
        Jump();
        //Rotation();

    }
    void AxisMovement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector3 mov = new Vector3(0, 0, vertical).normalized;
        if(horizontal ==0 && vertical == 0)
        {
            anim.SetFloat("Blend", 0f, 0.5f, Time.deltaTime);
        }
        player.Move(mov);
            //rigidBody.AddForce(Vector3.forward*vertical * speed);
            //rigidBody.AddForce(Vector3.left * horizontal * speed);
            transform.Translate(mov * speed * Time.deltaTime);
            //rigidBody.velocity = new Vector3(rigidBody.velocity.x, rigidBody.velocity.y, vertical * speed);
            Animation();
        //}
    }
    private bool isGrounded()
    {
        return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * 0.9f, groundLayer);
    }
    private void HandleRotation()
    {
      if (Input.GetKey(KeyCode.LeftArrow))
      {
         angle -= 5f;
         transform.rotation = Quaternion.Euler(0, angle, 0);
      }
      else if(Input.GetKey(KeyCode.RightArrow)){
         angle += 5;
         transform.rotation= Quaternion.Euler(0, angle, 0);
      }
    }
    private void Rotation()
    {
        mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(mouseX * speed * Vector3.up * Time.deltaTime, Space.Self);
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            anim.SetFloat("Blend", 1.95f);
            Vector3 pos = Vector3.up.normalized;
            rigidBody.AddForce(pos * jumpForce, ForceMode.Impulse);
            isGround = false;
            atHeight = true;
        }
    }
    
    private void Animation()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetFloat("Blend", 0.93f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Score += 1;
            Debug.Log(Score);
        }
        if (other.gameObject.CompareTag("PowerUp"))
        {
            health = 3;
        }
        if (other.gameObject.CompareTag("DoubleCoin"))
        {
            Score *= Score;
            Debug.Log(Score);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGround = true;
            atHeight = false;
        }
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Bullet"))
        {
            health -= 1;
            Debug.Log(health);
            if (health < 0)
            {
                anim.SetBool("isDead", true);
                StartCoroutine(Reload());
            }
        }
        if (collision.gameObject.CompareTag("Winner"))
        {
            if (Score >= 80)
            {
                Debug.Log("levelCompleted");
                Destroy(collision.gameObject);
                StartCoroutine(Reload());
            }
            else
            {
                Debug.Log("Not have Enough Coins");
            }
        }

    }
    IEnumerator Reload()
    {
        yield return new WaitForSeconds(4.0f);
        SceneManager.LoadScene(0);
    }
    private void Shoot()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            anim.SetBool("isShoot", true);
        }
    }
}
