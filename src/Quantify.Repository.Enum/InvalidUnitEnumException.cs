using System;

namespace Quantify.Repository.Enum
{
    /// <summary>
    /// Exception thrown to indicate, that the provided unit enum is invalid.
    /// </summary>
    public class InvalidUnitEnumException : Exception
    {
        /// <summary>
        /// The type of the enum.
        /// </summary>
        public virtual Type EnumType { get; }

        /// <summary>
        /// Constructs a new instance of <see cref="InvalidUnitEnumException"/> with the given message.
        /// </summary>
        /// <param name="message">Message for the exception.</param>
        /// <param name="enumType">The type of the enum.</param>
        /// <param name="argumentName">The name of the argument related to the exception.</param>
        public InvalidUnitEnumException(string message, Type enumType) : base(message)
        {
            EnumType = enumType;
        }
    }
}