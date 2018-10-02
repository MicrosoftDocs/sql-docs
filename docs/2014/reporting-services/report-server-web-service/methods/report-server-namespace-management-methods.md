---
title: "Report Server Namespace Management Methods | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "reports [Reporting Services], managing"
  - "management methods [Reporting Services]"
  - "methods [Reporting Services], about methods"
  - "methods [Reporting Services]"
ms.assetid: 2aa43ce9-f51e-408a-8ce0-b40d3dd62561
author: markingmyname
ms.author: maghan
manager: craigg
---
# Report Server Namespace Management Methods
  The Report Server Management Web service contains methods that you can use to manage reports, folders, and resources in the report server database.  
  
|Method|Action|  
|------------|------------|  
|<xref:ReportService2010.ReportingService2010.CancelJob%2A>|Cancels execution of a job.|  
|<xref:ReportService2010.ReportingService2010.CreateFolder%2A>|Adds a folder to the report server database or SharePoint library.|  
|<xref:ReportService2010.ReportingService2010.CreateCatalogItem%2A>|Adds a new item to a report server database or SharePoint library. This method applies to the `Report`, `Model`, `Dataset`, `Component`, `Resource`, and `DataSource` item types.|  
|M:ReportService2010.ReportingService2010.CreateReportEditSession(System.String,System.String,System.Byte[],ReportService2010.Warning[]@)|Creates a new report edit session.|  
|<xref:ReportService2010.ReportingService2010.DeleteItem%2A>|Removes an item from the report server database or SharePoint library.|  
|<xref:ReportService2010.ReportingService2010.FindItems%2A>|Returns the items in the report server database or SharePoint library that match the specified search criteria.|  
|<xref:ReportService2010.ReportingService2010.FireEvent%2A>|Triggers an event based on the supplied parameters.|  
|<xref:ReportService2010.ReportingService2010.GetExtensionSettings%2A>|Returns a list of settings for a given extension.|  
|<xref:ReportService2010.ReportingService2010.GetItemType%2A>|Retrieves the type of an item in the report server database or SharePoint library, if the item exists.|  
|<xref:ReportService2010.ReportingService2010.GetProperties%2A>|Returns the values of one or more properties on an item in the report server database or SharePoint library.|  
|<xref:ReportService2010.ReportingService2010.GetItemDefinition%2A>|Retrieves the definition or content for an item. This method applies to the `Report`, `Model`, `Dataset`, `Component`, `Resource`, and `DataSource` item types.|  
|<xref:ReportService2010.ReportingService2010.GetItemReferences%2A>|Returns a list of catalog item references associated with an item.|  
|<xref:ReportService2010.ReportingService2010.GetReportServerConfigInfo%2A>|Returns information on the connected report server instance or all the report server instances in a scale-out deployment.|  
|<xref:ReportService2010.ReportingService2010.GetSystemProperties%2A>|Returns one or more system properties.|  
|<xref:ReportService2010.ReportingService2010.ListChildren%2A>|Gets a list of children of a specified folder.|  
|<xref:ReportService2010.ReportingService2010.ListDatabaseCredentialRetrievalOptions%2A>|Returns a list of supported credential retrieval options.|  
|<xref:ReportService2010.ReportingService2010.ListEvents%2A>|Returns a list of event extensions as they appear in the report server configuration file.|  
|<xref:ReportService2010.ReportingService2010.ListJobs%2A>|Returns a list of jobs running on the report server.|  
|<xref:ReportService2010.ReportingService2010.ListExtensions%2A>|Returns a list of extensions that are configured for a given extension type.|  
|<xref:ReportService2010.ReportingService2010.ListExtensionTypes%2A>|Returns a list of supported extension types.|  
|<xref:ReportService2010.ReportingService2010.ListItemTypes%2A>|Returns a list of supported catalog item types.|  
|<xref:ReportService2010.ReportingService2010.ListJobActions%2A>|Returns a list of supported job actions.|  
|<xref:ReportService2010.ReportingService2010.ListJobStates%2A>|Returns a list of supported job states.|  
|<xref:ReportService2010.ReportingService2010.ListJobTypes%2A>|Returns a list of supported job types.|  
|<xref:ReportService2010.ReportingService2010.ListParents%2A>|Retrieves parent items for the given item.|  
|<xref:ReportService2010.ReportingService2010.ListSecurityScopes%2A>|Returns a list of supported security scopes.|  
|<xref:ReportService2010.ReportingService2010.Logoff%2A>|Logs out the current user making Web service requests. This method only applies to native mode.|  
|<xref:ReportService2010.ReportingService2010.LogonUser%2A>|Logs on a user and authenticates a user request to the Report Server Web service. This method only applies to native mode.|  
|<xref:ReportService2010.ReportingService2010.SetItemReferences%2A>|Sets the catalog items associated with an item.|  
|<xref:ReportService2010.ReportingService2010.MoveItem%2A>|Moves and/or renames an item.|  
|<xref:ReportService2010.ReportingService2010.SetProperties%2A>|Sets one or more properties of an item.|  
|<xref:ReportService2010.ReportingService2010.SetItemDefinition%2A>|Sets the definition or content for a specified item. This method applies to the `Report`, `Model`, `Dataset`, `Component`, `Resource`, and `DataSource` item types.|  
|<xref:ReportService2010.ReportingService2010.SetSystemProperties%2A>|Sets one or more system properties in the report server or SharePoint farm.|  
|<xref:ReportService2010.ReportingService2010.ValidateExtensionSettings%2A>|Validates [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] extension settings.|  
  
## See Also  
 [Building Applications Using the Web Service and the .NET Framework](../net-framework/building-applications-using-the-web-service-and-the-net-framework.md)   
 [Report Server Web Service](../report-server-web-service.md)   
 [Report Server Web Service Methods](report-server-web-service-methods.md)   
 [Technical Reference &#40;SSRS&#41;](../../technical-reference-ssrs.md)  
  
  
