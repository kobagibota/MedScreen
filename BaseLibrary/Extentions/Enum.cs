namespace BaseLibrary.Extentions
{
    public enum Threshold
    {
        LessThan,
        LessThanOrEqual,
        Equal,
        GreaterThan,
        GreaterThanOrEqual
    }

    public enum ResultType
    {
        Nunmber,
        Text,
        Qualitative
    }

    public enum ResultQualitative
    {
        Negative,
        Positive
    }

    public enum QCStatus
    {
        New,
        Process,
        Finish,
        Locked
    }
    public enum Evaluate 
    {
        Normal,
        Abnormal,
        Low,
        High
    }

    public enum LogAction
    {
        Update,
        Delete
    }

    public enum LabStatus
    {
        Inactive,
        Active
    }
}
