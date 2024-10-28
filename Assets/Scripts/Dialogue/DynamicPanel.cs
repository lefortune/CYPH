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
        { CharacterNames.Carrie, Color.magenta },
        { CharacterNames.Brother, new Color(75f/255f, 0f, 130f/255f) },  // Indigo
        { CharacterNames.Mother, Color.cyan },
        { CharacterNames.Father, new Color(128f/255f, 0, 0) }    // Maroon
    };

    void Awake()
    {
        panelTransform = GetComponent<RectTransform>();
        characterLabel = GetComponentInChildren<TextMeshProUGUI>();
        panelImage = GetComponent<Image>();
    }

    public void UpdateUISpeaker(CharacterNames speaker, string speakerLabel)
    {
        if (characterLabels.ContainsKey(speaker)) {
            panelImage.color = characterLabels[speaker];
            characterLabel.text = speakerLabel;
        } else {
            panelImage.color = Color.grey;
            characterLabel.text = "      ";
        }
        float textWidth = characterLabel.preferredWidth;
        Vector2 newSize = panelTransform.sizeDelta;
        newSize.x = textWidth + 20; // Padding
        panelTransform.sizeDelta = newSize;
    }
}
