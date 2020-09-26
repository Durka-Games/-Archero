using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ShootingEnemyManager : EnemyManager
{


    private void Update()
    {

        List<EnemyController> needRaycastMap = new List<EnemyController>();

        for (int i = 0; i < enemy.Count; i++) if (!enemy[i].raycast(player)) needRaycastMap.Add(enemy[i]);

        if (needRaycastMap.Count != 0)
        {
         
            FillRayCastMap();

            for (int i = 0; i < needRaycastMap.Count; i++) needRaycastMap[i].Move(isRayCast, MovableMap);

        }

    }
}
