using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float radius;
    //variable que guarda la referencia al objeto que queremos spawnear
    public GameObject[] prefabs;
    public float[] chances;
    public float fullChance;
    //variable que guarda la referencia de las posiciones donde spawnerar los objetos
    public GameObject[] posiciones;

    //variables para limitar el spawneo de enemigos dentro del tablero
    // public float xMin, xMax, zMin, zMax;

    //variable para alejar la superficie de la esfera donde generaremos los enemigos
    //public float radio;

    //variables para especificar el tiempo minimo y maximo de aparicion de un enemigo
    public float tMin, tMax;

    //variable para calcular posicion de spawneo de los enemigos
    //Vector3 offset;

    private void Start()
    {
        foreach (float item in chances)
        {
            fullChance += item;
        }
        //Hago el siguiente proceso pa poner los spawners en un "circulo"
        posiciones = GameObject.FindGameObjectsWithTag("SpawnPoint");
        int posStep = 0;
        foreach (GameObject prefab in posiciones)
        {
            float circumferenceProgress = (float)posStep / posiciones.Length;
            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float zScaled = Mathf.Sin(currentRadian);
            float x = xScaled * radius;
            float z = zScaled * radius;
            Vector3 currentPos = new Vector3(x,0,z);
            prefab.transform.position = currentPos;
            posStep++;
        }
        //llamamos al metodo spawnenemy pasado un tiempo aleatorio entre tmin y tmax
        Invoke("SpawnEnemy", Random.Range(tMin, tMax));
    }

    void SpawnEnemy()
    {
        if (!GameManager.gm.requirement)
        {

        float pick = Random.value * fullChance;
        int chosenIndex = 0;
        float cumulativeWeight = chances[0];

            // Step through the list until we've accumulated more weight than this.
            // The length check is for safety in case rounding errors accumulate.
        while (pick > cumulativeWeight && chosenIndex < chances.Length - 1)
        {
            chosenIndex++;
            cumulativeWeight += chances[chosenIndex];
        }

        int posPrefabs = chosenIndex;
        int posRandom = Random.Range(0, posiciones.Length);


        //nos invocamos recursivamente
        Invoke("SpawnEnemy", Random.Range(tMin, tMax));

            //calculamos la posicion donde vamos a generar el nuevo enemigo
            //offset = Random.onUnitSphere * radio

            //instanciamos un objeto en la escena
            //instantiate( QUE, DONDE, HACIA DONDE MIRA);
            GameObject spawn = ObjectPooler.SharedInstance.GetPooledObject(prefabs[posPrefabs].tag);
            if (spawn != null)
            {
                spawn.transform.position = posiciones[posRandom].transform.position;
                spawn.SetActive(true);
            }
            //GameObject enemy = ObjectPooler.objectPoolerSharedInstance.GetPooledObject("Enemy");
            //if (enemy != null)
            //{
            //    enemy.transform.position = transform.position + offset;
            //    enemy.transform.rotation = enemy.transform.rotation;
            //    enemy.SetActive(true);
            //}
        }

    }
}