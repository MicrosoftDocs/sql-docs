---
title: "Data sources supported in SQL Server Analysis Services tabular models | Microsoft Docs"
ms.custom: ""
ms.date: "10/16/2017"
ms.prod: "analysis-services"
ms.prod_service: "analysis-services, azure-analysis-services"
ms.service: ""
ms.component: ""
ms.reviewer: ""
ms.suite: "pro-bi"
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: d6c2b1b3-91fc-4175-af25-509946dc7f24
caps.latest.revision: 28
author: "Minewiskan"
ms.author: "owend"
manager: "kfile"
ms.workload: "On Demand"
---
# Data sources supported in tabular models
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
For Azure Analysis Services, see [Data sources supported in Azure Analysis Services](https://docs.microsoft.com/azure/analysis-services/analysis-services-datasource).

  This topic describes the types of data sources that can be used with tabular models.  
  
##  <a name="bkmk_supported_ds"></a> Supported data sources for in-memory tabular models  
When you install [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], setup does not install the providers that are listed for each data source. Some providers might be installed with other applications on your computer. In other cases, you may need to download and install the provider.  
  
|||||  
|-|-|-|-|  
|Source|Versions|File type|Providers|  
|Access databases|Microsoft Access 2010 and later.|.accdb or .mdb|ACE 14 OLE DB provider|  
|SQL Server relational databases|SQL Server 2008 and later, SQL Server Data Warehouse 2008 and later, Azure SQL Database, Azure SQL Data Warehouse, Analytics Platform System (APS)<br /><br /> <br /><br /> Analytics Platform System (APS) was formerly known as SQL Server Parallel Data Warehouse (PDW). Originally, connecting to PDW from Analysis Services required a special data provider. This provider was replaced in SQL Server 2012. Starting in SQL Server 2012, the SQL Server native client is used for connections to PDW/APS. |(not applicable)|OLE DB Provider for SQL Server<br /><br /> SQL Server Native Client OLE DB Provider<br /><br /> SQL Server Native 10.0 Client OLE DB Provider<br /><br /> .NET Framework Data Provider for SQL Client|  
|Oracle relational databases|Oracle 9i and later.|(not applicable)|Oracle OLE DB Provider<br /><br /> .NET Framework Data Provider for Oracle Client<br /><br /> .NET Framework Data Provider for SQL Server<br /><br /> OraOLEDB<br /><br /> MSDASQL|  
|Teradata relational databases|Teradata V2R6 and later|(not applicable)|TDOLEDB OLE DB provider<br /><br /> .Net Data Provider for Teradata|  
|Informix relational databases||(not applicable)|Informix OLE DB provider|  
|IBM DB2 relational databases|8.1|(not applicable)|DB2OLEDB|  
|Sybase Adaptive Server Enterprise (ASE) relational databases|15.0.2|(not applicable)|Sybase OLE DB provider|  
|Other relational databases|(not applicable)|(not applicable)|OLE DB provider or ODBC driver|  
|Text files|(not applicable)|.txt, .tab, .csv|ACE 14 OLE DB provider for Microsoft Access|  
|Microsoft Excel files|Excel 2010 and later|.xlsx, xlsm, .xlsb, .xltx, .xltm|ACE 14 OLE DB provider|  
|[!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook|Microsoft SQL Server 2008 and later Analysis Services|xlsx, xlsm, .xlsb, .xltx, .xltm|ASOLEDB 10.5<br /><br /> (used only with [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbooks that are published to SharePoint farms that have [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] installed)|  
|Analysis Services cube|Microsoft SQL Server 2008 and later Analysis Services|(not applicable)|ASOLEDB 10|  
|Data feeds<br /><br /> (used to import data from Reporting Services reports, Atom service documents, Microsoft Azure Marketplace DataMarket, and single data feed)|Atom 1.0 format<br /><br /> Any database or document that is exposed as a Windows Communication Foundation (WCF) Data Service (formerly ADO.NET Data Services).|`.atomsvc` for a service document that defines one or more feeds<br /><br /> .atom for an Atom web feed document|Microsoft Data Feed Provider for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]<br /><br /> .NET Framework data feed data provider for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]|  
|Office Database Connection files||.odc||  
  
  
##  <a name="bkmk_supported_ds_dq"></a> Supported data sources for DirectQuery models  
 DirectQuery is an alternative to in-memory storage mode, routing queries to and returning results directly from backend data systems rather than storing all data inside the model (and in RAM once the model is loaded). Because Analysis Services has to formulate queries in the native database query syntax, a smaller subset of data sources is supported for this mode.  
  
Data source   |Versions  |Providers
---------|---------|---------
Microsoft SQL Server    |  2008 and later      |       OLE DB Provider for SQL Server, SQL Server Native Client OLE DB Provider, .NET Framework Data Provider for SQL Client  
Microsoft Azure SQL Database    |   All      |  OLE DB Provider for SQL Server, SQL Server Native Client OLE DB Provider, .NET Framework Data Provider for SQL Client            
Microsoft Azure SQL Data Warehouse     |   All     |  SQL Server Native Client OLE DB Provider, .NET Framework Data Provider for SQL Client       
Microsoft SQL Analytics Platform System (APS)     |   All      |  OLE DB Provider for SQL Server, SQL Server Native Client OLE DB Provider, .NET Framework Data Provider for SQL Client       
Oracle relational databases     |  Oracle 9i and later       |  Oracle OLE DB Provider       
Teradata relational databases    |  Teradata V2R6 and later     | .Net Data Provider for Teradata    

  
##  <a name="bkmk_tips"></a> Tips for choosing data sources  
  
Importing tables from relational databases saves you steps because *foreign key* relationships are used during import to create relationships between tables in the model designer.  
  
Importing multiple tables, and then deleting the ones you don't need, can also save you steps. If you import tables one at a time, you might still need to create relationships between the tables manually.  
  
Columns that contain similar data in different data sources are the basis of creating relationships within the model designer. When using heterogeneous data sources, choose tables that have columns that can be mapped to tables in other data sources that contain identical or similar data.  
  
OLE DB providers can sometimes offer faster performance for large-scale data. When choosing between different providers for the same data source, you should try the OLE DB provider first.  
