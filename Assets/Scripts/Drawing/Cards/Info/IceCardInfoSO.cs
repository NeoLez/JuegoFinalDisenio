using UnityEngine;

[CreateAssetMenu(fileName = "IceCardInfo", menuName = "SO/CardInfo/IceCardInfo")]
public class IceCardInfoSO : ThrowCardInfoSO {
    public override Card GetCard(int position) {
        return new IceCard(this, position);
    }
}
