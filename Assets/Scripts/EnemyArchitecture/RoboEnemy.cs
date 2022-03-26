using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboEnemy : MonoBehaviour
{
    private Vector3 pos;
    [SerializeField] private float speed;
    private float angle=0;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(ChangeRotation());
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
    }
    private void Movement()
    {
        pos =Vector3.forward;
        transform.Translate(pos*speed*Time.deltaTime);
        anim.SetBool("isWalking", true);
    }
    IEnumerator ChangeRotation()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);
            angle += 90;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
        
    }
}
