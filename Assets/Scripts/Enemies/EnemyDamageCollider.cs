using UnityEngine;
using System.Collections;

public class EnemyDamageCollider : MonoBehaviour {

	public int damage;

	void OnCollisionEnter2D(Collision2D other){
		if(other.gameObject.tag == "Player"){

			Cat cat = other.gameObject.GetComponent<Cat>();

			if(!cat.invulnerable){
				cat.life -= damage;
				cat.receivedDamage = true;
			} 
		 

		}
	}
 

}
