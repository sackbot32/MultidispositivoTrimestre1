using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class GunChar
{
    public int damage;
    public float rate;
    public float lastTimeShot;
    public int bulletAmount;
    public float spread;
}

public class Gun : MonoBehaviour
{
    public InputActionReference shoot;
    public Transform gunBarrel;
    public GunChar[] listOfGuns;
    public GameObject bullet;
    private int currentGun;
    
    // Start is called before the first frame update
    void Start()
    {
        currentGun = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(shoot.action.IsPressed() && listOfGuns[currentGun].lastTimeShot >= listOfGuns[currentGun].rate)
        {
            Shoot();
            listOfGuns[currentGun].lastTimeShot = 0;
        }
        foreach (GunChar item in listOfGuns)
        {
            item.lastTimeShot += Time.deltaTime;
        }
    }

    private void Shoot()
    {
        for (int i = 0; i < listOfGuns[currentGun].bulletAmount; i++)
        {
            Instantiate(bullet, gunBarrel.position + new Vector3(Random.Range(-listOfGuns[currentGun].spread, listOfGuns[currentGun].spread), Random.Range(-listOfGuns[currentGun].spread, listOfGuns[currentGun].spread), 0), gunBarrel.rotation).GetComponent<Bullet>().damage = listOfGuns[currentGun].damage;
        }
    }
}
