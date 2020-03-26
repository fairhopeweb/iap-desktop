﻿//
// Copyright 2010 Google LLC
//
// Licensed to the Apache Software Foundation (ASF) under one
// or more contributor license agreements.  See the NOTICE file
// distributed with this work for additional information
// regarding copyright ownership.  The ASF licenses this file
// to you under the Apache License, Version 2.0 (the
// "License"); you may not use this file except in compliance
// with the License.  You may obtain a copy of the License at
// 
//   http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing,
// software distributed under the License is distributed on an
// "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
// KIND, either express or implied.  See the License for the
// specific language governing permissions and limitations
// under the License.
//

using Google.Solutions.Compute;
using Google.Solutions.IapDesktop.Application.Settings;
using Google.Solutions.IapDesktop.Application.Windows.RemoteDesktop;
using NUnit.Framework;

namespace Google.Solutions.IapDesktop.Application.Test.Windows
{
    [TestFixture]
    public class TestRemoteDesktop : WindowTestFixtureBase
    {
        private readonly VmInstanceReference instanceReference =
            new VmInstanceReference("project", "zone", "instance");

        [Test]
        public void WhenServerInvalid_ThenErrorIsShownAndWindowIsClosed()
        {
            var rdpService = new RemoteDesktopService(this.serviceProvider);
            rdpService.Connect(
                this.instanceReference,
                "invalid.corp",
                3389,
                new VmInstanceSettings());

            AwaitEvent<RemoteDesktopConnectionFailedEvent>();
            Assert.IsInstanceOf(typeof(RdpDisconnectedException), this.ExceptionShown);
            Assert.AreEqual(260, ((RdpDisconnectedException)this.ExceptionShown).DisconnectReason);
        }

        [Test]
        public void WhenPortNotListening_ThenErrorIsShownAndWindowIsClosed()
        {
            var rdpService = new RemoteDesktopService(this.serviceProvider);
            rdpService.Connect(
                this.instanceReference,
                "localhost",
                1,
                new VmInstanceSettings());

            AwaitEvent<RemoteDesktopConnectionFailedEvent>();
            Assert.IsInstanceOf(typeof(RdpDisconnectedException), this.ExceptionShown);
            Assert.AreEqual(516, ((RdpDisconnectedException)this.ExceptionShown).DisconnectReason);
        }

        [Test]
        public void WhenWrongPort_ThenErrorIsShownAndWindowIsClosed()
        {
            var rdpService = new RemoteDesktopService(this.serviceProvider);
            rdpService.Connect(
                this.instanceReference,
                "localhost",
                135,    // That one will be listening, but it is RPC, not RDP.
                new VmInstanceSettings());

            AwaitEvent<RemoteDesktopConnectionFailedEvent>();
            Assert.IsInstanceOf(typeof(RdpDisconnectedException), this.ExceptionShown);
            Assert.AreEqual(2308, ((RdpDisconnectedException)this.ExceptionShown).DisconnectReason);
        }
    }
}
