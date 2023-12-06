using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ColorPicker : MonoBehaviour, IPointerClickHandler
{
    public Image colorWheelImage;
    public Image selectedColorPanel;

    private void Start()
    {
        //brightnessSlider.onValueChanged.AddListener(UpdateSelectedColor(baseColor);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2 localCursor;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(colorWheelImage.rectTransform, eventData.position, eventData.pressEventCamera, out localCursor);

        float x = (localCursor.x + colorWheelImage.rectTransform.rect.width / 2) / colorWheelImage.rectTransform.rect.width;
        float y = (localCursor.y + colorWheelImage.rectTransform.rect.height / 2) / colorWheelImage.rectTransform.rect.height;

        Color pickedColor = colorWheelImage.sprite.texture.GetPixelBilinear(x, y);
        UpdateSelectedColor(pickedColor);
    }

    private void UpdateSelectedColor(Color baseColor)
    {
        Color finalColor = baseColor;
        selectedColorPanel.color = finalColor;
        // Implement logic to apply the selected color.
    }
}
