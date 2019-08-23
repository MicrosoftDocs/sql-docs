---
title: "Upgrade Master Data Services | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: master-data-services
ms.topic: conceptual
ms.assetid: 9c3543f3-3eb9-455d-a9bf-f17e9506ad21
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Upgrade Master Data Services
  There are four scenarios for upgrading to Microsoft [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] CTP2. Choose the scenario that fits your situation.  
  
-   [Upgrade without Database Engine Upgrade](#noengine)  
  
-   [Upgrade with Database Engine Upgrade](#engine)  
  
-   [Upgrade in Two-Computer Scenario](#twocomputer)  
  
-   [Upgrade with Restoring a Database from Backup](#restore)  
  
> [!IMPORTANT]
>  -   Upgrading from the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] CTP1 release to the CTP2 release is not supported.  
> -   Back up your database before performing any upgrade.  
> -   The upgrade process recreates stored procedures and upgrades tables used by [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)]. Any customizations you have made to either of these components may be lost.  
> -   Model deployment packages can be used only in the edition of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] they were created in. You cannot deploy model deployment packages created in [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]/[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
> -   You can continue using the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 version of Master Data Services Add-In for Excel after upgrading Master Data Services and Data Quality Services to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] CTP2. However, any earlier version of the Master Data Services Add-In for Excel will not work after upgrading to SQL Server 2014 CTP2. You can download the [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] SP1 version of Master Data Services Add-In for Excel from [here](https://go.microsoft.com/fwlink/?LinkId=328664).  
  
##  <a name="noengine"></a> Upgrade without Database Engine Upgrade  
 This scenario can be considered a side-by-side installation, because both [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]/[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] are installed in parallel, on either the same computer or separate computers.  
  
 In this scenario you continue to use [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] to host your MDS database. However, you must upgrade the schema of the MDS database, and then create a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] web application to access the MDS database. The MDS database can no longer be accessed by the [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] web application.  
  
 If you choose to install [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and an earlier version of SQL Server ([!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]/[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]) on the same computer, you can do so because the files are installed in a different location.  
  
-   In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], by default, the files are installed at *drive*:\Program Files\Microsoft SQL Server\120\Master Data Services.  
  
-   In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], by default, the files are installed at *drive*:\Program Files\Microsoft SQL Server\110\Master Data Services.  
  
-   In [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], by default, the files are installed at *drive*:\Program Files\Microsoft SQL Server\Master Data Services.  
  
 To perform this task, complete the following steps.  
  
1.  Install [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] and any other features you want.  
  
    1.  Open the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Setup wizard.  
  
    2.  In the left pane, click **Installation**.  
  
    3.  In the right pane, click **New SQL Server stand-alone installation or add features to an existing installation**.  
  
    4.  On the **Feature Selection** page, select **[!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)]** and any other features you want to install.  
  
    5.  Complete the wizard.  
  
2.  When the installation is complete, upgrade the MDS database schema.  
  
    1.  Open the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)].  
  
        > [!IMPORTANT]  
        >  To upgrade the MDS database schema, you must be logged in as the Administrator Account that was specified when the MDS database was created. In the MDS database, in mdm.tblUser, this user has the **ID** value of **1**. For information on changing this user, see [Change the System Administrator Account &#40;Master Data Services&#41;](../../master-data-services/change-the-system-administrator-account-master-data-services.md).  
  
    2.  In the left pane, click **Database Configuration**.  
  
    3.  In the right pane, click **Select Database** and specify the information for your [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] database instance.  
  
    4.  Click **Upgrade Database** to start the **Upgrade Database Wizard**. For more information, see [Upgrade Database Wizard &#40;Master Data Services Configuration Manager&#41;](../../master-data-services/upgrade-database-wizard-master-data-services-configuration-manager.md).  
  
3.  When the upgrade is complete, create a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] web application.  
  
    1.  Open the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)].  
  
    2.  In the left pane, click **Web Configuration**.  
  
    3.  In the right pane, from the **Website** list, select one of the following options:  
  
        -   **Default Web Site**, then click **Create Application**.  
  
        -   **Create new site**. A new web application is automatically created when the website is created.  
  
        > [!IMPORTANT]  
        >  Your existing MDS web application from an earlier version of SQL Server ([!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]) is available for selection in the SQL Server 2014 version of Master Data Services Configuration Manager. You must not select the existing web application, and instead must create a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] web application for MDS. Otherwise, you will receive an error when you try to associate the web application with the upgraded MDS database stating that the requested page cannot be accessed because the related configuration data for the page is invalid.  
        >   
        >  If you want to use the same name (alias) for MDS web application as your existing ([!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]) web application, you must first delete the web application and the associated application pool from IIS, and then create a web application with the same name using SQL Server 2014 version of Master Data Services Configuration Manager. For information about removing web application and application pools from IIS, see [Remove an Application (IIS)](https://go.microsoft.com/fwlink/?LinkId=323537) and [Remove an Application Pool (IIS)](https://go.microsoft.com/fwlink/?LinkId=323538).  
  
4.  Now associate the new web application with the upgraded MDS database.  
  
    1.  In the **Associate Application with Database** section, click **Select**.  
  
    2.  Select the MDS database.  
  
    3.  Click **Apply**.  
  
##  <a name="engine"></a> Upgrade with Database Engine Upgrade  
 In this scenario you will upgrade both the database engine and [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] application from [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or SQL Server 2012 to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
 To perform this task, complete the following steps.  
  
1.  **For [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] only**: Open **Control Panel** > **Programs and Features** and uninstall Microsoft [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)][!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)].  
  
2.  Upgrade the database engine to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
    1.  Open the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Setup wizard.  
  
    2.  In the left pane, click **Installation**.  
  
    3.  In the right pane, click **Upgrade from SQL Server 2005, SQL Server 2008, SQL Server 2008 R2 or SQL Server 2012**.  
  
    4.  Complete the wizard.  
  
3.  **For [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] only**: When the upgrade is complete, add the **[!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)]** feature.  
  
    1.  Open the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Setup wizard.  
  
    2.  In the left pane, click **Installation**.  
  
    3.  In the right pane, click **New SQL Server stand-alone installation or add features to an existing installation**.  
  
    4.  On the **Installation Type** page of the wizard, select the **Add features to an existing instance** option, and choose the instance where MDS database is installed.  
  
    5.  On the **Feature Selection** page, under **Shared Features**, select **[!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)]**.  
  
    6.  Complete the wizard.  
  
4.  Upgrade the MDS database schema.  
  
    1.  Open the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)].  
  
        > [!IMPORTANT]  
        >  To upgrade the MDS database schema, you must be logged in as the Administrator Account that was specified when the MDS database was created. In the MDS database, in mdm.tblUser, this user has the **ID** value of **1**. For information on changing this user, see [Change the System Administrator Account &#40;Master Data Services&#41;](../../master-data-services/change-the-system-administrator-account-master-data-services.md).  
  
    2.  In the left pane, click **Database Configuration**.  
  
    3.  In the right pane, click **Select Database** and specify the information for your database instance.  
  
    4.  Click **Upgrade Database** to start the **Upgrade Database Wizard**. For more information, see [Upgrade Database Wizard &#40;Master Data Services Configuration Manager&#41;](../../master-data-services/upgrade-database-wizard-master-data-services-configuration-manager.md).  
  
    5.  Click **Apply**.  
  
5.  When the upgrade is complete, create a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] web application.  
  
    1.  Open the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)].  
  
    2.  In the left pane, click **Web Configuration**.  
  
    3.  In the right pane, from the **Website** list, select one of the following options:  
  
        -   **Default Web Site**, then click **Create Application**.  
  
        -   **Create new site**. A new web application is automatically created when the website is created.  
  
        > [!IMPORTANT]  
        >  Your existing MDS web application from an earlier version of SQL Server ([!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]) is available for selection in the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of Master Data Services Configuration Manager. You must not select the existing web application, and instead must create a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] web application for MDS. Otherwise, you will receive an error when you try to associate the web application with the upgraded MDS database stating that the requested page cannot be accessed because the related configuration data for the page is invalid.  
        >   
        >  If you want to use the same name (alias) for MDS web application as your existing ([!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]) web application, you must first delete the web application and the associated application pool from IIS, and then create a web application with the same name using [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of Master Data Services Configuration Manager. For information about removing web application and application pools from IIS, see [Remove an Application (IIS)](https://go.microsoft.com/fwlink/?LinkId=323537) and [Remove an Application Pool (IIS)](https://go.microsoft.com/fwlink/?LinkId=323538).  
  
6.  Now associate the new web application with the upgraded MDS database.  
  
    1.  In the **Associate Application with Database** section, click **Select**.  
  
    2.  Select the MDS database.  
  
    3.  Click **Apply**.  
  
##  <a name="twocomputer"></a> Upgrade in Two-Computer Scenario  
 This scenario involves upgrading a system in which SQL Server is installed on two computers: one with [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], and the other with SQL Server 2008 R2 or SQL Server 2012.  
  
 If [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] is installed, you continue to use [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] respectively to host your MDS database on one computer. However, you must upgrade the schema of the MDS database, and then use the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] web application to access the MDS database. The MDS database can no longer be accessed by the [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] web application.  
  
-   In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], by default, the files are installed at *drive*:\Program Files\Microsoft SQL Server\120\Master Data Services.  
  
-   In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], by default, the files are installed at *drive*:\Program Files\Microsoft SQL Server\110\Master Data Services.  
  
-   In [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], by default, the files are installed at *drive*:\Program Files\Microsoft SQL Server\Master Data Services.  
  
 To perform this task, complete the following steps.  
  
1.  Install [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] and any other features you want.  
  
    1.  Open the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Setup wizard.  
  
    2.  In the left pane, click **Installation**.  
  
    3.  In the right pane, click **New SQL Server stand-alone installation or add features to an existing installation**.  
  
    4.  On the **Feature Selection** page, select **[!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)]** and any other features you want to install.  
  
    5.  Complete the wizard.  
  
2.  When the installation is complete, upgrade the MDS database schema.  
  
    1.  Open the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)].  
  
        > [!IMPORTANT]  
        >  To upgrade the MDS database schema, you must be logged in as the Administrator Account that was specified when the MDS database was created. In the MDS database, in mdm.tblUser, this user has the **ID** value of **1**. For information on changing this user, see [Change the System Administrator Account &#40;Master Data Services&#41;](../../master-data-services/change-the-system-administrator-account-master-data-services.md).  
  
    2.  In the left pane, click **Database Configuration**.  
  
    3.  In the right pane, click **Select Database** and specify the information for your [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] database instance on the other computer, if [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] is installed on the other computer.  
  
    4.  Click **Upgrade Database** to start the **Upgrade Database Wizard**. For more information, see [Upgrade Database Wizard &#40;Master Data Services Configuration Manager&#41;](../../master-data-services/upgrade-database-wizard-master-data-services-configuration-manager.md).  
  
3.  When the upgrade is complete, create a SQL Server 2014 web application.  
  
    1.  Open the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)].  
  
    2.  In the left pane, click **Web Configuration**.  
  
    3.  In the right pane, from the **Website** list, select one of the following options:  
  
        -   **Default Web Site**, then click **Create Application**.  
  
        -   **Create new site**. A new web application is automatically created when the website is created.  
  
        > [!IMPORTANT]  
        >  Your existing MDS web application from an earlier version of SQL Server ([!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]) is available for selection in the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of Master Data Services Configuration Manager. You must not select the existing web application, and instead must create a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] web application for MDS. Otherwise, you will receive an error when you try to associate the web application with the upgraded MDS database stating that the requested page cannot be accessed because the related configuration data for the page is invalid.  
        >   
        >  If you want to use the same name (alias) for MDS web application as your existing ([!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]) web application, you must first delete the web application and the associated application pool from IIS, and then create a web application with the same name using [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of Master Data Services Configuration Manager. For information about removing web application and application pools from IIS, see [Remove an Application (IIS)](https://go.microsoft.com/fwlink/?LinkId=323537) and [Remove an Application Pool (IIS)](https://go.microsoft.com/fwlink/?LinkId=323538).  
  
4.  Now associate the web application with the upgraded MDS database.  
  
    1.  In the **Associate Application with Database** section, click **Select**.  
  
    2.  Select the MDS database.  
  
    3.  Click **Apply**.  
  
##  <a name="restore"></a> Upgrade with Restoring a Database from Backup  
 In this scenario, [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] is installed along with SQL Server 2008 R2 or SQL Server 2012 on the same computer or two different computers. Also, a database was backed up on a version earlier than the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] CTP2 release, prior to upgrade, and the database has to be restored.  
  
-   In [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], by default, the files are installed at *drive*:\Program Files\Microsoft SQL Server\120\Master Data Services.  
  
-   In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], by default, the files are installed at *drive*:\Program Files\Microsoft SQL Server\110\Master Data Services.  
  
-   In [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)], by default, the files are installed at *drive*:\Program Files\Microsoft SQL Server\Master Data Services.  
  
 To perform this task, complete the following steps.  
  
1.  Install [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] and any other features you want.  
  
    1.  Open the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Setup wizard.  
  
    2.  In the left pane, click **Installation**.  
  
    3.  In the right pane, click **New SQL Server stand-alone installation or add features to an existing installation**.  
  
    4.  On the **Feature Selection** page, select **[!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)]** and any other features you want to install.  
  
    5.  Complete the wizard.  
  
2.  Restore the database that was backed up.  
  
3.  When the installation is complete, upgrade the MDS database schema.  
  
    1.  Open the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)].  
  
        > [!IMPORTANT]  
        >  To upgrade the MDS database schema, you must be logged in as the Administrator Account that was specified when the MDS database was created. In the MDS database, in mdm.tblUser, this user has the **ID** value of **1**. For information on changing this user, see [Change the System Administrator Account &#40;Master Data Services&#41;](../../master-data-services/change-the-system-administrator-account-master-data-services.md).  
  
    2.  In the left pane, click **Database Configuration**.  
  
    3.  In the right pane, click **Select Database** and specify the information for your [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] database instance.  
  
    4.  Click **Upgrade Database** to start the **Upgrade Database Wizard**. For more information, see [Upgrade Database Wizard &#40;Master Data Services Configuration Manager&#41;](../../master-data-services/upgrade-database-wizard-master-data-services-configuration-manager.md).  
  
4.  When the upgrade is complete, create a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] web application.  
  
    1.  Open the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)].  
  
    2.  In the left pane, click **Web Configuration**.  
  
    3.  In the right pane, from the **Website** list, select one of the following options:  
  
        -   **Default Web Site**, then click **Create Application**.  
  
        -   **Create new site**. A new web application is automatically created when the website is created.  
  
        > [!IMPORTANT]  
        >  Your existing MDS web application from an earlier version of SQL Server ([!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]) is available for selection in the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of Master Data Services Configuration Manager. You must not select the existing web application, and instead must create a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] web application for MDS. Otherwise, you will receive an error when you try to associate the web application with the upgraded MDS database stating that the requested page cannot be accessed because the related configuration data for the page is invalid.  
        >   
        >  If you want to use the same name (alias) for MDS web application as your existing ([!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]) web application, you must first delete the web application and the associated application pool from IIS, and then create a web application with the same name using [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] version of Master Data Services Configuration Manager. For information about removing web application and application pools from IIS, see [Remove an Application (IIS)](https://go.microsoft.com/fwlink/?LinkId=323537) and [Remove an Application Pool (IIS)](https://go.microsoft.com/fwlink/?LinkId=323538).  
  
5.  Now associate the new web application with the upgraded MDS database.  
  
    1.  In the **Associate Application with Database** section, click **Select**.  
  
    2.  Select the MDS database.  
  
    3.  Click **Apply**.  
  
## Troubleshooting  
 **Issue:** When you open the [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)][!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application, a "client version is not compatible with the database version" error message is displayed.  
  
 **Solution:** This issue occurs when a [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)] or [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] Master Data Manager web application tries to access a database that has been upgraded to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] Master Data Services. You must use a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] web application instead.  
  
 This issue may also occur if you did not stop and restart the **MDS Application Pool** in IIS when upgrading the MDS database schema. Restart the **MDS Application Pool** to correct the issue.  
  
## See Also  
 [Install Master Data Services](../../master-data-services/install-windows/install-master-data-services.md)  
  
  
