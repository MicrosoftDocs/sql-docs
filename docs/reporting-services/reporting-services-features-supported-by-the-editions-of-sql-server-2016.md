---
title: "Reporting Services Features Supported by the Editions of SQL Server 2016 | Microsoft Docs"
ms.custom: ""
ms.date: "05/30/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-native"
  - "reporting-services-sharepoint"
ms.tgt_pltfrm: ""
ms.topic: "get-started-article"
ms.assetid: 39f03d2d-6e48-4b34-a9d3-07f86313b937
caps.latest.revision: 3
author: "guyinacube"
ms.author: "asaxton"
manager: "erikre"
---

# Reporting Services Features Supported by the Editions of SQL Server 2016

This topic provides details of features supported by the different editions of SQL Server 2016.  
  
 SQL Server Evaluation edition is available for a 180-day trial period.  
  
 For the latest release notes, see [SQL Server 2016 Release Notes](../sql-server/sql-server-2016-release-notes.md). For the latest information on what is new, see [What's new in Reporting Services (SSRS)](~/reporting-services/what-s-new-in-sql-server-reporting-services-ssrs.md).
    
 **Try SQL Server 2016!**    
    
 > [![Download from Evaluation Center](../analysis-services/media/download.png)](https://www.microsoft.com/en-us/evalcenter/evaluate-sql-server-2016) **[Download SQL Server 2016  from the Evaluation Center](https://www.microsoft.com/en-us/evalcenter/evaluate-sql-server-2016)**    
    
> ![Azure Virtual Machine small](../analysis-services/media/azure-virtual-machine-small.png) **[Spin up a Virtual Machine with SQL Server 2016 already installed](https://azure.microsoft.com/en-us/marketplace/partners/microsoft/sqlserver2016rtmenterprisewindowsserver2012r2/?wt.mc_id=sqL16_vm)**    

For features supported by Evaluation and Developer editions see SQL Server Enterprise Edition.

To navigate the table for a SQL Server technology, click on its link:  

-   [Reporting Services](#SSRS)  
  
-   [Business Intelligence Clients](#BIC)  

##  <a name="SSRS"></a> Reporting Services  
  
|Feature Name|Enterprise|Standard|Web|Express with Advanced Services|Express with Tools|Express|Developer|  
|------------------|----------------|--------------|---------|------------------------------------|------------------------|-------------|---------------|  
|Mobile reports and KPIs|Yes||||||Yes|  
|Supported catalog DB [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] edition|Standard or higher|Standard or higher|Web|Express|||Standard or higher|  
|Supported data source [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] edition|All   [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] editions|All [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] editions|Web|Express|||All [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] editions|  
|Report server|Yes|Yes|Yes|Yes|||Yes|  
|Report designer|Yes|Yes|Yes|Yes|||Yes|  
|Report designer web portal|Yes|Yes|Yes|Yes|||Yes|  
|Role based security|Yes|Yes|Yes|Yes|||Yes|  
|Export to  Excel, PowerPoint, Word, PDF, and images|Yes|Yes|Yes|Yes|||Yes|  
|Enhanced gauges and charting|Yes|Yes|Yes|Yes|||Yes|  
|Pin report items to [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)]dashboards|Yes|Yes|Yes|Yes|||Yes|  
|Custom authentication|Yes|Yes|Yes|Yes|||Yes|  
|Report as data feeds|Yes|Yes|Yes|Yes|||Yes|  
|Model support|Yes|Yes|Yes||||Yes|  
|Create custom roles for role-based security|Yes|Yes|||||Yes|  
|Model Item security|Yes|Yes|||||Yes|  
|Infinite click through|Yes|Yes|||||Yes|  
|Shared component library|Yes|Yes|||||Yes|  
|Email and file share subscriptions and scheduling|Yes|Yes|||||Yes|  
|Report history, execution snapshots and caching|Yes|Yes|||||Yes|  
|SharePoint Integration|Yes|Yes|||||Yes|  
|Remote and non-SQL data source support<sup>1</sup>|Yes|Yes|||||Yes|  
|Data source, delivery and rendering, RDCE extensibility|Yes|Yes|||||Yes|  
|Custom branding|Yes||||||Yes|  
|Data driven report subscription|Yes||||||Yes|  
|Scale out deployment (Web farms)|Yes||||||Yes|  
|Alerting<sup>2</sup>|Yes||||||Yes|  
|[!INCLUDE[ssCrescent](../includes/sscrescent-md.md)] <sup>2</sup>|Yes||||||Yes|  
  
 <sup>1</sup> For more information on supported datasources in SQL Server 2016 Reporting Services (SSRS), see [Data Sources Supported by Reporting Services &#40;SSRS&#41;](../reporting-services/report-data/data-sources-supported-by-reporting-services-ssrs.md).  
  
 <sup>2</sup> Requires Reporting Services in SharePoint mode. For more information, see [Install Reporting Services SharePoint Mode](../reporting-services/install-windows/install-reporting-services-sharepoint-mode.md).  
  
## Report Server Database Server Edition Requirements  
 When creating a report server database, not all editions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] can be used to host the database. The following table shows you which editions of the [!INCLUDE[ssDE](../includes/ssde-md.md)] you can use for specific editions of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)].  
  
|For this edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Reporting Services|Use this edition of the Database Engine instance to host the database|  
|----------------------------------------------------------------------|---------------------------------------------------------------------------|  
|Enterprise|Enterprise,  or Standard  editions (local or remote)|  
|Standard|Enterprise,  or Standard  editions (local or remote)|  
|Web|Web edition (local only)|  
|Express with Advanced Services|Express with Advanced Services (local only).|  
|Evaluation|Evaluation|  
  
##  <a name="BIC"></a> Business Intelligence Clients  
 The following software client applications are available on the Microsoft Download center and are provided to assist you with creating business intelligence documents that run on a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance. When you host these documents in a server environment, use an edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that supports that document type. The following table identifies which [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] edition contains the server features required to host the documents created in these client applications.  
  
|Tool Name|Enterprise|Standard|Web|Express with Advanced Services|Express with Tools|Express|Developer|  
|---------------|----------------|--------------|---------|------------------------------------|------------------------|-------------|---------------|  
|[!INCLUDE[ssRBnoversion](../includes/ssrbnoversion-md.md)] (.rdl and .rds)|Yes|Yes|||||Yes|  
|[!INCLUDE[SS_MobileReptPub_Long](../includes/ss-mobilereptpub-long-md.md)] (.rsmobile)|Yes||||||Yes|  
|Power BI apps for mobile devices (iOS, Windows 10, Android) (.rsmobile)|Yes||||||Yes|  
  
> [!NOTE]  
> 1.  The above table identifies the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] editions that are required to enable these client tools; however these tools can access data hosted on any edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
> 2.  [!INCLUDE[SS_MobileReptPub_Long](../includes/ss-mobilereptpub-long-md.md)] is the single point for creation of mobile reports. Connect to  a [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] server to access data sources and create reports. Then publish them to the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] server for others in the organization to access, either on the server or on mobile devices. You can also use [!INCLUDE[SS_MobileReptPub_Long](../includes/ss-mobilereptpub-long-md.md)] stand alone with local data sources  
> 3.  Whether you are using  [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)] on-premises, [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)] in the cloud, or both as your report delivery solution you only need one mobile app to access dashboards and mobile reports on mobile devices. The [!INCLUDE[sspowerbi](../includes/sspowerbi-md.md)] apps are available for download from the Windows, iOS, or Android app stores.  

## Next steps

[Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md)  
[Product Specifications for SQL Server 2016](http://msdn.microsoft.com/library/6445fd53-6844-4170-a86b-7fe76a9f64cb)  
[Installation for SQL Server 2016](../database-engine/install-windows/installation-for-sql-server-2016.md) 

More questions? [Try asking the Reporting Services forum](http://go.microsoft.com/fwlink/?LinkId=620231)