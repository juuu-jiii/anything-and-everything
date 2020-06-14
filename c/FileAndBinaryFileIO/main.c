#include <stdio.h>
#include <stdlib.h>

// C FILE I/O AND BINARY FILE I/O

// C file I/O requires a FILE pointer, which points to the memory address of the file being
//      accessed by the program, and allows the program to keep track of said file during
//      during runtime.
// eg: FILE *fp;

//////////////////////////////////////////////////////////////////////////////////////////////

// fopen

// Opens a file, which can then be used for reading/writing/a combination of the two whatever

// FILE *fopen(const char *filename, const char *mode);

// Remember to use the escape sequence \\ when typing in filenames (if using a string literal)
// This is to avoid accidentally using other escape sequences, like \n and \t.

///////////////////////////////////////////////

// fopen modes

// r - read
// w - write (file DOES NOT need to exist)
// a - append (file DOES NOT need to exist)
// r+ - read AND write (start at beginning)
// w+ - read AND write (owerwrite file)
// a+ - read AND write (append if file exists)

// fopen might fail (the file might not exist, or might be write-protected, for example). In
//      such cases, fopen will return 0 (null pointer)

// example of fopen in use:

// FILE *fp;
// fp = open("c:\\test.txt", "r");

// This opens test.txt for reading in text mode.
// To open a file in binary mode, add 'b' after the mode string (rb, r+b, rb+ are all valid)

//////////////////////////////////////////////////////////////////////////////////////////////

// fclose

// Close a file once finished working with it. Use the function

// int fclose(FILE *a_file); (the * before a_file means a pointer needs to be passed in; pay
//      close attention to what is being specified!)

// fclose returns 0 if file closure is successful.

// Example usage:
// fclose(fp);

//////////////////////////////////////////////////////////////////////////////////////////////

// READING AND WRITING WITH fprintf, fscanf, fputc, and fgetc

// writing: use fprintf/fputc
// reading: use fscanf/fgetc

// fprintf and fscanf are similar to printf and scanf respectively, but a FILE pointer must be
//      passed in as a first argument:

// FILE *fp;
// fp = fopen("c:\\test.txt", "w"); (opening in write mode)
// fprintf(fp, "Testing ...\n");

// fgetc reads files char-by-char:

// int fgetc(FILE *fp);

// int return: when reading a normal char in the file, fgetc returns a value suitable for
//      storing in an unsigned char (int value 0-255). When at the end of a file, no more
//      chars exist, and so fgetc returns "EOF", a const indicating the end of the file has
//      been reached. An example on this coming in the topic after the next.

// fputc writes a char at a time - can be handy if copying a file char-by-char

// int fputc(int c, FILE *fp);

// Note 1st arg should be in range of an unsigned char (0-255) so it is valid. 2nd arg is the
//      file to write to. If successful, fputc returns the value c (the 1st arg), and EOF
//      otherwise.

//////////////////////////////////////////////////////////////////////////////////////////////

// BINARY FILE I/O - FREAD AND FWRITE

// Declarations for each function are similar:
// size_t fread(void *ptr, size_t size_of_elements, size_t number_of_elements, FILE *a_file);
// size_t fwrite(const void *ptr, size_t size_of_elements, size_t number_of_elements, FILE *a_file);

// So really the only difference is the first argument
// Notice that the functions accept pointers as arguments; thus, data structures like structs
//      and arrays can be written to files or read into memory.

///////////////////////////////////////////////

// fread

// size_t fread(void *ptr, size_t size_of_elements, size_t number_of_elements, FILE *a_file);

// void *ptr: void means it is a wildcard pointer, and that it can be used for any type variable.
// 1st arg (void *ptr) is the name of the array or the address of the struct to be written to
//      the file
// 2nd arg (size_t size_of_elements) is the size of each element of the array in bytes
//      eg: given an array of chars, they will be read in 1-byte chunks, so size_of_elements = 1
//      sizeof() can be used to get sizes of various datatypes; given a var int x, use sizeof(x)
//      to obtain the size of x. This works with structs and arrays, too. For a struct var
//      a_struct, use sizeof(a_struct) to determine the amount of memory it occupies.
//      sizeof(int) also works.
// 3rd arg (size_t number_of_elements) is how many elements to read or write eg if passing in
//      an array with 100 elements, the 3rd arg would be 100, so the program reads no more
//      than 100 elements (also to avoid index out of range hooha)
// 4th arg (FILE *a_file) is the file pointer being used. After fread is passed an array, it
//      reads from the file until the array is filled, returning the number of elements
//      actually read. If the filesize is 30 bytes but fread is instructed to read 100 it will
//      return that it only read 100 bytes.

// To check if the end of file was reached, use feof(), which accepts a FILE pointer, and
//      returns true of the end of the file was reached.

///////////////////////////////////////////////

// fwrite

// Similar in usage to fread, except now instead of reading from memory the program writes
//      from memory into a file.

// Example:

// FILE *fp;
// fp = fopen("c:\\test.bin", "wb"); ('b' added - binary IO)
// char x[10] = "ABCDEFGHIJ";
// fwrite(x, sizeof(x[0]), sizeof(x) / sizieof(x[0]), fp);

// Look at the second and third third arguments. The second is the size of EACH array element,
//      so just checking the first will do, since an array only holds elements of the same
//      datatype. The third is HOW MANY elements to read/write. Simple math here: just divide
//      the total size of x by the size of one of x's elements to get the number of elements
//      contained within x.



int main()
{
    printf("Hello world!\n");
    return 0;
}
