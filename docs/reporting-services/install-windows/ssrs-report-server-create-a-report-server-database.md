---
title: "Create a report server database, SSRS Configuration Manager | Microsoft Docs"
author: maggiesMSFT
ms.author: maggies
manager: kfile
ms.prod: reporting-services
ms.prod_service: reporting-services-native
ms.topic: conceptual
ms.custom: seodec18
ms.date: 12/15/2018
---

# Create a report server database 

[!INCLUDE [ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)]

[!INCLUDE [ssrs-previous-versions](../../includes/ssrs-previous-versions.md)]

SQL Server [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] native mode uses two [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational databases to store report server metadata and objects. One database is used for primary storage, and the second one stores temporary data. 

The databases are created together and bound by name. With a default [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, the databases are named **reportserver** and **reportservertempdb**. Collectively, the two databases are called the **report server database** or **report server catalog**.

SQL Server [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] **SharePoint mode** includes a third database that's used for data alerting metadata. The three databases are created for each SSRS service application. The database names by default include a GUID that represents the service application. The following are example names of the three SharePoint mode databases:

- ReportingService_90a9f37075544f22953c4a62e4a9f370  
  
- ReportingService_90a9f37075544f22953c4a62e4a9f370TempDB  
  
- ReportingService_90a9f37075544f22953c4a62e4a9f370_Alerting  
  
> [!IMPORTANT]  
> Don't write applications that run queries against the report server database. The report server database isn't a public schema. The table structure might change from one release to the next. If you write an application that requires access to the report server database, always use the SQL Server [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] APIs to access the report server database.  
>
> Execution log views are exceptions to this rule. For more information, see [Report Server ExecutionLog and the ExecutionLog3 View](../../reporting-services/report-server/report-server-executionlog-and-the-executionlog3-view.md).  
  
## Ways to create the report server database

 ### Native mode
 You can create the native mode report server database in the following ways:  
  
- **Automatic**. Use the SQL Server setup wizard if you choose the default configuration option for installation. In the SQL Server Installation Wizard, this option is **Install and configure** on the **Report Server Installation Options** page. If you choose the **Install only** option, you must use SQL Server Reporting Services Configuration Manager to create the database.  
  
- **Manual**. Use SQL Server [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager. Create the report server database manually if you use a remote [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] to host the database. For more information, see [Create a Native Mode Report Server Database](../../reporting-services/install-windows/ssrs-report-server-create-a-native-mode-report-server-database.md).  
  
### SharePoint mode 
The **Report Server Installation Options** page has only one option for SharePoint mode, **Install Only**. This option installs all the SQL Server [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] files and the SQL Server [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] shared service. The next step is to create at least one SSRS service application in one of the following ways:  
  
- Go to Central Administration in SharePoint Server to create an SSRS service application. For more information, see the **create a service application** section of [Install the first Report Server in SharePoint mode](../../reporting-services/install-windows/install-the-first-report-server-in-sharepoint-mode.md#bkmk_create_serrviceapplication).  
  
- Use SQL Server [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] PowerShell cmdlets to create a service application and the report server databases. For more information, see the sample for creating service applications in the topic [PowerShell cmdlets for Reporting Services SharePoint mode](../../reporting-services/report-server-sharepoint/powershell-cmdlets-for-reporting-services-sharepoint-mode.md).  
  
## Database server version requirements

 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is used to host the report server databases. The [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] instance can be local or remote. The following supported versions of [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] can host the report server databases:  
  
- [!INCLUDE[ssSQL15](../../includes/sssql15-md.md)]  
  
- [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)]  
  
- [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]  
  
- [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]  
  
- [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]  
  
If you create the report server database on a remote computer, configure the connection to use a domain user account or a service account that has network access. If you use a remote [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance, consider which credentials the report server should use to connect to the instance. For more information, see [Configure a Report Server Database Connection &#40;SSRS Configuration Manager&#41;](../../reporting-services/install-windows/configure-a-report-server-database-connection-ssrs-configuration-manager.md).  
  
> [!IMPORTANT]  
> The report server and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance hosting the report server database can be in different domains. For internet deployment, it's common practice to use a server that's behind a firewall. 
>
> If you configure a report server for internet access, use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] credentials to connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that's behind the firewall. Secure the connection by using IPSEC.  
  
## Edition requirements for a database server 

 When you create a report server database, not all editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can be used to host the database. For more information, see [Edition requirements for the report server database](../reporting-services-features-supported-by-the-editions-of-sql-server-2016.md#edition-requirements-for-the-report-server-database) in [SQL Server Reporting Services features supported by its editions](../reporting-services-features-supported-by-the-editions-of-sql-server-2016.md).  

## Next steps

Read about [Reporting Services Configuration Manager](https://msdn.microsoft.com/63519ef4-e68a-42fb-9cf7-31228ea4e434).  

More questions? Ask the [Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231).
