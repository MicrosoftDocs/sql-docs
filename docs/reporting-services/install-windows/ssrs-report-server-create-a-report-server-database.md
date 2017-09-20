---
title: "Create a Report Server Database  (SSRS Configuration Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "09/20/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "guyinacube"
ms.author: "asaxton"
manager: "erikre"
---

# Create a Report Server Database

[!INCLUDE [ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)]

[!INCLUDE [ssrs-previous-versions](../../includes/ssrs-previous-versions.md)]

[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] **Native mode** uses two [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational databases to store report server metadata and objects. One database is used for primary storage, and the second one stores temporary data. The databases are created together and bound by name. With a default [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, the databases are named **reportserver** and **reportservertempdb**. Collectively, the two databases are referred to as the "report server database" or "report server catalog".

[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] **SharePoint mode** includes a third database that is used for data alerting metadata. The three databases are created for each [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application and the database names by default include a guid that represents the service application. The following are example names of the three SharePoint mode databases:

-   ReportingService_90a9f37075544f22953c4a62e4a9f370  
  
-   ReportingService_90a9f37075544f22953c4a62e4a9f370TempDB  
  
-   ReportingService_90a9f37075544f22953c4a62e4a9f370_Alerting  
  
> [!IMPORTANT]  
>  Do not write applications that run queries against the report server database. The report server database is not a public schema. The table structure might change from one release to the next. If you write an application that requires access to the report server database, always use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] APIs to access the report server database.  
>   
>  The exception to this are the execution log views. For more information, see [Report Server ExecutionLog and the ExecutionLog3 View](../../reporting-services/report-server/report-server-executionlog-and-the-executionlog3-view.md)  
  
## Ways to Create the Report Server Database  
 **Native Mode:** You can create the Native mode report server database in the following ways:  
  
-   Automatically: Use SQL Server Setup Wizard, if you choose the default configuration installation option. In the SQL Server Installation Wizard, this is the **Install and configure** in the Report Server Installation Options page. If you chose the **Install only** option, you must use the Reporting Services Configuration Manager to create the database.  
  
-   Manually: Use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager. You must create the report server database manually if you are using a remote [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] to host the database. For more information, see [Create a Native Mode Report Server Database  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/ssrs-report-server-create-a-native-mode-report-server-database.md).  
  
 **SharePoint Mode:** The Report Server Installation Options page only has one option for SharePoint mode of **Install Only**. This option installs all the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] files and the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] shared service. The next step is to create at least one [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application in one of the following ways:  
  
-   Use SharePoint Central Administration to create a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] service application. For more information see the “Service Application” section of [Step 3: Create a Reporting Services Service Application](../../reporting-services/install-windows/install-the-first-report-server-in-sharepoint-mode.md#bkmk_create_serrviceapplication).  
  
-   Use [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] PowerShell cmdlets to create a service application and the report server databases. For more information see the sample for creating service applications in the topic [PowerShell cmdlets for Reporting Services SharePoint Mode](../../reporting-services/report-server-sharepoint/powershell-cmdlets-for-reporting-services-sharepoint-mode.md).  
  
## Database Server Version Requirements  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is used to host the report server databases. The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance can be a local or remote instance. The following are the supported versions of [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] that can be used to host the report server databases:  
  
-   [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]  
  
-   [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]  
  
-   [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]  
  
-   [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]  
  
-   [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]  
  
 Creating the report server database on a remote computer requires that you configure the connection to use a domain user account or a service account that has network access. If you decide to use a remote [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, consider carefully which credentials the report server should use to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. For more information, see [Configure a Report Server Database Connection  &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md).  
  
> [!IMPORTANT]  
>  Report Server and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance hosting the report server database can be in different domains. For Internet deployment, it is common practice to use a server that is behind a firewall. If you are configuring a report server for Internet access, use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] credentials to connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that is behind the firewall and use IPSEC to secure the connection.  
  
## Database Server Edition Requirements  
 When creating a report server database, be aware that not all editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can be used to host the database. For more information, see the “Report Server Database Server Edition Requirements” section of [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).  

## Next steps

[Reporting Services Configuration Manager](http://msdn.microsoft.com/en-us/63519ef4-e68a-42fb-9cf7-31228ea4e434)  

More questions? [Try asking the Reporting Services forum](http://go.microsoft.com/fwlink/?LinkId=620231)