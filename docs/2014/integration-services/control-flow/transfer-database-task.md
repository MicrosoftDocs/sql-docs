---
title: "Transfer Database Task | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.transferdatabasetask.f1"
helpviewer_keywords: 
  - "Transfer Database task [Integration Services]"
ms.assetid: b9a2e460-cdbc-458f-8df8-06b8b2de3d67
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Transfer Database Task
  The Transfer Database task transfers a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database between two instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. In contrast to the other tasks that only transfer [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] objects by copying them, the Transfer Database task can either copy or move a database. This task can also be used to copy a database within the same server.  
  
## Offline and Online Modes  
 The database can be transferred by using online or offline mode. When you use online mode, the database remains attached and it is transferred by using SQL Management Object (SMO) to copy the database objects. When you use offline mode, the database is detached, the database files are copied or moved, and the database is attached at the destination after the transfer finishes successfully. If the database is copied, it is automatically reattached at the source if the copy is successful. In offline mode, the database is copied more quickly, but the database is unavailable to users during the transfer.  
  
 Offline mode requires that you specify the network file shares on the source and destination servers that contain the database files. If the folder is shared and can be accessed by the user, you can reference the network share using the syntax \\\computername\Program Files\myfolder\\. Otherwise, you must use the syntax \\\computername\c$\Program Files\myfolder\\. To use the latter syntax, the user must have write access to the source and destination network shares.  
  
## Transfer of Databases Between Versions of SQL Server  
 The Transfer Database task can transfer a database between instances of different [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] versions.  
  
## Events  
 The Transfer Database task does not report incremental progress of the error message transfer; it reports only 0% and 100 % completion.  
  
## Execution Value  
 The execution value, defined in the `ExecutionValue` property of the task, returns the value 1, because in contrast to other transfer tasks, the Transfer Database task can transfer only one database.  
  
 By assigning a user-defined variable to the `ExecValueVariable` property of the Transfer Database task, information about the error message transfer can be made available to other objects in the package. For more information, see [Integration Services &#40;SSIS&#41; Variables](../integration-services-ssis-variables.md) and [Use Variables in Packages](../use-variables-in-packages.md).  
  
## Log Entries  
 The Transfer Database task includes the following custom log entries:  
  
-   SourceSQLServer    This log entry lists the name of the source server.  
  
-   DestSQLServer    This log entry lists the name of the destination server.  
  
-   SourceDB    This log entry lists the name of the database that is transferred.  
  
 In addition, a log entry for the `OnInformation` event is written when the destination database is overwritten.  
  
## Security and Permissions  
 To transfer a database using offline mode, the user who runs the package must be a member of the sysadmin server role.  
  
 To transfer a database using online mode, the user who runs the package must be a member of the sysadmin server role or the database owner (dbo) of the selected database.  
  
## Configuration of the Transfer Database Task  
 You can specify whether the task tries to reattach the source database if the database transfer fails.  
  
 The Transfer Database task can also be configured to permit overwriting a destination database that has the same name, replacing the destination database.  
  
 The source database can also be renamed as part of the transfer process. If you want to transfer a database to a destination instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that already contains a database that has the same name, renaming the source database allows the database to be transferred. However, the database file names must also be different; if database files that have the same names already exist at the destination, the task fails.  
  
 When you copy a database, the database cannot be smaller than the size of the **model** database on the destination server. You can either increase the size of the database to copy, or reduce the size of **model**.  
  
 At run time, the Transfer Database task connects to the source and destination servers by using one or two SMO connection managers. When you create a copy of a database on the same server, only one SMO connection manager is required. The SMO connection managers are configured separately from the Transfer Database task, and then are referenced in the Transfer Database task. The SMO connection managers specify the server and the authentication mode to use when the task accesses the server. For more information, see [SMO Connection Manager](../connection-manager/smo-connection-manager.md).  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click one of the following topics:  
  
-   [Transfer Database Task Editor &#40;General Page&#41;](../general-page-of-integration-services-designers-options.md)  
  
-   [Transfer Database Task Editor &#40;Databases Page&#41;](../transfer-database-task-editor-databases-page.md)  
  
-   [Expressions Page](../expressions/expressions-page.md)  
  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](../set-the-properties-of-a-task-or-container.md)  
  
## Programmatic Configuration of the Transfer Database Task  
 For more information about programmatically setting these properties, click the following topic:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.TransferDatabaseTask.TransferDatabaseTask>  
  
  
