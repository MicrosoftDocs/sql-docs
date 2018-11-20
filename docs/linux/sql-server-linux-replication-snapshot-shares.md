---
title: Configure snapshot folder shares SQL Server Replication on Linux | Microsoft Docs
description: This article describes how to configure snapshot folder shares SQL Server replication on Linux.
author: MikeRayMSFT 
ms.author: mikeray
manager: craigg
ms.date: 9/24/2018
ms.topic: article
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Configure replication snapshot folder with shares

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

The snapshot folder is a directory that you have designated as a share; agents that read from and write to this folder must have enough permissions to access it.

![replication diagram][1]

### Replication Snapshot Folder Share Explained

Before the examples, let's walk through how SQL Server uses samba shares in replication. Below is a basic example of how this works.

1. Samba shares are configured that files written to `/local/path1` by the replication agents on publisher can be seen by the subscriber
2. SQL Server is configured to use share paths when setting up the publisher on the distribution server such that all instances would look at the `//share/path`
3. SQL Server finds the local path from the `//share/path` to know where to look for the files
4. SQL Server reads/writes to local paths backed by a samba share


## Configure a samba share for the snapshot folder 

Replication agents will need a shared directory between replication hosts to access snapshot folders on other machines. For example, in transactional pull replication, the distribution agent resides on the subscriber, which requires access to the distributor to get articles. In this section, we'll go through an example of how to configure a samba share on two replication hosts.


## Steps

As an example, we will configure a snapshot folder on Host 1 (the distributor) to be shared with Host 2 (the subscriber) using Samba. 

### Install and start Samba on both machines 

On Ubuntu:

```bash
sudo apt-get -y install samba
sudo service smbd restart
```

On RHEL:

```bash
sudo yum install samba
sudo service smb start
sudo service smb status
```

### On Host 1 (Distributor) Set-up the Samba share 

1. Set-up user and password for samba:

  ```bash
  sudo smbpasswd -a mssql 
  ```

1. Edit the `/etc/samba/smb.conf` to include the following entry and fill in the *share_name* and *path* fields
 ```bash
  <[share_name]>
  path = </local/path/on/host/1>
  writable = yes
  create mask = 770
  directory mask 
  valid users = mssql 
  ```

  **Example**

  ```bash
  [mssql_data]    <- Name of the shared directory
  path = /var/opt/mssql/repldata <- location of directory we wish to share
  writable = yes  <- determines if the share is writable from other hosts
  create mask = 770  <- Linux permissions for files created 
  directory mask = 770 <- Linux permissions for directories created
  valid users = mssql   <- list of users who can login to this share
  ```

### On Host 2 (Subscriber)  Mount the Samba Share

Edit the command with the correct paths and run the following command on machine2:

  ```bash
  sudo mount //<name_of_host_1>/<share_name> </local/path/on/host/2> -o user=mssql,uid=mssql,gid=mssql
  ```

  **Example**

  ```bash
  mount //host1/mssql_data /var/opt/mssql/repldata_shared -o user=mssql,uid=mssql,gid=mssql

  user=mssql <- sets the login name for samba
  uid=mssql   <- makes the mssql user as the owner of the mounted directory
  gid=mssql   <- sets the mssql group as the owner of the mounted directory
  ```

### On Both Hosts  Configure SQL Server on Linux Instances to use Snapshot Share

Add the following section to `mssql.conf` on both machines. Use wherever the samba share for the //share/path. In this example, it would be `//host1/mssql_data`

  ```bash
  [uncmapping]
  //share/path = /local/path/on/hosts/
  ```

  **Example**

  On host1:

  ```bash
  [uncmapping]
  //host1/mssql_data = /local/path/on/hosts/1
  ```

  On host2:
  
  ```bash
  [uncmapping]
  //host1/mssql_data = /local/path/on/hosts/2
  ```

### Configuring Publisher with Shared paths

* When setting up replication, use the shares path (example `//host1/mssql_data`
* Map `//host1/mssql_data` to a local directory and the mapping added to `mssql.conf`.

## Next steps

[Concepts: SQL Server replication on Linux](sql-server-linux-replication.md)

[Replication stored procedures](../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md).

[1]: ./media/sql-server-linux-replication-snapshot-shares/image1.png
