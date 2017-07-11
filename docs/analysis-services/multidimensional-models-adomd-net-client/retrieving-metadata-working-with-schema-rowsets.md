---
title: "Working with Schema Rowsets in ADOMD.NET | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
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
  - "metadata [ADOMD.NET]"
  - "retrieving metadata"
  - "schema rowsets [ADOMD.NET]"
ms.assetid: 7bf75bf8-f1e1-44f6-ac42-c38a681654cf
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Retrieving Metadata - Working with Schema Rowsets
  When you need more metadata than is available in the ADOMD.NET object model, ADOMD.NET provides the capability to retrieve the full range of XML for Analysis (XMLA), OLE DB, OLE DB for OLAP, and OLE DB for Data Mining schema rowsets:  
  
 **XML for Analysis metadata**  
 The XML for Analysis schema rowsets provide a method for retrieving low-level information about the server. Information available includes the data sources available on the server, the keywords reserved by the provider, the literals supported by the provider, and more. You can even use an XML for Analysis schema rowset to discover all schema rowsets supported by the provider.  
  
 For more information: [XML for Analysis Schema Rowsets](../../analysis-services/schema-rowsets/xml/xml-for-analysis-schema-rowsets.md)  
  
 **OLE DB metadata**  
 The OLE DB schema rowsets provide an industry-standard method for retrieving information from a variety of providers.  
  
 For more information: [OLE DB Schema Rowsets](../../analysis-services/schema-rowsets/ole-db/ole-db-schema-rowsets.md)  
  
 **OLAP metadata**  
 Schema information provided for an analytical data source includes databases or catalogs available from the analytical data source, cubes and mining models in a database, roles that exist for cubes at the data source, and more.  
  
 For more information: [OLE DB for OLAP Schema Rowsets](../../analysis-services/schema-rowsets/ole-db-olap/ole-db-for-olap-schema-rowsets.md)  
  
 **Data Mining metadata**  
 In addition to OLAP metadata, data mining metadata can be retrieved using schema rowsets. The available rowsets expose information on the available data mining models in the database, the available mining algorithms, the parameters that the algorithm require, mining structures, and more.  
  
 For more information: [Data Mining Schema Rowsets](../../analysis-services/schema-rowsets/data-mining/data-mining-schema-rowsets.md)  
  
 For each of these various schema rowsets, you retrieve metadata from the rowset by passing either a GUID or XMLA name with the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.GetSchemaDataSet%2A> method of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object.  
  
## Retrieving Metadata by Passing GUIDS  
 The <xref:Microsoft.AnalysisServices.AdomdClient.AdomdSchemaGuid> class contains a list of fields that represent the schema rowsets most commonly supported by providers and analytical data sources. To retrieve both general and provider-specific metadata from a provider or analytical data source, you use the GUIDs contained within the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdSchemaGuid> object with the either of the following methods:  
  
-   <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.GetSchemaDataSet%2A>  
  
-   <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.GetSchemaDataSet%2A>  
  
> [!NOTE]  
>  The ADOMD.NET data provider exposes schema information through functionality made available by your specific provider and analytical data source. Each provider and data source may provide different metadata.  
  
## Retrieving Metadata by Passing XMLA Names  
 The following methods take as arguments the XMLA schema name that identifies which schema information to return, and an array of restrictions on those returned columns:  
  
-   <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.GetSchemaDataSet%2A>  
  
-   <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.GetSchemaDataSet%2A>  
  
-   <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.GetSchemaDataSet%2A>  
  
-   <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.GetSchemaDataSet%2A>  
  
 Each of these methods returns an instance of a **DataSet** object that is populated with the schema information. The **DataSet** object is from the **System.Data** namespace of the Microsoft .NET Framework Class Library.  
  
## Example  
 In the following example, the GetActions function takes a connection, the cube name, a coordinate, and a coordinate type, retrieves an [MDSCHEMA_ACTIONS Rowset](../../analysis-services/schema-rowsets/ole-db-olap/mdschema-actions-rowset.md), and returns the actions available on the selected coordinate.  
  
 [!code-cs[Adomd.NetClient#GetActions](../../analysis-services/multidimensional-models-adomd-net-client/codesnippet/csharp/retrieving-metadata-work_0_1.cs)]  
  
## See Also  
 [Retrieving Metadata from an Analytical Data Source](../../analysis-services/multidimensional-models-adomd-net-client/retrieving-metadata-from-an-analytical-data-source.md)  
  
  