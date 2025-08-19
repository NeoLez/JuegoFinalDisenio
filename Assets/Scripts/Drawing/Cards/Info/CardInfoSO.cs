using UnityEngine;

public abstract class CardInfoSO : ScriptableObject {
    [SerializeField] public Sprite icon;
    [SerializeField] public byte id;
    [SerializeField] public string spellName;
    [SerializeField] public byte inkCost;
    [SerializeField] public byte maxUses;
    [SerializeField] public GameObject cardUIPrefab;
    [SerializeField] public float cooldown;

    public abstract Card GetCard(int position);
}
