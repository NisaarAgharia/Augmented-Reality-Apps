using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARPlaneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    public Material[] materials;
    private int pos =0;

    public MeshRenderer currentGroundPlane;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

//Function for changing planes based on numbers and increments
    public void NextPlane(){
        pos=pos+1;
        if(pos>=materials.Length)
        {
            pos=0;
        }
      setPostTo(pos);
    }

    public void setPostTo(int newPosition){
        pos= newPosition;

          MeshRenderer[] renderers= transform.GetComponentsInChildren<MeshRenderer>();
        for(int i=0;i<renderers.Length;i++){
            renderers[i].material=materials[pos];
        }
        currentGroundPlane.material=materials[pos];

    }
}
