using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[System.Serializable]
public class GunChar
{
    public string name;
    public int damage;
    public float rate;
    public float lastTimeShot;
    public int bulletAmount;
    public float spread;
}

public class Gun : MonoBehaviour
{
    public InputActionAsset map;
    public Transform gunBarrel;
    public GunChar[] listOfGuns;
    public GameObject bullet;
    private int currentGun;
    public Text currentGunText;
    
    // Start is called before the first frame update
    void Start()
    {
        currentGun = 0;
        ChangeText();
    }

    // Update is called once per frame
    void Update()
    {
        if(map.FindAction("Shoot").IsPressed() && listOfGuns[currentGun].lastTimeShot >= listOfGuns[currentGun].rate && Time.timeScale > 0)
        {
            Shoot();
            listOfGuns[currentGun].lastTimeShot = 0;
        }

        Scroll(map.FindAction("Scroll").ReadValue<float>());

        if(map.FindAction("Number1").WasPressedThisFrame())
        {
            ChangeWeapon(0);
        }
        if(map.FindAction("Number2").WasPressedThisFrame())
        {
            ChangeWeapon(1);
        }
        if (map.FindAction("Number3").WasPressedThisFrame())
        {
            ChangeWeapon(2);
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
            GameObject spawn = ObjectPooler.SharedInstance.GetPooledObject("Bullet");
            if (spawn != null)
            {
                spawn.transform.position = gunBarrel.position + new Vector3(Random.Range(-listOfGuns[currentGun].spread, listOfGuns[currentGun].spread), Random.Range(-listOfGuns[currentGun].spread, listOfGuns[currentGun].spread), 0);
                spawn.transform.rotation = gunBarrel.rotation;
                spawn.GetComponent<Bullet>().damage = listOfGuns[currentGun].damage;
                spawn.SetActive(true);
            }
        }
    }

    private void Scroll(float scroll)
    {
        if(scroll > 0)
        {
            currentGun++;
            if(currentGun >= listOfGuns.Length)
            {
                currentGun = 0;
            }
        } 
        if(scroll < 0)
        {
            currentGun--;
            if (currentGun < 0)
            {
                currentGun = listOfGuns.Length-1;
            }
        }
        ChangeText();
    }

    private void ChangeWeapon(int value)
    {
        currentGun = value;
        ChangeText();
    }

    private void ChangeText()
    {
        currentGunText.text = "CurrentGun:" + listOfGuns[currentGun].name;
    }
}
