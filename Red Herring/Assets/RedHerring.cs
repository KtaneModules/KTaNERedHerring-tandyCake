using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using RedHerringSL;

public class RedHerring : MonoBehaviour
{
    public KMBombInfo Bomb;
    public KMAudio Audio;
    public KMSelectable buttonSelectable;
    public GameObject buttonObject;
    public GameObject buttonWhole;
    public Material startColor;
    public Material[] answerColors;
    KMAudio.KMAudioRef sound;
    public GameObject[] NoiseMakers;
    public FakeStatusLight FakeStatusLight;
    public Transform StatusLight;

    private IDictionary<string, object> tpAPI;

    //Logging
    static int moduleIdCounter = 1;
    int moduleId;
    private bool moduleSolved = false;

    bool active = false;
    int DistractionPicker;
    private List<string> Distractions = new List<string>{"Swan","Door1","Door2","Glass","DoubleOh","Needy","DiscordCall","DiscordJoin","DiscordLeave","FuckingNothing","ButtonMove","ButtonBig"};
    float Time = 0f;
	float ActualTime;

    bool WindowofPress = false;
    bool Started = false;
    bool Pressed = false;
    bool TogglePress = false;
    bool CanPress = false;

    int[] colorIndices = new int[] { 0, 1, 2, 3 };
    private Material[] modifiedColors;
    int stageNumber;
    int[][] table = new int[][]
    {
        new int[] { 3, 2, 0, 1},
        new int[] { 1, 0, 2, 3},
        new int[] { 2, 3, 1, 0},
        new int[] { 0, 1, 3, 2},
    };
    int correctColor;
    int randomChance;

    #pragma warning disable 0649
    private bool TwitchPlaysActive;
    #pragma warning restore 0649

    void Awake()
	{
        moduleId = moduleIdCounter++;
		GetComponent<KMBombModule>().OnActivate += RedHerringInTP;
        buttonSelectable.OnInteract += delegate () { ButtonPress(); return false; };
    }

    void Start()
	{
        DistractionPicker = UnityEngine.Random.Range(0,Distractions.Count());
        FakeStatusLight = Instantiate(FakeStatusLight);
        FakeStatusLight.transform.SetParent(transform, false);
        if (GetComponent<KMBombModule>() != null)
        FakeStatusLight.Module = GetComponent<KMBombModule>();
	    FakeStatusLight.GetStatusLights(StatusLight);
        FakeStatusLight.SetInActive();

        GetColorOrder();
	}

	void RedHerringInTP()
	{
        ActualTime = TwitchPlaysActive ? 5f : 0.6f;
        if (TwitchPlaysActive)
        {
            GameObject tpAPIGameObject = GameObject.Find("TwitchPlays_Info");
            //To make the module can be tested in test harness, check if the gameObject exists.
            if (tpAPIGameObject != null)
                tpAPI = tpAPIGameObject.GetComponent<IDictionary<string, object>>();
            else
                TwitchPlaysActive = false;
        }
    }

    void GetColorOrder()
    {
        stageNumber = 0;
        colorIndices.Shuffle();
        modifiedColors = colorIndices.Select(x => answerColors[x]).ToArray();
        Debug.LogFormat("[Red Herring #{0}] The color order forseen by the module is {1}.", moduleId, modifiedColors.Select(x => x.name).Join());
        CalculateColor();
    }
    void CalculateColor()
    {
        int rownum;

        if (Bomb.GetBatteryCount() > Bomb.GetPortCount())
            rownum = 0;
        else if (Bomb.GetSerialNumberNumbers().Last() > 4)
            rownum = 1;
        else if (Bomb.GetOnIndicators().Join("").Any(x => "AEIOU".Contains(x)))
            rownum = 2;
        else
            rownum = 3;
        correctColor = table[rownum][colorIndices[0]];
        Debug.LogFormat("[Red Herring #{0}] The solution color will be {1}.", moduleId, answerColors[correctColor].name);
        randomChance = (Array.IndexOf(colorIndices, correctColor) != 0 && UnityEngine.Random.Range(0, 3) == 0) ? 1 : 0; //one third chance of playing the sound one earlier.
        
    }

	void ButtonPress()
	{
		buttonSelectable.AddInteractionPunch();
		GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, buttonSelectable.transform);
		if (TogglePress == false)
		{
			Debug.LogFormat("[Red Herring #{0}] You have started the timer.", moduleId);
			TogglePress = true;
			StartCoroutine(StartThing());
		}

		else
		{
			if (CanPress == true)
			{
				FakeStatusLight.HandlePass(StatusLightState.Green);
                buttonObject.GetComponent<MeshRenderer>().material = startColor;
				moduleSolved = true;
			}
			else
			{
				Strike();
			}
		}
	}

	void Strike()
	{
		Debug.LogFormat("[Red Herring #{0}] You presssed when the button was {1}. You pressed too early. Strike, lazy pinhead.", moduleId, modifiedColors[stageNumber].name);
		FakeStatusLight.HandleStrike();
        buttonObject.GetComponent<MeshRenderer>().material = startColor;
        GetColorOrder();
		TogglePress = false;
		DistractionPicker = UnityEngine.Random.Range(0,Distractions.Count());
		TogglePress = false;
		StopAllCoroutines();
	}

    void PlayDistraction()
    {
        switch (DistractionPicker)
        {
            case 0:
                StartCoroutine(Swan());
                break;
            case 1:
                StartCoroutine(Door1Noise());
                break;
            case 2:
                StartCoroutine(Door2Noise());
                break;
            case 3:
                StartCoroutine(GlassNoise());
                break;
            case 4:
                StartCoroutine(DoubleOhStrikeTime());
                break;
            case 5:
                StartCoroutine(NeedyDistract());
                break;
            case 6:
                StartCoroutine(Discord1());
                break;
            case 7:
                StartCoroutine(Discord2());
                break;
            case 8:
                StartCoroutine(Discord3());
                break;
            case 9:
                return;
            case 10:
                StartCoroutine(ButtonMove());
                break;
            case 11:
                StartCoroutine(ButtonBig());
                break;
        }
    }
	
	IEnumerator Swan()
	{
		yield return new WaitForSeconds(1.2f);
		while(Started == true)
		{
			yield return new WaitForSeconds(1.8f);
			Audio.HandlePlaySoundAtTransformWithRef("Swan", transform, false);
		}
	}

	IEnumerator StartThing()
	{
        while (TogglePress == true)
        {
            for (int i = 0; i < 4; i++)
            {
                Started = true;
                Time = UnityEngine.Random.Range(4f, 6f);
                if (colorIndices[(stageNumber + randomChance) % 4] == correctColor)
                    PlayDistraction();
                yield return new WaitForSeconds(Time);
                buttonObject.GetComponent<MeshRenderer>().material = modifiedColors[stageNumber];

                CanPress = (colorIndices[stageNumber] == correctColor);
                if (TwitchPlaysActive)
                {
                    tpAPI["ircConnectionSendMessage"] = string.Format("The button has changed color to {0} on Module ", modifiedColors[stageNumber].name) + GetModuleCode() + " (Red Herring)!";
                }
                stageNumber++;

                yield return new WaitForSeconds(ActualTime);
                buttonObject.GetComponent<MeshRenderer>().material = startColor;
                CanPress = false;
            }
			Started = false;
			if (moduleSolved == true)
			{
				  TogglePress = false;
				  yield return null;
			}

			else
			{
			  TogglePress = false;
			  Debug.LogFormat("[Red Herring #{0}] You didn't press in time. Strike, slow poke.", moduleId);
			  GetComponent<KMBombModule>().HandleStrike();
                GetColorOrder();
			  yield return null;
			}
		}
	}

	IEnumerator Door1Noise()
	{
		yield return new WaitForSeconds(Time - 1.5f);
        Audio.HandlePlaySoundAtTransformWithRef("Door1", transform, false);
    }

	IEnumerator Door2Noise()
	{
		yield return new WaitForSeconds(Time - 1.75f);
        Audio.HandlePlaySoundAtTransformWithRef("Door2", transform, false);
    }

	IEnumerator GlassNoise()
	{
        if (Environment.MachineName == "CREAMMACHINE") yield break;
		yield return new WaitForSeconds(Time - 1.25f);
        Audio.HandlePlaySoundAtTransformWithRef("Glass", transform, false);
    }

	IEnumerator NeedyDistract()
	{
		yield return new WaitForSeconds(Time - 3f);
		sound = GetComponent<KMAudio>().PlayGameSoundAtTransformWithRef(KMSoundOverride.SoundEffect.NeedyWarning, transform);
		yield return new WaitForSeconds(4f);
		sound.StopSound();
		sound = null;
	}

	IEnumerator DoubleOhStrikeTime()
	{
		yield return new WaitForSeconds(2.5f);
        Audio.PlayGameSoundAtTransformWithRef(KMSoundOverride.SoundEffect.ButtonPress, buttonSelectable.transform);
        buttonSelectable.AddInteractionPunch();
		Audio.PlayGameSoundAtTransformWithRef(KMSoundOverride.SoundEffect.Strike, transform);
		FakeStatusLight.FlashStrike();
        if (TwitchPlaysActive)
        {
            tpAPI["ircConnectionSendMessage"] = "VoteNay Module "+GetModuleCode()+" (Red Herring) got a strike! -6 points from MrPeanut1028 VoteNay!";
        }
	}

	IEnumerator Discord1()
	{
		yield return new WaitForSeconds(Time - 6f);
        Audio.HandlePlaySoundAtTransformWithRef("DiscordCall", transform, false);
    }

	IEnumerator Discord2()
	{
		yield return new WaitForSeconds(Time - 2f);
        Audio.HandlePlaySoundAtTransformWithRef("DiscordJoin", transform, false);
    }

	IEnumerator Discord3()
	{
		yield return new WaitForSeconds(Time - 2f);
        Audio.HandlePlaySoundAtTransformWithRef("DiscordLeave", transform, false);
    }

    IEnumerator ButtonMove()
    {
        yield return new WaitForSeconds(Time - 3.5f);
        while (buttonWhole.transform.localPosition.x < 0.04f)
        {
            buttonWhole.transform.localPosition += new Vector3(0.005f, 0, 0);
            yield return null;
        }
    }
    IEnumerator ButtonBig()
    {
        yield return new WaitForSeconds(Time - 2.5f);
        while (buttonWhole.transform.localScale.x < .008f)
        {
            buttonWhole.transform.localScale += new Vector3(0.0005f, 0.0005f, 0.0005f);
            yield return null;
        }
    }

    IEnumerator AmnesiaNoise()
    {
        yield return new WaitForSecondsRealtime(Time - 2f);
        Audio.HandlePlaySoundAtTransformWithRef("Amnes", transform, false);
    }

	//twitch plays
    #pragma warning disable 414
    private readonly string TwitchHelpMessage = @"To push the button in the module, do !{0} push. (You have to respond before 5 seconds passes after it changes color to avoid striking). Color changes will be announced in chat.";
    #pragma warning restore 414

	IEnumerator ProcessTwitchCommand(string command)
	{
		if (Regex.IsMatch(command, @"^\s*(push)|(press)\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) || Regex.IsMatch(command, @"^\s*press\s*$", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant))
        {
			yield return null;
			buttonSelectable.OnInteract();
            yield break;
		}
	}

    IEnumerator TwitchHandleForcedSolve()
    {
        if (!TogglePress)
        {
            buttonSelectable.OnInteract();
        }
        while (!CanPress)
        {
            yield return new WaitForSeconds(0.1f);
        }
        buttonSelectable.OnInteract();
    }

    private string GetModuleCode()
    {
        Transform closest = null;
        float closestDistance = float.MaxValue;
        foreach (Transform children in transform.parent)
        {
            var distance = (transform.position - children.position).magnitude;
            if (children.gameObject.name == "TwitchModule(Clone)" && (closest == null || distance < closestDistance))
            {
                closest = children;
                closestDistance = distance;
            }
        }

        return closest != null ? closest.Find("MultiDeckerUI").Find("IDText").GetComponent<UnityEngine.UI.Text>().text : null;
    }
}
