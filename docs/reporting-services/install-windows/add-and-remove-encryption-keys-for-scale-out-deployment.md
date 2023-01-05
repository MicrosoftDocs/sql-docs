---
description: "Add and Remove Encryption Keys for Scale-Out Deployment"
title: "Add and Remove Encryption Keys for Scale-Out Deployment | Microsoft Docs"
ms.date: 05/31/2016
ms.service: reporting-services

ms.topic: conceptual
helpviewer_keywords: 
  - "encryption keys [Reporting Services]"
  - "deleting encryption keys"
  - "removing encryption keys"
  - "adding encryption keys"
  - "rskeymgmt utility"
  - "scale-out deployments [Reporting Services]"
ms.assetid: 2da86fb3-4b4d-407f-9825-74dcc42486f5
author: maggiesMSFT
ms.author: maggies
---
# Add and Remove Encryption Keys for Scale-Out Deployment
  You can run [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] in a scale-out deployment model by configuring multiple report servers to use a shared report server database. Membership in a scale-out deployment is based on whether the report server stores an encryption key in the report server database. You can control scale-out deployment membership by adding and removing encryption keys for specific report server instances. If you are removing nodes from the deployment, you can remove them in any order. If you are adding nodes to a deployment, you must join any new instances from a report server that is already part of the deployment.  
  
## Using the Reporting Services Configuration Tool to Configure Scale-Out Deployment  
 The easiest way to configure a scale-out deployment is to use the Reporting Services Configuration tool. For more information and step-by-step instructions, see [Configure a Native Mode Report Server Scale-Out Deployment &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-native-mode-report-server-scale-out-deployment.md).  
  
## Using Rskeymgmt to Configure Scale-Out Deployment  
 Use the **rskeymgmt** utility to initialize a report server instance to use a shared report server database. Adding a report server to a scale-out deployment requires that you initialize the report server. Initialization requires administrator permissions. You must have administrator credentials for the remote computer that hosts the report server you are joining to the deployment.  
  
### How to join a report server to a scale-out deployment (rskeymgmt)  
  
1.  Run **rskeymgmt.exe** locally on the computer that hosts a report server that is already a member of the report server scale-out deployment.  
  
2.  Use the **-j** argument to join a report server to the report server database. Use the **-m** and **-n** arguments to specify the remote report server instance you want to add to the deployment. Use the **-u** and **-v** arguments to specify an administrator account on the remote computer. If you are creating a scale-out deployment using multiple report server instances on the same computer, the syntax to use is slightly different. For more information about the syntax you should use, see [rskeymgmt Utility &#40;SSRS&#41;](../../reporting-services/tools/rskeymgmt-utility-ssrs.md).  
  
     The following example illustrates the arguments you must specify if you are joining a remote report server to a scale-out deployment (you can omit credentials if you have administrator permissions on the remote computer):  
  
    ```  
    rskeymgmt -j -m <remotecomputer> -n <namedreportserverinstance> -u <administratoraccount> -v <administratorpassword>  
    ```
3. Restart the Reporting Services Windows Service.
  
### How to remove a report server from a scale-out deployment (rskeymgmt)  
  
1.  Open the rsreportserver.config file of the report server you want to remove and find the installation ID. By default, this file is located at Program Files\Microsoft SQL Server\MSSQL.*n*\Reporting Services\ReportServer).  
  
     If you installed a single instance, there will only be one rsreportserver.config file on the computer. If multiple instances of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] are installed, use the Server Status page in the Reporting Services Configuration tool to find the instance identifier (for example, MSSQL.2) for the report server that you want to remove. The name of the folder that stores the program files for the report server instance will be based on the instance identifier (for example, Program Files\Microsoft SQL Server\MSSQL.2).  
  
2.  Run **rskeymgmt.exe**. You can run it on any report server that is part of the report server scale-out deployment.  
  
3.  Use the **-r** argument to release the report server instance from the scale-out deployment. The following example illustrates the arguments you must specify:  
  
    ```  
    rskeymgmt -r <installation ID>  
    ```  
4. Restart the Reporting Services Windows Service.
  
 These steps remove the report server from a scale-out deployment, but they do not uninstall the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance on the report server. After you remove the report server from the scale-out deployment, you can uninstall [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] from the server if you no longer need [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] on that server. For information, see [Uninstall an Existing Instance of SQL Server &#40;Setup&#41;](../../sql-server/install/uninstall-an-existing-instance-of-sql-server-setup.md)
  
## See Also  
 [Configure and Manage Encryption Keys &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-manage-encryption-keys.md)   
 [Initialize a Report Server &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-encryption-keys-initialize-a-report-server.md)  
  
  
