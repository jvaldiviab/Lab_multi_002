using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    // Cogemos la imagen
    public Image imageSelector;
    public Slider sliderBar;


    public void ChangePickableBallColor(bool isSelect)
    {
        if (isSelect)
        {
            imageSelector.color = Color.green;
        }
        else
        {
            imageSelector.color = Color.white;
        }
    }

    public void OcultarCursor(bool ocultar)
    {
        imageSelector.enabled = !ocultar;
    }

    public void ActivarSlider(bool activar)
    {
        sliderBar.gameObject.SetActive(activar);
    }

    public void SetValueBar(float vld)
    {
        sliderBar.value = vld;
    }
}
