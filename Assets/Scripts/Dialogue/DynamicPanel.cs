using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DynamicPanel : MonoBehaviour
{
    public RectTransform panelTransform;
    public TextMeshProUGUI characterLabel;
    private Image panelImage;

    private Dictionary<CharacterNames, Color> characterLabels = new Dictionary<CharacterNames, Color>() {
        { CharacterNames.Carrie, Color.white },
        { CharacterNames.Brother, new Color(1285f/255f, 64/255f, 128f/255f) },  // Indigo
        { CharacterNames.Mother, new Color(64/255f, 128f/255f, 128/255f) },
        { CharacterNames.Father, new Color(128f/255f, 64/255f, 64/255f) }    // Maroon
    };

    void Awake()
    {
        panelTransform = GetComponent<RectTransform>();
        characterLabel = GetComponentInChildren<TextMeshProUGUI>();
        panelImage = GetComponent<Image>();
    }

    public void UpdateUISpeaker(CharacterNames speaker, string speakerLabel, bool isNarrator)
    {
        if (isNarrator) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
        }

        if (characterLabels.ContainsKey(speaker)) {
            panelImage.color = characterLabels[speaker];
            characterLabel.text = speakerLabel;
        } else {
            panelImage.color = Color.white;
            characterLabel.text = "      ";
        }
        // float textWidth = characterLabel.preferredWidth;
        // Vector2 newSize = panelTransform.sizeDelta;
        // newSize.x = textWidth + 20; // Padding
        // panelTransform.sizeDelta = newSize;
    }
}
