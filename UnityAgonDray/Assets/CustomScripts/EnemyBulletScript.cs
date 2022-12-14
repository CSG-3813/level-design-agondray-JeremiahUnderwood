using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public GameObject sparkParticles;
    private Rigidbody rb;
    public float Speed = 100;

    // Start is called before the first frame update
    void Awake()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();
        rb.velocity = this.transform.forward * Speed;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, transform.forward, out hit, 0.5f, 1 << 8))
        {
            CollisionAction(hit.collider.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        CollisionAction(collision.collider.gameObject);
    }
    
    private void CollisionAction(GameObject collision)
    {
        if (collision.GetComponent<Rigidbody>())
        {
            collision.GetComponent<Rigidbody>().AddForce(rb.velocity, ForceMode.Impulse);
        }
        if (collision.GetComponent<NewPlayerController>())
        {
            collision.GetComponent<NewPlayerController>().health -= 25;
        }
        if (collision.GetComponent<EnemyAI>())
        {
            collision.GetComponent<EnemyAI>().HP -= 30;
            collision.GetComponent<EnemyAI>().currentState = EnemyAI.AIState.attacking;
        }
        Instantiate(sparkParticles, this.transform.position, this.transform.rotation);
        Destroy(this.gameObject);
    }
}
