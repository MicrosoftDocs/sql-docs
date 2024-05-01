---
title: "Authorization methods"
description: In Reporting Services, you can use these authorization methods to manage tasks, roles, and policies on the report server.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/06/2017
ms.service: reporting-services
ms.subservice: report-server-web-service
ms.topic: reference
ms.custom: updatefrequency5
helpviewer_keywords:
  - "security [Reporting Services], reports"
  - "authorization [Reporting Services]"
  - "reports [Reporting Services], security"
  - "tasks [Reporting Services]"
  - "roles [Reporting Services], methods"
---
# Authorization methods
  You can use these methods to manage tasks, roles, and policies on the report server.  
  
|Method|Action|  
|------------|------------|  
|<xref:ReportService2010.ReportingService2010.CreateRole%2A>|Adds a new role to the report server database. This method =applies to native mode only.|  
|<xref:ReportService2010.ReportingService2010.DeleteRole%2A>|Deletes a role from the report server database. This method applies to native mode only.|  
|<xref:ReportService2010.ReportingService2010.GetPermissions%2A>|Returns the user permissions that are associated with a particular item in the report server database or SharePoint library.|  
|<xref:ReportService2010.ReportingService2010.GetPolicies%2A>|Returns the policies that are associated with a particular item in the report server database or SharePoint library.|  
|<xref:ReportService2010.ReportingService2010.GetRoleProperties%2A>|Returns role metadata properties and a collection of associated tasks.|  
|<xref:ReportService2010.ReportingService2010.GetSystemPermissions%2A>|Returns the user's system permissions. This method applies to native mode only.|  
|<xref:ReportService2010.ReportingService2010.GetSystemPolicies%2A>|Returns the system policies, including groups and roles with which they're associated. This method applies to native mode only.|  
|<xref:ReportService2010.ReportingService2010.InheritParentSecurity%2A>|Deletes the policies that are associated with a particular item in the report server database and sets the security policies for the item to be the same as its parent.|  
|<xref:ReportService2010.ReportingService2010.IsSSLRequired%2A>|Returns a Boolean value that indicates whether the Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL), protocol is required to use the <xref:ReportService2010> end point.|  
|<xref:ReportService2010.ReportingService2010.ListRoles%2A>|Returns the names and descriptions of roles that the report server manages.|  
|<xref:ReportExecution2005.ReportExecutionService.ListSecureMethods%2A>|Returns a list of Simple Object Access Protocol (SOAP) methods in the <xref:ReportExecution2005> endpoint that require a secure connection when invoked. The **SecureConnectionLevel** setting of the report server is used to determine which methods are returned.|  
|<xref:ReportService2010.ReportingService2010.ListTasks%2A>|Returns the tasks that the report server manages.|  
|<xref:ReportService2010.ReportingService2010.SetPolicies%2A>|Sets the policies that are associated with a specified item.|  
|<xref:ReportService2010.ReportingService2010.SetRoleProperties%2A>|Sets role metadata properties and associates a set of tasks with a role. This method applies to native mode only.|  
|<xref:ReportService2010.ReportingService2010.SetSystemPolicies%2A>|Sets the system policy that defines groups and their associated roles. This method applies to native mode only.|  
  
## Related content  
 [Building Applications Using the Web Service and the .NET Framework](../../../reporting-services/report-server-web-service/net-framework/building-applications-using-the-web-service-and-the-net-framework.md)   
 [Report Server Web Service](../../../reporting-services/report-server-web-service/report-server-web-service.md)   
 [Report Server Web Service Methods](../../../reporting-services/report-server-web-service/methods/report-server-web-service-methods.md)   
 [Technical Reference &#40;SSRS&#41;](../../../reporting-services/technical-reference-ssrs.md)  
  
  
