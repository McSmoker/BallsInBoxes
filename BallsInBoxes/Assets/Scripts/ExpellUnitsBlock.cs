using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpellUnitsBlock : MonoBehaviour
{
    bool OneTime = true;
    private void Start()
    {
        StartCoroutine("KillYourself");
    }

    IEnumerator KillYourself()
    {
        while (OneTime)
        {
            OneTime = false;
            yield return new WaitForSeconds(1f);
        }
        Destroy(gameObject);
        yield return false;
    }
}
