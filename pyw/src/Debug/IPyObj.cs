namespace CeetemSoft.Pyw;

/// <summary>
///     All python wrapper objects should implement this interface so that proper debugger view support
///     can be achieved.
/// </summary>
/// <remarks>
///     Python wrapper objects are structs that only hold a handle. This interface is not intended to be
///     used directly as a boxing / unboxing penalty will be incurred during runtime. This penalty does
///     not matter when debugging.
/// </remarks>
public interface IPyDbgObj
{
    /// <summary>
    ///     The debug view calls this function to retrieve the handle value for the object.
    /// </summary>
    /// <returns>
    ///     The object handle value
    /// </returns>
    public nint GetHandle();

    /// <summary>
    ///     The debug module calls this function to map an instance of the type to the actual python type.
    /// </summary>
    /// <remarks>
    ///     The function should return the exact name of the python type for proper mapping. The interpreter
    ///     can be used to find the type names. For example: bool.__name__ yields 'bool' for the bool type.
    ///     The resulting NET string: "bool" should be returned.
    /// </remarks>
    /// <returns>
    ///     Return the name of the python type.
    /// </returns>
    public string GetTypename();

    /// <summary>
    ///     The debug view calls this function when a generic python object (PyObj) is shown in a debugger
    ///     to retrieve the value object to be shown for the variable.
    /// </summary>
    /// <remarks>
    ///     A python object could be of any subclass. This functions allows a custom value to be shown instead
    ///     of the object's default string representation. This can be useful for objects such as lists where
    ///     the default string representation is a concatenation of every element within the list. The string
    ///     could be extremely long. Instead, Size = 42 could be shown.
    /// </remarks>
    /// <returns>
    ///     Return the object value to be displayed or null if no value is to be displayed
    /// </returns>
    public object GetDebugValue();
}