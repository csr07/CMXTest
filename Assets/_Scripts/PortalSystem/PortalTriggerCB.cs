using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTriggerCB : MonoBehaviour
{
    private SectorManagerCB sectorMgr;
    public string currentSector;
    public string destinationSector;

    // Start is called before the first frame update
    void Start()
    {
        sectorMgr = FindObjectOfType<SectorManagerCB>();        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTrigger ENTER dest: " + destinationSector);
        sectorMgr.LoadSector(destinationSector);
        sectorMgr.UnloadSector(currentSector);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTrigger EXIT");
               
    }
}
