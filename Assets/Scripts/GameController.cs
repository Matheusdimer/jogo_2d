using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    private int _score = 0;

    [SerializeField] 
    private int lives = 3;

    [SerializeField]
    private TextMeshProUGUI textPontos;
    
    [SerializeField]
    private TextMeshProUGUI textVidas;

    [SerializeField]
    private Alavanca alavancaScript;
    
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
        textPontos.text = "Pontos: " + _score + "/14";
        if (_score >= 14)
        { 
            alavancaScript.Show(true);
        }
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

    public void PushAlavanca()
    {
        alavancaScript.ChangeSprite();
    }

    public void EndGame()
    {
        SceneManager.LoadScene("Fim");
    }

    public Vector3 Checkpoint(GameObject checkpoint)
    {
        var checkpointScript = checkpoint.GetComponent<checkpoint>();
        return checkpointScript.Check();
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scenes/SampleScene");
    }
}
