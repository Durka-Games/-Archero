using UnityEngine;
using System.Collections;

public class ShootingEnemyController : MonoBehaviour
{

    Behaviour halo; ////////////////////////////////////////////



    [SerializeField] private GameObject EnemyManager;
    private ShootingEnemyManager manager;

    // Start is called before the first frame update
    void Start()
    {
                
        halo = (Behaviour)GetComponent("Halo");/////////////////
        halo.enabled = false;///////////////////////////////////

        manager = EnemyManager.GetComponent<ShootingEnemyManager>();
        manager.addEnemy(this);

    }

    public bool raycast(Transform target)
    {

        Vector3 direction = target.position - transform.position;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit))
        {

            halo.enabled = !hit.transform.Equals(target);

            return hit.transform.Equals(target);

        }

        return false;

    }

    public void Move(bool[,] RaycastMap)
    {
        


    }

}
