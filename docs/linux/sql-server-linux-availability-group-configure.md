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

An availability Group is one or more databases that can have replicas on multiple SQL Servers for high-availability (HA), disaster-recovery (DR), and reporting. An availability group defines a set of two or more failover partners, known as availability replicas. Availability replicas are components of the availability group. For details see [Overview of Always On Availability Groups (SQL Server)](http://msdn.microsoft.com/library/ff877884.aspx).

This document describes how to create an availability group on SQL Server on Linux. The document uses the following specific terms:

- **Primary SQL Server**
   This server is the server that holds the primary replica. The primary replica is the availability group replica that allows read and write access to the database. This is also the server where you will create the first certificate and the first database in the availability group.

- **Secondary SQL Server**
   This server includes all SQL Servers that will hold a secondary availability group replica. The secondary availability group replicas cannot be written to. They may or may not be readable.

## Prerequisites

Before you create the availability group, you need to:

- Configure the host file on each server
- Set the server name for each server
- Install SQL Server

>[!NOTE]
>On Linux, you create the availability group before you create the cluster. This document provides an example that creates the availability group. It does not create the cluster. Create the cluster after you follow the steps in this document. For distribution specific instructions to create the cluster, see the links under [Next steps](#next-steps).

### Configure the hosts file

The hosts file on every SQL Server contains the IP address and name of every SQL Server that will participate in the availability group. 

The following command returns the IP address of the current server:

```bash
sudo ip a
```

Set the computer name on each server.

Update `/etc/hostname` file with the new name.


### Configure a computer name for each server

Each SQL Server name must be:

- 15 characters or less
- Unique within the network

To set the computer name, add it to `/etc/hostname`. The following script lets you edit `/etc/hostname` with `vi`.

```bash
sudo vi /etc/hosts
```

The following example shows `/etc/hostname` on **node1** with additions for **node1** and **node2**. In this document **node1** refers to the primary SQL Server. **node2** refers to the secondary SQL Server.;

```
127.0.0.1   localhost localhost4 localhost4.localdomain4
::1       localhost localhost6 localhost6.localdomain6
10.128.18.128 node1
10.128.16.77 node2
```

>[!WARNING]
>There should not be an entry for the machine's own hostname to 127.x.x.x. For example, in the above example, notice that node1 is mapped to 10.128.18.128 and not mapped to 127.0.0.1.


### Install SQL Server

Install SQL Server. The following links point to SQL Server installation instructions for various distributions. 

- [Red Hat Enterprise Linux](sql-server-linux-setup-red-hat.md)

- [SUSE Linux Enterprise Server](sql-server-linux-setup-suse-linux-enterprise-server.md)

- [Ubuntu](sql-server-linux-setup-ubuntu.md)

## Enable HADRON and restart sqlserver

Enable HADRON on each SQL Server, then restart `mssql-server`.  Run the following script:

```bash
sudo /opt/mssql/bin/mssql-conf set hadrenabled 1
sudo systemctl restart mssql-server
```

## Create a certificate

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
DROP CERTIFICATE dbm_certificate
```

>[!NOTE]
>Do not use Linux-style paths like `/var/opt/mssql/data/dbm_certificate.cer` for the certificates.

At this point your primary SQL server has a certificate at `/var/opt/mssql/data/dbm_certificate.cer` and a private key at `var/opt/mssql/data/dbm_certificate.pvk`. Copy these two files to the same location on all other SQL Servers. Use the mssql user or give permission to mssql user to access these files. 

For example on the source machine, the following command copies the  files to the target machine.

```bash
cd /var/opt/mssql/data
scp dbm_certificate.* root@targetmachine:/var/opt/mssql/data/
```

Alternatively on the destination server, you can copy the certificate and key with the following command.

```bash
cd /var/opt/mssql/data
chown mssql:mssql dbm_certificate.*
```

## Create the certificate on secondary servers

Run the following command on each of the secondary SQL Servers to create the certificate. 

```Transact-SQL
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<as3jsdjhaj304SDF>' ]
CREATE LOGIN dbm_login WITH PASSWORD = '<1Sample_Strong_Password!@#>'
CREATE USER dbm_user FOR LOGIN dbm_login
CREATE CERTIFICATE dbm_certificate   
AUTHORIZATION dbm_user
    FROM FILE = 'C:\var\opt\mssql\data\dbm_certificate.cer'
    WITH PRIVATE KEY (
    FILE = 'C:\var\opt\mssql\data\dbm_certificate.pvk',
    DECRYPTION BY PASSWORD = '<as3jsdjhaj304SDF>'
            )
```

## Create the HADR endpoints on all replicas

The HADR endpoint is also called the database mirroring endpoint. This endpoint identifies the name, IP address, and port for the availability group. Create the database mirroring endpoint on all SQL Servers. Run the following Transact-SQL on all SQL Servers: 

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

### Join secondary SQL Servers

On each secondary SQL Server, run the following Transact-SQL to join the availability group.

```Transact-SQL
ALTER AVAILABILITY GROUP [ag1] JOIN WITH (CLUSTER_TYPE = NONE)
		 
ALTER AVAILABILITY GROUP [ag1] GRANT CREATE ANY DATABASE
```

>[!NOTE]
>In some cases the first command, above will take approximately 30 seconds to complete. 

## Test the Availability Group

To test the availability group create a database, add it to an availability group and verify that the database is created on the secondary servers. 

### Create the database
On the primary SQL Server, run the following Transact-SQL to create a database called `db1`.

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

On each secondary SQL Server, run the following query to see if the `db1` database has been created.

```Transact-SQL
SELECT * FROM sys.databases WHERE name = 'db1'
```

## Notes



## Next steps

[Configure Red Hat Enterprise Linux Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-rhel.md)

[Configure SUSE Linux Enterprise Server Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-sles.md)

[Configure Ubuntu Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-ubuntu.md)