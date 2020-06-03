#include <stdio.h>
#include <stdlib.h>

// IF-STATEMENTS IN C

int main()
{
    // IF BLOCKS
    // if-statements have the same syntax as in C#
    if ( 5 < 10 )
    {
        printf("Five is now less than ten - that's a big surprise!\n");
    }

    // Just like in C#, if only one statement is to be executed should the statement
    //      evaluate to true, braces are OPTIONAL.
    if ( 5 < 10 )
        printf("Five is now less than ten - that's a big surprise!\n"); // Also valid

    // Regardless, it's better practice to just use braces anyway, so you don't
    //      accidentally forget to include them when more than one statement is
    //      supposed to run if the statement evaluates to true.


    //////////////////////////////////////////////////////////////////////////////////////////

    // ELSE BLOCKS
    // else blocks are the same:
    if ( 1 /* IF this condititon is TRUE */ )
    {
        // Execute this statement.
    }
    else
    {
        // Execute these statements otherwise (condition is FALSE).
    }

    //////////////////////////////////////////////////////////////////////////////////////////

    // ELSE IF BLOCKS
    // else if blocks are the same, too:

    int age; // Pretend this is the top of the program (again)

    printf("Enter your age\n"); // Prompt user to input age
    scanf("%d", &age); // Store input in variable age

    if ( age < 100 ) // Check this condition first.
    {
        printf("You are pretty young!\n");
    }
    else if ( age == 100 ) // Check this condition second i.e. if the first one is not met.
    {
        printf("You are old!\n");
    }
    else // This acts like a catch-all, executing if neither of the above conditions are met.
    {
        printf("You are really old!\n");
    }

    //////////////////////////////////////////////////////////////////////////////////////////

    // LOGICAL OPERATORS
    // All logical operators appear and work exactly as they do in C#
    // There IS an order of operations, however - similar to that used in arithmetic.
    // This order is:
    // 1. PARENTHESES, ()
    // 1. NOT, !; NOT is evaluated prior to both AND and OR.
    // 2. AND, &&; AND is evaluated after NOT, but prior to OR.
    // 3. OR, ||; OR is evaluated last.

    printf("%d\n", !( 1 || 0 )); // 0; note the ! outside of the parentheses
    printf("%d\n", 3 || 0); // 1; any number besides 0 is treated like a 1 (even .1 and -1)
    printf("%d\n", !( 1 || 1 && 0 )); // 0; && evaluated before ||
    printf("%d\n", !(( 1 || 0 ) && 0 )); // 0; note the double parentheses

    getchar();
    return 0;
}
