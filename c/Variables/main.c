#include <stdio.h>
#include <stdlib.h>

// VARIABLES

// The keyword "extern" declares a global variable.
// Global variables can be declared multiple times throughout the program, but can only be
//      defined once in a file, function, or code block.
// Three keywords: declare, define, initialise. Keep these in mind while working through the
//      following program.

// DECLARING variables
extern int a, b;
extern int c;
extern float f;

int main()
{
    // DEFINING variables
    int a, b;
    int c;
    float f;

    // INITIALISING variables
    a = 10;
    b = 20;

    c = a + b;
    printf("value of c: %d \n", c);

    f = 70.0 / 3.0;

    // Note the use of %f! %d returns undefined results!
    printf("value of f: %f \n", f);

    // This concept is used with functions, too; as long as a function is declared before its
    //      call, its definition can be placed anywhere in the code for it to run properly.

    return 0;
}

//////////////////////////////////////////////////////////////////////////////////////////////

// LVALUES AND RVALUES IN C

// lvalue expressions refer to a memory location, and may appear on either the left- or right-
//      hand side of an assignment.
// Example: variables
// rvalue expressions refer to a data value stored at a memory address. It cannot have a value
//      assigned to it, and so can only appear on the right-hand side of an assignment.
// Example: numeric literals, because they cannot be assigned

// int g = 20; is VALID.
// 10 = 20; is INVALID, and will cause a compile-time error, since 10 and 20 are both rvalues.
