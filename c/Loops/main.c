#include <stdio.h>
#include <stdlib.h>

// LOOPS IN C
// All loops (for, while, do...while) have the same syntax and functionality as in C#.

int main()
{
    // FOR LOOPS
    // The value of the LCV is updated at the end of the loop.
    // The condition is checked at the beginning of the loop.

    int x;

    printf("FOR LOOP\n");

    // Loop 10 times, printing numbers 0-9.
    for ( x = 0; x < 10; x++ )
    {
        printf("%d\n", x);
    }

    // Just like in C#, any (or all) of the three sections in the for loop's condition
    //      may be left blank - just remember to include the semicolons.
    // If all three components of the condition are blank, the loop basically becomes
    //      a while (true), and requires something else to break out of.

    getchar();

    //////////////////////////////////////////////////////////////////////////////////////////

    // WHILE LOOPS
    // The condition is checked at the beginning of the loop.

    printf("WHILE LOOP\n");

    x = 0; // Pretend this is the beginning of the code (as usual)

    while ( x < 10 )
    {
        printf("%d\n", x);
        x++;
    }

    // Unlike the for loop, the condition within the while loop cannot be left empty.

    getchar();

    //////////////////////////////////////////////////////////////////////////////////////////

    // DO...WHILE LOOPS
    // Executed at least once.
    // The condition is checked at the end of the loop.
    // Therefore, if the condition is still true, the code is executed again before the
    //      condition gets checked another time.

    x = 0; // Pretend this is the beginning of the code (again)

    do
    {
        // This code executes once despite the condition being false.
        printf("Hello world!\n");
    }
    while ( x != 0 ); // Remember the semicolon!

    getchar();

    //////////////////////////////////////////////////////////////////////////////////////////

    // BREAK AND CONTINUE: A REVIEW
    // The break command tells the program to exit the loop early regardless of the LCV's
    //      current value.
    // The continue command tells the program to move on to the next loop iteration
    //      immediately, skipping the rest of the code following it for the current iteration.
    // NOTE: If continue is encountered within a for loop, the loop will update itself. This
    //      does not apply for other the other loop types.

    return 0;
}
