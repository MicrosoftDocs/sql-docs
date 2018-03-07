---
title: "Secondary to primary replica connection redirection-Always On Availability Groups | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine"
ms.service: ""
ms.component: "availability-groups"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "dbe-high-availability"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "connection access to availability replicas"
  - "Availability Groups [SQL Server], availability replicas"
  - "Availability Groups [SQL Server], readable secondary replicas"
  - "active secondary replicas [SQL Server], read-only access to"
  - "readable secondary replicas"
  - "Availability Groups [SQL Server], active secondary replicas"
ms.assetid:
caps.latest.revision: 80
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "craigg"
ms.workload: "On Demand"
---
# Secondary to primary replica connection redirection (Always On Availability Groups)
[!INCLUDE[appliesto-sssqlv15-xxxx-xxxx-xxx-md](../../../includes/tsql-appliesto-ssvnext-xxxx-xxxx-xxx.md)]

An availability group (AG) listener and the corresponding cluster resource directs user traffic to the primary replica to ensure transparent reconnection after failover. In some cases, this capability is not available. For example:
- Service fabric
- Complex, error-prone architectures, like a multi-subnet cloud configuration or multi-subnet floating IP with Pacemaker
- The AG is configured with cluster-type `NONE` for read scale-out or DR

To solve these challenges, [!INCLUDE[sssqlv15-md](../../../includes/sssqlv15-md.md)] introduces *secondary to primary replica connection redirection*. This feature allows client applications to connect to any of the replicas of the availability group and the connection will be redirected to the primary replica, according to the AG configuration.

## READ_WRITE_ROUTING_URL option

When you create the AG, set the read/write routing URL for the primary replica to indicate how to route this replica when it becomes primary. To enable this use `READ_WRITE_ROUTING_URL`.

```sql
  <add_replica_option>::=  
       SEEDING_MODE = { AUTOMATIC | MANUAL }  
     | BACKUP_PRIORITY = n  
     | SECONDARY_ROLE ( {   
            [ ALLOW_CONNECTIONS = { NO | READ_ONLY | ALL } ]   
        [,] [ READ_ONLY_ROUTING_URL = 'TCP://system-address:port' ]  
     } )  
     | PRIMARY_ROLE ( {   
            [ ALLOW_CONNECTIONS = { READ_WRITE | ALL } ]   
        [,] [ READ_ONLY_ROUTING_LIST = { ( ‘<server_instance>’ [ ,...n ] ) | NONE } ]  
        [,] [ READ_WRITE_ROUTING_URL = { ( ‘<server_instance>’ ) ] 
     } )  
     | SESSION_TIMEOUT = integer
```

See [CREATE AVAILABILITY GROUP](../../../t-sql\statements\create-availability-group-transact-sql.md).

## PRIMARY_ROLE(READ_WRITE_ROUTING_URL) not set

||`SECONDARY_ROLE (ALLOW CONNECTIONS = NO)`|`SECONDARY_ROLE (ALLOW CONNECTIONS = READ_ONLY)`|`SECONDARY_ROLE (ALLOW CONNECTIONS = ALL)`|
|-----|-----|-----|-----|
|`ApplicationIntent=ReadWrite`<br/>or<br/>`ApplicationIntent` not set|Connection fail|Connection fail|Connection succeed<br/>Reads succeeds<br/>Writes fail|

|`SECONDARY_ROLE (ALLOW CONNECTIONS = )`|`NO`|`READ_ONLY`|`ALL`|
|-----|-----|-----|-----|
|`ApplicationIntent=ReadWrite`<br/>or<br/>`ApplicationIntent` not set|Connection fail|Connection fail|Connection succeed<br/>Reads succeeds<br/>Writes fail|


## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [About Client Connection Access to Availability Replicas &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/about-client-connection-access-to-availability-replicas-sql-server.md)   
 [Availability Group Listeners, Client Connectivity, and Application Failover &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/listeners-client-connectivity-application-failover.md)   
 [Statistics](../../../relational-databases/statistics/statistics.md)  
  
  
