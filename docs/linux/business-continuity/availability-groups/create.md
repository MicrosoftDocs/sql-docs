---
title: Create and Configure an Availability Group for SQL Server on Linux
description: This tutorial shows how to create and configure availability groups for SQL Server on Linux, as well as create availability group endpoints and certificates.
author: rwestMSFT
ms.author: randolphwest
ms.date: 08/11/2026
ms.service: sql
ms.subservice: linux
ms.topic: install-set-up-deploy
ms.custom:
  - linux-related-content
  - sfi-image-nochange
---

# Create and configure an availability group for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../../../includes/applies-to-version/sql-linux.md)]

This tutorial shows how to create and configure an availability group (AG) for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] on Linux. Unlike [!INCLUDE [sssql16-md](../../../includes/sssql16-md.md)] and earlier versions on Windows, you can enable an AG with or without creating the underlying Pacemaker cluster first. Integration with the cluster, if needed, happens later.

The tutorial includes the following tasks:

> [!div class="checklist"]
> - Enable availability groups.
> - Create availability group endpoints and certificates.
> - Use [!INCLUDE [ssmanstudiofull-md](../../../includes/ssmanstudiofull-md.md)] (SSMS) or Transact-SQL to create an availability group.
> - Create the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] login and permissions for Pacemaker.
> - Create availability group resources in a Pacemaker cluster (External type only).

## Prerequisites

Deploy the Pacemaker high availability cluster. For more information, see [Deploy a Pacemaker cluster for SQL Server on Linux](../failover-cluster-instance/deploy-pacemaker-cluster.md).

## Enable the availability groups feature

Unlike on Windows, you can't use PowerShell or [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] Configuration Manager to enable the availability groups (AG) feature. On Linux, you can enable the availability groups feature in two ways: use the **`mssql-conf`** utility, or edit the `mssql.conf` file manually.

> [!IMPORTANT]  
> You must enable the AG feature for configuration-only replicas, even on [!INCLUDE [ssexpress-md](../../../includes/ssexpress-md.md)].

### Use the `mssql-conf` utility

At a prompt, run the following command:

```bash
sudo /opt/mssql/bin/mssql-conf set hadr.hadrenabled 1
```

### Edit the mssql.conf file

You can also modify the `mssql.conf` file, located under the `/var/opt/mssql` folder. Add the following lines:

```ini
[hadr]

hadr.hadrenabled = 1
```

### Restart SQL Server

After enabling availability groups, you must restart [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]. Use the following command:

```bash
sudo systemctl restart mssql-server
```

## Create the availability group endpoints and certificates

An availability group uses TCP endpoints for communication. Under Linux, SQL Server supports endpoints for an AG only if you use certificates for authentication. You must restore the certificate from one instance on all other instances that participate as replicas in the same AG. You need the certificate process even for a configuration-only replica.

You can only create endpoints and restore certificates by using Transact-SQL. You can also use non-[!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]-generated certificates. You also need a process to manage and replace any certificates that expire.

> [!IMPORTANT]  
> If you plan to use the [!INCLUDE [ssmanstudiofull-md](../../../includes/ssmanstudiofull-md.md)] wizard to create the AG, you still need to create and restore the certificates by using Transact-SQL on Linux.

For full syntax on the options available for the various commands (including security), see:

- [BACKUP CERTIFICATE](../../../t-sql/statements/backup-certificate-transact-sql.md)
- [CREATE CERTIFICATE](../../../t-sql/statements/create-certificate-transact-sql.md)
- [CREATE ENDPOINT](../../../t-sql/statements/create-endpoint-transact-sql.md)

> [!NOTE]  
> Although you're creating an availability group, the type of endpoint uses `FOR DATABASE_MIRRORING`, because the endpoint type shares underlying aspects with that now-deprecated feature.

This example creates certificates for a three-node configuration. The instance names are `LinAGN1`, `LinAGN2`, and `LinAGN3`.

1. Execute the following script on `LinAGN1` to create the master key, certificate, and endpoint, and back up the certificate. For this example, the endpoint uses the typical TCP port 5022.

   ```sql
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<master-key-password>';
   GO

   CREATE CERTIFICATE LinAGN1_Cert
   WITH SUBJECT = 'LinAGN1 AG Certificate';
   GO

   BACKUP CERTIFICATE LinAGN1_Cert
   TO FILE = '/var/opt/mssql/data/LinAGN1_Cert.cer';
   GO

   CREATE ENDPOINT AGEP
   STATE = STARTED
   AS TCP
   (
       LISTENER_PORT = 5022,
       LISTENER_IP = ALL
   )
   FOR DATABASE_MIRRORING
   (
       AUTHENTICATION = CERTIFICATE LinAGN1_Cert,
       ROLE = ALL
   );
   GO
   ```

1. Do the same on `LinAGN2`:

   ```sql
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<master-key-password>';
   GO

   CREATE CERTIFICATE LinAGN2_Cert
   WITH SUBJECT = 'LinAGN2 AG Certificate';
   GO

   BACKUP CERTIFICATE LinAGN2_Cert
   TO FILE = '/var/opt/mssql/data/LinAGN2_Cert.cer';
   GO

   CREATE ENDPOINT AGEP
   STATE = STARTED
   AS TCP
   (
       LISTENER_PORT = 5022,
       LISTENER_IP = ALL
   )
   FOR DATABASE_MIRRORING
   (
       AUTHENTICATION = CERTIFICATE LinAGN2_Cert,
       ROLE = ALL
   );
   GO
   ```

1. Finally, perform the same sequence on `LinAGN3`:

   ```sql
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<master-key-password>';
   GO

   CREATE CERTIFICATE LinAGN3_Cert
   WITH SUBJECT = 'LinAGN3 AG Certificate';
   GO

   BACKUP CERTIFICATE LinAGN3_Cert
   TO FILE = '/var/opt/mssql/data/LinAGN3_Cert.cer';
   GO

   CREATE ENDPOINT AGEP
   STATE = STARTED
   AS TCP
   (
       LISTENER_PORT = 5022,
       LISTENER_IP = ALL
   )
   FOR DATABASE_MIRRORING
   (
       AUTHENTICATION = CERTIFICATE LinAGN3_Cert,
       ROLE = ALL
   );
   GO
   ```

1. Use `scp` or another utility to copy the backups of the certificate to each node that you want to be part of the AG.

   For this example:

   - Copy `LinAGN1_Cert.cer` to `LinAGN2` and `LinAGN3`.
   - Copy `LinAGN2_Cert.cer` to `LinAGN1` and `LinAGN3`.
   - Copy `LinAGN3_Cert.cer` to `LinAGN1` and `LinAGN2`.

1. Change ownership and the group associated with the copied certificate files to `mssql`.

   ```bash
   sudo chown mssql:mssql <CertFileName>
   ```

1. Create the instance-level logins and users associated with `LinAGN2` and `LinAGN3` on `LinAGN1`.

   ```sql
   CREATE LOGIN LinAGN2_Login
   WITH PASSWORD = '<password>';

   CREATE USER LinAGN2_User
   FOR LOGIN LinAGN2_Login;
   GO

   CREATE LOGIN LinAGN3_Login
   WITH PASSWORD = '<password>';

   CREATE USER LinAGN3_User
   FOR LOGIN LinAGN3_Login;
   GO
   ```

   > [!CAUTION]  
   > [!INCLUDE [password-complexity](../../includes/password-complexity.md)]

1. Restore `LinAGN2_Cert` and `LinAGN3_Cert` on `LinAGN1`. The other replicas' certificates are essential to AG communication and security.

   ```sql
   CREATE CERTIFICATE LinAGN2_Cert
       AUTHORIZATION LinAGN2_User
       FROM FILE = '/var/opt/mssql/data/LinAGN2_Cert.cer';
   GO

   CREATE CERTIFICATE LinAGN3_Cert
       AUTHORIZATION LinAGN3_User
       FROM FILE = '/var/opt/mssql/data/LinAGN3_Cert.cer';
   GO
   ```

1. Grant the logins associated with `LinAGN2` and `LinAGN3` permission to connect to the endpoint on `LinAGN1`.

   ```sql
   GRANT CONNECT ON ENDPOINT::AGEP TO LinAGN2_Login;
   GRANT CONNECT ON ENDPOINT::AGEP TO LinAGN3_Login;
   ```

1. Create the instance-level logins and users associated with `LinAGN1` and `LinAGN3` on `LinAGN2`.

   ```sql
   CREATE LOGIN LinAGN1_Login
   WITH PASSWORD = '<password>';

   CREATE USER LinAGN1_User
   FOR LOGIN LinAGN1_Login;
   GO

   CREATE LOGIN LinAGN3_Login
   WITH PASSWORD = '<password>';

   CREATE USER LinAGN3_User
   FOR LOGIN LinAGN3_Login;
   GO
   ```

1. Restore `LinAGN1_Cert` and `LinAGN3_Cert` on `LinAGN2`.

   ```sql
   CREATE CERTIFICATE LinAGN1_Cert
       AUTHORIZATION LinAGN1_User
       FROM FILE = '/var/opt/mssql/data/LinAGN1_Cert.cer';
   GO

   CREATE CERTIFICATE LinAGN3_Cert
       AUTHORIZATION LinAGN3_User
       FROM FILE = '/var/opt/mssql/data/LinAGN3_Cert.cer';
   GO
   ```

1. Grant the logins associated with `LinAGN1` and `LinAGN3` permission to connect to the endpoint on `LinAGN2`.

   ```sql
   GRANT CONNECT ON ENDPOINT::AGEP TO LinAGN1_Login;
   GRANT CONNECT ON ENDPOINT::AGEP TO LinAGN3_Login;
   GO
   ```

1. Create the instance-level logins and users associated with `LinAGN1` and `LinAGN2` on `LinAGN3`.

   ```sql
   CREATE LOGIN LinAGN1_Login
   WITH PASSWORD = '<password>';

   CREATE USER LinAGN1_User
   FOR LOGIN LinAGN1_Login;
   GO

   CREATE LOGIN LinAGN2_Login
   WITH PASSWORD = '<password>';

   CREATE USER LinAGN2_User
   FOR LOGIN LinAGN2_Login;
   GO
   ```

1. Restore `LinAGN1_Cert` and `LinAGN2_Cert` on `LinAGN3`.

   ```sql
   CREATE CERTIFICATE LinAGN1_Cert
       AUTHORIZATION LinAGN1_User
       FROM FILE = '/var/opt/mssql/data/LinAGN1_Cert.cer';
   GO

   CREATE CERTIFICATE LinAGN2_Cert
       AUTHORIZATION LinAGN2_User
       FROM FILE = '/var/opt/mssql/data/LinAGN2_Cert.cer';
   GO
   ```

1. Grant the logins associated with `LinAGN1` and `LinAGN2` permission to connect to the endpoint on `LinAGN3`.

   ```sql
   GRANT CONNECT ON ENDPOINT::AGEP TO LinAGN1_Login;
   GRANT CONNECT ON ENDPOINT::AGEP TO LinAGN2_Login;
   GO
   ```

## Create the availability group

This section shows how to use [!INCLUDE [ssmanstudiofull-md](../../../includes/ssmanstudiofull-md.md)] (SSMS) or Transact-SQL to create the availability group for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)].

### Use SQL Server Management Studio

This section shows how to create an AG with a cluster type of External by using SSMS with the New Availability Group Wizard.

1. In SSMS, expand **Always On High Availability**, right-click **Availability Groups**, and select **New Availability Group Wizard**.

1. On the **Introduction** dialog, select **Next**.

1. In the **Specify Availability Group Options** dialog, enter a name for the AG, and select a cluster type of `EXTERNAL` or `NONE` in the dropdown list. Use `EXTERNAL` when you deploy Pacemaker. Use `NONE` for specialized scenarios, such as read scale-out. Selecting the option for database level health detection is optional. For more information about this option, see [Availability group database level health detection failover option](../../../database-engine/availability-groups/windows/sql-server-always-on-database-health-detection-failover-option.md). Select **Next**.

   :::image type="content" source="media/create/cluster-type.png" alt-text="Screenshot of Create Availability Group showing cluster type." lightbox="media/create/cluster-type.png":::

1. In the **Select Databases** dialog, select the databases that you want to participate in the AG. Each database must have a full backup before you can add it to an AG. Select **Next**.

1. In the **Specify Replicas** dialog, select **Add Replica**.

1. In the **Connect to Server** dialog, enter the name of the Linux instance of [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] for the secondary replica, and the credentials to connect. Select **Connect**.

1. Repeat the previous two steps for the instance that will contain a configuration-only replica or another secondary replica.

1. All three instances appear on the **Specify Replicas** dialog. If you use a cluster type of External, for the secondary replica that is a true secondary, make sure the availability mode matches that of the primary replica and set the failover mode to External. For the configuration-only replica, select an availability mode of Configuration only.

   The following example shows an AG with two replicas, a cluster type of External, and a configuration-only replica.

   :::image type="content" source="media/create/readable-secondary.png" alt-text="Screenshot of Create Availability Group showing the readable secondary option." lightbox="media/create/readable-secondary.png":::

   The following example shows an AG with two replicas, a cluster type of None, and a configuration-only replica.

   :::image type="content" source="media/create/replicas-page.png" alt-text="Screenshot of Create Availability Group showing the Replicas page." lightbox="media/create/replicas-page.png":::

1. If you want to change the backup preferences, select the **Backup Preferences** tab. For more information about backup preferences with AGs, see [Configure backups on secondary replicas of an Always On availability group](../../../database-engine/availability-groups/windows/configure-backup-on-availability-replicas-sql-server.md).

1. If you use readable secondaries or create an AG with a cluster type of None for read-scale, you can create a listener by selecting the **Listener** tab. You can also add a listener later. To create a listener, select the **Create an availability group listener** option and enter a name, a TCP/IP port, and whether to use a static or automatically assigned DHCP IP address. For an AG with a cluster type of None, use a static IP that matches the primary's IP address.

   :::image type="content" source="media/create/listener.png" alt-text="Screenshot of Create Availability Group showing the listener option." lightbox="media/create/listener.png":::

1. If you create a listener for readable scenarios, SSMS allows the creation of read-only routing in the wizard. You can also add it later by using SSMS or Transact-SQL. To add read-only routing now:

   1. Select the **Read-Only Routing** tab.

   1. Enter the URLs for the read-only replicas. These URLs are similar to the endpoints, except they use the port of the instance, not the endpoint.

      1. Select each URL and from the bottom, select the readable replicas. To select multiple, hold down **Shift** or select-drag.

1. Select **Next**.

1. Choose how to initialize the secondary replicas. The default is to use [automatic seeding](../../../database-engine/availability-groups/windows/automatically-initialize-always-on-availability-group.md), which requires the same path on all servers participating in the AG. You can also have the wizard do a backup, copy, and restore (the second option); have it join if you manually backed up, copied, and restored the database on the replicas (third option); or add the database later (last option). As with certificates, if you're manually making backups and copying them, set permissions on the backup files on the other replicas. Select **Next**.

1. On the **Validation** dialog, if the wizard doesn't return **Success** for all checks, investigate further. Some warnings are acceptable and not fatal, such as if you don't create a listener. Select **Next**.

1. On the **Summary** dialog, select **Finish**. The process to create the AG now begins.

1. When the AG creation is complete, select **Close** on the **Results** page. You can now see the AG on the replicas in the dynamic management views, and under the Always On High Availability folder in SSMS.

### Use Transact-SQL

This section shows examples of creating an AG by using Transact-SQL. You can configure the listener and read-only routing after creating the AG. You can modify the AG itself by using `ALTER AVAILABILITY GROUP`, but you can't change the cluster type in [!INCLUDE [sssql17-md](../../../includes/sssql17-md.md)]. If you didn't mean to create an AG with a cluster type of External, you must delete it and recreate it with a cluster type of None.

For more information and other options, see:

- [CREATE AVAILABILITY GROUP](../../../t-sql/statements/create-availability-group-transact-sql.md)
- [ALTER AVAILABILITY GROUP](../../../t-sql/statements/alter-availability-group-transact-sql.md)
- [Configure read-only routing for an Always On availability group](../../../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md)
- [Configure a listener for an Always On availability group](../../../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md)

#### Example A: Two replicas with a configuration-only replica (External cluster type)

This example shows how to create a two-replica AG that uses a configuration-only replica.

1. Execute the following statement on the primary replica node, which contains the read/write copy of the databases. This example uses automatic seeding.

   ```sql
   CREATE AVAILABILITY GROUP [<AGName>]
   WITH (CLUSTER_TYPE = EXTERNAL)
   FOR DATABASE <DBName>
   REPLICA ON
   N'LinAGN1' WITH (
      ENDPOINT_URL = N' TCP://LinAGN1.FullyQualified.Name:5022',
      FAILOVER_MODE = EXTERNAL,
      AVAILABILITY_MODE = SYNCHRONOUS_COMMIT
   ),
   N'LinAGN2' WITH (
      ENDPOINT_URL = N'TCP://LinAGN2.FullyQualified.Name:5022',
      FAILOVER_MODE = EXTERNAL,
      AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
      SEEDING_MODE = AUTOMATIC
   ),
   N'LinAGN3' WITH (
      ENDPOINT_URL = N'TCP://LinAGN3.FullyQualified.Name:5022',
      AVAILABILITY_MODE = CONFIGURATION_ONLY
   );
   GO
   ```

1. In a query window connected to the other replica, execute the following statement to join the replica to the AG and start seeding from the primary to the secondary replica.

   ```sql
   ALTER AVAILABILITY GROUP [<AGName>]
   JOIN WITH (CLUSTER_TYPE = EXTERNAL);
   GO

   ALTER AVAILABILITY GROUP [<AGName>]
   GRANT CREATE ANY DATABASE;
   GO
   ```

1. In a query window connected to the configuration-only replica, run the following statement to join it to the AG.

   ```sql
   ALTER AVAILABILITY GROUP [<AGName>]
   JOIN WITH (CLUSTER_TYPE = EXTERNAL);
   GO
   ```

#### Example B: Three replicas with read-only routing (External cluster type)

This example shows you how to configure read-only routing as part of the initial AG creation for three full replicas.

1. Execute the following statement on the node that acts as the primary replica, and contains the fully read/write copy of the databases. This example uses automatic seeding.

   ```sql
   CREATE AVAILABILITY GROUP [<AGName>] WITH (CLUSTER_TYPE = EXTERNAL)
   FOR DATABASE < DBName > REPLICA ON
       N'LinAGN1' WITH (
           ENDPOINT_URL = N'TCP://LinAGN1.FullyQualified.Name:5022',
           FAILOVER_MODE = EXTERNAL,
           AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
           PRIMARY_ROLE(ALLOW_CONNECTIONS = READ_WRITE, READ_ONLY_ROUTING_LIST = (
               (
                   'LinAGN2.FullyQualified.Name',
                   'LinAGN3.FullyQualified.Name'
                   )
               )),
           SECONDARY_ROLE(ALLOW_CONNECTIONS = ALL, READ_ONLY_ROUTING_URL = N'TCP://LinAGN1.FullyQualified.Name:1433')
       ),
       N'LinAGN2' WITH (
           ENDPOINT_URL = N'TCP://LinAGN2.FullyQualified.Name:5022',
           FAILOVER_MODE = EXTERNAL,
           SEEDING_MODE = AUTOMATIC,
           AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
           PRIMARY_ROLE(ALLOW_CONNECTIONS = READ_WRITE, READ_ONLY_ROUTING_LIST = (
               (
                   'LinAGN1.FullyQualified.Name',
                   'LinAGN3.FullyQualified.Name'
                   )
               )),
           SECONDARY_ROLE(ALLOW_CONNECTIONS = ALL, READ_ONLY_ROUTING_URL = N'TCP://LinAGN2.FullyQualified.Name:1433')
       ),
       N'LinAGN3' WITH (
           ENDPOINT_URL = N'TCP://LinAGN3.FullyQualified.Name:5022',
           FAILOVER_MODE = EXTERNAL,
           SEEDING_MODE = AUTOMATIC,
           AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
           PRIMARY_ROLE(ALLOW_CONNECTIONS = READ_WRITE, READ_ONLY_ROUTING_LIST = (
               (
                   'LinAGN1.FullyQualified.Name',
                   'LinAGN2.FullyQualified.Name'
                   )
               )),
           SECONDARY_ROLE(ALLOW_CONNECTIONS = ALL, READ_ONLY_ROUTING_URL = N'TCP://LinAGN3.FullyQualified.Name:1433')
       )
       LISTENER '<ListenerName>' (
           WITH IP = ('<IPAddress>', '<SubnetMask>'), Port = 1433
       );
   GO
   ```

   A few things to note about this configuration:

   - `AGName` is the name of the AG.
   - `DBName` is the name of the database that you use with the AG. It can also be a comma-separated list of names.
   - `ListenerName` is a name that differs from any of the underlying servers or nodes. You register it in DNS along with `IPAddress`.
   - `IPAddress` is the IP address for `ListenerName`. It's also unique and doesn't match any of the servers or nodes. Applications and end users use either `ListenerName` or `IPAddress` to connect to the AG.
     - `SubnetMask` is the subnet mask of `IPAddress`. In [!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)] and previous versions, this value is `255.255.255.255`. In [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] and later versions, this value is `0.0.0.0`.

1. In a query window connected to the other replica, execute the following statement to join the replica to the AG and initiate the seeding process from the primary to the secondary replica.

   ```sql
   ALTER AVAILABILITY GROUP [<AGName>]
   JOIN WITH (CLUSTER_TYPE = EXTERNAL);
   GO

   ALTER AVAILABILITY GROUP [<AGName>]
   GRANT CREATE ANY DATABASE;
   GO
   ```

1. Repeat Step 2 for the third replica.

#### Example C: Two replicas with read-only routing (None cluster type)

This example creates a two-replica configuration that uses a cluster type of None. Use this configuration for the read-scale scenario where you don't expect failover. This step creates the listener that is the primary replica and configures read-only routing with round-robin functionality.

1. Execute the following statement on the node that acts as the primary replica, and contains the fully read/write copy of the databases. This example uses automatic seeding.

   ```sql
   CREATE AVAILABILITY GROUP [<AGName>]
   WITH (CLUSTER_TYPE = NONE)
   FOR DATABASE <DBName> REPLICA ON
       N'LinAGN1' WITH (
           ENDPOINT_URL = N'TCP://LinAGN1.FullyQualified.Name: <PortOfEndpoint>',
           FAILOVER_MODE = MANUAL,
           AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,
           PRIMARY_ROLE(
               ALLOW_CONNECTIONS = READ_WRITE,
               READ_ONLY_ROUTING_LIST = (('LinAGN1.FullyQualified.Name'.'LinAGN2.FullyQualified.Name'))
           ),
           SECONDARY_ROLE(
               ALLOW_CONNECTIONS = ALL,
               READ_ONLY_ROUTING_URL = N'TCP://LinAGN1.FullyQualified.Name:<PortOfInstance>'
           )
       ),
       N'LinAGN2' WITH (
           ENDPOINT_URL = N'TCP://LinAGN2.FullyQualified.Name:<PortOfEndpoint>',
           FAILOVER_MODE = MANUAL,
           SEEDING_MODE = AUTOMATIC,
           AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,
           PRIMARY_ROLE(ALLOW_CONNECTIONS = READ_WRITE, READ_ONLY_ROUTING_LIST = (
                    ('LinAGN1.FullyQualified.Name',
                       'LinAGN2.FullyQualified.Name')
                    )),
           SECONDARY_ROLE(ALLOW_CONNECTIONS = ALL, READ_ONLY_ROUTING_URL = N'TCP://LinAGN2.FullyQualified.Name:<PortOfInstance>')
       ),
       LISTENER '<ListenerName>' (WITH IP = (
                '<PrimaryReplicaIPAddress>',
                '<SubnetMask>'),
               Port = <PortOfListener>
       );
   GO
   ```

   In this example:

   - `AGName` is the name of the AG.
   - `DBName` is the name of the database that you use with the AG. It can also be a comma-separated list of names.
   - `PortOfEndpoint` is the port number for the endpoint you create.
     - `PortOfInstance` is the port number for the instance of [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)].
   - `ListenerName` is a placeholder name that's different from any of the underlying replicas.
   - `PrimaryReplicaIPAddress` is the IP address of the primary replica.
     - `SubnetMask` is the subnet mask of `IPAddress`. In [!INCLUDE [sssql19-md](../../../includes/sssql19-md.md)] and previous versions, this value is `255.255.255.255`. In [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] and later versions, this value is `0.0.0.0`.

1. Join the secondary replica to the AG and initiate automatic seeding.

   ```sql
   ALTER AVAILABILITY GROUP [<AGName>]
   JOIN WITH (CLUSTER_TYPE = NONE);
   GO

   ALTER AVAILABILITY GROUP [<AGName>]
   GRANT CREATE ANY DATABASE;
   GO
   ```

## Create the SQL Server login and permissions for Pacemaker

A Pacemaker high availability cluster that uses [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] on Linux needs access to the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instance, and permissions on the AG itself. These steps create the login and the associated permissions, along with a file that tells Pacemaker how to authenticate to [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)].

1. In a query window connected to the first replica, execute the following script:

   ```sql
   CREATE LOGIN PMLogin
       WITH PASSWORD = '<password>';
   GO

   GRANT VIEW SERVER STATE TO PMLogin;
   GO

   GRANT ALTER, CONTROL, VIEW DEFINITION
   ON AVAILABILITY GROUP::<AGThatWasCreated> TO PMLogin;
   GO
   ```

1. On Node 1, add the following two lines to the `/var/opt/mssql/secrets/passwd` file:

   ```output
   PMLogin

   <password>
   ```

   You might have to elevate your permissions with `sudo` to edit this file.

1. Lock down the file:

   ```bash
   sudo chmod 400 /var/opt/mssql/secrets/passwd
   ```

1. Repeat Steps 1-5 on the other servers that serve as replicas.

## Create the availability group resources in the Pacemaker cluster (External only)

After you create an AG in [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)], you must create the corresponding resources in Pacemaker when you specify a cluster type of External. An AG needs two resources: the availability group resource, and an IP address resource. Configuring the IP address resource is optional if you aren't using a listener. However, it's recommended when you need listener features.

The AG resource you create is a type of resource called a *clone*. The AG resource has copies on each node, and one controlling resource called the *promoted* resource. The *promoted* resource corresponds to the server that hosts the primary replica. The other resources host secondary replicas (regular or configuration-only), and they can be promoted in a failover.

> [!NOTE]  
> In [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)] with Cumulative Update (CU) 3 and later versions, Pacemaker HA agent v2 (Preview) is available for Red Hat Enterprise Linux (RHEL) and Ubuntu through the `mssql-server-ha` package. You can evaluate Pacemaker HA agent v2 in nonproduction deployments. The existing Pacemaker HA agent (v1) is still fully supported for production deployments. For more information, see [Pacemaker HA agent v2 (Preview)](#pacemaker-ha-agent-v2-preview).

### [Red Hat Enterprise Linux (RHEL) and Ubuntu](#tab/ru)

#### Pacemaker HA agent v1

1. Create the AG resource in Pacemaker by using the Pacemaker HA agent (v1): (`ocf:mssql:ag`)

   ```bash
   sudo pcs resource create <NameForAGResource> ocf:mssql:ag ag_name=<AGName> meta failure-timeout=30s promotable notify=true
   ```

   In this example, `NameForAGResource` is the unique name you give to this cluster resource for the AG, and `AGName` is the name of the AG that you created.

1. Create the IP address resource for the AG that you associate with the listener functionality.

   ```bash
   sudo pcs resource create <NameForIPResource> ocf:heartbeat:IPaddr2 ip=<IPAddress> cidr_netmask=<Netmask>
   ```

   In this example, `NameForIPResource` is the unique name for the IP resource, and `IPAddress` is the static IP address you assign to the resource.

1. To ensure that the IP address and the AG resource run on the same node, configure a colocation constraint.

   ```bash
   sudo pcs constraint colocation add <NameForIPResource> with promoted <NameForAGResource>-clone INFINITY
   ```

   In this example, `NameForIPResource` is the name for the IP resource, and `NameForAGResource` is the name for the AG resource.

1. Create an ordering constraint to ensure that the AG resource is running before the IP address. While the colocation constraint implies an ordering constraint, this step enforces it.

   ```bash
   sudo pcs constraint order promote <NameForAGResource>-clone then start <NameForIPResource>
   ```

   In this example, `NameForIPResource` is the name for the IP resource, and `NameForAGResource` is the name for the AG resource.

#### Pacemaker HA agent v2 (Preview)

Pacemaker HA agent v2 uses a service-based architecture. The agent runs as a dedicated system service named `mssql-pcsag`, which is responsible for handling SQL Server-specific high availability operations and communication with Pacemaker.

You manage the `mssql-pcsag` service through standard system service controls. Start, stop, restart, and check the status of this service as needed with the following commands:

```bash
sudo systemctl start mssql-pcsag  # Start the Pacemaker HA agent v2 (mssql-pcsag) service
sudo systemctl stop mssql-pcsag  # Stop the Pacemaker HA agent v2 (mssql-pcsag) service
sudo systemctl restart mssql-pcsag  # Restart the Pacemaker HA agent v2 (mssql-pcsag) service
sudo systemctl status mssql-pcsag  # Check the status of the Pacemaker HA agent v2 (mssql-pcsag) service
```

Pacemaker interacts with SQL Server availability groups through the `mssql-pcsag` service. For availability group monitoring and failover to function correctly:

- The Pacemaker cluster must be running.
- The `mssql-pcsag` service must be running.

Although Pacemaker and `mssql-pcsag` are separate components, they operate together at runtime. If either Pacemaker or the `mssql-pcsag` service stops, availability group failover operations don't function as expected.

> [!NOTE]  
> Restarting the `mssql-pcsag` service doesn't restart SQL Server. Similarly, restarting SQL Server doesn't automatically restart the Pacemaker HA agent. Verify that both services are running during troubleshooting.

Pacemaker HA agent v2 introduces reliability and performance improvements over the previous agent, including:

- Improved failover performance to reduce both planned and unplanned failover times.

- Support for flexible automatic failover policies, including configuration of [failure-condition level](../../../database-engine/availability-groups/windows/configure-flexible-automatic-failover-policy.md#failure-condition-level) and [health-check timeout](../../../database-engine/availability-groups/windows/configure-flexible-automatic-failover-policy.md#HCtimeout).

  Example: The following Transact-SQL statement changes the failure-condition level of an existing availability group named AG1 to level 2:

  ```sql
  ALTER AVAILABILITY GROUP AG1 SET (FAILURE_CONDITION_LEVEL = 2);
  ```

  Example: The following Transact-SQL statement changes the health-check timeout threshold of an existing availability group named AG1 to 60,000 milliseconds (60 seconds).

  ```sql
  ALTER AVAILABILITY GROUP AG1 SET (HEALTH_CHECK_TIMEOUT = 60000);
  ```

  Example: After applying the configuration, use the following Transact-SQL statement to verify the configured failure-condition level and health-check timeout for availability groups.

  ```sql
  SELECT failure_condition_level,
         health_check_timeout
  FROM sys.availability_groups;
  ```

- Support for TLS 1.3 for communication between the Pacemaker cluster and SQL Server.

1. Create the AG resource in Pacemaker by using Pacemaker HA agent v2: (`ocf:mssql:agv2`)

   ```bash
   sudo pcs resource create <NameForAGResource> ocf:mssql:agv2 ag_name=<AGName> meta failure-timeout=30s promotable notify=true
   ```

   If upgrading from Pacemaker HA agent v1 to v2, remove the existing AG resource before creating the `agv2` resource:

   ```bash
   sudo pcs resource delete <NameForAGResource>
   ```

   This operation temporarily stops AG synchronization while the resource is being recreated. Deleting and recreating the Pacemaker AG resource doesn't delete the AG. After the resource is recreated, Pacemaker resumes management and AG synchronization automatically.

1. Create the IP address resource for the AG that you associate with the listener functionality.

   ```bash
   sudo pcs resource create <NameForIPResource> ocf:heartbeat:IPaddr2 ip=<IPAddress> cidr_netmask=<Netmask>
   ```

   In this example, `NameForIPResource` is the unique name for the IP resource, and `IPAddress` is the static IP address you assign to the resource.

1. To ensure that the IP address and the AG resource run on the same node, configure a colocation constraint.

   ```bash
   sudo pcs constraint colocation add <NameForIPResource> with promoted <NameForAGResource>-clone INFINITY
   ```

   In this example, `NameForIPResource` is the name for the IP resource, and `NameForAGResource` is the name for the AG resource.

1. Create an ordering constraint to ensure that the AG resource is up and running before the IP address. While the colocation constraint implies an ordering constraint, this step enforces it.

   ```bash
   sudo pcs constraint order promote <NameForAGResource>-clone then start <NameForIPResource>
   ```

   In this example, `NameForIPResource` is the name for the IP resource, and `NameForAGResource` is the name for the AG resource.

### [SUSE Linux Enterprise Server (SLES)](#tab/sles)

> [!NOTE]  
> Starting in [!INCLUDE [sssql25-md](../../../includes/sssql25-md.md)], SUSE Linux Enterprise Server (SLES) isn't supported.

1. Create the AG resource with the following syntax:

   ```bash
   primitive <NameForAGResource> \
   ocf:mssql:ag \
   params ag_name="<AGName>" \
   meta failure-timeout=60s \
   op start timeout=60s \
   op stop timeout=60s \
   op promote timeout=60s \
   op demote timeout=10s \
   op monitor timeout=60s interval=10s \
   op monitor timeout=60s interval=11s role="Promoted" \
   op notify timeout=60s
   clone ms-ag_cluster <NameForAGResource> \
   meta promoted-max="1" promoted-node-max="1" clone-max="3" \
       promotable="true" clone-node-max="1" notify="true" \
   commit
   ```

   In this example, `NameForAGResource` is the unique name you give to this cluster resource for the AG, and `AGName` is the name of the AG you create.

1. Create the IP address resource for the AG that you associate with the listener functionality.

   ```bash
   crm configure \
   primitive <NameForIPResource> \
      ocf:heartbeat:IPaddr2 \
      params ip=<IPAddress> \
         cidr_netmask=<Netmask>
   ```

   In this example, `NameForIPResource` is the unique name for the IP resource, and `IPAddress` is the static IP address you assign to the resource. On SLES, you must also provide the netmask. For example, 255.255.255.0 has a value of 24 for `Netmask`.

1. To ensure that the IP address and the AG resource run on the same node, configure a colocation constraint.

   ```bash
   crm configure <NameForConstraint> inf: \
   <NameForIPResource> <NameForAGResource>:Promoted
   commit
   ```

   In this example, `NameForIPResource` is the name for the IP resource, `NameForAGResource` is the name for the AG resource, and `NameForConstraint` is the name for the constraint.

1. Create an ordering constraint to ensure that the AG resource is running before the IP address. While the colocation constraint implies an ordering constraint, this step enforces it.

   ```bash
   crm configure \
   order <NameForConstraint> Mandatory: <NameForAGResource>:promote <NameForIPResource>:start
   commit
   ```

   In this example, `NameForIPResource` is the name for the IP resource, `NameForAGResource` is the name for the AG resource, and `NameForConstraint` is the name for the constraint.

---

## Related content

- [Always On availability group failover on Linux](failover-high-availability.md)
