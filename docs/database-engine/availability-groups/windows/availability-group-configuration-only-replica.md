---
title: "Availability Group - with configuration only replica | Microsoft Docs"
ms.custom: ""
ms.date: "10/16/2017"
ms.prod: 
 - "sql-server-2016"
 - "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-high-availability"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: ""
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "jhubbard"
---
# Availability group with configuration only replica

The feature described in this article is introduced in SQL Server 2017 CU 1.

A SQL Server Always On availability group on a Windows Server Failover Cluster (WSFC) provides high availability with two synchronous replicas and a file-share witness. The WSFC synchronizes configuration metadata for failover arbitration between the availability group replicas and the file-share witness. When an availability group is not on a WSFC, the SQL Server instances store configuration metadata in the master database. This applies to availability groups on Linux, where `CLUSTER_TYPE = EXTERNAL` or on any operating system where `CLUSTER_TYPE = NONE`. In these cases, three synchronous replicas are required for high availability prior to SQL Server 2017 CU 1. 

SQL Server 2017 CU 1 enables high availability for an availability group with external or none cluster types for two synchronous replicas plus a *configuration only* replica. In a configuration only replica, the instance of SQL Server maintains configuration information about the availability group in the master database. The configuration only replica does not contain the databases in the availability group. The following diagram shows an availability group with a configuration only replica:

<!-- Add Diagram -->


## Capabilities 
Any edition of SQL Server can host a configuration only replica, including SQL Server Express. You can not create an availability group on an instance of SQL Server Express edition. 

A configuration only replica joins an availability group as secondary replica. 

The configuration only replica does not contain the databases in the availability group - only metadata about the availability group. 

Configuration only replica has availability mode `CONFIGURATION_ONLY`. 

Configuration only replicas do not count towards the maximum number of replicas per instance of SQL Server. SQL Server standard edition allows up to 3 replicas, SQL Server Enterprise Edition allows up to 9.


## Limits

No more than one configuration only replica per availability group. 

A configuration only replica can not be a primary replica.

You can not modify the availability mode of a configuration only replica. To change from a configuration only replica to a synchronous or asynchronous secondary replica, remove the configuration only replica, and add a secondary replica with the required availability mode. 

There is no user data. A configuration only replica is synchronous with the availability group metadata.


## Example

The following T-SQL example creates an availability group with two synchronous replicas and a configuration only replica. 

```sql
CREATE AVAILABILITY GROUP [ag1] 
    WITH (CLUSTER_TYPE = EXTERNAL) 
    FOR REPLICA ON 
    N'node1' WITH ( 
        ENDPOINT_URL = N'tcp://node1:5022', 
        AVAILABILITY_MODE = SYNCHRONOUS_COMMIT, 
        FAILOVER_MODE = EXTERNAL, 
        SEEDING_MODE = AUTOMATIC 
    ), 
    N'node2' WITH (  
        ENDPOINT_URL = N'tcp://node2:5022',  
        AVAILABILITY_MODE = SYNCHRONOUS_COMMIT, 
        FAILOVER_MODE = EXTERNAL, 
        SEEDING_MODE = AUTOMATIC 
    ), 
    N'node3' WITH ( 
        ENDPOINT_URL = N'tcp://node3:5022', 
        AVAILABILITY_MODE = CONFIGURATION_ONLY  
    ) 
```

In the preceding example, node three contains a configuration only replica. There are no additional options available for the configuration only replica. 

## Compare replica architectures

### Avalability group with two synchronous replicas and a configuration only replica

An availability group with two synchronous replicas and a configuration only replica provides data protection and may also provide high availability. The value assigned to `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` determines how these features are enabled. The following table describes the behavior. 

| |High availability & </br> data protection | Data protection
|:---|---|---
|`REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT=`|0 <sup>*</sup>|1
|Primary outage | Automatic failover. New primary is R/W | Automatic failover. New primary is not available for user transactions. 
|Secondary replica outage | Primary is R/W, running exposed to data loss (if primary fails and cannot be recovered). No automatic failover if primary fails as well. | Primary is not available for user transactions. No replica to fail over if primary fails as well. 
|Configuration only replica outage | Primary is R/W. No automatic failover if primary fails as well. | Primary is R/W. No automatic failover if primary fails as well. 
|Synchronous secondary + configuration only replica outage| Primary is not available for user transactions. No automatic failover. | Primary is not available for user transactions. No replica to failover to is primary fails as well. 

<sup>*</sup> Default
##  <a name="RelatedContent"></a> Related Content  
  
## See Also  
  
  
