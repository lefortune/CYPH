using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapPieceShow : MonoBehaviour
{
    public void ToggleVisibilityBasedOnName()
{
    var nameThresholds = new (string namePart, int requiredScrapPieces)[]
    {
        ("Brother", 1),
        ("Mother", 2),
        ("Father", 3),
        ("Carrie", 4)
    };

    foreach (var item in nameThresholds)
    {
        if (gameObject.name.Contains(item.namePart) && CutscenesManager.scrapPieces >= item.requiredScrapPieces)
        {
            gameObject.SetActive(true);
            return;
        }
    }

    gameObject.SetActive(false);
}

}
