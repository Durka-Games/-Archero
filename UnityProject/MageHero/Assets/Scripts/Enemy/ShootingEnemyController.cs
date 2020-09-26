using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using System.IO;
using System;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ShootingEnemyController : EnemyController
{

    protected ShootingEnemyManager manager;

    // Start is called before the first frame update
    void Start()
    {

        AI = GetComponent<NavMeshAgent>();
        manager = EnemyManager.GetComponent<ShootingEnemyManager>();

        StartCoroutine(start());

    }

    IEnumerator start()
    {

        yield return new WaitUntil(() => manager.GetIsReady());
        manager.addEnemy(this, out XStartCell, out YStartCell, out delX, out delY, out widht, out height);

    }

    public override bool raycast(Transform target)
    {

        Vector3 direction = target.position - transform.position;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, direction, out hit))
            if (hit.transform.Equals(target))
            {
                AI.isStopped = true;
                return true;
            }
        return false;

    }

    public override void Move(bool[,] RaycastMap, Tilemap MovableMap)
    {

        Vector3 position = MovableMap.WorldToCell(transform.position);

        float minCost = widht * widht + height * height;
        int minX = 0;
        int minY = 0;

        for(int x = 0; x < widht; x++)
        {

            for (int y = 0; y < height; y++)
            {

                if(RaycastMap[x, y])
                {

                    if(Vector3.Distance(position, new Vector3(x + XStartCell, y + YStartCell, 0)) < minCost)
                    {

                        minCost = Vector3.Distance(position, new Vector3(x + XStartCell, y + YStartCell, 0));
                        minX = x;
                        minY = y;

                    }

                }

            }

        }


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

}
