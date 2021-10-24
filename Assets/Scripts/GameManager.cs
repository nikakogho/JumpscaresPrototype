using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public Behaviour[] toDisable;
	public GameObject zombie, winUI;

	public void Restart()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}

	public void Exit()
	{
		Application.Quit ();
	}

	void Update()
	{
		if (zombie == null && !winUI.activeSelf) 
		{
			foreach (Behaviour thing in toDisable) 
			{
				thing.enabled = false;
			}
			Cursor.lockState = CursorLockMode.None;
			winUI.SetActive (true);
		}
	}
}
