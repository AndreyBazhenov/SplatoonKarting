using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float speed = 10f;
    public float damping = 5f;
    private Transform target;
    // Start is called before the first frame update
    public bool isActivated;

    private Renderer renderer;
    private Transform root;

    public GameObject explosionPrefab;

    void Start()
    {
        renderer = GetComponent<Renderer>(); 
        root = transform.root;
    }

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isActivated = true;
        }
	}

	private void OnCollisionEnter(Collision collision)
	{
        if (collision.transform.CompareTag("Car")&& collision.transform.root != root)
        {
            Debug.LogError("1");
            collision.transform.GetComponent<Rigidbody>().AddExplosionForce(2500000, transform.position, 50f, 3.0F);
            Destroy(gameObject);
        }
	}

	// Update is called once per frame
	void FixedUpdate()
    {
        if (isActivated)
        {
            if (transform.parent != null)
                transform.parent = null;
            transform.LookAt(target);
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
        }
        else
        {
            GetNearEnemy();

            if (target)
            {
                var lookPos = target.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
            }
        }
    }

    private void GetNearEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Target");

        float minDistance = 10000f;
        if (target)
            minDistance = Vector3.Distance(transform.position, target.transform.position);
        bool hasChanged= false;

        

		foreach (var enemy in enemies)
		{
            if (transform.root != enemy.transform.root)
            {
                if (Vector3.Distance(transform.position, enemy.transform.position) < minDistance)
                {
                    if (target != enemy.transform)
                        hasChanged = true;

                    target = enemy.transform;
                    minDistance = Vector3.Distance(transform.position, enemy.transform.position);
                }
            }
		}

        if(target && hasChanged)
            renderer.material.SetColor("_Color", target.root.GetComponentInChildren<Es.InkPainter.Sample.CollisionPainter>().GetColor());
    }

	private void OnDestroy()
	{
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
	}
}
