@echo off

rem
rem Copyright 2019 Google LLC
rem
rem Licensed to the Apache Software Foundation (ASF) under one
rem or more contributor license agreements.  See the NOTICE file
rem distributed with this work for additional information
rem regarding copyright ownership.  The ASF licenses this file
rem to you under the Apache License, Version 2.0 (the
rem "License"); you may not use this file except in compliance
rem with the License.  You may obtain a copy of the License at
rem 
rem   http://www.apache.org/licenses/LICENSE-2.0
rem 
rem Unless required by applicable law or agreed to in writing,
rem software distributed under the License is distributed on an
rem "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
rem KIND, either express or implied.  See the License for the
rem specific language governing permissions and limitations
rem under the License.
rem

rem Change to the directory where this script is located in
cd %~dp0

rem Invoke build.
powershell -NoProfile -ExecutionPolicy Bypass -File build.ps1 %*

rem Remove BOM to make file compatible with Sponge.
set RETURNVALUE=%ERRORLEVEL%
echo "Build exited with exit code %ERRORLEVEL%"

if exist sponge_log.xml (
    echo "Removing BOM from sponge_log.xml"
    powershell -NoProfile -ExecutionPolicy Bypass -File scripts\strip-bom.ps1 sponge_log.xml
)

exit /b %RETURNVALUE%