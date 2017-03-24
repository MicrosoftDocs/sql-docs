---
title: "XML for Analysis Schema Rowsets | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "rowsets [Analysis Services], XML for Analysis"
  - "XML for Analysis, schema rowsets"
  - "schema rowsets [Analysis Services], XML for Analysis"
  - "schema rowsets [XML for Analysis]"
ms.assetid: 36e3ecfd-fcc3-415a-9c43-f59921d2468a
caps.latest.revision: 34
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# XML for Analysis Schema Rowsets
  The [!INCLUDE[msCoName](../../../includes/msconame-md.md)] XML for Analysis (XMLA) provider includes schema rowsets that return metadata about server state, activity, and objects. Retrieving metadata is necessary if you are developing a client application that connects to an Analysis Services model whose structure and characteristics are variable.  
  
 Schema rowsets also provide insight into internal processes and operations that can help you monitor the server and troubleshoot problems. To better support ad hoc administrative tasks, you can run a Dynamic Management View (DMV) query against most schema rowsets. DMV queries return results in a readable, tabular format that you can view in [!INCLUDE[ssManStudio](../../../includes/ssmanstudio-md.md)].  
  
 The following table lists and describes each XMLA schema rowset, and identifies whether it returns information specific to tabular data models.  
  
## In This Section  
  
|Rowset<sup>1</sup>|Description|  
|------------------------|-----------------|  
|[DISCOVER_CALC_DEPENDENCY Rowset](../../../analysis-services/schema-rowsets/xml/discover-calc-dependency-rowset.md)|Returns information about dependencies among tables, columns, measures, and calculated column formulas.<br /><br /> Applies to tabular models deployed on an Analysis Services instance and [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] models in Excel workbooks that run in a SharePoint environment.|  
|[DISCOVER_CONNECTIONS Rowset](../../../analysis-services/schema-rowsets/xml/discover-connections-rowset.md)|Provides resource usage and activity information about the currently opened connections on the server.|  
|[DISCOVER_COMMAND_OBJECTS Rowset](../../../analysis-services/schema-rowsets/xml/discover-command-objects-rowset.md)|Provides resource usage and activity information about the objects in use by the referenced command.|  
|[DISCOVER_COMMANDS Rowset](../../../analysis-services/schema-rowsets/xml/discover-commands-rowset.md)|Provides resource usage and activity information about the currently executing or last executed commands in the opened connections on the server|  
|[DISCOVER_CSDL_METADATA Rowset](../../../analysis-services/schema-rowsets/xml/discover-csdl-metadata-rowset.md)|Returns an XML definition of a data source to a client that can consume a tabular or [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] model, and present the source data as part of a report.<br /><br /> Applies to tabular models deployed on an Analysis Services instance and [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] models in Excel workbooks that run in a SharePoint environment.|  
|[DISCOVER_DATASOURCES Rowset](../../../analysis-services/schema-rowsets/xml/discover-datasources-rowset.md)|Returns a list of the XMLA Provider data sources that are available on the server or Web service.|  
|[DISCOVER_DB_CONNECTIONS Rowset](../../../analysis-services/schema-rowsets/xml/discover-db-connections-rowset.md)|Provides resource usage and activity information about the currently opened connections from the server to a database.|  
|[DISCOVER_DIMENSION_STAT Rowset](../../../analysis-services/schema-rowsets/xml/discover-dimension-stat-rowset.md)|Returns statistics on the specified dimension.|  
|[DISCOVER_ENUMERATORS Rowset](../../../analysis-services/schema-rowsets/xml/discover-enumerators-rowset.md)|Returns a list of names, data types, and enumeration values of enumerators supported by the XMLA Provider for a specific data source.|  
|[DISCOVER_JOBS Rowset](../../../analysis-services/schema-rowsets/xml/discover-jobs-rowset.md)|Provides information about the active jobs executing on the server.|  
|[DISCOVER_KEYWORDS Rowset &#40;XMLA&#41;](../../../analysis-services/schema-rowsets/xml/discover-keywords-rowset-xmla.md)|Returns information about keywords reserved by the XMLA provider.|  
|[DISCOVER_LITERALS Rowset](../../../analysis-services/schema-rowsets/xml/discover-literals-rowset.md)|Returns information about literals, including data types and values, supported by the XMLA provider.|  
|[DISCOVER_LOCATIONS Rowset](../../../analysis-services/schema-rowsets/xml/discover-locations-rowset.md)|Returns information about the contents of a backup file.|  
|[DISCOVER_LOCKS Rowset](../../../analysis-services/schema-rowsets/xml/discover-locks-rowset.md)|Provides information about the current standing locks on the server.|  
|[DISCOVER_MEMORYGRANT Rowset](../../../analysis-services/schema-rowsets/xml/discover-memorygrant-rowset.md)|Returns a list of internal memory quota grants that are taken by jobs that are currently running on the server.|  
|[DISCOVER_MEMORYUSAGE Rowset](../../../analysis-services/schema-rowsets/xml/discover-memoryusage-rowset.md)|Returns the memory usage statistics for various objects allocated by the server.|  
|[DISCOVER_OBJECT_ACTIVITY Rowset](../../../analysis-services/schema-rowsets/xml/discover-object-activity-rowset.md)|Provides resource usage per object since the start of the service.|  
|[DISCOVER_OBJECT_MEMORY_USAGE Rowset](../../../analysis-services/schema-rowsets/xml/discover-object-memory-usage-rowset.md)|Provides information about memory resources used by objects.|  
|[DISCOVER_PARTITION_DIMENSION_STAT Rowset](../../../analysis-services/schema-rowsets/xml/discover-partition-dimension-stat-rowset.md)|Returns statistics on the dimension that is associated with a partition.|  
|[DISCOVER_PARTITION_STAT Rowset](../../../analysis-services/schema-rowsets/xml/discover-partition-stat-rowset.md)|Returns statistics on aggregations in a particular partition.|  
|[DISCOVER_PERFORMANCE_COUNTERS Rowset](../../../analysis-services/schema-rowsets/xml/discover-performance-counters-rowset.md)|Returns the value of one or more specified performance counters.|  
|[DISCOVER_PROPERTIES Rowset](../../../analysis-services/schema-rowsets/xml/discover-properties-rowset.md)|Returns a list of information and values about the standard and provider-specific properties that are supported by the XMLA provider for the specified data source.|  
|[DISCOVER_SCHEMA_ROWSETS Rowset](../../../analysis-services/schema-rowsets/xml/discover-schema-rowsets-rowset.md)|Returns the names, restrictions, description, and other information for all enumeration values and any additional provider-specific enumeration values supported by the XMLA provider.|  
|[DISCOVER_SESSIONS Rowset](../../../analysis-services/schema-rowsets/xml/discover-sessions-rowset.md)|Provides resource usage and activity information about the currently opened sessions on the server.|  
|[DISCOVER_STORAGE_TABLE_COLUMN_SEGMENTS Rowset](../../../analysis-services/schema-rowsets/xml/discover-storage-table-column-segments-rowset.md)|Provides information at the column and segment level about storage tables used by a tabular or [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] database.<br /><br /> Applies to tabular models deployed on an Analysis Services instance and [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] models in Excel workbooks that run in a SharePoint environment.|  
|[DISCOVER_STORAGE_TABLE_COLUMNS Rowset](../../../analysis-services/schema-rowsets/xml/discover-storage-table-columns-rowset.md)|Allows the client to determine the assignment of columns to storage tables used by a tabular or [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] database.<br /><br /> Applies to tabular models deployed on an Analysis Services instance and [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] models in Excel workbooks that run in a SharePoint environment.|  
|[DISCOVER_STORAGE_TABLES Rowset](../../../analysis-services/schema-rowsets/xml/discover-storage-tables-rowset.md)|Returns information about the tables used in a model.<br /><br /> Applies to tabular models deployed on an Analysis Services instance and [!INCLUDE[ssGemini](../../../includes/ssgemini-md.md)] models in Excel workbooks that run in a SharePoint environment.|  
|[DISCOVER_TRACE_COLUMNS Rowset](../../../analysis-services/schema-rowsets/xml/discover-trace-columns-rowset.md)|Describes the trace columns provided by the trace provider.|  
|[DISCOVER_TRACE_DEFINITION_PROVIDERINFO Rowset](../../../analysis-services/schema-rowsets/xml/discover-trace-definition-providerinfo-rowset.md)|Returns basic information about the trace provider, such as its name and description.|  
|[DISCOVER_TRACE_EVENT_CATEGORIES Rowset](../../../analysis-services/schema-rowsets/xml/discover-trace-event-categories-rowset.md)|Describes the event categories provided by the trace provider.|  
|[DISCOVER_TRACES Rowset](../../../analysis-services/schema-rowsets/xml/discover-traces-rowset.md)|Returns information about traces running on a server.|  
|[DISCOVER_TRANSACTIONS Rowset](../../../analysis-services/schema-rowsets/xml/discover-transactions-rowset.md)|Returns the current set of pending transactions on the system.|  
|[DISCOVER_XML_METADATA Rowset](../../../analysis-services/schema-rowsets/xml/discover-xml-metadata-rowset.md)|Returns an XML document describing a requested object.|  
  
 <sup>1</sup> All the schema rowsets listed here are supported by the MSOLAP data source provider for the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] XMLA Provider.  
  
## See Also  
 [Developing with XMLA in Analysis Services](../../../analysis-services/multidimensional-models-scripting-language-assl-xmla/developing-with-xmla-in-analysis-services.md)   
 [Use Dynamic Management Views &#40;DMVs&#41; to Monitor Analysis Services](../../../analysis-services/instances/use-dynamic-management-views-dmvs-to-monitor-analysis-services.md)   
 [Retrieving Metadata from an Analytical Data Source](../../../analysis-services/multidimensional-models-adomd-net-client/retrieving-metadata-from-an-analytical-data-source.md)  
  
  