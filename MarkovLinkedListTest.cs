
/*
* Markov Text Generation 
* Create a story
*
*file: MarkovLinkedList.cs
* author: Dylan Fox, Teagan Algarra, Madeline Stokes, Kate Pla
*/

using MyLinkedListLibrary;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System;

public class MarkovLinkedListTest
{
    
    static void Main(string[] args)
    {
        // failsafe if number of arguments is wrong
        if (args.Length < 1 || args.Length > 3)
        {
            Console.WriteLine("Incorrect number of arguments! Please try again with the following format: ");
            Console.WriteLine("{text file} {markov degree} {output length}");
            return;
        }
        Stopwatch stopwatch = new Stopwatch();

        // starts timer
        stopwatch.Start();

        // splits text into single word array
        string[] splitText = File.ReadAllText(args[0]).Split(" ");

        int markovDegree = int.Parse(args[1]);

        int wordLength = int.Parse(args[2]);

        SortedList<string, MarkovEntry> testDict = new SortedList<string, MarkovEntry>();

        // iterates through array, adding to dictionary each time
        for (int x = 0; x < splitText.Length - markovDegree; x++)
        {
            string result = String.Join(" ", splitText, x, markovDegree);

            if (testDict.ContainsKey(result))
            {
                // accesses the MarkovEntry.Add() function
                testDict[result].Add(splitText[x + 2]);
            }
            else
            {
                testDict.Add(result, new MarkovEntry(result));
                testDict[result].Add(splitText[x + 2]);
            }
        }

        // seed text
        string output = "I don't";

        for (int i = 2; i < wordLength; i++)
        {
            string[] words = output.Split(' ');
            string key = words[i - 2] + " " + words[i - 1];

            // failsafe for when key does not exist
            while (!testDict.ContainsKey(key))
            {
                key = RandomKey();
            }

            string randomValue = testDict[key].WeightedRandomWord();
            output += " " + randomValue;

        }

        // to make sure that the output ends in a period
        int y = 2;

        while (!output.EndsWith("."))
        {
            string[] words = output.Split(' ');
            string key = words[y - 2] + " " + words[y - 1];

            // failsafe for when key does not exist
            while (!testDict.ContainsKey(key))
            {
                key = RandomKey();
            }

            string randomValue = testDict[key].WeightedRandomWord();
            output += " " + randomValue;
            y++;
        }

        Console.WriteLine(output);
        Console.WriteLine("");

        // stop stopwatch and print the milliseconds
        stopwatch.Stop();
        Console.WriteLine($"The code took {stopwatch.ElapsedMilliseconds} milliseconds or {stopwatch.Elapsed} seconds to execute.");

        // size of file
        long length = new FileInfo(args[0]).Length;
        long kb = length / 1000;

        Console.WriteLine($"The size of the file was {length} bytes or {kb} kB.");
        // failsafe that selects a random key in case one does not exist
        string RandomKey()
        {
            Random rng = new Random();
            int index = rng.Next(testDict.Count);
            return testDict.Keys.ElementAt(index);
        }
    }
}