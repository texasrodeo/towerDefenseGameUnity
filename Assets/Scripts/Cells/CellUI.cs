using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CellUI : MonoBehaviour
{
    private Cell target;

    public Vector3 buildingOffset;

    public GameObject ui;

    public Text uiText;

    private static string text = "Продать за $";

    public void SetTarget(Cell cell)
    {
        target = cell;

        transform.position = target.getBuildPosition() + buildingOffset;

        uiText.text = text + target.GetTurret().GetSellReward();
        
        ui.SetActive(true);
    }

    public void UnsetTarget()
    {
        ui.SetActive(false);
        target = null;
        
    }

    public void SellTurret()
    {
        target.SellTurret();
    }
}
