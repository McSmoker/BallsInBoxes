using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Unit GetClosestUnit(List<Unit> units,Vector3 enemyPosition)
    {
        List<float> distances = new List<float>();
        List<Unit> positions = new List<Unit>();
        foreach (Unit unit in units)
        {
            distances.Add(Vector3.Distance(unit.transform.position, enemyPosition));
            positions.Add(unit);
        }
        int closestIndex = distances.IndexOf(distances.Min());
        return units[closestIndex];
    }
}
