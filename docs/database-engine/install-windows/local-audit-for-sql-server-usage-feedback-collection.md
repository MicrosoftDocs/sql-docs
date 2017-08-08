---
title: "Local Audit for SQL Server Usage Feedback Collection | Microsoft Docs"
ms.custom: ""
ms.date: "02/28/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "dbe-security"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "Local Audit"
ms.assetid: a0665916-7789-4f94-9086-879275802cf3
caps.latest.revision: 8
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "jhubbard"
---
# Local Audit for SQL Server Usage Feedback Collection
## Introduction

Microsoft SQL Server contains Internet-enabled features that can collect and send information about your computer or device ("standard computer information") to Microsoft. The Local Audit component of [SQL Server Usage Feedback collection](http://support.microsoft.com/kb/3153756) writes data collected by the service to a designated folder, representing the data (logs) that will be sent to Microsoft. The purpose of the Local Audit is to allow customers to see all data Microsoft collects with this feature, for compliance, regulatory or privacy validation reasons.  

As of SQL Server 2016 CU2, Local Audit is configurable at instance level for SQL Server Database Engine and Analysis Services (SSAS). In SQL Server 2016 CU4 and SQL Server 2016 SP1, Local Audit is also enabled for SQL Server Integration Services (SSIS). Other SQL Server components that get installed during Setup and SQL Server Tools that are downloaded or installed after Setup do not have Local Audit capability for usage feedback collection. 

## Prerequisites 

The following are prerequisites to enable Local Audit on each SQL Server instance: 

1. The instance is patched to SQL Server 2016 RTM CU2 or above. 

1. User must be a System Administrator or a role with access to add and modify Registry Key, create folders, manage folder security and stop/start a Windows Service.  

## Pre-configuration Steps Prior To Turning On Local Audit 

Before turning on Local Audit, a system administrator needs to:

1. Know the SQL Server instance name and the SQL Server CEIP Telemetry service logon account. 

1. Configure a new folder for the Local Audit files.

1. Grant permissions to the SQL Server CIEP Telemetry service logon account.

1. Create a registry key setting to configure Local Audit target directory. 

    For Database Engine and Integration Services create the key at *HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Microsoft SQL Server\\MSSQL14.\<INSTANCENAME\>\\CPE*. 
    
    For Analysis Services create the key at *HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Microsoft SQL Server\\MSAS14.\<INSTANCENAME\>\\CPE*.

### Get the SQL Server CEIP Service Logon Account

Do the following steps to get the SQL Server CEIP Telemetry service logon account
 
1. Launch **Services** - click on the **Windows**  button and type *services.msc*. 

2. Navigate to the appropriate service. For example, for the database engine locate **SQL Server CEIP service *instance name***. For Analysis Services locate **SQL Server Analysis Services CEIP *instance name***. For Integration Services locate **SQL Server Integration Services CEIP service 13**.

3. Right-click on the service and choose **Properties**. 

4. Click on the **Log On** tab. The Logon account is in listed in **This Account**. 

### Configure a new folder for the Local Audit files.    

Create a new folder (Local Audit Directory) where the Local Audit will write the logs. For example, the complete path to the Local Audit Directory for a default instance of the database engine would be: *C:\\SQLCEIPAudit\\MSSQLSERVER\\DB\\*. 
 
> Note: Configure the directory path for Local Audit outside the SQL Server installation path to avoid allowing auditing functionality and patching to cause potential problems with SQL Server.

  ||Design Decision|Recommendation|  
  |------|-----------------|----------|  
  |![Checkbox](../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Space availability |On moderate workload with about 10 databases, plan on about 2 MB of disk space per day per instance.|  
|![Checkbox](../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Separate directories | Create a directory for each instance. For example, use *C:\\SQLCEIPAudit\\MSSQLSERVER\\DB\\* for a SQL Server instance named `MSSQLSERVER`. This simplifies file management.
|![Checkbox](../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Separate folders |Use a specific folder for each service. For example for a given instance name, have one folder for the database engine. If an instance of SSAS uses the same instance name, create a separate folder for SSAS. Having both Database Engine and Analysis Services instances configured to the same folder will cause all the Local Audit to write to the same log file from both instances.| 
|![Checkbox](../../database-engine/availability-groups/windows/media/checkboxemptycenterxtraspacetopandright.gif "Checkbox")|Grant permissions to the SQL Server CIEP Telemetry service logon account|Enable **List folder contents**, **Read** and **Write** access to the SQL Server CEIP Telemetry service logon account|


### Grant permissions to the SQL Server CIEP Telemetry service logon account
  
1. In **File Explorer**, navigate to the location where the new folder is located.  

1. Right-click the new folder and choose **Properties**. 

1. On the **Security tab**, click **Edit** manage Permission.

1. Click **Add** and type the credential of the SQL Server CEIP Telemetry Service, for example `NT Service\SQLTELEMETRY`.   

1. Click on **Check Names** to validate the name you provided, then click **OK**. 

1. On the **Permission** dialog box, choose the Log On account to SQL Server CEIP Telemetry service and click on **List folder contents**, **Read** and **Write**.  

1. Click **OK** to apply the permission changes immediately. 
  
### Create a registry key setting to configure Local Audit target directory.

1. Launch regedit.  

1. Navigate to the appropriate CPE path. 

    For Database Engine and Integration Services use *HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Microsoft SQL Server\\MSSQL14.\<INSTANCENAME\>\\CPE*. 
    
    For Analysis Services use *HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Microsoft SQL Server\\MSAS14.\<INSTANCENAME\>\\CPE*.

1. Right-click on the CPE path and choose **New**. Click **String Value**.

1. Name the new registry key `UserRequestedLocalAuditDirectory`. 
 
## Turning Local Audit on or off

After you have completed the preconfiguration steps you can turn Local Audit on. To do this, use a System Administrator account or a similar role with access to modifying Registry Keys to turn Local Audit on or off by following the steps below. 

1. Launch **regedit**.  

1. Navigate to the appropriate CPE path. 

    For Database Engine and Integration Services use *HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Microsoft SQL Server\\MSSQL14.\<INSTANCENAME\>\\CPE*. 
    
    For Analysis Services use *HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Microsoft SQL Server\\MSAS14.\<INSTANCENAME\>\\CPE*.

1. Right-click on **UserRequestedLocalAuditDirectory** and click *Modify*. 

1. To turn Local Audit on, type the Local Audit path, for example *C:\\SQLCEIPAudit\\MSSQLSERVER\\DB\\*.
 
    To turn Local Audit off, empty the value in **UserRequestedLocalAuditDirectory**.

1. Close **regedit**. 

SQL Server CEIP should recognize the Local Audit setting immediately if the service is already running. To start the SQL Server CEIP Service, System Administrator or someone who has access to start or stop Windows Services can follow the steps below: 

1. Launch Services application, by clicking on the Windows button and type Services. 

1. Navigate to the appropriate service. 

    For Database Engine, use **SQL Server CEIP service (\<INSTANCENAME\>)**. 
    
    For Analysis Services, use **SQL Server Analysis Services CEIP (\<INSTANCENAME\>)**. 

1. Right-click on the service and choose Restart. 

1. Verfiy that the status of the service is **Running**. 

Local Audit will produce one log file per day. The log files will be in a form of `<YYYY-MM-DD>.json`. For example, *2016-07-12.json*. If there is an existing file for the day in the designated directory, Local Audit will append to it. Otherwise, it will create a new file for the day. 

> Note: After enabling Local Audit it may take up to 5 minutes for the log file to be written for the first time. 

## Maintenance 

1. To limit disk space usage by the files written by Local Audit, setup a policy or a regular job to clean up the Local Audit Directory to remove older, unneeded files.  

2. Secure the Local Audit Directory path so that it is only accessible by the appropriate people. Please note that the log files contain information as outlined in [How to configure SQL Server 2016 to send feedback to Microsoft](http://support.microsoft.com/kb/3153756). Access to this file should prevent most members of your organization from reading it.  

## Data Dictionary of Local Audit Output Data Structure 

- Local Audit log files are in JSON, containing a set of objects (rows) representing data points that are sent back to Microsoft at **emitTime**.  

- Each row follows a specific schema identified by **schemaVersion**.   

- Each row is an output of a SQLCEIP service session identified as **sessionID**.  

- Rows are emitted in sequence, identified by **sequence**. 

- Each data point row contains the output of a **queryIdentifier** which can be a T-SQL query, an XE session or a message related to a type of trace, identified as **traceName**.   

- **queryIdentifiers** are grouped and versioned together with **querySetVersion**. 

- **data** contains the output of the corresponding query execution which took **queryTimeInTicks**. 

- **queryIdentifiers** for T-SQL queries have the T-SQL query definition stored in query. 


| Logical Local Audit information hierarchy | Related columns |
| ------ | -------|
| Header | emitTime, schemaVersion 
| Machine | hostname, domainHash, sqmID, operatingSystem 
| Instance | instanceName, correlationID, clientVersion 
| Session | sessionID, traceName 
| Query | sequence, querySetVersion, queryIdentifier, query, queryTimeInTicks 
| Data |  data 

### Name/Value Pairs Definition and Examples 

The columns listed below represents the order of the Local Audit file output. One-way hash with SHA 256 is used to anonymized values for a number of the columns below.  

| Name | Description | Example values
|-------|--------| ----------|
|hostname | Anonymized machine name where SQL Server is installed| de3b3769a63970b63981ab7a956401388962c986bfd39d371f5870d800627d11 
|domainHash| Anonymized domain hash of the machine hosting SQL Server instance | de3b3769a63970b63981ab7a956401388962c986bfd39d371f5870d800627d11 
|sqmId |Identifier representing the machine where SQL Server is installed | 02AF58F5-753A-429C-96CD-3900E90DB990 
|instanceName| Anonymized SQL Server instance name| e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855 
|SchemaVersion| Schema version of SQLCEIP |  3 
|emitTime |Data point emit time in UTC | 2016-09-08T17:20:22.1124269Z 
|sessionId | Session identifier to service SQLCEIP service | 89decf9a-ad11-485c-94a7-fefb3a02ed86 
| correlationId | Place holder for an additional identifier | 0 
|sequence | Sequence number of the data points sent within the session | 15 
| clientVersion | SQL Server instance version | 13.0.2161.3 ((SQL16_RTM_QFE-CU).160907-1223) 
| operatingSystem | The OS version where SQL Server instance is installed | Microsoft Windows Server 2012 R2 Datacenter 
| querySetVersion | Version of a group of query definitions | 1.0.0.0 
|traceName | Categories of traces: (SQLServerXeQueries, SQLServerPeriodicQueries, SQLServerOneSettingsException) | SQLServerPeriodicQueries 
|queryIdentifier | An identifier of the query | SQLServerProperties.002 
|data	| The output of the information collected on queryIdentifier as an output of T-SQL query, XE session or the application |	[{"Collation": "SQL_Latin1_General_CP1_CI_AS","SqlFTinstalled": "0" "SqlIntSec": "1","IsSingleUser": "0","SqlFilestreamMode": "0","SqlPbInstalled": "0","SqlPbNodeRole": "","SqlVersionMajor": "13","SqlVersionMinor": "0","SqlVersionBuild": "2161","ProductBuildType": "","ProductLevel": "RTM","ProductUpdateLevel": "CU2","ProductUpdateReference": "KB3182270","ProductRevision": "3","SQLEditionId": "-1534726760","IsClustered": "0","IsHadrEnabled": "0","SqlAdvAInstalled": "0","PacketReceived": "1210","Version": "Microsoft SQL Server 2016 (RTM-CU2) (KB3182270) - 13.0.2161.3 (X64) \n\tSep  7 2016 14:24:16 \n\tCopyright (c) Microsoft Corporation\n\tStandard Edition (64-bit) on Windows Server 2012 R2 Datacenter 6.3 \u003cX64\u003e (Build 9600: ) (Hypervisor)\n"}],
|query|	If applicable, the T-SQL query definition related to the queryIdentifier that produces data.		This component does not get uploaded by SQL Server CEIP service. It is included in Local Audit as a reference to customers only.| SELECT\n      SERVERPROPERTY(\u0027Collation\u0027) AS [Collation],\n      SERVERPROPERTY(\u0027IsFullTextInstalled\u0027) AS [SqlFTinstalled],\n      SERVERPROPERTY(\u0027IsIntegratedSecurityOnly\u0027) AS [SqlIntSec],\n      SERVERPROPERTY(\u0027IsSingleUser\u0027) AS [IsSingleUser],\n      SERVERPROPERTY (\u0027FileStreamEffectiveLevel\u0027) AS [SqlFilestreamMode],\n      SERVERPROPERTY(\u0027IsPolybaseInstalled\u0027) AS [SqlPbInstalled],\n      SERVERPROPERTY(\u0027PolybaseRole\u0027) AS [SqlPbNodeRole],\n      SERVERPROPERTY(\u0027ProductMajorVersion\u0027) AS [SqlVersionMajor],\n      SERVERPROPERTY(\u0027ProductMinorVersion\u0027) AS [SqlVersionMinor],\n      SERVERPROPERTY(\u0027ProductBuild\u0027) AS [SqlVersionBuild],\n      SERVERPROPERTY(\u0027ProductBuildType\u0027) AS ProductBuildType,\n      SERVERPROPERTY(\u0027ProductLevel\u0027) AS ProductLevel,\n      SERVERPROPERTY(\u0027ProductUpdateLevel\u0027) AS ProductUpdateLevel,\n      SERVERPROPERTY(\u0027ProductUpdateReference\u0027) AS ProductUpdateReference,\n      RIGHT(CAST(SERVERPROPERTY(\u0027ProductVersion\u0027) AS NVARCHAR(30)),CHARINDEX(\u0027.\u0027, REVERSE(CAST(SERVERPROPERTY(\u0027ProductVersion\u0027) AS NVARCHAR(30)))) - 1) AS ProductRevision,\n      SERVERPROPERTY(\u0027EditionID\u0027) AS SQLEditionId,\n      SERVERPROPERTY(\u0027IsClustered\u0027) AS IsClustered,\n      SERVERPROPERTY(\u0027IsHadrEnabled\u0027) AS IsHadrEnabled,\n      SERVERPROPERTY(\u0027IsAdvancedAnalyticsInstalled\u0027) AS [SqlAdvAInstalled],\n      @@PACK_RECEIVED AS PacketReceived,\n      @@VERSION AS Version
|queryTimeInTicks | The duration it takes for the query with the following trace category to execute: (SQLServerXeQueries, SQLServerPeriodicQueries) |  0 
 
### Trace Categories 
Currently we collect the following trace categories: 

- **SQLServerXeQueries**: contains data points collected through Extended Event session. 

- **SQLServerPeriodicQueries**: contains data points collected through periodic queries executed in a SQL Server instance. 

- **SQLServerPerDBPeriodicQueries**: contains data points collected through periodic queries executed to up to 30 databases in a SQL Server instance. 

- **SQLServerOneSettingsException**: contains exception messages related to updating schema and/or query set. 

- **DigitalProductID**: contains data points for aggregating anonymized (SHA-256) hashed digital product ID of SQL Server instances. 

### Local Audit File Examples



Below is an excerpt of a JSON file output of Local Audit.

```JSON
{
    "hostName": "de3b3769a63970b63981ab7a956401388962c986bfd39d371f5870d800627d11",
    "domainHash": "de3b3769a63970b63981ab7a956401388962c986bfd39d371f5870d800627d11",
    "sqmId": "02AF58F5-753A-429C-96CD-3900E90DB990",
    "instanceName": "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
    "schemaVersion": "3",
    "emitTime": "2016-09-08T17:20:22.1124269Z",
    "sessionId": "89decf9a-ad11-485c-94a7-fefb3a02ed86",
    "correlationId": 0,
    "sequence": 15,
    "clientVersion": "13.0.2161.3 ((SQL16_RTM_QFE-CU).160907-1223)",
    "operatingSystem": "Microsoft Windows Server 2012 R2 Datacenter",
    "querySetVersion": "1.0.0.0",
    "traceName": "SQLServerPeriodicQueries",
    "queryIdentifier": "SQLServerProperties.002",
    "data": [
      {
        "Collation": "SQL_Latin1_General_CP1_CI_AS",
        "SqlFTinstalled": "0",
        "SqlIntSec": "1",
        "IsSingleUser": "0",
        "SqlFilestreamMode": "0",
        "SqlPbInstalled": "0",
        "SqlPbNodeRole": "",
        "SqlVersionMajor": "13",
        "SqlVersionMinor": "0",
        "SqlVersionBuild": "2161",
        "ProductBuildType": "",
        "ProductLevel": "RTM",
        "ProductUpdateLevel": "CU2",
        "ProductUpdateReference": "KB3182270",
        "ProductRevision": "3",
        "SQLEditionId": "-1534726760",
        "IsClustered": "0",
        "IsHadrEnabled": "0",
        "SqlAdvAInstalled": "0",
        "PacketReceived": "1210",
        "Version": "Microsoft SQL Server 2016 (RTM-CU2) (KB3182270) - 13.0.2161.3 (X64) \n\tSep  7 2016 14:24:16 \n\tCopyright (c) Microsoft Corporation\n\tStandard Edition (64-bit) on Windows Server 2012 R2 Datacenter 6.3 \u003cX64\u003e (Build 9600: ) (Hypervisor)\n"
      }
    ],
    "query": "SELECT\n      SERVERPROPERTY(\u0027Collation\u0027) AS [Collation],\n      SERVERPROPERTY(\u0027IsFullTextInstalled\u0027) AS [SqlFTinstalled],\n      SERVERPROPERTY(\u0027IsIntegratedSecurityOnly\u0027) AS [SqlIntSec],\n      SERVERPROPERTY(\u0027IsSingleUser\u0027) AS [IsSingleUser],\n      SERVERPROPERTY (\u0027FileStreamEffectiveLevel\u0027) AS [SqlFilestreamMode],\n      SERVERPROPERTY(\u0027IsPolybaseInstalled\u0027) AS [SqlPbInstalled],\n      SERVERPROPERTY(\u0027PolybaseRole\u0027) AS [SqlPbNodeRole],\n      SERVERPROPERTY(\u0027ProductMajorVersion\u0027) AS [SqlVersionMajor],\n      SERVERPROPERTY(\u0027ProductMinorVersion\u0027) AS [SqlVersionMinor],\n      SERVERPROPERTY(\u0027ProductBuild\u0027) AS [SqlVersionBuild],\n      SERVERPROPERTY(\u0027ProductBuildType\u0027) AS ProductBuildType,\n      SERVERPROPERTY(\u0027ProductLevel\u0027) AS ProductLevel,\n      SERVERPROPERTY(\u0027ProductUpdateLevel\u0027) AS ProductUpdateLevel,\n      SERVERPROPERTY(\u0027ProductUpdateReference\u0027) AS ProductUpdateReference,\n      RIGHT(CAST(SERVERPROPERTY(\u0027ProductVersion\u0027) AS NVARCHAR(30)),CHARINDEX(\u0027.\u0027, REVERSE(CAST(SERVERPROPERTY(\u0027ProductVersion\u0027) AS NVARCHAR(30)))) - 1) AS ProductRevision,\n      SERVERPROPERTY(\u0027EditionID\u0027) AS SQLEditionId,\n      SERVERPROPERTY(\u0027IsClustered\u0027) AS IsClustered,\n      SERVERPROPERTY(\u0027IsHadrEnabled\u0027) AS IsHadrEnabled,\n      SERVERPROPERTY(\u0027IsAdvancedAnalyticsInstalled\u0027) AS [SqlAdvAInstalled],\n      @@PACK_RECEIVED AS PacketReceived,\n      @@VERSION AS Version",
    "queryTimeInTicks": 0
  } ,
  {
    "hostName": "de3b3769a63970b63981ab7a956401388962c986bfd39d371f5870d800627d11",
    "domainHash": "de3b3769a63970b63981ab7a956401388962c986bfd39d371f5870d800627d11",
    "sqmId": "02AF58F5-753A-429C-96CD-3900E90DB990",
    "instanceName": "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855",
    "schemaVersion": "3",
    "emitTime": "2016-09-08T17:20:24.9819144Z",
    "sessionId": "89decf9a-ad11-485c-94a7-fefb3a02ed86",
    "correlationId": 0,
    "sequence": 61,
    "clientVersion": "13.0.2161.3 ((SQL16_RTM_QFE-CU).160907-1223)",
    "operatingSystem": "Microsoft Windows Server 2012 R2 Datacenter",
    "querySetVersion": "1.0.0.0",
    "traceName": "SQLServerPeriodicQueries",
    "queryIdentifier": "ExternalScriptStats.001",
    "data": [
      {
        "counter_name": "Total Executions                                                                                                                ",
        "cntr_value": "0"
      },
      {
        "counter_name": "Parallel Executions                                                                                                             ",
        "cntr_value": "0"
      },
      {
        "counter_name": "Streaming Executions                                                                                                            ",
        "cntr_value": "0"
      },
      {
        "counter_name": "SQL CC Executions                                                                                                               ",
        "cntr_value": "0"
      },
      {
        "counter_name": "Implied Auth. Logins                                                                                                            ",
        "cntr_value": "0"
      },
      {
        "counter_name": "Total Execution Time (ms)                                                                                                       ",
        "cntr_value": "0"
      },
      {
        "counter_name": "Execution Errors                                                                                                                ",
        "cntr_value": "0"
      }
    ],	
    "query": "select counter_name, cntr_value from sys.dm_os_performance_counters where object_name like \u0027%External Scripts%\u0027",
    "queryTimeInTicks": 155834
  } 
```
## Frequently Asked Questions

**How do DBAs read the Local Audit log files?**
These log files are written in JSON format. Each line will be a JSON object representing a piece of telemetry uploaded to Microsoft. The fields names should be self-explanatory. 

**What happens if the DBA disables Usage Feedback Collection?**
No Local Audit file will be written. 

**What happens if there is not internet connectivity/machine is behind the firewall?**
SQL Server 2016 usage feedback will not be sent to Microsoft. It will still try to write the local audit logs if configured correctly.

**How do DBAs disable Local Audit?**
Remove the UserRequestedLocalAuditDirectory registry key entry.

**Who can read the Local Audit log files?**
Anyone in your organization that has access to the Local Audit Directory.

**How do DBAs manage the log files written to the designated directory?**
DBAs will need to self-manage the clean-up of the files in the directory to avoid consuming too much disk space.

**Is there a client or tool that I can use to read this JSON output?**
The output can be read with Notepad, Visual Studio or any JSON reader of your choice.
Alternatively, you can read the JSON file and analyze the data in an SQL Server 2016 instance as illustrated below. More details on how to read JSON file in SQL Server, please visit [Importing JSON files into SQL Server using OPENROWSET (BULK) and OPENJSON (Transact-SQL)](http://blogs.msdn.microsoft.com/sqlserverstorageengine/2015/10/07/bulk-importing-json-files-into-sql-server/).

```Transact-SQL
DECLARE @JSONFile AS VARCHAR(MAX)

-- Read the JSON file into variable 
SELECT @JSONFile = BulkColumn 
FROM OPENROWSET (BULK 'C:\SQLCEIPAudit\MSSQLSERVER\2016-09-08.json', SINGLE_CLOB) MyFile 

-- Check if the JSON file has been read properly and if it's in a JSON format
SELECT 
	@JSONFile LocalAuditOutput, 
	ISJSON(@JSONFile) IsFileInJSONFormat

-- Get the query identifier, query and the data (output of the query)	
SELECT 
	sequence,
	queryIdentifier,
	query,
	data
FROM OPENJSON(@JSONFile) 
	WITH (sessionId VARCHAR(64)
		 ,sequence INT
		 ,queryIdentifier VARCHAR(128)
		 ,query VARCHAR(MAX)
		 ,data NVARCHAR(MAX) AS JSON)
-- Get specific details about the output of "DatabaseProperties.001" query	
SELECT 
	QueryIdentifier,
	DatabaseID,
	CompatibilityLevel,
	IsQueryStoreOn
FROM OPENJSON(@JSONFile) 
	WITH (sessionId VARCHAR(64)
		 ,sequence INT
		 ,queryIdentifier VARCHAR(128)
		 ,query VARCHAR(MAX)
		 ,data NVARCHAR(MAX) AS JSON) 
	CROSS APPLY OPENJSON(data) 
		WITH (	 DatabaseID varchar(128) '$.database_id'
				,CompatibilityLevel varchar(128) '$.compatibility_level'
				,IsQueryStoreOn varchar(128) '$.QS'
			 )
WHERE queryIdentifier = 'DatabaseProperties.001'
```

## See Also
[Local Audit for SSMS Usage Feedback Collection](https://docs.microsoft.com/sql/ssms/sql-server-management-studio-telemetry-ssms)

