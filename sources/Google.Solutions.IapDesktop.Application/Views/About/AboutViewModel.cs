﻿//
// Copyright 2023 Google LLC
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


using Google.Solutions.Common.Diagnostics;
using Google.Solutions.IapDesktop.Application.ObjectModel;
using Google.Solutions.IapDesktop.Application.Services;
using Google.Solutions.Mvvm.Binding;
using System;
using System.IO;
using System.Linq;

namespace Google.Solutions.IapDesktop.Application.Views.About
{
    [Service]
    public class AboutViewModel : ViewModelBase
    {
        public AboutViewModel(IUpdateService updateService)
        {
            this.Information = $"IAP Desktop\n" +
                $"Version {updateService.InstalledVersion}\n" +
                $".NET {ClrVersion.Version}";
            this.Copyright = $"\u00a9 2019-{DateTime.Now.Year} Google LLC";

            var assembly = GetType().Assembly;
            var resourceName = assembly.GetManifestResourceNames().First(s => s.EndsWith("About.rtf"));
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                this.LicenseText = result;
            }
        }

        public string AuthorText => "Johannes Passing";

        public string AuthorLink => "https://github.com/jpassing";

        public string Information { get; }

        public string Copyright { get; }

        public string LicenseText { get; }
    }
}