---
title: Configure iSCSI FCI storage  - SQL Server on Linux
description: Learn to configure a failover cluster instance (FCI) using iSCSI for SQL Server on Linux. 
ms.custom: seo-lt-2019
author: VanMSFT
ms.author: vanto
ms.reviewer: vanto
ms.date: 06/30/2020
ms.topic: conceptual
ms.service: sql
ms.subservice: linux
---
# Configure failover cluster instance - iSCSI - SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article explains how to configure iSCSI storage for a failover cluster instance (FCI) on Linux. 

## Configure iSCSI 
iSCSI uses networking to present disks from a server known as a target to servers. The servers connecting to the iSCSI target require that an iSCSI initiator is configured. The disks on the target are given explicit permissions so that only the initiators that should be able to access them can do so. The target itself should be highly available and reliable.

### Important iSCSI target information
While this section will not cover how to configure an iSCSI target since it is specific to the type of source you will be using, ensure that the security for the disks that will be used by the cluster nodes is configured.  

The target should never be configured on any of the FCI nodes if using a Linux-based iSCSI target. For performance and availability, iSCSI networks should be separate from those used by regular network traffic on both the source and the client servers. Networks used for iSCSI should be fast. Remember that network does consume some processor bandwidth, so plan accordingly if using a regular server.
The most important thing to ensure is completed on the target is that the disks that are created are assigned the proper permissions so that only those servers participating in the FCI have access to them. An example is shown below from the Microsoft iSCSI target where linuxnodes1 is the name created, and in this case, the IP addresses of the nodes are assigned so that NewFCIDisk1.vhdx appears to them.

![Initiator][1]

### Instructions

This section will cover how to configure an iSCSI initiator on the servers that will serve as nodes for the FCI. The instructions should work as is on RHEL and Ubuntu.

For more information on iSCSI initiator for the supported distributions, consult the following links:
- [Red Hat](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/7/html/storage_administration_guide/osm-create-iscsi-initiator)
- [SUSE](https://www.suse.com/documentation/sles11/stor_admin/data/sec_inst_system_iscsi_initiator.html) 
- [Ubuntu](https://help.ubuntu.com/lts/serverguide/iscsi-initiator.html)

1.	Choose one of the servers that will participate in the FCI configuration. It does not matter which one. iSCSI should be on a dedicated network, so configure iSCSI to recognize and use that network. Run `sudo iscsiadm -m iface -I <iSCSIIfaceName> -o new` where `<iSCSIIfaceName>` is the unique or friendly name for the network. The following example uses `iSCSINIC`:
   
    ```bash
    sudo iscsiadm -m iface -I iSCSINIC -o new
    ```
    ![Screenshot of the iface command and the response to the command.][6]
 
2.	Edit `/var/lib/iscsi/ifaces/iSCSIIfaceName`. Make sure it has the following values completely filled out:

    - iface.net_ifacename is the name of the network card as seen in the OS.
    - iface.hwaddress is the MAC address of the unique name that will be created for this interface below.
    - iface.ipaddress
    - iface.subnet_Mask 

    See the following example:

    ![Screenshot of the file with the values completely filled out.][2]

3.	Find the iSCSI target.

    ```bash
    sudo iscsiadm -m discovery -t sendtargets -I <iSCSINetName> -p <TargetIPAddress>:<TargetPort>
    ```

     \<iSCSINetName> is the unique/friendly name for the network, \<TargetIPAddress> is the IP address of the iSCSI target, and \<TargetPort> is the port of the iSCSI target. 

    ![Screenshot of the discovery command and the response to the command.][3]

 
4.	Log into the target

    ```bash
    sudo iscsiadm -m node -I <iSCSIIfaceName> -p TargetIPAddress -l
    ```

    \<iSCSIIfaceName> is the unique/friendly name for the network and \<TargetIPAddress> is the IP address of the iSCSI target.

    ![iSCSITargetLogin][4]

5.	Check to see that there is a connection to the iSCSI target.

    ```bash
    sudo iscsiadm -m session
    ```

    ![iSCSIVerify][5]

 
6.	Check iSCSI attached disks

    ```bash
    sudo grep "Attached SCSI" /var/log/messages
    ```
    ![Screenshot of the grep command and the response to the command showing the attached SCSI disks.][7]

7.	Create a physical volume on the iSCSI disk.

    ```bash
    sudo pvcreate /dev/<devicename>
    ```

    \<devicename> is the name of the device from the previous step. 

 
8.	Create a volume group on the iSCSI disk. Disks assigned to a single volume group are seen as a pool or collection. 

    ```bash
    sudo vgcreate <VolumeGroupName> /dev/devicename
    ```

    \<VolumeGroupName> is the name of the volume group and \<devicename> is the name of the device from Step 6. 
 
9.	Create and verify the logical volume for the disk.

    ```bash
    sudo lvcreate -Lsize -n <LogicalVolumeName> <VolumeGroupName>
    ```
    
    \<size> is the size of the volume to create, and can be specified with G (gigabytes), T (terabytes), etc.,\<LogicalVolumeName> is the name of the logical volume, and \<VolumeGroupName> is the name of the volume group from the previous step. 

    The example below creates a 25GB volume.
 
    ![Create25GBVol][10]

10.	Execute `sudo lvs` to see the LVM that was created.
 
11.	Format the logical volume with a supported filesystem. For EXT4, use the following example:

    ```bash
    sudo mkfs.ext4 /dev/<VolumeGroupName>/<LogicalVolumeName>
    ```

    \<VolumeGroupName> is the name of the volume group from the previous step. \<LogicalVolumeName> is the name of the logical volume from the previous step.  

12.	For system databases or anything stored in the default data location, follow these steps. Otherwise, skip to Step 13.

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
    
        \<TempDir> is the name of the folder. The example below creates a folder named /var/opt/mssql/TempDir.

        ```bash
        mkdir /var/opt/mssql/TempDir
        ```
        
   * Copy the SQL Server data and log files to the temporary directory. You will not receive any acknowledgement if successful.

        ```bash
        cp /var/opt/mssql/data/* <TempDir>
        ```
    
        \<TempDir> is the name of the folder from the previous step.
    
   * Verify that the files are in the directory.

        ```bash
        ls \<TempDir>
        ```
 
        \<TempDir> is the name of the folder from Step d.

   * Delete the files from the existing SQL Server data directory. You will not receive any acknowledgement if successful.

        ```bash
        rm - f /var/opt/mssql/data/*
        ```
    
   * Verify that the files have been deleted. The picture below shows an example of the entire sequence from c through h.

        ```bash
        ls /var/opt/mssql/data
        ```
    
        ![Screenshot of the ls command and the response to the command.][8]

   * Type `exit` to switch back to the root user.

   * Mount the iSCSI logical volume in the SQL Server data folder. You will not receive any acknowledgement if successful.

        ```bash
        mount /dev/<VolumeGroupName>/<LogicalVolumeName> /var/opt/mssql/data
        ```

        \<VolumeGroupName> is the name of the volume group and \<LogicalVolumeName> is the name of the logical volume that was created. The following example syntax matches the volume group and logical volume from the previous command.

        ```bash
        mount /dev/FCIDataVG1/FCIDataLV1 /var/opt/mssql/data
        ```

   * Change the owner of the mount to mssql. You will not receive any acknowledgement if successful.

        ```bash
        chown mssql /var/opt/mssql/data
        ```

   * Change ownership of the group of the mount to mssql. You will not receive any acknowledgement if successful.

        ```bash
        chgrp mssql /var/opt/mssql/data
        ```

   * Switch to the mssql user. You will not receive any acknowledgement if successful.

        ```bash
        su mssql
        ``` 

   * Copy the files from the temporary directory /var/opt/mssql/data. You will not receive any acknowledgement if successful.

        ```bash
        cp /var/opt/mssql/TempDir/* /var/opt/mssql/data
        ``` 
    
   * Verify the files are there.

        ```bash
        ls /var/opt/mssql/data
        ``` 

   *	Enter `exit` to not be mssql.

   *	Enter `exit` to not be root.

   *	Start SQL Server. If everything was copied correctly and security applied correctly, SQL Server should show as started.

        ```bash
        sudo systemctl start mssql-server
        sudo systemctl status mssql-server
        ``` 

   *	Stop SQL Server and verify it is shut down.

        ```bash
        sudo systemctl stop mssql-server
        sudo systemctl status mssql-server
        ``` 

13.	For things other than system databases, such as user databases or backups, follow these steps. If only using the default location, skip to Step 14.

   *	Switch to be the superuser. You will not receive any acknowledgement if successful.

        ```bash
        sudo -i
        ```

   *	Create a folder that will be used by SQL Server. 

        ```bash
        mkdir <FolderName>
        ```

        \<FolderName> is the name of the folder. The folder's full path needs to be specified if not in the right location. The example below creates a folder named /var/opt/mssql/userdata.

        ```bash
        mkdir /var/opt/mssql/userdata
        ```

   *	Mount the iSCSI logical volume in the folder that was created in the previous step. You will not receive any acknowledgement if successful.

        ```bash
        mount /dev/<VolumeGroupName>/<LogicalVolumeName> <FolderName>
        ```

        \<VolumeGroupName> is the name of the volume group, \<LogicalVolumeName> is the name of the logical volume that was created, and \<FolderName> is the name of the folder. Example syntax is shown below.

        ```bash
        mount /dev/FCIDataVG2/FCIDataLV2 /var/opt/mssql/userdata 
        ```

   *	Change ownership of the folder created to mssql. You will not receive any acknowledgement if successful.

        ```bash
        chown mssql <FolderName>
        ```

        \<FolderName> is the name of the folder that was created. An example is shown below.

        ```bash
        chown mssql /var/opt/mssql/userdata
        ```

   *	Change the group of the folder created to mssql. You will not receive any acknowledgement if successful.

        ```bash
        chown mssql <FolderName>
        ```

        \<FolderName> is the name of the folder that was created. An example is shown below.

        ```bash
        chown mssql /var/opt/mssql/userdata
        ```

   *	Type `exit` to no longer be the superuser.

   *	To test, create a database in that folder. The example shown below uses sqlcmd to create a database, switch context to it, verify the files exist at the OS level, and then deletes the temporary location. You can use SSMS.
  
        ![Screenshot of the sqlcmd command and the response to the command.][9]

   *	Unmount the share 

        ```bash
        sudo umount /dev/<VolumeGroupName>/<LogicalVolumeName> <FolderName>
        ```

        \<VolumeGroupName> is the name of the volume group, \<LogicalVolumeName> is the name of the logical volume that was created, and \<FolderName> is the name of the folder. Example syntax is shown below.

        ```bash
        sudo umount /dev/FCIDataVG2/FCIDataLV2 /var/opt/mssql/userdata 
        ```

14.	Configure the server so that only Pacemaker can activate the volume group.

    ```bash
    sudo lvmconf --enable-halvm --services -startstopservices
    ```

15.	Generate a list of the volume groups on the server. Anything listed that is not the iSCSI disk is used by the system, such as for the OS disk.

    ```bash
    sudo vgs
    ```

16.	Modify the activation configuration section of the file /etc/lvm/lvm.conf. Configure the following line:

    ```bash
    volume_list = [ <ListOfVGsNotUsedByPacemaker> ]
    ```

    \<ListOfVGsNotUsedByPacemaker> is the list of volume groups from the output of Step 20 that will not be used by the FCI. Put each one in quotes and separate by a comma. An example is shown below.

    ![Screenshot showing an example of a volume_list value.][11]

17.	When Linux starts, it will mount the file system. To ensure that only Pacemaker can mount the iSCSI disk, rebuild the root filesystem image. 

    Run the following command which may take a few moments to complete. You will get no message back if successful.

    ```bash
    sudo dracut -H -f /boot/initramfs-$(uname -r).img $(uname -r)
    ```

18.	Reboot the server.

19.	On another server that will participate in the FCI, perform Steps 1 - 6. This will present the iSCSI target to the SQL Server. 
 
20.	Generate a list of the volume groups on the server. It should show the volume group created earlier. 

    ```bash
    sudo vgs
    ``` 

23.	Start SQL Server and verify it can be started on this server.

    ```bash
    sudo systemctl start mssql-server
    sudo systemctl status mssql-server
    ```

24.	Stop SQL Server and verify that it is shut down.

    ```bash
    sudo systemctl stop mssql-server
    sudo systemctl status mssql-server
    ```

25.	Repeat Steps 1 - 6 on any other servers that will participate in the FCI.

You are now ready to configure the FCI.

| Distribution | Topic |
| :----------- | :---- |
| Red Hat Enterprise Linux with HA add-on | [Configure](sql-server-linux-shared-disk-cluster-configure.md)<br/>[Operate](sql-server-linux-shared-disk-cluster-red-hat-7-operate.md) |
| SUSE Linux Enterprise Server with HA add-on | [Configure](sql-server-linux-shared-disk-cluster-sles-configure.md) |

## Next steps

[Configure failover cluster instance - SQL Server on Linux](sql-server-linux-shared-disk-cluster-configure.md)

<!--Image references-->
[1]: ./media/sql-server-linux-shared-disk-cluster-configure-iscsi/05-initiator.png
[2]: ./media/sql-server-linux-shared-disk-cluster-configure-iscsi/10-iscsiTargetSettings.png
[3]: ./media/sql-server-linux-shared-disk-cluster-configure-iscsi/15-iSCSITargetResults.png
[4]: ./media/sql-server-linux-shared-disk-cluster-configure-iscsi/20-iSCSITargetLogin.png
[5]: ./media/sql-server-linux-shared-disk-cluster-configure-iscsi/25-iSCSIVerify.png
[6]: ./media/sql-server-linux-shared-disk-cluster-configure-iscsi/7-setiscsinetwork.png
[7]: ./media/sql-server-linux-shared-disk-cluster-configure-iscsi/30-iSCSIattachedDisks.png
[8]: ./media/sql-server-linux-shared-disk-cluster-configure-iscsi/45-CopyMove.png
[9]: ./media/sql-server-linux-shared-disk-cluster-configure-iscsi/50-ExampleCreateSSMS.png
[10]: ./media/sql-server-linux-shared-disk-cluster-configure-iscsi/40-Create25GBVol.png
[11]: ./media/sql-server-linux-shared-disk-cluster-configure-iscsi/55-ListOfVGs.png
