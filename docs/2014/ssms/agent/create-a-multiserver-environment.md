---
title: "Create a Multiserver Environment | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Agent, multiserver environments"
  - "master servers [SQL Server], about master servers"
  - "target servers [SQL Server], about target servers"
  - "multiserver environments [SQL Server]"
ms.assetid: edc2b60d-15da-40a1-8ba3-f1d473366ee6
author: stevestein
ms.author: sstein
manager: craigg
---
# Create a Multiserver Environment
  Multiserver administration requires that you set up a master server (MSX) and one or more target servers (TSX). Jobs that will be processed on all the target servers are first defined on the master server and then downloaded to the target servers.  
  
 By default, full Secure Sockets Layer (SSL) encryption and certificate validation are enabled for connections between master servers and target servers. For more information, see [Set Encryption Options on Target Servers](set-encryption-options-on-target-servers.md).  
  
 If you have a large number of target servers, avoid defining your master server on a production server that has significant performance requirements from other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] functionality, because target server traffic can slow performance on your production server. If you also forward events to a dedicated master server, you can centralize administration on one server. For more information, see [Manage Events](manage-events.md).  
  
> [!NOTE]  
>  To use multiserver job processing, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account must be a member of the **msdb** database role **TargetServersRole** on the master server. The Master Server Wizard automatically adds the service account to this role as part of the enlistment process  
  
## Considerations for Multiserver Environments  
 See the table below for supported MSX/TSX configurations.  
  
||**TSX = 7.0**|**TSX = 8.0 < SP3**|**TSX = 8.0 SP3 or higher**|**TSX = 9.0**|**TSX= 10.0**|**TSX = 10.5**|**TSX = 11.0**|  
|-|--------------------|---------------------------|----------------------------------|--------------------|--------------------|---------------------|---------------------|  
|**MSX = 7.0**|Yes|Yes|No|No|No|No|No|  
|**MSX = 8.0 < SP3**|Yes|Yes|No|No|No|No|No|  
|**MSX = 8.0 SP3 or higher**|No|No|Yes|Yes|Yes|Yes|Yes|  
|**MSX = 9.0**|No|No|No|Yes|Yes|Yes|Yes|  
|**MSX = 10.0**|No|No|No|No|Yes|Yes|Yes|  
|**MSX = 10.5**|No|No|No|No|No|Yes|Yes|  
|**MSX = 11.0**|No|No|No|No|No|No|Yes|  
  
 Consider the following issues when creating a multiserver environment:  
  
-   Each target server reports to only one master server. You must defect a target server from one master server before you can enlist it into a different one.  
  
-   When changing the name of a target server, you must defect it before changing the name and re-enlist it after the change.  
  
-   If you want to dismantle a multiserver configuration, you must defect all the target servers from the master server.  
  
-   SQL Server Integration Services only supports target servers that are the same version or higher than the master server version.  
  
## Related Tasks  
 The following topics document common tasks for creating a multiserver environment.  
  
|Description|Topic|  
|-----------------|-----------|  
|Describes how to create a master server.|[Make a Master Server](make-a-master-server.md)|  
|Describes how to create a target server.|[Make a Target Server](make-a-target-server.md)|  
|Describes how to enlist a target server into a master server.|[Enlist a Target Server to a Master Server](enlist-a-target-server-to-a-master-server.md)|  
|Describes how to defect a target server from a master server.|[Defect a Target Server from a Master Server](defect-a-target-server-from-a-master-server.md)|  
|Describes how to defect multiple target servers from a master server.|[Defect Multiple Target Servers from a Master Server](defect-multiple-target-servers-from-a-master-server.md)|  
|Describes how to check the status of a target server.|[sp_help_targetserver &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-help-targetserver-transact-sql)<br /><br /> [sp_help_targetservergroup &#40;Transact-SQL&#41;](/sql/relational-databases/system-stored-procedures/sp-help-targetservergroup-transact-sql)|  
  
## See Also  
 [Troubleshoot Multiserver Jobs That Use Proxies](troubleshoot-multiserver-jobs-that-use-proxies.md)  
  
  
