---
title: "Test an Analysis Services tabular model in DirectQuery mode | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tabular-models
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Test a model in DirectQuery mode
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  Review your options for testing a tabular model in DirectQuery mode at each stage of development, starting with design.  
  
## Test in Excel 
  
 When designing your model in SSDT, you can use the **Analyze in Excel** feature to test your modeling decisions against a sample dataset in-memory, or against the relational database.  When you click Analyze in Excel, a dialog box opens where you can specify options.
 
 ![Analyze in Excel DirectQuery options](../../analysis-services/tabular-models/media/analyze-in-excel-directquery-options.png)
 
 If your model's DirectQuery mode is on, you can specify DirectQuery connection mode, where you'll have two options:
 - **Sample data view** - With this option, any queries from Excel are directed to sample partitions containing a sample dataset in-memory. This option is helpful when you want to make sure any DAX formulas you have in measures, calculated columns, or row-level security perform correctly.
 
 - **Full data view** - With this option, any queries from Excel are sent to Analysis Services, and then on to the relational database. This option is, in-effect, fully functioning DirecQuery mode.
 
 ### Other clients
 When you use Analyze in Excel, an .odc connection file is created. You can use the connection string information from this file to connect to your model from other client applications. An additional parameter, DataView=Sample, is added to specify the client should connect to sample data partitions.  
  
## Monitor query execution on backend systems using xEvents or SQL Profiler 
 Start up a session trace, connected to the SQL Server relational database, to monitor connections coming from the Tabular model:  
  
-   [Monitor Analysis Services with SQL Server Extended Events](../../analysis-services/instances/monitor-analysis-services-with-sql-server-extended-events.md)  
  
-   [Use SQL Server Profiler to Monitor Analysis Services](../../analysis-services/instances/use-sql-server-profiler-to-monitor-analysis-services.md)  
  
 If you're using Oracle or Teradata, use the trace monitoring tools for those systems.  
  
  
