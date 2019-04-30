---
title: "Transfer Logins Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.transferloginstask.f1"
  - "sql13.dts.designer.transferloginstask.general.f1"
  - "sql13.dts.designer.transferloginstask.logins.f1"
helpviewer_keywords: 
  - "Transfer Logins task [Integration Services]"
ms.assetid: 1df60fd6-c019-405d-8155-c330dbac2cc1
author: janinezhang
ms.author: janinez
manager: craigg
---
# Transfer Logins Task

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  The Transfer Logins task transfers one or more logins between instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
## Transfer Logins Between Instances of SQL Server  
 The Transfer Logins task supports a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] source and destination.  
  
## Events  
 The task raises an information event that reports the number of logins transferred and a warning event when a login is overwritten.  
  
 The Transfer Logins task does not report incremental progress of the login transfer; it reports only 0% and 100 % completion.  
  
## Execution Value  
 The execution value, defined in the **ExecutionValue** property of the task, returns the number of logins transferred. By assigning a user-defined variable to the **ExecValueVariable** property of the Transfer Logins task, information about the login transfer can be made available to other objects in the package. For more information, see [Integration Services &#40;SSIS&#41; Variables](../../integration-services/integration-services-ssis-variables.md) and [Use Variables in Packages](https://msdn.microsoft.com/library/7742e92d-46c5-4cc4-b9a3-45b688ddb787).  
  
## Log Entries  
 The Transfer Logins task includes the following custom log entries:  
  
-   TransferLoginsTaskStarTransferringObjects    This log entry reports the transfer has started. The log entry includes the start time.  
  
-   TransferLoginsTaskFinishedTransferringObjects   This log entry reports the transfer has completed. The log entry includes the end time.  
  
 In addition, a log entry for the **OnInformation** event reports the number of logins that were transferred, and a log entry for the **OnWarning** event is written for each login on the destination that is overwritten.  
  
## Security and Permissions  
 To browse logins on the source server and to create logins on the destination server, the user must be a member of the sysadmin server role on both servers.  
  
## Configuration of the Transfer Logins Task  
 The Transfer Logins task can be configured to transfer all logins, only specified logins, or all logins that have access to specified databases only. The sa login cannot be transferred. The sa login may be renamed; however, the renamed sa login cannot be transferred either.  
  
 You can also indicate whether the task copies the security identifiers (SIDs) associated with the logins. If the Transfer Logins task is used in conjunction with the Transfer Database task the SIDs must be copied to the destination; otherwise, the transferred logins are not recognized by the destination database.  
  
 At the destination, the transferred logins are disabled and assigned random passwords. A member of the sysadmin role on the destination server must change the passwords and enable the logins before the logins can be used.  
  
 The logins to be transferred may already exist on the destination. The Transfer Logins task can be configured to handle existing logins in the following ways:  
  
-   Overwrite existing logins.  
  
-   Fail the task when duplicate logins exist.  
  
-   Skip duplicate logins.  
  
 At run time, the Transfer Logins task connects to the source and destination servers by using two SMO connection managers. The SMO connection managers are configured separately from the Transfer Logins task, and then referenced in the Transfer Logins task. The SMO connection managers specify the server and the authentication mode to use when accessing the server. For more information, see [SMO Connection Manager](../../integration-services/connection-manager/smo-connection-manager.md).  
  
 You can set properties through [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer or programmatically.  
  
 For more information about the properties that you can set in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Expressions Page](../../integration-services/expressions/expressions-page.md)  
  
 For more information about how to set these properties in [!INCLUDE[ssIS](../../includes/ssis-md.md)] Designer, click the following topic:  
  
-   [Set the Properties of a Task or Container](https://msdn.microsoft.com/library/52d47ca4-fb8c-493d-8b2b-48bb269f859b)  
  
## Programmatic Configuration of the Transfer Logins Task  
 For more information about programmatically setting these properties, click the following topic:  
  
-   <xref:Microsoft.SqlServer.Dts.Tasks.TransferLoginsTask.TransferLoginsTask>  
  
## Transfer Logins Task Editor (General Page)
  Use the **General** page of the **Transfer Logins Task Editor** dialog box to name and describe the Transfer Logins task.  
  
### Options  
 **Name**  
 Type a unique name for the Transfer Logins task. This name is used as the label in the task icon.  
  
> [!NOTE]  
>  Task names must be unique within a package.  
  
 **Description**  
 Type a description of the Transfer Logins task.  
  
## Transfer Logins Task Editor (Logins Page)
  Use the **Logins** page of the **Transfer Logins Task Editor** dialog box to specify properties for copying one or more [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins from one instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to another.  
  
> [!IMPORTANT]  
>  When the Transfer Logins task is executed, logins are created on the destination server with random passwords and the passwords are disabled. To use these logins, a member of the **sysadmin** fixed server role must change the passwords and then enable them. The **sa** login cannot be transferred.  
  
### Options  
 **SourceConnection**  
 Select a SMO connection manager in the list, or click **\<New connection...>** to create a new connection to the source server.  
  
 **DestinationConnection**  
 Select a SMO connection manager in the list, or click **\<New connection...>** to create a new connection to the destination server.  
  
 **LoginsToTransfer**  
 Select the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins to copy from the source to the destination server. This property has the options listed in the following table:  
  
|Value|Description|  
|-----------|-----------------|  
|**AllLogins**|All [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins on the source server will be copied to the destination server.|  
|**SelectedLogins**|Only logins specified with **LoginsList** will be copied to the destination server.|  
|**AllLoginsFromSelectedDatabases**|All logins from the databases specified with **DatabasesList** will be copied to the destination server.|  
  
 **LoginsList**  
 Select the logins on the source server to be copied to the destination server. This option is only available when **SelectedLogins** is selected for **LoginsToTransfer**.  
  
 **DatabasesList**  
 Select the databases on the source server that contain logins to be copied to the destination server. This option is only available when **AllLoginsFromSelectedDatabases** is selected for **LoginsToTransfer**.  
  
 **IfObjectExists**  
 Select how the task should handle logins of the same name that already exist on the destination server.  
  
 This property has the options listed in the following table:  
  
|Value|Description|  
|-----------|-----------------|  
|**FailTask**|Task fails if logins of the same name already exist on the destination server.|  
|**Overwrite**|Task overwrites logins of the same name on the destination server.|  
|**Skip**|Task skips logins of the same name that exist on the destination server.|  
  
 **CopySids**  
 Select whether the security identifiers associated with the logins should be copied to the destination server. **CopySids** must be set to **True** if the Transfer Logins task is used along with the Transfer Database task. Otherwise, the copied logins will not be recognized by the transferred database.  
  
