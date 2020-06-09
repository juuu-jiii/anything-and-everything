#include <stdio.h>
#include <stdlib.h>

// ARRAYS IN C

// Same purpose, and same functionality as with C#.

//////////////////////////////////////////////////////////////////////////////////////////////

// DECLARATION

// Similar to C#, though the [] are positioned differently.
// datatype array_name[array_size];

// APPLYING ARRAYS - STRINGS (AN INTRODUCTION)

// Strings don't exist in C! Well, not as an explicit datatype like in C#, anyway. In C,
//      strings need to be manually constructed. How? Recall that they are simply an array of
//      chars. Then, the following can be done:
// char string[100];
// This creates a "string" that is 100 characters long.

//////////////////////////////////////////////////////////////////////////////////////////////

// MULTI-DIMENSIONAL ARRAYS

// Similar to C#, with the same syntax alteration as above:
// int two_dimensional_array[first_dimension_size][second_dimension_size];

//////////////////////////////////////////////////////////////////////////////////////////////

// ASSIGNING VALUES TO ELEMENTS

// Exactly the same as in C#:
// array_name[index_of_element_to_assign] = value;

// Same applies for multi-dimensional arrays:
// array_name[first_dimension_index_of_element][second_dimension_index_of_element] = value;

//////////////////////////////////////////////////////////////////////////////////////////////

int main()
{
    // EXAMPLE

    char a_string[10]; // string of length 10 char

    // scanf is not the best way to do this; more on this in the next topic.
    // The use of %s instead of %d tells scanf to read in a string, and not an int.
    // Also, notice how the '&' is absent; when an array is passed into a function, the
    //      compiler automatically converts it into a pointer to the array's first element.
    // An array without any [] will act as a pointer.
    scanf("%s", a_string);

    // Looping through array to check if character 'a' was input.
    for (int i = 0; i < 10; i++)
    {
        if (a_string[i] == 'a')
        {
            printf("You entered an 'a'!\n");
        }
    }

    getchar();

    //////////////////////////////////////////////////////////////////////////////////////////////

    // EXAMPLE 2

    int x; // PRETEND OKAY
    int y;
    int array[8][8]; // Declaring a chessboard-sized array

    // Nested for loop
    for (x = 0; x < 8; x++)
    {
        for (y = 0; y < 8; y++)
        {
            // Initialise each element.
            array[x][y] = x * y;
        }
    }

    printf("Array indices:\n");

    for (x = 0; x < 8; x++)
    {
        for (y = 0; y < 8; y++)
        {
            printf("[%d][%d] = %d", x, y, array[x][y]);
        }
        printf("\n");
    }

    getchar();

    return 0;
}

//////////////////////////////////////////////////////////////////////////////////////////////

// A FEW THINGS TO NOTE

// Arrays do not require a reference operator ('&') when creating a pointer to them
// For instance, consider this:
char *ptr1; // Creating the pointer
char str[40]; // A "string" of length 40 char
ptr1 = str; // Automatically returns the memory address without the need for use of '&'

// As opposed to this:
int *ptr2;
int num;
ptr2 = &num; // Requires use of '&' to return memory address to ptr

// Try not to access/write to elements out of an array's range; dealing with random memory can
//      have adverse unpredictable effects, though usually the OS will crash the program if it
//      tries to access unallocated memory.
