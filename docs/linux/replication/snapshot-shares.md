---
title: Configure Snapshot Folder Shares
titleSuffix: SQL Server on Linux
description: Learn to configure snapshot folder shares SQL Server replication on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.date: 01/02/2026
ms.service: sql
ms.subservice: linux
ms.topic: how-to
ms.custom:
  - linux-related-content
monikerRange: ">=sql-server-ver15 || >=sql-server-linux-ver15"
---
# Configure replication snapshot folder with shares

[!INCLUDE [SQL Server - Linux](../../includes/applies-to-version/sql-linux.md)]

The snapshot folder is a directory that you have designated as a share; agents that read from and write to this folder must have enough permissions to access it.

:::image type="content" source="media/snapshot-shares/snapshot-share-replication.png" alt-text="Diagram of SQL Server replication using a Samba share between a publisher and subscriber." lightbox="media/snapshot-shares/snapshot-share-replication.png":::

## Replication snapshot folder share explained

Before the examples, let's walk through how SQL Server replication uses Samba shares. The following list provides a basic example of how this works.

1. Samba shares are configured so that files written to `/local/path1` by the replication agents on the publisher can be seen by the subscriber.
1. SQL Server is configured to use share paths when setting up the publisher on the distribution server such that all instances look at the `//share/path`.
1. SQL Server finds the local path from the `//share/path` to know where to look for the files.
1. SQL Server reads from and writes to local paths backed by a Samba share.

## Configure a Samba share for the snapshot folder

Replication agents will need a shared directory between replication hosts to access snapshot folders on other machines. For example, in transactional pull replication, the distribution agent resides on the subscriber, which requires access to the distributor to get articles. In this section, we'll go through an example of how to configure a Samba share on two replication hosts.

## Steps

As an example, we will configure a snapshot folder on Host 1 (the distributor) to be shared with Host 2 (the subscriber) using Samba.

### Install and start Samba on both machines

### [RHEL](#tab/rhel)

```bash
sudo yum install samba
sudo service smb start
sudo service smb status
```

### [SLES](#tab/sles)

```bash
sudo yum install samba
sudo service smb start
sudo service smb status
```

> [!NOTE]  
> Starting in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)], SUSE Linux Enterprise Server (SLES) isn't supported.

### [Ubuntu](#tab/ubuntu)

```bash
sudo apt-get -y install samba
sudo service smbd restart
```

---

### Set up the Samba share on distributor (host1)

1. Set up a user and password for Samba:

   ```bash
   sudo smbpasswd -a mssql
   ```

1. Edit the `/etc/samba/smb.conf` to include the following entry and fill in the *share_name* and *path* fields:

   ```output
   <[share_name]>
   path = </local/path/on/host/1>
   writable = yes
   create mask = 770
   directory mask = 770
   valid users = mssql
   ```

   The following table describes each setting.

   | Setting | Description |
   | --- | --- |
   | `[mssql_data]` | Name of the shared directory |
   | `path` | Location of directory we wish to share |
   | `writable` | Determine if the share is writable from other hosts |
   | `create mask` | Linux permissions for files created |
   | `directory mask` | Linux permissions for directories created |
   | `valid users` | List of users who can login to this share |

   **Example**

   ```ini
   [mssql_data]
   path = /var/opt/mssql/repldata
   writable = yes
   create mask = 770
   directory mask = 770
   valid users = mssql
   ```

### Mount the Samba share on subscriber (host2)

Edit the command with the correct paths, and then run it on `host2`:

```bash
sudo mount //<name_of_host_1>/<share_name> </local/path/on/host/2> -o user=mssql,uid=mssql,gid=mssql
```

**Example**

```bash
mount //host1/mssql_data /var/opt/mssql/repldata_shared -o user=mssql,uid=mssql,gid=mssql
```

- `user=mssql` sets the login name for Samba.
- `uid=mssql` makes the `mssql` user the owner of the mounted directory.
- `gid=mssql` makes the `mssql` group the owner of the mounted directory.

### Configure SQL Server on both Linux hosts to use snapshot share

Add the following section to `mssql.conf` on both machines. Use the Samba share path for `//share/path`. In this example, the share path is `//host1/mssql_data`.

```ini
[uncmapping]
//share/path = /local/path/on/host/
```

**Example**

On `host1`:

```ini
[uncmapping]
//host1/mssql_data = /var/opt/mssql/repldata
```

On `host2`:

```ini
[uncmapping]
//host1/mssql_data = /var/opt/mssql/repldata_shared
```

### Configure publisher with shared paths

- When setting up replication, use the share path (for example, `//host1/mssql_data`).
- Map `//host1/mssql_data` to a local directory, and add the mapping to `mssql.conf`.

## Related content

- [SQL Server replication on Linux](overview.md)
- [Replication stored procedures (Transact-SQL)](../../relational-databases/system-stored-procedures/replication-stored-procedures-transact-sql.md)
