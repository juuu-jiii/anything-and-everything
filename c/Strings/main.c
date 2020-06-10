#include <stdio.h>      // stdin, printf, and fgets
#include <stdlib.h>
#include <string.h>     // strcmp, *strcat, *strcpy, and strlen

// C STRINGS

// There are two types of strings in C: regular strings (arrays of chars), and string literals
//      (like the ones passed into printf). The difference is that string literals cannot be
//      modified, but arrays can.

// Chars cannot be treated like strings. Arrays act like pointers when passed into functions,
//      but chars don't; the function expects a char*, NOT a char.

//////////////////////////////////////////////////////////////////////////////////////////////

// BASICS

// String literals are words surrounded by double quotation marks.
// "This is a static string"

// C-style strings are ALWAYS terminated with a null character ('\0')
// Therefore, when declaring a string of 49 chars, account for '\0' by instead making the
//      string 50 chars long.
// The string terminator is not printable, and is not counted as a letter; however, it still
//      takes up space. So, a 50-char string would hold 49 letters and 1 null char at the end.

//////////////////////////////////////////////////////////////////////////////////////////////

// INPUT

// scanf() works, but terminates once it encounters a space character. Also, because it does
//      not account for array size, buffer overflows (when the user inputs a string longer
//      than the size of the string - which acts as an input "buffer") may result.

// fgets function
// Declared in stdio.h
// Prototype:
//      char *fgets(char *str, int size, FILE* file);
// FILE* lets fgets read from any file on disk - for now, pass in stdin (STanDard INput),
//      which tells the program to read from the keyboard. str stores data read from input,
//      and size stores the size of the char*, str. fgets returns str when input gets read
//      successfully
// fgets reads up to size - 1 characters, placing '\0' after the last char it reads
// fgets will read input until it has no more room to store data OR the user hits ENTER
// fgets might fill up the allocated space for str, but will never return a str w/o '\0'

//////////////////////////////////////////////////////////////////////////////////////////////

// EXAMPLE 3 (NECESSARY FUNCTION DECLARATION HERE, CONTINUED AFTER EXAMPLES 1 AND 2)
/// Removes the newline character from the end of a string entered using fgets.
void strip_newline(char *str, int size)
{
    int i;

    // Replace newline character with null terminator.
    for (i = 0; i < size; i++)
    {
        if (str[i] == '\n')
        {
            str[i] = '\0';

            // Exit the function once the newline character has been detected.
            return;
        }
    }

    // If this line of code is hit, then no newline character was entered.
}

int main()
{
    //////////////////////////////////////////////////////////////////////////////////////////////

    // A TRICK

    // This
    char *my_string; // A pointer of type char

    // Can be used like a string, in this way:
    my_string = malloc(sizeof(*my_string) * 256); // my_string's size is an array/str of 256 chars

    // With this, it is possible to access my_string just as though it were an array.
    // Remember to free up the allocated memory from the malloc call:
    free(my_string);

    //////////////////////////////////////////////////////////////////////////////////////////////

    // EXAMPLE

    // A nice long string
    char string[256];

    printf("Enter a long string: ");

    // string: the char pointer/address of the array storing data read from input
    // Recall that arrays do not require the address operator (&) to return their addresses.
    // 256: size of the data structure above
    // Also, notice stdin being passed in.
    fgets(string, 256, stdin);

    printf("You entered a very long string, %s", string);

    getchar();

    //////////////////////////////////////////////////////////////////////////////////////////////

    // EXAMPLE 2

    // fgets includes \n when reading input, unless the string has no room for it
    // Manually removing the \n:
    char input[256];
    int i;

    fgets(input, 256, stdin);

    for(i = 0; i < 256; i++)
    {
        if (input[i] == "\n")
        {
            input[i] = "\0";
            break;
        }
    }

    // With this solution, if the loop exits before i == 256, the input is < 256 char long,
    //      and so the user must have pressed enter, inserting the newline character

    getchar();

    //////////////////////////////////////////////////////////////////////////////////////////////

    // EXAMPLE 3 (CONTINUED)

    // int strcmp: compares two strings and returns an int. This int will be -ve if str1 < str2, 0
    //      if str1 == str2, and +ve if str1 > str2.
    // char *strcat: concatenation. Two args, adds the second (src) to the first (dest). HOWEVER,
    //      it assumes dest is large enough to hold both its contents and those of src.
    // char *strcpy: copy. Two args, copies the second (src) to the first (dest) such that
    //      strcmp(dest, src) returns 0.
    // size_t strlen: returns the length of the string arg passed into it (minus the '\0'). The
    //      size_t indicates that the value returned measures the size of something (and therefore
    //      is an int that cannot be -ve).

    char name[50]; // PRETENDDDDDD
    char lastname[50];
    char fullname[100]; // Large enough to hold both name and lastname

    printf("Please enter your name: ");
    fgets(name, 50, stdin);

    // Using function defined above
    strip_newline(name, 50);

    // strcmp returns 0 if both strings being compared are equal, -ve if str1 < str2, and +ve
    //      if str1 > str2. A case-sensitive comparison is made here.
    if (strcmp(name, "Alex") == 0)
    {
        printf("That's my name too!\n");
    }
    else
    {
        printf("That's not my name.\n");
    }

    // Find the length of the name input
    printf("Your name is %d letters long\n", strlen(name));

    printf("Enter your last name: ");
    fgets(lastname, 50, stdin);
    strip_newline(lastname, 50);

    fullname[0] = '\0';

    // strcat looks for the \0 and concatenates the second string arg at that location.
    strcat(fullname, name);     // Copy name into fullname
    strcat(fullname, " ");      // Separate the names using a space
    strcat(fullname, lastname); // Copy lastname onto the end of fullname

    printf("Your full name is %s\n", fullname);

    getchar();

    return 0;
}

//////////////////////////////////////////////////////////////////////////////////////////////

// SAFE PROGRAMMING

// The four new string functions rely on the presence of '\0' at the end of a string
// Also, some of them (like strcat) assume the destination string can hold the entire string
//      being appended/concatenated at the end
// You can use analogous versions of these functions:
//      char *strncpy(char *dest, const char *src, size_t len);
//      char *strncat(char *dest, const char *src, size_t len);
// *strncpy tells the compiler to copy only len bytes from src to dest. However, len still
//      must be smaller than dest. The issue with this is that strncpy does not guarantee that
//      dest will have a null terminator attached to it (eg if strlen(src) > strlen(dest)).
// *strncat copies up to len chars from src to the end of dest, starting from the null-terminator
//      character of destination. If there is no room left in dest to store all data in src, a
//      non-null-terminated string will be returned.
