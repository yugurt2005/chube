using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathSceneManager : MonoBehaviour
{    
    public void loadScene(string name) {
        SceneManager.LoadScene(name);
    }
}
