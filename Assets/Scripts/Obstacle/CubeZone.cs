using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeZone : MonoBehaviour
{
   public string ZoneName = "A";
   public DoorCubePuzzle door;

   private void OnTriggerEnter(Collider other)
   {
      if (other.GetComponent<CubePuzzle>() != null)
      {
         door.CubeEnter(ZoneName);
      }
   }

}
