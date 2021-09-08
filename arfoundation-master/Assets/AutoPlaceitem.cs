using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

/// <summary>
/// Listens for touch events and performs an AR raycast from the screen touch point.
/// AR raycasts will only hit detected trackables like feature points and planes.
///
/// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
/// and moved to the hit position.
/// </summary>
[RequireComponent(typeof(ARRaycastManager))]
public class AutoPlaceitem : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
  
   public float speed=2f;
   public bool isPlacing=false;
   public ItemPlacerConnection ItemPlacedController;
   public Material material;

    /// <summary>
    /// The prefab to instantiate on touch.
    /// </summary>
    void Awake()
    {
        m_RaycastManager = GetComponent<ARRaycastManager>();
    }

   
    void Update()
    {
        if(ItemPlacedController!=null){

        if (m_RaycastManager.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0)), s_Hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first one
            // will be the closest hit.
            var hitPose = s_Hits[0].pose;
         //   GameObjectToPlace.transform.position=hitPose.position;
         if(ItemPlacedController.hasItemBeenPlaces==false){

       
         isPlacing=true;
         ItemPlacedController.GetGameObjectToPlace().SetActive(true);
         ItemPlacedController.GetGameObjectToPlace().transform.parent = null;

              ItemPlacedController.GetGameObjectToPlace().transform.position=Vector3.Lerp( ItemPlacedController.GetGameObjectToPlace().transform.position,hitPose.position,Time.deltaTime*speed);
             //   ItemPlacedController.GetGameObjectToPlace().transform.rotation= hitPose.rotation;
             Vector3 CameraFlatPostion=new Vector3(Camera.main.transform.position.x,hitPose.position.y,Camera.main.transform.position.z);
                  ItemPlacedController.GetGameObjectToPlace().transform.LookAt(CameraFlatPostion);
         if (! ItemPlacedController.GetGameObjectToPlace().activeSelf){
             ItemPlacedController.GetGameObjectToPlace().SetActive(true);
         }
        }  }

        if (isPlacing==false && ItemPlacedController.hasItemBeenPlaces==false){

hideItem();
        }
        else{
            TapHasOccured();
        }
        isPlacing=false;
    }}


public void  TapHasOccured(){

  

    if (EventSystem.current.IsPointerOverGameObject() ||
     EventSystem.current.currentSelectedGameObject!=null){
        return;
    }
     if (Input.touchCount>0){

         Touch touch = Input.GetTouch(0);

        if (m_RaycastManager.Raycast(touch.position, s_Hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits are sorted by distance, so the first one
            // will be the closest hit.
            var hitPose = s_Hits[0].pose;
ItemPlacedController.hasItemBeenPlaces=true;
          setPlaneOn(false);
         }
}
}
public void hideItem(){
     if(ItemPlacedController!=null){

ItemPlacedController.GetGameObjectToPlace().SetActive(false);
ItemPlacedController.GetGameObjectToPlace().transform.parent=Camera.main.transform;
ItemPlacedController.GetGameObjectToPlace().transform.localPosition=Vector3.zero;
     }


}

public void setPlaneOn(bool isON){
    Color color = material.color;
    if(isON==true){
color.a=0.3f;
    }else{
color.a=0;
    }
    material.color=color;
}

    public void SetNewGameObjectToPlace(ItemPlacerConnection ItemPlacerController){
      shouldWeHideIt();
      //  GameObjectToPlace = newItem;
      this.ItemPlacedController=ItemPlacerController;
      setPlaneOn(true);


      
    }


public void shouldWeHideIt(){
     if(ItemPlacedController!=null){

    if (ItemPlacedController.hasItemBeenPlaces==false){
        hideItem();
    }}
}

public void removeItemToPlace(){
    ItemPlacedController=null;
     setPlaneOn(false);
      
}
    

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();

    ARRaycastManager m_RaycastManager;
}
