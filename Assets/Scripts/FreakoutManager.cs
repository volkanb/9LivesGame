using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FreakoutManager : MonoBehaviour {

	public List<GameObject> enemiesOnScreen;
	public SpriteRenderer mySpriteRenderer;
	public Animator myAnimator;
	public GameObject clawEffectPrefab;

	public Cat cat;

	void Start(){
		mySpriteRenderer = GetComponent<SpriteRenderer>();
		myAnimator = GetComponent<Animator>();
	}

	public void StartFreakout(Cat cat){
		myAnimator.SetBool("on",true);
		this.cat = cat;
		this.cat.GetComponent<SpriteRenderer>().enabled = false;
		this.cat.invulnerable = true;

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
