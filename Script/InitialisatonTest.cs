using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// La classe a été fait par Chamsi Ben Kaabar et Chiheb Ben Jamaa
//Derniere modification le 24/04/2021
public class InitialisatonTest : MonoBehaviour
{

    public GameObject p1;

    public GameObject p2;

    public bool reproduction;

    public bool attaquer_manger;

    public bool fuite_chasser;

    public bool boire;

    public bool defense;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fuite_chasser)
        {
            InitialisationFuite_Chasser();
            fuite_chasser = false;
        }
        else if (boire)
        {
            InitialisationBoire();
            boire = false;
        }
        else if (attaquer_manger)
        {
            InitialisationAttaque_Manger();
            attaquer_manger = false;

        }
        else if (defense)
        {
            InitialisationDefense();
            defense = false;
        }
        else if (reproduction)
        {
            InitialisationReproduction();
            reproduction = false;
        }
        
    }

    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa.
     * 
     * Méthode qui permet d'initialiser l'état de reproduction pour faire un test unitaire
     * 
     * Derniere modification le 24/04/2021
     */
    public void InitialisationReproduction()
    {
       
        p1.GetComponent<Agent>().genre = 0;
        p2.GetComponent<Agent>().genre = 1;
        p1.GetComponent<Agent>().ageMaturation = 0;
        p2.GetComponent<Agent>().ageMaturation = 0;
        p1.GetComponent<Agent>().vitesse = 2;
        p2.GetComponent<Agent>().vitesse = 2;
        p1.GetComponent<Agent>().frequenceReproduction = 0;
        p2.GetComponent<Agent>().frequenceReproduction = 0;

    }


    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa.
     * 
     * Méthode qui permet d'initialiser l'état de fuite et de chasse pour faire un test unitaire
     * 
     * Derniere modification le 24/04/2021
     */
    public void InitialisationFuite_Chasser()
    {
        p1.GetComponent<Agent>().portee = 50;
        p1.GetComponent<Agent>().faim = 1000;
        p1.GetComponent<Agent>().vitesseChasser = 3;
        p2.GetComponent<Agent>().vitesseChasser = 3.2f;
    }

    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa.
     * 
     * Méthode qui permet d'initialiser l'état de boire pour faire un test unitaire
     * 
     * Derniere modification le 24/04/2021
     */
    public void InitialisationBoire()
    {
        p1.GetComponent<Agent>().portee = 50;
        p1.GetComponent<Agent>().soif = 1000;
        p1.GetComponent<Agent>().vitesseChasser = 3;
        
    }

    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa.
     * 
     * Méthode qui permet d'initialiser l'état de defense pour faire un test unitaire
     * 
     * Derniere modification le 24/04/2021
     */
    public void InitialisationDefense()
    {
        p1.GetComponent<Agent>().portee = 50;
        p1.GetComponent<Agent>().faim = 1000;
        p1.GetComponent<Agent>().chance = 1;
        p2.GetComponent<Agent>().chance = 50000;
        p1.GetComponent<Agent>().ageMaturation = 0;
        p2.GetComponent<Agent>().ageMaturation = 0;
        p1.GetComponent<Agent>().vitesseChasser = 3;
        p2.GetComponent<Agent>().vitesseChasser = 2;
    }

    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa.
     * 
     * Méthode qui permet d'initialiser l'état attaquer et manger pour faire un test unitaire
     * 
     * Derniere modification le 24/04/2021
     */
    public void InitialisationAttaque_Manger()
    {
        p1.GetComponent<Agent>().portee = 50;
        p1.GetComponent<Agent>().faim = 1000;
        p1.GetComponent<Agent>().vitesseChasser = 3;
        p2.GetComponent<Agent>().vitesseChasser = 2;
    }
}
