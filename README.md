![GitHub repo size](https://img.shields.io/github/repo-size/fernando-goncalves92/SearchSQL) 
[![Build Status](https://img.shields.io/appveyor/ci/thiagoloureiro/netcore-jwt-integrator-extension/master.svg)](https://ci.appveyor.com/project/thiagoloureiro/netcore-jwt-integrator-extension)
![Net Standard Version](https://img.shields.io/badge/.net%20standard-2.0-blueviolet)
![GitHub issues](https://img.shields.io/github/issues/fernando-goncalves92/EasierLog)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

What is the EasierLog?
=====================
A simple and asynchronous tool developed with .NET Standard 2.0 to facilitate logging in your application. Save your logs to individual files per day or to your favorite database.

For more information about .NET supported versions: https://docs.microsoft.com/en-us/dotnet/standard/net-standard

Supported DBMS:

| DBMS | Minimum Known Version | Tested At | String Connection Format | Notes |
| ------- | ----- | ----- | ----- | ----- | 
| SQL Server | Unknown | Development Environment | https://www.connectionstrings.com/ole-db-driver-for-sql-server/standard-security/ | All versions that accept "object_id()" function are supported | 
| PostgreSQL | 9.4 | https://www.db-fiddle.com/ | https://www.connectionstrings.com/pgoledb/standard/ | All versions that accept "create table if not exists" are supported | 
| MySQL | 5.5 | https://www.db-fiddle.com/ | https://www.connectionstrings.com/mysql-oledb-mysqlprov/standard/ | All versions that accept "create table if not exists" are supported | 
| Oracle | 12c | https://dbfiddle.uk/ | https://www.connectionstrings.com/oracle-provider-for-ole-db-oraoledb/standard-security/ | All versions that accept auto increment column; Attempts to create a table without checking if it exists | 

All connections to database are made using OLEDB API.

## Get Started
Install using the Nuget package manager console or the `dotnet` CLI.

```
Install-Package EasierLog 

dotnet add package EasierLog
```

After install EasierLog you need to add this to your app.confg:

```xml
<configSections>
  <section name="EasierLog" type="System.Configuration.NameValueSectionHandler"/>
</configSections>
<EasierLog>
  <add key="destinationLog" value="Database"/>
  <add key="databaseConventionPatternForTableAndColumns" value="UpperCamelCase"/>
  <add key="connectionString" value="-"/>
  <add key="databaseTableToStoreLog" value="Log"/>
  <add key="directoryToStoreLog" value="C:\Lab"/>
  <add key="daysToKeepLogFiles" value="30"/>
  <add key="saveLogFileInCaseOfDatabaseLogFail" value="true"/>
</EasierLog>
```

| Key | Options | Description | Example |
| ----- | ----- | ----- | ----- | 
| destinationLog | Database, File | The destination of the log record | - |
| databaseTableToStoreLog | Any acceptable database table name | The table that will be created in your database | - |
| databaseConventionPatternForTableAndColumns | UpperCase, LowerCase, UpperCamelCase | The convention pattern for creating columns in your database | UPPERCASE, lowercase, UpperCamelCase |
| connectionString | The connection string using OLEDB API | The connection string from your database | - |
| directoryToStoreLog | An existing directory | The directory that will store your log information | C:\MyApplicationLogs |
| daysToKeepLogFiles | A value greater than zero | For how many days the log will be kept (zero to never clean) | - |
| saveLogFileInCaseOfDatabaseLogFail | true, false | Save log file in case of database fail | - |

Using it in your app:

```csharp
using EasierLog;

EasierLogger.Info(
  "My application name", 
  "My application module", 
  "My application version", 
  "User who triggered the log",
  string.Empty, 
  "Sales order completed successfully!");
  
EasierLogger.Warning(
  "My application name", 
  "My application module", 
  "My application version", 
  "User who triggered the log", 
  string.Empty, 
  "Sales order canceled by user");

EasierLogger.Error(
  "My application name", 
  "My application module", 
  "My application version", 
  "User who triggered the log", 
  exception, 
  "Error trying to create sales order");

EasierLogger.Trace("Any string content");
```
Package Info
---
| Package |  Version | Popularity |
| ------- | ----- | ----- |
| `EasierLog` | [![NuGet](https://img.shields.io/nuget/v/EasierLog.svg)](https://nuget.org/packages/EasierLog) | [![Nuget](https://img.shields.io/nuget/dt/EasierLog.svg)](https://nuget.org/packages/EasierLog) |

## Give a Star! :star:
If you liked the project or if EasierLog helped you, give a star 😉
