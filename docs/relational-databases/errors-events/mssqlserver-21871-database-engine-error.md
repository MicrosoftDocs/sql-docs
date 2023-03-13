---
title: "MSSQLSERVER_21871"
description: "MSSQLSERVER_21871"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "21871 (Database Engine error)"
---
# MSSQLSERVER_21871
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|21871|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SQLErrorNum21871|  
|Message Text|Publisher %s of database %s has not been redirected.|  
  
## Explanation  
**sp_validate_replica_hosts_as_publishers** checks the table MSredirected_publishers in the distribution database for an entry for the identified publisher and publisher database.  **sp_validate_replica_hosts_as_publishers** returns error 21871 when no entry is found.  
  
## User Action  
**sp_validate_replica_hosts_as_publishers** is only relevant for redirected publishers. If the publisher database is a member of an availability group, use the stored procedure **sp_redirect_publisher** to associate the publisher and publisher database with the Availability Group Listener Name of the availability group.  
  
