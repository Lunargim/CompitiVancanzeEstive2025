using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuStartGame : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("FightingScene");
    }
}
