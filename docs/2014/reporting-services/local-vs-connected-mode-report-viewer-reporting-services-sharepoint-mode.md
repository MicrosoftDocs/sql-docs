---
title: "Local Mode vs. Connected Mode Reports in the Report Viewer (Reporting Services in SharePoint Mode) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: a230a9bb-6046-401f-b5e5-53ff6edf2264
author: markingmyname
ms.author: maghan
manager: craigg
---
# Local Mode vs. Connected Mode Reports in the Report Viewer (Reporting Services in SharePoint Mode)
  [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] reports can be configure to run in either *local mode* or *connected mode*, which leverages a [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report server. Instead, you can use the Report Viewer to directly render reports from SharePoint when the data extension supports local mode reporting. This approach is called *local mode*. In previous versions of [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)], the SharePoint farm was required to be connected to a [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report server configured in SharePoint mode so the Report Viewer control could render reports. This approach is called *remote mode* or *connected mode*.  
  
 In *local mode* there is no [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report server. You must install the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] add-in for SharePoint products, but no [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report server is required. With Local mode, users can view reports but will not have access to server side features such as subscriptions and data alerts.  
  
||  
|-|  
|**[!INCLUDE[applies](../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint mode|  
  
 **In this topic:**  
  
-   [Local mode vs connected mode and supported extensions](#bkmk_local_vs_connected)  
  
-   [Configure Local Mode and Access Services with SharePoint 2013](#bkmk_local_mode_sharepoint2013)  
  
-   [Configure Local Mode Reporting with SharePoint 2010](#bkmk_local_mode_sharepoint2010)  
  
##  <a name="bkmk_local_vs_connected"></a> Local mode vs connected mode and supported extensions  
 **Local mode:** When you have a data extension that supports local mode, the Report Viewer directly renders reports from SharePoint. In *local mode* there is no [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report server. You must install the [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] add-in for SharePoint products, but no [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report server is required. With Local mode, users can view reports but will **not** have access to server side features such as subscriptions and data alerts.  
  
 **Connected mode**, also called *remote mode* requires a [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] report server in SharePoint mode, connected to the SharePoint farm so the Report Viewer control could render reports..  
  
 The following is a list of the data processing extensions that support local mode reporting:  
  
-   [!INCLUDE[msCoName](../includes/msconame-md.md)] Access 2010 reporting extension. For more information on Access Services, see [Use Access Services with SQL Reporting Services: Installing SQL Server 2008 R2 Reporting Services Add-In (SharePoint Server 2010)](https://go.microsoft.com/fwlink/?LinkId=192686).  
  
-   The [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] SharePoint list data extension. For more information on the SharePoint List Data Extension, see [Data Sources Supported by Reporting Services &#40;SSRS&#41;](create-deploy-and-manage-mobile-and-paginated-reports.md)  
  
 Custom data processing extensions can also be developed to support local mode. For more information, see [Implementing a Data Processing Extension](extensions/data-processing/implementing-a-data-processing-extension.md).  
  
 Local mode supports rendering reports that have an embedded data source or a shared data source from an .rsds file. However, you cannot manage the report or its associated data source. If you try to do this, you will receive an error that this is not supported in local mode. Managing data sources in the SharePoint site is supported in only connected mode.  
  
> [!NOTE]  
>  As with previous versions, you cannot embed user names and passwords in the .rsds file.  
  
##  <a name="bkmk_local_mode_sharepoint2013"></a> Configure Local Mode and Access Services with SharePoint 2013  
 You can configure your SharePoint 2013 farm to support existing Access 2010 web databases and [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] local mode. For more information, see [Set up and configure Access Services 2010 for web databases in SharePoint Server 2013](https://technet.microsoft.com/library/ee748653\(office.15\).aspx).  
  
 It is not possible to create new Access web databases for SharePoint 2013. Access 2013 uses a new type of database, *Access web app* that you build in Access, then use and share with others as a SharePoint app in a web browser.  
  
 For more information, see the following.  
  
-   [What's new in Access 2013](http://office.microsoft.com/access-help/what-s-new-in-access-2013-HA102809500.aspx) (http://office.microsoft.com/access-help/what-s-new-in-access-2013-HA102809500.aspx).  
  
-   [Basic tasks for an Access app](http://office.microsoft.com/access-help/basic-tasks-for-an-access-app-HA102840210.aspx?CTT=5&origin=HA102809500) (http://office.microsoft.com/access-help/basic-tasks-for-an-access-app-HA102840210.aspx?CTT=5&origin=HA102809500).  
  
##  <a name="bkmk_local_mode_sharepoint2010"></a> Configure Local Mode Reporting with SharePoint 2010  
 Local mode requires ASP.NET session state. The installation of Access services will enable ASP.Net sessions state. You can also enable using PowerShell.  
  
1.  Open the SharePoint 2010 Management Shell.  
  
2.  Type the following command:  
  
    ```  
    - Enable-SPSessionStateService  
    ```  
  
3.  When prompted, type the name of your database.  
  
4.  Perform an IIS reset.  
  
 For more information, see [Use Access Services with SQL Reporting Services: Installing SQL Server 2008 R2 Reporting Services Add-In (SharePoint Server 2010)](https://go.microsoft.com/fwlink/?LinkId=192686) and [Enable-SPSessionStateService](https://technet.microsoft.com/library/ff607857\(v=office.15\).aspx).  
  
## Connected mode  
 For the latest information on using ADS extension with [!INCLUDE[ssRSnoversion](../includes/ssrsnoversion-md.md)] connected mode, see [Access Services Report in SharePoint Site shows error in data extension 'ADS'](https://social.technet.microsoft.com/wiki/contents/articles/25298.access-services-report-in-sharepoint-site-shows-error-in-data-extension-ads.aspx).  
  
## See Also  
 [Data Sources Supported by Reporting Services &#40;SSRS&#41;](create-deploy-and-manage-mobile-and-paginated-reports.md)  
  
  
