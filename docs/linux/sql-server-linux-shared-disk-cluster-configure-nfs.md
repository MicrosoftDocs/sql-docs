---
title: Configure NFS storage FCI - SQL Server on Linux
description: Learn to configure a failover cluster instance (FCI) using NFS storage for SQL Server on Linux. 
ms.custom: seo-lt-2019
author: VanMSFT
ms.author: vanto
ms.reviewer: vanto
ms.date: 08/28/2017
ms.topic: conceptual
ms.service: sql
ms.subservice: linux
---
# Configure failover cluster instance - NFS - SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article explains how to configure NFS storage for a failover cluster instance (FCI) on Linux. 

NFS, or network file system, is a common method for sharing disks in the Linux world but not the Windows one. Similar to iSCSI, NFS can be configured on a server or some sort of appliance or storage unit as long as it meets the storage requirements for SQL Server.

## Important NFS server information

The source hosting NFS (either a Linux server or something else) must be using/compliant with version 4.2 or later. Earlier versions will not work with SQL Server on Linux.

When configuring the folder(s) to be shared on the NFS server, make sure they follow these guidelines general options:
- `rw` to ensure that the folder can be read from and written to
- `sync` to ensure guaranteed writes to the folder
- Do not use `no_root_squash` as an option; it is considered a security risk
- Make sure the folder has full rights (777) applied

Ensure that your security standards are enforced for accessing. When configuring the folder, make sure that only the servers participating in the FCI should see the NFS folder. An example of a modified /etc/exports on a Linux-based NFS solution is shown below where the folder is restricted to FCIN1 and FCIN2.

![Screenshot of an example of a modified /etc/exports on a Linux-based NFS solution is shown below where the folder is restricted to FCIN1 and FCIN2.][1]

## Instructions

1. Choose one of the servers that will participate in the FCI configuration. It does not matter which one. 

2. Check to see that the server can see the mount(s) on the NFS server.

    ```bash
    sudo showmount -e <IPAddressOfNFSServer>
    ```

    \<IPAddressOfNFSServer> is the IP address of the NFS server that you are going to use.

3. For system databases or anything stored in the default data location, follow these steps. Otherwise, skip to Step 4.
 
   * Ensure that SQL Server is stopped on the server that you are working on.

    ```bash
    sudo systemctl stop mssql-server
    sudo systemctl status mssql-server
    ```
   * Switch fully to be the superuser. You will not receive any acknowledgement if successful.

    ```bash
    sudo -i
    ```

   * Switch to be the mssql user. You will not receive any acknowledgement if successful.

    ```bash
    su mssql
    ```

   * Create a temporary directory to store the SQL Server data and log files. You will not receive any acknowledgement if successful.

    ```bash
    mkdir <TempDir>
    ```

    \<TempDir> is the name of the folder. The following example creates a folder named /var/opt/mssql/tmp.

    ```bash
    mkdir /var/opt/mssql/tmp
    ```

   * Copy the SQL Server data and log files to the temporary directory. You will not receive any acknowledgement if successful.
    
    ```bash
    cp /var/opt/mssql/data/* <TempDir>
    ```

    \<TempDir> is the name of the folder from the previous step.

   * Verify that the files are in the directory.

    ```bash
    ls TempDir
    ```

    \<TempDir> is the name of the folder from Step d.

   * Delete the files from the existing SQL Server data directory. You will not receive any acknowledgement if successful.

    ```bash
    rm - f /var/opt/mssql/data/*
    ```

   * Verify that the files have been deleted. 

    ```bash
    ls /var/opt/mssql/data
    ```
    
   * Type exit to switch back to the root user.

   * Mount the NFS share in the SQL Server data folder. You will not receive any acknowledgement if successful.

    ```bash
    mount -t nfs4 <IPAddressOfNFSServer>:<FolderOnNFSServer> /var/opt/mssql/data -o nfsvers=4.2,timeo=14,intr
    ```

    \<IPAddressOfNFSServer> is the IP address of the NFS server that you are going to use 

    \<FolderOnNFSServer> is the name of the NFS share. The following example syntax matches the NFS information from Step 2.

    ```bash
    mount -t nfs4 200.201.202.63:/var/nfs/fci1 /var/opt/mssql/data -o nfsvers=4.2,timeo=14,intr
    ```

   * Check to see that the mount was successful by issuing mount with no switches.

    ```bash
    mount
    ```

    ![Screenshot of the mount command and the response to the command showing no switches.][2]

   * Switch to the mssql user. You will not receive any acknowledgement if successful.

    ```bash
    su mssql
    ```

   * Copy the files from the temporary directory /var/opt/mssql/data. You will not receive any acknowledgement if successful.

    ```bash
    cp /var/opt/mssql/tmp/* /var/opt/mssqldata
    ```

   * Verify the files are there.

    ```bash
    ls /var/opt/mssql/data
    ```

   * Enter exit to not be mssql 
    
   * Enter exit to not be root

   * Start SQL Server. If everything was copied correctly and security applied correctly, SQL Server should show as started.

    ```bash
    sudo systemctl start mssql-server
    sudo systemctl status mssql-server
    ```
    
   * Create a database to test that security is set up properly. The following example shows that being done via Transact-SQL; it can be done via SSMS.
 
    ![CreateTestdatabase][3]

   * Stop SQL Server and verify it is shut down.

    ```bash
    sudo systemctl stop mssql-server
    sudo systemctl status mssql-server
    ```

   * If you are not creating any other NFS mounts, unmount the share. If you are, do not unmount.

    ```bash
    sudo umount <IPAddressOfNFSServer>:<FolderOnNFSServer> <FolderToMountIn>
    ```

    \<IPAddressOfNFSServer> is the IP address of the NFS server that you are going to use

    \<FolderOnNFSServer> is the name of the NFS share

    \<FolderMountedIn> is the folder created in the previous step. 

4. For things other than system databases, such as user databases or backups, follow these steps. If only using the default location, skip to Step 5.

   * Switch to be the superuser. You will not receive any acknowledgement if successful.

    ```bash
    sudo -i
    ```

   * Create a folder that will be used by SQL Server. 

    ```bash
    mkdir <FolderName>
    ```

    \<FolderName> is the name of the folder. The folder's full path needs to be specified if not in the right location. The following example creates a folder named /var/opt/mssql/userdata.

    ```bash
    mkdir /var/opt/mssql/userdata
    ```

   * Mount the NFS share in the folder that was created in the previous step. You will not receive any acknowledgement if successful.

    ```bash
    Mount -t nfs4 <IPAddressOfNFSServer>:<FolderOnNFSServer> <FolderToMountIn> -o nfsvers=4.2,timeo=14,intr
    ```

    \<IPAddressOfNFSServer> is the IP address of the NFS server that you are going to use

    \<FolderOnNFSServer> is the name of the NFS share

    \<FolderToMountIn> is the folder created in the previous step. Below is an example. 

    ```bash
    mount -t nfs4 200.201.202.63:/var/nfs/fci2 /var/opt/mssql/userdata -o nfsvers=4.2,timeo=14,intr
    ```

   * Check to see that the mount was successful by issuing mount with no switches.
  
   * Type exit to no longer be the superuser.

   * To test, create a database in that folder. The following example uses sqlcmd to create a database, switch context to it, verify the files exist at the OS level, and then deletes the temporary location. You can use SSMS.

    ![Screenshot of the sqlcmd command and the response to the command.][4]
 
   * Unmount the share 

    ```bash
    sudo umount <IPAddressOfNFSServer>:<FolderOnNFSServer> <FolderToMountIn>
    ```

    \<IPAddressOfNFSServer> is the IP address of the NFS server that you are going to use
    
    \<FolderOnNFSServer> is the name of the NFS share

    \<FolderMountedIn> is the folder created in the previous step. Below is an example. 
 
5. Repeat the steps on the other node(s).


## Next steps

[Configure failover cluster instance - SQL Server on Linux](sql-server-linux-shared-disk-cluster-configure.md)

<!--Image references-->
[1]: ./media/sql-server-linux-shared-disk-cluster-configure-nfs/05-nfsacl.png
[2]: ./media/sql-server-linux-shared-disk-cluster-configure-nfs/10-mountnoswitches.png
[3]: ./media/sql-server-linux-shared-disk-cluster-configure-nfs/20-createtestdatabase.png
[4]: ./media/sql-server-linux-shared-disk-cluster-configure-nfs/15-createtestdatabase.png
