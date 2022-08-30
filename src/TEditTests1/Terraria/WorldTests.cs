﻿using Xunit;
using TEdit.Terraria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using System.Reflection;
using System.IO;
using TEdit.MvvmLight.Threading;

namespace TEdit.Terraria.Tests
{
    public class WorldTests
    {
        static WorldTests()
        {
            TaskFactoryHelper.Initialize();
        }
        [Theory]
        [InlineData(".\\WorldFiles\\v1.3.0.1.wld")]
        [InlineData(".\\WorldFiles\\v1.2.wld")]
        [InlineData(".\\WorldFiles\\v1.2.4.wld")]
        [InlineData(".\\WorldFiles\\v1.2.4.1.wld")]
        [InlineData(".\\WorldFiles\\v1.2.3.wld")]
        [InlineData(".\\WorldFiles\\v1.2.3.1.wld")]
        [InlineData(".\\WorldFiles\\v1.2.2.wld")]
        [InlineData(".\\WorldFiles\\v1.2.1.wld")]
        [InlineData(".\\WorldFiles\\v1.2.1.2.wld")]
        [InlineData(".\\WorldFiles\\v1.2.1.1.wld")]
        [InlineData(".\\WorldFiles\\v1.2.0.3.wld")]
        [InlineData(".\\WorldFiles\\v1.2.0.3.1.wld")]
        [InlineData(".\\WorldFiles\\v1.2.0.2.wld")]
        [InlineData(".\\WorldFiles\\v1.2.0.1.wld")]
        [InlineData(".\\WorldFiles\\v1.1.wld")]
        [InlineData(".\\WorldFiles\\v1.1.2.wld")]
        [InlineData(".\\WorldFiles\\v1.1.1.wld")]
        [InlineData(".\\WorldFiles\\v1.0.wld")]
        [InlineData(".\\WorldFiles\\v1.0.6.wld")]
        [InlineData(".\\WorldFiles\\v1.0.6.1.wld")]
        [InlineData(".\\WorldFiles\\v1.0.5.wld")]
        [InlineData(".\\WorldFiles\\v1.0.4.wld")]
        [InlineData(".\\WorldFiles\\v1.0.3.wld")]
        [InlineData(".\\WorldFiles\\v1.0.2.wld")]
        [InlineData(".\\WorldFiles\\v1.0.1.wld")]
        public void LoadWorld_Test(string fileName)
        {
            var w = World.LoadWorld(fileName, showWarnings: false);
        }

        [Theory]
        [InlineData(".\\WorldFiles\\v1.3.0.1.wld")]
        [InlineData(".\\WorldFiles\\v1.2.wld")]
        [InlineData(".\\WorldFiles\\v1.2.4.wld")]
        [InlineData(".\\WorldFiles\\v1.2.4.1.wld")]
        [InlineData(".\\WorldFiles\\v1.2.3.wld")]
        [InlineData(".\\WorldFiles\\v1.2.3.1.wld")]
        [InlineData(".\\WorldFiles\\v1.2.2.wld")]
        [InlineData(".\\WorldFiles\\v1.2.1.wld")]
        [InlineData(".\\WorldFiles\\v1.2.1.2.wld")]
        [InlineData(".\\WorldFiles\\v1.2.1.1.wld")]
        [InlineData(".\\WorldFiles\\v1.2.0.3.wld")]
        [InlineData(".\\WorldFiles\\v1.2.0.3.1.wld")]
        [InlineData(".\\WorldFiles\\v1.2.0.2.wld")]
        [InlineData(".\\WorldFiles\\v1.2.0.1.wld")]
        [InlineData(".\\WorldFiles\\v1.1.wld")]
        [InlineData(".\\WorldFiles\\v1.1.2.wld")]
        [InlineData(".\\WorldFiles\\v1.1.1.wld")]
        [InlineData(".\\WorldFiles\\v1.0.wld")]
        [InlineData(".\\WorldFiles\\v1.0.6.wld")]
        [InlineData(".\\WorldFiles\\v1.0.6.1.wld")]
        [InlineData(".\\WorldFiles\\v1.0.5.wld")]
        [InlineData(".\\WorldFiles\\v1.0.4.wld")]
        [InlineData(".\\WorldFiles\\v1.0.3.wld")]
        [InlineData(".\\WorldFiles\\v1.0.2.wld")]
        [InlineData(".\\WorldFiles\\v1.0.1.wld")]
        public void SaveWorld_Test(string fileName)
        {
            var w = World.LoadWorld(fileName, showWarnings: false);

            var saveTest = fileName + ".test";
            World.Save(w, saveTest, incrementRevision: false, showWarnings: false);

            // essentially, just a save and load test
            var w2 = World.LoadWorld(saveTest, showWarnings: false);
        }
    }
}