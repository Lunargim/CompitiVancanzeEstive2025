using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndMenu : MonoBehaviour
{
    public static bool playerWon = false;
    public static bool enemyWon = false;

    public TextMeshProUGUI _text;

    public void Update()
    {
        if (playerWon)
        {
            _text.text = "Hai vinto!";
        }
        if (enemyWon)
        {
            _text.text = "Hai perso!";
        }
    }

}
