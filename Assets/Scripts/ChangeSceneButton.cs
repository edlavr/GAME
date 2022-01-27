using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeSceneButton : MonoBehaviour
{
    public void LoadScene(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
