using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{

    private BuildController _buildController;

    void Start()
    {
        _buildController = BuildController.getInstance();
    }
    
    
    
    // public void PurchaseTurret(GameObject _turret)
    // {
    //     Turret turret = _turret.GetComponent<Turret>();
    //     if (PlayerController.CurrentBalance >= turret.settings.cost)
    //     {
    //         _buildController.SetTurretToBuild(_turret);
    //    
    //         Debug.Log("turret was bought, cost + " + turret.settings.cost);
    //     }
    //     else
    //     {
    //         Debug.Log("Недостаточно средств на балансе");
    //     }
    // }
    
}
