---
title: Configure an Availability Group for High Availability
titleSuffix: SQL Server on Linux
description: Learn about creating a SQL Server Always On availability group (AG) for high availability on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 09/14/2026
ms.service: sql
ms.subservice: linux
ms.topic: how-to
ms.custom:
  - linux-related-content
---
# Configure SQL Server availability group for high availability on Linux

[!INCLUDE [SQL Server - Linux](../../../includes/applies-to-version/sql-linux.md)]

This article describes how to create a SQL Server Always On availability group (AG) for high availability on Linux. There are two configuration types for AGs. A *high availability* configuration uses a cluster manager to provide business continuity. This configuration can also include read-scale replicas. This article explains how to create the AG for high availability.

You can also create an AG without a cluster manager for *read-scale*. The AG for read scale only provides read-only replicas for performance scale-out. It doesn't provide high availability. To create an AG for read-scale, see [Configure a SQL Server availability group for read-scale on Linux](configure-read-scale.md).

Configurations that guarantee high availability and data protection require either two or three synchronous commit replicas. With three synchronous replicas, the AG can automatically recover even if one server isn't available. For more information, see [High availability and data protection for availability group configurations](high-availability.md).

All servers must be either physical or virtual, and virtual servers must be on the same virtualization platform. This requirement exists because the fencing agents are platform specific. See [Policies for Guest Clusters](https://access.redhat.com/articles/2912891#guest_policies).

## Installation steps

The steps to create an AG on Linux servers for high availability differ from the steps on a Windows Server failover cluster. The following list describes the high-level steps:

1. [Installation guidance for SQL Server on Linux](../../install-upgrade/setup.md).

   > [!IMPORTANT]  
   > All three servers in the AG need to be on the same platform - physical or virtual - because Linux high availability uses fencing agents to isolate resources on servers. The fencing agents are specific for each platform.

1. Create the AG. This step is covered in this current article.

1. Configure a cluster resource manager, like Pacemaker.

   The way to configure a cluster resource manager depends on the specific Linux distribution. See the following links for distribution specific instructions:

   - [RHEL](cluster-pacemaker.md?tabs=rhel)
   - [SUSE](cluster-pacemaker.md?tabs=sles)
   - [Ubuntu](cluster-pacemaker.md?tabs=ubuntu)

   > [!IMPORTANT]  
   > Production environments require a fencing agent for high availability. The examples in this article don't use fencing agents. They're for testing and validation only.
   >
   > A Pacemaker cluster uses fencing to return the cluster to a known state. The way to configure fencing depends on the distribution and the environment. Currently, fencing isn't available in some cloud environments. For more information, see [Support Policies for RHEL High Availability Clusters - Virtualization Platforms](https://access.redhat.com/articles/2912891).
   >
   > For SLES, see [SUSE Linux Enterprise High Availability Extension](https://documentation.suse.com/sle-ha/12-SP5/#redirectmsg).

1. Add the AG as a resource in the cluster.

   The way to add the AG as a resource in the cluster depends on the Linux distribution. See the following links for distribution specific instructions:

   - [RHEL](cluster-pacemaker.md?tabs=rhel#create-availability-group-resource)
   - [SLES](cluster-pacemaker.md?tabs=sles#configure-the-cluster-resources-for-sql-server)
   - [Ubuntu](cluster-pacemaker.md?tabs=ubuntu#create-availability-group-resource)

### Considerations for multiple network interfaces (NICs)

For information on setting up an availability group for servers with multiple NICs, see the relevant sections for:

- [RHEL](cluster-pacemaker.md?tabs=rhel#considerations-for-multiple-network-interfaces-nics)
- [SLES](cluster-pacemaker.md?tabs=sles#considerations-for-multiple-network-interfaces-nics)
- [Ubuntu](cluster-pacemaker.md?tabs=ubuntu#considerations-for-multiple-network-interfaces-nics)

[!INCLUDE [Create Prerequisites](../../includes/cluster-availability-group-create-prereq.md)]

## Create the AG

The examples in this section explain how to create the availability group using Transact-SQL. You can also use the SQL Server Management Studio Availability Group Wizard. When you create an AG using the wizard, it returns an error when you join the replicas to the AG. To fix this error, grant `ALTER`, `CONTROL`, and `VIEW DEFINITIONS` to the pacemaker on the AG on all replicas. Once you grant permissions on the primary replica, join the nodes to the AG through the wizard, but for HA to function properly, grant permission on all replicas.

For a high availability configuration that ensures automatic failover, the AG requires at least three replicas. Either of the following configurations can support high availability:

- [Three synchronous replicas](high-availability.md#threeSynch)

- [Two synchronous replicas plus a configuration replica](high-availability.md#twoSynch)

For more information, see [High availability and data protection for availability group configurations](high-availability.md).

> [!NOTE]  
> The availability groups can include additional synchronous or asynchronous replicas.

Create the AG for high availability on Linux. Use the [CREATE AVAILABILITY GROUP](../../../t-sql/statements/create-availability-group-transact-sql.md) statement with `CLUSTER_TYPE = EXTERNAL`.

- Availability group: `CLUSTER_TYPE = EXTERNAL`.

  Specifies that an external cluster entity manages the AG. Pacemaker is an example of an external cluster entity. When the AG cluster type is external,

- Set primary and secondary replicas: `FAILOVER_MODE = EXTERNAL`.

  Specifies that the replica interacts with an external cluster manager, like Pacemaker.

The following Transact-SQL scripts create an AG for high availability named `ag1`. The script configures the AG replicas with `SEEDING_MODE = AUTOMATIC`. This setting causes SQL Server to automatically create the database on each secondary server. Update the following script for your environment. Replace the `<node1>`, `<node2>`, or `<node3>` values with the names of the SQL Server instances that host the replicas. Replace the `<5022>` with the port you set for the data mirroring endpoint. To create the AG, run the following Transact-SQL on the SQL Server instance that hosts the primary replica.

Run **only one** of the following scripts:

- [Create availability group with three synchronous replicas](#threeSynch)
- [Create availability group with two synchronous replicas and a configuration replica](#configOnly)
- [Create availability group with two synchronous replicas](#readScale)

### Match the node name to the ServerName property

In the current implementation of the SQL Server resource agent, the node name must match the `ServerName` property from your instance. For example, if your node name is `node1`, make sure `SERVERPROPERTY('ServerName')` returns `node1` in your SQL Server instance. If there's a mismatch, your replicas go into a resolving state after the Pacemaker resource is created.

This rule is important when you use fully qualified domain names. For example, if you use `node1.contoso.onmicrosoft.com` as the node name during cluster setup, make sure `SERVERPROPERTY('ServerName')` returns `node1.contoso.onmicrosoft.com`, and not just `node1`. To fix this problem, you can:

- Rename your host name to the FQDN and use the `sp_dropserver` and `sp_addserver` stored procedures to ensure the metadata in SQL Server matches the change.
- Use the `addr` option in the `pcs cluster auth` command to match the node name to the `SERVERPROPERTY('ServerName')` value and use a static IP as the node address.

<a id="threeSynch"></a>

### Create availability group with three synchronous replicas

Create an AG with three synchronous replicas:

```sql
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

> [!IMPORTANT]  
> After you run the preceding script to create an AG with three synchronous replicas, don't run the following script:

<a id="configOnly"></a>

### Create availability group with two synchronous replicas and a configuration replica

Create an AG with two synchronous replicas and a configuration replica:

> [!IMPORTANT]  
> This architecture allows any edition of SQL Server to host the third replica. For example, the third replica can be hosted on SQL Server Express Edition. On Express Edition, the only valid endpoint type is `WITNESS`.

```sql
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

<a id="readScale"></a>

### Create availability group with two synchronous replicas

Create an AG with two synchronous replicas.

Include two replicas with synchronous availability mode. For example, the following script creates an AG called `ag1`. `node1` and `node2` host replicas in synchronous mode, with automatic seeding and automatic failover.

> [!IMPORTANT]  
> Only run the following script to create an AG with two synchronous replicas. Don't run the following script if you ran either preceding script.

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
   );

ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE;
```

You can also configure an AG with `CLUSTER_TYPE=EXTERNAL` using SQL Server Management Studio or PowerShell.

### Join secondary replicas to the AG

The Pacemaker user needs `ALTER`, `CONTROL`, and `VIEW DEFINITION` permissions on the availability group on all replicas. To grant these permissions, run the following Transact-SQL script after creating the availability group on the primary replica. Run the script on each secondary replica immediately after adding them to the availability group. Before running the script, replace `<pacemakerLogin>` with the name of the Pacemaker user account. If you don't have a login for Pacemaker, [create a sql server login for Pacemaker](cluster-pacemaker.md?tabs=ubuntu#create-a-sql-server-login-for-pacemaker).

```sql
GRANT ALTER, CONTROL, VIEW DEFINITION ON AVAILABILITY GROUP::ag1 TO <pacemakerLogin>
GRANT VIEW SERVER STATE TO <pacemakerLogin>
```

The following Transact-SQL script joins a SQL Server instance to an AG named `ag1`. Update the script for your environment. On each SQL Server instance that hosts a secondary replica, run the following Transact-SQL to join the AG.

```sql
ALTER AVAILABILITY GROUP [ag1] JOIN WITH (CLUSTER_TYPE = EXTERNAL);

ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE;
```

> [!NOTE]  
> For a configuration replica, only the join step is needed.

[!INCLUDE [Create Post](../../includes/cluster-availability-group-create-post.md)]

After you create the AG, you must configure integration with a cluster technology like Pacemaker for high availability. For a read-scale configuration using AGs, starting with [!INCLUDE [SQL Server version](../../../includes/sssql17-md.md)], setting up a cluster isn't required.

If you followed the steps in this article, you have an AG that isn't yet clustered. The next step is to add the cluster. This configuration is valid for read-scale and load balancing scenarios, but it's not complete for high availability. For high availability, you need to add the AG as a cluster resource. See [Related content](#related-content) for instructions.

## Remarks

After you configure the cluster and add the AG as a cluster resource, you can't use Transact-SQL to fail over the AG resources. SQL Server cluster resources on Linux aren't coupled as tightly with the operating system as they are on a Windows Server Failover Cluster (WSFC). The SQL Server service isn't aware of the presence of the cluster. All orchestration is done through the cluster management tools. In RHEL or Ubuntu, use `pcs`. In SLES, use `crm`.

If the AG is a cluster resource, there's a known issue in the current release where forced failover with data loss to an asynchronous replica doesn't work. This issue will be fixed in an upcoming release. Manual or automatic failover to a synchronous replica succeeds.

## Related content

- [Configure a Red Hat Enterprise Linux Pacemaker cluster for SQL Server availability groups](cluster-pacemaker.md?tabs=rhel)
- [Configure a SUSE Linux Enterprise Server Pacemaker cluster for SQL Server availability groups](cluster-pacemaker.md?tabs=sles)
- [Configure an Ubuntu Pacemaker cluster for SQL Server availability groups](cluster-pacemaker.md?tabs=ubuntu)
