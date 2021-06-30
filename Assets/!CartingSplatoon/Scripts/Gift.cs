using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : MonoBehaviour
{
    public int giftIndex;

	private void Start()
	{
		giftIndex = Random.Range(1, 4);
	}
}
