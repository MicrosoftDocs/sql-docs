---
# required metadata

title: Configure availability group for SQL Server on Linux | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 06/14/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 

# optional metadata
# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---

# Configure Always On availability group for SQL Server on Linux

This article describes how to create a SQL Server Always on availability group with three replicas for high availability on Linux. You can configure an availability group for SQL Server on Linux. There are two configuration type for availability groups. A *high availability* configuration has three servers and uses a cluster manager to provide business continuity. This configuration can also include read scale-out replicas. This document explains how to create the availability group high availability configuration.

You can also create a *read scale-out* availability group without a cluster manager. This configuration only provides read-only replicas for performance scale-out. It does not provide high availability. To create a read scale-out availability group, see [Configure read scale-out availability group for SQL Server on Linux](sql-server-linux-availability-group-configure-rs.md).

Configurations that guarantee high availability and data protection require either three synchronous commit replicas or two synchronous replicas and a witness. For details, see [High availability and data protection for availability group configurations](sql-server-linux-availability-group-ha.md). 

All servers must be either physical or virtual, and virtual servers must be on the same virtualization platform. This requirement is because the fencing agents are platform specific. See [Policies for Guest Clusters](https://access.redhat.com/articles/29440#guest_policies).

## Roadmap

The steps to create an availability group on Linux servers for high availability are different from the steps on a Windows Server failover cluster. The following list describes the high level steps: 

1. [Configure SQL Server on three cluster servers](sql-server-linux-setup.md).

   >[!IMPORTANT]
   >All three servers in the availability group need to be on the same platform - physical or virtual - because Linux high availability uses fencing agents to isolate resources on servers. The fencing agents are specific for each platform.

2. Create the availability group. This step is covered in this current article. 

3. Configure a cluster resource manager, like Pacemaker.
   
   The way to configure a cluster resource manager depends on the specific Linux distribution. See the following links for distribution specific instructions: 

   * [RHEL](sql-server-linux-availability-group-cluster-rhel.md)
   * [SUSE](sql-server-linux-availability-group-cluster-sles.md)
   * [Ubuntu](sql-server-linux-availability-group-cluster-ubuntu.md)

   >[!IMPORTANT]
   >Production environments require a fencing agent, like STONITH for high availability. The demonstrations in this documentation do not use fencing agents. The demonstrations are for testing and validation only. 
   
   >A Linux cluster uses fencing to return the cluster to a known state. The way to configure fencing depends on the distribution and the environment. Currently, fencing is not available in some cloud environments. For more information, see [Support Policies for RHEL High Availability Clusters - Virtualization Platforms](https://access.redhat.com/articles/29440).
   
   >For SLES, see [SUSE Linux Enterprise High Availability Extension](https://www.suse.com/documentation/sle-ha-12/singlehtml/book_sleha/book_sleha.html#cha.ha.fencing).

5. Add the availability group as a resource in the cluster.  

   The way to add the availability group as a resource in the cluster depends on the Linux distribution. See the following links for distribution specific instructions: 

   * [RHEL](sql-server-linux-availability-group-cluster-rhel.md#create-availability-group-resource)
   * [SLES](sql-server-linux-availability-group-cluster-sles.md#configure-the-cluster-resources-for-sql-server)
   * [Ubuntu](sql-server-linux-availability-group-cluster-ubuntu.md#create-availability-group-resource)

[!INCLUDE [Create Prerequisites](../includes/ss-linux-cluster-availability-group-create-prereq.md)]

## Create the availability group

There are three availability group configurations.

- [Three synchronous replicas](sql-server-linux-availability-group-ha.md#threeSynch)

- [Two synchronous replicas and a witness](sql-server-linux-availability-group-ha.md#witness)

- [Two synchronous replicas](sql-server-linux-availability-group-ha.md#twoSynch)

For high availability, use either three synchronous replicas, or two synchronous replicas and a witness. 

For information about all three configurations, see [High availability and data protection for availability group configurations](sql-server-linux-availability-group-ha.md).

Create the availability group for high availability on Linux. Use the [CREATE AVAILABILITY GROUP](https://docs.microsoft.com/en-us/sql/t-sql/statements/create-availability-group-transact-sql) with `CLUSTER_TYPE = EXTERNAL`. 

* Availability group - `CLUSTER_TYPE = EXTERNAL` 
   Specifies that an external cluster entity manages the availability group. Pacemaker is an example of an external cluster entity. When the availability group cluster type is external, 

* Set Primary and secondary replicas `FAILOVER_MODE = EXTERNAL`. 
   Specifies that the replica interacts with an external cluster manager, like Pacemaker. 

The following Transact-SQL scripts creates an availability group for high availability named `ag1`. The script configures the availability group replicas with `SEEDING_MODE = AUTOMATIC`. This setting causes SQL Server to automatically create the database on each secondary server. Update the following script for your environment. Replace the  `**<node1>**`, and `**<node2>**` values with the names of the SQL Server instances that host the replicas. Replace `**<node3**` with the value for the witness. Replace the `**<5022>**` with the port you set for the data mirroring endpoint. To create the availability group, run the following Transact-SQL on the SQL Server instance that hosts the primary replica.

Run **only one** of the following scripts: 

- Create availability group with three synchronous replicas.

   ```Transact-SQL
   CREATE AVAILABILITY GROUP [ag1]
       WITH (DB_FAILOVER = ON, CLUSTER_TYPE = EXTERNAL)
       FOR REPLICA ON
           N'**<node1>**' 
   	      	WITH (
		       ENDPOINT_URL = N'tcp://**<node1>:**<5022>**',
		       AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
		       FAILOVER_MODE = EXTERNAL,
		       SEEDING_MODE = AUTOMATIC
		       ),
           N'**<node2>**' 
		    WITH ( 
		       ENDPOINT_URL = N'tcp://**<node2>**:**<5022>**', 
		       AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
		       FAILOVER_MODE = EXTERNAL,
		       SEEDING_MODE = AUTOMATIC
		       ),
		   N'**<node3>**'
           WITH( 
		      ENDPOINT_URL = N'tcp://**<node3>**:**<5022>**', 
		      AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
		      FAILOVER_MODE = EXTERNAL,
		      SEEDING_MODE = AUTOMATIC
		      );
   		
   ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE;
   ```

   >[!IMPORTANT]
   >After you run the preceding script to create an availability group with three synchronous replicas, do not run the following script:

<a name="witnessScript"></a>

- Create availability group with two synchronous replicas and a witness

   Include two replicas with synchronous availability mode and a witness. For example, the following script creates an availability group called `ag1`. `node1` and `node2` host replicas in synchronous mode, with automatic seeding and automatic failover. `node3` is a witness. The script defines only the SQL Server instance name `node3`, the endpoint, and the availability mode.

   >[!IMPORTANT]
   >Only run the following script to create an availability group with two synchronous replicas and a witness. Do not run the following script if you ran the preceding script. 

   ```Transact-SQL
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
         AVAILABILITY_MODE = WITNESS_COMMIT
      );
   		
   ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE;
   ```


You can also configure an availability group with `CLUSTER_TYPE=EXTERNAL` using SQL Server Management Studio or PowerShell. 

### Join secondary replicas to the availability group

The following Transact-SQL script joins a SQL Server instance to an availability group named `ag1`. Update the script for your environment. On each SQL Server instance that hosts a secondary replica, run the following Transact-SQL to join the availability group.

```Transact-SQL
ALTER AVAILABILITY GROUP [ag1] JOIN WITH (CLUSTER_TYPE = EXTERNAL);
		 
ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE;
```

[!INCLUDE [Create Post](../includes/ss-linux-cluster-availability-group-create-post.md)]

>[!IMPORTANT]
>After you create the availability group, you must configure integration with a cluster technology like Pacemaker for high availability. For a read scale-out configuration using availability groups, starting with [!INCLUDE [SQL Server version](..\includes\sssqlv14-md.md)], setting up a cluster is not required.

If you followed the steps in this document, you have an availability group that is not yet clustered. The next step is to add the cluster. This configuration is valid for read scale-out/load balancing scenarios, it is not complete for high availability. For high availability, you need to add the availability group as a cluster resource. See [Next steps](#next-steps) for instructions. 

## Notes

>[!IMPORTANT]
>After you configure the cluster and add the availability group as a cluster resource, you cannot use Transact-SQL to fail over the availability group resources. SQL Server cluster resources on Linux are not coupled as tightly with the operating system as they are on a Windows Server Failover Cluster (WSFC). SQL Server service is not aware of the presence of the cluster. All orchestration is done through the cluster management tools. In RHEL or Ubuntu use `pcs`. In SLES use `crm`. 

>[!IMPORTANT]
>If the availability group is a cluster resource, there is a known issue in current release where forced failover with data loss to an asynchronous replica does not work. This will be fixed in the upcoming release. Manual or automatic failover to a synchronous replica succeeds. 


## Next steps

[Configure Red Hat Enterprise Linux Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-rhel.md)

[Configure SUSE Linux Enterprise Server Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-sles.md)

[Configure Ubuntu Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-ubuntu.md)
