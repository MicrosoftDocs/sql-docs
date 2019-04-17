---
title: "Transfer Database Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.transferdatabasetask.f1"
  - "sql13.dts.designer.transferdatabasetask.general.f1"
  - "sql13.dts.designer.transferdatabasetask.database.f1"
  - "sql13.dts.designer.transferdatabasetask.sourcedbfiles.f1"
  - "sql13.dts.designer.transferdatabasetask.destdbfiles.f1"
helpviewer_keywords: 
  - "Transfer Database task [Integration Services]"
ms.assetid: b9a2e460-cdbc-458f-8df8-06b8b2de3d67
author: janinezhang
ms.author: janinez
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
 The execution value, defined in the **ExecutionValue** property of the task, returns the value 1, because in contrast to other transfer tasks, the Transfer Database task can transfer only one database.  
  
 By assigning a user-defined variable to the **ExecValueVariable** property of the Transfer Database task, information about the error message transfer can be made available to other objects in the package. For more information, see [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md) and [Use Variables in Packages](https://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787).  
  
## Log Entries  
 The Transfer Database task includes the following custom log entries:  
  
-   SourceSQLServer    This log entry lists the name of the source server.  
  
-   DestSQLServer    This log entry lists the name of the destination server.  
  
-   SourceDB    This log entry lists the name of the database that is transferred.  
  
 In addition, a log entry for the **OnInformation** event is written when the destination database is overwritten.  
  
## Security and Permissions  
 To transfer a database using offline mode, the user who runs the package must be a member of the sysadmin server role.  
  
 To transfer a database using online mode, the user who runs the package must be a member of the sysadmin server role or the database owner (dbo) of the selected database.  
  
## Configuration of the Transfer Database Task  
 You can specify whether the task tries to reattach the source database if the database transfer fails.  
  
 The Transfer Database task can also be configured to permit overwriting a destination database that has the same name, replacing the destination database.  
  
 The source database can also be renamed as part of the transfer process. If you want to transfer a database to a destination instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that already contains a database that has the same name, renaming the source database allows the database to be transferred. However, the database file names must also be different; if database files that have the same names already exist at the destination, the task fails.  
  
 When you copy a database, the database cannot be smaller than the size of the **model** database on the destination server. You can either increase the size of the database to copy, or reduce the size of **model**.  
  
 At run time, the Transfer Database task connects to the source and destination servers by using one or two SMO connection managers. When you create a copy of a database on the same server, only one SMO connection manager is required. The SMO connection managers are configured separately from the Transfer Database task, and then are referenced in the Transfer Database task. The SMO connection managers specify the server and the authentication mode to use when the task accesses the server. For more information, see [SMO Connection Manager](../../integration-services/connection-manager/smo-connection-manager.md).  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Expressions Page](../../integration-services/expressions/expressions-page.md)  
  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](https://msdn.microsoft.com/library/52d47ca4-fb8c-493d-8b2b-48bb269f859b)  
  
## Programmatic Configuration of the Transfer Database Task  
 For more information about programmatically setting these properties, click the following topic:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.TransferDatabaseTask.TransferDatabaseTask>  
  
## Transfer Database Task Editor (General Page)
  Use the **General** page of the **Transfer Database Task Editor** dialog box to name and describe the Transfer Database task. The Transfer Database task copies or moves a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database between two instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This task can also be used to copy a database within the same server.   
  
### Options  
 **Name**  
 Type a unique name for the Transfer Database task. This name is used as the label in the task icon.  
  
> [!NOTE]  
>  Task names must be unique within a package.  
  
 **Description**  
 Type a description of the Transfer Database task.  
  
## Transfer Database Task Editor (Databases Page)
  Use the **Databases** page of the **Transfer Database Task Editor** dialog box to specify properties for the source and destination databases involved in the Transfer Database task. The Transfer Database task copies or moves a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database between two instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This task can also be used to copy a database within the same server.  
  
### Options  
 **SourceConnection**  
 Select a SMO connection manager in the list, or click **\<New connection...>** to create a new connection to the source server.  
  
 **DestinationConnection**  
 Select a SMO connection manager in the list, or click **\<New connection...>** to create a new connection to the destination server.  
  
 **DestinationDatabaseName**  
 Specify the name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database on the destination server.  
  
 To automatically populate this field with the source database name, specify the **SourceConnection** and **SourceDatabaseName** first.  
  
 To rename the database on the destination server, type the new name in this field.  
  
 **DestinationDatabaseFiles**  
 Specifies the names and locations of the database files on the destination server.  
  
 To automatically populate this field with the source database file names and locations, specify the **SourceConnection**, **SourceDatabaseName**, and **SourceDatabaseFiles** first.  
  
 To rename the database files or to specify the new locations on the destination server, populate this field with the source database information, and then click the browse button. In the **Destination database files** dialog box, edit the **Destination File**, **Destination Folder**, or **Network File Share**.  
  
> [!NOTE]  
>  If you locate the database files by using the browse button, the file location is entered using the local drive notation: for example, c:\\. You must replace this with the network share notation, including the computer name and share name. If the default administrative share is used, you must use the $ notation, and have administrative access to the share.  
  
 **DestinationOverwrite**  
 Specify whether the database on the destination server can be overwritten.  
  
 This property has the options listed in the following table:  
  
|Value|Description|  
|-----------|-----------------|  
|**True**|Overwrite destination server database.|  
|**False**|Do not overwrite destination server database.|  
  
> [!CAUTION]  
>  The data in the destination server database will be overwritten if you specify **True** for **DestinationOverwrite**, which may result in data loss. To avoid this, back up the destination server database to another location before executing the Transfer Database task.  
  
 **Action**  
 Specify whether the task will **Copy** or **Move** the database to the destination server.  
  
 **Method**  
 Specify whether the task will be executed while the database on the source server is in online or offline mode.  
  
 To transfer a database using offline mode, the user that runs the package must be a member of the **sysadmin** fixed server role.  
  
 To transfer a database using the online mode, the user that runs the package must be a member of the **sysadmin** fixed server role, or the database owner (**dbo**) of the selected database.  
  
 **SourceDatabaseName**  
 Select the name of the database to be copied or moved.  
  
 **SourceDatabaseFiles**  
 Click the browse button to select the database files.  
  
 **ReattachSourceDatabase**  
 Specify whether the task will attempt to reattach the source database if a failure occurs.  
  
 This property has the options listed in the following table:  
  
|Value|Description|  
|-----------|-----------------|  
|**True**|Reattach source database.|  
|**False**|Do not reattach source database.|  

## Source database files
  Use the **Source Database Files** dialog box to view the database file names and locations on the source server, or to specify a network file share location for the Transfer Database task.   
  
 To populate this dialog box with the database file names and locations on the source server, specify the **SourceConnection** and **SourceDatabaseName** first in the **Databases** page of the **Transfer Database Task Editor** dialog box.  
  
### Options  
 **Source File**  
 Database file names on the source server that will be transferred. **Source File** is read only.  
  
 **Source Folder**  
 Folder on the source server where the database files to be transferred reside. **Source Folder** is read only.  
  
 **Network File Share**  
 Network shared folder on the source server from where the database files will be transferred. Use **Network File Share** when you transfer a database in offline mode by specifying **DatabaseOffline** for **Method** in the **Databases** page of the **Transfer Database Task Editor** dialog box.  
  
 Enter the network file share location, or click the browse button **(...)** to locate the network file share location.  
  
 When you transfer a database in offline mode, the database files are copied to the **Network file share** location on the source server before they are transferred to the destination server.  

## Destination Database Files
  Use the **Destination Database Files** dialog box to view or change the database file names and locations on the destination server, or to specify a network file location for the Transfer Database task.  
  
 To automatically populate this dialog box with the database file names and locations on the source server, specify the **SourceConnection**, **SourceDatabaseName**, and **SourceDatabaseFiles** first in the **Databases** page of the **Transfer Database Task Editor** dialog box.  
  
### Options  
 **Destination File**  
 Names of the transferred database files on the destination server.  
  
 Enter the file name, or click the file name to edit it.  
  
 **Destination Folder**  
 Folder on the destination server where the database files will be transferred to.  
  
 Enter the folder path, click the folder path to edit it, or click browse to locate the folder where you want to transfer the database files on the destination server.  
  
 **Network File Share**  
 Network shared folder on the destination server where the database files will be transferred to. Use **Network file share** when you transfer a database in offline mode by specifying **DatabaseOffline** for **Method** in the **Databases** page of the **Transfer Database Task Editor** dialog box.  
  
 Enter the network file share location, or click browse to locate the network file share location.  
  
 When you transfer a database in offline mode, the database files are copied to the **Network file share** location before they are transferred to the **Destination folder** location.  
