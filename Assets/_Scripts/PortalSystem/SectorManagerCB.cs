using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SectorManagerCB : MonoBehaviour
{
    public static SectorManagerCB Instance;

    //public List<GameObject> currentSectorPortals;

    public string currentSector;    

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //currentSectorPortals = new List<GameObject>();
        SceneManager.LoadScene("SectorA", LoadSceneMode.Additive);
        currentSector = "SectorA";        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSector(string sectorName)//SectorCB nextSector)
    {        
        SceneManager.LoadScene(sectorName, LoadSceneMode.Additive);
    }

    public void UnloadSector(string sectorName)
    {
        SceneManager.UnloadSceneAsync(sectorName);
    }

    //public void DestroyCurrentSectorPortals()
    //{
    //    foreach (GameObject obj in currentSectorPortals)
    //    {
    //        currentSectorPortals.Remove(obj);
    //        Destroy(obj);
    //    }
    //}
}
