using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    private int _score = 0;

    [SerializeField] 
    private int lives = 3;

    [SerializeField]
    private TextMeshProUGUI textPontos;
    
    [SerializeField]
    private TextMeshProUGUI textVidas;
    
    // Start is called before the first frame update
    void Start()
    {
        textVidas.text = "Vidas: " + lives;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore()
    {
        _score++;
        textPontos.text = "Pontos: " + _score;
    }

    public bool Death()
    {
        lives--;
        textVidas.text = "Vidas: " + lives;

        if (lives == 0)
        {
            GameOver();
            return true;
        }

        return false;
    }

    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
