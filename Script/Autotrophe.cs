using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autotrophe : MonoBehaviour
{
    public Terrain ter;

    public float besoinEnerg;

    public float apportEnerg;

    public int age;

    public float jour;

    public float x;

    public float z;



    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.localScale = new Vector3(0.3f, 0.3f,0.3f);
        age = 0;
        jour = 0;
        apportEnerg = 2;
       /* x = Random.Range(0, ter.terrainData.size.x);//on lui donne des coordonnées aléatoire situées dans le terrain
        z = Random.Range(0, ter.terrainData.size.z);
        transform.position = new Vector3(x, 0, z);*/
    }

    // Update is called once per frame
    void Update()
    {
        jour += Time.deltaTime;
        if (jour > 30)
        {
            if (gameObject.transform.localScale.x <1)
            {
                apportEnerg += 2;
                gameObject.transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            }
            
            age++;
            jour = 0;

        }
    }

    
}
