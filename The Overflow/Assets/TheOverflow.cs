using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class TheOverflow : MonoBehaviour {

    public KMBombInfo Bomb;
    public KMAudio Audio;

    public KMSelectable ElButtones;

    public TextMesh NamberFaker;

    public GameObject Thiccass;

    int[] ImGoingToSitOnThatOneForALittleBit = new int[10];
    int[] ColorModifiers = new int[10];
    int GoalNumber;
    int GoalResets;
    int Resets;
    int Submit;
    int Total;
    int TotalMods;
    int TotalSolvable;
    int Iteration;

    byte[][] Colors = new byte[][] {
      new byte[] {255, 0, 0, 255},
      new byte[] {235, 98, 2, 255},
      new byte[] {255, 255, 0, 255},
      new byte[] {0, 255, 0, 255},
      new byte[] {0, 0, 255, 255},
    };

    string[] ColorLog = {"red", "orange", "yellow", "green", "blue"};

    bool FuckYouZefod = true;
    bool HasLogged;
    bool HasLoggedButEarlier;
    bool SubmissionStage;
    bool Animation;

    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    void Awake () {
        moduleId = moduleIdCounter++;
        ElButtones.OnInteract += delegate () { ElButtonesPress(); return false; };
        GetComponent<KMBombModule>().OnActivate += Kwan;
    }

    void Start () {
      for (int i = 0; i < 10; i++) {
        ImGoingToSitOnThatOneForALittleBit[i] = UnityEngine.Random.Range(100000, 999999);
        ColorModifiers[i] = UnityEngine.Random.Range(0, 5);
        Debug.LogFormat("[The Overflow #{0}] Number {1} is {2} colored {3}.", moduleId, i, ImGoingToSitOnThatOneForALittleBit[i], ColorLog[ColorModifiers[i]]);
      }
      StartCoroutine(TextCycle());
    }

    void Kwan () {
      StartCoroutine(Embiggenator());
    }

    void ElButtonesPress () {
      if (Animation)
        return;
      if (!SubmissionStage) {
        StartCoroutine(SmallBoi());
        Resets++;
        Debug.LogFormat("[The Overflow #{0}] There are now {1} resets.", moduleId, Resets);
      }
      else if ((int) Math.Floor(Bomb.GetTime()) % 10 == GoalNumber) {
        GetComponent<KMBombModule>().HandlePass();
        Debug.LogFormat("[The Overflow #{0}] You submitted on a {1}. That is right.", moduleId, GoalNumber);
      }
      else {
        GetComponent<KMBombModule>().HandleStrike();
        Debug.LogFormat("[The Overflow #{0}] You submitted on a {1}. That is incorrect.", moduleId, Bomb.GetTime() % 10 - Bomb.GetTime() % 1);
      }
    }

    IEnumerator Embiggenator () {
      while (FuckYouZefod) {
        Thiccass.transform.localScale += new Vector3(.001f, .001f, .001f);
        yield return new WaitForSeconds(.02f);
        Iteration += 1;
      }
    }

    IEnumerator SmallBoi () {
      if (Iteration == 0 || Animation == true)
        yield break;
      Animation = true;
      for (int i = 0; i < Iteration; i++) {
        Thiccass.transform.localScale -= new Vector3(.001f, .001f, .001f);
        yield return new WaitForSeconds(.01f);
      }
      Iteration = 0;
      Animation = false;
      FuckYouZefod = true;
      StartCoroutine(Embiggenator());
    }

    IEnumerator TextCycle () {
      while (true)
        for (int i = 0; i < 10; i++) {
          NamberFaker.color = new Color32(Colors[ColorModifiers[i]][0], Colors[ColorModifiers[i]][1], Colors[ColorModifiers[i]][2], 255);
          if (i == 0)
            NamberFaker.text = "*" + ImGoingToSitOnThatOneForALittleBit[i].ToString();
          else
            NamberFaker.text = ImGoingToSitOnThatOneForALittleBit[i].ToString();
          yield return new WaitForSecondsRealtime(1f);
          NamberFaker.text = "";
          yield return new WaitForSecondsRealtime(.5f);
        }
    }

    void Update () {
      if (Animation)
        FuckYouZefod = false;
      TotalMods = Bomb.GetSolvableModuleNames().Count;
      TotalSolvable = Bomb.GetSolvedModuleNames().Count;
      if (!HasLoggedButEarlier) {
        GoalResets = (int)(Math.Ceiling(((float)TotalMods * 1.2)));
        var x = UnityEngine.Random.Range(0, (int)(Math.Ceiling(((float)TotalMods))));
        GoalResets += x + 1;
        Debug.LogFormat("[The Overflow #{0}] Goal amount of resets are {1}.", moduleId, GoalResets);
        HasLoggedButEarlier = true;
      }
      if (Resets >= GoalResets || (float) TotalSolvable / (float) TotalMods >= .5f) {
        StartCoroutine(SmallBoi());
        FuckYouZefod = false;
        SubmissionStage = true;
        GoalNumber = int.Parse(ImGoingToSitOnThatOneForALittleBit[Bomb.GetSerialNumberNumbers().Last()].ToString()[Resets % 6].ToString());
        if (!HasLogged)
          Debug.LogFormat("[The Overflow #{0}] Initial goal number is {1}.", moduleId, GoalNumber);
        switch (ColorModifiers[Bomb.GetSerialNumberNumbers().Last()]) {
          case 0:
          GoalNumber -= 5;
          break;
          case 1:
          GoalNumber--;
          break;
          case 3:
          GoalNumber += 2;
          break;
          case 4:
          GoalNumber += 7;
          break;
        }
        if (GoalNumber > 9)
          GoalNumber %= 10;
        while (GoalNumber < 0)
          GoalNumber += 10;
        if (!HasLogged)
          Debug.LogFormat("[The Overflow #{0}] After modifications, the final goal number is {1}.", moduleId, GoalNumber);
        HasLogged = true;
      }
    }

    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"Use !{0} anything to solve the module.";
    #pragma warning restore 414

    IEnumerator ProcessTwitchCommand (string Command) {
      yield return null;
      Thiccass.gameObject.SetActive(false);
      GetComponent<KMBombModule>().HandlePass();
    }

    IEnumerator TwitchHandleForcedSolve () {
      yield return ProcessTwitchCommand("Weed Eater");
    }
}
