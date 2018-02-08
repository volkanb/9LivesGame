using UnityEngine;
using System.Collections;

public class RapierDamageCollider : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
 
		if(other.gameObject.tag == "Enemy"){

			Enemy enemyVariables = other.GetComponent<Enemy> ();

			if(enemyVariables.canReceiveDamage){
				if(!enemyVariables.invulnerable ){
					
					enemyVariables.life -= 1;
					enemyVariables.receivedDamage = true;
					 
				}
			} else{
				PussInBoots player = FindObjectOfType<PussInBoots>();

			}
		}
	}
}
