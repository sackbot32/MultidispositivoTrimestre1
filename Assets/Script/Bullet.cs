using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    private float timeSpent;
    public int damage;

    private void Awake()
    {
        timeSpent = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            timeSpent += Time.deltaTime;
        }
        transform.position += transform.forward * speed * Time.deltaTime;
        if(timeSpent >= lifeTime)
        {
            timeSpent = 0;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<EnemyHealth>() != null)
        {
            timeSpent = 0;
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}
