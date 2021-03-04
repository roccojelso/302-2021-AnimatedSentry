using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUI : MonoBehaviour
{

    public GameObject uiText;

    // Start is called before the first frame update
    void Start()
    {
        uiText.SetActive(false);
    }

    private void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.tag == "Enemy")
        {
            uiText.SetActive(true);
            StartCoroutine("WaitForSec");
        }
    }
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(4);
        Destroy(uiText);
        Destroy(gameObject);

    }

}
