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
    public bool isEnterChapter = false;
	[SerializeField]
	private CanvasGroup _coverMainMenu;
	[SerializeField]
	private bool isESCMenu;
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

		if (!isESCMenu){
			GeneralUIManager.Instance.SetBlack();
			mainPageManager.EnterPage(0f);
			isEnterChapter = false;
			_coverMainMenu.blocksRaycasts = false;
		}
	}

    async void Start(){
		if (!isESCMenu){
        	await GeneralUIManager.Instance.FadeOutBlack(2f);           
		}
    }

	public void EnteringChapter(){
		isEnterChapter = true;
		_coverMainMenu.blocksRaycasts = true;
	}
}
