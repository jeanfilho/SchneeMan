using UnityEngine;
using System.Collections;

public class MenuLoader : MonoBehaviour 
{
	public void LoadScene(int level)
	{
		Application.LoadLevel (level);
	}

}
