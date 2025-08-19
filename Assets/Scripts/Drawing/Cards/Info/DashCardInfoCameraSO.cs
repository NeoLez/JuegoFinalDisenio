using UnityEngine;

[CreateAssetMenu(fileName = "DashCardInfo", menuName = "SO/CardInfo/DashCardInfo")]
public class DashCardInfoCameraSO : ThrowCardInfoSO {
    [SerializeField] public float moveDistance;
    [SerializeField] public float time;
    [SerializeField] public AnimationCurve curve;
    [SerializeField] public float forceStrength;
    [SerializeField] public AudioClip dashAudio;
    
    public override Card GetCard(int position) {
        return new DashCard(this, position);
    }
}