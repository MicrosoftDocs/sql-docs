---
title: "Transfer Master Stored Procedures Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.transfermasterspstask.f1"
  - "sql13.dts.designer.transferstoredprocedurestask.general.f1"
  - "sql13.dts.designer.transferstoredprocedurestask.storedprocedures.f1"
helpviewer_keywords: 
  - "Transfer Master Stored Procedures task [Integration Services]"
ms.assetid: 81702560-48a3-46d1-a469-e41304c7af8e
author: janinezhang
ms.author: janinez
manager: craigg
---
# Transfer Master Stored Procedures Task
  The Transfer Master Stored Procedures task transfers one or more user-defined stored procedures between **master** databases on instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To transfer a stored procedure from the **master** database, the owner of the procedure must be dbo.  
  
 The Transfer Master Stored Procedures task can be configured to transfer all stored procedures or only specified stored procedures. This task does not copy system stored procedures.  
  
 The master stored procedures to be transferred may already exist on the destination. The Transfer Master Stored Procedures task can be configured to handle existing stored procedures in the following ways:  
  
-   Overwrite existing stored procedures.  
  
-   Fail the task when duplicate stored procedures exist.  
  
-   Skip duplicate stored procedures.  
  
 At run time, the Transfer Master Stored Procedures task connects to the source and destination servers by using two SMO connection managers. The SMO connection managers are configured separately from the Transfer Master Stored Procedures task, and then referenced in the Transfer Master Stored Procedures task. The SMO connection managers specify the server and the authentication mode to use when accessing the server. For more information, see [SMO Connection Manager](../../integration-services/connection-manager/smo-connection-manager.md).  
  
## Transferring Stored Procedures Between Instances of SQL Server  
 The Transfer Master Stored Procedures task supports a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] source and destination.  
  
## Events  
 The task raises an information event that reports the number of stored procedures transferred and a warning event when a stored procedure is overwritten.  
  
 The Transfer Master Stored Procedures task does not report incremental progress of the login transfer; it reports only 0% and 100 % completion.  
  
## Execution Value  
 The execution value, defined in the **ExecutionValue** property of the task, returns the number of stored procedures transferred. By assigning a user-defined variable to the **ExecValueVariable** property of the Transfer Master Stored Procedures task, information about the stored procedure transfer can be made available to other objects in the package. For more information, see [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md) and [Use Variables in Packages](https://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787).  
  
## Log Entries  
 The Transfer Master Stored Procedures task includes the following custom log entries:  
  
-   TransferStoredProceduresTaskStartTransferringObjects  This log entry reports that the transfer has started. The log entry includes the start time.  
  
-   TransferSStoredProceduresTaskFinishedTransferringObjects  This log entry reports that the transfer has finished. The log entry includes the end time.  
  
 In addition, a log entry for the **OnInformation** event reports the number of stored procedures that were transferred, and a log entry for the **OnWarning** event is written for each stored procedure on the destination that is overwritten.  
  
## Security and Permissions  
 The user must have permission to view the list of stored procedure in the **master** database on the source, and must be a member of the sysadmin server role or have permission to created stored procedures in the **master** database on the destination server.  
  
## Configuration of the Transfer Master Stored Procedures Task  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Expressions Page](../../integration-services/expressions/expressions-page.md)  
  
 For information about programmatically setting these properties, click the following topic:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.TransferStoredProceduresTask.TransferStoredProceduresTask>  
  
### Configuring the Transfer Master Stored Procedures Task Programmatically  
  
## Related Tasks  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](https://msdn.microsoft.com/library/52d47ca4-fb8c-493d-8b2b-48bb269f859b)  
  
## Transfer Master Stored Procedures Task Editor (General Page)
  Use the **General** page of the **Transfer Master Stored Procedures Task Editor** dialog box to name and describe the Transfer Master Stored Procedures task.  
  
> [!NOTE]  
>  This task transfers only user-defined stored procedures owned by **dbo** from a **master** database on the source server to a **master** database on the destination server. Users must be granted the CREATE PROCEDURE permission in the **master** database on the destination server or be members of the **sysadmin** fixed server role on the destination server to create stored procedures there.  
  
### Options  
 **Name**  
 Type a unique name for the Transfer Master Stored Procedures task. This name is used as the label in the task icon.  
  
> [!NOTE]  
>  Task names must be unique within a package.  
  
 **Description**  
 Type a description of the Transfer Master Stored Procedures task.  
  
## Transfer Master Stored Procedures Task Editor (Stored Procedures Page)
  Use the **Stored Procedures** page of the **Transfer Master Stored Procedures Task Editor** dialog box to specify properties for copying one or more user-defined stored procedures from the **master** database in one instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance to a **master** database in another instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  This task transfers only user-defined stored procedures owned by **dbo** from a **master** database on the source server to a **master** database on the destination server. Users must be granted the CREATE PROCEDURE permission in the **master** database on the destination server or be members of the **sysadmin** fixed server role on the destination server to create stored procedures there.  
  
### Options  
 **SourceConnection**  
 Select a SMO connection manager in the list, or click **\<New connection...>** to create a new connection to the source server.  
  
 **DestinationConnection**  
 Select a SMO connection manager in the list, or click **\<New connection...>** to create a new connection to the destination server.  
  
 **IfObjectExists**  
 Select how the task should handle user-defined stored procedures of the same name that already exist in the **master** database on the destination server.  
  
 This property has the options listed in the following table:  
  
|Value|Description|  
|-----------|-----------------|  
|**FailTask**|Task fails if stored procedures of the same name already exist in the **master** database on the destination server.|  
|**Overwrite**|Task overwrites stored procedures of the same name in the **master** database on the destination server.|  
|**Skip**|Task skips stored procedures of the same name that exist in the **master** database on the destination server.|  
  
 **TransferAllStoredProcedures**  
 Select whether all user-defined stored procedures in the **master** database on the source server should be copied to the destination server.  
  
|Value|Description|  
|-----------|-----------------|  
|**True**|Copy all user-defined stored procedures in the **master** database.|  
|**False**|Copy only the specified stored procedures.|  
  
 **StoredProceduresList**  
 Select which user-defined stored procedures in the **master** database on the source server should be copied to the destination **master** database. This option is only available when **TransferAllStoredProcedures** is set to **False**.  
  
## See Also  
 [Transfer SQL Server Objects Task](../../integration-services/control-flow/transfer-sql-server-objects-task.md)   
 [Integration Services Tasks](../../integration-services/control-flow/integration-services-tasks.md)   
 [Control Flow](../../integration-services/control-flow/control-flow.md)  
  
  
