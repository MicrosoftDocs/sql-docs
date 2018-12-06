---
title: "Install Data Quality Services | Microsoft Docs"
ms.custom: ""
ms.date: "09/11/2017"
ms.prod: sql
ms.prod_service: "data-quality-services"
ms.reviewer: ""
ms.technology: data-quality-services
ms.topic: conceptual
ms.assetid: 486e4216-a946-4c6e-828c-61bc905f7ec1
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Install Data Quality Services

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

  [!INCLUDE[ssDQSnoversionLong](../../includes/ssdqsnoversionlong-md.md)] (DQS) contains the following two components: **[!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)]** and **[!INCLUDE[ssDQSClient](../../includes/ssdqsclient-md.md)]**.  
  
|DQS Component|Description|  
|-------------------|-----------------|  
|[!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)]|[!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] is installed on top of the [!INCLUDE[ssNoversion](../../includes/ssNoVersion-md.md)] Database Engine, and includes three databases: DQS_MAIN, DQS_PROJECTS, and DQS_STAGING_DATA. DQS_MAIN contains DQS stored procedures, the DQS engine, and published knowledge bases. DQS_PROJECTS contains the data quality project information. DQS_STAGING_DATA is the staging area where you can copy your source data to perform DQS operations, and then export your processed data.|  
|[!INCLUDE[ssDQSClient](../../includes/ssdqsclient-md.md)]|[!INCLUDE[ssDQSClient](../../includes/ssdqsclient-md.md)] is a standalone application that enables you to connect to [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)], and provides you with a highly-intuitive graphical user interface to perform data-quality operations, and other administrative tasks related to DQS.|  
  
> [!IMPORTANT]  
>  Apart from the above two DQS components, you can also:  
>   
>  Use the DQS Cleansing Transformation in Integration Services that performs data cleansing within the Integration Services packaging process, and is automatically installed when you install Integration Services. For information about installing Integration Services, see [Install Integration Services](../../integration-services/install-windows/install-integration-services.md).  
>   
>  Enable DQS integration in Master Data Services to use the DQS matching functionality in the Master Data Services Add-in for Excel. For more information, see [Enable Data Quality Services Integration with Master Data Services](../../master-data-services/install-windows/enable-data-quality-services-integration-with-master-data-services.md).  
  
 DQS installation is a three-part process:  
  
-   [Pre-Installation Tasks](#PreInstallationTasks): Verify system requirements before you install DQS.  
  
-   [Data Quality Services Installation Tasks](#DQSInstallation): Install DQS by using SQL Server Setup.  
  
-   [Post-Installation Tasks](#PostInstallationTasks): Perform these tasks after finishing with the SQL Server Setup to finish installing DQS.  
  
> [!NOTE]  
>  This topic does not include instructions for running Setup from the command line. For information about command-line options for installing [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] and client, see [Feature Parameters](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md#Feature) in [Install SQL Server from the Command Prompt](../../database-engine/install-windows/install-sql-server-from-the-command-prompt.md).  
  
##  <a name="PreInstallationTasks"></a> Pre-Installation Tasks  
 Before installing DQS, make sure that your computer meets the minimum system requirements. The following table provides information about the minimum system requirements for the DQS components:  
  
|DQS Component|Minimum System Requirements|  
|-------------------|---------------------------------|  
|[!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)]|Memory (RAM): Minimum: 2 GB / Recommended: 4 GB or more<br /><br /> [!INCLUDE[ssNoVersion](../../includes/ssNoVersion-md.md)] Database Engine. For more information, see [Install SQL Server Database Engine](../../database-engine/install-windows/install-sql-server-database-engine.md).|  
|[!INCLUDE[ssDQSClient](../../includes/ssdqsclient-md.md)]|.NET Framework 4.0 (installed during the [!INCLUDE[ssDQSClient](../../includes/ssdqsclient-md.md)] installation, if not already installed)<br /><br /> Internet Explorer 6.0 SP1 or later|  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] and [!INCLUDE[ssDQSClient](../../includes/ssdqsclient-md.md)] can be installed on the same computer, or different computers. Both the components can be installed independently of each other, and in any sequence. However, to use [!INCLUDE[ssDQSClient](../../includes/ssdqsclient-md.md)], you will need a [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] installed to connect to.  
>   
>  You can connect to the [!INCLUDE[ssNoVersion](../../includes/ssNoVersion-md.md)] version of [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] using either the current or earlier version of [!INCLUDE[ssDQSClient](../../includes/ssdqsclient-md.md)] and DQS Cleansing Transformation. For information about upgrading your existing version of DQS to [!INCLUDE[ssNoVersion](../../includes/ssNoVersion-md.md)], see [Upgrade Data Quality Services](../../database-engine/install-windows/upgrade-data-quality-services.md).  
>   
>  Although Microsoft Excel is not a prerequisite for installing Data Quality Client, Microsoft Excel 2003 or later must be installed on the Data Quality Client computer to perform various operations in the client application such as importing domain values from an excel file or mapping to the source data in an Excel file for knowledge discovery, cleansing, or matching activities.  
  
 For detailed information about the minimum system requirements for installing [!INCLUDE[ssNoVersion](../../includes/ssNoVersion-md.md)], see [Hardware and Software Requirements for Installing SQL Server](../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md).  
  
##  <a name="DQSInstallation"></a> Data Quality Services Installation Tasks  
 You have to use the [!INCLUDE[ssNoVersion](../../includes/ssNoVersion-md.md)] Setup to install DQS components. When you run the SQL Server Setup, you have to go through a series of installation wizard pages to select appropriate options based on your requirements. The following table lists only those pages in the installation wizard where the options that you select will affect your installation of DQS:  
  
|Page|Action|  
|----------|------------|  
|Feature Selection|Select:<br /><br /> **Data Quality Services** under **Database Engine Services** to install the [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)]. <br />If you select the **Data Quality Services** check box, SQL Server Setup will copy an installer file, DQSInstaller.exe, under the SQL Server instance directory on your computer. You must run this file after you have completed the SQL Server Setup to *complete* the [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] installation. Also, you must perform some additional steps to configure your [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] before you can use it. For more information, see [Post-Installation Tasks](#PostInstallationTasks).<br /><br /> **Data Quality Client** to install [!INCLUDE[ssDQSClient](../../includes/ssdqsclient-md.md)].<br /><br /> (Recommended) **Management Tools - Complete** under **Management Tools - Basic** to install [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. It provides you a graphical user interface to manage your SQL Server instance, and will aid you in performing additional tasks post installation as listed in the next section.|  
|Database Engine Configuration|Click **Add Current User** to add your user Windows account to the sysadmin fixed server role. This is required for you to be able to run the DQSInstaller.exe file later to complete the [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] installation.|  
  
##  <a name="PostInstallationTasks"></a> Post-Installation Tasks  
 After you complete the SQL Server installation wizard, you must perform additional steps mentioned in this section to complete your [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] installation, configure it, and then use it.  
  
1.  To complete the [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] installation, run the DQSInstaller.exe file. On running the DQSInstaller.exe file:  
  
    -   The DQS_MAIN, DQS_PROJECTS, and DQS_STAGING_DATA databases are created.  
  
    -   The ##MS_dqs_db_owner_login## and ##MS_dqs_service_login## logins are created.  
  
    -   The dqs_administrator, dqs_kb_editor, and dqs_kb_operator roles are created in the DQS_MAIN database.  
  
    -   The DQInitDQS_MAIN stored procedure is created in the master database.  
  
    -   DQS_install.log file is typically created in the C:\Program Files\Microsoft SQL Server\MSSQL13.*<instance_name>*\MSSQL\Log folder. The file contains information about the actions performed on running the DQSInstaller.exe file.  
  
    -   If a Master Data Services database is present in the same SQL Server instance as [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)], a user mapped to the Master Data Services login is created, and is granted the dqs_administrator role on the DQS_MAIN database.  
  
     This completes the [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] installation.  
  
     For more information see [Run DQSInstaller.exe to Complete Data Quality Server Installation](../../data-quality-services/install-windows/run-dqsinstaller-exe-to-complete-data-quality-server-installation.md).  
  
2.  Grant DQS Roles to Users:  
  
     To log on to [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] using [!INCLUDE[ssDQSClient](../../includes/ssdqsclient-md.md)], a user must have any of the following three roles on the DQS_MAIN database:  
  
    -   **dqs_administrator**  
  
    -   **dqs_kb_editor**  
  
    -   **dqs_kb_operator**  
  
     By default, if your user account is a member of the sysadmin fixed server role, you can log on to [!INCLUDE[ssDQSServer](../../includes/ssdqsserver-md.md)] using [!INCLUDE[ssDQSClient](../../includes/ssdqsclient-md.md)] even if none of the DQS roles are granted to your user account. For information about the three DQS roles, see [DQS Security](../../data-quality-services/dqs-security.md).  
  
     For more information see [Grant DQS Roles to Users](../../data-quality-services/install-windows/grant-dqs-roles-to-users.md).  
  
    > [!NOTE]  
    >  The three DQS roles are not available for the DQS_PROJECTS and DQS_STAGING_DATA databases.  
  
3.  Make your data available for DQS operations. Make sure that you can access your source data for the DQS operations, and can export the processed data to a table in a database.  
  
     For more information see  
                    [Access Data for the DQS Operations](../../data-quality-services/install-windows/access-data-for-the-dqs-operations.md).  
  
## See Also  
 [Video: Install and Configure DQS](https://go.microsoft.com/fwlink/?LinkId=238241)   
 [Upgrade SQLCLR Assemblies After .NET Framework Update](../../data-quality-services/install-windows/upgrade-sqlclr-assemblies-after-net-framework-update.md)   
 [Export and Import DQS Knowledge Bases Using DQSInstaller.exe](../../data-quality-services/install-windows/export-and-import-dqs-knowledge-bases-using-dqsinstaller-exe.md)   
 [Upgrade Data Quality Services](../../database-engine/install-windows/upgrade-data-quality-services.md)   
 [Remove Data Quality Server Objects](../../sql-server/install/remove-data-quality-server-objects.md)   
 [Install SQL Server Business Intelligence Features](../../sql-server/install/install-sql-server-business-intelligence-features.md)   
 [Uninstall SQL Server](../../sql-server/install/uninstall-sql-server.md)   
 [Data Quality Services](../../data-quality-services/data-quality-services.md)   
 [Troubleshoot Installation and Configuration Issues in DQS](https://social.technet.microsoft.com/wiki/contents/articles/3776.aspx)  
  
  
