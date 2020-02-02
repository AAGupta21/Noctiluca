using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public ShipManager shipManager = null;
    public PlayerController player = null;
    public MusicManager am = null;

    public GameObject MainMenu_Panel = null;

    [SerializeField] private List<GameObject> ListOfObjects = null;
    [SerializeField] private GameObject ShipObject = null;
    [SerializeField] private GameObject PlayerObject_UnderWater = null;
    
    [SerializeField] private float BlackOutDuration = 0.25f;

    [SerializeField] private GameObject CameraObject = null;

    [SerializeField] private GameObject BlackOut_Img = null;
    [SerializeField] private GameObject MainMenu_BG = null;
    [SerializeField] private GameObject Menu_Panel = null;
    public GameObject Story_Panel = null;
    public GameObject Credits_Panel = null;

    [SerializeField] private GameObject InGameUI_Panel = null;
    [SerializeField] private GameObject Ocean_BG_Objects = null;
    [SerializeField] private GameObject SeaBed = null;

    [SerializeField] private GameObject WaterSurface = null;

    [SerializeField] private GameObject SurfaceWater_Canvas = null;
    [SerializeField] private GameObject SurfaceSprites = null;

    [SerializeField] private GameObject BluePrint_Solved_Sprites = null;
    [SerializeField] private GameObject BluePrint_Sprites = null;
    [SerializeField] private GameObject CheckList_Sprites = null;

    [SerializeField] private GameObject BluePrint_Btn = null;
    [SerializeField] private GameObject CheckList_Btn = null;
    
    [SerializeField] private Vector3 PlayerPosWhenDiving = Vector3.zero;
    [SerializeField] private Vector3 PlayerPosWhenOnSurface = Vector3.zero;

    public int Object_Cnt = 0;

    public GameObject EnterTheShip = null;
    public GameObject GetBackToBoatText = null;
    
    public static bool InWater = false;
    
    public void StartGame_Btn()
    {
        if(MainMenu_Panel.activeInHierarchy)
        {
            MainMenu_Panel.SetActive(false);
        }

        if(!Story_Panel.activeInHierarchy)
        {
            Story_Panel.SetActive(true);
        }
    }

    public void StartStory()
    {
        StartCoroutine(StartGame());
    }

    private void Start()
    {
        player.ObjectsCollected = new List<GameObject>();
        //StartCoroutine(StartGame());
    }

    private IEnumerator StartGame()
    {
        yield return BlackEffect();
        InShipLoad();
    }

    public void LoadMainMenu()
    {
        am.PlayMainMenuMusic();

        if(Credits_Panel.activeInHierarchy)
        {
            Credits_Panel.SetActive(false);
        }

        if(ShipObject.activeInHierarchy)
        {
            ShipObject.SetActive(false);
        }
        
        if (EnterTheShip.activeInHierarchy)
        {
            EnterTheShip.SetActive(false);
        }

        if (GetBackToBoatText.activeInHierarchy)
        {
            GetBackToBoatText.SetActive(false);
        }

        if (SurfaceSprites.activeInHierarchy)
        {
            SurfaceSprites.SetActive(false);
        }

        if (InGameUI_Panel.activeInHierarchy)
        {
            InGameUI_Panel.SetActive(false);
        }

        if (Ocean_BG_Objects.activeInHierarchy)
        {
            Ocean_BG_Objects.SetActive(false);
        }

        if (SeaBed.activeInHierarchy)
        {
            SeaBed.SetActive(false);
        }

        if (WaterSurface.activeInHierarchy)
        {
            WaterSurface.SetActive(false);
        }

        if(BluePrint_Btn.activeInHierarchy)
        {
            BluePrint_Btn.SetActive(false);
        }

        if (CheckList_Btn.activeInHierarchy)
        {
            CheckList_Btn.SetActive(false);
        }



        player.Stop_Overlay();

        Menu_Panel.SetActive(true);
        MainMenu_BG.SetActive(true);
        MainMenu_Panel.SetActive(true);
    }

    public void EndGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        Menu_Panel.SetActive(false);
        MainMenu_BG.SetActive(false);
        Credits_Panel.SetActive(true);
    }

    public void SwitchToBluePrints()
    {
        if(!BluePrint_Sprites.activeInHierarchy)
        {
            CheckList_Sprites.SetActive(false);
            BluePrint_Sprites.SetActive(true);
        }
    }

    public void SwitchToCheckList()
    {
        if(!CheckList_Sprites.activeInHierarchy)
        {
            BluePrint_Sprites.SetActive(false);
            CheckList_Sprites.SetActive(true);
        }
    }

    public void PlayClickSound()
    {
        am.PlayClickSound();
    }

    private void InShipLoad()
    {
        if(Story_Panel.activeInHierarchy)
        {
            Story_Panel.SetActive(false);
        }

        am.PlaySurfaceWaterMusic();

        StopCoroutine(RandomSoundUnderWater());

        if (!ShipObject.activeInHierarchy)
        {
            ShipObject.SetActive(true);
        }

        if (EnterTheShip.activeInHierarchy)
        {
            EnterTheShip.SetActive(false);
        }

        if (GetBackToBoatText.activeInHierarchy)
        {
            GetBackToBoatText.SetActive(false);
        }

        if (!SurfaceSprites.activeInHierarchy)
        {
            SurfaceSprites.SetActive(true);
        }

        if (!InGameUI_Panel.activeInHierarchy)
        {
            InGameUI_Panel.SetActive(true);
        }

        if (!BluePrint_Btn.activeInHierarchy)
        {
            BluePrint_Btn.SetActive(true);
        }

        if (!CheckList_Btn.activeInHierarchy)
        {
            CheckList_Btn.SetActive(true);
        }

        if (!Ocean_BG_Objects.activeInHierarchy)
        {
            Ocean_BG_Objects.SetActive(true);
        }

        if (!SeaBed.activeInHierarchy)
        {
            SeaBed.SetActive(true);
        }

        if (!WaterSurface.activeInHierarchy)
        {
            WaterSurface.SetActive(true);
        }

        player.Stop_Overlay();

        PlayerObject_UnderWater.SetActive(false);

        shipManager.EnablePlayerObject();
        shipManager.SetPosforSurface();

        WaterSurface.gameObject.transform.position = transform.GetChild(0).position;
        WaterSurface.gameObject.transform.rotation = transform.GetChild(0).rotation;

        player.gameObject.transform.position = PlayerPosWhenOnSurface;
        
        SurfaceWater_Canvas.SetActive(true);
        
        if(player.IsCarryingObject)
        {
            CheckList_Sprites.transform.GetChild(Object_Cnt).gameObject.SetActive(true);

            if (Object_Cnt < 6)
            {
                Object_Cnt++;
            }
            else
            {
                SceneManager.LoadScene(1);
            }

            player.IsCarryingObject = false;

            if(Object_Cnt > 1)
            {
                BluePrint_Sprites.transform.GetChild(Object_Cnt - 2).gameObject.SetActive(false);
                BluePrint_Solved_Sprites.transform.GetChild(Object_Cnt - 2).gameObject.SetActive(true);
            }
        }

        InWater = false;
    }

    private void InWaterLoad()
    {
        am.PlayUnderWaterMusic();

        if (SurfaceSprites.activeInHierarchy)
        {
            SurfaceSprites.SetActive(false);
        }

        if (BluePrint_Btn.activeInHierarchy)
        {
            BluePrint_Btn.SetActive(false);
        }

        if (CheckList_Btn.activeInHierarchy)
        {
            CheckList_Btn.SetActive(false);
        }

        PlayerObject_UnderWater.SetActive(true);
        player.gameObject.transform.position = PlayerPosWhenDiving;

        shipManager.DisablePlayerObject();

        SurfaceWater_Canvas.SetActive(false);
        
        WaterSurface.gameObject.transform.position = transform.GetChild(1).position;
        WaterSurface.gameObject.transform.rotation = transform.GetChild(1).rotation;

        if(!ListOfObjects[Object_Cnt].activeInHierarchy)
        {
            ListOfObjects[Object_Cnt].SetActive(true);
        }

        player.Initiate_overlay();

        InWater = true;
        shipManager.SetPosforUnderWater();

        StartCoroutine(RandomSoundUnderWater());
    }

    private IEnumerator RandomSoundUnderWater()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(6f, 10f));

            am.PlayBubbleSound();

        }
    }

    public void LoadGetonBoatText()
    {
        if (!EnterTheShip.activeInHierarchy)
        {
            EnterTheShip.SetActive(true);
        }
    }

    public void LoadGetBackToBoatText()
    {
        if(!GetBackToBoatText.activeInHierarchy)
        {
            GetBackToBoatText.gameObject.transform.position = player.gameObject.transform.position + Vector3.right * 5f;

            GetBackToBoatText.SetActive(true);
        }
    }

    private IEnumerator DisableGetBackToBoat()
    {
        yield return new WaitForSeconds(5f);
        GetBackToBoatText.SetActive(false);
    }

    public void GoInWater_Func()
    {
        StartCoroutine(GoInWater());
    }
    
    private IEnumerator GoInWater()
    {
        yield return BlackEffect();
        InWaterLoad();
    }

    public void GetOutOfWater_Func()
    {
        StartCoroutine(GetOutOfWater());
    }

    private IEnumerator GetOutOfWater()
    {
        yield return BlackEffect();
        InShipLoad();
    }

    private IEnumerator BlackEffect()
    {
        BlackOut_Img.SetActive(true);

        Color col = BlackOut_Img.GetComponent<Image>().color;
        Color camCol = CameraObject.GetComponent<Camera>().backgroundColor;

        float t = 0f;

        while(t < BlackOutDuration)
        {
            camCol.a = col.a = Mathf.Lerp(0f, 1f, t / BlackOutDuration);
            BlackOut_Img.GetComponent<Image>().color = col;
            CameraObject.GetComponent<Camera>().backgroundColor = camCol;
            yield return new WaitForSeconds(0.1f);
            t += 0.1f;
        }

        col.a = 1f;
        BlackOut_Img.GetComponent<Image>().color = col;
        CameraObject.GetComponent<Camera>().backgroundColor = camCol;

        StartCoroutine(BlackOutEffect());

        if(MainMenu_Panel.activeInHierarchy)
        {
            MainMenu_Panel.SetActive(false);
        }
    }
    
    private IEnumerator BlackOutEffect()
    {
        BlackOut_Img.SetActive(true);

        Color col = BlackOut_Img.GetComponent<Image>().color;
        Color camCol = CameraObject.GetComponent<Camera>().backgroundColor;

        float t = 0f;

        while (t < BlackOutDuration)
        {
            camCol.a = col.a = Mathf.Lerp(1f, 0f, t / BlackOutDuration);
            BlackOut_Img.GetComponent<Image>().color = col;
            CameraObject.GetComponent<Camera>().backgroundColor = camCol;

            yield return new WaitForSeconds(0.1f);
            t += 0.1f;
        }

        col.a = 0f;
        BlackOut_Img.GetComponent<Image>().color = col;
        CameraObject.GetComponent<Camera>().backgroundColor = camCol;

        BlackOut_Img.SetActive(false);
    }
}