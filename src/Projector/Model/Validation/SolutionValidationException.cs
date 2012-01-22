using System;
using System.Collections.Generic;

namespace Projector.Model.Validation
{
    public class SolutionValidationException : Exception
    {
        public SolutionValidationException(IEnumerable<SolutionValidationFailureReason> failureReasons)
        {
            FailureReasons = failureReasons;
        }

        public IEnumerable<SolutionValidationFailureReason> FailureReasons { get; private set; }
    }
}