---
# required metadata

title: Configure availability group for SQL Server on Linux | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: jhubbard
ms.date: 02/09/2017
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 150b0765-2c54-4bc4-b55a-7e57a5501a0f 

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

# Configure Availability Group for SQL Server on Linux

An availability group is one or more databases that can have replicas on multiple SQL Servers for high-availability (HA), disaster-recovery (DR), and reporting. An availability group defines a set of two or more failover partners, known as availability replicas. Availability replicas are components of the availability group. For details see [Overview of Always On Availability Groups (SQL Server)](http://msdn.microsoft.com/library/ff877884.aspx).

This document describes how to create an availability group on SQL Server on Linux. The document uses the following specific terms:

- **Primary SQL Server**
   This server is the server that holds the primary replica. The primary replica is the availability group replica that allows read and write access to the database. This is also the server where you will create the first certificate and the first database in the availability group.

- **Secondary SQL Server**
   This server includes all SQL Servers that will hold a secondary availability group replica. The secondary availability group replicas cannot be written to. They may or may not be readable.

## Prerequisites

Before you create the availability group, you need to:

- Set your environment so that all nodes in the cluster can communicate
- Install SQL Server

>[!NOTE]
>On Linux, you create the availability group before you create the cluster. This document provides an example that creates the availability group. It does not create the cluster. Create the cluster after you follow the steps in this document. For distribution specific instructions to create the cluster, see the links under [Next steps](#next-steps).

1. **Update the computer name for each host**

   Each SQL Server name must be:
   
   - 15 characters or less
   - Unique within the network
   
   To set the computer name, edit `/etc/hostname`. The following script lets you edit `/etc/hostname` with `vi`.

   ```bash
   sudo vi /etc/hostname
   ```

1. **Configure the hosts file**

   The hosts file on every server contains the IP addresses and names of all servers that will participate in the availability group. 

   The following command returns the IP address of the current server:

   ```bash
   sudo ip addr show
   ```

   Update `/etc/hosts`. The following script lets you edit `/etc/hosts` with `vi`.

   ```bash
   sudo vi /etc/hosts
   ```

   The following example shows `/etc/hosts` on **node1** with additions for **node1** and **node2**. In this document **node1** refers to the primary SQL Server. **node2** refers to the secondary SQL Server.;


   ```
   127.0.0.1   localhost localhost4 localhost4.localdomain4
   ::1       localhost localhost6 localhost6.localdomain6
   10.128.18.128 node1
   10.128.16.77 node2
   ```

### Install SQL Server

Install SQL Server. The following links point to SQL Server installation instructions for various distributions. 

- [Red Hat Enterprise Linux](sql-server-linux-setup-red-hat.md)

- [SUSE Linux Enterprise Server](sql-server-linux-setup-suse-linux-enterprise-server.md)

- [Ubuntu](sql-server-linux-setup-ubuntu.md)

## Enable Always On availability groups and restart sqlserver

Enable Always On availability groups on each SQL Server, then restart `mssql-server`.  Run the following script:

```bash
sudo /opt/mssql/bin/mssql-conf set hadrenabled 1
sudo systemctl restart mssql-server
```

## Create a certificate

SQL Server on Linux uses certificates to authenticate communication between the mirroring endpoints. 

Connect to the primary SQL Server and run the following Transact-SQL to create the certificate:

```Transact-SQL
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<as3jsdjhaj304SDF>'
CREATE CERTIFICATE dbm_certificate WITH SUBJECT = 'dbm'
BACKUP CERTIFICATE dbm_certificate
   TO FILE = 'C:\var\opt\mssql\data\dbm_certificate.cer'
       WITH PRIVATE KEY (
           FILE = 'C:\var\opt\mssql\data\dbm_certificate.pvk',
               ENCRYPTION BY PASSWORD = '<as3jsdjhaj304SDF>'
       )
```

>[!NOTE]
>For this release, do not use Linux-style paths like `/var/opt/mssql/data/dbm_certificate.cer` for the certificates.

At this point your primary SQL server has a certificate at `/var/opt/mssql/data/dbm_certificate.cer` and a private key at `var/opt/mssql/data/dbm_certificate.pvk`. Copy these two files to the same location on all other servers. Use the mssql user or give permission to mssql user to access these files. 

For example on the source server, the following command copies the  files to the target machine.

```bash
cd /var/opt/mssql/data
scp dbm_certificate.* root@node2:/var/opt/mssql/data/
```

On the target server, give permission to mssql user to access the certificate.

```bash
cd /var/opt/mssql/data
chown mssql:mssql dbm_certificate.*
```

## Create the certificate on secondary servers

Run the following command on all secondary servers to create the certificate. The command also creates a user named dbm_user and authorizes the user to access the certificate.

```Transact-SQL
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<as3jsdjhaj304SDF>' ]
CREATE CERTIFICATE dbm_certificate   
AUTHORIZATION dbm_user
    FROM FILE = 'C:\var\opt\mssql\data\dbm_certificate.cer'
    WITH PRIVATE KEY (
    FILE = 'C:\var\opt\mssql\data\dbm_certificate.pvk',
    DECRYPTION BY PASSWORD = '<as3jsdjhaj304SDF>'
            )
```

## Create db mirroring endpoint user

Run the following command on all SQL Servers to create the database mirroring endpoint user.

```Transact-SQL
CREATE LOGIN dbm_login WITH PASSWORD = '<1Sample_Strong_Password!@#>'
CREATE USER dbm_user FOR LOGIN dbm_login
```

## Create the database mirroring endpoints on all replicas

Database mirroring endpoints use Transmission Control Protocol (TCP) to send and receive messages between the server instances participating database mirroring sessions or hosting availability replicas. The database mirroring endpoint listens on a unique TCP port number. Run the following Transact-SQL on all SQL Servers: 

```Transact-SQL
CREATE ENDPOINT [Hadr_endpoint]
    AS TCP (LISTENER_IP = (<0.0.0.0>), LISTENER_PORT = <5022>)
    FOR DATA_MIRRORING (
	    ROLE = ALL,
	    AUTHENTICATION = CERTIFICATE dbm_certificate,
		ENCRYPTION = REQUIRED ALGORITHM AES
		)
ALTER ENDPOINT [Hadr_endpoint] STATE = STARTED
GRANT CONNECT ON ENDPOINT::[Hadr_endpoint] TO [dbm_login]
```

>[!IMPORTANT]
>The TCP port on the firewall needs to be open for the listener port.

## Create the availability group

Create the availability group. The following Transact-SQL creates an availability group name `ag1`. The script configures the availability group replicas with `SEEDING_MODE = AUTOMATIC`. This setting causes SQL Server to automatically create the database on each secondary server after it is added to the availability group. Run the following Transact-SQL on the primary SQL Server to create the availability group.

```Transact-SQL
CREATE AVAILABILITY GROUP [ag1]
    WITH (DB_FAILOVER = ON, CLUSTER_TYPE = NONE)
    FOR REPLICA ON
        N'node1' WITH (
            ENDPOINT_URL = N'tcp://node1:5022',
		    AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
		    FAILOVER_MODE = AUTOMATIC,
		    SEEDING_MODE = AUTOMATIC,
		    SECONDARY_ROLE (ALLOW_CONNECTIONS = ALL)
		    ),
        N'node2' WITH ( 
		    ENDPOINT_URL = N'tcp://node2:5022', 
		    AVAILABILITY_MODE = SYNCHRONOUS_COMMIT,
		    FAILOVER_MODE = AUTOMATIC,
		    SEEDING_MODE = AUTOMATIC,
		    SECONDARY_ROLE (ALLOW_CONNECTIONS = ALL)
		    )
		
ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE
```

>[!NOTE]
>`CLUSTER_TYPE` is a new option for `CREATE AVAILABILITY GROUP`. `CLUSTER_TYPE = NONE` is required for availability groups that are not on servers that belong to a Windows Server Failover Cluster.    


### Join secondary SQL Servers to the availability group

On each secondary SQL Server, run the following Transact-SQL to join the availability group.

```Transact-SQL
ALTER AVAILABILITY GROUP [ag1] JOIN WITH (CLUSTER_TYPE = NONE)
		 
ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE
```

## Create the database

Ensure the database you are adding to the Availability group is in full recovery mode and has a valid log backup. If this is a test database or a new database created, take a database backup. On the primary SQL Server, run the following Transact-SQL to create and back up a database called `db1`.

```Transact-SQL
CREATE DATABASE [db1]
ALTER DATABASE [db1] SET RECOVERY FULL
BACKUP DATABASE [db1] TO DISK = N'NUL'
```

### Add the database to the availability group

On the primary SQL Server, run the following Transact-SQL to add a database called `db1` to an availability group called `ag1`.

```Transact-SQL
ALTER AVAILABILITY GROUP [ag1] ADD DATABASE [db1]
```

### Verify that the database is created on the secondary servers

On each secondary SQL Server, run the following query to see if the `db1` database has been created and is synchronized.

```Transact-SQL
SELECT * FROM sys.databases WHERE name = 'db1'
GO
SELECT DB_NAME(database_id) AS database, synchronization_state_desc FROM sys.dm_hadr_database_replica_states
```

## Notes

### Operations before the cluster is configured 
If you followed the steps in this document, you have an availability group that is not yet clustered. The next step is to add the cluster. While the availability group is not in a cluster, note the following behaviors:

- If the primary replica goes down and comes back up - for example if the SQL Server instance or node restarts - the availability group will go in `RESOLVING` state. Because there is no cluster controller to manage the availability group elect one of replicas as primary, you need to run `ALTER AVAILABILITY GROUP FAILOVER` on the replica that you choose as primary. You can run `ALTER AVAILABILITY GROUP FAILOVER` on any the former primary replica or any secondary replica. Note the following behavior:
   - If you run this on the former primary replica then previous configuration returns.
   - If you run this on a secondary replica then the rest of replicas - including the former PRIMARY - will automatically join the availability group.
   
   >[!NOTE]
   >A database restart does not trigger the availability group to go into a `RESOLVING` state. Only an instance restart triggers availability group state evaluation.

- Manual failover is a two step process.
   1. Demote the current primary. On the primary SQL Server, run the following query:
      ```Transact-SQL
      ALTER AVAILABILITY GROUP [AgName] SET (ROLE = SECONDARY)
      ```
   1. Promote the current secondary to new primary. On the node that you want to promote run the following query:
      ```Transact-SQL
      ALTER AVAILABILITY GROUP [AgName] FAILOVER
      ```

>[!IMPORTANT]
>After you configure the cluster, you cannot use Transact-SQL to fail over the availability group resources. SQL Server cluster resources on Linux are not coupled as tightly with the operating system as they are on a Windows Server Failover Cluster (WSFC). SQL Server is not aware of the presence of the cluster. All orchestration is done through the cluster resources. 

## Next steps

[Configure Red Hat Enterprise Linux Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-rhel.md)

[Configure SUSE Linux Enterprise Server Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-sles.md)

[Configure Ubuntu Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-ubuntu.md)