---
title: "ADOMD.NET Client Programming | Microsoft Docs"
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
applies_to: 
  - "SQL Server 2016 Preview"
helpviewer_keywords: 
  - "programming [ADOMD.NET]"
  - "ADOMD.NET, programming"
ms.assetid: 55156115-ecd1-4ed9-876e-23406af9bbf9
caps.latest.revision: 42
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# ADOMD.NET Client Programming
  The ADOMD.NET client components reside within the **Microsoft.AnalysisServices.AdomdClient** namespace (in microsoft.analysisservices.adomdclient.dll). These client components provide the functionality for client and middle-tier applications to easily query data and metadata from an analytical data store, such as [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].  
  
## Using the ADOMD.NET Client Objects  
 In querying an analytical data source, there are a set of common tasks that need to be performed. The following table represents the common tasks in which you use the ADOMD.NET client objects to perform such a query.  
  
|Task|Description|  
|----------|-----------------|  
|[Establishing Connections in ADOMD.NET](../../analysis-services/multidimensional-models-adomd-net-client/connections-in-adomd-net.md)|In ADOMD.NET, you use an <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object to establish connections with analytical data sources, such as [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] databases. You can use the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object to run commands, retrieve data, and retrieve metadata from the analytical data source.|  
|[Retrieving Metadata from an Analytical Data Source](../../analysis-services/multidimensional-models-adomd-net-client/retrieving-metadata-from-an-analytical-data-source.md)|After a connection has been established, you can use a wide variety of objects to retrieve information about the underlying data source. This functionality allows applications to adapt to the data source to which they have connected.|  
|[Executing Commands Against an Analytical Data Source](../../analysis-services/multidimensional-models-adomd-net-client/executing-commands-against-an-analytical-data-source.md)|The <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand> object provides the interfaces necessary for running commands against the underlying analytical data source.|  
|[Retrieving Data from an Analytical Data Source](../../analysis-services/multidimensional-models-adomd-net-client/retrieving-data-from-an-analytical-data-source.md)|After a command runs, data could be retrieved and parsed using either the <xref:Microsoft.AnalysisServices.AdomdClient.CellSet>, <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader>, or **System.XmlReader** objects.|  
|[Performing Transactions in ADOMD.NET](../../analysis-services/multidimensional-models-adomd-net-client/connections-in-adomd-net-performing-transactions.md)|All of the actions listed in the previous rows of this table can take place within a read-committed transaction, in which shared locks are held while the data is being read to avoid dirty reads. The data can still be changed before the end of the transaction, resulting in non-repeatable reads or phantom data. The <xref:Microsoft.AnalysisServices.AdomdClient.AdomdTransaction> object provides the transaction functionality in ADOMD.NET.|  
  
 Interaction with the ADOMD.NET object hierarchy typically starts with one or more of the objects in the topmost layer, as described in the following table.  
  
|To|Use this object|  
|--------|---------------------|  
|Connect to an analytical data source|<xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection><br /> The <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object represents both a connection to a data source and the data source metadata. For example, you can connect to a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] local cube (.cub) file, and then examine the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.Cubes%2A> property to obtain metadata about the cubes present on the analytical data source. This object also represents the implementation of the **IDbConnection** interface, an interface that is required by all .NET Framework data providers.|  
|Discover the data mining capabilities of the data source|<xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection><br /> The <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object exposes several mining collections:<br /><br /><br /><br /> The <xref:Microsoft.AnalysisServices.AdomdClient.MiningModelCollection> contains a list of every mining model in the data source.<br /><br /><br /><br /> The <xref:Microsoft.AnalysisServices.AdomdClient.MiningServiceCollection> provides information about the available mining algorithms.<br /><br /><br /><br /> The <xref:Microsoft.AnalysisServices.AdomdClient.MiningStructureCollection> exposes information about the mining structures on the server.|  
|Query the data source|<xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand><br /> The <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand> object represents the statement or query that will be sent to the server. Once a connection is established to a data source, you use a <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand> object to run statements in the supported language, such as Multidimensional Expressions (MDX) or Data Mining Data Mining Extensions (DMX). You can also use a <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand> object to return results in the form of <xref:Microsoft.AnalysisServices.AdomdClient.CellSet> or <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> objects.|  
|Retrieve data in a fast, efficient way|<xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader><br /> The <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> can be created with a call to the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand.Execute%2A> or <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand.ExecuteReader%2A> method of an <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand> object. This object implements the **IDbDataReader** interface from the **System.Data** namespace of the .NET Framework class library.|  
|Retrieve analytical data with the highest amount of metadata|<xref:Microsoft.AnalysisServices.AdomdClient.CellSet><br /> The <xref:Microsoft.AnalysisServices.AdomdClient.CellSet> can be created with a call to the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand.Execute%2A> or <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand.ExecuteCellSet%2A> method of an <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand>. Once an <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand> has returned a <xref:Microsoft.AnalysisServices.AdomdClient.CellSet>, you can then examine the analytical data contained by the <xref:Microsoft.AnalysisServices.AdomdClient.CellSet>.|  
|Retrieve metadata about cubes, such as available dimensions, measures, named sets, and so on|<xref:Microsoft.AnalysisServices.AdomdClient.CubeDef><br /> The <xref:Microsoft.AnalysisServices.AdomdClient.CubeDef> represents metadata about a cube. You reference the <xref:Microsoft.AnalysisServices.AdomdClient.CubeDef> from the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection>.|  
|Retrieve data using the **System.Data.IDbDataAdapter** interface|<xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataAdapter><br /> The <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataAdapter> provides read-only support for existing .NET Framework client applications.|  
  
## See Also  
 [ADOMD.NET Server Programming](../../analysis-services/multidimensional-models-adomd-net-server/adomd-net-server-programming.md)   
 [Developing with ADOMD.NET](../../analysis-services/multidimensional-models/adomd-net/developing-with-adomd-net.md)  
  
  