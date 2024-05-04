using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour
{
    public void changeScene(string name)
    {
        SceneManager.LoadScene(name);
    }
}
