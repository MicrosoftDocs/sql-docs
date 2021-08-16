---
title: "MSSQLSERVER_912 Microsoft Docs"
description: The database script level could not be upgraded to the latest requred by the server.
ms.custom: ""
ms.date: "08/16/2021"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "912 (Database Engine error)"
ms.assetid: E9C55C8B-EC83-4E20-A54C-5B0FE61AE053
author: PijoCoder
ms.author: mathoma
---
# MSSQLSERVER_912
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|912|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DB_RUNSCRIPTUPGRADE_STEP_FAILED|  
|Message Text|	Script level upgrade for database '%.*ls' failed because upgrade step '%.*ls' encountered error %d, state %d, severity %d. This is a serious error condition which might interfere with regular operation and the database will be taken offline. If the error happened during upgrade of the 'master' database, it will prevent the entire SQL Server instance from starting. Examine the previous errorlog entries for errors, take the appropriate corrective actions and re-start the database so that the script upgrade steps run to completion.|  
                
  
## Explanation  
The database script level could not be upgraded to the latest requred by the server. Examine the errorlog for upgrade errors, correct them and re-start the database.

Whenever [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is upgraded or a Cumulative Update is appled, only the binaries are upgraded and not the database and its objects. Once the binaries are replaced with new versions and the service restarts for the first time, it starts a database upgrade using 'msdb110_upgrade.sql' T-SQL script which is located under 'C:\Program Files\Microsoft SQL Server\MSSQLXX.YYYY\MSSQL\Install\.'

Under certain scenarios, the upgrade script encounters Script level upgrade errors which always includes Error 912 (the generic error message which will contain a reference to the script that failed and what error the failed script encountered). For example:

	
	Error: 912, Severity: 21, State: 2.
	Script level upgrade for database 'master' failed because upgrade step 'xxx.sql' encountered error <Error Number>, state <Error State>, severity <Error Severity>. This is a serious error condition which might interfere with regular operation and the database will be taken offline. If the error happened during upgrade of the 'master' database, it will prevent the entire SQL Server instance from starting. Examine the previous errorlog entries for errors, take the appropriate corrective actions and re-start the database so that the script upgrade steps run to completion.
	
	Error: 3417, Severity: 21, State: 3.
	Cannot recover the master database. SQL Server is unable to run. Restore master from a full backup, repair it, or rebuild it. For more information about how to rebuild the master database, see SQL Server Books Online.

## User Action  
  
To find the cause of the issue, we need to look at the error log entries preceding error 912 and troubleshoot the error referenced in the messaging of Error 912. To do this, we usually need to start the SQL Server Service with trace flag 902.  This allows the SQL service to skip the upgrade script during startup so that we get a chance to investigate and fix the issue. 

	Steps to start SQL with trace flag 902 by Configuration Manager, sqlservr.exe or NET START
	
Configuration Manager
		Open SQL Server Configuration Manager
	-  Select the SQL server instance in SQL Server Services,
	-  Right-click the instance, and then click Properties.
	-  Click the Startup Parameter tab.
	-  Add “-T902”
	-  Close instance properties
	-  Start SQL Server Service

Command Prompt Using Sqlservr.exe
-  Open an administrative command prompt and migrate to the SQL Server Binn directory   
    Default Instance :      	\Program Files\Microsoft SQL Server\MSSQL<version>\MSSQL\Binn
    Named Instance : 	\Program Files\Microsoft SQL Server\MSSQL<version>.<instance name>\MSSQL\Binn
	
- For a Default Instance use the following command:

```DOS
Sqlservr.exe -s MSSQLSERVER  -T902
```

- For a Named Instance use the following command ***where sql2016 is an example of an instance name:

    ```dos
    sqlservr.exe -s sql2016  -T902
    ```

Command Prompt Using Net Start

- Default Instance:

    ```dos
    NET START MSSQLSERVER /902 
    ```

- Named Instance :   

    ```dos
    NET START MSSQL$INSTANCENAME  /T902
    ```

