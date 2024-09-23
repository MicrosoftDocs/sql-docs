---
title: Configure iSCSI FCI storage  - SQL Server on Linux
description: Learn to configure a failover cluster instance (FCI) using iSCSI for SQL Server on Linux.
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
# Configure failover cluster instance - iSCSI - SQL Server on Linux

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

This article explains how to configure iSCSI storage for a failover cluster instance (FCI) on Linux.

## Configure iSCSI

iSCSI uses networking to present disks from a server known as a target to servers. The servers connecting to the iSCSI target require that an iSCSI initiator is configured. The disks on the target are given explicit permissions so that only the initiators that should be able to access them can do so. The target itself should be highly available and reliable.

### Important iSCSI target information

While this section doesn't cover how to configure an iSCSI target since it's specific to the type of source you use, ensure that the security for the disks that will be used by the cluster nodes is configured.

The target should never be configured on any of the FCI nodes if using a Linux-based iSCSI target. For performance and availability, iSCSI networks should be separate from networks used by regular network traffic on both the source and the client servers. Networks used for iSCSI should be fast. Remember that network does consume some processor bandwidth, so plan accordingly if using a regular server.

The most important thing to ensure is completed on the target is that the disks that are created are assigned the proper permissions so that only those servers participating in the FCI have access to them. An example is shown here from the Microsoft iSCSI target where `linuxnodes1` is the name created, and in this case, the IP addresses of the nodes are assigned so that `NewFCIDisk1.vhdx` appears to them.

:::image type="content" source="media/sql-server-linux-shared-disk-cluster-configure-iscsi/05-initiator.png" alt-text="Screenshot of the initiator." lightbox="media/sql-server-linux-shared-disk-cluster-configure-iscsi/05-initiator.png":::

### Instructions

This section covers how to configure an iSCSI initiator on the servers that serve as nodes for the FCI. The instructions should work as is on Red Hat Enterprise Linux (RHEL) and Ubuntu.

For more information on iSCSI initiator for the supported distributions, see the following links:

- [Red Hat](https://docs.redhat.com/en/documentation/red_hat_enterprise_linux/8/html/managing_storage_devices/configuring-an-iscsi-initiator_managing-storage-devices)
- [SUSE](https://documentation.suse.com/sles/15-SP2/html/SLES-all/cha-iscsi.html)
- [Ubuntu](https://ubuntu.com/server/docs/iscsi-initiator-or-client)

1. Choose one of the servers that will participate in the FCI configuration. It doesn't matter which one. iSCSI should be on a dedicated network, so configure iSCSI to recognize and use that network. Run `sudo iscsiadm -m iface -I <iSCSIIfaceName> -o new` where `<iSCSIIfaceName>` is the unique or friendly name for the network. The following example uses `iSCSINIC`:

   ```bash
   sudo iscsiadm -m iface -I iSCSINIC -o new
   ```

   Here's the expected output.

   ```output
   New interface iSCSINIC added
   ```

1. Edit `/var/lib/iscsi/ifaces/iSCSIIfaceName`. Make sure it has the following values completely filled out:

   - `iface.net_ifacename` is the name of the network card as seen in the OS.
   - `iface.hwaddress` is the MAC address of the unique name that will be created for the following interface.
   - `iface.ipaddress`
   - `iface.subnet_Mask`

   See the following example:

   :::image type="content" source="media/sql-server-linux-shared-disk-cluster-configure-iscsi/10-iscsitargetsettings.png" alt-text="Screenshot of the file with the values completely filled out.":::

1. Find the iSCSI target.

   ```bash
   sudo iscsiadm -m discovery -t sendtargets -I <iSCSINetName> -p <TargetIPAddress>:<TargetPort>
   ```

   `<iSCSINetName>` is the unique/friendly name for the network, `<TargetIPAddress>` is the IP address of the iSCSI target, and `<TargetPort>` is the port of the iSCSI target.

   Here's the expected output.

   ```output
   10.181.182.1:3260,1 iqn.1991-05.com.contoso:dcl-linuxnodes1-target
   10.201.202.1:3260,1 iqn.1991-05.com.contoso:dc1-linuxnodes1-target
   [2002:b4b5:b601::b4b5:b601]:3260,1 iqn.1991-05.com.contoso:dcl-linuxnodes1-target
   [2002:8c9:ca01::c8c9:ca01]:3260,1 iqn.1991-05.com.contoso:dcl-linuxnodes1-target
   ```

1. Sign in to the target.

   ```bash
   sudo iscsiadm -m node -I <iSCSIIfaceName> -p TargetIPAddress -l
   ```

   `<iSCSIIfaceName>` is the unique/friendly name for the network and `<TargetIPAddress>` is the IP address of the iSCSI target.

   Here's the expected output.

   ```output
   Logging in to [iface: iSCSINIC, target: ian.1991-05.com.contoso:dcl-linuxnodesl-tar get, portal: 10.181.182.1,3260] (multiple)
   Login to [iface: iSCSINIC, target: ian.1991-05.com.contoso:dcl-linuxnodesl-tar get, portal: 10.181.182.1,3260] successful.
   ```

1. Check to see that there's a connection to the iSCSI target.

   ```bash
   sudo iscsiadm -m session
   ```

   The output looks similar to the following example:

   ```output
   tcp: [1] 10.105.16.7:3260,1 iqn.1991-05.com.contoso:dcl-linuxnodes1-target (non-flash)
   ```

1. Check iSCSI attached disks.

   ```bash
   sudo grep "Attached SCSI" /var/log/messages
   ```

   :::image type="content" source="media/sql-server-linux-shared-disk-cluster-configure-iscsi/30-iscsiattacheddisks.png" alt-text="Screenshot of the grep command and the response to the command showing the attached SCSI disks.":::

1. Create a physical volume on the iSCSI disk.

   ```bash
   sudo pvcreate /dev/<devicename>
   ```

   `<devicename>` is the name of the device from the previous step.

1. Create a volume group on the iSCSI disk. Disks assigned to a single volume group are seen as a pool or collection.

   ```bash
   sudo vgcreate <VolumeGroupName> /dev/devicename
   ```

   `<VolumeGroupName>` is the name of the volume group and `<devicename>` is the name of the device from Step 6.

1. Create and verify the logical volume for the disk.

   ```bash
   sudo lvcreate -Lsize -n <LogicalVolumeName> <VolumeGroupName>
   ```

   `<size>` is the size of the volume to create, and can be specified with G (gigabytes), T (terabytes), etc., `<LogicalVolumeName>` is the name of the logical volume, and `<VolumeGroupName>` is the name of the volume group from the previous step.

   Here's the expected output.

   ```output
   Logical volume "FCIDataLV1" created.
   ```

   The following example creates a 25-GB volume.

1. Execute `sudo lvs` to see the LVM that was created.

1. Format the logical volume with a supported filesystem. For EXT4, use the following example:

    ```bash
    sudo mkfs.ext4 /dev/<VolumeGroupName>/<LogicalVolumeName>
    ```

    `<VolumeGroupName>` is the name of the volume group from the previous step. `<LogicalVolumeName>` is the name of the logical volume from the previous step.

1. For system databases or anything stored in the default data location, follow these steps. Otherwise, skip to Step 13.

   1. Ensure that SQL Server is stopped on the server that you're working on.

      ```bash
      sudo systemctl stop mssql-server
      sudo systemctl status mssql-server
      ```

   1. Switch fully to be the superuser. You don't receive any acknowledgment if successful.

      ```bash
      sudo -i
      ```

   1. Switch to be the `mssql` user. You don't receive any acknowledgment if successful.

      ```bash
      su mssql
      ```

   1. Create a temporary directory to store the SQL Server data and log files. You don't receive any acknowledgment if successful.

      ```bash
      mkdir <TempDir>
      ```

      `<TempDir>` is the name of the folder. The following example creates a folder named /var/opt/mssql/TempDir.

      ```bash
      mkdir /var/opt/mssql/TempDir
      ```

   1. Copy the SQL Server data and log files to the temporary directory. You don't receive any acknowledgment if successful.

      ```bash
      cp /var/opt/mssql/data/* <TempDir>
      ```

      `<TempDir>` is the name of the folder from the previous step.

   1. Verify that the files are in the directory.

      ```bash
      ls <TempDir>
      ```

      `<TempDir>` is the name of the folder from previous steps.

   1. Delete the files from the existing SQL Server data directory. You don't receive any acknowledgment if successful.

      ```bash
      rm - f /var/opt/mssql/data/*
      ```

   1. Verify that the files have been deleted. The following image shows an example of the entire sequence from c through h.

      ```bash
      ls /var/opt/mssql/data
      ```

      :::image type="content" source="media/sql-server-linux-shared-disk-cluster-configure-iscsi/45-copymove.png" alt-text="Screenshot of the ls command and the response to the command." lightbox="media/sql-server-linux-shared-disk-cluster-configure-iscsi/45-copymove.png":::

   1. Type `exit` to switch back to the `root` user.

   1. Mount the iSCSI logical volume in the SQL Server data folder. You don't receive any acknowledgment if successful.

      ```bash
      mount /dev/<VolumeGroupName>/<LogicalVolumeName> /var/opt/mssql/data
      ```

      `<VolumeGroupName>` is the name of the volume group and `<LogicalVolumeName>` is the name of the logical volume that was created. The following example syntax matches the volume group and logical volume from the previous command.

      ```bash
      mount /dev/FCIDataVG1/FCIDataLV1 /var/opt/mssql/data
      ```

   1. Change the owner of the mount to `mssql`. You don't receive any acknowledgment if successful.

      ```bash
      chown mssql /var/opt/mssql/data
      ```

   1. Change ownership of the group of the mount to `mssql`. You don't receive any acknowledgment if successful.

      ```bash
      chgrp mssql /var/opt/mssql/data
      ```

   1. Switch to the `mssql` user. You don't receive any acknowledgment if successful.

      ```bash
      su mssql
      ```

   1. Copy the files from the temporary directory `/var/opt/mssql/data`. You don't receive any acknowledgment if successful.

      ```bash
      cp /var/opt/mssql/TempDir/* /var/opt/mssql/data
      ```

   1. Verify the files are there.

      ```bash
      ls /var/opt/mssql/data
      ```

   1. Enter `exit` to not be `mssql`.

   1. Enter `exit` to not be `root`.

   1. Start SQL Server. If everything was copied correctly and security applied correctly, SQL Server should show as started.

      ```bash
      sudo systemctl start mssql-server
      sudo systemctl status mssql-server
      ```

   1. Stop SQL Server and verify that it has shut down.

      ```bash
      sudo systemctl stop mssql-server
      sudo systemctl status mssql-server
      ```

1. For things other than system databases, such as user databases or backups, follow these steps. If only using the default location, skip to Step 14.

   1. Switch to be the superuser. You don't receive any acknowledgment if successful.

      ```bash
      sudo -i
      ```

   1. Create a folder to be used by SQL Server.

      ```bash
      mkdir <FolderName>
      ```

      `<FolderName>` is the name of the folder. The folder's full path needs to be specified if not in the right location. The following example creates a folder named /var/opt/mssql/userdata.

      ```bash
      mkdir /var/opt/mssql/userdata
      ```

   1. Mount the iSCSI logical volume in the folder that was created in the previous step. You don't receive any acknowledgment if successful.

      ```bash
      mount /dev/<VolumeGroupName>/<LogicalVolumeName> <FolderName>
      ```

      `<VolumeGroupName>` is the name of the volume group, `<LogicalVolumeName>` is the name of the logical volume that was created, and `<FolderName>` is the name of the folder. Example syntax is shown here.

      ```bash
      mount /dev/FCIDataVG2/FCIDataLV2 /var/opt/mssql/userdata
      ```

   1. Change ownership of the folder created to `mssql`. You don't receive any acknowledgment if successful.

      ```bash
      chown mssql <FolderName>
      ```

      `<FolderName>` is the name of the folder that was created. An example is shown here.

      ```bash
      chown mssql /var/opt/mssql/userdata
      ```

   1. Change the group of the folder created to `mssql`. You don't receive any acknowledgment if successful.

      ```bash
      chown mssql <FolderName>
      ```

      `<FolderName>` is the name of the folder that was created. An example is shown here.

      ```bash
      chown mssql /var/opt/mssql/userdata
      ```

   1. Type `exit` to no longer be the superuser.

   1. To test, create a database in that folder. The following script creates a database, switches context to it, verifies the files exist at the OS level, and then deletes the temporary location. You can use SSMS or **sqlcmd** to run this script.

      ```sql
      DROP DATABASE TestDB;
      GO

      CREATE DATABASE TestDB
          ON (NAME = TestDB_Data, FILENAME = '/var/opt/mssql/userdata/TestDB_Data.mdf')
          LOG ON (NAME = TestDB_Log, FILENAME = '/var/opt/mssql/userdata/TestDB_Log.ldf');
      GO

      USE TestDB;
      GO
      ```

      Run the following command in the shell to see the new database files.

      ```bash
      sudo ls /var/opt/mssal/userdata
      ```

      Here's the expected output.

      ```output
      lost+found TestDB_Data.mdf
      TestDB_Log.ldf
      ```

      Delete the database to clean up.

      ```sql
      DROP DATABASE TestDB;
      GO
      ```

      ```bash
      sudo ls /var/opt/mssal/userdata
      ```

      Here's the expected output.

      ```output
      lost+found
      ```

   1. Unmount the share

      ```bash
      sudo umount /dev/<VolumeGroupName>/<LogicalVolumeName> <FolderName>
      ```

      `<VolumeGroupName>` is the name of the volume group, `<LogicalVolumeName>` is the name of the logical volume that was created, and `<FolderName>` is the name of the folder. Example syntax is shown here.

      ```bash
      sudo umount /dev/FCIDataVG2/FCIDataLV2 /var/opt/mssql/userdata
      ```

1. Configure the server so that only Pacemaker can activate the volume group.

   ```bash
   sudo lvmconf --enable-halvm --services -startstopservices
   ```

1. Generate a list of the volume groups on the server. Anything listed that isn't the iSCSI disk is used by the system, such as for the OS disk.

   ```bash
   sudo vgs
   ```

1. Modify the activation configuration section of the file /etc/lvm/lvm.conf. Configure the following line:

   ```bash
   volume_list = [ <ListOfVGsNotUsedByPacemaker> ]
   ```

   `<ListOfVGsNotUsedByPacemaker>` is the list of volume groups from the output of Step 20 that aren't used by the FCI. Put each one in quotes and separate by a comma. An example is shown here.

   :::image type="content" source="media/sql-server-linux-shared-disk-cluster-configure-iscsi/55-listofvgs.png" alt-text="Screenshot showing an example of a volume_list value." lightbox="media/sql-server-linux-shared-disk-cluster-configure-iscsi/55-listofvgs.png":::

1. When Linux starts, it mounts the file system. To ensure that only Pacemaker can mount the iSCSI disk, rebuild the root filesystem image.

   Run the following command, which might take a few moments to complete. You get no message back if successful.

   ```bash
   sudo dracut -H -f /boot/initramfs-$(uname -r).img $(uname -r)
   ```

1. Restart the server.

1. On another server that will participate in the FCI, perform Steps 1 - 6. This presents the iSCSI target to the SQL Server.

1. Generate a list of the volume groups on the server. It should show the volume group created earlier.

   ```bash
   sudo vgs
   ```

1. Start SQL Server and verify it can be started on this server.

   ```bash
   sudo systemctl start mssql-server
   sudo systemctl status mssql-server
   ```

1. Stop SQL Server and verify that it has shut down.

   ```bash
   sudo systemctl stop mssql-server
   sudo systemctl status mssql-server
   ```

1. Repeat Steps 1 - 6 on any other servers that will participate in the FCI.

You're now ready to configure the FCI.

## Related content

- [Configure failover cluster instance - SQL Server on Linux (RHEL)](sql-server-linux-shared-disk-cluster-configure.md)
- [Operate RHEL failover cluster instance (FCI) for SQL Server](sql-server-linux-shared-disk-cluster-red-hat-7-operate.md)
- [Configure SLES shared disk cluster for SQL Server](sql-server-linux-shared-disk-cluster-sles-configure.md)
