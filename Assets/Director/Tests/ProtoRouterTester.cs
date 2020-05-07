using System;
using System.Collections;
using System.Collections.Generic;
using DirectorProtobuf;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class ProtoRouterTester
    {
        public class TestRoute : Routable
        {
            public DataWrapper WrapperReceived;
            public void Awake() { GetComponent<ProtoRouter>().RegisterRoute(DataWrapper.MsgOneofCase.ExecuteCommand, this); }

            public override void Route(DataWrapper wrapper) { WrapperReceived = wrapper; }
        }
        private GameObject go;

        [SetUp]
        public void setup()
        {
            go = new GameObject();
            go.AddComponent<ProtoRouter>();
            go.AddComponent<TestRoute>();
        }
        
        [TearDown]
        public void tearDown()
        {
            GameObject.Destroy(go);
        }

        [Test]
        public void ProtoRouterRouted()
        {
            DataWrapper wrapper = new DataWrapper();
            ExecuteCommand command = new ExecuteCommand();
            command.Name = "Test";
            wrapper.ExecuteCommand = command;
            go.GetComponent<ProtoRouter>().RouteProtobuf(wrapper);
            TestRoute ro = go.GetComponent<TestRoute>();
            Assert.IsNotNull(ro.WrapperReceived);
            Assert.IsTrue(wrapper.MsgCase == DataWrapper.MsgOneofCase.ExecuteCommand);
            Assert.IsTrue(wrapper.ExecuteCommand.Name.Equals("Test"));
        }
    }
}
