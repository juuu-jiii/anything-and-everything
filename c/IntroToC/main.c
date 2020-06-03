// Similar to C#'s Using statements, and Python's import system.
#include <stdio.h>
#include <stdlib.h>

// INTRO TO C

// Main function. Runs when program is executed. The preceding keyword int
    //      means an integer is returned by the function.
int main()
{
    // COMMENTS
    // Comments are the same as in C#. Both // and /**/ styles work.

    //////////////////////////////////////////////////////////////////////////////////////////

    // VARIABLES
    // In C, declare all variables before other types of statements in the given code block.
    // Here are some examples of valid variable declarations:
    int x;
    int a, b, c, d; // You may declare multiple variables of the same type on the same line.
    char letter;
    float the_float;
    double doubleNum;
    // int the_float; is not allowed, since a float of the same name has already been declared.

    //////////////////////////////////////////////////////////////////////////////////////////

    // BASIC SYNTAX
    // printf(); = Console.Write();!!! W/o the newline char the next print will be on the same line!
    // Note the escape sequence.
    printf("Hello world! I am alive! Beware.\n");

    // getchar(); = Console.ReadLine();
    getchar();

    // A return value of 0 means the program exited successfully.
    // Output window: "exited normally"
    // Otherwise: "exited with code XX"
    // return 0;

    //////////////////////////////////////////////////////////////////////////////////////////

    // READING INPUT
    int this_is_a_number; // Should be declared at the top but imagine this is the top for readability purposes.

    printf("Enter a number: ");

    // scanf takes a string that tells it what to look out for
    // %d here tells scanf to read in an int
    // The 2nd argument in scanf is the location of the variable in which to store the data read in; this is
    //      denoted using an '&', followed by the variable. Simply put, '&' gives scanf the location of this
    //      variable in memory.
    // Each scanf call checks the input string to see what data type to expect, and stores that input value
    //      within the variable.
    scanf("%d", &this_is_a_number);

    // This works in the same way as with Python. %d represents an int. Any decimal points input when the program
    //      runs will thus be truncated.
    // NOTE: the program DOES NOT CRASH if you input a string. The value just does not get assigned to the variable
    //      when scanf finishes running. When printf runs, the variable's default value (0) is returned instead.
    printf("You entered %d\n", this_is_a_number);

    // This type of string formatting DOES NOT WORK in C
    //printf("You entered {0}", &this_is_a_number);
    getchar();

    //////////////////////////////////////////////////////////////////////////////////////////

    // MATH
    // +, -, *, /, % (modulus) all work like expected.
    int number = 0;

    number = 4 * 6;
    // printf(number); does not work! You must manually substitute the variable's value into the output string.
    printf("%d\n", number);

    number = number + 5;
    printf("%d\n", number);

    // Both types of operations are acceptable.
    number += 5;
    printf("%d\n", number);

    // Modulus
    number %= 7;
    printf("%d\n", number);

    //////////////////////////////////////////////////////////////////////////////////////////

    // COMPARISON/RELATIONAL OPERATORS
    // With bools and comparison operators, a TRUE evaluation returns an int 1, and FALSE 0.
    // Pay attention to the string formatting used below.
    printf("%d\n", number != 5);
    printf("%d\n", number == 5);
    printf("%d\n", number > 5);
    printf("%d\n", number < 5);
    printf("%d\n", number >= 6);
    printf("%d\n", number <= 6);

    getchar();

    return 0;

    //////////////////////////////////////////////////////////////////////////////////////////

    // A FEW THINGS TO NOTE
    // Keyboard shortcut Ctrl + K + C/U doesn't seem to work.
    // Alt + Left Click Drag seems finicky. Can be used to insert tab spaces
    //     and delete chunks, but doesn't work for typing nor pasting.
}
