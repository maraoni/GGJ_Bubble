using TMPro;
using UnityEngine;

public class MachineText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI myText;
    [SerializeField] private string DebugText;
    [HideInInspector] public bool IsTyping = false;


    [ContextMenu("Display DebugText")]
    public void DisplayText()
    {
        _ = ShowText(DebugText, true);
    }

    public void DisplayText(string someText, bool shouldDissapear)
    {
        _ = ShowText(someText, shouldDissapear);
    }

    private async Awaitable ShowText(string someText, bool shouldDissaper)
    {
        IsTyping = true;
        string totalText = "";
        foreach (char c in someText)
        {
            await Awaitable.WaitForSecondsAsync(0.03f);
            totalText += c;
            myText.text = totalText;
        }

        if (shouldDissaper)
        {
            await Awaitable.WaitForSecondsAsync(1.0f);
            myText.text = "";
        }
        IsTyping = false;
    }
}
