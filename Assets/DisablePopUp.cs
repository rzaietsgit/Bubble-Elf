using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisablePopUp : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
