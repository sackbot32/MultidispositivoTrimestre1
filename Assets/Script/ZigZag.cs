using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZigZag : MonoBehaviour
{
    public float rotateSpeed;
    Transform player;
    NavMeshAgent agent;
    Rigidbody rb;
    public int zigZagging;
    public float timeBetweenZag;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > timeBetweenZag)
        {
            time = 0;
            zigZagging++;
            if(zigZagging > 3)
            {
                zigZagging = 0;
            }
            switch (zigZagging)
            {
                case 0:
                    agent.SetDestination(player.position);
                    transform.LookAt(player.position);
                    break;
                case 1:
                    agent.SetDestination(transform.position + transform.right.normalized * 5);
                    transform.LookAt(transform.position + transform.right.normalized * 5);
                    break;
                case 2:
                    agent.SetDestination(player.position);
                    transform.LookAt(player.position);
                    break;
                case 3:
                    agent.SetDestination(transform.position + transform.right.normalized * -5);
                    transform.LookAt(transform.position + transform.right.normalized * -5);
                    break;
                default:
                    break;
            }
        }



        if (Time.timeScale > 0)
        {
            transform.GetChild(0).transform.Rotate(rotateSpeed, 0, 0);
        }

    }
}
