---
title: "Transfer Logins Task Editor (Logins Page) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.transferloginstask.logins.f1"
helpviewer_keywords: 
  - "Transfer Logins Task Editor"
ms.assetid: bf244c24-bd45-4ece-b66b-78b488f35c5b
caps.latest.revision: 23
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Transfer Logins Task Editor (Logins Page)
  Use the **Logins** page of the **Transfer Logins Task Editor** dialog box to specify properties for copying one or more [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins from one instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to another. For more information about this task, see [Transfer Logins Task](../../integration-services/control-flow/transfer-logins-task.md).  
  
> [!IMPORTANT]  
>  When the Transfer Logins task is executed, logins are created on the destination server with random passwords and the passwords are disabled. To use these logins, a member of the **sysadmin** fixed server role must change the passwords and then enable them. The **sa** login cannot be transferred.  
  
## Options  
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
  
## See Also  
 [Integration Services Error and Message Reference](../../integration-services/integration-services-error-and-message-reference.md)   
 [Integration Services Tasks](../../integration-services/control-flow/integration-services-tasks.md)   
 [Transfer Logins Task Editor &#40;General Page&#41;](../../integration-services/control-flow/transfer-logins-task-editor-general-page.md)   
 [Expressions Page](../../integration-services/expressions/expressions-page.md)   
 [SMO Connection Manager](../../integration-services/connection-manager/smo-connection-manager.md)   
 [Strong Passwords](../../relational-databases/security/strong-passwords.md)   
 [Security Considerations for a SQL Server Installation](../../sql-server/install/security-considerations-for-a-sql-server-installation.md)  
  
  