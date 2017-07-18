---
title: "Change the Account for Scale Out Logging | Microsoft Docs"
ms.custom: ""
ms.date: "07/18/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
caps.latest.revision: 1
author: "haoqian"
ms.author: "haoqian"
manager: "jhubbard"
---
# Change the Account for Scale Out Logging
When executing packages in Scale Out, the event messages are logged into SSISDB with an auto-created user **##MS_SSISLogDBWorkerAgentLogin##**. 
The login of this user uses SQL Server authentication. To change the account, follows the steps below:

## 1. Create a user of SSISDB
For instructions of creating a database user, see [Create a Database User](../../relational-databases/security/authentication-access/create-a-database-user.md).

## 2. Add the user to database role ssis_cluster_worker

For instructions of joining a database role, see [Join a Role](../../relational-databases/security/authentication-access/join-a-role.md).

## 3. Update logging information in SSISDB
Call stored procedure [catalog].[update_logdb_info] with Sql Server name and connection string as parameters.

#### Example
```tsql
SET @serverName = CONVERT(sysname, SERVERPROPERTY('servername'))
SET @connectionString = 'Data Source=' + @serverName + ';Initial Catalog=SSISDB;Integrated Security=SSPI;'
EXEC [internal].[update_logdb_info] @serverName, @connectionString
GO
```

## 4. Restart Scale Out Woker service

> Note: If you use a Windows user account for logging, it must be the same account running Scale Out Worker service. Otherwise, the login to SQL Server will fail.