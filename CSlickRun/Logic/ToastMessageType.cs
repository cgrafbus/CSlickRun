using System.ComponentModel;

namespace CSlickRun.Logic
{
    public enum ToastMessageType
    {
        [Description("Warning")]
        Warning,
        [Description("Error")]
        Error,
        [Description("Info")]
        Info,
        [Description("Question")]
        Question
    }
}
