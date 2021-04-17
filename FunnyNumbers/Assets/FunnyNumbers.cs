using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using KModkit;

public class FunnyNumbers : MonoBehaviour
{

    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMSelectable[] Buttons;
    public TextMesh uno;
    public TextMesh dos;
    public TextMesh tres;
    public TextMesh quart;
    public TextMesh cinco;
    private List<int> funnies = new List<int> { 69, 6969, 420, 69420, 42069, 25, 21, 789, 80085, 58008, 8008, 3, 522, 777, 13 };
    private List<int> notfunnies = new List<int> { 0, 1, 68, 70, 421, 419, 19, 666, 24, 2003 };

    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;
    int retard1 = 69;
    int retard2 = 69;
    int retard3 = 69;
    int retard4 = 69;
    int retard5 = 69;
    int stageretard = 1;
    int HAHAHAHAHAHHAHAHA = -1;
    int bullshitvariable = 0;

    void Awake()
    {
        moduleId = moduleIdCounter++;

        foreach (KMSelectable Button in Buttons)
        {
            Button.OnInteract += delegate () { ButtonPress(Button); return false; };
        }
    }
    void Start()
    {
        retard1 = UnityEngine.Random.Range(0, 10);
        retard2 = UnityEngine.Random.Range(10, 100);
        retard3 = UnityEngine.Random.Range(100, 1000);
        retard4 = UnityEngine.Random.Range(1000, 10000);
        retard5 = UnityEngine.Random.Range(10000, 100000);
        uno.text = "0";
        dos.text = "0";
        tres.text = "0";
        quart.text = "0";
        cinco.text = retard1.ToString();
        FunneChegg(retard1);
    }
    void ButtonPress(KMSelectable Button)
    {
        if (Button == Buttons[0])
        {
            Button.AddInteractionPunch();
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            if (HAHAHAHAHAHHAHAHA > 0)
            {
                stageretard += 1;
                HAHAHAHAHAHHAHAHA = -1;
                Debug.LogFormat("[Funny Numbers #{0}] You pressed EL FUNNY. That was correct.", moduleId);
                if (stageretard >= 6)
                {
                    GetComponent<KMBombModule>().HandlePass();
                    moduleSolved = true;
                    Audio.PlaySoundAtTransform("moan", transform);
                }
                else
                {
                    switch (stageretard)
                    {
                        case 2:
                        if (UnityEngine.Random.Range(0, 10) == 1) {
                          stageretard = 5;
                        }
                            FunneChegg(retard2);
                            break;
                        case 3:
                        if (UnityEngine.Random.Range(0, 7) == 1) {
                          stageretard = 5;
                        }
                            FunneChegg(retard3);
                            break;
                        case 4:
                        if (UnityEngine.Random.Range(0, 5) == 2) {
                          stageretard = 5;
                        }
                            FunneChegg(retard4);
                            break;
                        case 5:
                            FunneChegg(retard5);
                            break;
                        default:
                            Debug.LogFormat("Chegg");
                            break;
                    }
                }
            }
            else
            {
                GetComponent<KMBombModule>().HandleStrike();
                Debug.LogFormat("[Funny Numbers #{0}] You pressed funny when it was unfunny. Fuck! Nooooooooooooo!", moduleId);
            }
        }
        else if (Button == Buttons[1])
        {
            Button.AddInteractionPunch();
            GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
            if (HAHAHAHAHAHHAHAHA <= 0)
            {
                stageretard += 1;
                HAHAHAHAHAHHAHAHA = -1;
                Debug.LogFormat("[Funny Numbers #{0}] You pressed EL NO FUNNY. That was correct.", moduleId);
                if (stageretard >= 6)
                {
                    GetComponent<KMBombModule>().HandlePass();
                    moduleSolved = true;
                    Audio.PlaySoundAtTransform("moan", transform);
                }
                else
                {
                    switch (stageretard)
                    {
                        case 2:
                            FunneChegg(retard2);
                            break;
                        case 3:
                            FunneChegg(retard3);
                            break;
                        case 4:
                            FunneChegg(retard4);
                            break;
                        case 5:
                            FunneChegg(retard5);
                            break;
                        default:
                            Debug.Log("Chegg");
                            break;
                    }
                }
            }
            else
            {
                GetComponent<KMBombModule>().HandleStrike();
                Debug.LogFormat("[Funny Numbers #{0}] You pressed unfunny when it was funny. Fuck!", moduleId);
            }
        }
        else
        {
        }
    }
    public static bool IsPrime(int number)
    {
        if (number <= 1) return false;
        if (number == 2) return true;
        if (number % 2 == 0) return false;

        var boundary = (int)Math.Floor(Math.Sqrt(number));

        for (int i = 3; i <= boundary; i += 2)
            if (number % i == 0)
                return false;

        return true;
    }
    void FunneChegg(int sex)
    {
        uno.text = ((sex % 100000 - sex % 10000) / 10000).ToString();
        dos.text = ((sex % 10000 - sex % 1000) / 1000).ToString();
        tres.text = ((sex % 1000 - sex % 100) / 100).ToString();
        quart.text = ((sex % 100 - sex % 10) / 10).ToString();
        cinco.text = (sex % 10).ToString();
        Debug.LogFormat("[Funny Numbers #{0}] The starting number is {1} for stage {2}. Starting value is -1 since it is divisible by 1.", moduleId, sex, stageretard);
        for (int i = 0; i < 15; i++)
        {
            if (sex == funnies[i])
            {
                HAHAHAHAHAHHAHAHA += 100000;
                Debug.LogFormat("[Funny Numbers #{0}] IT IS AUTOMATICALLY FUNNY. PRESS FUNNY^TM.", moduleId);
            }
        }
        for (int i = 0; i < 10; i++)
        {
            if (sex == notfunnies[i])
            {
                HAHAHAHAHAHHAHAHA -= 100000;
                Debug.LogFormat("[Funny Numbers #{0}] IT IS AUTOMATICALLY NOT FUNNY. PRESS NOT FUNNY^TM.", moduleId);
            }
        }
        for (int i = 0; i < 15; i++)
        {
            if (sex % funnies[i] == 0)
            {
                HAHAHAHAHAHHAHAHA += 1;
                Debug.LogFormat("[Funny Numbers #{0}] This number is divisible by {1}. Adding 1 gives you {2}.", moduleId, funnies[i], HAHAHAHAHAHHAHAHA);
            }
        }
        for (int i = 2; i < 10; i++)
        {
            if (sex % notfunnies[i] == 0)
            {
                HAHAHAHAHAHHAHAHA -= 1;
                Debug.LogFormat("[Funny Numbers #{0}] This number is divisible by {1}. Subtracting 1 gives you {2}.", moduleId, notfunnies[i], HAHAHAHAHAHHAHAHA);
            }
        }
        if (sex % 2 == 0)
        {
            HAHAHAHAHAHHAHAHA -= 1;
            Debug.LogFormat("[Funny Numbers #{0}] This number is even. Subtracting 1 gives you {1}.", moduleId, HAHAHAHAHAHHAHAHA);
        }
        else
        {
            HAHAHAHAHAHHAHAHA += 1;
            Debug.LogFormat("[Funny Numbers #{0}] This number is odd. Adding 1 gives you {1}.", moduleId, HAHAHAHAHAHHAHAHA);
        }
        if (IsPrime(sex) == true)
        {
            HAHAHAHAHAHHAHAHA += 2;
            Debug.LogFormat("[Funny Numbers #{0}] This number is prime. Adding 2 gives you {1}.", moduleId, HAHAHAHAHAHHAHAHA);
        }
        if ((float)Math.Sqrt(sex) % 1 == 0)
        {
            HAHAHAHAHAHHAHAHA -= 2;
            Debug.LogFormat("[Funny Numbers #{0}] This number is a square. Subtracting 2 gives you {1}.", moduleId, HAHAHAHAHAHHAHAHA);
            bullshitvariable = (int)Math.Sqrt(sex);
            if (IsPrime(bullshitvariable) == true)
            {
                HAHAHAHAHAHHAHAHA -= 2;
                Debug.LogFormat("[Funny Numbers #{0}] This number is a square of a prime. Subtracting 2 gives you {1}.", moduleId, HAHAHAHAHAHHAHAHA);
            }
        }
    }

    // Twitch Plays
    #pragma warning disable 414
    private readonly string TwitchHelpMessage = "!{0} <funny/unfunny> [Presses that button]";
    #pragma warning restore 414

    IEnumerator ProcessTwitchCommand(string input)
    {
        input = input.ToLowerInvariant();
        if (input.Split(' ').ToArray().Length != 1)
            yield break;
        var words = new string[] { "funny", "unfunny" };
        if (!words.Contains(input))
            yield break;
        yield return null;
        Buttons[Array.IndexOf(words, input)].OnInteract();
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        while (!moduleSolved)
        {
            if (HAHAHAHAHAHHAHAHA > 0)
                Buttons[0].OnInteract();
            else
                Buttons[1].OnInteract();
            yield return new WaitForSeconds(.1f);
        }
    }
}
