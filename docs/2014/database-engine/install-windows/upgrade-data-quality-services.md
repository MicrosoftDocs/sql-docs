---
title: "Upgrade Data Quality Services | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
ms.assetid: f396666b-7754-4efc-9507-0fd114cc32d5
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Upgrade Data Quality Services
  This topic provides information on how to upgrade your existing installation of Data Quality Services (DQS) to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] CTP2. As part of upgrading your Data Quality Server in DQS to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], you must also upgrade the DQS databases schema.  
  
> [!IMPORTANT]
>  -   You must back up your DQS databases before upgrading DQS to prevent any accidental data loss during the schema upgrade. For information about backing up DQS databases, see [Backing Up and Restoring DQS Databases](../../data-quality-services/backing-up-and-restoring-dqs-databases.md).  
> -   You can connect to the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of Data Quality Server by using the current or an earlier version of Data Quality Client or the [DQS Cleansing Transformation](../../integration-services/data-flow/transformations/dqs-cleansing-transformation.md) in Integration Services to perform your data quality tasks.  
> -   You can continue using the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 version of Master Data Services Add-In for Excel after upgrading Data Quality Services and Master Data Services to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] CTP2. However, any earlier version of the Master Data Services Add-In for Excel will not work after upgrading to SQL Server 2014 CTP2. You can download the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 version of Master Data Services Add-In for Excel from [here](https://go.microsoft.com/fwlink/?LinkId=328664).  
  
##  <a name="Prerequisites"></a> Prerequisites  
  
-   You must be logged on as a member of the Administrators group on the [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] computer.  
  
-   Your Windows user account must be a member of the sysadmin fixed server role in the SQL Server instance where [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] is installed.  
  
##  <a name="Upgrade"></a> Upgrading DQS  
 To upgrade DQS:  
  
1.  Back up your DQS databases before you start the upgrade process. For information about backing up DQS databases, see [Backing Up and Restoring DQS Databases](../../data-quality-services/backing-up-and-restoring-dqs-databases.md).  
  
2.  Upgrade the SQL Server instance where DQS is installed to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
    1.  Run the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Setup wizard.  
  
    2.  In the left pane, click **Installation**.  
  
    3.  In the right pane, click **Upgrade from SQL Server 2005, SQL Server 2008, SQL Server 2008 R2 or SQL Server 2012**.  
  
    4.  Complete the Setup wizard.  
  
        > [!NOTE]  
        >  This will upgrade your SQL Server instance to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and also install the latest Data Quality Client, if Data Quality Client was previously installed on this computer. If you have Data Quality Client installed on other computers, you must run the sub steps in Step 2 on those computers to install the current version of Data Quality Client. The Setup wizard installs the current version of Data Quality Client alongside the existing version of Data Quality Client. After you upgrade the DQS databases schema, you can connect to the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of Data Quality Server by using the current or an earlier version of Data Quality Client.  
  
3.  Upgrade the DQS databases schema.  
  
    1.  Start Command Prompt as an administrator.  
  
    2.  At the command prompt, change your directory to the location where DQSInstaller.exe is available. For the default instance of SQL Server, the DQSInstaller.exe file is available at C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Binn:  
  
        ```  
        cd C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\Binn  
        ```  
  
    3.  At the command prompt, type the following command, and press ENTER:  
  
        ```  
        dqsinstaller.exe -upgrade  
        ```  
  
    4.  The installer prompts you for backing up the DQS databases before proceeding. If you have already backed up your DQS databases, type `Y` or `Yes`, and then press ENTER to continue with the upgrade.  
  
    5.  A completion message is displayed after successful upgrade of the DQS databases schema.  
  
##  <a name="Verify"></a> Verifying the DQS Databases Schema Upgrade  
 To verify that DQS databases schema have successfully upgraded, you can check the current version in the DQS_MAIN and DQS_PROJECTS databases by querying the A_DB_VERSION table in each database. To do so:  
  
1.  Start SQL Server Management Studio, and connect to the SQL Server instance that contains the upgraded DQS databases schema.  
  
2.  Run the following query:  
  
    ```  
    SELECT * FROM DQS_MAIN.dbo.A_DB_VERSION WHERE STATUS=2;  
    SELECT * FROM DQS_PROJECTS.dbo.A_DB_VERSION WHERE STATUS=2;  
    ```  
  
3.  The output will display an entry for each upgrade along with the date of the upgrade. The maximum VERSION_ID and ASSEMBLY_VERSION on the latest date is the current version. A value of 2 in the STATUS column indicates success. If an error has occurred, the error will be listed in the ERROR column. A sample output:  
  
    |ID|UPGRADE_DATE|VERSION_ID|ASSEMBLY_VERSION|USER_NAME|STATUS|ERROR|  
    |--------|-------------------|-----------------|-----------------------|----------------|------------|-----------|  
    |1000|2013-08-11 05:26:39.567|1200|11.0.3000.0|\<DOMAIN\UserName>|2||  
    |1001|2013-09-19 15:09:37.750|1600|12.0.xxxx.0|\<DOMAIN\UserName>|2||  
  
## See Also  
 [Install Data Quality Services](../../data-quality-services/install-windows/install-data-quality-services.md)   
 [Remove Data Quality Server Objects](../../sql-server/install/remove-data-quality-server-objects.md)   
 [Upgrade to SQL Server 2014](upgrade-sql-server.md)  
  
  
