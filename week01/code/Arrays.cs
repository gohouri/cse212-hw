public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Process for solving MultiplesOf:
        // Step 1: Create a new array of size 'length' to store the multiples
        // Step 2: Use a loop that runs 'length' times
        // Step 3: For each iteration i (0 to length-1), calculate the multiple as number * (i + 1)
        //         - i=0: number * 1 = number (first multiple)
        //         - i=1: number * 2 (second multiple)
        //         - i=2: number * 3 (third multiple)
        //         - and so on...
        // Step 4: Store each calculated multiple in the array at index i
        // Step 5: Return the array with all multiples

        double[] result = new double[length];
        
        for (int i = 0; i < length; i++)
        {
            result[i] = number * (i + 1);
        }
        
        return result;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // Process for solving RotateListRight:
        // Step 1: Calculate the split point. The list needs to be divided into two parts:
        //         - The last 'amount' elements will move to the front
        //         - The first 'data.Count - amount' elements will move to the back
        // Step 2: Get a copy of the last 'amount' elements using GetRange
        //         - Use GetRange(data.Count - amount, amount) to get elements from index (data.Count - amount) to the end
        // Step 3: Get a copy of the first (data.Count - amount) elements using GetRange
        //         - Use GetRange(0, data.Count - amount) to get elements from the beginning
        // Step 4: Clear the original list to make it empty
        // Step 5: Add the last portion first (the elements that will rotate to the front)
        // Step 6: Add the first portion (the elements that will be pushed to the back)
        // Result: The list now has the last 'amount' elements at the front, followed by the rest

        // Get the last 'amount' elements that will move to the front
        List<int> lastElements = data.GetRange(data.Count - amount, amount);
        
        // Get the first (data.Count - amount) elements that will move to the back
        List<int> firstElements = data.GetRange(0, data.Count - amount);
        
        // Clear the list and rebuild it with the elements in the new order
        data.Clear();
        data.AddRange(lastElements);  // Add the last elements first (they rotate to front)
        data.AddRange(firstElements);  // Add the first elements (they move to the back)
    }
}
