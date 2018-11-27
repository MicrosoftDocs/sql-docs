---
title: "Upgrade Master Data Services | Microsoft Docs"
ms.custom: ""
ms.date: "07/21/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: 9c3543f3-3eb9-455d-a9bf-f17e9506ad21
author: MashaMSFT
ms.author: mathoma
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
manager: craigg
---
# Upgrade Master Data Services

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]
  
  The following are the scenarios for upgrading Microsoft [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] Master Data Services.  
  
-   [Upgrade without Database Engine Upgrade](../../database-engine/install-windows/upgrade-master-data-services.md#noengine)  
  
-   [Upgrade with Database Engine Upgrade](../../database-engine/install-windows/upgrade-master-data-services.md#engine)  
  
-   [Upgrade in Two-Computer Scenario](../../database-engine/install-windows/upgrade-master-data-services.md#twocomputer)  
  
-   [Upgrade with Restoring a Database from Backup](../../database-engine/install-windows/upgrade-master-data-services.md#restore)  
  
> [!IMPORTANT]  
> -   Upgrading from the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] CTP1 release to the CTP2 release is not supported.  
> -   Back up your database before performing any upgrade.  
> -   The upgrade process recreates stored procedures and upgrades tables used by [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)]. Any customizations you have made to either of these components may be lost.  
> -   Model deployment packages can be used only in the edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] they were created in. You cannot deploy model deployment packages created in [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], or [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] to [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)].  
> -   After upgrading Data Quality Services and Master Data Services to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], any earlier version of the Master Data Services Add-In for Excel will no longer work. You can download the [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] Master Data Services Add-In for Excel from [Master Data Services Add-in for Microsoft Excel](../../master-data-services/microsoft-excel-add-in/master-data-services-add-in-for-microsoft-excel.md).  
  
##  <a name="fileLocation"></a> File Location  
  
-   In [!INCLUDE[ss2017](../../includes/sssqlv14-md.md)], by default, the files are installed at *drive*:\Program Files\Microsoft SQL Server\140\Master Data Services.  

-   In [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)], by default, the files are installed at *drive*:\Program Files\Microsoft SQL Server\130\Master Data Services.  
  
-   In [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], by default, the files are installed at *drive*:\Program Files\Microsoft SQL Server\120\Master Data Services.  
  
-   In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], by default, the files are installed at *drive*:\Program Files\Microsoft SQL Server\110\Master Data Services.  
  
-   In [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], by default, the files are installed at *drive*:\Program Files\Microsoft SQL Server\Master Data Services.  
  
##  <a name="noengine"></a> Upgrade without Database Engine Upgrade  
 In this scenario you continue to use [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], or [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] to host your MDS database. However, you must upgrade the schema of the MDS database, and then create a current [!INCLUDE[ssNoversion](../../includes/ssnoversion-md.md)] web application to access the MDS database. After the upgrade, the MDS database can no longer be accessed by the earlier web application.  
  
 You can install the current [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] and an earlier version of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] on the same computer. The files are installed in different locations, as shown in [File Location](#fileLocation).  
  
 **To upgrade without Database Engine upgrade**  
  
1.  Install [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] and any other features you want.  
  
    1.  Open the [!INCLUDE[ssNoversion](../../includes/ssnoversion-md.md)] Setup wizard.  
  
    2.  In the left pane, click **Installation**.  
  
    3.  In the right pane, click **New SQL Server stand-alone installation or add features to an existing installation**.  
  
    4.  On the **Feature Selection** page, select **[!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)]** and any other features you want to install.  
  
    5.  Complete the wizard.  
  
2.  Upgrade the MDS database schema.  
  
    1.  Open the current [!INCLUDE[ssNoversion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)].  
  
        > [!IMPORTANT]  
        >  To upgrade the MDS database schema, you must be logged in as the Administrator Account that was specified when the MDS database was created. In the MDS database, in mdm.tblUser, this user has the **ID** value of **1**.  
  
    2.  In the left pane, click **Database Configuration**.  
  
    3.  In the right pane, click **Select Database** and specify the information for your [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], or [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]  database instance.  
  
    4.  Click **Upgrade Database** to start the **Upgrade Database Wizard**. For more information, see [Upgrade Database Wizard &#40;Master Data Services Configuration Manager&#41;](../../master-data-services/upgrade-database-wizard-master-data-services-configuration-manager.md).  
  
3.  Create a web application.  
  
    1.  Open the current [!INCLUDE[ssNoversion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)].  
  
    2.  In the left pane, click **Web Configuration**.  
  
    3.  In the right pane, from the **Website** list, select one of the following options:  
  
        -   **Default Web Site**, then click **Create Application**.  
  
        -   **Create new site**. A new web application is automatically created when the website is created.  
  
        > [!IMPORTANT]  
        >  Your existing MDS web application from an earlier version of SQL Server ([!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], or [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]) is available for selection in the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of Master Data Services Configuration Manager. You must not select the existing web application, and instead must create a [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] web application for MDS. Otherwise, you will receive an error when you try to associate the web application with the upgraded MDS database stating that the requested page cannot be accessed because the related configuration data for the page is invalid.  
        >   
        >  If you want to use the same name (alias) for MDS web application as your existing ([!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], or [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]) web application, you must first delete the web application and the associated application pool from IIS, and then create a web application with the same name using [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] version of Master Data Services Configuration Manager. For information about removing web application and application pools from IIS, see [Remove an Application (IIS)](https://go.microsoft.com/fwlink/?LinkId=323537) and [Remove an Application Pool (IIS)](https://go.microsoft.com/fwlink/?LinkId=323538).  
  
4.  Associate the new web application with the upgraded MDS database.  
  
    1.  In the **Associate Application with Database** section, click **Select**.  
  
    2.  Select the MDS database.  
  
    3.  Click **Apply**.  
  
##  <a name="engine"></a> Upgrade with Database Engine Upgrade  
 In this scenario you will upgrade both the database engine and [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] application from an earlier version to either [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] or [!INCLUDE[ssSQL16](../../includes/sssqlv14-md.md)].  
  
 **To upgrade with Database Engine upgrade**  
  
1.  **For [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] only**: Open **Control Panel** > **Programs and Features** and uninstall Microsoft [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)][!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)].  
  
2.  Upgrade the database engine to [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] or [!INCLUDE[ssSQL16](../../includes/sssqlv14-md.md)]. For more information, see [Choose a Database Engine Upgrade Method](../../database-engine/install-windows/choose-a-database-engine-upgrade-method.md).  
  
3.  Complete all the steps in [Upgrade without Database Engine Upgrade](#noengine) .  
  
##  <a name="twocomputer"></a> Upgrade in Two-Computer Scenario  
 In this scenario you upgrade a system in which SQL Server is installed on two computers: one with [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] or [!INCLUDE[ssSQL16](../../includes/sssqlv14-md.md)], and the other with an earlier version of [!INCLUDE[ssNoversion](../../includes/ssnoversion-md.md)].  
  
 If an earlier version of [!INCLUDE[ssNoversion](../../includes/ssnoversion-md.md)] is installed, you continue to use the earlier version to host your MDS database on one computer. However, you must upgrade the schema of the MDS database, and then use the [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] or [!INCLUDE[ssSQL16](../../includes/sssqlv14-md.md)] web application respectively to access the MDS database. The MDS database can no longer be accessed by the earlier version web application.  
  
 **To upgrade in two-computer scenario**  
  
-   Complete all the steps in [Upgrade without Database Engine Upgrade](#noengine).  
  
##  <a name="restore"></a> Upgrade with Restoring a Database from Backup  
 In this scenario, either [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] or [!INCLUDE[ssSQL16](../../includes/sssqlv14-md.md)] is installed along with an earlier version on the same computer or two different computers. A database was backed up on a version earlier than the [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] or [!INCLUDE[ssSQL16](../../includes/sssqlv14-md.md)] release, prior to upgrade, and the database has to be restored.  
  
 **To upgrade with restoring a database from backup**  
  
1.  Install [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] and any other features you want.  
  
    1.  Open the [!INCLUDE[sssnoversion](../../includes/ssnoversion-md.md)] Setup wizard.  
  
    2.  In the left pane, click **Installation**.  
  
    3.  In the right pane, click **New SQL Server stand-alone installation or add features to an existing installation**.  
  
    4.  On the **Feature Selection** page, select **[!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)]** and any other features you want to install.  
  
    5.  Complete the wizard.  
  
2.  Restore the database that was backed up.  
  
3.  Upgrade the MDS database schema, create a web application, and associate the new web application with the upgraded MDS database. For the instructions, see steps 2 - 4 in [Upgrade without Database Engine Upgrade](#noengine)  
  
## Troubleshooting  
 **Issue:** When you open the [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], or [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] web application, a "client version is not compatible with the database version" error message is displayed.  
  
 **Solution:** This issue occurs when a [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)], or [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)] Master Data Manager web application tries to access a database that has been upgraded to  or [!INCLUDE[ssSQL16](../../includes/sssqlv14-md.md)] Master Data Services. You must use a  or [!INCLUDE[ssSQL16](../../includes/sssqlv14-md.md)] web application instead.  
  
 This issue may also occur if you did not stop and restart the **MDS Application Pool** in IIS when upgrading the MDS database schema. Restart the **MDS Application Pool** to correct the issue.  
  
## See Also  
 [Install Master Data Services](../../master-data-services/install-windows/install-master-data-services.md)  
  
  
