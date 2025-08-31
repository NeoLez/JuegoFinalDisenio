using UnityEngine;

[CreateAssetMenu(fileName = "LevitationCardInfo", menuName = "SO/CardInfo/LevitationCardInfo")]
public class LevitationCardInfoSO : ThrowCardInfoSO {
    public override Card GetCard(int position) {
        return new LevitationCard(this, position);
    }
}
