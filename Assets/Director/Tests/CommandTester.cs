using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Assert = UnityEngine.Assertions.Assert;

namespace Tests
{
    public class CommandTester
    {
        private Command co;
        private NotifyObject no;
        [SetUp]
        public void Setup()
        {
            co = new Command("Test", true);
            no = new NotifyObject();
            co.AddNotifyObject(no);
        }

        [TearDown]
        public void TearDown()
        {
            co = null;
            no = null;
        }
        [Test]
        public void BasicTests()
        {
            Assert.IsTrue(co.GetName().Equals("Test"));
            Assert.IsTrue(co.IsEnabled());
            co.SetEnabled(false);
            Assert.IsFalse(co.IsEnabled());
        }
        
        [Test]
        public void NotifyObjects()
        {
            Assert.IsFalse(no.IsTriggered());
            co.Execute();
            Assert.IsTrue(no.IsTriggered());
            no.Reset();
            Assert.IsFalse(no.IsTriggered());
            no.Notify();
            Assert.IsTrue(no.IsTriggered());
        }

        [Test]
        public void NotifyObjectsDestroy()
        {
            no.Destroy = true;
            co.Execute();
            Assert.IsFalse(no.IsTriggered());
        }


    }
}
