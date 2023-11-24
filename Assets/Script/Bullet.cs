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
        timeSpent += Time.deltaTime;
        transform.position += transform.forward * speed * Time.deltaTime;
        if(timeSpent >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
