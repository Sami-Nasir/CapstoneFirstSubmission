using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] private float speed;
    private float angle=0;
    private float pos;
    private float offset = 0.15f;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
    }
    private void Rotate()
    {
        angle += 25f;
        transform.rotation = Quaternion.Euler(-90, 0, angle);
    }
    //IEnumerator Dance()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(0.5f);
    //        transform.Translate(0, 0,pos-offset);
    //        yield return new WaitForSeconds(0.5f);
    //        transform.Translate(0,0, pos+offset);
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
