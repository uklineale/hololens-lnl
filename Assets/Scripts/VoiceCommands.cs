using UnityEngine;
using System.Collections;
using UnityEngine.Windows.Speech;
using System.Collections.Generic;
using System;
using System.Linq;

public class VoiceCommands : MonoBehaviour {

    private KeywordRecognizer _keywordRecognizer;
    private Dictionary<string, Action> _commands;
    private Vector3 _offset;
    //private List<GameObject> _generatedList;

    public GameObject GeneratedCube;
    public GameObject GeneratedSphere;

    private GameObject DemoArea;

	// Use this for initialization
	void Start () {
        _offset = new Vector3(0, 0, 0);
        DemoArea = GameObject.Find("DemoArea");

        _commands = new Dictionary<string, Action>();
        _commands.Add("Cube", CreateCube);
        _commands.Add("Sphere", CreateSphere);

        _commands.Add("Reset", () =>
        {
            foreach (Transform child in DemoArea.transform)
            {
                GameObject.Destroy(child.gameObject);
            }
            _offset = new Vector3(0, 0, 0);
        });

        _keywordRecognizer = new KeywordRecognizer(_commands.Keys.ToArray());
        _keywordRecognizer.OnPhraseRecognized += CallCommand;
        _keywordRecognizer.Start();
    }

    void CreateShape(GameObject shape)
    {
        var genShape = Instantiate(shape,
    DemoArea.transform.position + _offset, Quaternion.identity) as GameObject;
        genShape.transform.parent = DemoArea.transform;

        genShape.GetComponent<Placement>().InitPlacement();

        _offset = _offset + new Vector3(2f, 0, 2f);
    }

    void CreateCube()
    {
        CreateShape(GeneratedCube);
    }

    void CreateSphere()
    {
        CreateShape(GeneratedSphere);
    }

	
	// Update is called once per frame
	void Update () {
	
	}

    private void CallCommand(PhraseRecognizedEventArgs args)
    {
        Action command;
        if (_commands.TryGetValue(args.text, out command))
        {
            command.Invoke();
        }
    }
}
