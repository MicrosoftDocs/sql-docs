---
title: "Transfer Jobs Task Editor (Jobs Page) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.transferjobstask.jobs.f1"
helpviewer_keywords: 
  - "Transfer Jobs Task Editor"
ms.assetid: e72b1dc7-8cda-4ee6-abb5-d438370f04df
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Transfer Jobs Task Editor (Jobs Page)
  Use the **Jobs** page of the **Transfer Jobs Task Editor** dialog box to specify properties for copying one or more [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent jobs from one instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to another. For more information about the Transfer Jobs task, see [Transfer Jobs Task](control-flow/transfer-jobs-task.md).  
  
> [!NOTE]  
>  To access jobs on the source server, users must be a member of at least the **SQLAgentUserRole** fixed database role on the server. To successfully create jobs on the destination server, the user must be a member of the **sysadmin** fixed server role or one of the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent fixed database roles. For more information about [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent fixed database roles and their permissions, see [SQL Server Agent Fixed Database Roles](../ssms/agent/sql-server-agent-fixed-database-roles.md).  
  
## Options  
 **SourceConnection**  
 Select a SMO connection manager in the list, or click **\<New connection...>** to create a new connection to the source server.  
  
 **DestinationConnection**  
 Select a SMO connection manager in the list, or click **\<New connection...>** to create a new connection to the destination server.  
  
 **TransferAllJobs**  
 Select whether the task should copy all or only the specified [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Agent jobs from the source to the destination server.  
  
 This property has the options listed in the following table:  
  
|Value|Description|  
|-----------|-----------------|  
|**True**|Copy all jobs.|  
|**False**|Copy only the specified jobs.|  
  
 **JobsList**  
 Click the browse button **(...)** to select the jobs to copy. At least one job must be selected.  
  
> [!NOTE]  
>  Specify the **SourceConnection** before selecting jobs to copy.  
  
 The **JobsList** option is unavailable when **TransferAllJobs** is set to **True**.  
  
 **IfObjectExists**  
 Select how the task should handle jobs of the same name that already exist on the destination server.  
  
 This property has the options listed in the following table:  
  
|Value|Description|  
|-----------|-----------------|  
|**FailTask**|Task fails if jobs of the same name already exist on the destination server.|  
|**Overwrite**|Task overwrites jobs of the same name on the destination server.|  
|**Skip**|Task skips jobs of the same name that exist on the destination server.|  
  
 **EnableJobsAtDestination**  
 Select whether the jobs copied to the destination server should be enabled.  
  
 This property has the options listed in the following table:  
  
|Value|Description|  
|-----------|-----------------|  
|**True**|Enable jobs on destination server.|  
|**False**|Disable jobs on destination server.|  
  
## See Also  
 [Integration Services Error and Message Reference](../../2014/integration-services/integration-services-error-and-message-reference.md)   
 [Integration Services Tasks](control-flow/integration-services-tasks.md)   
 [Transfer Jobs Task Editor &#40;General Page&#41;](general-page-of-integration-services-designers-options.md)   
 [Expressions Page](expressions/expressions-page.md)   
 [SMO Connection Manager](connection-manager/smo-connection-manager.md)  
  
  
