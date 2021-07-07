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
	private PaintIn3D.P3dPaintSphere roller;
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
			startSize = roller.Radius;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (!vehicle.IsPlayer)
			return;
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
		if (vehicle.IsPlayer)
		{
			if ((Input.GetAxisRaw("Gift") + UnityStandardAssets.CrossPlatformInput.CrossPlatformInputManager.GetAxisRaw("Gift")) > 0.1)
			{
				if (m_isAxisInUse == false)
				{
					if(currentGift != 0)
						UseGift();
					// Call your event function here.
					m_isAxisInUse = true;
				}
			}
			else
			{
				m_isAxisInUse = false;
			}

			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				roller.Radius = 1;
			}
		}
	}

	private void CheckCurentGift()
	{
		if (currentRocket)
		{
			Destroy(currentRocket.gameObject);
			currentRocket = null;
		}
		switch (currentGift)
		{
			case 1:
				currentRocket = Instantiate(rocketPrefab, rocketParent.position, rocketParent.rotation, rocketParent).GetComponent<Rocket>();
				GameController.Instance.uiController.ShowGift(true, "Rocket");
				break;
			case 2:
				GameController.Instance.uiController.ShowGift(true, "Boost");
				break;
			case 3:
				GameController.Instance.uiController.ShowGift(true, "BrushSize");
				break;

			default:
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


		GameController.Instance.uiController.ShowGift(false, "");

		currentGift = 0;
	}

	IEnumerator StartUseBoost()
	{
		roller.Radius = startSize;
		vehicle.boosting = true;
		yield return new WaitForSeconds(needBoostTime);
		vehicle.boosting = false;
	}

	IEnumerator StartUseRoller()
	{
		vehicle.boosting = false;
		roller.Radius = startSize * needBigRollerSizeMul;
		yield return new WaitForSeconds(needBoostTime);

		roller.Radius = startSize;
	}
}
