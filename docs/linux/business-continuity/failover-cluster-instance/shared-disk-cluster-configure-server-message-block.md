---
title: Configure SMB Storage FCI for SQL Server on Linux
description: Learn to configure a failover cluster instance (FCI) using SMB storage for SQL Server on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 08/11/2026
ms.service: sql
ms.subservice: linux
ms.topic: how-to
ms.custom:
  - linux-related-content
  - sfi-image-nochange
  - sfi-ropc-blocked
---
# Configure SMB storage failover cluster instance for SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../../../includes/applies-to-version/sql-linux.md)]

This article explains how to configure SMB storage for a failover cluster instance (FCI) on Linux.

Outside Windows, SMB is also called a Common Internet File System (CIFS) share, which Samba implements. On Windows, you access an SMB share with the format `\\SERVERNAME\SHARENAME`. For Linux-based [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] installations, you must mount the SMB share as a folder.

## Important source and server information

Follow these tips to use SMB successfully:

- The SMB share can be on Windows, Linux, or an appliance as long as it uses SMB 3.0 or later. For more information about Samba and SMB 3.0, see [SMB 3.0](https://wiki.samba.org/index.php/Samba3/SMB2#SMB_3.0) to check whether your Samba implementation complies with SMB 3.0.
- The SMB share should be highly available.
- Set security properly on the SMB share. The following example is from `/etc/samba/smb.conf`, where `SQLData` is the name of the share.

```ini
[SQLData]
path=/var/smb/SQLData
read only = no
browseable = yes
guest ok = no
writeable = yes
valid users = SQLSambaUser
```

## Instructions

1. Choose one of the servers to participate in the FCI configuration. It doesn't matter which one.

1. Get information about the `mssql` user.

   ```bash
    sudo id mssql
   ```

   Note the `uid`, `gid`, and groups.

1. Execute `sudo smbclient -L //NameOrIP/ShareName -U User`.

   - `<NameOrIP>` is the DNS name or IP address of the server hosting the SMB share.
   - `<ShareName>` is the name of the SMB share.

1. For system databases, or anything stored in the default data location, follow these steps. Otherwise skip to step 5.

   1. Ensure that [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] is stopped on the server you're working on.

      ```bash
      sudo systemctl stop mssql-server
      sudo systemctl status mssql-server
      ```

   1. Switch fully to the superuser.

      ```bash
      sudo -i
      ```

   1. Switch to the `mssql` user.

      ```bash
      su mssql
      ```

   1. Create a temporary directory to store the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] data and log files.

      ```bash
      mkdir <TempDir>
      ```

      - `<TempDir>` is the name of the folder. The following example creates a folder named `/var/opt/mssql/tmp`.

      ```bash
      mkdir /var/opt/mssql/tmp
      ```

   1. Copy the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] data and log files to the temporary directory.

      ```bash
      cp /var/opt/mssql/data/* <TempDir>
      ```

      - `<TempDir>` is the name of the folder from the previous step.

   1. Verify that the files are in the directory.

      ```bash
      ls <TempDir>
      ```

      \<TempDir> is the name of the folder from Step d.

   1. Delete the files from the existing [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] data directory.

      ```bash
      rm -f /var/opt/mssql/data/*
      ```

   1. Verify that the files no longer exist.

      ```bash
      ls /var/opt/mssql/data
      ```

   1. Enter `exit` to switch back to the `root` user.

   1. Mount the SMB share in the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] data folder. This example shows the syntax for connecting to a Windows Server-based SMB 3.0 share.

      ```bash
      mount -t cifs //<ServerName>/<ShareName> /var/opt/mssql/data -o vers=3.0,username=<UserName>,password=<Password>,domain=<domain>,uid=<mssqlUID>,gid=<mssqlGID>,file_mode=0777,dir_mode=0777
      ```

      - `<ServerName>` is the name of the server with the SMB share
      - `<ShareName>` is the name of the share
      - `<UserName>` is the name of the user to access the share
      - `<Password>` is the password for the user
      - `<domain>` is the name of Active Directory
      - `<mssqlUID>` is the UID of the `mssql` user
      - `<mssqlGID>` is the GID of the `mssql` user

   1. Verify the mount by running `mount` with no switches.

      ```bash
      mount
      ```

   1. Switch to the `mssql` user.

      ```bash
      su mssql
      ```

   1. Copy the files from the temporary directory `/var/opt/mssql/data`.

      ```bash
      cp /var/opt/mssql/tmp/* /var/opt/mssql/data
      ```

   1. Verify the files are there.

      ```bash
      ls /var/opt/mssql/data
      ```

   1. Enter `exit` to leave the `mssql` user.

   1. Enter `exit` to leave the `root` user.

   1. Start [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)]. If you copied everything and applied security correctly, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] starts.

      ```bash
      sudo systemctl start mssql-server
      sudo systemctl status mssql-server
      ```

   1. To test further, create a database to ensure the permissions are fine. The following example uses Transact-SQL; you can use SQL Server Management Studio (SSMS).

      :::image type="content" source="media/shared-disk-cluster-configure-server-message-block/10-testcreatedb.png" alt-text="Screenshot showing the creation of the test database.":::

   1. Stop [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] and verify it's shut down. If you plan to add or test other disks, don't shut down [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] until you add and test them.

      ```bash
      sudo systemctl stop mssql-server
      sudo systemctl status mssql-server
      ```

   1. If you're finished, unmount the share. Otherwise, unmount after you finish testing or adding disks.

      ```bash
      sudo umount //<IPAddressorServerName>/<ShareName>/<FolderMountedIn>
      ```

      - `<IPAddressOrServerName>` is the IP address or name of the SMB host
      - `<ShareName>` is the name of the share
      - `<FolderMountedIn>` is the name of the folder where SMB is mounted

1. For things other than system databases, such as user databases or backups, follow these steps. If you use only the default location, skip to Step 14.

   1. Switch to the superuser.

      ```bash
      sudo -i
      ```

   1. Create a folder for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] to use.

      ```bash
      mkdir <FolderName>
      ```

      \<FolderName> is the name of the folder. Specify the folder's full path if it isn't in the right location. The following example creates a folder named `/var/opt/mssql/userdata`.

      ```bash
      mkdir /var/opt/mssql/userdata
      ```

   1. Mount the SMB share in the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] data folder. This example shows the syntax for connecting to a Samba-based SMB 3.0 share.

      ```bash
      mount -t cifs //<ServerName>/<ShareName> <FolderName> -o vers=3.0,username=<UserName>,password=<Password>,uid=<mssqlUID>,gid=<mssqlGID>,file_mode=0777,dir_mode=0777
      ```

      - `<ServerName>` is the name of the server with the SMB share
      - `<ShareName>` is the name of the share
      - `<FolderName>` is the name of the folder created in the last step
      - `<UserName>` is the name of the user to access the share
      - `<Password>` is the password for the user
      - `<mssqlUID>` is the UID of the `mssql` user
      - `<mssqlGID>` is the GID of the `mssql` user.

   1. Verify the mount by running `mount` with no switches.

   1. Enter `exit` to leave the superuser.

   1. To test, create a database in that folder. The following example uses **`sqlcmd`** to create a database, switch context to it, verify the files exist at the OS level, and then delete the temporary location. You can use SSMS.

   1. Unmount the share.

      ```bash
      sudo umount //<IPAddressorServerName>/<ShareName> /<FolderMountedIn>
      ```

      - `<IPAddressOrServerName>` is the IP address or name of the SMB host
      - `<ShareName>` is the name of the share
      - `<FolderMountedIn>` is the name of the folder where SMB is mounted.

1. Repeat the steps on the other nodes.

You're now ready to configure the FCI.

## Related content

- [Configure failover cluster instance - SQL Server on Linux (RHEL)](shared-disk-cluster-configure.md)
