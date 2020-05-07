using System;
using System.Collections;
using System.Collections.Generic;
using DirectorProtobuf;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = NUnit.Framework.Assert;

namespace Tests
{
    public class ServerConnectionTester
    {
        private GameObject go;

        [SetUp]
        public void setup()
        {
            go = new GameObject();
            go.AddComponent<ServerConnection>();
        }

        [TearDown]
        public void tearDown()
        {
            GameObject.Destroy(go);
        }
       
        [Test]
        public void RequieredClassesExist()
        {
          Assert.IsNotNull(go.GetComponent<CommandTracker>());
          Assert.IsNotNull(go.GetComponent<ProtoRouter>());
        }
        
        [Test]
        public void SendToServerSafe()
        {
            DataWrapper wrapper = new DataWrapper();
            DataList list = new DataList();
            wrapper.DataList = list;
            go.GetComponent<ServerConnection>().SendToServer(wrapper);
        }
        
        [Test]
        public void InvalidDataWrapper()
        {
            DataWrapper wrapper = new DataWrapper();
            go.GetComponent<ServerConnection>().SendToServer(wrapper);
            Assert.That(()=> throwsE(null), Throws.Exception);
        }

        private void throwsE(string s)
        {
            throw new Exception();
        }


    }
}
