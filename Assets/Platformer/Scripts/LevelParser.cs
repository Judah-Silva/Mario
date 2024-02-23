using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParser : MonoBehaviour
{
    public Transform environmentRoot;
    public GameObject rockPrefab;
    public GameObject brickPrefab;
    public GameObject questionBoxPrefab;
    public GameObject stonePrefab;
    public Material questionMarkMat;

    private Vector2 offset = new Vector2(0f, 0.4f);
    private int repeat = 0;
    
    private string filename;

    // --------------------------------------------------------------------------
    void Start()
    {
        filename = "Test";
        LoadLevel();
    }

    // --------------------------------------------------------------------------
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadLevel();
        }
        foreach (Transform child in environmentRoot)
        {
            if (child.gameObject.CompareTag("Question"))
            {
                Renderer renderer = child.gameObject.GetComponent<Renderer>();
                questionMarkMat.mainTextureOffset = offset;
                renderer.material = questionMarkMat;
            }
        }

        if (repeat % 120 == 0)
        {
            offset.y = (offset.y - 0.2f) % 1;
        }

        repeat++;
    }

    // --------------------------------------------------------------------------
    private void LoadLevel()
    {
        string fileToParse = $"{Application.dataPath}{"/Resources/"}{filename}.txt";
        Debug.Log($"Loading level file: {fileToParse}");

        Stack<string> levelRows = new Stack<string>();

        // Get each line of text representing blocks in our level
        using (StreamReader sr = new StreamReader(fileToParse))
        {
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                levelRows.Push(line);
            }

            sr.Close();
        }

        int row = 0;

        // Go through the rows from bottom to top
        while (levelRows.Count > 0)
        {
            string currentLine = levelRows.Pop();

            char[] letters = currentLine.ToCharArray();
            for (int column = 0; column < letters.Length; column++)
            {
                var letter = letters[column];
                // Todo - Instantiate a new GameObject that matches the type specified by letter
                // Todo - Position the new GameObject at the appropriate location by using row and column
                // Todo - Parent the new GameObject under levelRoot
                Vector3 newPos = new Vector3(column + 0.5f, row + 0.5f, 0f);
                if (letter == 'x')
                {
                    Instantiate(rockPrefab, newPos, Quaternion.identity, environmentRoot);
                } else if (letter == 'b')
                {
                    Instantiate(brickPrefab, newPos, Quaternion.identity, environmentRoot);
                } else if (letter == '?')
                {
                    Instantiate(questionBoxPrefab, newPos, Quaternion.identity, environmentRoot);
                } else if (letter == 's')
                {
                    Instantiate(stonePrefab, newPos, Quaternion.identity, environmentRoot);
                }
            }

            row++;
        }
    }

    // --------------------------------------------------------------------------
    public void ReloadLevel()
    {
        foreach (Transform child in environmentRoot)
        {
           Destroy(child.gameObject);
        }
        LoadLevel();
    }
}
