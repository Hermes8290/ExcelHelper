﻿/*
 * Copyright (C) 2004-2017 AMain.com, Inc.
 * Copyright 2009-2013 Josh Close
 * All Rights Reserved
 * 
 * See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html
 * See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html
 */

using System;

namespace ExcelHelper.TypeConversion
{
    /// <summary>
    /// Represents errors that occur while reading a Excel file.
    /// </summary>
    public class ExcelTypeConverterException : ExcelHelperException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelTypeConverterException"/> class.
        /// </summary>
        public ExcelTypeConverterException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelTypeConverterException"/> class
        /// with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ExcelTypeConverterException(
            string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExcelTypeConverterException"/> class
        /// with a specified error message and a reference to the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ExcelTypeConverterException(
            string message,
            Exception innerException)
            : base(message, innerException)
        {
        }
    }
}