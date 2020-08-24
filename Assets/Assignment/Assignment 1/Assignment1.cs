using System; 
using UnityEngine;

/// <summary>
/// A class that is made for holding some functionality that was part of the Assignment for week 1 from FutureGames.
/// </summary>
public class Assignment1 : MonoBehaviour
{
    /// <summary>
    /// The allowed maximumSize for arrays that will be created.
    /// </summary>
    [SerializeField] private int maxSizeForArray = 10;

    /// <summary>
    /// The minimum value used for the <see cref="Random.Range(float, float)"/>
    /// </summary>
    [SerializeField] private int minRandomRange = 0;

    /// <summary>
    /// The maximum value used for the <see cref="Random.Range(float, float)"/>
    /// </summary>
    [SerializeField] private int maxRandomRange = 100;

    /// <summary>
    /// An <see cref="int"/> holding the vale that should be check if its odd or even.
    /// </summary>
    private int randomNumber;

    /// <summary>
    /// A variable that should have its value swapped with <see cref="swapValueB"/>
    /// </summary>
    private int swapValueA;

    /// <summary>
    /// A variable that should have its value swapped with <see cref="swapValueA"/>
    /// </summary>
    private int swapValueB;

    /// <summary>
    /// An array that will be filled with random values and then used in all of the methods where an array is needed.
    /// </summary>
    private int[] randomFilledArray;

    /// <summary>
    /// Sets all values for the arrays and <see cref="int"/>. Then calls all the methods.
    /// </summary>
    private void Start()
    {
        randomFilledArray = new int[maxSizeForArray];

        GenerateAndApplyValues();
        OddOrEvenNumber();
        HighestAndLowestValue();
        ReverseArray();
        FizzBuzz();
        SwapValues(ref swapValueA, ref swapValueB);
    }

    /// <summary>
    /// Generates a random <see cref="int"/> between the <see cref="minRandomRange"/> and <see cref="maxRandomRange"/> values.
    /// </summary>
    /// <returns>A random <see cref="int"/>.</returns>
    private int GetRandomInt()
    {
        return UnityEngine.Random.Range(minRandomRange, maxRandomRange); 
    }

    /// <summary>
    /// A method that generates random numbers for the script.
    /// </summary>
    private void GenerateAndApplyValues()
    {
        randomNumber = GetRandomInt();
        swapValueA = GetRandomInt();
        swapValueB = GetRandomInt();

        for (int i = 0; i < maxSizeForArray; i++)
        {
            randomFilledArray[i] = GetRandomInt();
        }
    }

    /// <summary>
    /// Checks if a <see cref="int"/> is even or odd, then Logs what it is.
    /// </summary>
    private void OddOrEvenNumber()
    {
        if (randomNumber % 2 == 0)
        {
            Debug.Log(randomNumber + " is an even number");
        }
        else
        {
            Debug.Log(randomNumber + " is an odd number");
        }
    }

    /// <summary>
    /// Calculates the highest and lowest values of an array of <see cref="int"/>.
    /// Could add an parameter if the method is supposed to receive an array to check.
    /// </summary>
    private void HighestAndLowestValue()
    {
        int high = randomFilledArray[0];
        int low = randomFilledArray[0];
        string fullArray = string.Empty + randomFilledArray[0];

        for (int i = 1; i < randomFilledArray.Length; i++)
        {
            fullArray += ", " + randomFilledArray[i];

            if (randomFilledArray[i] > high)
            {
                high = randomFilledArray[i];
            }
            else if (randomFilledArray[i] < low)
            {
                low = randomFilledArray[i];
            }
        }
        
        Debug.Log(fullArray + "\n" + low + " is the lowest value and " + high + " is the highest value");
    }

    /// <summary>
    /// Reverse an array and prints out how it was before and then how it is now;
    /// Could add an parameter if the method is supposed to receive an array to check.
    /// </summary>
    private void ReverseArray()
    {
        int index = 0;

        string before = string.Empty;
        string after = string.Empty;
        int[] reversedArray = new int[randomFilledArray.Length];

        for (int i = randomFilledArray.Length - 1; i >= 0; i--)
        {
            reversedArray[index] = randomFilledArray[i];
            before = reversedArray[index] + ", " + before;
            after += reversedArray[index] + ", ";

            index++;
        }

        randomFilledArray = reversedArray;

        Debug.Log("Before: " + before.TrimEnd(new char[] { ' ', ',' }) + "\n" + "After: " + after.TrimEnd(new char[] { ' ', ',' }));
    }

    /// <summary>
    /// Runs over an array and checks if the value is a multiplication of 3 or 5 or both.
    /// Writes out all of the values but writes Fizz if its a multiplication of 3, Buzz if its a multiplication of 5 and FizzBuzz if its a multiplication of both 3 and 5.
    /// Could add an parameter if the method is supposed to receive an array to check.
    /// </summary>
    private void FizzBuzz()
    {
        string fizzbuzzString = string.Empty;
        bool boolCheck = true;

        foreach (int i in randomFilledArray)
        {
            if (i % 3 == 0)
            {
                fizzbuzzString += "Fizz";
                boolCheck = false;
            }

            if (i % 5 == 0)
            {
                fizzbuzzString += "Buzz";
                boolCheck = false;
            }

            if (boolCheck)
            {
                fizzbuzzString += i;
            }

            fizzbuzzString += ", ";
            boolCheck = true;
        }

        Debug.Log(fizzbuzzString.TrimEnd(new char[] { ' ', ',' }));
    }

    /// <summary>
    /// Swaps the value of two variables.
    /// </summary>
    /// <param name="valueA">Reference to the first variable that should be swapped with the second.</param>
    /// <param name="valueB">Reference to the second variable that should be swapped with the first.</param>
    private void SwapValues(ref int valueA, ref int valueB)
    {
        int valueHolder = valueA;
        valueA = valueB;
        valueB = valueHolder;
        Debug.Log("Value of variable A is now " + valueA + " and value of variable B is now " + valueB);
        SwapBackVariables(ref valueA, ref valueB);
    }

    /// <summary>
    /// Alternative method for swapping two values.
    /// </summary>
    /// <param name="valueA">Reference to the first value that should be swapped with the second.</param>
    /// <param name="valueB">Reference to the second value that should be swapped with the first.</param>
    private void SwapBackVariables(ref int valueA, ref int valueB)
    {
        valueA = valueA + valueB;
        valueB = valueA - valueB;
        valueA = valueA - valueB;
        Debug.Log("Value of variable A is now " + valueA + " and value of variable B is now " + valueB);
    }
}
