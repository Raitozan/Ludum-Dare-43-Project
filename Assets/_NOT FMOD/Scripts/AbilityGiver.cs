using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityGiver : MonoBehaviour {

	public int abilityToChange;
	public int abilityId;

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
		{
			if(abilityToChange == 1)
				collision.gameObject.GetComponent<Player>().ability1 = abilityId;
			else
				collision.gameObject.GetComponent<Player>().ability2 = abilityId;
			Destroy(gameObject);
		}
	}
}
