using UnityEngine;
using System.Collections;

public class RapierDamageCollider : MonoBehaviour {

    public int cuts = 0;

    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Enemy")
        {

            Enemy enemyVariables = other.GetComponent<Enemy>();

            if (enemyVariables.canReceiveDamage)
            {
                if (!enemyVariables.invulnerable)
                {

                    enemyVariables.life -= 1;
                    enemyVariables.receivedDamage = true;

                }
            }
            else
            {
                PussInBoots player = FindObjectOfType<PussInBoots>();

            }
        }

        if (other.gameObject.tag == "Anchor")
        {

            int chandeliersCut = FindObjectOfType<ChandelierFall>().anchorsCut;

            chandeliersCut++;
            //cuts += 1;
            Destroy(other.gameObject);
        }
    }
}
