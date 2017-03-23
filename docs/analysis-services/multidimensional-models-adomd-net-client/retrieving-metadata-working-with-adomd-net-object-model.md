---
title: "Working with the ADOMD.NET Object Model | Microsoft Docs"
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
  - "object model (client) [ADOMD.NET]"
  - "retrieving metadata"
ms.assetid: 0183dcdc-f2ea-4246-ad00-6e8ccc9d8217
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Retrieving Metadata - Working with ADOMD.NET Object Model
  ADOMD.NET provides an object model for viewing the cubes and subordinate objects contained by an analytical data source. However, not all metadata for a given analytical data source is available through the object model. The object model provides access to only the information that is most useful for a client application to display in order to allow a user to interactively construct commands. Because of the reduced complexity of the metadata to present, the ADOMD.NET object model is easier to use.  
  
 In the ADOMD.NET object model, the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object provides access to information on the online analytical processing (OLAP) cubes and mining models defined on an analytical data source, and related objects such as dimensions, named sets, and mining algorithms.  
  
## Retrieving OLAP Metadata  
 Each <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object has a collection of <xref:Microsoft.AnalysisServices.AdomdClient.CubeDef> objects that represent the cubes available to the user or application. The <xref:Microsoft.AnalysisServices.AdomdClient.CubeDef> object exposes information about the cube, as well as various objects related to the cube, such as dimensions, key performance indicators, measures, named sets, and so on.  
  
 Whenever possible, you should use the <xref:Microsoft.AnalysisServices.AdomdClient.CubeDef> object to represent metadata in client applications designed to support multiple OLAP servers, or for general metadata display and access purposes.  
  
> [!NOTE]  
>  For provider specific metadata, or for detailed metadata display and access, use schema rowsets to retrieve metadata. For more information, see [Working with Schema Rowsets in ADOMD.NET](../../analysis-services/multidimensional-models-adomd-net-client/retrieving-metadata-working-with-schema-rowsets.md).  
  
 The following example uses the <xref:Microsoft.AnalysisServices.AdomdClient.CubeDef> object to retrieve the visible cubes and their dimensions from the local server:  
  
 [!code-cs[Adomd.NetClient#RetrieveCubesAndDimensions](../../analysis-services/multidimensional-models-adomd-net-client/codesnippet/csharp/retrieving-metadata-work_1_1.cs)]  
  
## Retrieving Data Mining Metadata  
 Each <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object has several collections that provide information about the data mining capabilities of the data source:  
  
-   The <xref:Microsoft.AnalysisServices.AdomdClient.MiningModelCollection> contains a list of every mining model in the data source.  
  
-   The <xref:Microsoft.AnalysisServices.AdomdClient.MiningServiceCollection> provides information about the available mining algorithms.  
  
-   The <xref:Microsoft.AnalysisServices.AdomdClient.MiningStructureCollection> exposes information about the mining structures on the server.  
  
 To determine how to query against a mining model on the server, iterate through the <xref:Microsoft.AnalysisServices.AdomdServer.MiningModel.Columns%2A> collection. Each <xref:Microsoft.AnalysisServices.AdomdClient.MiningModelColumn> object exposes the following characteristics:  
  
-   Whether the object is an input column (<xref:Microsoft.AnalysisServices.AdomdClient.MiningModelColumn.IsInput%2A>).  
  
-   Whether the object is a prediction column (<xref:Microsoft.AnalysisServices.AdomdClient.MiningModelColumn.IsPredictable%2A>).  
  
-   The values associated with a discrete column (<xref:Microsoft.AnalysisServices.AdomdClient.MiningModelColumn.Values%2A>)  
  
-   The type of data in the column (<xref:Microsoft.AnalysisServices.AdomdClient.MiningModelColumn.Type%2A>).  
  
## See Also  
 [Retrieving Metadata from an Analytical Data Source](../../analysis-services/multidimensional-models-adomd-net-client/retrieving-metadata-from-an-analytical-data-source.md)  
  
  