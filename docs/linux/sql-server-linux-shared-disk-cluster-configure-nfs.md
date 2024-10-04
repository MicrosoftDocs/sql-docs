---
title: Configure NFS storage FCI - SQL Server on Linux
description: Learn to configure a failover cluster instance (FCI) using NFS storage for SQL Server on Linux.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: vanto
ms.date: 09/23/2024
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom:
  - linux-related-content
---
# Configure failover cluster instance - NFS - SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article explains how to configure NFS storage for a failover cluster instance (FCI) on Linux.

NFS, or network file system, is a common method for sharing disks in the Linux world but not the Windows one. Similar to iSCSI, NFS can be configured on a server or some sort of appliance or storage unit as long as it meets the storage requirements for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

## Important NFS server information

The source hosting NFS (either a Linux server or something else) must be using/compliant with version 4.2 or later. Earlier versions don't work with [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] on Linux.

When configuring the folder(s) to be shared on the NFS server, make sure they follow these guidelines general options:

- `rw` to ensure that the folder can be read from and written to
- `sync` to ensure guaranteed writes to the folder
- Don't use `no_root_squash` as an option; it's considered a security risk
- Make sure the folder has full rights (`777`) applied

Ensure that your security standards are enforced for accessing. When configuring the folder, make sure that only the servers participating in the FCI should see the NFS folder. In the following example, a modified `/etc/exports` on a Linux-based NFS solution is shown, where the folder is restricted to `FCIN1` and `FCIN2`.

```output
# /etc/exports: the access control list for filesystems which may be exported
#               to NFS clients. See export(5).
#
/var/nfs/fci1   FCIN1(rw,sync) FCIN2(rw,sync)
```

## Instructions

1. Choose one of the servers that will participate in the FCI configuration. It doesn't matter which one.

1. Check to see that the server can see the mount(s) on the NFS server.

   ```bash
   sudo showmount -e <IPAddressOfNFSServer>
   ```

   - `<IPAddressOfNFSServer>` is the IP address of the NFS server that you're going to use.

1. For system databases or anything stored in the default data location, follow these steps. Otherwise, skip to Step 4.

   - Ensure that [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] is stopped on the server that you're working on.

     ```bash
     sudo systemctl stop mssql-server
     sudo systemctl status mssql-server
     ```

   - Switch fully to be the superuser.

     ```bash
     sudo -i
     ```

   - Switch to be the `mssql` user.

     ```bash
     su mssql
     ```

   - Create a temporary directory to store the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] data and log files.

     ```bash
     mkdir <TempDir>
     ```

     - `<TempDir>` is the name of the folder. The following example creates a folder named `/var/opt/mssql/tmp`.

     ```bash
     mkdir /var/opt/mssql/tmp
     ```

   - Copy the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] data and log files to the temporary directory.

     ```bash
     cp /var/opt/mssql/data/* <TempDir>
     ```

     - `<TempDir>` is the name of the folder from the previous step.

   - Verify that the files are in the directory.

     ```bash
     ls TempDir
     ```

     - `<TempDir>` is the name of the folder from the previous step.

   - Delete the files from the existing [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] data directory.

     ```bash
     rm - f /var/opt/mssql/data/*
     ```

   - Verify that the files have been deleted.

     ```bash
     ls /var/opt/mssql/data
     ```

   - Type exit to switch back to the root user.

   - Mount the NFS share in the [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] data folder.

     ```bash
     mount -t nfs4 <IPAddressOfNFSServer>:<FolderOnNFSServer> /var/opt/mssql/data -o nfsvers=4.2,timeo=14,intr
     ```

     - `<IPAddressOfNFSServer>` is the IP address of the NFS server that you're going to use
     - `<FolderOnNFSServer>` is the name of the NFS share. The following example syntax matches the NFS information from Step 2.

     ```bash
     mount -t nfs4 10.201.202.63:/var/nfs/fci1 /var/opt/mssql/data -o nfsvers=4.2,timeo=14,intr
     ```

   - Check to see that the mount was successful by issuing mount with no switches.

     ```bash
     mount
     ```

     Here's the expected output.

     ```output
     10.201.202.63:/var/nfs/fcil on /var/opt/mssql/data type nfs4 (rw,relatime,vers=4.2,rsize=524288,wsize=524288,namlen=255,hard, proto=tcp,port=0,timeo=14, retrans=2,sec=sys,clientaddr=10.201.202.128,local lock=none, addr=10.201.202.63)
     ```

   - Switch to the `mssql` user.

     ```bash
     su mssql
     ```

   - Copy the files from the temporary directory /var/opt/mssql/data.

     ```bash
     cp /var/opt/mssql/tmp/* /var/opt/mssqldata
     ```

   - Verify the files are there.

     ```bash
     ls /var/opt/mssql/data
     ```

   - Enter `exit` to not be `mssql`.

   - Enter `exit` to not be root.

   - Start [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)]. If everything was copied correctly and security applied correctly, [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] should show as started.

     ```bash
     sudo systemctl start mssql-server
     sudo systemctl status mssql-server
     ```

   - Create a database to test that security is set up properly. The following example shows that being done via Transact-SQL; it can be done via SSMS.

     :::image type="content" source="media/sql-server-linux-shared-disk-cluster-configure-nfs/20-createtestdatabase.png" alt-text="Screenshot showing how to create the test database.":::

   - Stop [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] and verify it's shut down.

     ```bash
     sudo systemctl stop mssql-server
     sudo systemctl status mssql-server
     ```

   - If you aren't creating any other NFS mounts, unmount the share. If you are, don't unmount.

     ```bash
     sudo umount <IPAddressOfNFSServer>:<FolderOnNFSServer> <FolderToMountIn>
     ```

     - `<IPAddressOfNFSServer>` is the IP address of the NFS server that you're going to use
     - `<FolderOnNFSServer>` is the name of the NFS share
     - `<FolderMountedIn>` is the folder created in the previous step.

1. For things other than system databases, such as user databases or backups, follow these steps. If only using the default location, skip to Step 5.

   - Switch to be the superuser.

     ```bash
     sudo -i
     ```

   - Create a folder that will be used by [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

     ```bash
     mkdir <FolderName>
     ```

     - `<FolderName>` is the name of the folder. The folder's full path needs to be specified if not in the right location.

     The following example creates a folder named `/var/opt/mssql/userdata`.

     ```bash
     mkdir /var/opt/mssql/userdata
     ```

   - Mount the NFS share in the folder that was created in the previous step.

     ```bash
     mount -t nfs4 <IPAddressOfNFSServer>:<FolderOnNFSServer> <FolderToMountIn> -o nfsvers=4.2,timeo=14,intr
     ```

     - `<IPAddressOfNFSServer>` is the IP address of the NFS server that you're going to use
     - `<FolderOnNFSServer>` is the name of the NFS share
     - `<FolderToMountIn>` is the folder created in the previous step.

     The following example mounts the NFS share.

     ```bash
     mount -t nfs4 10.201.202.63:/var/nfs/fci2 /var/opt/mssql/userdata -o nfsvers=4.2,timeo=14,intr
     ```

   - Check to see that the mount was successful by issuing mount with no switches.

   - Type exit to no longer be the superuser.

   - To test, create a database in that folder. The following example uses sqlcmd to create a database, switch context to it, verify the files exist at the OS level, and then deletes the temporary location. You can use SSMS.

     :::image type="content" source="media/sql-server-linux-shared-disk-cluster-configure-nfs/15-createtestdatabase.png" alt-text="Screenshot of the sqlcmd command and the response to the command.":::

   - Unmount the share

     ```bash
     sudo umount <IPAddressOfNFSServer>:<FolderOnNFSServer> <FolderToMountIn>
     ```

     - `<IPAddressOfNFSServer>` is the IP address of the NFS server that you're going to use
     - `<FolderOnNFSServer>` is the name of the NFS share
     - `<FolderMountedIn>` is the folder created in the previous step.

1. Repeat the steps on the other node(s).

## Related content

- [Configure failover cluster instance - SQL Server on Linux (RHEL)](sql-server-linux-shared-disk-cluster-configure.md)
