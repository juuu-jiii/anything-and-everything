#include <stdio.h>
#include <stdlib.h>

// SWITCH CASES IN C
// These are EXACTLY the same as C#'s.

void playgame()
{
    printf("playgame() called\n");
}

void loadgame()
{
    printf("loadgame() called\n");
}

void playmultiplayer()
{
    printf("playmultiplayer() called\n");
}

int main()
{
    int input;

    printf("1. Play game\n");
    printf("2. Load game\n");
    printf("3. Play multiplayer\n");
    printf("4. Exit\n");
    printf("Selection: ");
    scanf("%d", &input);

    switch (input)
    {
        case (1):
            {
                playgame();
                break;
            }
        case (2):
            {
                loadgame();
                break;
            }
        case (3):
            {
                playmultiplayer();
                break;
            }
        case (4):
            {
                printf("Thanks for playing!\n");
                break;
            }
        default:
            {
                printf("Bad input, quitting!\n");
                break;
            }

        // Recall in C# that if you do not have a break; in a case block the control will fall
        //      through to the next case block. The same applies here. However, unlike C#, the
        //      final case does not need to have a break;. The code will still compile, and
        //      the compiler will know how to exit the switch on its own.
    }

    getchar();
    return 0;
}
