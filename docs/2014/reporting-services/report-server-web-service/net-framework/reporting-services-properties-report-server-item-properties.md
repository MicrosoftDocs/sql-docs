---
title: "Report Server Item Properties | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "docset-sql-devref"
  - "reporting-services-native"
ms.topic: "reference"
helpviewer_keywords: 
  - "report servers [Reporting Services], properties"
  - "item-specific properties [Reporting Services]"
  - "report items [Reporting Services], properties"
  - "items [Reporting Services], properties"
ms.assetid: 21edec6d-9897-48fb-8c75-182305b1dbdb
author: markingmyname
ms.author: maghan
manager: craigg
---
# Report Server Item Properties
  Item properties are properties that are specific to items in the report server database. Such items include reports, linked reports, folders, resources, models, and data sources.  
  
 The following item property names are reserved. You cannot create user-defined properties of the same name. You can read or modify many of these properties using the Report Server Web service methods.  
  
## Item Properties  
 The following properties apply to all items in the report server database.  
  
|Property|Description|  
|--------------|-----------------|  
|**CreatedBy**|The name of the user who originally added the item to the report server database.|  
|**CreationDate**|The date and time the item was added to the report server database.|  
|**Description**|The description of the item.|  
|**Hidden**|A value that indicates whether the item is visible and available to users.|  
|**ID**|The ID of an item in the report server database.|  
|**ModifiedBy**|The name of the user who last modified the item in the report server database.|  
|**ModifiedDate**|The date and time the user last modified the item.|  
|**Name**|The name of an item in the report server database.|  
|**Path**|The full path name of the item. The path of any item in the report server database has a maximum character length of 260.|  
|**Size**|The size, in bytes, of an item in the report server database.|  
|**Type**|The type of an item in the report server database.|  
|**VirtualPath**|The virtual path to an item in the report server database. The value of the <xref:ReportService2010.CatalogItem.VirtualPath%2A> property is the path under which a user expects to see the item. For example, a report called report1, which is located in the user's personal My Reports folder, has a virtual path of /My Reports. The actual path of the item is /Users/username/My Reports.|  
  
## Folder Properties  
 In addition to the item properties listed previously, the following property applies to folders in the report server database.  
  
|Property|Description|  
|--------------|-----------------|  
|**Reserved**|A value returned by the <xref:ReportService2010.ReportingService2010.GetProperties%2A> method for folders that are reserved by the report server. Reserved folders include Users, My Reports, and /. Reserved folders cannot be modified or removed.|  
  
## Report Properties  
 In addition to the item properties listed previously, the following properties apply to reports in the report server database.  
  
|Property|Description|  
|--------------|-----------------|  
|**Language**|The language used in a report. The value is a language code defined in the Internet Engineering Task Force (IETF) RFC1766 specification. The first part is a two-character designation of the basic language. The second part is separated by a hyphen and designates the variation or dialect of the language. If a value is not specified in the `Style` element associated with the `Body` element in the report definition, the default value is the language of the report server.|  
|`ReportProcessingTimeout`|The time-out, in seconds, for an individual report. If this value is set, the report server attempts to stop the processing of a report when the specified time has elapsed. Valid values are `-1` through `2`,`147`,`483`,`647`. If the value is `-1`, the report does not time out during processing. If the value is `null`, the value of the system property `ReportProcessingTimeout` is used for the report processing time-out. The default value is `null`. For more information, see [Report Server System Properties](reporting-services-properties-report-server-system-properties.md).|  
|**ExecutionDate**|The date and time at which a report snapshot was last created for a report.|  
|**CanRunUnattended**|A value that indicates whether a report can be run unattended on a schedule. If this property is set to `true`, default values for report parameters are defined and data source credentials are stored with the report, or credential retrieval option is set to `None`. If this property is set to `false`, the prerequisites for running a report unattended are not met. Please see [Configure the Unattended Execution Account &#40;SSRS Configuration Manager&#41;](../../install-windows/configure-the-unattended-execution-account-ssrs-configuration-manager.md) for more information.|  
|**HasParameterDefaultValues**|A value that indicates whether the report has valid default values set for all report parameters. The value is also `true` if a report does not have report parameters. If this property set to `false`, one or more report parameters do not have a valid default value.|  
|**HasDataSourceCredentials**|A value that indicates that the credential retrieval option set for all data sources associated with the report is either `None` or `Store`. If this property is set to `false`, a credential retrieval option set for one of the data sources associated with the report is either `Integrated` or `Prompt`.|  
|**IsSnapshotExecution**|A value that indicates whether the report is a snapshot.|  
|**HasScheduleReadyDataSources**|A value that indicates whether the data sources of a report are configured to support scheduled execution. If this property is set to `false`, users cannot subscribe to the report.|  
  
## Resource Properties  
 In addition to the item properties listed previously, the following property applies to resources in the report server database.  
  
|Property|Description|  
|--------------|-----------------|  
|**MimeType**|The MIME type of a resource in the report server database.|  
  
## See Also  
 [Building Applications Using the Web Service and the .NET Framework](building-applications-using-the-web-service-and-the-net-framework.md)   
 [Report Server Web Service](../report-server-web-service.md)   
 [Technical Reference &#40;SSRS&#41;](../../technical-reference-ssrs.md)  
  
  
