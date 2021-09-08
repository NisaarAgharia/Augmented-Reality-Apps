using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacerConnection : MonoBehaviour
{
    public bool hasItemBeenPlaces= false;
    public GameObject ItemToSetIntoPlacer;
    public AutoPlaceitem PlacerScript;
    // Start is called before the first frame update
    void Start()
    {
        if (hasItemBeenPlaces==false){
            ItemToSetIntoPlacer.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ButtonClicked(){
        if (hasItemBeenPlaces==false){
            if(PlacerScript.ItemPlacedController!=this){
            PlacerScript.SetNewGameObjectToPlace(this);
            }
            else
            {
                putItemAway();
            }
           
        }else{

putItemAway();
        }
     
        
    }

    public GameObject GetGameObjectToPlace(){
        return ItemToSetIntoPlacer;
    }

    public void putItemAway(){
        PlacerScript.SetNewGameObjectToPlace(this);
        hasItemBeenPlaces=false;
        PlacerScript.hideItem();
         PlacerScript.removeItemToPlace();
    }
    }

