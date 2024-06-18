using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

	public static T Instance;

	protected virtual void Awake()
	{
		if( !Instance )
		{
			Instance = GetComponent<T>();

			DontDestroyOnLoad( gameObject );
		}
		else if( Instance != this )
		{
			Destroy( gameObject );
			return;
		}
	}
}