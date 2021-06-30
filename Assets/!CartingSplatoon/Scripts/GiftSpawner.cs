using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiftSpawner : MonoBehaviour
{
    private Gift gift;
    [SerializeField]
    private GameObject giftPrefab;
    private float timer;
    [SerializeField]
    private float needTimeToSpawn = 10f;

	// Update is called once per frame
	private void Start()
	{
        gift = Instantiate(giftPrefab, transform.position, Quaternion.identity).GetComponent<Gift>();
    }

	void Update()
    {
        if (gift == null)
        {
            if (timer <= needTimeToSpawn)
            {
                timer += Time.deltaTime;
            }
            else
            {
                timer = 0;
                gift = Instantiate(giftPrefab, transform.position, Quaternion.identity).GetComponent<Gift>();
            }
        }
    }
}
