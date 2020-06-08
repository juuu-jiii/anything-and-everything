#include <stdio.h>
#include <stdlib.h>

// STRUCTURES IN C

// Structs (full: structures) are used to store diff values in vars of potentially diff types
//      under the same name.
// Recall: classes vs structs in C#. What makes these two similar? What makes them different?
// Useful whenever a considerable amount of data exists that needs to be grouped together.

//////////////////////////////////////////////////////////////////////////////////////////////

// DEFINITION

// Similar to C#:
// struct struct_name
// {
//      struct_members;
// };

// NOTICE the semicolon after the closing brace!

//////////////////////////////////////////////////////////////////////////////////////////////

// DECLARATION

// Similar to C#:
// struct struct_name name_of_single_struct;

// NOTICE how struct is still included at the beginning of the declaration!

//////////////////////////////////////////////////////////////////////////////////////////////

// DATA ACCESS

// Accessing a variable belonging to a struct is exactly the same as in C#:
// name_of_single_struct.variable_name;

//////////////////////////////////////////////////////////////////////////////////////////////

// EXAMPLE PROGRAM

struct database
{
    // ONLY declare! No need to initialise!
    int id_number;
    int age;
    float salary;
}; // semicolon! (but IDE adds it for you though - still, take note)

// Writing functions with struct return types:
// struct database function();
// The above line of code returns a struct following the database struct blueprint already
//      defined previously.

// (USED IN EXAMPLE PROGRAM 2)
struct example
{
    int x;
};

int main()
{
    struct database employee; // Treated like a normal var, except with struct keyword prior

    // Variables contained within the employee struct variable CAN be modified!
    // How is this different from C#?
    employee.age = 22;
    employee.id_number = 1;
    employee.salary = 12000.21;

    //////////////////////////////////////////////////////////////////////////////////////////////

    // EXAMPLE PROGRAM 2

    struct example structure; // pretend!!
    struct example *ptr;

    structure.x = 12;

    // Recall: & gives the memory address of the corresponding variable
    // This applies to structs, as with other datatypes, too.
    ptr = &structure;

    // To access data stored in a struct from a pointer, use "->" instead of '.'
    // "->" acts like * when it is used with pointers. It returns the contents of the associated
    //      memory address, and NOT the memory address itself.
    printf("%d\n", ptr->x);

    getchar();

    return 0;
}

//////////////////////////////////////////////////////////////////////////////////////////////

// UNIONS

// Are like structs, except all variables share the same memory. When one is declared, the
//      compiler allocates memory equal to the largest datatype. Like a storage chest where
//      you can store one big item or one small item, but not both at the same time.
