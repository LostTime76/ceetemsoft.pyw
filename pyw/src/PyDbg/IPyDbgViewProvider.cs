namespace CeetemSoft.Pyw;

public interface IPyDbgViewProvider
{
    /// <summary>
    ///     This function is called by the debug module to obtain the python type associated with a debug view
    /// </summary>
    /// <returns>
    ///     The implementer should return a pointer to the python C type to be associated with a debug view
    /// </returns>
    public nint GetPyType();

    /// <summary>
    ///     This function is called to create and obtain a debug view for a python object. Collections
    ///     such as lists and dictionaries can only store python objects and do not neccessarily know
    ///     which subclass the object is. This function queries the type of the python object and then
    ///     creates a debug view based on the interface object associated with the type
    /// </summary>
    /// <param name="obj">
    ///     The python object to create a view for
    /// </param>
    /// <param name="name">
    ///     The name to be displayed for the field in the debugger view. Implementers store the name and
    ///     use it with the DebuggerDisplay attribute.
    /// </param>
    /// <returns>
    ///     Implementers return a newly created debug view for the python object.
    /// </returns>
    public object CreateView(PyObj obj, string name);

    /// <summary>
    ///     This function is called to obtain the value to be shown in a debug view for an object field.
    ///     For example, a boolean implemter could return 'True' for a bool python object.
    /// </summary>
    /// <returns>
    ///     The value string to be displayed for the object in the debug view
    /// </returns>
    public string GetValue(PyObj obj);
}