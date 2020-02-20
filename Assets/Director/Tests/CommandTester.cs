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
        public void setup()
        {
            co = new Command("Test", true);
            no = new NotifyObject();
            co.addNotifyObject(no);
        }

        [TearDown]
        public void tearDown()
        {
            co = null;
            no = null;
        }
        [Test]
        public void BasicTests()
        {
            Assert.IsTrue(co.getName().Equals("Test"));
            Assert.IsTrue(co.isEnabled());
            co.setEnabled(false);
            Assert.IsFalse(co.isEnabled());
        }
        
        [Test]
        public void NotifyObjects()
        {
            Assert.IsFalse(no.isTriggered());
            co.execute();
            Assert.IsTrue(no.isTriggered());
            no.reset();
            Assert.IsFalse(no.isTriggered());
            no.notify();
            Assert.IsTrue(no.isTriggered());
        }

        [Test]
        public void NotifyObjectsDestroy()
        {
            no.Destroy = true;
            co.execute();
            Assert.IsFalse(no.isTriggered());
        }


    }
}
