---
title: "Analysis Services Features Supported by the Editions of SQL Server 2016 | Microsoft Docs"
ms.custom: ""
ms.date: "09/02/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/data-mining"
  - "analysis-services/multidimensional-tabular"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: f09d7be1-bd63-43f8-b91c-bf19166b4457
caps.latest.revision: 4
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Analysis Services Features Supported by the Editions of SQL Server 2016
This topic provides details of features supported by the different editions of [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].  
  
 SQL Server Evaluation edition is available for a 180-day trial period.  
  
 For the latest release notes, see [SQL Server 2016 Release Notes](../sql-server/sql-server-2016-release-notes.md). For the latest information on what is new, see [What's New in Analysis Services](../analysis-services/what-s-new-in-analysis-services.md).
    
 **Try SQL Server 2016!**    
    
 > [![Download from Evaluation Center](../analysis-services/media/download.png)](https://www.microsoft.com/en-us/evalcenter/evaluate-sql-server-2016) **[Download SQL Server 2016  from the Evaluation Center](https://www.microsoft.com/en-us/evalcenter/evaluate-sql-server-2016)**    
    
> ![Azure Virtual Machine small](../analysis-services/media/azure-virtual-machine-small.png) **[Spin up a Virtual Machine with SQL Server 2016 already installed](https://azure.microsoft.com/en-us/marketplace/partners/microsoft/sqlserver2016rtmenterprisewindowsserver2012r2/?wt.mc_id=sqL16_vm)**    
    
  
 For features supported by Evaluation and Developer editions see SQL Server Enterprise Edition.
  
 To navigate the table for a SQL Server technology, click on its link: 
 
 -  [Analysis Services](#SSAS)  
  
-   [BI Semantic Model (Multi Dimensional)](#BIMD)  
  
-   [BI Semantic Model (Tabular)](#BIT)  
  
-   [Power Pivot for SharePoint](#PPSP)  
  
-   [Data Mining](#DM)  
  
-   [Business Intelligence Clients](#BIC)  

##  <a name="SSAS"></a> Analysis Services  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express with Tools|Express|Developer|  
|-------------|----------------|--------------|---------|------------------------------------|------------------------|-------------|---------------|  
|Scalable shared databases|Yes||||||Yes|  
|Backup/Restore & Attach/Detach databases|Yes|Yes|||||Yes|  
|Synchronize databases|Yes||||||Yes|  
|Always On failover cluster instances|Yes<br /><br /> Number of nodes is the operating system maximum|Yes<br /><br /> Support for 2 nodes|||||Yes<br /><br /> Number of nodes is the operating system maximum|  
|Programmability (AMO, ADOMD.Net, OLEDB, XML/A, ASSL, TMSL)|Yes|Yes|||||Yes|  
  
##  <a name="BIMD"></a> BI Semantic Model (Multi Dimensional)  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express with Tools|Express|Developer|  
|-------------|----------------|--------------|---------|------------------------------------|------------------------|-------------|---------------|  
|Semi-additive measures|Yes|No <sup>1</sup>|||||Yes|  
|Hierarchies|Yes|Yes|||||Yes|  
|KPIs|Yes|Yes|||||Yes|  
|Perspectives|Yes||||||Yes|  
|Actions|Yes|Yes|||||Yes|  
|Account intelligence|Yes|Yes|||||Yes|  
|Time intelligence|Yes|Yes|||||Yes|  
|Custom rollups|Yes|Yes|||||Yes|  
|Writeback cube|Yes|Yes|||||Yes|  
|Writeback dimensions|Yes||||||Yes|  
|Writeback cells|Yes|Yes|||||Yes|  
|Drillthrough|Yes|Yes|||||Yes|  
|Advanced hierarchy types (parent-child and ragged hierarchies)|Yes|Yes|||||Yes|  
|Advanced dimensions (reference dimensions, many-to-many dimensions)|Yes|Yes|||||Yes|  
|Linked measures and dimensions|Yes|Yes  <sup>2</sup> |||||Yes|  
|Translations|Yes|Yes|||||Yes|  
|Aggregations|Yes|Yes|||||Yes|  
|Multiple partitions|Yes|Yes, up to 3|||||Yes|  
|Proactive caching|Yes||||||Yes|  
|Custom assemblies (stored procedures)|Yes|Yes|||||Yes|  
|MDX queries and scripts|Yes|Yes|||||Yes|  
|DAX queries|Yes|Yes|||||Yes|  
|Role-based security model|Yes|Yes|||||Yes|  
|Dimension and cell-level security|Yes|Yes|||||Yes|  
|Scalable string storage|Yes|Yes|||||Yes|  
|MOLAP, ROLAP, and HOLAP storage models|Yes|Yes|||||Yes|  
|Binary and compressed XML transport|Yes|Yes|||||Yes|  
|Push-mode processing|Yes||||||Yes|  
|Direct writeback|Yes||||||Yes|  
|Measure expressions|Yes||||||Yes|  
  
 <sup>1</sup> The LastChild semi-additive measure is supported in Standard edition, but other semi-additive measures, such as None, FirstChild, FirstNonEmpty, LastNonEmpty, AverageOfChildren, and ByAccount, are not. Additive measures, such as Sum, Count, Min, Max, and non-additive measures (DistinctCount) are supported on all editions.  
  <sup>2</sup> Standard edition supports linking measures and dimensions within the same database, but not from other databases or instances.
   
##  <a name="BIT"></a> BI Semantic Model (Tabular)  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express with Tools|Express|Developer|  
|-------------|----------------|--------------|---------|------------------------------------|------------------------|-------------|---------------|  
|Hierarchies|Yes|Yes|||||Yes|  
|KPIs|Yes|Yes|||||Yes|  
|Perspectives|Yes||||||Yes|  
|Translations|Yes|Yes|||||Yes|  
|DAX calculations, DAX queries, MDX queries|Yes|Yes|||||Yes|  
|Row-level security|Yes|Yes|||||Yes|  
|Multiple partitions|Yes||||||Yes|  
|In-memory storage mode|Yes|Yes|||||Yes|  
|DirectQuery storage mode|Yes||||||Yes|  
  
##  <a name="PPSP"></a> Power Pivot for SharePoint  
  
|Feature|Enterprise|Standard|Web|Express with Advanced Services|Express with Tools|Express|Developer|  
|-------------|----------------|--------------|---------|------------------------------------|------------------------|-------------|---------------|  
|SharePoint farm integration based on shared service architecture|Yes||||||Yes|  
|Usage reporting|Yes||||||Yes|  
|Health monitoring rules|Yes||||||Yes|  
|Power Pivot gallery|Yes||||||Yes|  
|Power Pivot data refresh|Yes||||||Yes|  
|Power Pivot data feeds|Yes||||||Yes|  
  
##  <a name="DM"></a> Data Mining  
  
|Feature Name|Enterprise|Standard|Web|Express with Advanced Services|Express with Tools|Express|Developer|  
|------------------|----------------|--------------|---------|------------------------------------|------------------------|-------------|---------------|  
|Standard algorithms|Yes|Yes|||||Yes|  
|Data mining tools (Wizards, Editors,  Query Builders)|Yes|Yes|||||Yes|  
|Cross validation|Yes||||||Yes|  
|Models on filtered subsets of mining structure data|Yes||||||Yes|  
|Time series: Custom blending between ARTXP and ARIMA methods|Yes||||||Yes|  
|Time series: Prediction with new data|Yes||||||Yes|  
|Unlimited concurrent DM queries|Yes||||||Yes|  
|Advanced configuration & tuning options for data mining algorithms|Yes||||||Yes|  
|Support for plug-in algorithms|Yes||||||Yes|  
|Parallel model processing|Yes||||||Yes|  
|Time series: cross-series prediction|Yes||||||Yes|  
|Unlimited attributes for association rules|Yes||||||Yes|  
|Sequence prediction|Yes||||||Yes|  
|Multiple prediction targets for naïve Bayes, neural network and logistic regression|Yes||||||Yes|  
  
##  <a name="BIC"></a> Business Intelligence Clients  
 The following software client applications are available on the Microsoft Download center and are provided to assist you with creating business intelligence documents that run on a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance. When you host these documents in a server environment, use an edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] that supports that document type. The following table identifies which [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] edition contains the server features required to host the documents created in these client applications.  
  
|Tool Name|Enterprise|Standard|Web|Express with Advanced Services|Express with Tools|Express|Developer|  
|---------------|----------------|--------------|---------|------------------------------------|------------------------|-------------|---------------|  
|Data Mining Add-ins for Excel and Visio 2010 (.xlsx, .vsdx)|Yes|Yes|||||Yes|  
|[!INCLUDE[ssGeminiClient](../includes/ssgeminiclient-md.md)] 2010 and 2013 (.xlsx)|Yes||||||Yes|  
|[!INCLUDE[ssMDSshort](../includes/ssmdsshort-md.md)] [!INCLUDE[ssMDSXLS](../includes/ssmdsxls-md.md)] (.xlsx)|Yes||||||Yes|  
  
> [!NOTE]  
>  1.  [!INCLUDE[ssGeminiClient](../includes/ssgeminiclient-md.md)] is an Excel add-in for creating workbooks with a data model.  [!INCLUDE[ssGeminiClient](../includes/ssgeminiclient-md.md)] does not depend on [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], but [!INCLUDE[ssGeminiShort](../includes/ssgeminishort-md.md)] is required for sharing and collaborating on Excel workbooks with a data model in SharePoint. This capability is available as part of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Enterprise edition.  
>   
>      In Excel 2016, Power Pivot capability is built in, so you no longer need the Power Pivot add-in.  
> 2.  The above table identifies the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] editions that are required to enable these client tools; however these tools can access data hosted on any edition of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 ## See Also  
 [Product Specifications for SQL Server 2016](http://msdn.microsoft.com/library/6445fd53-6844-4170-a86b-7fe76a9f64cb)   
 [Installation for SQL Server 2016](../database-engine/install-windows/installation-for-sql-server-2016.md)  


