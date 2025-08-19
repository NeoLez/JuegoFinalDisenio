using UnityEngine;

[CreateAssetMenu(fileName = "FireCardInfo", menuName = "SO/CardInfo/FireCardInfo")]
public class FireCardInfoSO : ThrowCardInfoSO {
    public override Card GetCard(int position) {
        return new FireCard(this, position);
    }
}
