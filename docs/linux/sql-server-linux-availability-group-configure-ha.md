---
title: Configure availability group for SQL Server on Linux
description: Learn about creating a SQL Server Always On availability group (AG) for high availability on Linux.
author: VanMSFT
ms.custom: seo-lt-2019
ms.author: vanto
ms.reviewer: vanto
ms.date: 08/24/2022
ms.topic: conceptual
ms.service: sql
ms.subservice: linux
ms.assetid: 
---
# Configure SQL Server Always On Availability Group for high availability on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article describes how to create a SQL Server Always On Availability Group (AG) for high availability on Linux. There are two configuration types for AGs. A *high availability* configuration uses a cluster manager to provide business continuity. This configuration can also include read-scale replicas. This document explains how to create the AG for high availability.

You can also create an AG without a cluster manager for *read-scale*. The AG for read scale only provides read-only replicas for performance scale-out. It does not provide high availability. To create an AG for read-scale, see [Configure a SQL Server Availability Group for read-scale on Linux](sql-server-linux-availability-group-configure-rs.md).

Configurations that guarantee high availability and data protection require either two or three synchronous commit replicas. With three synchronous replicas, the AG can automatically recover even if one server is not available. For more information, see [High availability and data protection for Availability Group configurations](sql-server-linux-availability-group-ha.md). 

All servers must be either physical or virtual, and virtual servers must be on the same virtualization platform. This requirement is because the fencing agents are platform specific. See [Policies for Guest Clusters](https://access.redhat.com/articles/29440#guest_policies).

## Roadmap

The steps to create an AG on Linux servers for high availability are different from the steps on a Windows Server failover cluster. The following list describes the high-level steps: 

1. [Configure SQL Server on three cluster servers](sql-server-linux-setup.md).

   >[!IMPORTANT]
   >All three servers in the AG need to be on the same platform - physical or virtual - because Linux high availability uses fencing agents to isolate resources on servers. The fencing agents are specific for each platform.

2. Create the AG. This step is covered in this current article. 

3. Configure a cluster resource manager, like Pacemaker.
   
   The way to configure a cluster resource manager depends on the specific Linux distribution. See the following links for distribution specific instructions: 

   * [RHEL](sql-server-linux-availability-group-cluster-pacemaker.md?tabs=rhel)
   * [SUSE](sql-server-linux-availability-group-cluster-pacemaker.md?tabs=sles)
   * [Ubuntu](sql-server-linux-availability-group-cluster-pacemaker.md?tabs=ubuntu)

   >[!IMPORTANT]
   >Production environments require a fencing agent, like STONITH for high availability. The demonstrations in this documentation do not use fencing agents. The demonstrations are for testing and validation only. 
   >
   >A Linux cluster uses fencing to return the cluster to a known state. The way to configure fencing depends on the distribution and the environment. Currently, fencing is not available in some cloud environments. For more information, see [Support Policies for RHEL High Availability Clusters - Virtualization Platforms](https://access.redhat.com/articles/29440).
   >
   >For SLES, see [SUSE Linux Enterprise High Availability Extension](https://www.suse.com/documentation/sle-ha-12/singlehtml/book_sleha/book_sleha.html#cha.ha.fencing).

5. Add the AG as a resource in the cluster.  

   The way to add the AG as a resource in the cluster depends on the Linux distribution. See the following links for distribution specific instructions: 

   * [RHEL](sql-server-linux-availability-group-cluster-pacemaker.md?tabs=rhel#create-availability-group-resource)
   * [SLES](sql-server-linux-availability-group-cluster-pacemaker.md?tabs=sles#configure-the-cluster-resources-for-sql-server)
   * [Ubuntu](sql-server-linux-availability-group-cluster-pacemaker.md?tabs=ubuntu#create-availability-group-resource)

### Considerations for multiple Network Interfaces (NICs)

For information on setting up an availability group for servers with multiple NICs, see the relevant sections for:

- [RHEL](sql-server-linux-availability-group-cluster-pacemaker.md?tabs=rhel#considerations-for-multiple-network-interfaces-nics)
- [SLES](sql-server-linux-availability-group-cluster-pacemaker.md?tabs=sles#considerations-for-multiple-network-interfaces-nics)
- [Ubuntu](sql-server-linux-availability-group-cluster-pacemaker.md?tabs=ubuntu#considerations-for-multiple-network-interfaces-nics)

[!INCLUDE [Create Prerequisites](includes/ss-linux-cluster-availability-group-create-prereq.md)]

## Create the AG

The examples in this section explain how to create the availability group using Transact-SQL. You can also use the SQL Server Management Studio Availability Group Wizard. When you create an AG with the wizard, it will return an error when you join the replicas to the AG. To fix this, grant `ALTER`, `CONTROL`, and `VIEW DEFINITIONS` to the pacemaker on the AG on all replicas. Once permissions are granted on the primary replica, join the nodes to the AG through the wizard, but for HA to function properly, grant permission on all replicas.

For a high availability configuration that ensures automatic failover, the AG requires at least three replicas. Either of the following configurations can support high availability:

- [Three synchronous replicas](sql-server-linux-availability-group-ha.md#threeSynch)

- [Two synchronous replicas plus a configuration replica](sql-server-linux-availability-group-ha.md#twoSynch)

For information, see [High availability and data protection for Availability Group configurations](sql-server-linux-availability-group-ha.md).

>[!NOTE]
>The availability groups can include additional synchronous or asynchronous replicas. 

Create the AG for high availability on Linux. Use the [CREATE AVAILABILITY GROUP](../t-sql/statements/create-availability-group-transact-sql.md) with `CLUSTER_TYPE = EXTERNAL`. 

* Availability group - `CLUSTER_TYPE = EXTERNAL` 
   Specifies that an external cluster entity manages the AG. Pacemaker is an example of an external cluster entity. When the AG cluster type is external, 

* Set Primary and secondary replicas `FAILOVER_MODE = EXTERNAL`. 
   Specifies that the replica interacts with an external cluster manager, like Pacemaker. 

The following Transact-SQL scripts create an AG for high availability named `ag1`. The script configures the AG replicas with `SEEDING_MODE = AUTOMATIC`. This setting causes SQL Server to automatically create the database on each secondary server. Update the following script for your environment. Replace the  `<node1>`, `<node2>`, or `<node3>` values with the names of the SQL Server instances that host the replicas. Replace the `<5022>` with the port you set for the data mirroring endpoint. To create the AG, run the following Transact-SQL on the SQL Server instance that hosts the primary replica.

> [!IMPORTANT]
> In the current implementation of the SQL Server resource agent, the node name must match the `ServerName` property from your instance. For example, if your node name is *node1*, make sure SERVERPROPERTY('ServerName') returns *node1* in your SQL Server instance. If there is a mismatch, your replicas will go into a resolving state after the pacemaker resource is created.
>
> A scenario where this rule is important is when using fully qualified domain names. For example, if you use *node1.yourdomain.com* as the node name during cluster setup, make sure SERVERPROPERTY('ServerName') returns *node1.yourdomain.com*, and not just *node1*. The possible workarounds for this problem are:
> - Rename your host name to the FQDN and use `sp_dropserver` and `sp_addserver` store procedures to ensure the metadata in SQL Server matches the change. 
> - Use the `addr` option in the `pcs cluster auth` command to match the node name to the SERVERPROPERTY('ServerName') value and use a static IP as the node address.

Run **only one** of the following scripts: 

- [Create Availability Group with three synchronous replicas](#threeSynch).
- [Create Availability Group with two synchronous replicas and a configuration replica](#configOnly)
- [Create Availability Group with two synchronous replicas](#readScale).

<a name="threeSynch"></a>

- Create AG with three synchronous replicas

   ```SQL
   CREATE AVAILABILITY GROUP [ag1]
       WITH (DB_FAILOVER = ON, CLUSTER_TYPE = EXTERNAL)
       FOR REPLICA ON
           N'<node1>' 
   	      	WITH (
		       ENDPOINT_URL = N'tcp://<node1>:<5022>',
		       AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
		       FAILOVER_MODE = EXTERNAL,
		       SEEDING_MODE = AUTOMATIC
		       ),
           N'<node2>' 
		    WITH ( 
		       ENDPOINT_URL = N'tcp://<node2>:<5022>', 
		       AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
		       FAILOVER_MODE = EXTERNAL,
		       SEEDING_MODE = AUTOMATIC
		       ),
		   N'<node3>'
           WITH( 
		      ENDPOINT_URL = N'tcp://<node3>:<5022>', 
		      AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
		      FAILOVER_MODE = EXTERNAL,
		      SEEDING_MODE = AUTOMATIC
		      );
   		
   ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE;
   ```

   >[!IMPORTANT]
   >After you run the preceding script to create an AG with three synchronous replicas, do not run the following script:

<a name="configOnly"></a>
- Create AG with two synchronous replicas and a configuration replica:

   >[!IMPORTANT]
   >This architecture allows any edition of SQL Server to host the third replica. For example, the third replica can be hosted on SQL Server Express Edition. On Express Edition, the only valid endpoint type is `WITNESS`. 

   ```SQL
   CREATE AVAILABILITY GROUP [ag1] 
      WITH (CLUSTER_TYPE = EXTERNAL) 
      FOR REPLICA ON 
       N'<node1>' WITH ( 
          ENDPOINT_URL = N'tcp://<node1>:<5022>', 
          AVAILABILITY_MODE = SYNCHRONOUS_COMMIT, 
          FAILOVER_MODE = EXTERNAL, 
          SEEDING_MODE = AUTOMATIC 
          ), 
       N'<node2>' WITH (  
          ENDPOINT_URL = N'tcp://<node2>:<5022>',  
          AVAILABILITY_MODE = SYNCHRONOUS_COMMIT, 
          FAILOVER_MODE = EXTERNAL, 
          SEEDING_MODE = AUTOMATIC 
          ), 
       N'<node3>' WITH ( 
          ENDPOINT_URL = N'tcp://<node3>:<5022>', 
          AVAILABILITY_MODE = CONFIGURATION_ONLY  
          );
   ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE;
   ```
<a name="readScale"></a>

- Create AG with two synchronous replicas

   Include two replicas with synchronous availability mode. For example, the following script creates an AG called `ag1`. `node1` and `node2` host replicas in synchronous mode, with automatic seeding and automatic failover.

   >[!IMPORTANT]
   >Only run the following script to create an AG with two synchronous replicas. Do not run the following script if you ran either preceding script. 

   ```SQL
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
      );
   		
   ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE;
   ```


You can also configure an AG with `CLUSTER_TYPE=EXTERNAL` using SQL Server Management Studio or PowerShell. 

### Join secondary replicas to the AG

The Pacemaker user requires `ALTER`, `CONTROL`, and `VIEW DEFINITION` permissions on the availability group on all replicas. To grant permissions, run the following Transact-SQL script after the availability group is created on the primary replica and each secondary replica immediately after they are added to the availability group. Before you run the script, replace `<pacemakerLogin>` with the name of the Pacemaker user account. If you do not have a login for Pacemaker, [create a sql server login for Pacemaker](sql-server-linux-availability-group-cluster-pacemaker.md?tabs=ubuntu#create-a-sql-server-login-for-pacemaker).

```sql
GRANT ALTER, CONTROL, VIEW DEFINITION ON AVAILABILITY GROUP::ag1 TO <pacemakerLogin>
GRANT VIEW SERVER STATE TO <pacemakerLogin>
```

The following Transact-SQL script joins a SQL Server instance to an AG named `ag1`. Update the script for your environment. On each SQL Server instance that hosts a secondary replica, run the following Transact-SQL to join the AG.

```sql
ALTER AVAILABILITY GROUP [ag1] JOIN WITH (CLUSTER_TYPE = EXTERNAL);
		 
ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE;
```

[!INCLUDE [Create Post](includes/ss-linux-cluster-availability-group-create-post.md)]

>[!IMPORTANT]
>After you create the AG, you must configure integration with a cluster technology like Pacemaker for high availability. For a read-scale configuration using AGs, starting with [!INCLUDE [SQL Server version](../includes/sssql17-md.md)], setting up a cluster is not required.

If you followed the steps in this document, you have an AG that is not yet clustered. The next step is to add the cluster. This configuration is valid for read-scale/load balancing scenarios, it is not complete for high availability. For high availability, you need to add the AG as a cluster resource. See [Next steps](#next-steps) for instructions. 

## Notes

>[!IMPORTANT]
>After you configure the cluster and add the AG as a cluster resource, you cannot use Transact-SQL to fail over the AG resources. SQL Server cluster resources on Linux are not coupled as tightly with the operating system as they are on a Windows Server Failover Cluster (WSFC). SQL Server service is not aware of the presence of the cluster. All orchestration is done through the cluster management tools. In RHEL or Ubuntu use `pcs`. In SLES use `crm`. 

>[!IMPORTANT]
>If the AG is a cluster resource, there is a known issue in current release where forced failover with data loss to an asynchronous replica does not work. This will be fixed in the upcoming release. Manual or automatic failover to a synchronous replica succeeds.

## Next steps

[Configure Red Hat Enterprise Linux Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-pacemaker.md?tabs=rhel)

[Configure SUSE Linux Enterprise Server Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-pacemaker.md?tabs=sles)

[Configure Ubuntu Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-pacemaker.md?tabs=ubuntu)
