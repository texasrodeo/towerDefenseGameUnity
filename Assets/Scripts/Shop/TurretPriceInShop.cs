using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretPriceInShop : MonoBehaviour
{
    public GameObject turret;

    public Text turretPrice;

    private static string dollarSign = "$";
    private void Awake()
    {
        turretPrice.text = dollarSign + turret.GetComponent<Turret>().settings.cost;
    }
}
