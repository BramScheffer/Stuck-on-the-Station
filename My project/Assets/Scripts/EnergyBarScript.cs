using UnityEngine;
using UnityEngine.UI;

public class EnergyBarUI : MonoBehaviour
{
    public Image energyBarImage;  // Je energiebar image (blauw gedeelte)

    // Call this method whenever your energy changes in your existing energy script
    public void UpdateEnergyBar(float energyPercent)
    {
        // Zorg ervoor dat de waarde tussen 0 en 1 ligt
        energyPercent = Mathf.Clamp01(energyPercent);

        // Stel de fillAmount van de image in op basis van het percentage (0 = leeg, 1 = vol)
        energyBarImage.fillAmount = energyPercent;
    }
}
