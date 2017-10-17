﻿/*
 * Copyright (C) 2004-2017 AMain.com, Inc.
 * Copyright 2009-2013 Josh Close
 * All Rights Reserved
 * 
 * See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html
 * See LICENSE.txt for details or visit http://www.opensource.org/licenses/ms-pl.html
 */

using System;
using System.Linq;
using ExcelHelper.Configuration;
using ExcelHelper.TypeConversion;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable UnusedAutoPropertyAccessor.Local
// ReSharper disable UnusedMember.Local
// ReSharper disable ClassNeverInstantiated.Local
// ReSharper disable MemberCanBePrivate.Local

namespace ExcelHelper.Tests
{
    [TestClass]
    public class ExcelClassMappingTests
    {
        [TestMethod]
        public void MapTest()
        {
            var map = new TestMappingDefaultClass();

            Assert.AreEqual(3, map.PropertyMaps.Count);

            Assert.AreEqual("GuidColumn", map.PropertyMaps[0].Data.Names.FirstOrDefault());
            Assert.AreEqual(0, map.PropertyMaps[0].Data.Index);
            Assert.AreEqual(typeof(GuidConverter), map.PropertyMaps[0].Data.TypeConverter.GetType());

            Assert.AreEqual("IntColumn", map.PropertyMaps[1].Data.Names.FirstOrDefault());
            Assert.AreEqual(1, map.PropertyMaps[1].Data.Index);
            Assert.AreEqual(typeof(Int32Converter), map.PropertyMaps[1].Data.TypeConverter.GetType());

            Assert.AreEqual("StringColumn", map.PropertyMaps[2].Data.Names.FirstOrDefault());
            Assert.AreEqual(2, map.PropertyMaps[2].Data.Index);
            Assert.AreEqual(typeof(StringConverter), map.PropertyMaps[2].Data.TypeConverter.GetType());
        }

        [TestMethod]
        public void MapByNameTest()
        {
            var map = new TestMappingByNameClass();

            Assert.AreEqual(3, map.PropertyMaps.Count);

            Assert.AreEqual("GuidColumn", map.PropertyMaps[0].Data.Names.FirstOrDefault());
            Assert.AreEqual("IntColumn", map.PropertyMaps[1].Data.Names.FirstOrDefault());
            Assert.AreEqual("StringColumn", map.PropertyMaps[2].Data.Names.FirstOrDefault());
        }

        [TestMethod]
        public void MapNameTest()
        {
            var map = new TestMappingNameClass();

            Assert.AreEqual(3, map.PropertyMaps.Count);

            Assert.AreEqual("Guid Column", map.PropertyMaps[0].Data.Names.FirstOrDefault());
            Assert.AreEqual("Int Column", map.PropertyMaps[1].Data.Names.FirstOrDefault());
            Assert.AreEqual("String Column", map.PropertyMaps[2].Data.Names.FirstOrDefault());
        }

        [TestMethod]
        public void MapIndexTest()
        {
            var map = new TestMappingIndexClass();

            Assert.AreEqual(3, map.PropertyMaps.Count);

            Assert.AreEqual(2, map.PropertyMaps[0].Data.Index);
            Assert.AreEqual(3, map.PropertyMaps[1].Data.Index);
            Assert.AreEqual(1, map.PropertyMaps[2].Data.Index);
        }

        [TestMethod]
        public void MapIgnoreTest()
        {
            var map = new TestMappingIngoreClass();

            Assert.AreEqual(3, map.PropertyMaps.Count);

            Assert.IsTrue(map.PropertyMaps[0].Data.Ignore);
            Assert.IsFalse(map.PropertyMaps[1].Data.Ignore);
            Assert.IsTrue(map.PropertyMaps[2].Data.Ignore);
        }

        [TestMethod]
        public void MapTypeConverterTest()
        {
            var map = new TestMappingTypeConverterClass();

            Assert.AreEqual(3, map.PropertyMaps.Count);

            Assert.IsInstanceOfType(map.PropertyMaps[0].Data.TypeConverter, typeof(Int16Converter));
            Assert.IsInstanceOfType(map.PropertyMaps[1].Data.TypeConverter, typeof(StringConverter));
            Assert.IsInstanceOfType(map.PropertyMaps[2].Data.TypeConverter, typeof(Int64Converter));
        }

        [TestMethod]
        public void MapMultipleNamesTest()
        {
            var map = new TestMappingMultipleNamesClass();

            Assert.AreEqual(3, map.PropertyMaps.Count);

            Assert.AreEqual(3, map.PropertyMaps[0].Data.Names.Count);
            Assert.AreEqual(3, map.PropertyMaps[1].Data.Names.Count);
            Assert.AreEqual(3, map.PropertyMaps[2].Data.Names.Count);

            Assert.AreEqual("guid1", map.PropertyMaps[0].Data.Names[0]);
            Assert.AreEqual("guid2", map.PropertyMaps[0].Data.Names[1]);
            Assert.AreEqual("guid3", map.PropertyMaps[0].Data.Names[2]);

            Assert.AreEqual("int1", map.PropertyMaps[1].Data.Names[0]);
            Assert.AreEqual("int2", map.PropertyMaps[1].Data.Names[1]);
            Assert.AreEqual("int3", map.PropertyMaps[1].Data.Names[2]);

            Assert.AreEqual("string1", map.PropertyMaps[2].Data.Names[0]);
            Assert.AreEqual("string2", map.PropertyMaps[2].Data.Names[1]);
            Assert.AreEqual("string3", map.PropertyMaps[2].Data.Names[2]);
        }

        [TestMethod]
        public void MapConstructorTest()
        {
            var map = new TestMappingConstructorClass();

            Assert.IsNotNull(map.Constructor);
        }

        [TestMethod]
        public void MapMultipleTypesTest()
        {
            var config = new ExcelConfiguration();
            config.RegisterClassMap<AMap>();
            config.RegisterClassMap<BMap>();

            Assert.IsNotNull(config.Maps[typeof(A)]);
            Assert.IsNotNull(config.Maps[typeof(B)]);
        }

        [TestMethod]
        public void PropertyMapAccessTest()
        {
            var config = new ExcelConfiguration();
            config.RegisterClassMap<AMap>();
            config.Maps[typeof(A)].PropertyMap<A>(m => m.AId).Ignore();

            Assert.AreEqual(true, config.Maps[typeof(A)].PropertyMaps[0].Data.Ignore);
        }

        [TestMethod]
        public void PropertyMapWriteOnlyTest()
        {
            var config = new ExcelConfiguration();
            config.RegisterClassMap<AMap>();
            config.Maps[typeof(A)].PropertyMap<A>(m => m.AId).WriteOnly();

            Assert.AreEqual(true, config.Maps[typeof(A)].PropertyMaps[0].Data.WriteOnly);
        }

        [TestMethod]
        public void PropertyMapOptionalReadTest()
        {
            var config = new ExcelConfiguration();
            config.RegisterClassMap<AMap>();
            config.Maps[typeof(A)].PropertyMap<A>(m => m.AId).OptionalRead();

            Assert.AreEqual(true, config.Maps[typeof(A)].PropertyMaps[0].Data.OptionalRead);
        }

        private class A
        {
            public int AId { get; set; }
        }

        private sealed class AMap : ExcelClassMap<A>
        {
            public AMap()
            {
                Map(m => m.AId);
            }
        }

        private class B
        {
            public int BId { get; set; }
        }

        private sealed class BMap : ExcelClassMap<B>
        {
            public BMap()
            {
                Map(m => m.BId);
            }
        }

        private class TestClass
        {
            public string StringColumn { get; set; }
            public int IntColumn { get; set; }
            public Guid GuidColumn { get; set; }
            public string NotUsedColumn { get; set; }

            public TestClass()
            {
            }

            public TestClass(
                string stringColumn)
            {
                StringColumn = stringColumn;
            }
        }
		
        private sealed class TestMappingConstructorClass : ExcelClassMap<TestClass>
        {
            public TestMappingConstructorClass()
            {
                ConstructUsing(() => new TestClass("String Column"));
            }
        }

        private sealed class TestMappingDefaultClass : ExcelClassMap<TestClass>
        {
            public TestMappingDefaultClass()
            {
                Map(m => m.GuidColumn);
                Map(m => m.IntColumn);
                Map(m => m.StringColumn);
            }
        }

        private sealed class TestMappingByNameClass : ExcelClassMap<TestClass>
        {
            public TestMappingByNameClass()
            {
                Map("GuidColumn");
                Map("IntColumn");
                Map("StringColumn");
            }
        }

        private sealed class TestMappingNameClass : ExcelClassMap<TestClass>
        {
            public TestMappingNameClass()
            {
                Map(m => m.GuidColumn).Name("Guid Column");
                Map(m => m.IntColumn).Name("Int Column");
                Map(m => m.StringColumn).Name("String Column");
            }
        }

        private sealed class TestMappingIndexClass : ExcelClassMap<TestClass>
        {
            public TestMappingIndexClass()
            {
                Map(m => m.GuidColumn).Index(3);
                Map(m => m.IntColumn).Index(2);
                Map(m => m.StringColumn).Index(1);
            }
        }

        private sealed class TestMappingIngoreClass : ExcelClassMap<TestClass>
        {
            public TestMappingIngoreClass()
            {
                Map(m => m.GuidColumn).Ignore();
                Map(m => m.IntColumn);
                Map(m => m.StringColumn).Ignore();
            }
        }

        private sealed class TestMappingWriteOnlyClass : ExcelClassMap<TestClass>
        {
            public TestMappingWriteOnlyClass()
            {
                Map(m => m.GuidColumn).WriteOnly();
                Map(m => m.IntColumn);
                Map(m => m.StringColumn).WriteOnly();
            }
        }

        private sealed class TestMappingTypeConverterClass : ExcelClassMap<TestClass>
        {
            public TestMappingTypeConverterClass()
            {
                Map(m => m.GuidColumn).TypeConverter<Int16Converter>();
                Map(m => m.IntColumn).TypeConverter<StringConverter>();
                Map(m => m.StringColumn).TypeConverter(new Int64Converter());
            }
        }

        private sealed class TestMappingMultipleNamesClass : ExcelClassMap<TestClass>
        {
            public TestMappingMultipleNamesClass()
            {
                Map(m => m.GuidColumn).Name("guid1", "guid2", "guid3");
                Map(m => m.IntColumn).Name("int1", "int2", "int3");
                Map(m => m.StringColumn).Name("string1", "string2", "string3");
            }
        }
    }
}