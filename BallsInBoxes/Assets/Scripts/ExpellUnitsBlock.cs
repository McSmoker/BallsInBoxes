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
            Debug.Log("countdownStart");
            yield return new WaitForSeconds(1f);
        }
        Destroy(gameObject);
        yield return null;
    }
}
