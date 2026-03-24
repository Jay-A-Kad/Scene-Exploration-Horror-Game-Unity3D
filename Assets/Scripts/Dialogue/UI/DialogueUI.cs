

using System.Runtime.CompilerServices;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using NUnit.Framework;



public class DialogueUI : MonoBehaviour
{
    [Header("UI configs")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private TextMeshProUGUI bodyText;
    [SerializeField] private TextMeshProUGUI HintText;

    //player lock movement while dialogue is active

    //
    private IReadOnlyList<DialogueLine> activeLines;
    private int currentLineIndex;

    public bool isOpen { get; private set; }
    public event Action OnDialogueFinished;


    private void Awake()
    {
        HideImmediately();
    }

    private void Update()
    {
        if (!isOpen) return;

        if (AdvancedPressed())
        {
            ShowNextLine();
        }
    }
    public void ShowDialogue(IReadOnlyList<DialogueLine> lines)
    {
        if (lines == null || lines.Count == 0)
        {
            OnDialogueFinished?.Invoke();
            return;
        }
        activeLines = lines;
        currentLineIndex = 0;
        isOpen = true;
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }

        RenderCurrentLine();

    }

    private void ShowNextLine()
    {
        currentLineIndex++;
        if (currentLineIndex >= activeLines.Count)
        {
            CloseDialogue();
            return;
        }

        RenderCurrentLine();
    }

    private void RenderCurrentLine()
    {
        var line = activeLines[currentLineIndex];
        if (speakerText != null)
        {
            speakerText.text = line.speaker;
        }
        if (bodyText != null)
            bodyText.text = line.text;
        if (HintText != null)
        {
            HintText.text = "[Press E to continue]";
        }
    }

    private void CloseDialogue()
    {
        isOpen = false;
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
        activeLines = null;
        OnDialogueFinished?.Invoke();
    }

    private void HideImmediately()
    {
        isOpen = false;
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }
    private bool AdvancedPressed()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
}