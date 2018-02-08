using UnityEngine;
using System.Collections;

public class ParryCollider : MonoBehaviour {

	public PussInBoots player;

	void OnTriggerEnter2D(Collider2D other){
		if(other.gameObject.tag == "EnemyDamageCollider"){
			Enemy targetEnemy = other.gameObject.GetComponentInParent<Enemy> ();
			targetEnemy.prepareForParry = true;
			player.ParrySuccess(targetEnemy);
		}
	}
}
