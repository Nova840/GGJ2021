using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour {

    [SerializeField]
    private TMP_Text text = default;

    [SerializeField]
    private RectTransform backgroundImage = default;

    [SerializeField]
    private RectTransform textContainer = default;

    [SerializeField]
    private float backgroundEdgeBufferSpace = 5;

    [SerializeField]
    private float typeSpeed = 20;

    private static DialogManager instance;

    private List<string> typeQueue = new List<string>();

    private Coroutine typeCoroutine = null;

    private Vector2 originalBackgroundAnchoredPosition;
    private Vector2 originalContainerAnchoredPosition;

    private void Awake() {
        instance = this;
    }

    private void Start() {
        originalBackgroundAnchoredPosition = backgroundImage.anchoredPosition;
        originalContainerAnchoredPosition = textContainer.anchoredPosition;
    }

    private void Update() {
        bool hasTextToTypeOrIsTyping = HasTextToTypeOrIsTyping();
        bool hasTextToTypeAndNotTyping = HasTextToTypeAndIsNotTyping();

        if (hasTextToTypeOrIsTyping != textContainer.gameObject.activeSelf)
            textContainer.gameObject.SetActive(hasTextToTypeOrIsTyping);

        if (hasTextToTypeAndNotTyping) {
            typeCoroutine = StartCoroutine(Type(typeQueue[0]));
            typeQueue.RemoveAt(0);
        }
    }

    private static bool HasTextToType() {
        return instance.typeQueue.Count > 0;
    }

    private static bool IsTyping() {
        return instance.typeCoroutine != null;
    }

    private static bool HasTextToTypeOrIsTyping() {
        return HasTextToType() || IsTyping();
    }

    private static bool HasTextToTypeAndIsNotTyping() {
        return HasTextToType() && !IsTyping();
    }

    public static bool HasText() {
        return HasTextToTypeOrIsTyping();
    }

    private IEnumerator Type(string textToType) {
        float charInterval = 1 / typeSpeed;
        float time = charInterval;//so it starts on the first char
        int charsTyped = 0;

        bool yielded = false;
        while (charsTyped < textToType.Length) {
            time += Time.deltaTime;
            int charsToTypeThisFrame = Mathf.FloorToInt(time / charInterval);
            time -= charsToTypeThisFrame * charInterval;
            charsTyped += charsToTypeThisFrame;

            text.text = ColorString(textToType, charsTyped);
            SetSize();

            if (yielded && Input.GetButtonDown("Fire1")) {
                break;
            }
            yield return null;
            yielded = true;
        }
        while (!Input.GetButtonDown("Fire1")) {
            yield return null;
        }
        typeCoroutine = null;
    }

    private string ColorString(string originalString, int charsToColor) {
        string coloredText = "";
        Color textColorInvisible = text.color;
        textColorInvisible.a = 0;
        for (int i = 0; i < originalString.Length; i++) {
            coloredText += ColorChar(originalString[i], i < charsToColor ? text.color : textColorInvisible);
        }
        return coloredText;
    }

    private string ColorChar(char c, Color color) {
        return "<color=#" + ColorUtility.ToHtmlStringRGBA(color) + ">" + c + "</color>";
    }

    private void SetSize() {
        backgroundImage.sizeDelta = text.GetRenderedValues(false) + new Vector2(2, 1) * backgroundEdgeBufferSpace;
        backgroundImage.anchoredPosition = originalBackgroundAnchoredPosition - Vector2.up * backgroundEdgeBufferSpace / 2;//changed + to - because it's on the bottom of the screen
        textContainer.anchoredPosition = originalContainerAnchoredPosition + Vector2.one * backgroundEdgeBufferSpace / 2;//changed - to + because it's on the bottom of the screen
    }

    public static void AddToTypeQueue(params string[] textsToType) {
        instance.typeQueue.AddRange(textsToType);
    }

}