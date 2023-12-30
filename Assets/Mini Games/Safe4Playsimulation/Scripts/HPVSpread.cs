using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPVSpread : MonoBehaviour
{
    GameObject[] hpv;
    List<GameObject> inactiveHPV;
    AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        hpv = GameObject.FindGameObjectsWithTag("HPV");
        foreach (GameObject obj in hpv)
        {
            obj.SetActive(false);
        }

        inactiveHPV = new List<GameObject>(hpv);
    }

    public void NewInfection()
    {
        GameObject newHPV = inactiveHPV[Random.Range(0, inactiveHPV.Count)];

        newHPV.SetActive(true);

        inactiveHPV.Remove(newHPV);

        audioData = GetComponent<AudioSource>();
        audioData.Play();
    }
}
