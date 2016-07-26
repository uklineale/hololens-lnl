using UnityEngine;
using System.Collections;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System;
using System.Linq;

public class VoiceCommands : MonoBehaviour {

    private KeywordRecognizer _keywordRecognizer;
    //private List<GameObject> _generatedList;

    public GameObject GeneratedCube;
    public GameObject GeneratedSphere;

    private GameObject DemoArea;
    private readonly string[] keywords = {"Cube", "Sphere", "Reset", "Enlarge", "Shrink", "Red", "Blue", "Yellow", "Green", "Gray", "Cyan", "Magenta", "Clear", "White", "Black"};

// Use this for initialization
void Start () {
        DemoArea = GameObject.Find("DemoArea");

        _keywordRecognizer = new KeywordRecognizer(keywords);
        _keywordRecognizer.OnPhraseRecognized += CallCommand;
        _keywordRecognizer.Start();
    }

    void CreateShape(GameObject shape)
    {
        var genShape = Instantiate(shape,
    DemoArea.transform.position, Quaternion.identity) as GameObject;
        genShape.transform.parent = DemoArea.transform;

        genShape.GetComponent<Placement>().InitPlacement();
    }

    void ChangeColor(Color color)
    {
        foreach (Transform child in DemoArea.transform)
        {
            if (child.gameObject.GetComponent<Placement>().IsSelected == true)
            {
                child.gameObject.GetComponent<Renderer>().material.color = color;
            }
        }
    }

    void Enlarge()
    {
        foreach (Transform child in DemoArea.transform)
        {
            if (child.gameObject.GetComponent<Placement>().IsSelected == true)
            {
                child.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            }
        }
    }

    void Shrink()
    {
        foreach (Transform child in DemoArea.transform)
        {
            if (child.gameObject.GetComponent<Placement>().IsSelected == true)
            {
                child.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
            }
        }
    }

    void Reset()
    {
        foreach (Transform child in DemoArea.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    private void CallCommand(PhraseRecognizedEventArgs args)
    {
        switch (args.text)
        {
            case "Cube":
                CreateShape(GeneratedCube);
                break;
            case "Sphere":
                CreateShape(GeneratedSphere);
                break;
            case "Reset":
                Reset();
                break;
            case "Enlarge":
                Enlarge();
                break;
            case "Shrink":
                Shrink();
                break;
            case "Red":
                ChangeColor(Color.red);
                break;
            case "Blue":
                ChangeColor(Color.blue);
                break;
            case "Yellow":
                ChangeColor(Color.yellow);
                break;
            case "Green":
                ChangeColor(Color.green);
                break;
            case "Grey":
                ChangeColor(Color.gray);
                break;
            case "Cyan":
                ChangeColor(Color.cyan);
                break;
            case "Magenta":
                ChangeColor(Color.magenta);
                break;
            case "Clear":
                ChangeColor(Color.clear);
                break;
            case "White":
                ChangeColor(Color.white);
                break;
            case "Black":
                ChangeColor(Color.black);
                break;
            default:
                break;
        }
    }
}
