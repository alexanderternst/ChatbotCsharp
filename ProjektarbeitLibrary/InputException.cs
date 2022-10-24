namespace ProjektarbeitLibrary
{
    /// <summary>
    /// Eigene Exception Klasse
    /// </summary>
    public class InputExceptionCB : Exception
        {
            public int InputErrorNum { get; set; }

            public InputExceptionCB(string message)
                : base(message)
            {

            }

            public InputExceptionCB(string message, int errorNum)
                : this(message)
            {
                InputErrorNum = errorNum;
            }
        }
}
