using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    public int nbScene;

    public static bool pause;

    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && !pause)
        {
            Pause();
        }
            
    }

    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa
     * 
     * Méthode qui permet d'afficher le menu option.
     * scene (GameObject) : variable à afficher
     * 
     * Derniere modification le 03/04/2021
     */
    public void MenuOption(GameObject scene)
    {
        scene.SetActive(true);
        gameObject.SetActive(false);
        
    }

    /**Fait par Chamsi Ben kaabar et Chiheb Ben jamaa
     * 
     * Méthode qui permet de mettre la simulation en pause.
     * 
     * Derniere modification le 03/04/2021
     */
    public void Pause()
    {

        pause = !pause;
        if (pause)
        {
            Time.timeScale = 0;
        }                      
        panel.SetActive(pause);
        
    }

    /**
     * 
     * Méthode qui permet de remettre à 0 les attributs de la classe MenuPause.
     * 
     */
    public void Recommencer()
    {
        pause = false;
    }


}
