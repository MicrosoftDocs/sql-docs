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
author: "MikeRayMSFT"
ms.author: "mikeray"
manager: "jhubbard"
---
# Availability group with configuration only replica

The feature described in this article is introduced in SQL Server 2017 CU 1.

A SQL Server Always On availability group on a Windows Server Failover Cluster (WSFC) provides high availability with two synchronous replicas and a file-share witness. The WSFC synchronizes configuration metadata for failover arbitration between the availability group replicas and the file-share witness. When an availability group is not on a WSFC, the SQL Server instances store configuration metadata in the master database. For example, an availability group on a Linux cluster has `CLUSTER_TYPE = EXTERNAL` and requires three synchronous replicas are required for high availability prior to SQL Server 2017 CU 1. 

SQL Server 2017 CU 1 enables high availability for an availability group with external or none cluster types for two synchronous replicas plus a *configuration only* replica. In a configuration only replica, the instance of SQL Server maintains configuration information about the availability group in the master database. The configuration only replica does not contain the user databases in the availability group. The following diagram shows an availability group with a configuration only replica:

![Availability group with configuration only replica][1]

In the availability group diagram, a primary replica pushes configuration data to both the secondary replica and the configuration only replica. The secondary replica also receives user data. The configuration only replica does not receive user data. The secondary replica is in synchronous availability mode. 

The configuration only replica does not contain the databases in the availability group - only metadata about the availability group. 

## Example

Configuration only replica availability mode is `CONFIGURATION_ONLY`. A configuration only replica is defined when the availability group is created. The following T-SQL example creates an availability group with two synchronous replicas and a configuration only replica.

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

In the preceding example, node three is configuration only replica. There are no additional options available for the configuration only replica. 

## Compare replica architecture availability behavior

SQL Server 2017 introduces the `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` cluster resource setting. This setting guarantees the specified number of secondary replicas write the transaction data to log before the primary replica commits each transaction. When you use an external cluster manager, this setting affects both high availability and data protection. The default value for the setting depends on the architecture at the time the cluster resource is created.

### Two synchronous replicas and a configuration only replica

An availability group with two (or more) synchronous replicas and a configuration only replica provides data protection and may also provide high availability. The default value for `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` is 0. The following table describes availability behavior. 

| |High availability & </br> data protection | Data protection
|:---|---|---
|`REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT=`|0 <sup>*</sup>|1
|Primary outage | Automatic failover. New primary is R/W. | Automatic failover. New primary is not available for user transactions. 
|Secondary replica outage | Primary is R/W, running exposed to data loss (if primary fails and cannot be recovered). No automatic failover if primary fails as well. | Primary is not available for user transactions. No replica to fail over to if primary fails as well. 
|Configuration only replica outage | Primary is R/W. No automatic failover if primary fails as well. | Primary is R/W. No automatic failover if primary fails as well. 
|Synchronous secondary + configuration only replica outage| Primary is not available for user transactions. No automatic failover. | Primary is not available for user transactions. No replica to failover to if primary fails as well. 
<sup>*</sup> Default

### Two synchronous replicas

An availability group with two synchronous replicas provides read scale-out and data protection. The default value for `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` is 0. The following table describes availability behavior. 

| |Read scale-out |Data protection
|:---|---|---
|`REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT=`|0 <sup>*</sup>|1
|Primary outage | Manual failover. Might have data loss. New primary is R/W.| Automatic failover. New primary is not available for user transactions until former primary recovers and joins availability group as secondary.
|One secondary replica outage  |Primary is R/W, running exposed to data loss. |Primary is not available for user transactions until secondary recovers.
<sup>*</sup> Default

>[!NOTE]
>The preceding scenario is the behavior prior to SQL Server 2017 CU 1. 

### Three synchronous replicas

An availability group with three synchronous replicas can provide read scale-out, high availability, and data protection. The default value for `REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT` is 1. The following table describes availability behavior. 

| |Read scale-out|High availability & </br> data protection | Data protection
|:---|---|---|---
|`REQUIRED_SYNCHRONIZED_SECONDARIES_TO_COMMIT=`|0 |1<sup>*</sup>|2
|Primary outage | Manual failover. Might have data loss. New primary is R/W. |Automatic failover. New primary is R/W. |Automatic failover. New primary is not available for user transactions until former primary recovers and joins availability group as secondary. 
|One secondary replica outage  | Primary is R/W. No automatic failover if primary fails. |Primary is R/W. No automatic failover if primary fails as well. | Primary is not available for user transactions. 
<sup>*</sup> Default value set by pacemaker

## Requirements

Any edition of SQL Server can host a configuration only replica, including SQL Server Express. 

The availability group needs at least one secondary replica - in addition to the primary replica.

Configuration only replicas do not count towards the maximum number of replicas per instance of SQL Server. SQL Server standard edition allows up to three replicas, SQL Server Enterprise Edition allows up to 9.

## Limits

No more than one configuration only replica per availability group. 

A configuration only replica cannot be a primary replica.

You cannot modify the availability mode of a configuration only replica. To change from a configuration only replica to a synchronous or asynchronous secondary replica, remove the configuration only replica, and add a secondary replica with the required availability mode. 

There is no user data. A configuration only replica is synchronous with the availability group metadata.

An availability group with one primary replica and one configuration only replica, but no secondary replica is not valid. 

You cannot create an availability group on an instance of SQL Server Express edition. 

##  <a name="RelatedContent"></a> Related Content  
  
## See Also  
  
  
[1]:./media/availability-group-configuration-only-replica/configuration-only-example.png