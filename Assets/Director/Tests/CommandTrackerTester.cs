using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = NUnit.Framework.Assert;
using DirectorProtobuf;

namespace Tests
{
    public class CommandTrackerTester
    {
        private GameObject go;
        private CommandTracker ct;
        [SetUp]
        public void Setup()
        {
            go = new GameObject();
            go.AddComponent<ServerConnection>();
            ct = go.GetComponent<CommandTracker>();
            Command command = new Command("Test", true);
            ct.RegisterCommand(command, false);
        }

        [TearDown]
        public void TearDown()
        {
            ct = null;
            GameObject.Destroy(go);
        }
        
        // A Test behaves as an ordinary method
        [Test]
        public void RegisterCommand()
        {
            Assert.IsTrue(ct.CommandExists("Test"));
            Assert.IsFalse(ct.CommandExists("NotATest"));
        }

        [Test]
        public void ExecuteCommand()
        {
            NotifyObject no = new NotifyObject();
            ct.AddNotifyObject("Test", no);
            Assert.IsFalse(no.IsTriggered());
            ExecuteCommand command = new ExecuteCommand();
            command.Name = "Test";
            ct.ReceivedCommand(command);
            Assert.IsTrue(no.IsTriggered());
        }
        
        [Test]
        public void NoKeyForNotifyDNT()
        {
            NotifyObject no = new NotifyObject();
            Assert.DoesNotThrow(() => ct.AddNotifyObject("Test", no));
        }
        

    }
}
