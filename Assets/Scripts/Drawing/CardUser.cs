using UnityEngine;

public class CardUser : MonoBehaviour {
    //tp2 Leonardo Gonzalez Chiavassa
    private CardStorage _cardStorage;
    
    private void Awake() {
        var _input = GameManager.Input;
        
        _cardStorage = GetComponent<CardStorage>();

        _input.CardUsage.UseCard0.performed += _ => _cardStorage.SetCurrentCard(0);
        _input.CardUsage.UseCard1.performed += _ => _cardStorage.SetCurrentCard(1);
        _input.CardUsage.UseCard2.performed += _ => _cardStorage.SetCurrentCard(2);
        _input.CardUsage.UseCard3.performed += _ => _cardStorage.SetCurrentCard(3);
        _input.CardUsage.UseCard4.performed += _ => _cardStorage.SetCurrentCard(4);
        _input.CardUsage.UseCard5.performed += _ => _cardStorage.SetCurrentCard(5);
        

        _input.CardUsage.CardSelectRelative.performed += _ => {
            float v = _input.CardUsage.CardSelectRelative.ReadValue<float>();
            if (v > 0) {
                _cardStorage.SetNextCard();
            } else if (v < 0) {
                _cardStorage.SetPreviousCard();
            }};
    }
}