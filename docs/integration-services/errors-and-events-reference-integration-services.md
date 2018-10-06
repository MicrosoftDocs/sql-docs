---
title: "Errors and Events Reference (Integration Services) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Integration Services packages, events"
  - "events [Integration Services]"
  - "errors [Integration Services]"
  - "Integration Services, errors"
ms.assetid: cf4f0f14-8087-42d7-9b67-e4929228abd6
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Errors and Events Reference (Integration Services)
  This section of the documentation contains information about several errors and events related to [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]. Cause and resolution information is included for error messages.  
  
 For more information about [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] error messages, including a list of most [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] errors and their descriptions, see [Integration Services Error and Message Reference](../integration-services/integration-services-error-and-message-reference.md). However, the list currently does not include troubleshooting information.  
  
> [!IMPORTANT]  
>  Many of the error messages that you may see when you are working with [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] come from other components. These may include OLE DB providers, other database components such as the [!INCLUDE[ssDE](../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)] , or other services or components such as the file system, the SMTP server, or Microsoft Message Queueing. To find information about these external error messages, see the documentation specific to the component.  
  
## Error Messages  
  
|Symbolic name of error|Description|  
|----------------------------|-----------------|  
|DTS_E_CACHELOADEDFROMFILE|Indicates that the package cannot run because a Cache Transform transformation is trying to write data to the in-memory cache. However, a Cache connection manager has already loaded a cache file into the in-memory cache.|  
|DTS_E_CANNOTACQUIRECONNECTIONFROMCONNECTIONMANAGER|Indicates that the package cannot run because a specified connection failed.|  
|DTS_E_CANNOTCONVERTBETWEENUNICODEANDNONUNICODESTRINGCOLUMN|Indicates that a data flow component is trying to pass Unicode string data to another component that expects non-Unicode string data in the corresponding column, or vice versa.|  
|DTS_E_CANNOTCONVERTBETWEENUNICODEANDNONUNICODESTRINGCOLUMNS|Indicates that a data flow component is trying to pass Unicode string data to another component that expects non-Unicode string data in the corresponding column, or vice versa.|  
|DTS_E_CANTINSERTCOLUMNTYPE|Indicates that the column cannot be added to the database table because the conversion between the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] column data type and the database column data type is not supported.|  
|DTS_E_CONNECTIONNOTFOUND|Indicates that the package cannot run because the specified connection manager cannot be found.|  
|DTS_E_CONNECTIONREQUIREDFORMETADATA|Indicates that [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer must connect to a data source to retrieve new or updated metadata for a source or destination, and that it is unable to connect to the data source.|  
|DTS_E_MULTIPLECACHEWRITES|Indicates that the package cannot run because a Cache Transform transformation is trying to write data to the in-memory cache. However, another Cache Transform transformation has already written to the in-memory cache.|  
|DTS_E_PRODUCTLEVELTOLOW|Indicates that the package cannot run because the appropriate version of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] is not installed.|  
|DTS_E_READNOTFILLEDCACHE|Indicates that a Lookup transformation is trying to read data from the in-memory cache at the same time that a Cache Transform transformation is writing data to the cache.|  
|DTS_E_UNPROTECTXMLFAILED|Indicates that the system did not decrypt a protected XML node.|  
|DTS_E_WRITEWHILECACHEINUSE|Indicates that a Cache Transform transformation is trying to write data to the in-memory cache at the same time that a Lookup transformation is reading data from the in-memory cache.|  
|DTS_W_EXTERNALMETADATACOLUMNSOUTOFSYNC|Indicates that the column metadata in the data source does not match the column metadata in the source or destination component that is connected to the data source.|  
  
## Events (SQLISPackage)  
 For more information, see [Events Logged by an Integration Services Package](../integration-services/performance/events-logged-by-an-integration-services-package.md).  
  
|Event|Description|  
|-----------|-----------------|  
|SQLISPackage_12288|Indicates that a package started.|  
|SQLISPackage_12289|Indicates that a package has finished running successfully.|  
|SQLISPACKAGE_12291|Indicates that a package was unable to finish running and has stopped.|  
|SQLISPackage_12546|Indicates that a task or other executable in a package has finished its work.|  
|SQLISPackage_12549|Indicates that a warning message was raised in a package.|  
|SQLISPackage_12550|Indicates that an error message was raised in a package.|  
|SQLISPackage_12551|Indicates that a package did not finish its work and stopped.|  
|SQLISPackage_12557|Indicates that a package has finished running.|  
  
## Events (SQLISService)  
 For more information, see [Events Logged by the Integration Services Service](../integration-services/service/events-logged-by-the-integration-services-service.md).  
  
|Event|Description|  
|-----------|-----------------|  
|SQLISService_256|Indicates that the service is about to start.|  
|SQLISService_257|Indicates that the service has started.|  
|SQLISService_258|Indicates that the service is about to stop.|  
|SQLISService_259|Indicates that the service has stopped.|  
|SQLISService_260|Indicates that the service tried to start, but could not.|  
|SQLISService_272|Indicates that the configuration file does not exist at the specified location.|  
|SQLISService_273|Indicates that the configuration file could not be read or is not valid.|  
|SQLISService_274|Indicates that the registry entry that contains the location of the configuration file does not exist or is empty.|  
  
## See Also  
 [Integration Services Error and Message Reference](../integration-services/integration-services-error-and-message-reference.md)  
  
  
