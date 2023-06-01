using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public List<Button> elementButtons;
    private Const.ElementType cacheType = Const.ElementType.NULL;
    private Const.ElementCategory cacheCategory = Const.ElementCategory.NULL;
    private Button cacheButton = null;

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
        if(button == cacheButton)
        {
            button.transform.Find("selected").gameObject.SetActive(false);
            ElementManager.Instance.curType = Const.ElementType.NULL;
            ElementManager.Instance.curCategory = Const.ElementCategory.NULL;
            cacheType = Const.ElementType.NULL;
            cacheCategory = Const.ElementCategory.NULL;
            cacheButton = null;
        }
        else
        {
            if(cacheButton != null)
            {
                cacheButton.transform.Find("selected").gameObject.SetActive(false);
            }
            button.transform.Find("selected").gameObject.SetActive(true);
            ElementManager.Instance.curType = type;
            ElementManager.Instance.curCategory = category;
            cacheType = type;
            cacheCategory = category;
            cacheButton = button;
        }
    }
}
