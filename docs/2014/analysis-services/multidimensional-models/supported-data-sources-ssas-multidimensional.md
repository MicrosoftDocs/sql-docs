---
title: "Data Sources Supported (SSAS Multidimensional) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server Analysis Services, data sources"
  - "data sources [Analysis Services], about data sources"
  - "Analysis Services, data sources"
  - "connections [Analysis Services]"
  - "SSAS, data sources"
ms.assetid: c97e0f8d-7ddd-4941-8b51-e7832f30fbbe
author: minewiskan
ms.author: owend
manager: craigg
---
# Data Sources Supported (SSAS Multidimensional)
  This topic describes the types of data sources that you can use in a multidimensional model.  
  
##  <a name="bkmk_supported_ds"></a> Supported Data Sources  
 You can retrieve data from the data sources in the following table. When you install [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], setup does not install the providers that are listed for each data source. Some providers might already be installed with other applications on your computer; in other cases you will need to download and install the provider.  
  
> [!NOTE]  
>  Third party providers, such as the Oracle OLE DB Provider, may also be used to connect to third party databases, with support provided by those third parties.  
  
|||||  
|-|-|-|-|  
|Source|Versions|File type|Providers <sup>1</sup>|  
|Access databases|Microsoft Access 2007, 2010, 2013.|.accdb or .mdb|Microsoft Jet 4.0 OLE DB provider|  
|SQL Server relational databases <sup>5</sup>|Microsoft SQL Server 2005, 2008, 2008 R2, 2012, 2014 [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]<sup>2</sup>, SQL Server Parallel Data Warehouse (PDW) <sup>3</sup>|(not applicable)|OLE DB Provider for SQL Server<br /><br /> SQL Server Native Client OLE DB Provider<br /><br /> SQL Server Native 11.0 Client OLE DB Provider<br /><br /> .NET Framework Data Provider for SQL Client|  
|Oracle relational databases|Oracle 9i, 10g, 11g.|(not applicable)|Oracle OLE DB Provider<br /><br /> .NET Framework Data Provider for Oracle Client<br /><br /> .NET Framework Data Provider for SQL Server<br /><br /> MSDAORA OLE DB provider <sup>4</sup><br /><br /> OraOLEDB<br /><br /> MSDASQL|  
|Teradata relational databases|Teradata V2R6, V12|(not applicable)|TDOLEDB OLE DB provider<br /><br /> .Net Data Provider for Teradata|  
|Informix relational databases|V11.10|(not applicable)|Informix OLE DB provider|  
|IBM DB2 relational databases|8.1|(not applicable)|DB2OLEDB|  
|Sybase Adaptive Server Enterprise (ASE) relational databases|15.0.2|(not applicable)|Sybase OLE DB provider|  
|Other relational databases|(not applicable)|(not applicable)|An OLE DB provider|  
  
 <sup>1</sup> ODBC data sources are not supported for multidimensional solutions. Although Analysis Services itself will handle the connection, the designers in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] used for building solutions cannot connect to an ODBC data source, even when using the MSDASQL driver. If your business requirements include an ODBC data source, consider building a tabular solution instead.  
  
 <sup>2</sup> For more information, see [!INCLUDE[ssSDS](../../includes/sssds-md.md)], on [azure.microsoft.com](https://go.microsoft.com/fwlink/?LinkID=157856).  
  
 <sup>3</sup> For more information about [!INCLUDE[ssSDS](../../includes/sssds-md.md)] PDW, see [SQL Server Parallel Data Warehouse](https://go.microsoft.com/fwlink/?LinkId=150895).  
  
 <sup>4</sup> In some cases, using the MSDAORA OLE DB provider can result in connection errors, particularly with newer versions of Oracle. If you encounter any errors, we recommend that you use one of the other providers listed for Oracle.  
  
 <sup>5</sup> Some features require a SQL Server relational database that runs on-premises. Specifically, writeback and ROLAP storage require that the underlying data source is a SQL Server relational database.  
  
## See Also  
 [Data Sources Supported &#40;SSAS Tabular&#41;](../tabular-models/data-sources-supported-ssas-tabular.md)   
 [Data Sources in Multidimensional Models](data-sources-in-multidimensional-models.md)   
 [Data Source Views in Multidimensional Models](data-source-views-in-multidimensional-models.md)  
  
  
