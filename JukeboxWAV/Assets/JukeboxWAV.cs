using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;

public class JukeboxWAV : MonoBehaviour
{
    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMSelectable[] Arrows;
    public KMSelectable[] Circboys;
    public Material[] Songs;
    public GameObject weed;
    public GameObject[] Shit;
    public Material[] poooooooooo;
    public GameObject ScrewdriverHandle;

    // Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved;

    int poop = 0;
    bool RickSanchez = false;
    int uhhhhhvv = 0;
    int TheNutDotTMCopyright = 0;
    private List<string> Fuck = new List<string> { "Africa", "1812 Overture", "Chocolate Rain", "Danger Zone", "Drunken Sailor", "Everybody Wants To Rule the World", "Harder Better Faster Stronger", "Hardware Store", "Karma Chameleon", "Love Potion Number 9", "Mambo Number 5", "Megalovania", "Mountain King", "Never Gonna Give You Up", "Photograph", "Running In the Nineties", "Sixteen Tons", "Smooth Criminal", "Starstrukk", "Stayin\' Alive", "Take On Me", "You Spin Me Right Round (Like a Record)", "XO", "Graze the Roof", "The Man Behind The Slaughter", "Peepee Song", "Sandstorm" };
    private List<string> Shitlord = new List<string> { "Africa", "CannonSong", "ChocolateRain", "DangerZone", "DrunkenSailor", "EverybodyWantsToRuleTheWorld", "HarderBetterFasterStronger", "HardwareStore", "KarmaChameleon", "LovePotionNoNine", "MamboNoFive", "Megalovania", "MountainKing", "NeverGoing", "Photograph", "RunningInTheNineties", "SixteenTons", "SmoothCriminal", "Starfuck", "StayinAlive", "TakeOnMe", "YouSpinMeRightRound", "XO", "GrazeTheRoof", "ManBehindTheSlaughter", "PeePee", "Sandstorm" };
    private List<float> Leprosy = new List<float> { 15.716f, 12.416f, 12.666f, 12.033f, 12.316f, 14.650f, 10.700f, 31.268f, 10.133f, 12.066f, 11.183f, 12.483f, 11.933f, 13.150f, 10.800f, 12.150f, 13.300f, 12.133f, 15.300f, 13.616f, 14.050f, 13.433f, 26.033f, 17.016f, 39.883f, 15.500f, 14.166f };
    bool WeedChungi = false;
    float BamboozlingTimeKeeper = 0f;
    private Vector3 wsdfgh = new Vector3(-.07304f, 0.0097f, 0.0578f);
    private Vector3 iuhfahufhiuofaijhfdjhfdljheijhfiuergiuhpegraiuverefrwhpogu = new Vector3(0.02707f, 0.0097f, 0.0578f);
    int check = 0;

    void Awake()
    {
        moduleId = moduleIdCounter++;
        foreach (KMSelectable Arrow in Arrows)
        {
            Arrow.OnInteract += delegate () { ArrowsPress(Arrow); return false; };
        }

        foreach (KMSelectable Circboy in Circboys)
        {
            Circboy.OnInteract += delegate () { CircboysPress(Circboy); return false; };
        }
    }

    void Start()
    {
        System.DateTime curTime = System.DateTime.Now;
        if (curTime.Month == 4 && curTime.Day == 1)
        {
            // April Fools! Always Astley!
            RickSanchez = true;
        }
        poop = UnityEngine.Random.Range(0, Songs.Count());
        if (RickSanchez == true)
        {
            poop = 13;
        }
        weed.GetComponent<MeshRenderer>().material = Songs[poop];
        uhhhhhvv = Bomb.GetSerialNumberNumbers().Last();
        Debug.LogFormat("[Jukebox.WAV #{0}] You started at {1} (Which is an index of {2}).", moduleId, Fuck[uhhhhhvv], uhhhhhvv);
        Debug.LogFormat("[Jukebox.WAV #{0}] The song display is {1} (Which is an index of {2}).", moduleId, Fuck[poop], poop);
    }

    void ArrowsPress(KMSelectable Arrow)
    {
        Arrow.AddInteractionPunch();
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, Arrow.transform);
        if (!moduleSolved)
        {
            if (Arrow == Arrows[0])
            {
                uhhhhhvv--;
                if (uhhhhhvv < 0)
                {
                    uhhhhhvv = Fuck.Count() - 1;
                }
                Debug.Log(uhhhhhvv);
            }
            else
            {
                uhhhhhvv++;
                if (uhhhhhvv > Fuck.Count() - 1)
                {
                    uhhhhhvv = 0;
                }
                Debug.Log(uhhhhhvv);
            }
        }
    }

    void CircboysPress(KMSelectable Circboy)
    {
        Circboy.AddInteractionPunch();
        Audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, Circboy.transform);
        if (!moduleSolved)
        {
            if (Circboy == Circboys[0])
            {
                if (uhhhhhvv == poop)
                {
                    Debug.LogFormat("[Jukebox.WAV #{0}] You submitted at {1}. Module disarmed.", moduleId, Fuck[poop]);
                    moduleSolved = true;
                    GetComponent<KMBombModule>().HandlePass();
                    moduleSolved = true;
                    Audio.PlaySoundAtTransform(Shitlord[poop], transform);
                    StartCoroutine(IDONTKNOWWHATIVEBEENTOLD());
                }

                else
                {
                    TheNutDotTMCopyright++;
                }

                if (TheNutDotTMCopyright == 2)
                {
                    Debug.LogFormat("[Jukebox.WAV #{0}] You submitted at {1}. Strike, lard head.", moduleId, Fuck[poop]);
                    GetComponent<KMBombModule>().HandleStrike();
                    TheNutDotTMCopyright = 0;
                    StartCoroutine(FUCKNOOOOOOOOOOOOOOOOOOOOO());
                }

                else if (TheNutDotTMCopyright == 1)
                {
                    Debug.LogFormat("[Jukebox.WAV #{0}] You submitted at {1}. You have another chance.", moduleId, Fuck[poop]);
                    for (int i = 0; i < 2; i++)
                    {
                        Shit[i].GetComponent<MeshRenderer>().material = poooooooooo[0];
                    }
                }
            }

            else if (Circboy == Circboys[1])
            {
                uhhhhhvv = Bomb.GetSerialNumberNumbers().Last();
            }
        }
    }

    void Update()
    {
        if (moduleSolved&& !WeedChungi)
        {
            BamboozlingTimeKeeper += Time.deltaTime / Leprosy[uhhhhhvv];
            ScrewdriverHandle.transform.localPosition = Vector3.Lerp(wsdfgh, iuhfahufhiuofaijhfdjhfdljheijhfiuergiuhpegraiuverefrwhpogu, BamboozlingTimeKeeper);
            if (BamboozlingTimeKeeper >= Leprosy[uhhhhhvv])
            {
                WeedChungi = true;
            }
        }
    }

    IEnumerator FUCKNOOOOOOOOOOOOOOOOOOOOO()
    {
        Shit[0].GetComponent<MeshRenderer>().material = poooooooooo[1];
        Shit[1].GetComponent<MeshRenderer>().material = poooooooooo[1];
        yield return new WaitForSeconds(1f);
        Shit[0].GetComponent<MeshRenderer>().material = poooooooooo[4];
        Shit[1].GetComponent<MeshRenderer>().material = poooooooooo[4];
    }

    IEnumerator IDONTKNOWWHATIVEBEENTOLD()
    {
        if (check == 0)
        {
            Shit[0].GetComponent<MeshRenderer>().material = poooooooooo[3];
            Shit[1].GetComponent<MeshRenderer>().material = poooooooooo[4];
            check++;
        }

        else
        {
            check--;
            Shit[0].GetComponent<MeshRenderer>().material = poooooooooo[4];
            Shit[1].GetComponent<MeshRenderer>().material = poooooooooo[3];
        }

        yield return new WaitForSeconds(.5f);
        StartCoroutine(IDONTKNOWWHATIVEBEENTOLD());
    }

    // Twitch Plays
    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"You can move the music that is going to be played by using the command !{0} [left/right] [1-21] | You can submit by using !{0} submit | You can reset by using !{0} reset";
    #pragma warning restore 414

    string[] Validity = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

    IEnumerator ProcessTwitchCommand(string command)
    {
        string[] parameters = command.Split(' ');
        if (Regex.IsMatch(parameters[0], @"^\s*right\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (parameters.Length == 2)
            {
                foreach (char c in parameters[1])
                {
                    if (!c.ToString().EqualsAny(Validity))
                    {
                        yield return "sendtochaterror The number being submitted contains a character that is not a number.";
                        yield break;
                    }
                }

                if (parameters[1].Length > 2 || parameters[1].Length < 1)
                {
                    yield return "sendtochaterror Number being typed is longer than 2 digits or shorter than 1 digit.";
                    yield break;
                }

                if (long.Parse(parameters[1]) < 1 || long.Parse(parameters[1]) > 26)
                {
                    yield return "sendtochaterror Number is not between 1-26.";
                    yield break;
                }

                for (int x = 0; x < long.Parse(parameters[1]); x++)
                {
                    Arrows[1].OnInteract();
                    yield return new WaitForSecondsRealtime(0.1f);
                }
            }
        }

        if (Regex.IsMatch(parameters[0], @"^\s*left\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            if (parameters.Length == 2)
            {
                foreach (char c in parameters[1])
                {
                    if (!c.ToString().EqualsAny(Validity))
                    {
                        yield return "sendtochaterror The number being submitted contains a character that is not a number.";
                        yield break;
                    }
                }

                if (parameters[1].Length > 2 || parameters[1].Length < 1)
                {
                    yield return "sendtochaterror Number being typed is longer than 2 digits or shorter than 1 digit.";
                    yield break;
                }

                if (long.Parse(parameters[1]) < 1 || long.Parse(parameters[1]) > 26)
                {
                    yield return "sendtochaterror Number is not between 1-26.";
                    yield break;
                }

                for (int x = 0; x < long.Parse(parameters[1]); x++)
                {
                    Arrows[0].OnInteract();
                    yield return new WaitForSecondsRealtime(0.1f);
                }
            }
        }

        if (Regex.IsMatch(command, @"^\s*submit\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            Circboys[0].OnInteract();
        }

        else if (Regex.IsMatch(command, @"^\s*reset\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
            yield return null;
            Circboys[1].OnInteract();
        }
    }

    IEnumerator TwitchHandleForcedSolve()
    {
        while (uhhhhhvv != poop)
        {
            Arrows[1].OnInteract();
            yield return new WaitForSeconds(.1f);
        }
        Circboys[0].OnInteract();
    }
}
