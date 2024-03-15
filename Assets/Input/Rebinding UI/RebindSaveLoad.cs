using UnityEngine;
using UnityEngine.InputSystem;

public class RebindSaveLoad : MonoBehaviour
{
    public InputActionAsset actions1;
    public InputActionAsset actions2;

    public void OnEnable()
    {
        var rebinds = PlayerPrefs.GetString("rebinds1");
        if (!string.IsNullOrEmpty(rebinds))
        {
            actions1.LoadBindingOverridesFromJson(rebinds);
        }

        rebinds = PlayerPrefs.GetString("rebinds2");
        if (!string.IsNullOrEmpty(rebinds))
        {
            actions2.LoadBindingOverridesFromJson(rebinds);
        }
    }

    public void OnDisable()
    {
        var rebinds = actions1.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds1", rebinds);

        rebinds = actions2.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds2", rebinds);
    }
}
