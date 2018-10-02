---
title: "Analysis Services Schema Rowsets | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.topic: "reference"
topic_type: 
  - "apiref"
  - "apinav"
helpviewer_keywords: 
  - "SSAS, data access interfaces"
  - "Analysis Services data access interfaces, schema rowsets"
  - "data access interfaces [Analysis Services]"
  - "XML for Analysis, schema rowsets"
  - "rowsets [Analysis Services], retrieving schema rowsets"
  - "retrieving schema rowsets"
  - "XMLA, schema rowsets"
  - "rowsets [Analysis Services]"
  - "schema rowsets [Analysis Services], retrieving"
ms.assetid: 820d4b59-d428-4616-b792-c848e5da407e
author: minewiskan
ms.author: owend
manager: craigg
---
# Analysis Services Schema Rowsets
  Schema rowsets are predefined tables that contain information about Analysis Services objects and server state, including database schema, active sessions, connections, commands, and jobs that are executing on the server. You can query schema rowset tables in an XML/A script window in SQL Server Management Studio, run a DMV query against a schema rowset, or create a custom application that incorporates schema rowset information (for example, a reporting application that retrieves the list of available dimensions that can be used to create a report).  
  
> [!NOTE]  
>  If you are using schema rowsets in XML/A script, the information that is returned in the *Result* parameter of the [Discover](../xmla/xml-elements-methods-discover.md) method is structured according to the rowset column layouts described in this section. The [!INCLUDE[msCoName](../../includes/msconame-md.md)] XML for Analysis (XMLA) provider supports rowsets required by the XML for Analysis Specification. The XMLA provider also supports some of the standard schema rowsets for OLE DB, OLE DB for OLAP, and OLE DB for Data Mining data source providers. The supported rowsets are described in the following topics.  
  
## In This Section  
  
|Topic|Description|  
|-----------|-----------------|  
|[XML for Analysis Schema Rowsets](xml/xml-for-analysis-schema-rowsets.md)|Describes the XMLA rowsets supported by the XMLA provider.|  
|[OLE DB Schema Rowsets](ole-db/ole-db-schema-rowsets.md)|Describes the OLE DB schema rowsets supported by the XMLA provider.|  
|[OLE DB for OLAP Schema Rowsets](ole-db-olap/ole-db-for-olap-schema-rowsets.md)|Describes the OLE DB for OLAP schema rowsets supported by the XMLA provider.|  
|[Data Mining Schema Rowsets](data-mining/data-mining-schema-rowsets.md)|Describes the data mining schema rowsets supported by the XMLA provider.|  
  
## See Also  
 [Multidimensional Model Data Access &#40;Analysis Services - Multidimensional Data&#41;](../multidimensional-models/mdx/multidimensional-model-data-access-analysis-services-multidimensional-data.md)   
 [Use Dynamic Management Views &#40;DMVs&#41; to Monitor Analysis Services](../instances/use-dynamic-management-views-dmvs-to-monitor-analysis-services.md)  
  
  
