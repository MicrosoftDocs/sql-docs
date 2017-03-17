---
title: "DISCOVER_DATASOURCES Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "DISCOVER_DATASOURCES"
apitype: "NA"
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "DISCOVER_DATASOURCES rowset"
ms.assetid: f3ff26ab-a447-416b-ba54-1716df2283de
caps.latest.revision: 39
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_DATASOURCES Rowset
  Returns a list of the XML for Analysis (XMLA) provider data sources that are available on the server or Web service. The published data sources are returned from a URL of the application Web server. The client can connect to one of the data sources in this list.  
  
 If you call the [Discover](../../../analysis-services/xmla/xml-elements-methods-discover.md) method with the **DISCOVER_DATASOURCES** enumeration value in the [RequestType](../../../analysis-services/xmla/xml-elements-properties/requesttype-element-xmla.md) element, the **Discover** method returns the **DISCOVER_DATASOURCES** rowset.  
  
 **Applies to:** tabular models, multidimensional models  
  
## Rowset Columns  
 The client selects a data source by setting the **DataSourceInfo** property in the [Properties](../../../analysis-services/xmla/xml-elements-properties/properties-element-xmla.md) element that is sent along with the [Command](../../../analysis-services/xmla/xml-elements-properties/command-element-xmla.md) element by the [Execute](../../../analysis-services/xmla/xml-elements-methods-execute.md) method. A client should not construct the contents of the **DataSourceInfo** property to send to the server. Instead, the client should use the **Discover** method to find the data sources that the provider supports. The client then sends back the same value for the **DataSourceInfo** property that it gets from the **DISCOVER_DATASOURCES** rowset.  
  
 The **DISCOVER_DATASOURCES** rowset contains the following columns.  
  
|Column name|Type indicator|Restriction|Description|  
|-----------------|--------------------|-----------------|-----------------|  
|**DataSourceName**|**DBTYPE_WSTR**|Yes|The name of the data source, such as **Adventure Works**.|  
|**DataSourceDescription**|**DBTYPE_WSTR**||The description of the data source entered by the publisher.<br /><br /> May return **NULL**.|  
|**URL**|**DBTYPE_WSTR**|Yes|The unique path that shows where to invoke the XML for Analysis (XMLA) methods for that data source.<br /><br /> May return **NULL**.|  
|**DataSourceInfo**|**DBTYPE_WSTR**||A string that contains any additional information required to connect to the data source.<br /><br /> May return **NULL**.|  
|**ProviderName**|**DBTYPE_WSTR**|Yes|The name of the provider for the data source.<br /><br /> Example: `"MSOLAP"`<br /><br /> May return **NULL**.|  
|**ProviderType**|**DBTYPE_WSTR**|Yes|The types of data supported by the provider. This array can include one or more of the following types:<br /><br /> **MDP**: multidimensional data provider.<br /><br /> **TDP**: tabular data provider.<br /><br /> **DMP**: data mining provider (implements the OLE for DB for Data Mining specification).|  
|**AuthenticationMode**|**DBTYPE_WSTR**|Yes|A specification of what type of security mode the data source uses. Values can be one of the following:<br /><br /> **Unauthenticated**: No user ID or password has to be sent.<br /><br /> **Authenticated**: User ID and password must be included in the information required to connect to the data source.<br /><br /> **Integrated**: The data source uses the underlying security to determine authorization, such as Integrated Security provided by [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Internet Information Services (IIS).|  
  
 This schema rowset is not sorted.  
  
> [!IMPORTANT]  
>  The **DISCOVER_DATASOURCES** rowset cannot be queried using DMV queries and the SELECT command syntax. However, the **DISCOVER_DATASOURCES** rowset can be queried using <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.GetSchemaDataSet%2A>.  
  
## Using ADOMD.NET to return the rowset  
 When using ADOMD.NET and the schema rowset to retrieve metadata, you can use either the GUID or string to reference a schema rowset object in the GetSchemaDataSet method. For more information, see [Working with Schema Rowsets in ADOMD.NET](../../../analysis-services/multidimensional-models-adomd-net-client/retrieving-metadata-working-with-schema-rowsets.md).  
  
 The following table provides the GUID and string values that identify this rowset.  
  
|Argument|Value|  
|--------------|-----------|  
|GUID|06c03d41-f66d-49f3-b1b8-987f7af4cf18|  
|ADOMDNAME|DataSources|  
  
## See Also  
 [XML for Analysis Schema Rowsets](../../../analysis-services/schema-rowsets/xml/xml-for-analysis-schema-rowsets.md)  
  
  