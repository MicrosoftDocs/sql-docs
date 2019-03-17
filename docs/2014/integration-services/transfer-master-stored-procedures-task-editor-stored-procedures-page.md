---
title: "Transfer Master Stored Procedures Task Editor (Stored Procedures Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.transferstoredprocedurestask.storedprocedures.f1"
helpviewer_keywords: 
  - "Transfer Stored Procedures Task Editor"
ms.assetid: 5fcf171e-cc0b-4c24-8eb5-3a4b4775e64a
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Transfer Master Stored Procedures Task Editor (Stored Procedures Page)
  Use the **Stored Procedures** page of the **Transfer Master Stored Procedures Task Editor** dialog box to specify properties for copying one or more user-defined stored procedures from the **master** database in one instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance to a **master** database in another instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. For more information about this task, see [Transfer Master Stored Procedures Task](control-flow/transfer-master-stored-procedures-task.md).  
  
> [!NOTE]  
>  This task transfers only user-defined stored procedures owned by **dbo** from a **master** database on the source server to a **master** database on the destination server. Users must be granted the CREATE PROCEDURE permission in the **master** database on the destination server or be members of the **sysadmin** fixed server role on the destination server to create stored procedures there.  
  
## Options  
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
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Integration Services Tasks](control-flow/integration-services-tasks.md)   
 [Transfer Master Stored Procedures Task Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)   
 [Expressions Page](expressions/expressions-page.md)   
 [SMO Connection Manager](connection-manager/smo-connection-manager.md)  
  
  
