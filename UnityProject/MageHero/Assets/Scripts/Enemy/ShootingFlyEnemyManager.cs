using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingFlyEnemyManager : EnemyManager
{
    private void Update()
    {

        List<EnemyController> needRayCast = new List<EnemyController>();

        for (int i = 0; i < enemy.Count; i++) if (!enemy[i].raycast(player)) needRayCast.Add(enemy[i]);

        if(needRayCast.Count != 0)
        {

            FillRayCastMap();

            for (int i = 0; i < needRayCast.Count; i++) needRayCast[i].Move(isRayCast, MovableMap);

        }

    }

}
