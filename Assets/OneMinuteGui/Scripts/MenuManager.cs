using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class MenuManager : MonoBehaviour 
{

	public InputField name;
	public InputField password;
	public GameObject StartMenu;


	[SerializeField]
	private string m_animationPropertyName;

	[SerializeField]
	private GameObject m_initialScreen;

	[SerializeField]
	private List<GameObject> m_navigationHistory;

	public void GoBack()
	{
		if (m_navigationHistory.Count > 1)
		{
			int index = m_navigationHistory.Count - 1;
			Animate(m_navigationHistory[index - 1], true);

			GameObject target = m_navigationHistory[index];
			m_navigationHistory.RemoveAt(index);
			Animate(target, false);
		}
	}
	 void Start()
	{
		if (PlayerPrefs.GetString ("Name").CompareTo ("")==0) {
			Debug.Log (PlayerPrefs.GetString ("Name"));
		} else {
			GoToMenu (StartMenu);
			Debug.Log (PlayerPrefs.GetString ("Name")+"er");
		};
	}

	public void GoToMenu(GameObject target)
	{
		if (target == null)
		{
			return;
		}

		if (m_navigationHistory.Count > 0)
		{
			Animate(m_navigationHistory[m_navigationHistory.Count - 1], false);
		}

		m_navigationHistory.Add(target);
		Animate(target, true);
	}

	private void Animate(GameObject target, bool direction)
	{
		if (target == null)
		{
			return;
		}

		target.SetActive(true);

		Canvas canvasComponent = target.GetComponent<Canvas>();
		if (canvasComponent != null)
		{
			canvasComponent.overrideSorting = true;
			canvasComponent.sortingOrder = m_navigationHistory.Count;
		}

		Animator animatorComponent = target.GetComponent<Animator>();
		if (animatorComponent != null)
		{
			animatorComponent.SetBool(m_animationPropertyName, direction);
		}
	}
	public void StartGame(string scenename)
	{	StartCoroutine(checkInternetConnection(scenename));
		
	
	}

	private IEnumerator checkInternetConnection(string scenename){
		WWW www = new WWW("http://google.com");
		yield return www;
		if (www.error != null) {
			Debug.Log ("Error Connect");
		} else {
			Debug.Log ("sceneName to load: " + scenename);
			SceneManager.LoadScene (scenename);
		
		}
	} 

	private void Awake()
	{
		m_navigationHistory = new List<GameObject>{m_initialScreen};
	}

	public void LoginMethod ()
	{
		if (name.text.CompareTo (null) == 0) {
			Debug.Log ("error idiot");

		} else {

			Debug.Log ("all ok");
			PlayerPrefs.SetString("Name",name.text);
			PlayerPrefs.SetString("Password",password.text);
			GoToMenu (StartMenu);
		}
	}

    public void LogOut()
    {
        PlayerPrefs.SetString("Name", null);
        PlayerPrefs.SetString("Password", null);
    }
}
