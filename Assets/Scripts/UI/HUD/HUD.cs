using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public List<Button> elementButtons;

    private void Awake()
    {
        foreach(Button elementBtn in elementButtons)
        {
            ElementModel model = elementBtn.GetComponent<ElementModel>();
            Const.ElementCategory category = model.category;
            Const.ElementType type = model.type;
            elementBtn.onClick.AddListener(() => OnElementChanged(elementBtn, category, type));
        }
    }

    public void OnElementChanged(Button button, Const.ElementCategory category, Const.ElementType type)
    {
        foreach(Button btn in elementButtons)
        {
            btn.transform.Find("selected").gameObject.SetActive(false);
        }
        Transform selected = button.transform.Find("selected");
        selected.gameObject.SetActive(true);
    }
}
