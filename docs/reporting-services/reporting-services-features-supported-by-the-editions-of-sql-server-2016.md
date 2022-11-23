---
title: Features supported by different editions - SQL Server Reporting Services | Microsoft Docs
description: Learn about SQL Server Reporting Services (SSRS) features supported by the different editions of SQL Server. 
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
author: maggiesMSFT
ms.author: maggies
ms.date: 10/12/2022
---

# SQL Server Reporting Services features supported by editions

[!INCLUDE[ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../includes/ssrs-appliesto-pbirs.md)]

This topic explains the SQL Server Reporting Services (SSRS) features supported by the different editions of SQL Server. SQL Server Evaluation edition is available for a 180-day trial period.  

## Related links
  
 - [Release notes for SQL Server Reporting Services (SSRS)](release-notes-reporting-services.md). 
 - [What's new in SQL Server Reporting Services (SSRS)](~/reporting-services/what-s-new-in-sql-server-reporting-services-ssrs.md).
 - [Features supported by the editions of SQL Server](~/sql-server/editions-and-components-of-sql-server-version-15.md)

##  <a name="SSRS"></a> SQL Server Reporting Services  

For features supported by the Evaluation and Developer editions, see the SQL Server Enterprise edition column in the following table.

|Feature name|Enterprise|Standard|Web|Express with Advanced Services|Developer|  
|------|---------|---------------|-----------|-------|---------|  
| Power BI reports and Excel workbooks<sup>4</sup> | Yes, with Software Assurance | | | | Yes |
|Mobile reports and analytics|Yes||||Yes|  
|Supported catalog database [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] edition|Standard or higher|Standard or higher|Web|Express|Standard or higher|  
|Supported data source [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] edition|All   [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] editions|All [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] editions|Web|Express|All [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] editions|  
|Report server|Yes|Yes|Yes|Yes|Yes|  
|Report designer|Yes|Yes|Yes|Yes|Yes|  
|Report designer web portal|Yes|Yes|Yes|Yes|Yes|  
|Role-based security|Yes|Yes|Yes|Yes|Yes|  
|Export to Excel, PowerPoint, Word, PDF, and images|Yes|Yes|Yes|Yes|Yes|  
|Enhanced gauges and charting|Yes|Yes|Yes|Yes|Yes|  
|Pin report items to [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)] dashboards|Yes|Yes|Yes|Yes|Yes|  
|Custom authentication|Yes|Yes|Yes||Yes|  
|Report as data feeds|Yes|Yes|Yes|Yes|Yes|  
|Model support|Yes|Yes|Yes||Yes|  
|Create custom roles for role-based security|Yes|Yes|||Yes|  
|Model item security|Yes|Yes|||Yes|  
|Infinite click through|Yes|Yes|||Yes|  
|Shared-component library|Yes|Yes|||Yes|  
|Email and file share subscriptions and scheduling|Yes|Yes|||Yes|  
|Report history, execution snapshots, and caching|Yes|Yes|||Yes|  
|SharePoint integration<sup>2</sup>|Yes|Yes|||Yes|  
|Remote and non-SQL data source support<sup>1</sup>|Yes|Yes|||Yes|  
|Data source, delivery, and rendering and RDCE extensibility|Yes|Yes|||Yes|  
|Custom branding|Yes||||Yes|  
|Data-driven report subscription|Yes||||Yes|  
|Scale-out deployment (web farms)|Yes||||Yes|  
|Alerting<sup>2</sup> (SSRS 2016) |Yes||||Yes|  
|Power view<sup>2</sup> (SSRS 2016) |Yes||||Yes| 
|Comments<sup>3</sup> |Yes|Yes|Yes|Yes|Yes|  

 <sup>1</sup> For more information on supported data sources in SQL Server Reporting Services (SSRS), see [Data sources supported by Reporting Services &#40;SSRS&#41;](../reporting-services/report-data/data-sources-supported-by-reporting-services-ssrs.md).  
  
 <sup>2</sup> Requires SQL Server 2016 Reporting Services in SharePoint mode. For more information, see [Install SQL Server Reporting Services in SharePoint mode](../reporting-services/install-windows/install-reporting-services-sharepoint-mode.md). Starting in SQL Server 2017 Reporting Services, integration with SharePoint is no longer available. Power View support is no longer available after SQL Server 2017.

<sup>3</sup> Only in Power BI Report Server and SQL Server 2017 Reporting Services and later.

<sup>4</sup> Only in Power BI Report Server.

> [!NOTE]
> SQL Server Express with Tools and SQL Server Express don't support SQL Server Reporting Services.
  
## Edition requirements for the report server database
 When you create a report server database, not all editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can be used to host the database. The following table shows you which editions of the [!INCLUDE[ssDE](../includes/ssde-md.md)] you can use for specific editions of SQL Server [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)].  
  
|For this edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Reporting Services or Power BI Report Server,|Use this edition of the Database Engine instance to host the database.|  
|----------------------------------------------------------------------|---------------------------------------------------------------------------|  
|Power BI Premium (for Power BI Report Server)|Enterprise or Standard editions (local or remote)|  
|Enterprise (including Enterprise Software Assurance)|Enterprise or Standard editions (local or remote)|  
|Standard|Enterprise or Standard editions (local or remote)|  
|Web|Web edition (local only)|  
|Express with Advanced Services|Express with Advanced Services (local only)|  
|Evaluation|Evaluation|
|Developer|Developer|
  
##  <a name="BIC"></a> Business intelligence clients  
The following software client applications are available on the Microsoft Download Center. They help you create business intelligence documents that run on a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance. When you host these documents in a server environment, use an edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that supports that document type. The following table identifies which [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] edition contains the server features required to host the documents created in these client applications.  
  
|Tool name|Enterprise|Standard|Web|Express with Advanced Services|Developer|  
|---------------|----------------|--------------|------------------------|-------------|---------------| 
| Power BI Desktop optimized for Power BI Report Server, **.pbix** | Yes, with Software Assurance | | | | Yes |
|[!INCLUDE[ssRBnoversion](../includes/ssrbnoversion.md)], **.rdl** and **.rds**|Yes|Yes|Yes|Yes|Yes|  
|[!INCLUDE[SS_MobileReptPub_Long](../includes/ss-mobilereptpub-long.md)], **.rsmobile**|Yes||||Yes|  
|Power BI apps for mobile devices (iOS, Windows, and Android), **.rsmobile**|Yes||||Yes|  
  
> [!NOTE]  
> * The preceding table identifies the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] editions that are required to enable these client tools. However, these tools can access data hosted on any edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
> * [!INCLUDE[SS_MobileReptPub_Long](../includes/ss-mobilereptpub-long.md)] is the single point for creation of mobile reports. Connect to an SSRS server to access data sources and create reports. Then publish them to the SSRS server for others in the organization to access, either on the server or on mobile devices. You can also use [!INCLUDE[SS_MobileReptPub_Long](../includes/ss-mobilereptpub-long.md)] stand alone with local data sources. However, SQL Server Mobile Report Publisher is deprecated for all releases of SQL Server Reporting Services after SQL Server Reporting Services 2019.
> * Whether you use [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)] on-premises, [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)] in the cloud, or both as your report delivery solution, you only need one mobile app to access dashboards and mobile reports on mobile devices. The [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)] apps are available for download from the Windows, iOS, or Android app stores.  

## Next steps

* Read about [Features supported by the editions of SQL Server 2017](~/sql-server/editions-and-components-of-sql-server-2017.md). 

* [Plan a SQL Server installation](../sql-server/install/planning-a-sql-server-installation.md).

* More questions? Ask the [SQL Server Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user).
