---
title: "MSSQLSERVER_21892 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "21892 (Database Engine error)"
ms.assetid: 199818e8-28f4-440e-ad85-7bea88f51153
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_21892
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|21892|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SQLErrorNum21892|  
|Message Text|Unable to query sys.availability_replicas at the availability group primary associated with virtual network name '%s' for the server names of the member replicas: error = %d, error message = %s.',|  
  
## Explanation  
 `sp_validate_replica_hosts_as_publishers` queries the current primary of the availability group associated with the redirected publisher, to determine the instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that host the member replicas.  When this query fails, error 21892 is returned.  
  
 `sp_validate_replica_hosts_as_publishers` is typically on of the first use of the temporary linked server, so if there are connectivity problems they are likely to appear first with `sp_validate_replica_hosts_as_publishers`. Unlike `sp_validate_redirected_publisher`, the linked server used by `sp_validate_replica_hosts_as_publishers` always uses the credentials of the caller when connecting to any of the availability group replica hosts.  
  
## User Action  
 When running this stored procedure, made certain that you run from a login that is valid on all of the replicas. The login will require sufficient authorization to query availability group metadata tables as well as to query the subscription metadata tables in the publisher database replica.  
  
 Examine the original referenced error to determine the cause of the failure and appropriate corrective action.  
  
  
