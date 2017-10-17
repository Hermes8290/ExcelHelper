﻿/*
 * Copyright (C) 2004-2017 AMain.com, Inc.
 * Copyright 2009-2013 Josh Close
 * All Rights Reserved
 * 
 * See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html
 * See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html
 */

using ExcelHelper.TypeConversion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExcelHelper.Tests.TypeConversion
{
    [TestClass]
    public class DefaultTypeConverterTests
    {
        [TestMethod]
        public void PropertiesTest()
        {
            var converter = new TestConverter();
            Assert.AreEqual(true, converter.AcceptsNativeType);
            Assert.AreEqual(typeof(double), converter.ConvertedType);
        }

        private class TestConverter : DefaultTypeConverter
        {
            public TestConverter()
                : base(true, typeof(double))
            {
            }
        }
    }
}