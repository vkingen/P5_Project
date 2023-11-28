using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void LoadTiltScene()
    {
        Wait(5);
        SceneManager.LoadScene("Pseudotilbrush");
    }

    // Update is called once per frame
    public void LoadMMA()
    {
        Wait(5);
        SceneManager.LoadScene("MMA VR Workshop");
    }

    private IEnumerator Wait(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);

    }
}
