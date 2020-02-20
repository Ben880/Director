using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

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
            go.AddComponent<CommandTracker>();
            ct = go.GetComponent<CommandTracker>();
            ct.registerCommand(new Command("Test", true));
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
            ct.executeCommand("Test");
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
