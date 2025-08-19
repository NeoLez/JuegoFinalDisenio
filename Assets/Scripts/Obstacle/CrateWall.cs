using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CrateWall : MonoBehaviour {
    [SerializeField] public List<GameObject> crateList;
    [SerializeField] public List<int> crateListIDs;
    private Collider selfCollider;
    private int wallInstanceId;
    
    private Material material;
    [SerializeField] private List<Transform> positions = new();

    [SerializeField] private Transform sub1;
    [SerializeField] private Transform sub2;
    

    private void Awake() {
        Renderer child0 = transform.GetChild(0).GetComponent<Renderer>();
        material = new Material(child0.material);
        child0.material = material;
        
        
        selfCollider = GetComponent<Collider>();
        selfCollider.hasModifiableContacts = true;

        wallInstanceId = GetComponent<Rigidbody>().GetInstanceID();

        foreach (var crate in crateList) {
            crate.GetComponent<Collider>().hasModifiableContacts = true;
            crateListIDs.Add(crate.GetComponent<Rigidbody>().GetInstanceID());
        }
    }
    
    void OnEnable() => Physics.ContactModifyEvent += OnContactModify;
    void OnDisable() => Physics.ContactModifyEvent -= OnContactModify;

    void OnContactModify(PhysicsScene scene, NativeArray<ModifiableContactPair> pairs)
    {
        foreach (ModifiableContactPair pair in pairs) {
            if (pair.bodyInstanceID == wallInstanceId || pair.otherBodyInstanceID == wallInstanceId) {
                bool foundCrate = false;
                foreach (var crateID in crateListIDs) {
                    if (pair.bodyInstanceID == crateID || pair.otherBodyInstanceID == crateID) {
                        foundCrate = true;
                        break;
                    }
                }

                if (!foundCrate) {
                    for (int i = 0; i < pair.contactCount; i++) {
                        pair.IgnoreContact(i);
                    }
                }
            }
        }
    }

    private void Update() {
        material.SetVector("_TransparencySpherePositionSubtract1", sub1.position.Swizzle_xyz0());
        material.SetFloat("_TransparencySphereSubtractSize1", sub1.localScale.x);
        material.SetVector("_TransparencySpherePositionSubtract2", sub2.position.Swizzle_xyz0());
        material.SetFloat("_TransparencySphereSubtractSize2", sub2.localScale.x);
        
        for (int i = 0; i < 4 && i < positions.Count; i++) {
            material.SetVector("_TransparencySpherePosition"+i, positions[i].position.Swizzle_xyz0());
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(crateList.Contains(other.gameObject))
            positions.Add(other.transform);
    }

    private void OnTriggerExit(Collider other) {
        positions.Remove(other.transform);
    }
}