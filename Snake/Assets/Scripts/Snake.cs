using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
public class Snake : MonoBehaviour
{
    //Essa classe controla toda a movimentação da cobra

    //variável booleana que indica se a cobra comeu ou não
    bool ate = false;
    //variável booleana que indica se a cobra tocou em algo que não é comida e por isso morreu.
    bool isDead = false;
    //Referência para o prefab da cauda, para que esse possa ser instanciado
    public GameObject tailPrefab;
    //direção para a qual a cobra está se movendo
    Vector2 dir = Vector2.right;
    //lista que guarda os elementos da cauda da cobra
    List<Transform> tail = new List<Transform>();


    /*No método Start, é utilizado o InvokeRepeating, uma função que faz com que um método seja chamado repetidademente nos intervalos passados
    por parâmetro na função.
    */
    void Start()
    {
        InvokeRepeating("Move", 0.3f, 0.3f);
    }

    /*No método Update é onde ocorre a troca de direção da movimentação da cobra e a troca de cena caso ela tenha morrido
     */
    void Update()
    {
        if (isDead == false)
        {
            if (Input.GetKey(KeyCode.RightArrow)) //Seta direita foi apertada, então troca a direção para a direita
                dir = Vector2.right;
            else if (Input.GetKey(KeyCode.DownArrow)) //Seta para baixo foi apertada, então troca a direção para baixo
                dir = Vector2.down; 
            else if (Input.GetKey(KeyCode.LeftArrow)) //Seta para esquerda foi apertada, então troca a direção para a esquerda
                dir = Vector2.left;
            else if (Input.GetKey(KeyCode.UpArrow)) //Seta para cima foi apertada, então troca a direção para cima
                dir = Vector2.up;
        }
        else
        {
            //Se entrou nesse else, é porque a cobra morreu, então a cabeça dela é destruída (que é onde esse script está) e a cena de Lose é carregada
            Destroy(this.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    
    /*Esse método faz a movimentção da cobra e adiciona a cauda quando a cobra come alguma fruta.
     */
    private void Move()
    {
        if (isDead == false)
        {
            //A cobra se move na posição de dir, que é gerenciado no update, na troca de direções
            Vector2 v = transform.position;
            transform.Translate(dir);

            if (ate)
            {
                /*Se a cobra comeu, vamos inserir mais uma parte da cauda. Esta será inserida no lugar de v, que era onde a cabeça 
                estava antes de se movimentar, e agora é um espaço vazio. Ao instanciar o novo elemento da cauda, vamos também adicioná-lo
                em uma lista, para que possamos movimentá-lo.
                */
                GameObject g = (GameObject)Instantiate(tailPrefab, v, Quaternion.identity);
                tail.Insert(0, g.transform);
                ate = false;
            }
            /*Se a cobra não comeu, ocorrerá apenas a movimentação da cauda, que consiste em pegar o último elemento da lista e colocá-lo
            no lugar onde a cabeça se encontrava antes de se movimentar, isso faz com que a movimentação seja mais fácil do que ter que movimentar
            pedaço por pedaço da cauda.
                Ex:
                    oooox (inicial)
                    oooo x (cabeça se moveu)
                     oooox (movimentação do último elemento da cauda no lugar vazio)

                Para isso fazemos a troca de lugar da cauda dentro da lista que guarda os elementos da cauda, pegando o último elemento
                e colocando-o como primeiro elemento da lista.
             */
            else if (tail.Count > 0)
            {
                tail.Last().position = v;
                tail.Insert(0, tail.Last());
                tail.RemoveAt(tail.Count - 1);
            }
        }
    }
    
    /* Esse método é próprio do MonoBehaviour, e ele roda todo frame, independente de ter sido feita a sua chamada no Update ou no Start.
    Ele checa por colisões do tipo Trigger, entre o objeto em que esse script está e o objeto com que colidiu. Não necessariamente ambos os objetos
    precisam ter o Collider Trigger, se um deles tiver, já funciona.
    Nesse caso utilizamos esse método para checar com o que a cabeça da cobra colidiu. Se foi com uma comida, destruímos esse prefab da comida
    e marcamos ate pra true. Já se a cabeça da cobra colidiu com algo que não seja comida, quer dizer que ela deve morrer, então marcamos isDead
    pra true.
     */
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            ate = true;
            Destroy(other.gameObject);
        }
        else
        {
            isDead = true;
        }
    }
}
