---
title: "Upgrade DQS Databases Schema After Installing SQL Server Update | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "data-quality-services"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
ms.assetid: c8f3fbae-02c4-464d-a35c-7108f48c58cb
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Upgrade DQS Databases Schema After Installing SQL Server Update

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  After you have installed a SQL Server update (patch, hotfix, or cumulative update) on a previously configured DQS instance, you might have to upgrade the DQS databases schema by running the DQSInstaller.exe file with the **upgrade** command line parameter. Otherwise, you might receive the following error while trying to connect to Data Quality Server using your Data Quality Client:  
  
```  
An error occurred in the Microsoft .NET Framework while trying to load assembly id 65581.  
```  
  
 Upgrading DQS databases schema does not impact your existing data in the DQS databases (knowledge bases, data quality projects, and exported results in the DQS_STAGING_DATA database). However, you must back up your DQS databases before upgrading DQS databases schema to prevent any accidental data loss during the schema upgrade. For information about backing up DQS databases, see [Backing Up and Restoring DQS Databases](../../data-quality-services/backing-up-and-restoring-dqs-databases.md).  
  
> [!NOTE]  
>  Most of the SQL Server updates will require an upgrade to the DQS databases schema. For information about the SQL Server updates that will require an upgrade to the DQS databases schema, see the chart in step 1.A in [Upgrade DQS: Installing Cumulative Updates or Hotfix Patches on Data Quality Services](https://go.microsoft.com/fwlink/?LinkID=251565).  
  
## Prerequisites  
  
-   You must be logged on as a member of the Administrators group on the [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] computer.  
  
-   Your Windows user account must be a member of the sysadmin fixed server role in the SQL Server instance where [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] is installed.  
  
### To upgrade DQS databases schema  
  
1.  (Recommended) Back up your DQS databases before you proceed with the schema upgrade. For information about backing up DQS databases, see [Backing Up and Restoring DQS Databases](../../data-quality-services/backing-up-and-restoring-dqs-databases.md).  
  
2.  Start Command Prompt.  
  
3.  At the command prompt, change your directory to the location where DQSInstaller.exe is available. If you installed the default instance of SQL Server, the DQSInstaller.exe file will be available at C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Binn:  
  
    ```  
    cd C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Binn  
    ```  
  
4.  At the command prompt, type the following command, and press ENTER:  
  
    ```  
    dqsinstaller.exe -upgrade  
    ```  
  
5.  The installer prompts you for backing up the DQS databases before proceeding. If you have already backed up your DQS databases, type **Y** or **Yes** and press ENTER to continue with the upgrade.  
  
6.  A completion message is displayed after successful upgrade of the DQS databases schema.  
  
## Next Steps  
 Log on to the upgraded Data Quality Server from a Data Quality Client application.  
  
 For more information about upgrading DQS databases schema after installing SQL Server updates and associated troubleshooting steps, see [Upgrade DQS: Installing Cumulative Updates or Hotfix Patches on Data Quality Services](https://go.microsoft.com/fwlink/?LinkID=251565).  
  
## See Also  
 [Install Data Quality Services](../../data-quality-services/install-windows/install-data-quality-services.md)   
 [Upgrade SQLCLR Assemblies After .NET Framework Update](../../data-quality-services/install-windows/upgrade-sqlclr-assemblies-after-net-framework-update.md)  
  
  
