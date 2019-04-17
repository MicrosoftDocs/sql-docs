---
title: "Deprecated Features in SQL Server Reporting Services in SQL Server 2014 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "Reporting Services, backward compatibility"
  - "deprecated features [Reporting Services]"
  - "HTML OWC rendering extension [Reporting Services]"
  - "Report Server Web service, endpoints"
ms.assetid: 3876c01e-f81d-4cce-9104-5106a8c369e6
author: markingmyname
ms.author: maghan
manager: kfile
---
# Deprecated Features in SQL Server Reporting Services in SQL Server 2014
  This topic describes the deprecated [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] features. The features are still available in the release in which they are deprecated; however the features are scheduled to be removed in a future release of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Deprecated features should not be used in new applications.  
  
 In this topic:  
  
-   [SQL Server 2014 Reporting Services Deprecated Features](#bkmk_2014)  
  
-   [SQL Server 2012 SP1 Reporting Services Deprecated Features](#bkmk_2012sp1)  
  
-   [SQL Server 2012 Reporting Services Deprecated Features](#bkmk_2012)  
  
-   [SQL Server 2008 R2 Reporting Services Deprecated Features](#bkmk_kj)  
  
##  <a name="bkmk_2014"></a> SQL Server 2014 Reporting Services Deprecated Features  
  
### Features Not Supported in the Next Version of SQL Server  
 The following [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] features will not be supported in the **next** version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Do not use these features in new development work, and modify applications that currently use these features as soon as possible.  
  
#### HTML Rendering Extension Device Information Settings  
 The following device information settings for the HTML rendering extension are deprecated.  
  
-   ActionScript  
  
-   ActiveXControls  
  
-   GetImage  
  
-   OnlyVisibleStyles  
  
-   ReplacementRoot  
  
-   ResourceStreamRoot  
  
-   StreamRoot  
  
-   UsePx  
  
-   Zoom  
  
 For more information on the HTML rendering extension, see [HTML Device Information Settings](html-device-information-settings.md)  
  
#### Microsoft Word and Microsoft Excel 1997-2003 Rendering  
 The[!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] BIFF8 rendering extensions [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] reports to the [!INCLUDE[msCoName](../includes/msconame-md.md)] Word and [!INCLUDE[msCoName](../includes/msconame-md.md)] Excel 1997-2003 binary interchange file format. [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] includes extensions that render in the [!INCLUDE[msCoName](../includes/msconame-md.md)] Office 2007-2010 Open XML format.  
  
#### Report Definition Language (RDL) 2005 and Earlier  
 Report Definition Language (RDL) 2005 and earlier is deprecated. For more information about RDL, see [Report Definition Language &#40;SSRS&#41;](reports/report-definition-language-ssrs.md).  
  
 For more information on upgrading reports, see [Upgrade Reports](install-windows/upgrade-reports.md).  
  
#### SQL Server 2005 and earlier Custom Report Items  
 Custom Report Items (CRI) compiled for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] 2005 and earlier are deprecated.  
  
#### Reporting Services Snapshots 2005 and earlier  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] snapshots rendered with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] 2005 and earlier are deprecated.  
  
#### Report Models  
 Semantic modeling language (SMDL) report models are deprecated. Although you can you continue to use existing report models as data sources in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)][!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] reports you should consider updating your reports to remove their dependency on report models.  
  
 [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] does not include tools for creating or updating report models. For more information, see [Breaking Changes in SQL Server Reporting Services in SQL Server 2014](breaking-changes-in-sql-server-reporting-services-in-sql-server-2016.md).  
  
#### Deprecated Methods in the Web Service Endpoint  
 The following operations are deprecated in the <xref:ReportService2010.ReportingService2010> Web service endpoint:  
  
-   <xref:ReportService2010.ReportingService2010.GetProperties%2A>  
  
-   <xref:ReportService2010.ReportingService2010.IsSSLRequired%2A>  
  
#### SharePoint Web Parts  
 The installation cabinet file **RSWebParts.cab** and the SharePoint web parts that can be extracted from the cab file, are deprecated. The deprecated web parts are Report Explorer (**SPExplorer.dwp**) and Report Viewer (**SPViewer.dwp**).  
  
 For more information on the deprecated web parts, see [View and Explore Native Mode Reports Using SharePoint Web Parts (SSRS)](https://msdn.microsoft.com/library/ms159772.aspx)  
  
### Features Not Supported in a Future Version of SQL Server  
 The following [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] features are supported in the next version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], but will be removed in a later version. The specific version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] has not been determined.  
  
 No [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] features were deprecated in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].  
  
##  <a name="bkmk_2012sp1"></a> SQL Server 2012 SP1 Reporting Services Deprecated Features  
 This section describes [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] features deprecated in [!INCLUDE[ssSQL11SP1](../includes/sssql11sp1-md.md)]. The following [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] features are supported in the next version of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)], but will be removed in a later version. The specific version of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] has not been determined.  
  
### SharePoint Web Parts  
 The installation cabinet file **RSWebParts.cab** and the SharePoint web parts that can be extracted from the cab file, are deprecated. The deprecated web parts are Report Explorer (**SPExplorer.dwp**) and Report Viewer (**SPViewer.dwp**).  
  
 For more information on the deprecated web parts, see [View and Explore Native Mode Reports Using SharePoint Web Parts (SSRS)](https://msdn.microsoft.com/library/ms159772.aspx)  
  
##  <a name="bkmk_2012"></a> SQL Server 2012 Reporting Services Deprecated Features  
 This section describes [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] features deprecated in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)].  
  
### HTML Rendering Extension Device Information Settings  
 The following device information settings for the HTML rendering extension are deprecated.  
  
-   ActionScript  
  
-   ActiveXControls  
  
-   GetImage  
  
-   OnlyVisibleStyles  
  
-   ReplacementRoot  
  
-   ResourceStreamRoot  
  
-   StreamRoot  
  
-   UsePx  
  
-   Zoom  
  
 For more information on the HTML rendering extension, see [HTML Device Information Settings](html-device-information-settings.md)  
  
### Microsoft Word and Microsoft Excel 1997-2003 Rendering  
 The[!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] BIFF8 rendering extensions [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] reports to the [!INCLUDE[msCoName](../includes/msconame-md.md)] Word and [!INCLUDE[msCoName](../includes/msconame-md.md)] Excel 1997-2003 binary interchange file format. [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] includes extensions that render in the [!INCLUDE[msCoName](../includes/msconame-md.md)] Office 2007-2010 Open XML format.  
  
### Report Definition Language (RDL) 2005 and Earlier  
 Report Definition Language (RDL) 2005 and earlier is deprecated. For more information about RDL, see [Report Definition Language &#40;SSRS&#41;](reports/report-definition-language-ssrs.md).  
  
 For more information on upgrading reports, see [Upgrade Reports](install-windows/upgrade-reports.md).  
  
### SQL Server 2005 and earlier Custom Report Items  
 Custom Report Items (CRI) compiled for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] 2005 and earlier are deprecated.  
  
### Reporting Services Snapshots 2005 and earlier  
 [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] snapshots rendered with [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] 2005 and earlier are deprecated.  
  
### Report Models  
 Semantic modeling language (SMDL) report models are deprecated. Although you can you continue to use existing report models as data sources in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)][!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] reports you should consider updating your reports to remove their dependency on report models.  
  
 [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] does not include tools for creating or updating report models. For more information, see [Breaking Changes in SQL Server Reporting Services in SQL Server 2014](breaking-changes-in-sql-server-reporting-services-in-sql-server-2016.md).  
  
### Deprecated Methods in the Web Service Endpoint  
 The following operations are deprecated in the <xref:ReportService2010.ReportingService2010> Web service endpoint:  
  
-   <xref:ReportService2010.ReportingService2010.GetProperties%2A>  
  
-   <xref:ReportService2010.ReportingService2010.IsSSLRequired%2A>  
  
##  <a name="bkmk_kj"></a> SQL Server 2008 R2 Reporting Services Deprecated Features  
  
> [!NOTE]  
>  Because SQL Server 2008 R2 is a minor version upgrade of SQL Server 2008, we recommend that you also review the content in the SQL Server 2008 section.  
  
### Report Server Web Service Endpoints  
 The Web services <xref:ReportService2005.ReportingService2005> and <xref:ReportService2006.ReportingService2006> have been deprecated in this release. These endpoints have been replaced by a new endpoint: <xref:ReportService2010.ReportingService2010>.  
  
 The new endpoint includes all the functionality available in the deprecated endpoints and the new features introduced in SQL Server 2008 R2.  
  
## See Also  
 [What's New &#40;Reporting Services&#41;](what-s-new-reporting-services.md)   
 [Backward Compatibility](../getting-started/backward-compatibility.md)   
 [Behavior Changes to SQL Server Reporting Services  in SQL Server 2014](behavior-changes-to-sql-server-reporting-services-in-sql-server-2016.md)   
 [Discontinued Functionality to SQL Server Reporting Services in SQL Server 2014](discontinued-functionality-to-sql-server-reporting-services-in-sql-server.md)  
  
  
