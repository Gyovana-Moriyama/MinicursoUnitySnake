using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    /// <summary>
    /// Essa classe faz com que a comida seja spawnada na tela do jogo, dentro dos limites da mesma.
    /// </summary>

    //Prefab da comida para que essa possa ser instanciada
    public GameObject foodPrefab;

    //----Referências das bordas do jogo, servem como base para que a comida não seja spawnada fora da área visível no jogo.//
    public Transform borderLeft;
    public Transform borderRight;
    public Transform borderTop;
    public Transform borderBottom;
    //-----------------------------------------------------------------------------------------------------------------------//

    //Variáveis para controlar o tempo que vai demorar para spawnar outra comida "time" e a taxa com que isso ocorrerá "repeatRate".
    public int time, repeatRate;

    //No método Start, é utilizado o InvokeRepeating, uma função que faz com que um método seja chamado repetidademente nos intervalos passados
    //por parâmetro na função.
    void Start()
    {
        //Primeiro coloca o nome do método a ser chamado, depois o tempo e por último a taza de repetição.
        InvokeRepeating("Spawn", time, repeatRate);
    }
    
    /*Esse método controla o spawn de comida dentro do jogo. Nele são colocados os delimitadores de espaço, tomando como
    parâmetro as bordas, e com isso instanciando a comida no jogo.
     */
    void Spawn()
    {
        int x = (int)Random.Range(borderLeft.position.x, borderRight.position.x);
        int y = (int)Random.Range(borderBottom.position.y, borderTop.position.y);

        Instantiate(foodPrefab, new Vector2(x,y), Quaternion.identity);
    }

}
