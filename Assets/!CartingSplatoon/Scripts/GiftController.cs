using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftController : MonoBehaviour
{
	[Header("RocketData")]
	[SerializeField]
	private GameObject rocketPrefab;
	[SerializeField]
	private Transform rocketParent;


	[Header("ShieldData")]
	[SerializeField]
	private GameObject shield;
	[SerializeField]
	private float needShieldTime = 5f;
	private float shieldTimer;


	[Header("BoostData")]
	[SerializeField]
	private VehicleBehaviour.WheelVehicle vehicle;
	[SerializeField]
	private float needBoostTime = 5f;
	private float boostTimer;


	[Header("BoostData")]
	[SerializeField]
	private Es.InkPainter.Sample.CollisionPainter roller;
	[SerializeField]
	private float needBigRollerTime = 5f;
	private float bigRollerTimer;



	[SerializeField]
	private int currentGift;
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Gift"))
		{
			currentGift = other.GetComponent<Gift>().giftIndex;
			Destroy(other.gameObject);
		}
	}


	private void CheckCurentGift()
	{
		switch (currentGift)
		{
			case 1:
				Instantiate(rocketPrefab, rocketParent.position, rocketParent.rotation, rocketParent);
				break;
			default:
				break;
		}
	}
}
