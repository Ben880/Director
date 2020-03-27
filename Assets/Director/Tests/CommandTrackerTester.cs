using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;
using DirectorProtobuf;

namespace Tests
{
    public class CommandTrackerTester
    {
        private GameObject go;
        private CommandTracker ct;
        [SetUp]
        public void setup()
        {
            go = new GameObject();
            go.AddComponent<ServerConnection>();
            go.AddComponent<CommandTracker>();
            ct = go.GetComponent<CommandTracker>();
            Command command = new Command("Test", true);
            ct.registerCommand(command, false);
        }

        [TearDown]
        public void tearDown()
        {
            GameObject.Destroy(go);
        }
        
        // A Test behaves as an ordinary method
        [Test]
        public void registerCommand()
        {
            Assert.IsTrue(ct.commandExists("Test"));
            Assert.IsFalse(ct.commandExists("NotATest"));
        }

        [Test]
        public void executeCommand()
        {
            NotifyObject no = new NotifyObject();
            ct.addNotifyObject("Test", no);
            Assert.IsFalse(no.isTriggered());
            DirectorRPC rpc = new DirectorRPC();
            rpc.Name = "Test";
            ct.recievedCommand(rpc);
            Assert.IsTrue(no.isTriggered());
        }
        
        [Test]
        public void noKeyForNotifyObjectThrowsE()
        {
            NotifyObject no = new NotifyObject();
            try
            {
                ct.addNotifyObject("Test", no);
                Assert.IsTrue(false);
            }
            catch (Exception e)
            {
                Assert.IsTrue(true);
            }
        }
        
    }
}
