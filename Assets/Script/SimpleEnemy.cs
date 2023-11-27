using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemy : MonoBehaviour
{
    public float rotateSpeed;
    Transform player;
    NavMeshAgent agent;
    Rigidbody rb;
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
        if (Time.timeScale > 0)
        {
            transform.GetChild(0).transform.Rotate(rotateSpeed, 0, 0);
        }
        agent.SetDestination(player.position);
    }
}
