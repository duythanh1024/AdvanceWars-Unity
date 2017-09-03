using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour {
	void Start () {
		
	}
	void Update () {
		
	}
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void CreateMap()
    {
        SceneManager.LoadScene(2);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
