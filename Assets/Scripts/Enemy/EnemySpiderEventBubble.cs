using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpiderEventBubble : MonoBehaviour
{
    EnemySpider e;

    private void Start()
    {
        e = GetComponentInParent<EnemySpider>();
    }

    public void StartAttack()
    {
        e.StartAttack();
    }

    public void EndAttack()
    {
        e.EndAttack();
    }
}
