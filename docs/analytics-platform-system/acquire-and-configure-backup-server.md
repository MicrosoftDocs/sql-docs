---
title: Acquire and configure a backup server - Parallel Data Warehouse | Microsoft Docs
description: This article describes how to configure a non-appliance Windows system as a backup server for use with the backup and restore features in Analytics Platform System (APS) and Parallel Data Warehouse (PDW). 
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
---

# Acquire and configure a backup server for Parallel Data Warehouse
This article describes how to configure a non-appliance Windows system as a backup server for use with the backup and restore features in Analytics Platform System (APS) and Parallel Data Warehouse (PDW).  
  
  
## <a name="Basics"></a>Backup server basics  
The backup server:  
  
-   Is provided and managed by your own IT team.  
  
-   Does not require any PDW-specific software or tools. PDW does not install any software onto the backup server.  
  
-   Is located in your own non-appliance rack, and cannot be placed within the APS appliance.  
  
-   Can be connected to the appliance InfiniBand network. Backups can be performed over InfiniBand or Ethernet; InfiniBand is recommended for performance reasons.  
  
-   Is in your own customer domain, not the appliance domain. There is no trust relationship between your customer domain and the appliance domain.  
  
-   Hosts a backup file share, which is a Windows file share that uses the Server Message Block (SMB) application-level network protocol. The backup file share permissions give a Windows domain user (usually this is a dedicated backup user) the ability to perform backup and restore operations on the share. The username and password credentials of the Windows domain user are stored in PDW so that PDW can perform backup and restore operations on the backup file share.  
  
## <a name="Step1"></a>Step 1: Determine Capacity Requirements  
The system requirements for the Backup server depend almost completely on your own workload. Before purchasing or provisioning a backup server you need to figure out your capacity requirements. The Backup server does not have to be dedicated only to backups, as long as it will handle the performance and storage requirements of your workload. You can also have multiple backup servers in order to backup and restore each database to one of several servers.  
  
Use the [Backup server capacity planning worksheet](backup-capacity-planning-worksheet.md) to help determine your capacity requirements.  
  
## <a name="Step2"></a>Step 2: Acquire the backup server  
Now that you better understand your capacity requirements, you can plan the servers and networking components that you will need to purchase or provision. Incorporate the following list of requirements into your purchasing plan, and then purchase your server or provision an existing server.  
  
### Software requirements  
Any file server that uses the Windows File Sharing (SMB) protocol.  
  
We recommend Windows Server 2012 or beyond in order to:  
  
-   Get the performance benefit of file pre-allocation over SMB.  
  
-   Use Instant File Initialization (IFI) for backup operations. Your IT team manages this setting on the backup server. The PDW Configuration Manager (dwconfig.exe) does not set or control IFI on your backup server. Previous versions of Windows do not have IFI, but can still be used as backup servers.  
  
### Networking requirements  
Although not required, InfiniBand is the recommended connection type for Backup servers. To prepare for connecting the loading server to the appliance InfiniBand network:  
  
1.  Plan to rack the server close enough to the appliance so that you can connect it to the appliance InfiniBand switches. For more information from Mellanox Technologies about InfiniBand, see the whitepaper, [Introduction to InfiniBand](https://www.mellanox.com/pdf/whitepapers/IB_Intro_WP_190.pdf).  
  
2.  Purchase a Mellanox ConnectX-3 FDR InfiniBand single or dual port network adapter. We recommend purchasing the network adapter with two ports for fault tolerance during data transmission. A two port network adapter is required for high availability.  
  
3.  Purchase 2 FDR InfiniBand cables for a dual port card, or 1 FDR InfiniBand cable for a single port card. The FDR InfiniBand cables will connect the loading server to the appliance InfiniBand network. The cable length depends on the distance between the loading server and the appliance InfiniBand switches, according to your environment.  
  
## <a name="Step3"></a>Step 3: Connect the server to the InfiniBand networks  
Use these steps to connect the loading server to the InfiniBand network. If the server is not using the InfiniBand network, skip this step.  
  
1.  Rack the server close enough to the appliance so that you can connect it to the appliance InfiniBand network.  
  
2.  Install the InfiniBand Mellanox ConnectX-3 FDR InfiniBand network adapter into the loading server.  
  
3.  Use the FDR cables to connect the InfiniBand network adapter to one of the two InfiniBand switches in the first appliance rack.  
  
4.  Install and configure the appropriate Windows driver for the InfiniBand network adapter.  
  
    -   InfiniBand drivers for Windows are developed by the OpenFabrics Alliance, an industry consortium of InfiniBand vendors.  The correct driver may have been distributed with your InfiniBand network adapter. If not, the driver can be downloaded from www.openfabrics.org.  
  
5.  Configure the InfiniBand and DNS settings for the network adapters. For configuration instructions, see [Configure InfiniBand Network Adapters](configure-infiniband-network-adapters.md).  
  
## <a name="Step4"></a>Step 4: Configure the backup file share  
PDW will access the backup server through a UNC file share. To set up the file share:  
  
1.  Create a folder on the backup server for storing your backups.  
  
2.  Create a file share, called a backup share, for the backup folder.  
  
3.  Designate or create a Windows domain account in your customer domain that you want to use for the purposes of performing backups and restores. For security reasons, it is best to use a dedicated account as the backup user.  
  
4.  Add permissions to the backup share so that only trusted accounts and a domain backup account can access, read, and write to the share location.  
  
5.  Add the backup domain account credentials to PDW.  
  
    For example:  
  
    ```sql  
    EXEC sp_pdw_add_network_credentials '10.192.147.63', 'seattle\david', '********';  
    ```  
  
    For more information, see these stored procedures:  
  
    -   [sp_pdw_add_network_credentials](../relational-databases/system-stored-procedures/sp-pdw-add-network-credentials-sql-data-warehouse.md)  
  
    -   [sp_pdw_remove_network_credentials](../relational-databases/system-stored-procedures/sp-pdw-remove-network-credentials-sql-data-warehouse.md)  
  
## <a name="Step5"></a>Step 5: Start backing up your data  
You are now ready to start backing up data to your backup server.  
  
To backup data, use a query client to connect to SQL Server PDW and then submit BACKUP DATABASE OR RESTORE DATABASE commands. Use the DISK= clause to specify the Backup server and backup location.  
  
> [!IMPORTANT]  
> Remember to use the InfiniBand IP address of the backup server. Otherwise, the data will be copied over Ethernet instead of InfiniBand.  
  
For example:  
  
```sql  
BACKUP DATABASE Invoices TO DISK = '\\10.172.14.255\backups\yearly\Invoices2013Full';  
  
RESTORE DATABASE Invoices2013Full  
FROM DISK = '\\10.172.14.255\backups\yearly\Invoices2013Full'  
```  
  
For more information, see: 
  
-   [BACKUP DATABASE](../t-sql/statements/backup-database-parallel-data-warehouse.md)   
  
-   [RESTORE DATABASE](../t-sql/statements/restore-database-parallel-data-warehouse.md)  
  
## <a name="Security"></a>Security notices  
The backup server is not joined to the private domain for the appliance. It is in your own network, and there is no trust relationship between your own domain and private appliance domain.  
  
Since PDW backups are not stored on the appliance, your IT team is responsible for managing all aspects of the backup security. For example, this includes managing the security of the backup data, the security of the server used to store backups, and the security of the networking infrastructure that connects the backup server to the APS appliance.  
  
### Manage network credentials  
  
Network access to the backup directory is based on standard Windows file sharing security. Before performing a backup, you need to create or designate a Windows account that will be used for authenticating PDW to the backup directory. This windows account must have permission to access, create, and write to the backup directory.  
  
> [!IMPORTANT]  
> To reduce security risks with your data, we advise that you designate one Windows account solely for the purpose of performing backup and restore operations. Allow this account to have permissions to the backup location and nowhere else.  
  
To store the user name and password in PDW, use the [sp_pdw_add_network_credentials](../relational-databases/system-stored-procedures/sp-pdw-add-network-credentials-sql-data-warehouse.md) stored procedure. PDW uses Windows Credential Manager to store and encrypt user names and passwords on the Control node and Compute nodes. The credentials are not backed up with the BACKUP DATABASE command.  
  
To remove network credentials from PDW, use the [sp_pdw_remove_network_credentials](../relational-databases/system-stored-procedures/sp-pdw-remove-network-credentials-sql-data-warehouse.md) stored procedure.  
  
To list all of the network credentials stored in SQL Server PDW, use the [sys.dm_pdw_network_credentials](../relational-databases/system-dynamic-management-views/sys-dm-pdw-network-credentials-transact-sql.md) dynamic management view.  
  
### Secure communications  
  
Operations on the loading server can use a UNC path to pull data from outside the trusted internal network. An attacker on the network or with ability to influence name resolution can intercept or modify data sent to the PDW. This presents a tampering and information disclosure risk. To help mitigate the risk of tampering:

- Require signing on the connection. 
- On the loading server, set the following group policy option in Security Settings\Local Policies\Security Options:  Microsoft network client: Digitally sign communications (always): Enabled.  
  
## See Also  
[Backup and restore](backup-and-restore-overview.md)  
  
