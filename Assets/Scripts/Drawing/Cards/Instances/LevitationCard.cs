using Facts;
using StatusEffects;
using UnityEngine;

public class LevitationCard : ThrowCard{
    private readonly LevitationCardInfoSO _info;
    public LevitationCard(LevitationCardInfoSO cardInfo, int position) : base(cardInfo, position) {
    }

    protected override void OnThrowHit(RaycastHit? hit) {
        if (!hit.HasValue) {
            RegisterUse();
            return;
        }

        var rhit = hit.Value;
        
        if (rhit.collider.gameObject.TryGetComponent(out StatusEffectsHandler status)) {
            status.AddEffect(StatusEffectsType.LEVITATION, 9999999f);
        }
        RegisterUse();
    }
}