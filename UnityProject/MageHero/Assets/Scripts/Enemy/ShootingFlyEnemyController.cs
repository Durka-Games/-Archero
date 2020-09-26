using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class ShootingFlyEnemyController : EnemyController
{

    protected ShootingFlyEnemyManager manager;

    void Start()
    {

        AI = GetComponent<NavMeshAgent>();
        manager = EnemyManager.GetComponent<ShootingFlyEnemyManager>();

        StartCoroutine(start());

    }

    IEnumerator start()
    {

        yield return new WaitUntil(() => manager.GetIsReady());
        manager.addEnemy(this, out XStartCell, out YStartCell, out delX, out delY, out widht, out height);

    }

    public override void Move(bool[,] RaycastMap, Tilemap MovableMap)
    {
        Vector3 position = MovableMap.WorldToCell(transform.position);

        float minCost = widht * widht + height * height;
        int minX = -1;
        int minY = -1;

        for (int x = 0; x < widht; x++)
        {

            for (int y = 0; y < height; y++)
            {

                if (RaycastMap[x, y])
                {

                    if (Vector3.Distance(position, new Vector3(x + XStartCell, y + YStartCell, 0)) < minCost)
                    {

                        minCost = Vector3.Distance(position, new Vector3(x + XStartCell, y + YStartCell, 0));
                        minX = x;
                        minY = y;

                    }

                }

            }
                        
        }

        if (minX != -1 && minY != -1)
        {

            Vector3Int moveVectorCells = new Vector3Int(minX + XStartCell, minY + YStartCell, 0);

            if (!moveVector.Equals(moveVectorCells))
            {

                moveVector = MovableMap.CellToWorld(moveVectorCells);

                moveVector = new Vector3(moveVector.x + delX / 2, moveVector.y, moveVector.z + delY / 2);

                AI.ResetPath();
                AI.SetDestination(moveVector);
                AI.isStopped = false;

            }

        }
        else { transform.Rotate(new Vector3(0, 45f * Time.deltaTime, 0)); }

    }

    public override bool raycast(Transform target)
    {

        Vector3 direction = target.position - transform.position;

        direction.y = 0;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit))
            if (hit.transform.Equals(target))
            {
                Debug.DrawRay(transform.position, direction.normalized * hit.distance, Color.green);
                AI.isStopped = true;
                return true;
            }
        Debug.DrawRay(transform.position, direction.normalized * hit.distance, Color.red);
        return false;

    }
}
