namespace Ambev.DeveloperEvaluation.WebApi.Common
{
    /// <summary>
    /// Represents the error response format for the API.
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// A machine-readable error type identifier.
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// A short, human-readable summary of the problem.
        /// </summary>
        public string Error { get; set; } = string.Empty;

        /// <summary>
        /// A human-readable explanation specific to this occurrence of the problem.
        /// </summary>
        public string Detail { get; set; } = string.Empty;
    }
}
