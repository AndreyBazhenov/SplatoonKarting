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
	private Rocket currentRocket;

	
	[Header("BoostData")]
	[SerializeField]
	private VehicleBehaviour.WheelVehicle vehicle;
	[SerializeField]
	private float needBoostTime = 5f;


	[Header("RollerData")]
	[SerializeField]
	private Es.InkPainter.Sample.CollisionPainter roller;
	[SerializeField]
	private float needBigRollerTime = 5f;
	[SerializeField]
	private float needBigRollerSizeMul = 1.5f;
	private float startSize;




	[SerializeField]
	private int currentGift;

	private void Start()
	{
		startSize = roller.GetSize();
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Gift"))
		{
			currentGift = other.GetComponent<Gift>().giftIndex;
			Destroy(other.gameObject);
			CheckCurentGift();
		}
	}


	private void CheckCurentGift()
	{
		switch (currentGift)
		{
			case 1:
				currentRocket = Instantiate(rocketPrefab, rocketParent.position, rocketParent.rotation, rocketParent).GetComponent<Rocket>();
				break;
			default:
				if (currentRocket)
				{
					Destroy(currentRocket.gameObject);
					currentRocket = null;
				}
				break;
		}
	}

	public void UseGift()
	{
		switch (currentGift)
		{
			case 0:
				Debug.LogError("Empty");
				break;
			case 1:
				currentRocket.isActivated = true;
				break;
			case 2:
				currentRocket.isActivated = true;
				break;
			case 3:
				currentRocket.isActivated = true;
				break;
			default:				
				break;
		}
		currentGift = 0;
	}

	IEnumerator StartUseBoost()
	{
		vehicle.boosting = false;
		roller.SetSize(startSize * needBigRollerSizeMul);
		yield return new WaitForSeconds()

	}

	IEnumerator StartUseRoller()
	{
		vehicle.boosting = false;
		
	}
}
