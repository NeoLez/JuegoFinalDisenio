using UnityEngine;

public abstract class ThrowCard : Card {
    private ThrowCardInfoSO ThrowCardInfo;
    protected ThrowCard(ThrowCardInfoSO cardInfo, int position) : base(cardInfo, position) {
        ThrowCardInfo = cardInfo;
    }

    protected override void OnThrowActivation() {
        base.OnThrowActivation();
        Ray ray = new Ray(GameManager.MainCamera.transform.position, GameManager.MainCamera.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, ThrowCardInfo.distance, LayerMask.GetMask("Ground", "DraggableObject")))
        {
            OnThrowHit(hit);
        }
        OnThrowHit(null);
    }

    protected abstract void OnThrowHit(RaycastHit? hit);
}