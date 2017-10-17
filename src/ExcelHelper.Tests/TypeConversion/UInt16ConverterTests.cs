﻿/*
 * Copyright (C) 2004-2017 AMain.com, Inc.
 * Copyright 2009-2013 Josh Close
 * All Rights Reserved
 * 
 * See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html
 * See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html
 */

using System.Globalization;
using ExcelHelper.TypeConversion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExcelHelper.Tests.TypeConversion
{
    [TestClass]
    public class UInt16ConverterTests
    {
        [TestMethod]
        public void PropertiesTest()
        {
            var converter = new UInt16Converter();
            Assert.AreEqual(true, converter.AcceptsNativeType);
            Assert.AreEqual(typeof(ushort), converter.ConvertedType);
        }

        [TestMethod]
        public void ConvertFromExcelTest()
        {
            var converter = new UInt16Converter();
            var typeConverterOptions = new TypeConverterOptions {
                CultureInfo = CultureInfo.CurrentCulture
            };

            Assert.AreEqual((ushort)123, converter.ConvertFromExcel(typeConverterOptions, (double)123));
            Assert.AreEqual((ushort)123, converter.ConvertFromExcel(typeConverterOptions, "123"));
            Assert.AreEqual((ushort)123, converter.ConvertFromExcel(typeConverterOptions, " 123 "));
            Assert.AreEqual((ushort)0, converter.ConvertFromExcel(typeConverterOptions, null));

            typeConverterOptions.NumberStyle = NumberStyles.HexNumber;
            Assert.AreEqual((ushort)0x123, converter.ConvertFromExcel(typeConverterOptions, "123"));

            try {
                converter.ConvertFromExcel(typeConverterOptions, "");
                Assert.Fail();
            } catch (ExcelTypeConverterException) {
            }
        }
    }
}