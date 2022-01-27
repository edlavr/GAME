using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeSceneButton : MonoBehaviour
{
    public void LoadScene(int sceneNumber)
    {
        if (sceneNumber == 0)
        {
            Destroy(VolumeSlider.Instance.gameObject);
        }
        SceneManager.LoadScene(sceneNumber);
    }
}
