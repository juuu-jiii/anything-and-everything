#include <stdio.h>
#include <stdlib.h> // Contains function malloc()

// POINTERS IN C

// Basically the reference types of C#; they "point" to locations in memory.
// Think of a safe-deposit box. It has a number associated with it --> memory address of vars.
// A box that contains a card with the location of another box is like a pointer; a location
//      in memory (i.e. a var) storing another memory address.
// Pointers are useful when passing in large data into a function; rather than copying the
//      word-for-word, you can just substitute the pointer variable into the function.
// Pointers provide direct access to the computer's memory. More memory can be requested from
//      the system should it be required for the program. This memory is returned in the form
//      of a memory address, which must then be stored within a pointer variable.

//////////////////////////////////////////////////////////////////////////////////////////////

// POINTER SYNTAX

// Syntax is different than in the past because pointers have the ability to store the memory
//      location of the data, as well as the data contained within that memory location. Thus,
//      the compiler must know when a variable is a pointer, and the datatype it points to.

// Declaration:
// <variable_type> *<variable_name>;
// Note the use of the asterisk! This is what tells the compiler a variable is a pointer.

// Here is an example of a valid declaration:
int *points_to_integer;

// Declaring multiple variables on the same line
// This declares one pointer, and one regular int variable:
int *pointer1, nonpointer1;

// To declare multiple pointers on the same line, they must all be preceded with an *
int *pointer2, *pointer3;

//////////////////////////////////////////////////////////////////////////////////////////////

// USING THE POINTER

// A pointer is used to store a memory address. So, if used like this:
//      function_call_expecting_memory_address(pointer);
// it evaluates to the memory address.
// Using the pointer like this:
//      function_call_expecting_data(*pointer);
// evaluates to the value stored in the memory location itself. This is called dereferencing
//      the pointer.

//////////////////////////////////////////////////////////////////////////////////////////////

int main()
{
    // POINTING TO SOMETHING: RETRIEVING AN ADDRESS

    // To get a pointer to reference another variable, it is necessary to obtain the memory
    //      address of said variable.
    // Recall the scanf() function. In the second argument to the function, the variable whose
    //      value is to be changed is used, preceded by an '&'. This symbol is called the
    //      "address-of" operator. It is placed in front of variables to get the compiler to
    //      return their corresponding memory addresses, instead of their stored values.

    // Here is an example of this in action:
    int x; // Regular int var
    int *p; // Pointer to an int; "*p" (the pointer's value) is an int, so p to an int

    // Assigning the memory address of x to pointer p
    // Using & is like looking at the label on a safe-deposit box instead of inside of it.
    p = &x;

    // Storing a value in x; p can also be used here, since it effectively holds x's address.
    // The number stored in x is stored in the same location referenced by p.
    // Note the use of &. Since it is used to pass x's address in, scanf is storing the value
    //      in the same address p is pointing to. Thus, scanf works using pointers.
    printf("Enter a number: ");
    scanf("%d", &x);

    // Using *p to dereference p i.e. getting the value stored p (that is, x's address).
    // It looks up the address stored in p, and returns the value stored there.
    // Basically like looking into a safe-deposit box to find the number of, and the key to,
    //      another box, which is then opened.
    printf("%d\n", *p);

    getchar();

    //////////////////////////////////////////////////////////////////////////////////////////////

    // SOME THINGS TO NOTE:
    // Pointers must always be initialised prior to their use to prevent crashes. The OS crashes a
    //      program when an uninitialised pointer is used to prevent it from accessing memory it
    //      knows the program does not own.

    //////////////////////////////////////////////////////////////////////////////////////////////

    // THE MALLOC() FUNCTION

    // malloc() (memory allocation) is used to initialise pointers with memory from free store
    //      (a section of memory available to all programs). It accepts one argument: the amount
    //      memory to allocate (in bytes). The function then gets a blocks of memory of that size,
    //      returning a pointer to that allocated memory block.

    // Example of use:
    int *ptr1 = malloc(sizeof(int)); // Pretend
    // Like in C#, different var types have different sizes. sizeof() returns the size of a
    //      specified expression (in this case, a datatype) in bytes
    // The above line points ptr at a memory address of size int. This memory is then unavailable
    //      to other programs.
    // This memory must hence be freed at the end of its usage, lest the memory be lost to the OS
    //      for the duration of the program. This is called a memory leak; the program does not keep
    //      track of all its memory.

    // This is a cleaner example of the previous line of code:
    int *ptr2 = malloc(sizeof(*ptr2));
    // This takes the size of the variable pointed to, using the pointer directly.
    // *ptr dereferences ptr; because ptr points to an int, *ptr returns said int, and so
    //      sizeof(*ptr) returns the size of an int.
    // Rewriting the code like this reduces the number of changes that need to be made, should the
    //      datatype need to be altered at some point during the development process.

    // free() deallocates memory/returns it to the OS. It accepts one argument: the pointer whose
    //      associated memory address is to be freed/"garbage collected".
    free(ptr1);
    free(ptr2);

    // After freeing a pointer, reset it so it points to 0 (in other words, a null pointer).
    // This is to provide immediate feedback should something foolish be performed with the pointer,
    //      rather than later, after considerable damage has been done.
    ptr1 = 0;
    ptr2 = 0;

    // Null pointers are often used to indicate problems e.g. malloc() returns 0 if it cannot
    //      properly allocate memory. Make sure to handle this correctly - sometimes the OS actually
    //      runs out of memory, causing such a value to be returned!

    return 0;
}

