using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;
    private MeshRenderer mesh;
    public int damage;
    public bool titan;
    private Color baseColor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        baseColor = transform.GetChild(0).transform.GetComponent<MeshRenderer>().material.color;
        currentHealth = maxHealth;
        mesh = transform.GetChild(0).transform.GetComponent<MeshRenderer>();
        mesh.material.color = baseColor;
        if (titan)
        {
            GameObject[] posiciones = GameObject.FindGameObjectsWithTag("SpawnPoint");
            transform.position = posiciones[Random.Range(0,posiciones.Length)].transform.position;
        }
    }
    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
        float h = 0;
        float s = 0;
        float v = 0;
        Color.RGBToHSV(mesh.material.color,out h, out s,out v);
        mesh.material.color = Color.HSVToRGB(h,s * currentHealth/maxHealth,v);
        if (currentHealth < 0)
        {
            mesh.material.color = baseColor;
            gameObject.SetActive(false);
            if (!titan)
            {
                GameManager.gm.enemyDeath();

            }
            if(titan)
            {
                GameManager.gm.Victory();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>() != null)
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            if (!titan)
            {
                gameObject.SetActive(false);
            }
        }
    }

}
