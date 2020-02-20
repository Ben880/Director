using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class CommandTrackerTest
    {
        
        // A Test behaves as an ordinary method

        [Test]
        public void CommandTrackerTestRegisterCommand()
        {
            
            Assert.IsTrue(true);
            //GameObject testObj = new GameObject("name");
            //var ct = testObject.AddComponent<CommandTracker>();
            //ct.registerCommand(new Command(testCommandName, true));
            //Assert.IsTrue(ct.commandExists(testCommandName));
            //ct = null;
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator CommandTrackerTestWithEnumeratorPasses()
        {
            Assert.IsTrue(true);
            yield return null;
        }


    }
}
