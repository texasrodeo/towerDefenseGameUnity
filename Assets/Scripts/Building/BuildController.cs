using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BuildController : MonoBehaviour
{

    public CellUI cellUi;
    
    private static BuildController instance;

    private GameObject turretToBuild;

    private Turret currentTurretSettings;

    private Vector3 positionOffsetToBuild;

    private Cell selectedCell;

    private GameObject phantomTurret;

    public static BuildController getInstance()
    {
        return instance;
    }

    public bool IsReadyToBuild()
    {
        return getInstance().turretToBuild != null;
    }

    public bool HasEnoughMoneyToBuild()
    {
        return PlayerController.CurrentBalance >= getInstance().currentTurretSettings.settings.cost;
    }

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }

        instance = this;
    }

    public void SetSelectedCell(Cell cell)
    {
        if (selectedCell == cell)
        {
            DeselectCell();
        }
        else
        {
            selectedCell = cell;
            turretToBuild = null;
            cellUi.SetTarget(cell);
        }
    }

    public void DeselectCell()
    {
        cellUi.UnsetTarget();
        selectedCell = null;
    }
    
    public void SetTurretToBuild(GameObject turret)
    {

        turretToBuild = turret;
        selectedCell = null;
        cellUi.UnsetTarget();
        
        currentTurretSettings =  turret.GetComponent<Turret>();
        positionOffsetToBuild = currentTurretSettings.settings.buildingOffset;
    }

    public void UnsetTurretToBuild()
    {
        turretToBuild = null;
    }

    public GameObject Build(Transform cell)
    {
        if (turretToBuild != null)
        {
            if (PlayerController.CurrentBalance >= currentTurretSettings.settings.cost)
            {
                PlayerController.getInstance().SpendMoney(currentTurretSettings.settings.cost);
                return Instantiate(this.turretToBuild, cell.position+positionOffsetToBuild, cell.rotation);
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }

    }
}
