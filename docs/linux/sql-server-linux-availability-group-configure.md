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

Alternatively, you copy from the target machine with the following command.

```bash
cd /var/opt/mssql/data
chown mssql:mssql dbm_certificate.*
```

### Create a master key 



### Create the HADR endpoints on all replicas

### Create the Availability group on the primary SQL Server instance

### Join secondary SQL Server instances to the Availability Group

### Create the database

### Add additional databases to the availability group

## Next steps

[Configure Red Hat Enterprise Linux Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-rhel.md)

[Configure SUSE Enterprise Linux Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-sles.md)

[Configure Ubuntu Cluster for SQL Server Availability Group Cluster Resources](sql-server-linux-availability-group-cluster-ubuntu.md)