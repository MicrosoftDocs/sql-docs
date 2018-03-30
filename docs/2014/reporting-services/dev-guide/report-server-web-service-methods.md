---
title: "Report Server Web Service Methods | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "Report Server Web service, methods"
  - "Web service [Reporting Services], methods"
  - "XML Web service [Reporting Services], features"
  - "Web service [Reporting Services], features"
  - "Report Server Web service, features"
  - "XML Web service [Reporting Services], methods"
ms.assetid: ce5afa27-e90c-44a7-b204-098a065b3665
caps.latest.revision: 48
author: "douglaslM"
ms.author: "carlasab"
manager: "jhubbard"
---
# Report Server Web Service Methods
  The Report Server Web services include several categories of methods that are based on component features. These methods are provided through several Web service endpoints (three for report management and one for report execution) which are exposed as members of the <xref:ReportService2010.ReportingService2010> and <xref:ReportExecution2005.ReportExecutionService> classes. These classes can be generated through a proxy class tool such as wsdl.exe, which is included with the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)] SDK. For more information about the Report Server Web services and the [!INCLUDE[dnprdnshort](../../../includes/dnprdnshort-md.md)], see [Building Applications Using the Web Service and the .NET Framework](../../../2014/reporting-services/dev-guide/building-applications-using-the-web-service-and-the-net-framework.md).  
  
## Endpoints and Methods  
 The following table lists the endpoints of the Report Server Web service, and the categories of methods provided by the <xref:ReportService2010.ReportingService2010> endpoint. For information on the methods available in the other endpoints, see [Technical Reference &#40;SSRS&#41;](../../../2014/reporting-services/technical-reference-ssrs.md).  
  
|Topic|Description|  
|-----------|-----------------|  
|[Report Server Web Service Endpoints](../../../2014/reporting-services/dev-guide/report-server-web-service-endpoints.md)|Describes the management and execution endpoints of the Report Server Web service.|  
|[Report Server Namespace Management Methods](../../../2014/reporting-services/dev-guide/report-server-namespace-management-methods.md)|Describes methods that you can use to manage the report server database. Specifically you can manage folders and resources and set item properties.|  
|[Authorization Methods](../../../2014/reporting-services/dev-guide/authorization-methods.md)|Describes methods that you can use to manage tasks, roles, and policies.|  
|[Data Sources and Connection Methods](../../../2014/reporting-services/dev-guide/data-sources-and-connection-methods.md)|Describes methods that you can use to set and manage data source connection and credential information for reports.|  
|[Report Parameters Methods](../../../2014/reporting-services/dev-guide/report-parameters-methods.md)|Describes methods that you can use to set and retrieve parameters for reports.|  
|[Model Methods](../../../2014/reporting-services/dev-guide/model-methods.md)|Describes methods that you can use to manage models.|  
|[Rendering and Execution Methods](../../../2014/reporting-services/dev-guide/rendering-and-execution-methods.md)|Describes methods that you can use to manage report execution, rendering, and caching.|  
|[Report History Methods](../../../2014/reporting-services/dev-guide/report-history-methods.md)|Describes methods that you can use to create and manage report history snapshots.|  
|[Scheduling Methods](../../../2014/reporting-services/dev-guide/scheduling-methods.md)|Describes methods that you can use to create and manage shared schedules and cache refresh plans that are used by the report server.|  
|[Subscription and Delivery Methods](../../../2014/reporting-services/dev-guide/subscription-and-delivery-methods.md)|Describes methods that you can use to create and manage subscriptions and report delivery.|  
|[Linked Reports Methods](../../../2014/reporting-services/dev-guide/linked-reports-methods.md)|Describes methods that you can use to create and manage linked reports.|  
  
## See Also  
 [Accessing the SOAP API](../../../2014/reporting-services/dev-guide/accessing-the-soap-api.md)   
 [Building Applications Using the Web Service and the .NET Framework](../../../2014/reporting-services/dev-guide/building-applications-using-the-web-service-and-the-net-framework.md)   
 [Report Server Web Service](../../../2014/reporting-services/dev-guide/report-server-web-service.md)   
 [Technical Reference &#40;SSRS&#41;](../../../2014/reporting-services/technical-reference-ssrs.md)  
  
  