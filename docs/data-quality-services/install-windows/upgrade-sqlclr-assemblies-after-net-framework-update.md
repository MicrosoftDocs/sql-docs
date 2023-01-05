---
title: "Upgrade SQLCLR assemblies after .NET Framework update"
description: Learn how to update the SQLCLR assemblies used by SQL Server Data Quality Services (DQS) after you've updated the .NET framework.
author: swinarko
ms.author: sawinark
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: data-quality-services
ms.topic: conceptual
ms.custom: seo-lt-2019
---
# Upgrade SQLCLR assemblies after .NET framework update

[!INCLUDE [SQL Server - Windows only ](../../includes/applies-to-version/sql-windows-only.md)]

  [!INCLUDE[ssDQSnoversion](../../includes/ssdqsnoversion-md.md)] (DQS) is a collection of SQL Common Language Runtime (SQLCR) routines that reference Microsoft .NET Framework 4 assemblies. When you install any .NET Framework updates on your computer that affect any such referenced .NET Framework assembly, it leads to a change in the Module Version ID (MVID) of the assembly in the Global Assembly Cache (GAC). This causes a mismatch between the MVIDs of the referenced assembly in GAC and the assembly in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)].  
  
 If the .NET Framework update requires you to restart the [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] computer, the affected SQLCLR assemblies are upgraded automatically to fix the MVID mismatch issue on restarting the [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] computer. However, for .NET Framework updates that do not require you to restart your [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] computer, an error occurs due to the mismatch in the MVIDs of the assemblies when you try to connect to a [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] using a [!INCLUDE[ssDQSClient](../../includes/ssdqsclient-md.md)]:  
  
```  
A new version of .NET was installed on this machine. In order to continue to work with DQS please run dqsinstaller.exe -upgradedlls.  
```  
  
 To fix this issue, the affected SQLCLR assemblies in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] must be upgraded. You can do so by running the DQSInstaller.exe file with the **upgradedlls** command line parameter to skip recreating the DQS databases, and just upgrade the affected assemblies. This ensures that your knowledge bases, data quality projects, and any other data in DQS are preserved.  
  
## Prerequisites  
  
-   You must be logged on as a member of the Administrators group on the [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] computer.  
  
-   Your Windows user account must be a member of the sysadmin fixed server role in the SQL Server instance where [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] is installed.  
  
### To upgrade SQLCLR Assemblies  
  
1.  Start Command Prompt.  
  
2.  At the command prompt, change your directory to the location where DQSInstaller.exe is available. If you installed the default instance of SQL Server, the DQSInstaller.exe file will be available at C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Binn:  
  
    ```  
    cd C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Binn  
    ```  
  
3.  At the command prompt, type the following command, and press ENTER:  
  
    ```  
    dqsinstaller.exe -upgradedlls  
    ```  
  
4.  Rest of the steps are same as steps 2-6 in the [Run DQSInstaller.exe from Start Screen, Start Menu or Windows Explorer](../../data-quality-services/install-windows/run-dqsinstaller-exe-to-complete-data-quality-server-installation.md#WindowsExplorer) section in [Run DQSInstaller.exe to Complete Data Quality Server Installation](../../data-quality-services/install-windows/run-dqsinstaller-exe-to-complete-data-quality-server-installation.md).  
  
## See Also  
 [Install Data Quality Services](../../data-quality-services/install-windows/install-data-quality-services.md)   
 [Upgrade DQS Databases Schema After Installing SQL Server Update](../../data-quality-services/install-windows/upgrade-dqs-databases-schema-after-installing-sql-server-update.md)  
  
  
