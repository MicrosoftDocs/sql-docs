---
title: "Remove Data Quality Server Objects | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
ms.assetid: 1b7c6dbb-b40e-4822-9caa-608e1056af8e
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Remove Data Quality Server Objects
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  Uninstalling [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] from an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], or completely removing an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that has [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] does not delete some [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] objects, including the DQS databases. This implies that you do not lose your DQS data if you uninstall [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] using the SQL Server setup. You must manually delete these [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] objects after the uninstall process is complete.  
  
> [!NOTE]
>  -   Before uninstalling [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)], consider backing up all your existing knowledge bases by exporting it to a .dqsb file, and use the file later to import all the knowledge bases back to a new [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] installation. Exporting and importing of all DQS knowledge bases can only be done by running DQSInstaller.exe with appropriate command line parameters from the command prompt. For more information, see [Export and Import DQS Knowledge Bases Using DQSInstaller.exe](../../data-quality-services/install-windows/export-and-import-dqs-knowledge-bases-using-dqsinstaller-exe.md).  
> -   Before deleting the DQS databases, consider backing up the databases if you want to preserve it, and use it later for restoring the data. For information about doing so, see [Manage DQS Databases](../../data-quality-services/manage-dqs-databases.md).  
  
## Uninstall Data Quality Server from a SQL Server Instance  
 If you are just uninstalling [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] from an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you must manually delete the following [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] objects from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance after the [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] uninstall process is complete:  
  
-   DQS_MAIN, DQS_PROJECTS, and DQS_STAGING_DATA databases.  
  
-   \##MS_dqs_db_owner_login## and ##MS_dqs_service_login## logins.  
  
-   DQInitDQS_MAIN stored procedure from the master database.  
  
 You can delete these objects in SQL Server Management Studio by right-clicking the object, and clicking **Delete** in the shortcut menu.  
  
> [!IMPORTANT]  
>  If you just uninstall [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] from a SQL server instance using the `-uninstall` command line parameter from the command prompt, all the DQS objects are deleted as part of the uninstall process. You do not have to delete them manually after uninstalling [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)]. To uninstall [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] from command prompt, type the following command at the command prompt, and press ENTER:   
> `dqsinstaller.exe -uninstall`  
  
## Uninstall SQL Server Instance Containing Data Quality Server  
 If you are completely uninstalling the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that has [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)], you must manually delete the DQS_MAIN, DQS_PROJECTS, and DQS_STAGING_DATA databases from your computer after the uninstall process is complete. For a default [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation, the DQS_MAIN, DQS_PROJECTS, and DQS_STAGING_DATA databases files are available at C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA.  
  
## See Also  
 [Uninstall an Existing Instance of SQL Server &#40;Setup&#41;](../../sql-server/install/uninstall-an-existing-instance-of-sql-server-setup.md)   
 [Uninstall SQL Server 2016](../../sql-server/install/uninstall-sql-server.md)  
  
  
