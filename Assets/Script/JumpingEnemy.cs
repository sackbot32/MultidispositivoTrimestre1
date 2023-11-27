using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JumpingEnemy : MonoBehaviour
{
    public float rotateSpeed;
    public float jumpForceUp;
    public float jumpForceForward;
    Transform player;
    NavMeshAgent agent;
    Rigidbody rb;
    public float timeBetweenJump;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(player.position);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(Time.timeScale > 0)
        {
            transform.GetChild(0).transform.Rotate(rotateSpeed, 0, 0);
        }
        if (time < timeBetweenJump)
        {
            if (agent.enabled)
            {  
                agent.SetDestination(player.position);
            } else
            {
                
            }
        }
        else
        {
            time = 0;
            agent.enabled = false;
            rb.AddForce(new Vector3(0, jumpForceUp, 0) + transform.forward * jumpForceForward);

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        agent.enabled = true;
    }
}
