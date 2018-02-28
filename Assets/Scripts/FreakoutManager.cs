tousing UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class FreakoutManager : MonoBehaviour {

	public List<GameObject> enemiesOnScreen;
	public SpriteRenderer mySpriteRenderer;
	public Animator myAnimator;
	public GameObject clawEffectPrefab;

	public Cat cat;

    public GameObject m_FBFiller;                                       // Placeholder for the filler in the level.
    public GameObject m_FBIcon;                                         // Placeholder for the icon in the level.
    public float m_percentage = 0.1f;                                   // The percentage of how much the bar will increase by.
    public bool ready = false;                                          // Checks if the freak out bar is ready.
    public float m_fillAmount = 0;                                          // 0 = empty, 1 = full.

    void Start(){
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		myAnimator = GetComponent<Animator>();
        m_FBFiller = GameObject.Find("FOB Filler");                     // Assigns the Filler to m_FBFiller.
        m_FBIcon = GameObject.Find("FOB Icon");                         // Assigns the Icon to m_FBicon.
        m_FBFiller.GetComponent<Image>().fillAmount = m_fillAmount;     // Grabs the current fill amount.
    }

    public void StartFreakout(Cat cat){
		myAnimator.SetBool("on",true);
		this.cat = cat;
		this.cat.GetComponent<SpriteRenderer>().enabled = false;
		this.cat.invulnerable = true;

        // Resets the fill amount.
        m_fillAmount = 0;
        UpdateFillAmount();
    }

    public void IncreaseFBBar()
    {
        m_fillAmount += m_percentage; // Increases the fill by a percentage.
        UpdateFillAmount();
    }

    public void UpdateFillAmount()
    {
        m_FBFiller.GetComponent<Image>().fillAmount = m_fillAmount;
    }

    public void StopFreakout(){
		myAnimator.SetBool("on",false);
		this.cat.freakoutMode = false;
		this.cat.invulnerable = false;
		this.cat.StopFreakout();
		this.cat.GetComponent<SpriteRenderer>().enabled = true;
		this.cat = null;
	}

	public void KillAll(){

		foreach(GameObject enemie in enemiesOnScreen){
			Destroy(enemie);
		}
		enemiesOnScreen.Clear();
	}

	public void SpawnClawEffect(){
		foreach(GameObject enemie in enemiesOnScreen){
			GameObject effect = null;
			if(enemie.GetComponentInChildren<Rigidbody2D>().transform != null){
			 	effect = (GameObject)Instantiate(clawEffectPrefab, enemie.GetComponentInChildren<Rigidbody2D>().transform);
			} else{
				effect = (GameObject)Instantiate(clawEffectPrefab, enemie.transform); 
			}

			effect.transform.localPosition = Vector3.zero;
		}
	}

	public void AddEnemie(GameObject enemie){
		enemiesOnScreen.Add(enemie);
	}

	public void RemoveEnemie(GameObject enemie){
		for(int i = 0; i < enemiesOnScreen.Count; i++){
			if(enemiesOnScreen[i].Equals(enemie)){
				enemiesOnScreen.RemoveAt(i);
				break;
			}
		}
	}
}
