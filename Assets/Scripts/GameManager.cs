using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static Action<bool, Transform> canPickDebrisEvent;

    [Header("Pick Debris")]
    [SerializeField] int totalDebris;
    [SerializeField] int collectedDebris;
    [SerializeField] Transform truckDebrisPicker;
    [SerializeField] Button pickDebrisButton;
    [SerializeField] Transform currentDebris;

    [Header("Responser")]
    [SerializeField] TMP_Text totalDebrisText;
    [SerializeField] TMP_Text collectedDebrisText;
    [SerializeField] TMP_Text notificationText;

    private void Start()
    {
        totalDebrisText.SetText("Total Debris     = 0" + totalDebris);
        pickDebrisButton.onClick.AddListener(() => { StartCoroutine(DebrisPicker()); });
    }

    private void OnEnable()
    {
        canPickDebrisEvent += UpdatePickDebris;
    }

    private void OnDisable()
    {
        canPickDebrisEvent -= UpdatePickDebris;
    }

    void UpdatePickDebris(bool can, Transform debris)
    {
        if (debris != null)
        {
            currentDebris = debris; 
        }
        else
        {
            currentDebris = null;
        }

        pickDebrisButton.interactable = can;
    }

    IEnumerator DebrisPicker()
    {
        if (currentDebris != null)
        {
            collectedDebris++;
            currentDebris.gameObject.SetActive(false);
            currentDebris.transform.position = truckDebrisPicker.position;
            currentDebris.transform.rotation = truckDebrisPicker.rotation;
            Vector3 scale = currentDebris.transform.localScale / 2;
            currentDebris.transform.localScale = scale;
            currentDebris.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            currentDebris.GetChild(0).gameObject.SetActive(false);

            yield return new WaitForSeconds(0.2f);
            currentDebris.gameObject.SetActive(true);
            currentDebris = null;
            pickDebrisButton.interactable = false;
            collectedDebrisText.SetText("Collected Debris   = 0" + collectedDebris);

            yield return new WaitForSeconds(0.2f);

            if (totalDebris == collectedDebris)
            {
                notificationText.SetText("You Sucessfully Collected All Debris");
            }
        }
        else
        {
            Debug.LogError("Debris Missing!");
        }

    }
}
