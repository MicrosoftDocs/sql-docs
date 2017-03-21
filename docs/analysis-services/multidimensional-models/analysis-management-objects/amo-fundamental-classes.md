---
title: "AMO Fundamental Classes | Microsoft Docs"
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
  - "data sources [AMO]"
  - "AMO, database objects"
  - "AMO, server objects"
  - "Analysis Management Objects, server objects"
  - "database objects [AMO]"
  - "Analysis Management Objects, database objects"
  - "AMO, data sources"
  - "Analysis Management Objects, data sources"
ms.assetid: 440e9287-53a2-4db3-9481-1d2ceb6e5b5a
caps.latest.revision: 28
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# AMO Fundamental Classes
  Fundamental classes are the starting point for working with Analysis Management Objects (AMO). Through these classes you establish your environment for the rest of the objects that will be used in your application. Fundamental classes include the following objects: <xref:Microsoft.AnalysisServices.Server>, <xref:Microsoft.AnalysisServices.Database>, <xref:Microsoft.AnalysisServices.DataSource>, and <xref:Microsoft.AnalysisServices.DataSourceView>.  
  
 The following illustration shows the relationship of the classes that are explained in this topic.  
  
 ![AMO Fundamental Classes](../../../analysis-services/multidimensional-models/analysis-management-objects/media/amo-fundamentalclasses.gif "AMO Fundamental Classes")  
  
 This topic contains the following sections:  
  
-   [Server Objects](#ServerObjects)  
  
-   [Database Objects](#DatabaseObjects)  
  
-   [DataSource and DataSourceView Objects](#DSandDSV)  
  
##  <a name="ServerObjects"></a> Server Objects  
 Additionally, you will have access to the following methods:  
  
-   Connection management: Connect, Disconnect, Reconnect, and GetConnectionState.  
  
-   Transaction management: BeginTransaction, CommitTransaction, and RollbackTransaction.  
  
-   Backup and Restore.  
  
-   DDL execution: Execute, CancelCommand, SendXmlaRequest, StartXmlaRequest.  
  
-   Metadata management: UpdateObjects and Validate.  
  
 To connect to a server, you need a standard connection string, as used in ADOMD.NET and OLEDB. For more information, see <xref:System.Configuration.ConnectionStringSettings.ConnectionString%2A>. The name of the server can be specified as a connection string without having to use a connection string format.  
  
 For more information about methods and properties available, see <xref:Microsoft.AnalysisServices.Server> in the <xref:Microsoft.AnalysisServices>.  
  
##  <a name="DatabaseObjects"></a> Database Objects  
 To work with a <xref:Microsoft.AnalysisServices.Database> object in your application, you must get an instance of the database from the parent server databases collection. To create a database, you add a <xref:Microsoft.AnalysisServices.Database> object to a server databases collection and update the new instance to the server. To delete a database, you drop the <xref:Microsoft.AnalysisServices.Database> object by using its own Drop method.  
  
 Databases can be backed up by using the BackUp method (from the <xref:Microsoft.AnalysisServices.Database> object or from the <xref:Microsoft.AnalysisServices.Server> object), but can only be restored from the <xref:Microsoft.AnalysisServices.Server> object with the Restore method.  
  
 For more information about methods and properties available, see <xref:Microsoft.AnalysisServices.Database> in the <xref:Microsoft.AnalysisServices>.  
  
##  <a name="DSandDSV"></a> DataSource and DataSourceView Objects  
 Data sources are managed by using the <xref:Microsoft.AnalysisServices.DataSourceCollection> from the database class. An instance of <xref:Microsoft.AnalysisServices.DataSource> can be created by using the Add method from a <xref:Microsoft.AnalysisServices.DataSourceCollection> object. An instance of <xref:Microsoft.AnalysisServices.DataSource> can be deleted by using the Remove method from a <xref:Microsoft.AnalysisServices.DataSourceCollection> object.  
  
 <xref:Microsoft.AnalysisServices.DataSourceView> objects are managed from the <xref:Microsoft.AnalysisServices.DataSourceViewCollection> object in the database class.  
  
 For more information about methods and properties available, see <xref:Microsoft.AnalysisServices.DataSource> and <xref:Microsoft.AnalysisServices.DataSourceView> in the <xref:Microsoft.AnalysisServices>.  
  
## See Also  
 <xref:Microsoft.AnalysisServices>   
 [Introducing AMO Classes](../../../analysis-services/multidimensional-models/analysis-management-objects/amo-classes-introduction.md)   
 [Programming AMO Fundamental Objects](../../../analysis-services/multidimensional-models/analysis-management-objects/programming-amo-fundamental-objects.md)  
  
  