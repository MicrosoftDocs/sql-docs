---
title: "Using Reporting Services SOAP Headers | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: reporting-services
ms.topic: "reference"
helpviewer_keywords: 
  - "Web service [Reporting Services], SOAP"
  - "Report Server Web service, SOAP"
  - "SOAP headers [Reporting Services]"
  - "SOAP [Reporting Services], headers"
  - "XML Web service [Reporting Services], SOAP"
ms.assetid: 306d2c06-a25a-40f8-8a35-13dd32e8841e
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Using Reporting Services SOAP Headers
  Communication with a Web service method using SOAP follows a standard format. Part of this format is the data that is encoded in an XML document. The XML document consists of a root **Envelope** element, which in turn consists of a required **Body** element and an optional **Header** element. The **Body** element contains the data specific to the message. The optional **Header** element can contain additional information not directly related to the particular message. Each child element of the **Header** element is called a SOAP header.  
  
 Although the SOAP headers can contain data related to the message, they typically contain information processed by the Web server infrastructure.  
  
 The Report Server Web services define several classes for use in the SOAP header: <xref:ReportService2005.BatchHeader>, <xref:ReportService2010.ItemNamespaceHeader>, <xref:ReportService2010.ServerInfoHeader>, <xref:ReportService2010.TrustedUserHeader>, and <xref:ReportExecution2005.ExecutionHeader>.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[Batching Methods](batching-methods.md)|Describes how to batch multiple operations into a single transaction using <xref:ReportService2005.BatchHeader>.|  
|[Identifying Execution State](identifying-execution-state.md)|Describes how to manage session state in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] using **SessionHeader**.|  
|[Setting the Item Namespace for the GetProperties Method](setting-the-item-namespace-for-the-getproperties-method.md)|Describes how to retrieve properties based on either the path or the ID of an item by using the <xref:ReportService2010.ReportingService2010.GetProperties%2A> method and the <xref:ReportService2010.ItemNamespaceHeader> SOAP header.|  
  
## See Also  
 [Building Applications Using the Web Service and the .NET Framework](../report-server-web-service/net-framework/building-applications-using-the-web-service-and-the-net-framework.md)   
 [Technical Reference &#40;SSRS&#41;](../technical-reference-ssrs.md)  
  
  
