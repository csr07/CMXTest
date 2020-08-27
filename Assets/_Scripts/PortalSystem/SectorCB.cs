using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SectorCB : MonoBehaviour
{
    private string sectorName;
    public GameObject portalsGrp;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject portalsGrp = GameObject.Find("Portals");        
        
        foreach (Transform t in portalsGrp.transform)
        {            
            CreatePortalTrigger(t);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreatePortalTrigger(Transform t)
    {
        GameObject portal = Instantiate(Resources.Load("PortalGO")) as GameObject;       

        portal.transform.localPosition = t.localPosition;
        portal.transform.rotation = t.rotation;
        portal.transform.parent = t;        

        portal.GetComponent<PortalTriggerCB>().currentSector = t.GetComponent<PortalSpawnPointCB>().currentSector;
        portal.GetComponent<PortalTriggerCB>().destinationSector = t.GetComponent<PortalSpawnPointCB>().destSector;

        //SectorManagerCB.Instance.currentSectorPortals.Add(portal);
    }
}
