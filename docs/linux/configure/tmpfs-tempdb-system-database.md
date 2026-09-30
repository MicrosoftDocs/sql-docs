---
title: Enable and Run tempdb on tmpfs for SQL Server 2025 on Linux
description: Learn how to use tmpfs for the tempdb system database, in SQL Server 2025 on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.date: 11/18/2025
ms.service: sql
ms.subservice: linux
ms.topic: how-to
ms.custom:
  - linux-related-content
  - ignite-2025
---
# Enable and run tempdb on tmpfs for SQL Server 2025 on Linux

[!INCLUDE [sqlserver2025-linux](../../includes/applies-to-version/sqlserver2025-linux.md)]

This article guides you to enable and run `tempdb` database files on the **tmpfs** filesystem in [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)].

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux and containers traditionally support **XFS** and **ext4** filesystems for deploying [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] database files and logs. However, for temporary databases like `tempdb`, which don't require data to be saved from one uptime period to another, using a **tmpfs** filesystem that utilizes memory (RAM) can enhance overall performance for `tempdb`-based workloads.

For more information about the **tmpfs** filesystem, see [tmpfs - The Linux Kernel documentation](https://www.kernel.org/doc/html/latest/filesystems/tmpfs.html).

| Configuration | Description |
| --- | --- |
| **Supported configuration** | **tmpfs** is ideal for storing non-persistent data that doesn't need to be saved across restarts. Currently only the `tempdb` database files are supported on **tmpfs** filesystem for both container and non-container based deployments. |
| **Unsupported configuration** | **tmpfs** filesystem can be used for user databases in SQL container deployments, but only for development purposes. However, this configuration isn't supported. You can provide feedback for this scenario on [GitHub](https://github.com/microsoft/mssql-docker/issues). |

## Physical or virtual machine deployments

To enable **tmpfs** support for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux on physical or virtual machines, you need to mount the **tmpfs** filesystem correctly, which requires sudo access. Once the mount points are set up, you can place the `tempdb` files on these mounts and start [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] with `tempdb` files mounted on the **tmpfs** filesystem.

### Enable tmpfs for `tempdb`

1. Create the `tempdb` directory.

   Use the `mkdir` command to create a directory for the `tempdb` database. Ensure that it's owned by the `mssql` user and group to allow [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] access:

   ```bash
   sudo mkdir /var/opt/mssql/tempdb
   sudo chown mssql. /var/opt/mssql/tempdb
   ```

1. Mount the tmpfs filesystem.

   Use the following command to mount the **tmpfs** filesystem:

   ```bash
   sudo mount -t tmpfs -o size=4G tmpfs /var/opt/mssql/tempdb/
   ```

   The `-t` option specifies the type of filesystem, which in this case is `tmpfs`.

   The `-o` option allows you to specify mount options. Here, `size=4G` sets the maximum size of the **tmpfs** to 4 GB, meaning it can use up to 4 GB of RAM.

   This command mounts **tmpfs** to the target directory used by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] `tempdb` database.

   Optionally, add it to `fstab` to maintain the mounts across restarts:

   ```bash
   echo "tmpfs /var/opt/mssql/tempdb tmpfs defaults,size=4G 0 0" | sudo tee -a /etc/fstab
   ```

1. Update `tempdb` file location.

    Ensure [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is up and running. Connect to your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance using SQL Server Management Studio (SSMS), and run the following Transact-SQL (T-SQL) commands.

   Identify all the `tempdb` files using the following T-SQL script, then run the `ALTER DATABASE` command to update the `tempdb` file location:

   ```sql
   SELECT [name],
          physical_name
   FROM sys.master_files
   WHERE database_id = 2;
   ```

   Based on the number of files you see for `tempdb`, you create the `ALTER DATABASE` commands as follows:

   ```sql
   ALTER DATABASE tempdb
       MODIFY FILE (NAME = tempdev, FILENAME = '/var/opt/mssql/tempdb/tempdb.mdf');

   ALTER DATABASE tempdb
       MODIFY FILE (NAME = tempdev2, FILENAME = '/var/opt/mssql/tempdb/tempdb2.mdf');

   ALTER DATABASE tempdb
       MODIFY FILE (NAME = tempdev3, FILENAME = '/var/opt/mssql/tempdb/tempdb3.mdf');

   ALTER DATABASE tempdb
       MODIFY FILE (NAME = tempdev4, FILENAME = '/var/opt/mssql/tempdb/tempdb4.mdf');

   ALTER DATABASE tempdb
       MODIFY FILE (NAME = templog, FILENAME = '/var/opt/mssql/tempdb/templog.ldf');
   ```

1. Restart [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to apply the changes.

   ```bash
   sudo systemctl restart mssql-server
   ```

1. Verify `tempdb` file locations.

    Once [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is up and running, verify that the `tempdb` data and log files are now located in the new directory by connecting to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] using a tool like SSMS.

   ```sql
   SELECT [name],
          physical_name
   FROM sys.master_files
   WHERE database_id = 2;
   ```

You should now see all the `tempdb` database files on the new mount path that is `/var/opt/mssql/tempdb`.

### Hot resizing of the tmpfs mount

You can also hot-resize the **tmpfs** mount in case the `tempdb` is full. To hot-resize, run the following command to resize the **tmpfs** mount to 6 GB:

```bash
sudo mount -o remount,size=6G /var/opt/mssql/tempdb
```

Optionally, make the new size persist across restarts by modifying the `fstab` entry:

```bash
sudo sed -i 's|tmpfs /var/opt/mssql/tempdb tmpfs defaults,size=4G 0 0|tmpfs /var/opt/mssql/tempdb tmpfs defaults,size=6G 0 0|' /etc/fstab
```

## Container deployments

For developer workloads, **tmpfs** can be used for user databases. **tmpfs** filesystems can significantly speed up test cases for user databases deployed in containers. Since **tmpfs** uses RAM instead of disk storage, it allows for rapid read/write operations. This configuration is useful in development and testing environments where quick iterations are needed.

However, **tmpfs** for user databases is **not** supported. You can provide feedback related to user databases on **tmpfs** on [GitHub](https://github.com/microsoft/mssql-docker/issues).

### Host only the `tempdb` database on the tmpfs filesystem

If you're deploying a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] container and want to ensure that the container uses **tmpfs** for the `tempdb`, you can run the following command:

```bash
docker run \
  -e ACCEPT_EULA=Y \
  -e 'MSSQL_SA_PASSWORD=<password>' \
  --tmpfs /var/opt/mssql/tempdb:uid=10001,gid=10001,size=4G \
  -p 5433:1433 \
  --name sql1 \
  -h sql1 \
  -d mcr.microsoft.com/mssql/server:2025-latest
```

The `--tmpfs` option sets the size to 4 GB and the `uid` (user ID) and `gid` (group ID) to `10001` to ensure that the required permissions are set correctly for the `tempdb` files to be created.

Once the container is up and running, connect to the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance using SSMS, and update the `tempdb` file locations to `/var/opt/mssql/tempdb` with the following T-SQL commands:

```sql
SELECT [name],
       physical_name
FROM sys.master_files
WHERE database_id = 2;
```

Based on the number of files that you see, modify the following command. In this example, there are four `tempdb` data files and one log file. Use the following commands to update the file locations:

```sql
ALTER DATABASE tempdb
MODIFY FILE (NAME = tempdev, FILENAME = '/var/opt/mssql/tempdb/tempdb.mdf');
GO

ALTER DATABASE tempdb
MODIFY FILE (NAME = tempdev2, FILENAME = '/var/opt/mssql/tempdb/tempdb2.mdf');
GO

ALTER DATABASE tempdb
MODIFY FILE (NAME = tempdev3, FILENAME = '/var/opt/mssql/tempdb/tempdb3.mdf');
GO

ALTER DATABASE tempdb
MODIFY FILE (NAME = tempdev4, FILENAME = '/var/opt/mssql/tempdb/tempdb4.mdf');
GO

ALTER DATABASE tempdb
MODIFY FILE (NAME = templog, FILENAME = '/var/opt/mssql/tempdb/templog.ldf');
GO
```

After you update the file locations, stop and restart the container using the following commands:

```bash
docker stop sql1
docker start sql1
```

### Deploy containers with all data and log files on tmpfs

> [!WARNING]  
> This configuration isn't supported, but can be used for development purposes. You can provide feedback for this scenario on [GitHub](https://github.com/microsoft/mssql-docker/issues).

```bash
docker run -e ACCEPT_EULA=Y \
  -e 'MSSQL_SA_PASSWORD=<password>' \
  --tmpfs /var/opt/mssql/data:uid=10001,gid=10001,size=4G \
  -p 5434:1433 \
  --name sql2 \
  -h sql2 \
  -d mcr.microsoft.com/mssql/server:2025-latest
```

## Related content

- [Installation guidance for SQL Server on Linux](../install-upgrade/setup.md)
- [Deploy and connect to SQL Server Linux containers](../containers/deploy.md)
- [Configure SQL Server on Linux with the mssql-conf tool](mssql-conf.md)
