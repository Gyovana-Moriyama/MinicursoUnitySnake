using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
   /// <summary>
   /// Essa classe serve para manejar os acontecimentos dos menus, tanto o inicial quanto o de final do jogo
   /// </summary>

   //Esse método serve para fazer o botão de "PLAY" funcionar, ela faz com que a cena do jogo seja carregada
   public void PlayGame()
   {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
   }

    //Esse método serve para que ao clicar no botão de "QUIT" o jogo feche
   public void QuitGame()
   {
       Debug.Log("QUIT");
       Application.Quit();
   }

    //Esse método serve para fazer o botão "MENU" funcionar, ela faz com que a cena do menu seja carregada
   public void BackMenu()
   {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
   }
}
