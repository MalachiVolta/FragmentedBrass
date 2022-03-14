using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMesh : MonoBehaviour
{
    private SkinnedMeshRenderer m_Renderer;
    private SkinnedMeshRenderer m_Renderer2;
    public GameObject tool;
    public GameObject mag;
    private void Start()
    {
        m_Renderer = tool.GetComponent<SkinnedMeshRenderer>();
        m_Renderer2 = mag.GetComponent<SkinnedMeshRenderer>();
    }
    void ToggleRadio()
    {
        m_Renderer.enabled = !m_Renderer.enabled;
    }

    void ToggleMag()
    {
        m_Renderer2.enabled = !m_Renderer2.enabled;
    }
}
