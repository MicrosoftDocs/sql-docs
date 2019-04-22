---
title: "Reporting Services Properties | Microsoft Docs"
ms.custom: ""
ms.date: "03/09/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: reporting-services
ms.topic: "reference"
helpviewer_keywords: 
  - "report servers [Reporting Services], properties"
  - "properties [Reporting Services], about properties"
  - "reports [Reporting Services], properties"
  - "Report Server Web service, properties"
  - "report properties [Reporting Services]"
  - "XML Web service [Reporting Services], properties"
  - "Web service [Reporting Services], properties"
  - "properties [Reporting Services]"
ms.assetid: 8c855194-4c20-4ecc-a328-5137d54b560c
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Reporting Services Properties
  The report server defines a set of system properties that are global to the report server and a set of item properties that are associated with an individual item stored in the report server database. Properties defined by the report server cannot be deleted, and in some cases they are read-only. An application can extend system properties and item properties by adding additional user-defined properties to the system and item properties.  
  
 The following Web service methods retrieve and set [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] properties.  
  
|Method|Action|  
|------------|------------|  
|<xref:ReportService2010.ReportingService2010.GetProperties%2A>|Returns the values of one or more properties on an item in the report server database.|  
|<xref:ReportService2010.ReportingService2010.GetSystemProperties%2A>|Returns one or more system properties.|  
|<xref:ReportService2010.ReportingService2010.SetProperties%2A>|Sets one or more properties of an item in the report server database.|  
|<xref:ReportService2010.ReportingService2010.SetSystemProperties%2A>|Sets one or more system properties.|  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Report Server Item Properties](reporting-services-properties-report-server-item-properties.md)|Describes the item-specific properties in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)].|  
|[Report Server System Properties](reporting-services-properties-report-server-system-properties.md)|Describes the system-specific properties in [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)].|  
  
## See Also  
 [Building Applications Using the Web Service and the .NET Framework](building-applications-using-the-web-service-and-the-net-framework.md)   
 [Report Server Web Service](../report-server-web-service.md)   
 [Technical Reference &#40;SSRS&#41;](../../technical-reference-ssrs.md)  
  
  
