using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUIManager : Singleton<TitleUIManager>
{
    public MainPageManager mainPageManager;
    public LoadPageManager loadPageManager;
    public CreditPageManager creditPageManager;
	public BTSPageManager bTSPageManager;
	public GalleryPageManager galleryPageManager;
    // Tween 

    protected override void Awake()
	{
		if( !Instance )
		{
			Instance = this;
		}
		else if( Instance != this )
		{
			Destroy( gameObject );
			return;
		}

        GeneralUIManager.Instance.SetBlack();
        mainPageManager.EnterPage(0f);
	}

    async void Start(){
        await GeneralUIManager.Instance.FadeOutBlack(2f);           
    }
}
