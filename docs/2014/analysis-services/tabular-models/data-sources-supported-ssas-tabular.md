---
title: "Data Sources Supported (SSAS Tabular) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: d6c2b1b3-91fc-4175-af25-509946dc7f24
author: minewiskan
ms.author: owend
manager: craigg
---
# Data Sources Supported (SSAS Tabular)
  This topic describes the types of data sources that can be used with tabular models.  
  
 This article contains the following sections:  
  
-   [Supported Data Sources](#bkmk_supported_ds)  
  
-   [Unsupported Sources](#bkmk_unsupported_ds)  
  
-   [Tips for Choosing Data Sources](#bkmk_tips)  
  
##  <a name="bkmk_supported_ds"></a> Supported Data Sources  
 You can import data from the data sources in the following table. When you install [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], setup does not install the providers that are listed for each data source. Some providers might already be installed with other applications on your computer; in other cases you will need to download and install the provider.  
  
|||||  
|-|-|-|-|  
|Source|Versions|File type|Providers <sup>1</sup>|  
|Access databases|Microsoft Access 2003, 2007, 2010.|.accdb or .mdb|ACE 14 OLE DB provider|  
|SQL Server relational databases|Microsoft SQL Server2005, 2008, 2008 R2; SQL Server 2012, Microsoft SQL Azure Database <sup>2</sup>|(not applicable)|OLE DB Provider for SQL Server<br /><br /> SQL Server Native Client OLE DB Provider<br /><br /> SQL Server Native 10.0 Client OLE DB Provider<br /><br /> .NET Framework Data Provider for SQL Client|  
|SQL Server Parallel Data Warehouse (PDW) <sup>3</sup>|2008 R2|(not applicable)|OLE DB provider for SQL Server PDW|  
|Oracle relational databases|Oracle 9i, 10g, 11g.|(not applicable)|Oracle OLE DB Provider<br /><br /> .NET Framework Data Provider for Oracle Client<br /><br /> .NET Framework Data Provider for SQL Server<br /><br /> OraOLEDB<br /><br /> MSDASQL|  
|Teradata relational databases|Teradata V2R6, V12|(not applicable)|TDOLEDB OLE DB provider<br /><br /> .Net Data Provider for Teradata|  
|Informix relational databases||(not applicable)|Informix OLE DB provider|  
|IBM DB2 relational databases|8.1|(not applicable)|DB2OLEDB|  
|Sybase Adaptive Server Enterprise (ASE) relational databases|15.0.2|(not applicable)|Sybase OLE DB provider|  
|Other relational databases|(not applicable)|(not applicable)|OLE DB provider or ODBC driver|  
|Text files|(not applicable)|.txt, .tab, .csv|ACE 14 OLE DB provider for Microsoft Access|  
|Microsoft Excel files|Excel 97-2003, 2007, 2010|.xlsx, xlsm, .xlsb, .xltx, .xltm|ACE 14 OLE DB provider|  
|[!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbook|Microsoft SQL Server 2008 R2 Analysis Services|xlsx, xlsm, .xlsb, .xltx, .xltm|ASOLEDB 10.5<br /><br /> (used only with [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbooks that are published to SharePoint farms that have [!INCLUDE[ssGeminiShort](../../includes/ssgeminishort-md.md)] installed)|  
|Analysis Services cube|Microsoft SQL Server 2005, 2008, 2008 R2 Analysis Services|(not applicable)|ASOLEDB 10|  
|Data feeds<br /><br /> (used to import data from Reporting Services reports, Atom service documents, Microsoft Azure Marketplace DataMarket, and single data feed)|Atom 1.0 format<br /><br /> Any database or document that is exposed as a Windows Communication Foundation (WCF) Data Service (formerly ADO.NET Data Services).|.atomsvc for a service document that defines one or more feeds<br /><br /> .atom for an Atom web feed document|Microsoft Data Feed Provider for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]<br /><br /> .NET Framework data feed data provider for [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)]|  
|Office Database Connection files||.odc||  
  
 <sup>1</sup> You can also use the OLE DB Provider for ODBC.  
  
 <sup>2</sup> For more information about SQL Azure, see the web site [SQL Azure](https://go.microsoft.com/fwlink/?LinkID=157856).  
  
 <sup>3</sup> For more information about SQL Server PDW, see the web site [SQL Server 2008 R2 Parallel Data Warehouse](https://go.microsoft.com/fwlink/?LinkId=150895).  
  
 <sup>4</sup> In some cases, using the MSDAORA OLE DB provider can result in connection errors, particularly with newer versions of Oracle. If you encounter any errors, we recommend that you use one of the other providers listed for Oracle.  
  
##  <a name="bkmk_unsupported_ds"></a> Unsupported Sources  
 The following data source is not currently supported:  
  
-   Server documents such as Access databases already published to SharePoint, cannot be imported.  
  
##  <a name="bkmk_tips"></a> Tips for Choosing Data Sources  
  
1.  Importing tables from relational databases saves you steps because *foreign key* relationships are used during import to create relationships between tables in the model designer.  
  
2.  Importing multiple tables, and then deleting the ones you don't need, can also save you steps. If you import tables one at a time, you might still need to create relationships between the tables manually.  
  
3.  Columns that contain similar data in different data sources are the basis of creating relationships within the model designer. When using heterogeneous data sources, choose tables that have columns that can be mapped to tables in other data sources that contain identical or similar data.  
  
4.  OLE DB providers can sometimes offer faster performance for large scale data. When choosing between different providers for the same data source, you should try the OLE DB provider first.  
  
## See Also  
 [Data Sources &#40;SSAS Tabular&#41;](../data-sources-ssas-tabular.md)   
 [Import Data &#40;SSAS Tabular&#41;](../import-data-ssas-tabular.md)  
  
  
