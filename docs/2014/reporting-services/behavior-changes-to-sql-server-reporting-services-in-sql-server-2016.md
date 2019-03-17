---
title: "Behavior Changes to SQL Server Reporting Services  in SQL Server 2014 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "Reporting Services, backward compatibility"
  - "rows [Reporting Services], heights"
  - "leading blanks"
  - "installing Reporting Services, behavior changes with release"
  - "cryptography [Reporting Services]"
  - "Setup [Reporting Services], behavior changes with release"
  - "DefaultValueQueryBased property"
  - "encryption [Reporting Services]"
  - "decryption [Reporting Services]"
  - "blank characters [SQL Server]"
  - "initializing installations [Reporting Services]"
  - "behavior changes [Reporting Services]"
ms.assetid: 2a767f0f-84f2-4099-8784-1e37790f858e
author: markingmyname
ms.author: maghan
manager: kfile
---
# Behavior Changes to SQL Server Reporting Services  in SQL Server 2014
  This topic describes behavior changes in [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)]. Behavior changes affect how features work or interact in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] as compared to previous versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 In this topic:  
  
-   [SQL Server 2014 Reporting Services Behavior Changes](#bkmk_sql14)  
  
-   [SQL Server 2012 Reporting Services Behavior Changes](#bkmk_rc0)  
  
-   [SQL Server 2008 R2 Reporting Services Behavior Changes](#bkmk_kj)  
  
##  <a name="bkmk_sql14"></a> [!INCLUDE[ssSQL14](../includes/sssql14-md.md)] Reporting Services Behavior Changes  
 There are no [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] behavior changes in [!INCLUDE[ssSQL14](../includes/sssql14-md.md)].  
  
##  <a name="bkmk_rc0"></a> [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] Reporting Services Behavior Changes  
 This section describes behavior changes to [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint mode.  
  
### View Items permission will not download Shared Datasets (SharePoint Mode)  
 **New Behavior:** Users with the SharePoint permission of "View Items" can no longer download the contents of Reporting Services shared datasets. This behavior change is now consistent with the "View Items" permissions for reports, data sources, and models. Users with "View Items" permission can view and execute reports, data sources, and models but they cannot download their content.  
  
 **Previous Behavior:** Users with the "View Items" SharePoint permission could download the contents of Reporting Services shared datasets.  
  
 For more information on SharePoint permission levels, see [User permissions and permission levels](https://technet.microsoft.com/library/cc721640.aspx)  
  
### Report Server trace logs are in a new location for SharePoint mode (SharePoint Mode)  
 **New behavior:** For a report server installed in SharePoint mode, the report server trace logs will be under %Programfiles%\Common Files\Microsoft Shared\Web Server Extensions\14\Web Services\ReportServer\LogFiles.  
  
 **Previous Behavior:** Report Server trace logs were found under a path similar to the following:  %Programfilesdir%\Microsoft SQL Server\\<RS_instance>\Reporting Services\LogFiles  
  
### GetServerConfigInfo SOAP API is no longer supported (SharePoint Mode)  
 **New behavior**: Use PowerShell cmdlet "Get-SPRSServiceApplicationServers"  
  
 **Previous Behavior:** Customers could develop SOAP client code to communicate directly with the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] end point, and call GetReportServerConfigInfo().  
  
### Report Server Configuration and Management Tools  
  
#### Configuration Manager is not used for SharePoint Mode  
 **New behavior:** The [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Configuration Manager no longer supports SharePoint Mode report servers. Configuration of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint mode can now be completed by using SharePoint Central administration and therefore [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Configuration Manager no longer supports SharePoint mode. Configuration Manager is now only used for Native mode report servers.  
  
#### You cannot change the server from one mode to another  
 **New behavior:** You cannot change server modes. If you install a report server as Native mode you cannot change or re-configure it to be SharePoint mode. If you install in SharePoint mode, you can change the report server to native mode.  
  
 **Previous Behavior:** Customer installs a [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report server in SharePoint mode. If the customer wants to switch the report server to Native mode, they could open the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] configuration manager to switch to Native mode by either creating a new or connecting to an existing Native mode database. The customer could also use [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] Configuration Manager to switch from SharePoint mode to Native mode.  
  
##  <a name="bkmk_kj"></a> SQL Server 2008 R2 Reporting Services Behavior Changes  
 This section describes behavior changes in [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)].  
  
> [!NOTE]  
>  Because SQL Server 2008 R2 is a minor version upgrade of SQL Server 2008, we recommend that you also review the content in the SQL Server 2008 section.  
  
### SecureConnectionLevel Property in the Reporting Services WMI Provider Library  
 In the WMI provider library for [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)], the **SecureConnectionLevel** property allows values of `0`,`1`,`2`,`3`, with `0` indicating that Secure Socket Layer (SSL) is not required for any of the Web service methods, `3` indicating that SSL is required for all the Web service methods, and `1` and `2` indicate subsets of Web service methods that require SSL. In [!INCLUDE[ssKatmai](../includes/sskatmai-md.md)] [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)], these values will have only two possible meanings:  
  
-   `0` indicates SSL is not required for any of the Web service methods.  
  
-   A positive integer indicates SSL is required for all the Web service methods.  
  
 This change affects how the report server responds to Web service calls. For example, <xref:ReportService2005.ReportingService2005.ListSecureMethods%2A> now returns nothing if **SecureConnectionLevel** is set to 0 and all the methods in <xref:ReportService2005.ReportingService2005> if **SecureConnectionLevel** is set to `1`, `2`, or `3`.  
  
## See Also  
 [What's New &#40;Reporting Services&#41;](what-s-new-reporting-services.md)   
 [Deprecated Features in SQL Server Reporting Services in SQL Server 2014](deprecated-features-in-sql-server-reporting-services-ssrs.md)   
 [Discontinued Functionality to SQL Server Reporting Services in SQL Server 2014](discontinued-functionality-to-sql-server-reporting-services-in-sql-server.md)   
 [Breaking Changes in SQL Server Reporting Services in SQL Server 2014](breaking-changes-in-sql-server-reporting-services-in-sql-server-2016.md)  
  
  
