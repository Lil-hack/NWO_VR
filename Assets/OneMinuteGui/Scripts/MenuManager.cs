using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;

public class MenuManager : MonoBehaviour 
{

	public InputField name;
	public InputField password;
	public GameObject StartMenu;
	public GameObject LoginMenu;
	public GameObject RegMenu;
	public GameObject Hero;
	public GameObject ErrorMenu;
	public GameObject ErrorShop;
	public GameObject BuyReady;
	public Text ErrorText;
	public Text ErrorShopText;
	public GetStats getStats;
	public GameObject heroIntro;

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
		if (PlayerPrefs.GetString ("uuid").CompareTo ("")==0) {
			m_navigationHistory = new List<GameObject>{m_initialScreen};
			heroIntro.SetActive (true);
			GoToMenu (m_initialScreen);
		} else {
			m_navigationHistory = new List<GameObject>{StartMenu};
			heroIntro.SetActive (false);
			GoToMenu (StartMenu);
			Debug.Log (PlayerPrefs.GetString ("uuid"));
		};
	}

	public void GoToMenu(GameObject target)
	{
		if (target == null)
		{
			return;
		}
		if(target==StartMenu)
		{
			Hero.SetActive (true);
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

	{	
		getStats.GetDataForStart (scenename);
	
		
	
	}



	private void Awake()
	{
		

	}

	public void LoginMethod ()
	{
		if (name.text.CompareTo (null) == 0) {
			Debug.Log ("error idiot");

		} else {

			Debug.Log ("all ok");

			GoToMenu (StartMenu);
		}
	}

    public void LogOut()
	{	
		PlayerPrefs.DeleteAll ();
		StartMenu.SetActive(false);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
	public void ErrorClose()
	{
		ErrorMenu.SetActive (false);
		Hero.SetActive (true);
	}
	public void RegClose()
	{
		RegMenu.SetActive (false);
	}
	public void LoginClose()
	{
		LoginMenu.SetActive (false);
	}
}
