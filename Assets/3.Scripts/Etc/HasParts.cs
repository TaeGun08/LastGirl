using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HasParts : MonoBehaviour
{
    public enum PartType
    {
        Unknown,
        Arm,
        Leg,
        Body,
        Head,
    }

    [System.Serializable]
    public class PartsColliders
    {
        public Collider[] HeadHitColliders;
        public Collider[] BodyHitColliders;
        public Collider[] LegHitColliders;
        public Collider[] ArmHitColliders;
    }

    [Header("Parts Settings")] 
    [SerializeField] protected PartsColliders Parts;
    protected Dictionary<Collider, PartType> partsDic = new Dictionary<Collider, PartType>();

    protected void Start()
    {
        foreach (Collider hitCollider in Parts.HeadHitColliders)
            partsDic.Add(hitCollider,  PartType.Head);
        
        foreach (Collider hitCollider in Parts.BodyHitColliders)
            partsDic.Add(hitCollider,  PartType.Body);
        
        foreach (Collider hitCollider in Parts.LegHitColliders)
            partsDic.Add(hitCollider,  PartType.Leg);
        
        foreach (Collider hitCollider in Parts.ArmHitColliders)
            partsDic.Add(hitCollider,  PartType.Arm);
    }

    public PartType GetPartsType(Collider collider)
    {
        return partsDic.GetValueOrDefault(collider, PartType.Unknown);
    } 
}
