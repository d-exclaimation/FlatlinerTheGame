using UnityEngine;
using UnityEngine.SceneManagement;

public class Proceed : MonoBehaviour {

    public void proceedToNextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
