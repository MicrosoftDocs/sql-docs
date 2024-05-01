---
title: Create and configure an availability group for SQL Server on Linux
description: This tutorial shows how to create and configure availability groups for SQL Server on Linux, as well as create availability group endpoints and certificates.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: vanto
ms.date: 12/28/2023
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - linux-related-content
---

# Create and configure an availability group for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This tutorial covers how to create and configure an availability group (AG) for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux. Unlike [!INCLUDE [sssql16-md](../includes/sssql16-md.md)] and earlier on Windows, you can enable an AG with or without creating the underlying Pacemaker cluster first. Integration with the cluster, if needed, isn't done until later.

The tutorial includes the following tasks:

> [!div class="checklist"]
> - Enable availability groups.
> - Create availability group endpoints and certificates.
> - Use [!INCLUDE [ssmanstudiofull-md](../includes/ssmanstudiofull-md.md)] (SSMS) or Transact-SQL to create an availability group.
> - Create the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] login and permissions for Pacemaker.
> - Create availability group resources in a Pacemaker cluster (External type only).

## Prerequisites

Deploy the Pacemaker high availability cluster as described in [Deploy a Pacemaker cluster for SQL Server on Linux](sql-server-linux-deploy-pacemaker-cluster.md).

## Enable the availability groups feature

Unlike on Windows, you can't use PowerShell or [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] Configuration Manager to enable the availability groups (AG) feature. Under Linux, you must use `mssql-conf` to enable the feature. There are two ways to enable the availability groups feature: use the `mssql-conf` utility, or edit the `mssql.conf` file manually.

> [!IMPORTANT]  
> The AG feature must be enabled for configuration-only replicas, even on [!INCLUDE [ssexpress-md](../includes/ssexpress-md.md)].

### Use the mssql-conf utility

At a prompt, issue the following command:

```bash
sudo /opt/mssql/bin/mssql-conf set hadr.hadrenabled 1
```

### Edit the mssql.conf file

You can also modify the `mssql.conf` file, located under the `/var/opt/mssql` folder, to add the following lines:

```ini
[hadr]

hadr.hadrenabled = 1
```

### Restart [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]

After enabling availability groups, as on Windows, you must restart [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], using the following command:

```bash
sudo systemctl restart mssql-server
```

## Create the availability group endpoints and certificates

An availability group uses TCP endpoints for communication. Under Linux, endpoints for an AG are only supported if certificates are used for authentication. You must restore the certificate from one instance on all other instances that will participate as replicas in the same AG. The certificate process is required even for a configuration-only replica.

Creating endpoints and restoring certificates can only be done via Transact-SQL. You can use non-[!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]-generated certificates as well. You also need a process to manage and replace any certificates that expire.

> [!IMPORTANT]  
> If you plan to use the [!INCLUDE [ssmanstudiofull-md](../includes/ssmanstudiofull-md.md)] wizard to create the AG, you still need to create and restore the certificates by using Transact-SQL on Linux.

For full syntax on the options available for the various commands (including security), consult:

- [BACKUP CERTIFICATE](../t-sql/statements/backup-certificate-transact-sql.md)
- [CREATE CERTIFICATE](../t-sql/statements/create-certificate-transact-sql.md)
- [CREATE ENDPOINT](../t-sql/statements/create-endpoint-transact-sql.md)

> [!NOTE]  
> Although you're creating an availability group, the type of endpoint uses `FOR DATABASE_MIRRORING`, because some underlying aspects were once shared with that now-deprecated feature.

This example creates certificates for a three-node configuration. The instance names are `LinAGN1`, `LinAGN2`, and `LinAGN3`.

1. Execute the following script on `LinAGN1` to create the master key, certificate, and endpoint, and back up the certificate. For this example, the typical TCP port of 5022 is used for the endpoint.

   ```sql
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<StrongPassword>';
   GO

   CREATE CERTIFICATE LinAGN1_Cert
       WITH SUBJECT = 'LinAGN1 AG Certificate';
   GO

   BACKUP CERTIFICATE LinAGN1_Cert TO FILE = '/var/opt/mssql/data/LinAGN1_Cert.cer';
   GO

   CREATE ENDPOINT AGEP STATE = STARTED AS TCP (
       LISTENER_PORT = 5022,
       LISTENER_IP = ALL
   )
   FOR DATABASE_MIRRORING(AUTHENTICATION = CERTIFICATE LinAGN1_Cert, ROLE = ALL);
   GO
   ```

1. Do the same on `LinAGN2`:

   ```sql
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<StrongPassword>';
   GO

   CREATE CERTIFICATE LinAGN2_Cert
   WITH SUBJECT = 'LinAGN2 AG Certificate';
   GO

   BACKUP CERTIFICATE LinAGN2_Cert
   TO FILE = '/var/opt/mssql/data/LinAGN2_Cert.cer';
   GO

   CREATE ENDPOINT AGEP
   STATE = STARTED
   AS TCP (
       LISTENER_PORT = 5022,
       LISTENER_IP = ALL)
   FOR DATABASE_MIRRORING (
       AUTHENTICATION = CERTIFICATE LinAGN2_Cert,
       ROLE = ALL);
   GO
   ```

1. Finally, perform the same sequence on `LinAGN3`:

   ```sql
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<StrongPassword>';
   GO

   CREATE CERTIFICATE LinAGN3_Cert
       WITH SUBJECT = 'LinAGN3 AG Certificate';
   GO

   BACKUP CERTIFICATE LinAGN3_Cert TO FILE = '/var/opt/mssql/data/LinAGN3_Cert.cer';
   GO

   CREATE ENDPOINT AGEP STATE = STARTED AS TCP (
       LISTENER_PORT = 5022,
       LISTENER_IP = ALL
   )
   FOR DATABASE_MIRRORING(AUTHENTICATION = CERTIFICATE LinAGN3_Cert, ROLE = ALL);
   GO
   ```

1. Using `scp` or another utility, copy the backups of the certificate to each node that will be part of the AG.

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
   CREATE LOGIN LinAGN2_Login WITH PASSWORD = '<StrongPassword>';
   CREATE USER LinAGN2_User FOR LOGIN LinAGN2_Login;
   GO

   CREATE LOGIN LinAGN3_Login WITH PASSWORD = '<StrongPassword>';
   CREATE USER LinAGN3_User FOR LOGIN LinAGN3_Login;
   GO
   ```

1. Restore `LinAGN2_Cert` and `LinAGN3_Cert` on `LinAGN1`. Having the other replicas' certificates is an important aspect of AG communication and security.

   ```sql
   CREATE CERTIFICATE LinAGN2_Cert AUTHORIZATION LinAGN2_User
   FROM FILE = '/var/opt/mssql/data/LinAGN2_Cert.cer';
   GO

   CREATE CERTIFICATE LinAGN3_Cert AUTHORIZATION LinAGN3_User
   FROM FILE = '/var/opt/mssql/data/LinAGN3_Cert.cer';
   GO
   ```

1. Grant the logins associated with `LinAG2` and `LinAGN3` permission to connect to the endpoint on `LinAGN1`.

   ```sql
   GRANT CONNECT ON ENDPOINT::AGEP TO LinAGN2_Login;
   GO

   GRANT CONNECT ON ENDPOINT::AGEP TO LinAGN3_Login;
   GO
   ```

1. Create the instance-level logins and users associated with `LinAGN1` and `LinAGN3` on `LinAGN2`.

   ```sql
   CREATE LOGIN LinAGN1_Login WITH PASSWORD = '<StrongPassword>';
   CREATE USER LinAGN1_User FOR LOGIN LinAGN1_Login;
   GO

   CREATE LOGIN LinAGN3_Login WITH PASSWORD = '<StrongPassword>';
   CREATE USER LinAGN3_User FOR LOGIN LinAGN3_Login;
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

1. Grant the logins associated with `LinAG1` and `LinAGN3` permission to connect to the endpoint on `LinAGN2`.

   ```sql
   GRANT CONNECT ON ENDPOINT::AGEP TO LinAGN1_Login;
   GO

   GRANT CONNECT ON ENDPOINT::AGEP TO LinAGN3_Login;
   GO
   ```

1. Create the instance-level logins and users associated with `LinAGN1` and `LinAGN2` on `LinAGN3`.

   ```sql
   CREATE LOGIN LinAGN1_Login WITH PASSWORD = '<StrongPassword>';
   CREATE USER LinAGN1_User FOR LOGIN LinAGN1_Login;
   GO

   CREATE LOGIN LinAGN2_Login WITH PASSWORD = '<StrongPassword>';
   CREATE USER LinAGN2_User FOR LOGIN LinAGN2_Login;
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

1. Grant the logins associated with `LinAG1` and `LinAGN2` permission to connect to the endpoint on `LinAGN3`.

   ```sql
   GRANT CONNECT ON ENDPOINT::AGEP TO LinAGN1_Login;
   GO

   GRANT CONNECT ON ENDPOINT::AGEP TO LinAGN2_Login;
   GO
   ```

## Create the availability group

This section covers how to use [!INCLUDE [ssmanstudiofull-md](../includes/ssmanstudiofull-md.md)] (SSMS) or Transact-SQL to create the availability group for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

### Use [!INCLUDE [ssmanstudiofull-md](../includes/ssmanstudiofull-md.md)]

This section shows how to create an AG with a cluster type of External using SSMS with the New Availability Group Wizard.

1. In SSMS, expand **Always On High Availability**, right-click **Availability Groups**, and select **New Availability Group Wizard**.

1. On the Introduction dialog, select **Next**.

1. In the Specify Availability Group Options dialog, enter a name for the availability group, and select a cluster type of `EXTERNAL` or `NONE` in the dropdown list. External should be used when Pacemaker will be deployed. None is for specialized scenarios, such as read scale-out. Selecting the option for database level health detection is optional. For more information on this option, see [Availability group database level health detection failover option](../database-engine/availability-groups/windows/sql-server-always-on-database-health-detection-failover-option.md). Select **Next**.

   :::image type="content" source="media/sql-server-linux-create-availability-group/image3.png" alt-text="Screenshot of Create Availability Group showing cluster type." lightbox="media/sql-server-linux-create-availability-group/image3.png":::

1. In the Select Databases dialog, select the databases that will participate in the AG. Each database must have a full backup before it can be added to an AG. Select **Next**.

1. In the Specify Replicas dialog, select **Add Replica**.

1. In the Connect to Server dialog, enter the name of the Linux instance of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] that will be the secondary replica, and the credentials to connect. Select **Connect**.

1. Repeat the previous two steps for the instance that will contain a configuration-only replica or another secondary replica.

1. All three instances should now be listed on the Specify Replicas dialog. If using a cluster type of External, for the secondary replica that will be a true secondary, make sure the Availability Mode matches that of the primary replica and failover mode is set to External. For the configuration-only replica, select an availability mode of Configuration only.

   The following example shows an AG with two replicas, a cluster type of External, and a configuration-only replica.

   :::image type="content" source="media/sql-server-linux-create-availability-group/image4.png" alt-text="Screenshot of Create Availability Group showing the readable secondary option." lightbox="media/sql-server-linux-create-availability-group/image4.png":::

   The following example shows an AG with two replicas, a cluster type of None, and a configuration-only replica.

   :::image type="content" source="media/sql-server-linux-create-availability-group/image5.png" alt-text="Screenshot of Create Availability Group showing the Replicas page." lightbox="media/sql-server-linux-create-availability-group/image5.png":::

1. If you want to alter the backup preferences, select the Backup Preferences tab. For more information on backup preferences with AGs, see [Configure backups on secondary replicas of an Always On availability group](../database-engine/availability-groups/windows/configure-backup-on-availability-replicas-sql-server.md).

1. If using readable secondaries or creating an AG with a cluster type of None for read-scale, you can create a listener by selecting the Listener tab. A listener can also be added later. To create a listener, choose the option **Create an availability group listener** and enter a name, a TCP/IP port, and whether to use a static or automatically assigned DHCP IP address. Remember that for an AG with a cluster type of None, the IP should be static and set to the primary's IP address.

   :::image type="content" source="media/sql-server-linux-create-availability-group/image6.png" alt-text="Screenshot of Create Availability Group showing the listener option." lightbox="media/sql-server-linux-create-availability-group/image6.png":::

1. If a listener is created for readable scenarios, SSMS 17.3 or later allows the creation of the read-only routing in the wizard. It can also be added later via SSMS or Transact-SQL. To add read-only routing now:

   1. Select the Read-Only Routing tab.

   1. Enter the URLs for the read-only replicas. These URLs are similar to the endpoints, except they use the port of the instance, not the endpoint.

   1. Select each URL and from the bottom, select the readable replicas. To multi-select, hold down SHIFT or select-drag.

1. Select **Next**.

1. Choose how the secondary replicas will be initialized. The default is to use [automatic seeding](../database-engine/availability-groups/windows/automatically-initialize-always-on-availability-group.md), which requires the same path on all servers participating in the AG. You can also have the wizard do a backup, copy, and restore (the second option); have it join if you have manually backed up, copied, and restored the database on the replicas (third option); or add the database later (last option). As with certificates, if you're manually making backups and copying them, permissions on the backup files needs to be set on the other replicas. Select **Next**.

1. On the Validation dialog, if everything doesn't come back as Success, investigate. Some warnings are acceptable and not fatal, such as if you don't create a listener. Select **Next**.

1. On the Summary dialog, select **Finish**. The process to create the AG now begins.

1. When the AG creation is complete, select **Close** on the Results. You can now see the AG on the replicas in the dynamic management views, and under the Always On High Availability folder in SSMS.

### Use Transact-SQL

This section shows examples of creating an AG using Transact-SQL. The listener and read-only routing can be configured after the AG is created. The AG itself can be modified with `ALTER AVAILABILITY GROUP`, but changing the cluster type can't be done in [!INCLUDE [sssql17-md](../includes/sssql17-md.md)]. If you didn't mean to create an AG with a cluster type of External, you must delete it and recreate it with a cluster type of None. More information and other options can be found at the following links:

- [CREATE AVAILABILITY GROUP (Transact-SQL)](../t-sql/statements/create-availability-group-transact-sql.md)
- [ALTER AVAILABILITY GROUP (Transact-SQL)](../t-sql/statements/alter-availability-group-transact-sql.md)
- [Configure read-only routing for an Always On availability group](../database-engine/availability-groups/windows/configure-read-only-routing-for-an-availability-group-sql-server.md)
- [Configure a listener for an Always On availability group](../database-engine/availability-groups/windows/create-or-configure-an-availability-group-listener-sql-server.md)

#### Example A: Two replicas with a configuration-only replica (External cluster type)

This example shows how to create a two-replica AG that uses a configuration-only replica.

1. Execute on the node that will be the primary replica containing the fully read/write copy of the databases. This example uses automatic seeding.

   ```sql
   CREATE AVAILABILITY GROUP [<AGName>]
   WITH (CLUSTER_TYPE = EXTERNAL)
   FOR DATABASE <DBName>
   REPLICA ON N'LinAGN1' WITH (
      ENDPOINT_URL = N' TCP://LinAGN1.FullyQualified.Name:5022',
      FAILOVER_MODE = EXTERNAL,
      AVAILABILITY_MODE = SYNCHRONOUS_COMMIT),
   N'LinAGN2' WITH (
      ENDPOINT_URL = N'TCP://LinAGN2.FullyQualified.Name:5022',
      FAILOVER_MODE = EXTERNAL,
      AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
      SEEDING_MODE = AUTOMATIC),
   N'LinAGN3' WITH (
      ENDPOINT_URL = N'TCP://LinAGN3.FullyQualified.Name:5022',
      AVAILABILITY_MODE = CONFIGURATION_ONLY);
   GO
   ```

1. In a query window connected to the other replica, execute the following to join the replica to the AG and initiate the seeding process from the primary to the secondary replica.

   ```sql
   ALTER AVAILABILITY GROUP [<AGName>] JOIN WITH (CLUSTER_TYPE = EXTERNAL);
   GO

   ALTER AVAILABILITY GROUP [<AGName>] GRANT CREATE ANY DATABASE;
   GO
   ```

1. In a query window connected to the configuration only replica, join it to the AG.

   ```sql
   ALTER AVAILABILITY GROUP [<AGName>] JOIN WITH (CLUSTER_TYPE = EXTERNAL);
   GO
   ```

#### Example B: Three replicas with read-only routing (External cluster type)

This example shows three full replicas and how read-only routing can be configured as part of the initial AG creation.

1. Execute on the node that will be the primary replica containing the fully read/write copy of the databases. This example uses automatic seeding.

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

   - `AGName` is the name of the availability group.
   - `DBName` is the name of the database that is used with the availability group. It can also be a list of names separated by commas.
   - `ListenerName` is a name that is different than any of the underlying servers/nodes. It will be registered in DNS along with `IPAddress`.
   - `IPAddress` is an IP address that is associated with `ListenerName`. It's also unique and not the same as any of the servers/nodes. Applications and end users use either `ListenerName` or `IPAddress` to connect to the AG.
   - `SubnetMask` is the subnet mask of `IPAddress`. In [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] and previous versions, this is `255.255.255.255`. In [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] and later versions, this is `0.0.0.0`.

1. In a query window connected to the other replica, execute the following to join the replica to the AG and initiate the seeding process from the primary to the secondary replica.

   ```sql
   ALTER AVAILABILITY GROUP [<AGName>] JOIN WITH (CLUSTER_TYPE = EXTERNAL);
   GO

   ALTER AVAILABILITY GROUP [<AGName>] GRANT CREATE ANY DATABASE;
   GO
   ```

1. Repeat Step 2 for the third replica.

#### Example C: Two replicas with read-only routing (None cluster type)

This example shows the creation of a two-replica configuration using a cluster type of None. It's used for the read scale scenario where no failover is expected. This creates the listener that is actually the primary replica, and the read-only routing, using the round robin functionality.

1. Execute on the node that will be the primary replica containing the fully read/write copy of the databases. This example uses automatic seeding.

```sql
CREATE AVAILABILITY
GROUP [<AGName>]
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

   Where:

   - `AGName` is the name of the availability group.
   - `DBName` is the name of the database that will be used with the availability group. It can also be a list of names separated by commas.
   - `PortOfEndpoint` is the port number used by the endpoint created.
   - `PortOfInstance` is the port number used by the instance of [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].
   - `ListenerName` is a name that is different than any of the underlying replicas but isn't actually used.
   - `PrimaryReplicaIPAddress` is the IP address of the primary replica.
   - `SubnetMask` is the subnet mask of `IPAddress`. In [!INCLUDE [sssql19-md](../includes/sssql19-md.md)] and previous versions, this is `255.255.255.255`. In [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] and later versions, this is `0.0.0.0`.

1. Join the secondary replica to the AG and initiate automatic seeding.

   ```sql
   ALTER AVAILABILITY GROUP [<AGName>] JOIN WITH (CLUSTER_TYPE = NONE);
   GO

   ALTER AVAILABILITY GROUP [<AGName>] GRANT CREATE ANY DATABASE;
   GO
   ```

## Create the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] login and permissions for Pacemaker

A Pacemaker high availability cluster underlying [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux needs access to the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] instance, and permissions on the availability group itself. These steps create the login and the associated permissions, along with a file that tells Pacemaker how to log into [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

1. In a query window connected to the first replica, execute the following script:

   ```sql
   CREATE LOGIN PMLogin WITH PASSWORD ='<StrongPassword>';
   GO

   GRANT VIEW SERVER STATE TO PMLogin;
   GO

   GRANT ALTER, CONTROL, VIEW DEFINITION ON AVAILABILITY GROUP::<AGThatWasCreated> TO PMLogin;
   GO
   ```

1. On Node 1, enter the command

   ```bash
   sudo emacs /var/opt/mssql/secrets/passwd
   ```

   This opens the Emacs editor.

1. Enter the following two lines into the editor:

   ```output
   PMLogin

   <StrongPassword>
   ```

1. Hold down the `Ctrl` key, then press `X`, then `C`, to exit and save the file.

1. Execute

   ```bash
   sudo chmod 400 /var/opt/mssql/secrets/passwd
   ```

   to lock down the file.

1. Repeat Steps 1-5 on the other servers that will serve as replicas.

## Create the availability group resources in the Pacemaker cluster (External only)

After an availability group is created in [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], the corresponding resources must be created in Pacemaker, when a cluster type of External is specified. There are two resources associated with an AG: the AG itself and an IP address. Configuring the IP address resource is optional if you aren't using the listener functionality, but is recommended.

The AG resource you created is a type of resource called a *clone*. The AG resource essentially has copies on each node, and there's one controlling resource called the *master*. The master is associated with the server hosting the primary replica. The other resources host secondary replicas (regular or configuration-only) and can be promoted to master in a failover.

[!INCLUDE [bias-sensitive-term-t](../includes/bias-sensitive-term-t.md)]

### [Red Hat Enterprise Linux (RHEL) and Ubuntu](#tab/ru)

1. Create the AG resource with the following syntax:

   ```bash
   sudo pcs resource create <NameForAGResource> ocf:mssql:ag ag_name=<AGName> meta failure-timeout=30s --master meta notify=true
   ```

   Where `NameForAGResource` is the unique name given to this cluster resource for the AG, and `AGName` is the name of the AG that was created.

   On RHEL 7.7 and Ubuntu 18.04, and later versions, you might encounter a warning with the use of `--master`, or an error like `sqlag_monitor_0 on ag1 'not configured' (6): call=6, status=complete, exitreason='Resource must be configured with notify=true'`. To avoid this situation, use:

   ```bash
   sudo pcs resource create <NameForAGResource> ocf:mssql:ag ag_name=<AGName> meta failure-timeout=30s master notify=true
   ```

1. Create the IP address resource for the AG that will be associated with the listener functionality.

   ```bash
   sudo pcs resource create <NameForIPResource> ocf:heartbeat:IPaddr2 ip=<IPAddress> cidr_netmask=<Netmask>
   ```

   Where `NameForIPResource` is the unique name for the IP resource, and `IPAddress` is the static IP address assigned to the resource.

1. To ensure that the IP address and the AG resource are running on the same node, a colocation constraint must be configured.

   ```bash
   sudo pcs constraint colocation add <NameForIPResource> <NameForAGResource>-master INFINITY with-rsc-role=Master
   ```

   Where `NameForIPResource` is the name for the IP resource, and `NameForAGResource` is the name for the AG resource.

1. Create an ordering constraint to ensure that the AG resource is up and running before the IP address. While the colocation constraint implies an ordering constraint, this enforces it.

   ```bash
   sudo pcs constraint order promote <NameForAGResource>-master then start <NameForIPResource>
   ```

   Where `NameForIPResource` is the name for the IP resource, and `NameForAGResource` is the name for the AG resource.

### [SUSE Linux Enterprise Server (SLES)](#tab/sles)

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
   op monitor timeout=60s interval=11s role="Master" \
   op monitor timeout=60s interval=12s role="Slave" \
   op notify timeout=60s
   ms ms-ag_cluster <NameForAGResource> \
   meta master-max="1" master-node-max="1" clone-max="3" \
   clone-node-max="1" notify="true" \
   commit
   ```

   Where `NameForAGResource` is the unique name given to this cluster resource for the AG, and `AGName` is the name of the AG that was created.

1. Create the IP address resource for the AG that will be associated with the listener functionality.

   ```bash
   crm configure \
   primitive <NameForIPResource> \
      ocf:heartbeat:IPaddr2 \
      params ip=<IPAddress> \
         cidr_netmask=<Netmask>
   ```

   Where `NameForIPResource` is the unique name for the IP resource, and `IPAddress` is the static IP address assigned to the resource. On SLES, you also need to provide the netmask. For example, 255.255.255.0 would have a value of 24 for `Netmask`.

1. To ensure that the IP address and the AG resource are running on the same node, a colocation constraint must be configured.

   ```bash
   crm configure <NameForConstraint> inf: \
   <NameForIPResource> <NameForAGResource>:Master
   commit
   ```

   Where `NameForIPResource` is the name for the IP resource, `NameForAGResource` is the name for the AG resource, and `NameForConstraint` is the name for the constraint.

1. Create an ordering constraint to ensure that the AG resource is up and running before the IP address. While the colocation constraint implies an ordering constraint, this enforces it.

   ```bash
   crm configure \
   order <NameForConstraint> inf: <NameForAGResource>:promote <NameForIPResource>:start
   commit
   ```

   Where `NameForIPResource` is the name for the IP resource, `NameForAGResource` is the name for the AG resource, and `NameForConstraint` is the name for the constraint.

---

## Next step

In this tutorial, you learned how to create and configure an availability group for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux. You learned how to:
> [!div class="checklist"]
> - Enable availability groups.
> - Create AG endpoints and certificates.
> - Use [!INCLUDE [ssmanstudiofull-md](../includes/ssmanstudiofull-md.md)] (SSMS) or Transact-SQL to create an AG.
> - Create the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] login and permissions for Pacemaker.
> - Create AG resources in a Pacemaker cluster.

For most AG administration tasks, including upgrades and failing over, see:

> [!div class="nextstepaction"]
> [Operate HA availability group for SQL Server on Linux](sql-server-linux-availability-group-failover-ha.md)
