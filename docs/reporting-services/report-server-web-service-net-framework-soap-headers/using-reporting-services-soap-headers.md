---
title: "Use Reporting Services SOAP headers"
description: Use Reporting Services SOAP headers to batch operations into a single transaction, manage session state, and retrieve properties based on the path or ID of an item.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server-web-service
ms.topic: reference
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "Web service [Reporting Services], SOAP"
  - "Report Server Web service, SOAP"
  - "SOAP headers [Reporting Services]"
  - "SOAP [Reporting Services], headers"
  - "XML Web service [Reporting Services], SOAP"
---
# Use Reporting Services SOAP headers
  Communication with a Web service method using SOAP follows a standard format. Part of this format is the data that is encoded in an XML document. The XML document consists of a root **Envelope** element, which in turn consists of a required **Body** element and an optional **Header** element. The **Body** element contains the data specific to the message. The optional **Header** element can contain additional information not directly related to the particular message. Each child element of the **Header** element is called a SOAP header.  
  
 Although the SOAP headers can contain data related to the message, they typically contain information processed by the Web server infrastructure.  
  
 The Report Server Web services define several classes for use in the SOAP header: <xref:ReportService2005.BatchHeader>, <xref:ReportService2010.ItemNamespaceHeader>, <xref:ReportService2010.ServerInfoHeader>, <xref:ReportService2010.TrustedUserHeader>, and <xref:ReportExecution2005.ExecutionHeader>.  
  
## In this section  
  
|Article|Description|  
|-----------|-----------------|  
|[Batch methods](../../reporting-services/report-server-web-service-net-framework-soap-headers/batching-methods.md)|Describes how to batch multiple operations into a single transaction using <xref:ReportService2005.BatchHeader>.|  
|[Identify the execution state](../../reporting-services/report-server-web-service-net-framework-soap-headers/identifying-execution-state.md)|Describes how to manage session state in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] using **SessionHeader**.|  
|[Set the item namespace for the GetProperties method](../../reporting-services/report-server-web-service-net-framework-soap-headers/setting-the-item-namespace-for-the-getproperties-method.md)|Describes how to retrieve properties based on either the path or the ID of an item by using the <xref:ReportService2010.ReportingService2010.GetProperties%2A> method and the <xref:ReportService2010.ItemNamespaceHeader> SOAP header.|  
  
## Related content

- [Build applications by using the Web service and the .NET framework](../../reporting-services/report-server-web-service/net-framework/building-applications-using-the-web-service-and-the-net-framework.md)
- [Technical reference &#40;SSRS&#41;](../../reporting-services/technical-reference-ssrs.md)
