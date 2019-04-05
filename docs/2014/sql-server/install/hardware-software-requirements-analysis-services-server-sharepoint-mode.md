---
title: "Hardware and Software Requirements for Analysis Services Server in SharePoint Mode (SQL Server 2014) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: fb86ca0a-518c-4c61-ae78-7680c57fae1f
author: markingmyname
ms.author: maghan
manager: craigg
---
# Hardware and Software Requirements for Analysis Services Server in SharePoint Mode (SQL Server 2014)
  [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] supports both SharePoint 2010 and SharePoint 2013. [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] 2013 runs outside the SharePoint farm though it can be installed on SharePoint servers. [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] 2010 runs on application servers in a SharePoint 2010 farm and uses SharePoint features and infrastructure to support server operations. To install [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] for either version of SharePoint use the [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] installation wizard. Following the installation, complete the following:  
  
-   Run the appropriate version of the [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] configuration tool for the version of SharePoint.  
  
-   For SharePoint 2013, install the [Install or Uninstall the PowerPivot for SharePoint Add-in &#40;SharePoint 2013&#41;](../../analysis-services/instances/install-windows/install-or-uninstall-the-power-pivot-for-sharepoint-add-in-sharepoint-2013.md).  

  
##  <a name="bkmk_sqleditions"></a> SQL Server Edition Requirements  
 Business intelligence features are not all available in all editions of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. For details, see [Features Supported by the Editions of SQL Server 2014](../../../2014/getting-started/features-supported-by-the-editions-of-sql-server-2014.md) and [Editions and Components of SQL Server 2014](../editions-and-components-of-sql-server-2016.md).  
  
 The current release notes can be found at [Microsoft SQL Server 2014 Release Notes](https://go.microsoft.com/fwlink/?LinkID=296445).  
  

  
##  <a name="bkmk_sqllicense"></a> SQL Server licensing  
 For more information on SQL Server licensing, see the following:  
  
-   [SQL Server 2014 Licensing Datasheet](https://download.microsoft.com/download/6/6/F/66FF3259-1466-4BBA-A505-2E3DA5B2B1FA/SQL_Server_2014_Licensing_Datasheet.pdf) (https://download.microsoft.com/download/6/6/F/66FF3259-1466-4BBA-A505-2E3DA5B2B1FA/SQL_Server_2014_Licensing_Datasheet.pdf).  
  
-   [How To Buy: SQL Server licensing models support](https://www.microsoft.com/licensing/product-licensing/sql-server-2014?activetab=sql-server-2014-pivot%3aprimaryr2) (https://www.microsoft.com/licensing/product-licensing/sql-server-2014?activetab=sql-server-2014-pivot%3aprimaryr2).  
  

  
##  <a name="bkmk_ssas__sharepoint_2013"></a> Analysis Services Installed on SharePoint 2013  
 If you install Analysis Services server in SharePoint mode on a server by itself, then minimum system requirements are based on [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] rather than SharePoint Server requirements.  
  
 [Hardware and Software Requirements for Installing SQL Server 2014](hardware-and-software-requirements-for-installing-sql-server.md)  
  
 [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint runs best on new generation business servers that offer higher RAM thresholds and more processing capability. Large amounts of RAM are used to store [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] data in memory. The RAM supports the ability to adapt to structural changes. Extra processors support long running scans of raw, un-aggregated data. The data assumes its structure in a dynamic environment, in response to user-driven data analysis initiated through an Excel client or front-end interface.  
  
> [!TIP]  
>  [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint utilizes L2 and L3 caches. To improve performance, consider using Processors with larger L2 and L3 caches.  
  
 The following table describes the minimum and recommended hardware configuration for a standalone [!INCLUDE[ssGeminiShortvnext](../../includes/ssgeminishortvnext-md.md)] server that is not part of the SharePoint farm:  
  
|Component|Minimum|Recommended|  
|---------------|-------------|-----------------|  
|Processor|64-bit dual-core processor, 3 gigahertz.|16 Cores|  
|RAM|8 gigabytes of RAM|64 gigabytes of RAM|  
|Storage|80 gigabytes of storage|80 gigabytes or more|  
  
 If you install Analysis Services server in SharePoint mode on a SharePoint farm server, review the minimum system requirements for both [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] and SharePoint Server at the following links:  
  
-   [Hardware and Software Requirements for Installing SQL Server 2014](hardware-and-software-requirements-for-installing-sql-server.md)  
  
-   [Hardware and software requirements for SharePoint 2013](https://technet.microsoft.com/library/cc262485\(office.15\).aspx).  
  
 The standard SharePoint 2013 hardware and software recommendations are for a web-based document management solution for a workgroup or team. Because [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] processing is data intensive, the standard recommendation is sufficient if the overall workload is small, for example, less than 100 users or workbooks. A larger [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] deployment requires more computing power.  
  

  
##  <a name="bkmk_ssas__sharepoint_2010"></a> Analysis Services Installed on a SharePoint 2010 Server  
 [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] 2010 runs on application servers in a SharePoint 2010 farm and uses SharePoint features and infrastructure to support server operations. The following table summarizes requirements related to SharePoint 2010 deployments:  
  
|Component|Requirement|  
|---------------|-----------------|  
|SharePoint version|SharePoint 2010 Enterprise, with Excel Services, Secure Store Service, and the Claims to Windows Token Service configured in the same server farm.<br /><br /> SharePoint must be installed using the Server Farm option in SharePoint Setup (SharePoint's Standalone installation option is not supported). [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] requires server farm infrastructure to support administrative and data access. The standalone installation does not provide these services.<br /><br /> The developer edition, which runs on Windows 7 or Windows Vista, is not supported for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] server installations.|  
|Service Packs|SharePoint Server 2010 Service Pack 1 (SP1) is required.<br /><br /> SharePoint 2010 Service Pack 1 is required for [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)][!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] features.<br /><br /> SharePoint 2010 August 2010 Cumulative Update, or later is required when Upgrading from a previous version of [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] to [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]. The August 2010 Cumulative update or later should be installed after installing SharePoint Service Pack 1. A new installation of [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] does not require the cumulative update. For more information, see [August 2010 Cumulative Update for SharePoint has been released](http://blogs.technet.com/b/stefan_gossner/archive/2010/09/02/august-2010-cumulative-update-for-sharepoint-has-been-released.aspx).|  
|SharePoint web application|[!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint 2010 only supports SharePoint web applications that are configured for classic-mode authentication. If you are adding [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint to an existing farm, be sure that the web application you plan to use it with is configured for classic-mode authentication. For instructions on how to check authentication mode, see the section "Verify the Web application uses Classic mode authentication" in [Deploy PowerPivot Solutions to SharePoint](../../analysis-services/power-pivot-sharepoint/deploy-power-pivot-solutions-to-sharepoint.md).|  
|Data providers required for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] server-side data refresh|Server-side data refresh repeats the same data retrieval steps used to originally import the data. This means that the data providers used to import the data on a client workstation must also be present on the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint server.<br /><br /> In addition, using data feeds on a SharePoint server requires that you have ADO.NET Data Services. The SharePoint Prerequisite Installer program does not install this software for you. The following software must be installed manually.<br /><br /> ADO.NET Data Services 3.5 SP1 runtime assemblies, used to export a SharePoint list as a data feed. Download and install the version that matches your operating system:<br /><br /> For Windows Server 2008 R2, use [ADO.NET Data Services Update for .NET Framework 3.5 SP1 for Windows 7 and windows Server 2008 R2 (https://go.microsoft.com/fwlink/?LinkId=182557)](https://go.microsoft.com/fwlink/?LinkId=182557). Windows Server 2008 R2 **SP1** already contains the updated provider.<br /><br /> For Windows Server 2008, use [ADO.NET Data Services Update for .NET Framework 3.5 SP1 for Windows 2000, Windows Server 2003, Windows XP, Windows Vista and Windows Server 2008 (https://go.microsoft.com/fwlink/?LinkId=158125)](https://go.microsoft.com/fwlink/?LinkId=158125).|  
  
 [Determine Hardware and Software Requirements (SharePoint 2010) (https://go.microsoft.com/fwlink/?LinkId=169734)](https://go.microsoft.com/fwlink/?LinkId=169734)  
  
 
  
## Additional Information  
 For information on SharePoint changes, see [Changes from SharePoint 2010 to SharePoint 2013](https://technet.microsoft.com/library/ff607742\(office.15\).aspx) (https://technet.microsoft.com/library/ff607742(office.15).aspx).  
  

  
  
