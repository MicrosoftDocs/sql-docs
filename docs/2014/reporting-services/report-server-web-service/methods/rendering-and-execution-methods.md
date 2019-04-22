---
title: "Rendering and Execution Methods | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "rendered reports [Reporting Services]"
  - "reports [Reporting Services], execution options"
  - "methods [Reporting Services], execution options"
  - "methods [Reporting Services], rendering"
ms.assetid: 12626aad-f0be-4653-87d0-60eb3a3fff78
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Rendering and Execution Methods
  You can use these methods to manage item execution and caching, and report rendering.  
  
|Method|Action|  
|------------|------------|  
|<xref:ReportService2010.ReportingService2010.FlushCache%2A>|Invalidates the cache for an item.|  
|<xref:ReportService2010.ReportingService2010.GetCacheOptions%2A>|Returns the cache configuration for an item and the settings that describe when the cached copy of the item expires.|  
|<xref:ReportService2010.ReportingService2010.GetExecutionOptions%2A>|Returns the execution option and associated settings for an individual item.|  
|<xref:ReportService2010.ReportingService2010.ListExecutionSettings%2A>|Returns a list of supported execution settings.|  
|<xref:ReportExecution2005.ReportExecutionService.Render%2A>|Processes the specified report and renders it in a specified format.|  
|<xref:ReportService2010.ReportingService2010.SetCacheOptions%2A>|Configures an item to be cached and provides settings that specify when the cached copy of the item expires.|  
|<xref:ReportService2010.ReportingService2010.SetExecutionOptions%2A>|Sets execution options and associated execution properties for a specified item.|  
|<xref:ReportService2010.ReportingService2010.UpdateItemExecutionSnapshot%2A>|Generates an item execution snapshot for a specified item.|  
  
## See Also  
 [Building Applications Using the Web Service and the .NET Framework](../net-framework/building-applications-using-the-web-service-and-the-net-framework.md)   
 [Report Server Web Service](../report-server-web-service.md)   
 [Report Server Web Service Methods](report-server-web-service-methods.md)   
 [Technical Reference &#40;SSRS&#41;](../../technical-reference-ssrs.md)  
  
  
