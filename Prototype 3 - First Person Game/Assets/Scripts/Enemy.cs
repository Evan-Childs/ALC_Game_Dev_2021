using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class Enemy : MonoBehaviour
{

    [Header("Stats")]
    public int curHP;
    public int maxHP;
    public int score;
    public int scoreToGive;

    [Header("Movement")]
    public float moveSpeed;
    public float attackRange;
    public float yPathOffset;

    private List<Vector3> path;
    private Weapons weapon;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        //Gather the components
        weapon = GetComponent<Weapons>();
        target = FindObjectOfType<PlayerController>().gameObject;

        InvokeRepeating("UpdatePath", 0.0f, 0.5f);
    }

    void UpdatePath()
    {
        //Calculate path to the target
        NavMeshPath navMeshPath = new NavMeshPath();
        NavMesh.CalculatePath(transform.position, target.transform.position, NavMesh.AllAreas, navMeshPath);

        //Save path as list
        path = navMeshPath.corners.ToList();
    }

    void ChaseTarget()
    {
        if (path.Count == 0)
            return;
        //Move towards closest path
        transform.position = Vector3.MoveTowards(transform.position, path[0] + new Vector3(0, yPathOffset, 0), moveSpeed * Time.deltaTime);
        if(transform.position == path[0] + new Vector3(0, yPathOffset, 0))
            path.RemoveAt(0);
    }
    public void TakeDamage(int damage)
    {
        curHP -= damage;

        if(curHP <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        //look at target
        Vector3 dir = (target.transform.position - transform.position).normalized;
        float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;

        transform.eulerAngles = Vector3.up * angle;
        //Get distance from enemy to player/target
        float dist = Vector3.Distance(transform.position, target.transform.position);

        if(dist <= attackRange)
        {
            if(weapon.CanShoot())
            {
                weapon.Shoot();
            }
        }
        else
        {
            ChaseTarget();
        }
    }
}
