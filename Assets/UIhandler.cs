using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class UIhandler : MonoBehaviour
{

    public VisualElement m_healthbar;
    public static UIhandler instance { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        m_healthbar = uiDocument.rootVisualElement.Q<VisualElement>("HealthBar");
        SetHealthValue(1f);
       
    }

    public void SetHealthValue(float width)
    {
        m_healthbar.style.width = Length.Percent(width * 100.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
