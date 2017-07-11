---
title: "Retrieving Data Using the CellSet | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
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
  - "CellSet object"
  - "retrieving data"
  - "data retrieval [ADOMD.NET], CellSet object"
ms.assetid: 77e4ee58-882d-4012-91a3-0565f18a4882
caps.latest.revision: 41
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Retrieving Data Using the CellSet
  When retrieving analytical data, the <xref:Microsoft.AnalysisServices.AdomdClient.CellSet> object provides the most interactivity and flexibility. The <xref:Microsoft.AnalysisServices.AdomdClient.CellSet> object is an in-memory cache of hierarchical data and metadata that retains the original dimensionality of the data. The <xref:Microsoft.AnalysisServices.AdomdClient.CellSet> object can also be traversed in either a connected or disconnected state. Because of this disconnected ability, the <xref:Microsoft.AnalysisServices.AdomdClient.CellSet> object can be used to view data and metadata in any order and provides the most comprehensive object model for data retrieval. This disconnected capability also causes the <xref:Microsoft.AnalysisServices.AdomdClient.CellSet> object to have the most overhead, and to be the slowest ADOMD.NET data retrieval object model to populate.  
  
## Retrieving Data in a Connected State  
 To use the <xref:Microsoft.AnalysisServices.AdomdClient.CellSet> object to retrieve data, you follow these steps:  
  
1.  **Create a new instance of the object.**  
  
     To create a new instance of the <xref:Microsoft.AnalysisServices.AdomdClient.CellSet> object, you call the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand.Execute%2A> or <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand.ExecuteCellSet%2A> method of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand> object.  
  
2.  **Identify metadata.**  
  
     Besides retrieving data, ADOMD.NET also retrieves metadata for the cellset. As soon as the command has run the query and returned a <xref:Microsoft.AnalysisServices.AdomdClient.CellSet>, you can retrieve the metadata through various objects. This metadata is needed for client applications to display and interact with cellset data. For example, many client applications provide functionality for drilling down on, or hierarchically displaying the child positions of, a specified position in a cellset.  
  
     In ADOMD.NET, the <xref:Microsoft.AnalysisServices.AdomdClient.CellSet.Axes%2A> and <xref:Microsoft.AnalysisServices.AdomdClient.CellSet.FilterAxis%2A> properties of the <xref:Microsoft.AnalysisServices.AdomdClient.CellSet> object represent the metadata of the query and slicer axes, respectively, in the returned cellset. Both properties return references to <xref:Microsoft.AnalysisServices.AdomdClient.Axis> objects, which in turn contain the positions represented on each axis.  
  
     Each <xref:Microsoft.AnalysisServices.AdomdClient.Axis> object contains a collection of <xref:Microsoft.AnalysisServices.AdomdClient.Position> objects that represent the set of tuples available for that axis. Each <xref:Microsoft.AnalysisServices.AdomdClient.Position> object represents a single tuple that contains one or more members, represented by a collection of <xref:Microsoft.AnalysisServices.AdomdClient.Member> objects.  
  
3.  **Retrieve data from the cellset collection.**  
  
     Besides retrieving metadata, ADOMD.NET also retrieves data for the cellset. As soon as the command has run the query and returned a <xref:Microsoft.AnalysisServices.AdomdClient.CellSet>, you can retrieve the data by using the <xref:Microsoft.AnalysisServices.AdomdClient.CellSet.Cells%2A> collection of the <xref:Microsoft.AnalysisServices.AdomdClient.CellSet>. This collection contains the values that are calculated for the intersection of all axes in the query. Therefore, there are several indexers for accessing each intersection, or cell. For a list of indexers, see <xref:Microsoft.AnalysisServices.AdomdClient.CellCollection.Item%2A>.  
  
### Example of Retrieving Data in a Connected State  
 The following example makes a connection to the local server, and then runs a command on the connection. The example parses the results by using the **CellSet** object model: the captions (metadata) for the columns are retrieved from the first axis, and the captions (metadata) for each row are retrieved from the second axis, and the intersecting data is retrieved by using the <xref:Microsoft.AnalysisServices.AdomdClient.CellSet.Cells%2A> collection.  
  
 [!code-cs[Adomd.NetClient#ReturnCommandUsingCellSet](../../analysis-services/multidimensional-models-adomd-net-client/codesnippet/csharp/retrieving-data-using-th_0_1.cs)]  
  
## Retrieving Data in a Disconnected State  
 By loading XML returned from a previous query, you can use the <xref:Microsoft.AnalysisServices.AdomdClient.CellSet> object to provide a comprehensive method of browsing analytical data without requiring an active connection.  
  
> [!NOTE]  
>  Not all properties of the objects that are available from the <xref:Microsoft.AnalysisServices.AdomdClient.CellSet> object are available while in a disconnected state. For more information, see <xref:Microsoft.AnalysisServices.AdomdClient.CellSet.LoadXml%2A>.  
  
### Example of Retrieving Data in a Disconnected State  
 The following example is similar to the metadata and data example shown earlier in this topic. However, the command in the following example runs with a call to <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand.ExecuteXmlReader%2A>, and the result is returned as a **System.Xml.XmlReader**. The example then populates the <xref:Microsoft.AnalysisServices.AdomdClient.CellSet> object by using this **System.Xml.XmlReader** with the <xref:Microsoft.AnalysisServices.AdomdClient.CellSet.LoadXml%2A> method. Although this example loads the **System.Xml.XmlReader** immediately, you could cache the XML that is contained by the reader to a hard disk or transport that data to a different application through any means before loading the data into a cellset.  
  
 [!code-cs[Adomd.NetClient#DemonstrateDisconnectedCellset](../../analysis-services/multidimensional-models-adomd-net-client/codesnippet/csharp/retrieving-data-using-th_0_2.cs)]  
  
## See Also  
 [Retrieving Data from an Analytical Data Source](../../analysis-services/multidimensional-models-adomd-net-client/retrieving-data-from-an-analytical-data-source.md)   
 [Retrieving Data Using the AdomdDataReader](../../analysis-services/multidimensional-models-adomd-net-client/retrieving-data-using-the-adomddatareader.md)   
 [Retrieving Data Using the XmlReader](../../analysis-services/multidimensional-models-adomd-net-client/retrieving-data-using-the-xmlreader.md)  
  
  