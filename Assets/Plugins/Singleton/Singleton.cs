using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

	public static T Instance;

	protected virtual void Awake()
	{
		// Only one instance of debug console is allowed
		if( !Instance )
		{
			Instance = GetComponent<T>();

			// If it is a singleton object, don't destroy it between scene changes
			DontDestroyOnLoad( gameObject );
		}
		else if( Instance != this )
		{
			Destroy( gameObject );
			return;
		}
	}
}