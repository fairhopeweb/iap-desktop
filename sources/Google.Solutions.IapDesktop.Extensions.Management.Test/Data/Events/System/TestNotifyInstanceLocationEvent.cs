﻿//
// Copyright 2019 Google LLC
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

using Google.Solutions.IapDesktop.Extensions.Management.Data.Events;
using Google.Solutions.IapDesktop.Extensions.Management.Data.Events.System;
using Google.Solutions.IapDesktop.Extensions.Management.Data.Logs;
using Google.Solutions.Testing.Application.Test;
using NUnit.Framework;
using System;

namespace Google.Solutions.IapDesktop.Extensions.Management.Test.Data.Events.System
{
    [TestFixture]
    public class TestNotifyInstanceLocationEvent : ApplicationFixtureBase
    {
        [Test]
        public void WhenRecordContainsNodeType_ThenFieldsAreExtracted()
        {
            var json = @"
             {
               'protoPayload': {
                 '@type': 'type.googleapis.com/google.cloud.audit.AuditLog',
                 'authenticationInfo': {
                 },
                 'serviceName': 'compute.googleapis.com',
                 'methodName': 'NotifyInstanceLocation',
                 'request': {
                   '@type': 'type.googleapis.com/NotifyInstanceLocation'
                 },
                 'metadata': {
                   'serverId': '4aaaa7b32a208e7ccb4ee62acedee725',
                   'nodeType': 'c2-node-60-240',
                   'timestamp': '2020-05-04T01:50:10.917Z',
                   '@type': 'type.googleapis.com/google.cloud.audit.GceInstanceLocationMetadata'
                 }
               },
               'insertId': '-x0boqfe25xye',
               'resource': {
                 'type': 'gce_instance',
                 'labels': {
                   'instance_id': '7045222222254025',
                   'project_id': 'project-1',
                   'zone': 'us-central1-a'
                 }
               },
               'timestamp': '2020-05-04T01:50:16.885Z',
               'severity': 'INFO',
               'logName': 'projects/project-1/logs/cloudaudit.googleapis.com%2Fsystem_event',
               'receiveTimestamp': '2020-05-04T01:50:17.020301892Z'
             } ";

            var r = LogRecord.Deserialize(json);
            Assert.IsTrue(NotifyInstanceLocationEvent.IsInstanceScheduledEvent(r));

            var e = (NotifyInstanceLocationEvent)r.ToEvent();

            Assert.AreEqual(7045222222254025, e.InstanceId);
            Assert.IsNull(e.InstanceReference);
            Assert.AreEqual("INFO", e.Severity);
            Assert.IsNull(e.Status);
            Assert.AreEqual("4aaaa7b32a208e7ccb4ee62acedee725", e.ServerId);
            Assert.AreEqual("c2-node-60-240", e.NodeType.Name);
            Assert.AreEqual(new DateTime(2020, 5, 4, 1, 50, 10, 917), e.SchedulingTimestamp);
        }

        [Test]
        public void WhenRecordContainsNodeTypeButLacksLabels_ThenFieldsAreExtracted()
        {
            var json = @"
             {
               'protoPayload': {
                 '@type': 'type.googleapis.com/google.cloud.audit.AuditLog',
                 'authenticationInfo': {
                 },
                 'serviceName': 'compute.googleapis.com',
                 'methodName': 'NotifyInstanceLocation',
                 'request': {
                   '@type': 'type.googleapis.com/NotifyInstanceLocation'
                 },
                 'metadata': {
                   'serverId': '4aaaa7b32a208e7ccb4ee62acedee725',
                   'nodeType': 'c2-node-60-240',
                   'timestamp': '2020-05-04T01:50:10.917Z',
                   '@type': 'type.googleapis.com/google.cloud.audit.GceInstanceLocationMetadata'
                 }
               },
               'insertId': '-x0boqfe25xye',
               'resource': {
                 'type': 'gce_instance',
                 'labels': {
                   'instance_id': '7045222222254025',
                 }
               },
               'timestamp': '2020-05-04T01:50:16.885Z',
               'severity': 'INFO',
               'logName': 'projects/project-1/logs/cloudaudit.googleapis.com%2Fsystem_event',
               'receiveTimestamp': '2020-05-04T01:50:17.020301892Z'
             } ";

            var r = LogRecord.Deserialize(json);
            Assert.IsTrue(NotifyInstanceLocationEvent.IsInstanceScheduledEvent(r));

            var e = (NotifyInstanceLocationEvent)r.ToEvent();

            Assert.AreEqual(7045222222254025, e.InstanceId);
            Assert.IsNull(e.InstanceReference);
            Assert.AreEqual("INFO", e.Severity);
            Assert.IsNull(e.Status);
            Assert.AreEqual("4aaaa7b32a208e7ccb4ee62acedee725", e.ServerId);
            Assert.IsNull(e.NodeType);
            Assert.AreEqual(new DateTime(2020, 5, 4, 1, 50, 10, 917), e.SchedulingTimestamp);
        }

        [Test]
        public void WhenRecordLacksNodeType_ThenFieldsAreExtracted()
        {
            var json = @"
             {
               'protoPayload': {
                 '@type': 'type.googleapis.com/google.cloud.audit.AuditLog',
                 'authenticationInfo': {
                 },
                 'serviceName': 'compute.googleapis.com',
                 'methodName': 'NotifyInstanceLocation',
                 'request': {
                   '@type': 'type.googleapis.com/NotifyInstanceLocation'
                 },
                 'metadata': {
                   'serverId': '4aaaa7b32a208e7ccb4ee62acedee725',
                   'timestamp': '2020-05-04T01:50:10.917Z',
                   '@type': 'type.googleapis.com/google.cloud.audit.GceInstanceLocationMetadata'
                 }
               },
               'insertId': '-x0boqfe25xye',
               'resource': {
                 'type': 'gce_instance',
                 'labels': {
                   'instance_id': '7045222222254025',
                   'project_id': 'project-1',
                   'zone': 'us-central1-a'
                 }
               },
               'timestamp': '2020-05-04T01:50:16.885Z',
               'severity': 'INFO',
               'logName': 'projects/project-1/logs/cloudaudit.googleapis.com%2Fsystem_event',
               'receiveTimestamp': '2020-05-04T01:50:17.020301892Z'
             } ";

            var r = LogRecord.Deserialize(json);
            Assert.IsTrue(NotifyInstanceLocationEvent.IsInstanceScheduledEvent(r));

            var e = (NotifyInstanceLocationEvent)r.ToEvent();

            Assert.AreEqual(7045222222254025, e.InstanceId);
            Assert.IsNull(e.InstanceReference);
            Assert.AreEqual("INFO", e.Severity);
            Assert.IsNull(e.Status);
            Assert.AreEqual("4aaaa7b32a208e7ccb4ee62acedee725", e.ServerId);
            Assert.IsNull(e.NodeType);
            Assert.AreEqual(new DateTime(2020, 5, 4, 1, 50, 10, 917), e.SchedulingTimestamp);
        }
    }
}
