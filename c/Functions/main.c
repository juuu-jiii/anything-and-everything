#include <stdio.h>
#include <stdlib.h> // Includes the rand() function that will be used.

// FUNCTIONS IN C

//////////////////////////////////////////////////////////////////////////////////////////

// FUNCTION PROTOTYPES

// Functions require a prototype, consisting of the function's signature, followed by a
//      semicolon.
// It is similar to declaring an abstract method in C#. A body is not required when
//      writing the prototype.
// Think of it like declaring a variable without initialising it. The function is merely
//      being declared here, and is not defined yet.
// Function format (method signature in C#):
//      returnType funcName (arg1Type arg1,..., argNType argN);
// Functions that do not return values have a void return type. Parentheses can be left
//      empty if the function accepts no arguments.
// Here is an example of a function prototype. Notice it is declared outside of main():
int mult(int x, int y);

// Note: unlike C#, functions do not have an access modifier!
// Also note: naming conventions for functions are different compared to C - they do not
//      start with an uppercase letter.

int main()
{
    int a = rand(); // rand() is a standard function that all compilers have.

    //////////////////////////////////////////////////////////////////////////////////////////

    // EXAMPLE PROGRAM

    int x; // Pretend you-know-what (as usual)
    int y;

    printf("Input two numbers to be multiplied together: ");
    scanf("%d", &x); // Input 2 numbers separated by a space character
    scanf("%d", &y); // A newline char is inserted after scanf finishes running
    printf("The product of those numbers is %d\n", mult(x, y));

    return 0;
}

// mult() is defined below main. The compiler does not raise an error about it being
//      undeclared, because its prototype sits above main. Therefore, a function may be called
//      without definition so long as its prototype is already present before its call.
//      Despite the code compiling, it cannot run, unless the function's definition is also
//      included, as is done here.
int mult (int x, int y)
{
    return x * y;
}

// Similarly to C#, fully defining the function before it is used (in this case before main)
//      removes the need for a prototype.
// Also like in C#, you can use the return command in a void return-type function (without a
//      trailing expression, of course) to force the program to exit the function early.
