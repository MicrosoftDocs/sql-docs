---
title: Acquire and configure backup server
description: Learn how to configure a non-appliance Windows system as a backup server for use with Analytics Platform System and Parallel Data Warehouse.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: martinle
ms.date: 06/23/2022
ms.service: sql
ms.subservice: data-warehouse
ms.topic: how-to
ms.custom:
  - seo-dt-2019
  - kr2b-contr-experiment
---

# Acquire and configure a backup server for Analytics Platform System

A non-appliance Windows system can act as a backup server with the backup and restore features in Analytics Platform System (APS) and Parallel Data Warehouse (PDW). This article describes how to configure the system.

## <a name="Basics"></a>Backup server basics

The backup server:

- Is provided and managed by your own IT team.
- Doesn't require any PDW-specific software or tools. PDW doesn't install any software onto the backup server.
- Is located in your own non-appliance rack. It can't be placed within the APS appliance.
- Can be connected to the appliance InfiniBand network. Backups can be performed over InfiniBand or Ethernet. We recommend InfiniBand for performance reasons.
- Is in your own customer domain, not the appliance domain. There's no trust relationship between your customer domain and the appliance domain.
- Hosts a backup file share, which is a Windows file share that uses the Server Message Block (SMB) application-level network protocol. The backup file share permissions give a Windows domain user, usually a dedicated backup user, the ability to perform backup and restore operations on the share. The username and password credentials of the Windows domain user are stored in PDW so that PDW can perform backup and restore operations on the backup file share.

## <a name="Step1"></a>Determine capacity requirements

The system requirements for the backup server depend almost completely on your own workload. Before you purchase or provision a backup server, figure out your capacity requirements. The backup server doesn't have to be dedicated only to backups, as long as it handles the performance and storage requirements of your workload. You can also have multiple backup servers in order to back up and restore each database to one of several servers.

Use the [Backup server capacity planning worksheet](backup-capacity-planning-worksheet.md) to help determine your capacity requirements.
## <a name="Step2"></a>Acquire the backup server

Once you better understand your capacity requirements, plan the servers and networking components that you need to purchase or provision. Incorporate the following list of requirements into your purchasing plan, and then purchase your server or provision an existing server.

### Software requirements

Any file server that uses the Windows File Sharing (SMB) protocol.

We recommend Windows Server 2012 or beyond in order to:

- Get the performance benefit of file pre-allocation over SMB.
- Use Instant File Initialization (IFI) for backup operations. Your IT team manages this setting on the backup server. The PDW Configuration Manager (*dwconfig.exe*) doesn't set or control IFI on your backup server. Previous versions of Windows don't have IFI, but can still be used as backup servers.

### Networking requirements

Although not required, we recommend InfiniBand as the connection type for Backup servers. To prepare for connecting the loading server to the appliance InfiniBand network:

- Plan to rack the server close enough to the appliance so that you can connect it to the appliance InfiniBand switches. For more information from Mellanox Technologies about InfiniBand, see the whitepaper, [Introduction to InfiniBand](https://www.mellanox.com/pdf/whitepapers/IB_Intro_WP_190.pdf).
- Purchase a Mellanox ConnectX-3 FDR InfiniBand single or dual port network adapter. We recommend purchasing the network adapter with two ports for fault tolerance during data transmission. A two port network adapter is required for high availability.
- Purchase two FDR InfiniBand cables for a dual port card, or one FDR InfiniBand cable for a single port card. The FDR InfiniBand cables connect the loading server to the appliance InfiniBand network. The cable length depends on the distance between the loading server and the appliance InfiniBand switches, according to your environment.

## <a name="Step3"></a>Connect the server to the InfiniBand networks

Use these steps to connect the loading server to the InfiniBand network. If the server isn't using the InfiniBand network, skip this step.

1. Rack the server close enough to the appliance so that you can connect it to the appliance InfiniBand network.

1. Install the InfiniBand Mellanox ConnectX-3 FDR InfiniBand network adapter into the loading server.

1. Use the FDR cables to connect the InfiniBand network adapter to one of the two InfiniBand switches in the first appliance rack.

1. Install and configure the appropriate Windows driver for the InfiniBand network adapter.

   InfiniBand drivers for Windows are developed by the OpenFabrics Alliance, an industry consortium of InfiniBand vendors. The correct driver might have been distributed with your InfiniBand network adapter. If not, download the driver from [OpenFabrics Alliance](https://www.openfabrics.org).

1. Configure the InfiniBand and DNS settings for the network adapters. For configuration instructions, see [Configure InfiniBand Network Adapters](configure-infiniband-network-adapters.md).

## <a name="Step4"></a>Configure the backup file share

PDW accesses the backup server through a UNC file share. To set up the file share:

1. Create a folder on the backup server for storing your backups.

1. Create a file share, called a *backup share*, for the backup folder.

1. Designate or create a Windows domain account in your customer domain that you want to use for the purposes of performing backups and restores. For security reasons, it's best to use a dedicated account as the backup user.

1. Add permissions to the backup share so that only trusted accounts and a domain backup account can access, read, and write to the share location.

1. Add the backup domain account credentials to PDW.

   For example:

   ```sql
   EXEC sp_pdw_add_network_credentials '10.192.147.63', 'seattle\david', '********';
   ```

   For more information, see these stored procedures:

   - [sp_pdw_add_network_credentials](../relational-databases/system-stored-procedures/sp-pdw-add-network-credentials-sql-data-warehouse.md)
   - [sp_pdw_remove_network_credentials](../relational-databases/system-stored-procedures/sp-pdw-remove-network-credentials-sql-data-warehouse.md)

## <a name="Step5"></a>Start backing up your data

You can now start backing up data to your backup server.

To back up data, use a query client to connect to SQL Server PDW and then submit `BACKUP DATABASE OR RESTORE DATABASE` commands. Use the `DISK=` clause to specify the backup server and backup location.

> [!IMPORTANT]
> Remember to use the InfiniBand IP address of the backup server. Otherwise, the data is copied over Ethernet instead of InfiniBand.

For example:

```sql
BACKUP DATABASE Invoices TO DISK = '\\10.172.14.255\backups\yearly\Invoices2013Full';

RESTORE DATABASE Invoices2013Full
FROM DISK = '\\10.172.14.255\backups\yearly\Invoices2013Full'
```

For more information, see:

- [BACKUP DATABASE](../t-sql/statements/backup-transact-sql.md?view=aps-pdw-2016&preserve-view=true)
- [RESTORE DATABASE](../t-sql/statements/restore-statements-transact-sql.md?view=aps-pdw-2016&preserve-view=true)

## <a name="Security"></a>Security notices

The backup server isn't joined to the private domain for the appliance. It is in your own network. There's no trust relationship between your own domain and private appliance domain.

Since PDW backups aren't stored on the appliance, your IT team is responsible for managing all aspects of the backup security. For example, these aspects include managing the security of the backup data, the security of the server used to store backups, and the security of the networking infrastructure that connects the backup server to the APS appliance.

### Manage network credentials

Network access to the backup directory is based on standard Windows file sharing security. Before performing a backup, you need to create or designate a Windows account that is used for authenticating PDW to the backup directory. This Windows account must have permission to access, create, and write to the backup directory.

> [!IMPORTANT]
>
> To reduce security risks with your data, we advise that you designate one Windows account solely for the purpose of performing backup and restore operations. Allow this account to have permissions to the backup location and nowhere else.

To store the user name and password in PDW, use the [sp_pdw_add_network_credentials](../relational-databases/system-stored-procedures/sp-pdw-add-network-credentials-sql-data-warehouse.md) stored procedure. PDW uses Windows Credential Manager to store and encrypt user names and passwords on the Control node and Compute nodes. The credentials aren't backed up with the `BACKUP DATABASE` command.

To remove network credentials from PDW, use the [sp_pdw_remove_network_credentials](../relational-databases/system-stored-procedures/sp-pdw-remove-network-credentials-sql-data-warehouse.md) stored procedure.

To list all of the network credentials stored in SQL Server PDW, use the [sys.dm_pdw_network_credentials](../relational-databases/system-dynamic-management-views/sys-dm-pdw-network-credentials-transact-sql.md) dynamic management view.

### Secure communications

Operations on the loading server can use a UNC path to pull data from outside the trusted internal network. An attacker on the network or with ability to influence name resolution can intercept or modify data sent to the PDW. This situation presents a tampering and information disclosure risk. To help mitigate the risk of tampering:

- Require signing on the connection.
- On the loading server, set the following group policy option in *Security Settings\Local Policies\Security Options*:

  Microsoft network client: **Digitally sign communications (always)**: *Enabled*.

## Next steps

[Backup and restore](backup-and-restore-overview.md)
