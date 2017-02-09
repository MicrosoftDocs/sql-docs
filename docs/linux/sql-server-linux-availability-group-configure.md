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

A server that participates in an availability group is called a cluster node. 

## Prerequisites


## Configure the hosts file

The hosts file on every cluster node contains the IP address and name of every cluster node. 

The following command returns the IP address of the current node:

```bash
sudo ip a
```

Set the computer name on each node.

Update `/etc/hostname` file with the new name.


### Configure a computer name for each node

Each node name must be:

- 15 characters or less
- Unique within the network

To set the computer name, add it to `/etc/hostname`. The following script lets you edit `/etc/hostname` with `vi`.

```bash
sudo vi /etc/hosts
```

The following example shows `/etc/hostname` on **node1** with additions for **node1** and **node2**.

```
127.0.0.1   localhost localhost4 localhost4.localdomain4
::1       localhost localhost6 localhost6.localdomain6
10.128.18.128 node1
10.128.16.77 node2
```

>[!WARNING]
>There should not be an entry for the machine's own hostname to 127.x.x.x. For example, in the above example, notice that node1 is mapped to 10.128.18.128 and not mapped to 127.0.0.1.


## Install SQL Server

Install SQL Server. The following links point to SQL Server installation instructions for various distributions. 

- [Red Hat Enterprise Linux](sql-server-linux-setup-red-hat.md)

- [SUSE Linux Enterprise Server](sql-server-linux-setup-suse-linux-enterprise-server.md)

- [Ubuntu](sql-server-linux-setup-ubuntu.md)

### Enable HADRON and restart sqlserver

Enable HADRON on each SQL Server, then restart `mssql-server`.  Run the following script:

```bash
sudo /opt/mssql/bin/mssql-conf set hadrenabled 1
sudo systemctl restart mssql-server
```

## Configure the Availability Group

After you have configured the server name, the hosts file, installed SQL Server, and enabled HADRON you can configure an availability group.  

### Create a certificate

Connect to the SQL Server on the primary node and run the following Transact-SQL to create the certificate:

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

At this point your primary server has a certificate at `/var/opt/mssql/data/dbm_certificate.cer` and a private key at `var/opt/mssql/data/dbm_certificate.pvk`. Copy these two files to the same location on all other nodes. Use the mssql user or give permission to mssql user to access these files. 

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

### Create a master key

Run the following command on each of the secondary nodes to create the certificate on the secondary nodes. 

```Transact-SQL
CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<as3jsdjhaj304SDF>' ]
CREATE LOGIN dbm_login WITH PASSWORD = '<1Sample_Strong_Password!@#>'
CREATE USER dbm_user FOR LOGIN dbm_login
CREATE CERTIFICATE dbm_certificate   
AUTHORIZATION dbm_user
    FROM FILE = 'C:\var\opt\mssql\data\dbm_certificate.cer'
    WITH PRIVATE KEY (
    FILE = 'C:\var\opt\mssql\data\dbm_certificate.pvk',
    DECRYPTION BY PASSWORD = 'as3jsdjhaj304SDF'
            )
```

### Create the HADR endpoints on all replicas

The HADR endpoint is also called the database mirroring endpoint. This endpoint identifies the name, IP address, and port for the availability group. Create the database mirroring endpoint on all nodes. Run the following Transact-SQL on all nodes: 

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

### Create the Availability group on the primary SQL Server instance

Create the availability group. Run the following Transact-SQL on the primary node.

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

### Join secondary SQL Server instances to the Availability Group

### Create the database

### Add additional databases to the availability group

## Next steps

[Configure Red Hat Enterprise Linux Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-rhel.md)

[Configure SUSE Enterprise Linux Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-sles.md)

[Configure Ubuntu Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-ubuntu.md)