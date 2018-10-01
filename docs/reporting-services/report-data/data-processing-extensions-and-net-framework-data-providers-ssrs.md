---
title: "Data Processing Extensions and .NET Framework Data Providers (SSRS) | Microsoft Docs"
ms.date: 03/14/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-data


ms.topic: conceptual
helpviewer_keywords: 
  - "reports [Reporting Services], data"
  - "data processing extensions [Reporting Services]"
  - "data providers [Reporting Services]"
  - "data retrieval [Reporting Services]"
  - "Reporting Services, data sources"
  - "report data [Report Builder], accessing"
ms.assetid: 42a5afb5-f4c8-4957-b1fd-77bf39afa5be
author: markingmyname
ms.author: maghan
---
# Data Processing Extensions and .NET Framework Data Providers (SSRS)
  A [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension is a component installed with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], designed to retrieve data from a specific type of data source and to provide extra functionality to support report design and report processing. A [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] data provider is a component available from [!INCLUDE[msCoName](../../includes/msconame-md.md)] or third-party sources that supports <xref:System.Data> interfaces that allow you to retrieve and to modify data from a specific type of data source.  
  
## Understanding a Data Processing Extension  
 A [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension supports a subset of the <xref:System.Data> interfaces. Data processing extensions require only read-only access to a data source, so the interfaces for write and update are not implemented. Each data processing extension can provide custom features to support report processing. For example, a data processing extension might support the following types of features:  
  
-   Managing credentials separately from the connection string  
  
-   Supporting multivalue parameters  
  
-   Retrieving server aggregates, which are calculated on the data source  
  
-   Retrieving data properties as well as data values from the data source  
  
## Understanding a Data Provider  
 A [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] data provider (sometimes known as a driver) supports a standard set of <xref:System.Data> interfaces for reading, writing, and updating data on a data source. A data provider can be used when there is no data processing extension available for a specific type of data source. Many third-party standard [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] data providers are available.  
  
 Because [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] has an extensible data provider architecture, you can build a custom data processing extension to include the extra functionality supplied by [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extensions. For more information, see [Implementing a Data Processing Extension](../../reporting-services/extensions/data-processing/implementing-a-data-processing-extension.md). For third-party data processing extensions, see the documentation that comes with the third-party data processing extension.  
  
> [!NOTE]  
>  A [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] data provider or custom data processing extension must be installed and registered before it can be used to access data from a data source. The data processing extension must be installed and registered both on the reporting client to author the report and on the report server to view the published report. Not all data providers are designed to work in a server environment. For more information, see [Register a Standard .NET Framework Data Provider &#40;SSRS&#41;](../../reporting-services/report-data/register-a-standard-net-framework-data-provider-ssrs.md).and [Deploying a Data Processing Extension](../../reporting-services/extensions/data-processing/deploying-a-data-processing-extension.md).  
  
## See Also  
 [Data Processing Extensions Overview](../../reporting-services/extensions/data-processing/data-processing-extensions-overview.md)   
 [Report Embedded Datasets and Shared Datasets &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md)  
  
  
