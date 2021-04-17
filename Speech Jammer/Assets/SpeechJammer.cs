using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class SpeechJammer : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public TextMesh Display;
    public TextMesh[] ButtonTexts;
    public KMSelectable[] Buttons;

    List<int> IndividualWords = new List<int> {};
    int PhraseSelector;
    int Mods;
    int IgnoredMods;
    int CorrectPresses = 0;
    int Stage = -1;

    string[][] TongueTwisters = new string[12][] {
      new string[5] {"AARON", "EARNED", "AN", "IRON", "URN"},
      new string[5] {"SHE", "SELLS", "SEA SHELLS", "SEA", "SHORE"},
      new string[5] {"PETER", "PIPER", "PICKED", "PECK", "PEPPERS"},
      new string[5] {"BIT", "BOUGHT", "BUTTER", "BITTER", "BETTER"},
      new string[5] {"CAN", "CLAM", "CRAM", "CLEAN", "CREAM"},
      new string[5] {"WOOD", "WOULD", "WOODCHUCK", "COULD", "CHUCK"},
      //new string[] {"I", "SCREAM", "ICE", "CREAM", "WE"}, not sure tbh
      new string[5] {"WISH", "WASH", "IRISH", "WRIST", "WATCH"},
      new string[5] {"NEAR", "EAR", "NEARER", "NEARLY", "EERIE"},
      new string[5] {"KNOW", "NEW", "YORK", "NEED", "UNIQUE"},
      new string[5] {"IMAGINE", "IMAGINARY", "MENAGERIE", "MANAGER", "MANAGING"},
      new string[5] {"WITCHES", "WATCHES", "WITCH", "WATCH", "WHICH"},
      new string[5] {"NINE", "NICE", "NIGHT", "NURSES", "NICELY"}
    };
    string[] OriginalPhrases = {"Aaron earned an iron urn.", "She sells sea shells by the sea shore.", "Peter Piper picked a peck of pickled peppers.", "Betty Botter bought some butter; But she said the butter’s bitter; If I put it in my batter, it will make my batter bitter; But a bit of better butter will make my batter better; So ‘twas better Betty Botter bought a bit of better butter.", "How can a clam cram in a clean cream can?", /*"", */"How much wood can a woodchuck chuck if a wood chuck could chuck wood?", "I wish to wash my Irish wristwatch.", "Near an ear, a nearer ear, a nearly eerie ear.", "You know New York, you need New York, you know you need unique New York", "Imagine an imaginary menagerie manager imagining managing an imaginary menagerie.", "If two witches were watching two watches which witch would watch which watch?", "Nine nice night nurses nursing nicely."};
    public static string[] ignoredModules = null;

    bool HasRan;
    bool IgnoreRan;
    bool Solvable;

    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    void Awake () {
        moduleId = moduleIdCounter++;

        foreach (KMSelectable Button in Buttons) {
            Button.OnInteract += delegate () { ButtonPress(Button); return false; };
        }


        if (ignoredModules == null)
            ignoredModules = GetComponent<KMBossModule>().GetIgnoredModules("Speech Jammer", new string[]{
                "14",
                "42",
                "501",
                "A>N<D",
                "Bamboozling Time Keeper",
                "Black Arrows",
                "Brainf---",
                "Busy Beaver",
                "Don't Touch Anything",
                "Floor Lights",
                "Forget Any Color",
                "Forget Enigma",
                "Forget Everything",
                "Forget Infinity",
                "Forget It Not",
                "Forget Maze Not",
                "Forget Me Later",
                "Forget Me Not",
                "Forget Perspective",
                "Forget The Colors",
                "Forget Them All",
                "Forget This",
                "Forget Us Not",
                "Iconic",
                "Keypad Directionality",
                "Kugelblitz",
                "Multitask",
                "OmegaDestroyer",
                "OmegaForest",
                "Organization",
                "Password Destroyer",
                "Purgatory",
                "RPS Judging",
                "Security Council",
                "Shoddy Chess",
                "Simon Forgets",
                "Simon's Stages",
                "Souvenir",
                "Speech Jammer",
                "Tallordered Keys",
                "The Time Keeper",
                "Timing is Everything",
                "The Troll",
                "Turn The Key",
                "The Twin",
                "Übermodule",
                "Ultimate Custom Night",
                "The Very Annoying Button",
                "Whiteout"
            });

    }

    void Start () {
      PhraseSelector = UnityEngine.Random.Range(0, TongueTwisters.Length);
      TongueTwisters[PhraseSelector].Shuffle();
      for (int i = 0; i < 5; i++) {
        ButtonTexts[i].text = TongueTwisters[PhraseSelector][i];
        switch (ButtonTexts[i].text.Length) {
          case 10:
          ButtonTexts[i].fontSize = 150;
          break;
          case 9: case 8: case 7:
          ButtonTexts[i].fontSize = 165;
          break;
          case 6:
          ButtonTexts[i].fontSize = 225;
          break;
          default:
          ButtonTexts[i].fontSize = 300;
          break;
        }
      }
    }

    void ButtonPress (KMSelectable Button) {
      Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, Button.transform);
      if (!Solvable) {
        GetComponent<KMBombModule>().HandleStrike();
        return;
      }
      for (int i = 0; i < 5; i++) {
        if (Button == Buttons[i]) {
          if (TongueTwisters[PhraseSelector][i] == TongueTwisters[PhraseSelector][IndividualWords[CorrectPresses]]) {
            CorrectPresses++;
            if (CorrectPresses == IndividualWords.Count()) {
              GetComponent<KMBombModule>().HandlePass();
            }
          }
          else {
            GetComponent<KMBombModule>().HandleStrike();
          }
        }
      }
    }

    void Update () {
      if (!IgnoreRan && ignoredModules != null) {
        for (int i = 0; i < Bomb.GetSolvableModuleNames().Count(); i++) {
          for (int j = 0; j < ignoredModules.Length; j++) {
            if (Bomb.GetSolvableModuleNames()[i] == ignoredModules[j]) {
              IgnoredMods++;
            }
          }
        }
        IgnoreRan = true;
      }
      if (Mods == 0 && !HasRan) {
        Mods = Bomb.GetSolvableModuleNames().Count() - IgnoredMods;
        if (Bomb.GetSolvableModuleNames().Count() == IgnoredMods) {
          GetComponent<KMBombModule>().HandlePass();
        }
        if (Mods != 0) {
          for (int i = 0; i < Mods; i++) {
            IndividualWords.Add(UnityEngine.Random.Range(0, 5));
            Debug.LogFormat("[Speech Jammer #{0}] The phrase at stage {1} is \"{2}\"", moduleId, i + 1, TongueTwisters[PhraseSelector][IndividualWords[i]]);
          }
          HasRan = true;
        }
      }
      int Solved = Bomb.GetSolvedModuleNames().Count(x => !ignoredModules.Contains(x));

      /*else if (Mods >= Bomb.GetSolvedModuleNames().Count()) {
        Solvable = true;
        Display.text = String.Empty;
      }*/
      if (Solved == Mods) {
        Solvable = true;
        Display.text = String.Empty;
      }
      else if (Solved > Stage) {
        Stage++;
        Debug.Log(IndividualWords.Count());
        Debug.Log(Stage);
        Display.text = TongueTwisters[PhraseSelector][IndividualWords[Stage]];
        switch (Display.text.Length) {
          case 10:
          Display.fontSize = 150;
          break;
          case 9: case 8: case 7:
          Display.fontSize = 165;
          break;
          case 6:
          Display.fontSize = 225;
          break;
          default:
          Display.fontSize = 300;
          break;
        }
      }
    }

    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} to do something.";
    #pragma warning restore 414

    IEnumerator ProcessTwitchCommand (string Command) {
      yield return null;
    }

    IEnumerator TwitchHandleForcedSolve () {
      GetComponent<KMBombModule>().HandlePass();
      yield return null;
    }
}
