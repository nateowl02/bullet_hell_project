using UnityEngine;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{
    public void GameStart()
    {
        SceneManager.LoadScene("game_scene");
    }

    public void Tutorial()
    {
        SceneManager.LoadScene("tutorial_scene");
    }
}
