---
title: "Retrieving Data from an Analytical Data Source | Microsoft Docs"
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
  - "data retrieval [ADOMD.NET]"
  - "retrieving data"
  - "ADOMD.NET, data retrieval"
  - "data retrieval [ADOMD.NET], about retrieving data"
ms.assetid: 88358189-28aa-4bc7-8dda-5a92e3a012b8
caps.latest.revision: 41
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Retrieving Data from an Analytical Data Source
  Once you make a connection and create the query, you can retrieve any data. In ADOMD.NET, you can retrieve data using three different objects (<xref:Microsoft.AnalysisServices.AdomdClient.CellSet>, <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader>, and <xref:System.Xml.XmlReader>) by calling one of the **Execute** methods of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand> object.  
  
 Each of these three objects balances interactivity and overhead:  
  
-   *Interactivity* refers to the ease-of-use and amount of information available through the object model.  
  
-   *Overhead* refers to the amount of traffic that an object model generates over the network connection to the server, the amount of memory needed for the object model, and the speed with which the object model retrieves data.  
  
 To help you select the data retrieval object that best suits the needs of your application, the following table highlights the differences between interactivity and overhead for each object.  
  
|Object|Interactivity|Overhead|Retains dimensionality|Usage Information|  
|------------|-------------------|--------------|----------------------------|-----------------------|  
|<xref:Microsoft.AnalysisServices.AdomdClient.CellSet>|Highest|Moderately high, which results in slowest retrieval of data|Yes|[Retrieving Data Using the CellSet](../../analysis-services/multidimensional-models-adomd-net-client/retrieving-data-using-the-cellset.md)|  
|<xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataAdapter>|Moderate|Moderate|No|[Populating a DataSet from a DataAdapter](http://go.microsoft.com/fwlink/?LinkId=70016)|  
|<xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader>|Moderate|Moderate|No|[Retrieving Data Using the AdomdDataReader](../../analysis-services/multidimensional-models-adomd-net-client/retrieving-data-using-the-adomddatareader.md)|  
|<xref:System.Xml.XmlReader>|Lowest|Lowest, which results in fastest data retrieval|Yes|[Retrieving Data Using the XmlReader](../../analysis-services/multidimensional-models-adomd-net-client/retrieving-data-using-the-xmlreader.md)|  
  
## See Also  
 [ADOMD.NET Client Programming](../../analysis-services/multidimensional-models-adomd-net-client/adomd-net-client-programming.md)  
  
  