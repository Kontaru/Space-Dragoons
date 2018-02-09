using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

    //---------------------------------------------------------------------------
    //----- Entity: Serves as a tag system
    //---------------------------------------------------------------------------

    #region --- Entity Types Setter ---

    public enum Entities
    {
        None,
        Player,
        Enemy,
        Heavy,
        Light,
        Pickup
    }

    #endregion

    public Entities EntityType;
}
