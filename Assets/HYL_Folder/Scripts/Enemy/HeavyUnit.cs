using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyUnit : Base {

    //---------------------------------------------------------------------------
    //----- Heavy Unit: Takes less damage, moves slower, but has
    //----- a spreadshot to remain lethality
    //---------------------------------------------------------------------------

    public override void FireBullet()
    {

        base.FireBullet();

        Instantiate(bulletPrefab, transform.position + transform.TransformDirection(new Vector3(0.5f, 0, 1.5F)), transform.rotation);

        Instantiate(bulletPrefab, transform.position + transform.TransformDirection(new Vector3(-0.5f, 0, 1.5F)), transform.rotation);
    }
}
