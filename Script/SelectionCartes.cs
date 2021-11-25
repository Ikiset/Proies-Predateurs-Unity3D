using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionCartes : MonoBehaviour
{

    public Terrain terrainActuel;
    public GameObject[] terrain = new GameObject[3];
     public Toggle[] selectTerrain = new Toggle[3];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    /**Fait par Chamsi Ben Kaabar et Chiheb Ben Jamaa
     * 
     * Méthode qui permet de selectionner le terrain de la simulation
     * 
     * dernière modification 15/04/2021
     */
    public void SelectTerrain()
    {
        for (int i = 0; i < terrain.Length; i++)
        {
            terrain[i].SetActive(selectTerrain[i].isOn);
        }

        terrainActuel = Terrain.activeTerrain;

    }
}
