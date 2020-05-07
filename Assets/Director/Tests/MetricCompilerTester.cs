using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Assert = NUnit.Framework.Assert;
using UnityEngine.TestTools;

namespace Tests
{
    public class MetricCompilerTester
    {
        private GameObject go;

        [SetUp]
        public void setup()
        {
            go = new GameObject();
            go.AddComponent<ServerConnection>();
            go.AddComponent<MetricCompiler>();
        }

        [TearDown]
        public void tearDown()
        {
            GameObject.Destroy(go);
        }
        
        [Test]
        public void MetricCompilerTesterSimplePasses()
        {
            Assert.DoesNotThrow(() => go.GetComponent<MetricCompiler>().SendData());
        }

        
    }
}
