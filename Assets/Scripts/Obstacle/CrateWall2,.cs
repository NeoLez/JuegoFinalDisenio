using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CrateWall2 : MonoBehaviour {
    [SerializeField] public List<GameObject> crateList;
    [SerializeField] public List<int> crateListIDs;
    private Collider selfCollider;
    private int wallInstanceId;
    
    private Material material;
    [SerializeField] private List<Transform> positions = new();

    [SerializeField] private Transform sub1;
    [SerializeField] private Transform sub2;
    

    private void Awake() {
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

    private void Update() {
        lastTime = Time.time;
    }

    private int lastID;
    private float lastHitTimestamp;
    private float lastTime;
    void OnContactModify(PhysicsScene scene, NativeArray<ModifiableContactPair> pairs)
    {
        foreach (ModifiableContactPair pair in pairs) {
            if (pair.bodyInstanceID == wallInstanceId || pair.otherBodyInstanceID == wallInstanceId) {
                bool foundCrate = false;
                foreach (var crateID in crateListIDs) {
                    if (pair.bodyInstanceID == crateID || pair.otherBodyInstanceID == crateID) {
                        foundCrate = true;
                        if (lastID != crateID || lastTime - lastHitTimestamp >= 0.7f) {
                        }
                        
                        lastID = crateID;
                        lastHitTimestamp = lastTime;
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
    
    private void OnTriggerEnter(Collider other) {
        if(crateList.Contains(other.gameObject))
            positions.Add(other.transform);
    }

    private void OnTriggerExit(Collider other) {
        positions.Remove(other.transform);
    }
}