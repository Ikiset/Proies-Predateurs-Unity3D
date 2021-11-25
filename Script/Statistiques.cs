using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Fait par Chamsi Ben Kaabar et Chiheb Ben Jamaa
//Dernière modification :23 / 04 / 2021
public class Statistiques : MonoBehaviour
{
    public int nbMortsTotal;
    public int nbEnvie;
    public int nbReproduction;
    public int nbPredateurs;
    public int maxNbAgent;
    public int nbProies;
    public Terrain ter;
    public List<double> proies;
    public List<double> predateurs;
    public List<double> lapin;
    public List<double> loup;
    public List<double> tempsProies;
    public List<double> tempsPredateurs;
    public bool estStart;
    public int nbProieDebut;
    public int nbPredateurDebut;

    public Text nbMortsTotalText;
    public Text nbEnvieText;
    public Text nbReproductionText;

    void Start()
    {
        if (!estStart)
        {

            nbMortsTotal = 0;
            lapin = new List<double>();
            loup = new List<double>();
            tempsProies = new List<double>();
            tempsPredateurs = new List<double>();
            predateurs = new List<double>();
            proies = new List<double>();
            //Lotka_Volterra(4, 10, 0, 50, 0.05f);
            tempsProies.Add(0);
            tempsPredateurs.Add(0);
            predateurs.Add(nbPredateurs);
            proies.Add(nbProies);
            estStart = true;
            maxNbAgent = 40;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (maxNbAgent < nbProies + nbPredateurs)
        {
            maxNbAgent = nbProies + nbPredateurs;
        }

    }




    /**Fait par chamsi Ben Kaabar et Chiheb Ben Jamaa
     * 
     * Méthode qui permet d'afficher les informations sur le nombre d'agents en vies, le nombre d'agents morts et le nombre de reproduction durant une simulation
 
     * Dernière modification :23/04/2021
     */
    public void AfficheInformation()
    {
        nbEnvieText.text = nbEnvie.ToString();
        nbMortsTotalText.text = nbMortsTotal.ToString();
        nbReproductionText.text = nbReproduction.ToString();
    }

    /**
     * Fait par Chamsi Ben Kaabar et Chiheb Ben Jamaa
     * 
     * Méthode qui permet de re-initialiser une variable après une simulation 
 
     *  Dernière modification :23/04/2021
     */
    public void Recommencer()
    {
        estStart = false;
    }

}
