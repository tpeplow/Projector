namespace Projector.Model.Validation
{
    public class SolutionValidationFailureReason
    {
        public SolutionValidationFailureReason(SolutionValidationFailureReasons reason, string message)
        {
            Reason = reason;
            Message = message;
        }
        
        public SolutionValidationFailureReasons Reason { get; private set; }
        public string Message { get; private set; }
    }
}