---
title: "Executing Commands Against an Analytical Data Source | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
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
  - "AdomdCommand object"
  - "commands [ADOMD.NET]"
  - "ADOMD.NET, commands"
ms.assetid: 1a958e5f-fc18-480b-9706-fc44e3b1d534
caps.latest.revision: 37
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Executing Commands Against an Analytical Data Source
  After establishing a connection to an analytical data source, you can use an <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand> object to run commands against and return results from that data source. These commands can retrieve data by using Multidimensional Expressions (MDX), Data Mining Extensions (DMX), or even a limited syntax of SQL. Additionally, you can use Analysis Services Scripting Language (ASSL) commands to modify the underlying database.  
  
## Creating a Command  
 Before running a command, you must create it. You can create a command using one of two methods:  
  
-   The first method uses the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand> constructor, which can take a command to run at the data source, and an <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object on which to run the command.  
  
-   The second method uses the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.CreateCommand%2A> method of the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection> object.  
  
 The text of the command to be run can be queried and modified using the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand.CommandText%2A> property. The commands that you create do not have to return data after they run.  
  
## Running a Command  
 After you have created an <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand> object, there are several <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand.Execute%2A> methods that your command can use to perform various actions. The following table lists some of these actions.  
  
|To|Use this method|  
|--------|---------------------|  
|Return results as a stream of data|<xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand.ExecuteReader%2A> to return an <xref:Microsoft.AnalysisServices.AdomdClient.AdomdDataReader> object|  
|Return a <xref:Microsoft.AnalysisServices.AdomdClient.CellSet> object|<xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand.ExecuteCellSet%2A>|  
|Run commands that do not return rows|<xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand.ExecuteNonQuery%2A>|  
|Return an **XMLReader** object that contains the data in an XML for Analysis (XMLA) compliant format|<xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand.ExecuteXmlReader%2A>|  
  
### Example of Running a Command  
 This example uses the <xref:Microsoft.AnalysisServices.AdomdClient.AdomdCommand> to run an XMLA command that will process the **Adventure Works DW** cube on the local server, without returning data.  
  
 [!code-cs[Adomd.NetClient#ExecuteXMLAProcessCommand](../../analysis-services/multidimensional-models-adomd-net-client/codesnippet/csharp/executing-commands-again_1.cs)]  
  
  