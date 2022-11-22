---
description: "Compare shared and embedded data sources - Report Builder & Reporting Services (SSRS)"
title: "Compare shared and embedded  data sources - Report Builder & Reporting Services | Microsoft Docs"
ms.date: 11/18/2019
ms.service: reporting-services
ms.subservice: report-data


ms.topic: conceptual
author: maggiesMSFT
ms.author: maggies
---
# Compare shared and embedded data sources - Report Builder & Reporting Services (SSRS)

[!INCLUDE [ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-2016-and-later](../../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../../includes/ssrs-appliesto-pbirs.md)] [!INCLUDE[ssrs-appliesto-sharepoint-2013-2016i](../../includes/ssrs-appliesto-sharepoint-2013-2016.md)]
 
You can connect to data with either a shared or an embedded data source. A *shared data source* is defined independent of any report. You can use it in multiple reports on a report server or SharePoint site. An *embedded data source* is defined in a report. You can only use it in that report. 

 Shared data sources are useful when you have data sources that you use often. We recommend that you create and use shared data sources as much as possible. They make reports and report access easier to manage, and help to keep reports and the data sources they access more secure. If you need a shared data source, you may need to ask your system administrator to create one for you.  
  
 An embedded data source, also known as a *report-specific data source*, is a data connection that's saved in the report definition. Embedded data source connection information can be used only by the report in which it's embedded. To define and manage embedded data sources, use the **Data Source Properties** dialog box.  
  
 The difference between embedded and shared data sources is in how they are created, stored, and managed.  
  
-   In Report Designer, create embedded or shared data sources as part of a [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] project. You can control whether to use them locally for preview or to deploy them as part of the project to a report server or SharePoint site. You can use custom data extensions that have been installed on your computer and on the report server or SharePoint site where you deploy your reports.  
  
     System administrators can install and configure additional data processing extensions and .NET Framework data providers. For more information, see [Data Processing Extensions and .NET Framework Data Providers &#40;SSRS&#41;](../../reporting-services/report-data/data-processing-extensions-and-net-framework-data-providers-ssrs.md).  
  
     Developers can use the <xref:Microsoft.ReportingServices.DataProcessing> API to create data processing extensions to support additional types of data sources.  
  
-   In [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)], browse to a report server or SharePoint site and select shared data sources or create embedded data sources in the report. You can't create a shared data source in [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)]. You can't use custom data extensions in [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)].  

## Summary of differences
  
 The following table summarizes the differences between embedded and shared data sources.  
  
|Description|Embedded<br /><br /> Data Source|Shared<br /><br /> Data Source|  
|-----------------|------------------------------|----------------------------|  
|Data connection is embedded in the report definition.|![Available](../../reporting-services/report-data/media/greencheck.gif "Available")||  
|Pointer to the data connection on the report server is embedded in the report definition.||![Available](../../reporting-services/report-data/media/greencheck.gif "Available")|  
|Managed on the report server|![Available](../../reporting-services/report-data/media/greencheck.gif "Available")|![Available](../../reporting-services/report-data/media/greencheck.gif "Available")|  
|Required for shared datasets||![Available](../../reporting-services/report-data/media/greencheck.gif "Available")|  
|Required for components||![Available](../../reporting-services/report-data/media/greencheck.gif "Available")|  

## Next steps

[Create and Manage Shared Data Sources](../../reporting-services/report-data/create-modify-and-delete-shared-data-sources-ssrs.md)   
[Create and Modify Embedded Data Sources](../../reporting-services/report-data/create-and-modify-embedded-data-sources.md)   
[Set Deployment Properties](../../reporting-services/tools/set-deployment-properties-reporting-services.md)   
[Specify Credential and Connection Information for Report Data Sources](../../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md)  

More questions? [Try asking the Reporting Services forum](https://go.microsoft.com/fwlink/?LinkId=620231)
