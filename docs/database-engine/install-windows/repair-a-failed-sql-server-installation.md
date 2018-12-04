---
title: "Repair a Failed SQL Server Installation | Microsoft Docs"
ms.custom: ""
ms.date: "09/08/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
ms.assetid: 90c11b28-892b-44d6-978e-0eee48c75b7d
author: MashaMSFT
ms.author: mathoma
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
manager: craigg
---
# Repair a Failed SQL Server Installation

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Repair operation can be used in the following scenarios:  
  
- Repair an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is corrupted after it was successfully installed. 
  
- Repair an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] if the upgrade operation is cancelled or fails after the instance name is mapped to the newly-upgraded instance. 
  
    - If you see the following message in the summary log, you can repair the failed upgrade instance:  
  
         "[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] upgrade failed. To continue, investigate the reason for the failure, correct the problem, and then repair your installation."  
  
    - If you see the following message in the summary log, you need to uninstall and reinstall [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You cannot repair the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. 
  
         "[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] upgrade failed. To continue, investigate the reason for the failure, correct the problem."  
  
 When you repair an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]:  
  
- All missing or corrupt files are replaced. 
  
- All missing or corrupt registry keys are replaced. 
  
- All missing or invalid configuration values are set to default values. 
  
 Before you continue, for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover clusters, review the following important information:  
  
- Repair must be run on individual cluster nodes. 
  
- To repair a failover cluster node after a failed Prepare operation, use **Remove node** and then perform the Prepare step again. For more information, see [Add or Remove Nodes in a SQL Server Failover Cluster &#40;Setup&#41;](../../sql-server/failover-clusters/install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md). 
  
## Repair a failed installation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from the Installation Center 
  
1. Launch the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup program (setup.exe) from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation media. 
  
2. After prerequisites and system verification, the Setup program will display the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Installation Center page. 
  
3. Click **Maintenance** in the left-hand navigation area, and then click **Repair** to start the repair operation. 
  
   >[!TIP]  
   > If the Installation Center was launched using the start menu, you will need to provide the location of the installation media at this time. 
  
4. Setup support rule and file routines will run to ensure that your system has prerequisites installed and that the computer passes Setup validation rules. Click **OK** or **Install** to continue. 
  
5. On the Select Instance page, select the instance to repair, and then click **Next** to continue. 
  
6. The repair rules will run to validate the operation. To continue, click **Next**. 
  
7. The Ready to Repair page indicates that the operation is ready to proceed. To continue, click **Repair**. 
  
8. The Repair Progress page shows the status of the repair operation. The Complete page indicates that the operation is finished. 
  
### To repair a failed installation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using Command Prompt  
  
1. Run the following command at a command prompt:  
  
    ```  
    Setup.exe /q /ACTION=Repair /INSTANCENAME=instancename  
    ```  
  
## See Also  
 [View and Read SQL Server Setup Log Files](../../database-engine/install-windows/view-and-read-sql-server-setup-log-files.md)   
 [Installation How-to articles](https://msdn.microsoft.com/library/59de41e7-557f-462a-8914-53ec35496baa)  
  
  
