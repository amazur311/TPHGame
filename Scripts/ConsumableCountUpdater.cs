using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConsumableCountUpdater : MonoBehaviour
{
    // Start is called before the first frame update

    public PlayerAttack playerAttack;
    public TextMeshProUGUI pistolAmmoCnt;
    public TextMeshProUGUI rifleAmmoCnt;
    public TextMeshProUGUI shotgunAmmoCnt;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //pistolAmmoCnt.SetText(""+ playerAttack.pistolTotal);
        //shotgunAmmoCnt.SetText("" + playerAttack.shotgunTotal);
    }
}
