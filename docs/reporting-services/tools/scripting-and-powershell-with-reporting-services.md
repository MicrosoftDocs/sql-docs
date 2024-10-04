---
title: "Scripting and PowerShell with Reporting Services"
description: Learn about the support for scripting and PowerShell cmdlets for SharePoint mode report servers in Reporting Services.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "scripts [Reporting Services]"
  - "Reporting Services, scripting"
  - "scripting [Reporting Services]"
---
# Scripting and PowerShell with Reporting Services
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] supports a wide range of development and management scenarios through script. These scenarios include the rs.exe command line utility and PowerShell cmdlets for SharePoint mode report servers. You can also use the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] object model from PowerShell for both Native and SharePoint mode.  
  
-   Administrators can write script in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] to automate how they deploy and manage a report server installation. Administrators can also generate and run [!INCLUDE[tsql](../../includes/tsql-md.md)] scripts that create, configure, and update a report server database. Administrators can also use the record and playback script features in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to automate routine maintenance tasks.  
  
-   Developers can create custom applications that include script. You can run script that makes calls to the Report Server Web service. Almost any operation that you can write in managed code can also be written in script.  

- The `RS.exe` utility, a script host running on the report server, processes [!INCLUDE[msCoName] (../../includes/msconame-md.md)] [!INCLUDE[visual-basic](../../includes/visual-basic-md.md)] .NET script as the supported script language in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. 
  `
## Reporting Services SharePoint mode PowerShell cmdlets and samples   
<!--:::image type="icon" source="/analysis-services/analysis-services/instances/install-windows/media/rs-powershellicon.jpg":::-->

[!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode includes [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] cmdlets for report server administration.  
  
-   [PowerShell cmdlets for Reporting Services SharePoint mode](../../reporting-services/report-server-sharepoint/powershell-cmdlets-for-reporting-services-sharepoint-mode.md) Includes the following examples:  
  
    -   Create a service application and proxy  
  
    -   Review and update a delivery extension  
  
    -   Get and set Properties of the Reporting Service Application Database, for example database timeout  
  
    -   List Data Extensions  
  
## Reporting Services object model and PowerShell samples  
<!--:::image type="icon" source="/analysis-services/analysis-services/instances/install-windows/media/rs-powershellicon.jpg":::-->
    
-   [Use PowerShell to change and list Reporting Services subscription owners and run a subscription](../../reporting-services/subscriptions/manage-subscription-owners-and-run-subscription-powershell.md).  
  
-   [Use PowerShell to create an Azure VM With a native mode report server](/previous-versions/azure/dn449661(v=azure.100)).  
  
-   See the section "Access the WMI classes by using PowerShell" in [Access the Reporting Services WMI provider](../../reporting-services/tools/access-the-reporting-services-wmi-provider.md).  
  

## RS.exe scripting samples  
  
-   [Sample Reporting Services rs.exe script to copy content between report servers](../../reporting-services/tools/sample-reporting-services-rs-exe-script-to-copy-content-between-report-servers.md).  
  
-   For more script, application, and extension examples, see [SQL Server Reporting Services product samples](https://go.microsoft.com/fwlink/?LinkId=177889).  
  
## Related content

- [RS.exe utility &#40;SSRS&#41;](../../reporting-services/tools/rs-exe-utility-ssrs.md)
- [Script deployment and administrative tasks](../../reporting-services/tools/script-deployment-and-administrative-tasks.md)
- [Script with the rs.exe utility and the web service](../../reporting-services/tools/script-with-the-rs-exe-utility-and-the-web-service.md)
