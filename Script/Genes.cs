using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genes : MonoBehaviour
{


    public float vitesseMax;
    public float vitesseMin;
    public List<float> genes = new List<float>();
    public Color[] peau;
    public float frequenceReproduction;
    public float ageM;


    public Genes(float vitesseMin, float vitesseMax, float frequenceReproductionMin, float frequenceReproductionMax, float ageMaturationMin, float ageMaturationMax, float besoinEnergMin, float besoinEnergMax, float esperenceDeVieMin, float esperenceDeVieMax, float besoinHydriqueMin, float besoinHydriqueMax)
    {
        genes.Add(Random.Range(vitesseMin, vitesseMax));
        genes.Add(Random.Range(0, 2));
        genes.Add(Random.Range(frequenceReproductionMin, frequenceReproductionMax));
        genes.Add(Random.Range(ageMaturationMin, ageMaturationMax));
        genes.Add(Random.Range(besoinEnergMin, besoinEnergMax));
        genes.Add(Random.Range(esperenceDeVieMin, esperenceDeVieMax));
        genes.Add(Random.Range(besoinHydriqueMin, besoinHydriqueMax));
        // genes.Add(peau[Random.Range(0, peau.Length)]);
    }

    public Genes()
    {

    }


    /**Fait par Chamsi Ben Kaabar et Chamsi Ben Kaabar
     * 
     * Méthode qui prend en paramètre une variable de type Genes, qui permet de faire un croissement génetique entre le male et la femelle du petit
     * pour lui attribuer les caractéristiques du gene du nouveau né.
     * p1 (Genes) : variable qui permet de stocker les informations du gene  
     * La méthode renvoie une liste de float qui stocke les valeurs des caractéristiques du nouveau né.
  
     * dernière modification: debut mars
     */
    public List<float> CroissementGenes(Genes p1)
    {


        List<float> tmp = new List<float>();
        int aleat = Random.Range(0, 2);
        //première méthode de crossing over, qui consiste que le nouveau né prend la 
        //première partie de la liste de la mère et la deuxieme partie de la liste du père afin de melanger les caractéristiques des parents
        if (aleat == 0)
        {

            for (int i = 0; i < genes.Count / 2; i++)
            {
                tmp.Add(genes[i]);
            }
            for (int i = p1.genes.Count / 2; i < p1.genes.Count; i++)
            {
                tmp.Add(p1.genes[i]);
            }


        }
        /*deuxieme méthode de crossing over, qui consiste que le nouveau ne prend les caractéristiques de la mère et du père en alternant à chaque
         * fois entre la caractéristique de la mère puis du père
         */
        else
        {
            for (int i = 0; i < genes.Count; i++)
            {
                if (i % 2 == 0)
                {
                    tmp.Add(p1.genes[i]);
                }
                else
                {
                    tmp.Add(genes[i]);
                }

            }
        }


        return tmp;
    }



}
