using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody rb;
    public UDPReceive udpReceive;
    public float jumpforce;
    public float groundCheckDistance;
    private bool isGrounded = false;

    public GameObject playerDead;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, groundCheckDistance))
        {
            isGrounded = true;
        }

        else
        {
            isGrounded = false;
        }

        if (isGrounded)
        {
            //Recebe os dados via protocolo UDP
            string data = udpReceive.data;
            data = data.Remove(0, 1);
            data = data.Remove(data.Length - 1, 1);

            string[] points = data.Split(',');

            //Separa os dados em 21 pontos mapeados da mão
            for (int i = 0; i < 21; i++)
            {
                float x = 7 - float.Parse(points[i * 3]) / 175;
                float y = float.Parse(points[i * 3 + 1]) / 175;
                float z = float.Parse(points[i * 3 + 2]) / 175;

                /*if (x < 100)
                {
                    AdjustPositionAndRotation(new Vector3(0, 0, 0));
                    rb.AddForce(new Vector3(0, jumpforce, jumpforce));
                }*/
            }

            //Pega o ponto 0 da mão (punho) como referência
            if (float.TryParse(points[0], out float firstNumber))
            {
                //Se este ponto estiver entre 221 e 970 (pontos da captura na câmera), o personagem se move para frente
                if (firstNumber > 221 && firstNumber < 970)
                {
                    AdjustPositionAndRotation(new Vector3(0, 0, 0));
                    rb.AddForce(new Vector3(0, jumpforce, jumpforce));
                    print("pra frente");
                }

                //
                /*else if (firstNumber >= 540)
                {
                    AdjustPositionAndRotation(new Vector3(0, 180, 0));
                    rb.AddForce(new Vector3(0, jumpforce, -jumpforce));
                }*/

                //Se este ponto for menor ou igual a 221 (pontos da captura na câmera), o personagem se move para a esquerda
                else if (firstNumber <= 221)
                {
                    AdjustPositionAndRotation(new Vector3(0, -90, 0));
                    rb.AddForce(new Vector3(-jumpforce, jumpforce, 0));
                    print("pra esquerda");
                }

                //Se este ponto for maior ou igual a 970 (pontos da captura na câmera), o personagem se move para a direita
                else if (firstNumber >= 970)
                {
                    AdjustPositionAndRotation(new Vector3(0, 90, 0));
                    rb.AddForce(new Vector3(jumpforce, jumpforce, 0));
                    print("pra direita");
                }
            }
        }
    }

    //Ajusta a rotação do personagem
    void AdjustPositionAndRotation(Vector3 newRotation)
    {
        rb.velocity = Vector3.zero;
        transform.eulerAngles = newRotation;
        transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.Round(transform.position.z));
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StepTrigger"))
        {
            LevelManager.levelManager.SetSteps();
        }
        if (other.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }

    //Função de Game Over
    void GameOver()
    {
        Instantiate(playerDead, transform.position, transform.rotation);
        gameObject.SetActive(false);
        LevelManager.levelManager.GameOver();
    }
}
