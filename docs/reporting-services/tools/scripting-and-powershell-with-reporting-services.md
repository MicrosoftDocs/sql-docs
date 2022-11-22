---
title: "Scripting and PowerShell with Reporting Services | Microsoft Docs"
description: Learn about the support for scripting and using PowerShell cmdlets for SharePoint mode report servers in Reporting Services. 
ms.date: 09/14/2015
ms.service: reporting-services
ms.subservice: tools


ms.topic: conceptual
helpviewer_keywords: 
  - "scripts [Reporting Services]"
  - "Reporting Services, scripting"
  - "scripting [Reporting Services]"
ms.assetid: 1ac2646d-ed5a-4436-b18f-2150c33f3d87
author: maggiesMSFT
ms.author: maggies
---
# Scripting and PowerShell with Reporting Services
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] supports a wide range of development and management scenarios through script, including the rs.exe command line utility, PowerShell cmdlets for SharePoint mode report servers, and leveraging the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] object model from PowerShell for both Native and SharePoint mode.  
  
-   Administrators can write script in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] to automate how they deploy and manage a report server installation. Administrators can also generate and run [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts that create, configure, and update a report server database. Administrators can also use the record and playback script features in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to automate routine maintenance tasks.  
  
-   Developers can create custom applications that include script. You can run script that makes calls to the Report Server Web service. Almost any operation that you can write in managed code can also be written in script.  
  
-   [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] supports [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[vbprvb](../../includes/vbprvb-md.md)] .NET script as the script language that can be processed by the RS.exe utility, a script host that runs on the report server.  
  
## Reporting Services SharePoint mode PowerShell cmdlets and samples  
 ![PowerShell related content](/analysis-services/analysis-services/instances/install-windows/media/rs-powershellicon.jpg "PowerShell related content")  
  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode includes [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] cmdlets for report server administration.  
  
-   [PowerShell cmdlets for Reporting Services SharePoint Mode](../../reporting-services/report-server-sharepoint/powershell-cmdlets-for-reporting-services-sharepoint-mode.md) Includes the following examples:  
  
    -   Create a service application and proxy  
  
    -   Review and update a delivery extension  
  
    -   Get and set Properties of the Reporting Service Application Database, for example database timeout  
  
    -   List Data Extensions  
  
## Reporting Services Object model and Powershell samples  
 ![PowerShell related content](/analysis-services/analysis-services/instances/install-windows/media/rs-powershellicon.jpg "PowerShell related content")  
  
 PowerShell calling the core object model and for the most part valid for SharePoint and native mode, for example the migration work, subscription work, and more related samples for subscriptions work in SQL15.  
  
-   [Use PowerShell to Change and List Reporting Services Subscription Owners and Run a Subscription](../../reporting-services/subscriptions/manage-subscription-owners-and-run-subscription-powershell.md).  
  
-   [Use PowerShell to Create an Azure VM With a Native Mode Report Server](/previous-versions/azure/dn449661(v=azure.100)).  
  
-   See the section "Access the WMI classes using PowerShell" in [Access the Reporting Services WMI Provider](../../reporting-services/tools/access-the-reporting-services-wmi-provider.md).  
  

## RS.exe scripting samples  
  
-   [Sample Reporting Services rs.exe Script to Copy Content between Report Servers](../../reporting-services/tools/sample-reporting-services-rs-exe-script-to-copy-content-between-report-servers.md).  
  
-   For additional script, application, and extension examples, see [SQL Server Reporting Services Product Samples](https://go.microsoft.com/fwlink/?LinkId=177889).  
  
## See Also  
 [RS.exe Utility &#40;SSRS&#41;](../../reporting-services/tools/rs-exe-utility-ssrs.md)   
 [Script Deployment and Administrative Tasks](../../reporting-services/tools/script-deployment-and-administrative-tasks.md)   
 [Script with the rs.exe Utility and the Web Service](../../reporting-services/tools/script-with-the-rs-exe-utility-and-the-web-service.md)  
  
