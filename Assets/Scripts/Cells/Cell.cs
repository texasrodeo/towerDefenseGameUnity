using System;
using System.Collections;
using System.Collections.Generic;
using Field.Settings;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour
{
    public CellSettings settings;

    private Renderer _renderer;

    private Color startColor;

    private GameObject turret;

    private BuildController _buildController;

    public GameObject sellEffect;
    
    // Start is called before the first frame update
    void Start()
    {
        this._renderer = GetComponent<Renderer>();
        this.startColor = _renderer.material.color;
        this._buildController = BuildController.getInstance();
    }

    public Vector3 getBuildPosition()
    {
        return transform.position + settings.buildOffsetPosition;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (turret != null)
        {
            Debug.Log("Нажали на турель");
            _buildController.SetSelectedCell(this);
            return;
        }
        else
        {
            turret = _buildController.Build(transform);
        }
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!_buildController.IsReadyToBuild())
        {
            return;
        } 
        if (_buildController.HasEnoughMoneyToBuild())
        {
            _renderer.material.color = settings.hoverColor;
        }
        else
        {
            _renderer.material.color = settings.notEnoughMoneyColor;
        }
       
    }

    private void OnMouseExit()
    {
        _renderer.material.color = startColor;
    }
    
    void OnMouseOver () {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Turret was unset");
            _buildController.UnsetTurretToBuild();
            _renderer.material.color = startColor;
        }
    }

    public Turret GetTurret()
    {
        return turret.GetComponent<Turret>();
    }

    public void SellTurret()
    {
        PlayerController.getInstance().RecieveMoney(GetTurret().GetSellReward());
        
        GameObject effect = Instantiate(sellEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        
        Destroy(turret);
        turret = null;
        _buildController.DeselectCell();
    }

}
