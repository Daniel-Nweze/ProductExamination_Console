

namespace Resources.Enums
{
    public enum ResultStatus // En enum är en statisk lista av saker och ting. Vi kan sätta olika statusdelar exempelvis för en skrivare i.e Idle, Starting Printing, etcetra.
    {
        Success,
        Exists,
        Failed,
        SuccessWithErrors,
        NotFound
    }
}
