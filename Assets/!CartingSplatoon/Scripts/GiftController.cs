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

	private bool m_isAxisInUse;
	private IEnumerator coroutine;
	private void Start()
	{
		if(roller)
			startSize = roller.GetSize();
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Gift"))
		{
			currentGift = other.GetComponent<Gift>().giftIndex;
			Debug.LogError("CurGift  " + currentGift);
			Destroy(other.gameObject);
			CheckCurentGift();
		}
	}

	private void Update()
	{
		if ((Input.GetAxisRaw("Gift") + UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetAxisRaw("Gift")) != 0)
		{
			if (m_isAxisInUse == false)
			{
				UseGift();
				// Call your event function here.
				m_isAxisInUse = true;
			}
		}
		if ((Input.GetAxisRaw("Gift") + UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetAxisRaw("Gift")) == 0)
		{
			m_isAxisInUse = false;
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
		if (coroutine != null)
			StopCoroutine(coroutine);
		switch (currentGift)
		{
			case 0:
				Debug.LogError("Empty");
				break;
			case 1:
				currentRocket.isActivated = true;
				break;
			case 2:
				coroutine = StartUseBoost();
				StartCoroutine(coroutine);
				break;
			case 3:
				coroutine = StartUseRoller();
				StartCoroutine(coroutine);
				break;
			default:				
				break;
		}
		currentGift = 0;
	}

	IEnumerator StartUseBoost()
	{
		roller.SetSize(startSize);
		vehicle.boosting = true;
		yield return new WaitForSeconds(needBoostTime);
		vehicle.boosting = false;
	}

	IEnumerator StartUseRoller()
	{
		vehicle.boosting = false;
		roller.SetSize(startSize * needBigRollerSizeMul);
		yield return new WaitForSeconds(needBoostTime);

		roller.SetSize(startSize);
	}
}
