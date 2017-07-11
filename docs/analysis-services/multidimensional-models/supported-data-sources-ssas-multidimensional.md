---
title: "Supported Data Sources (SSAS - Multidimensional) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "SQL Server Analysis Services, data sources"
  - "data sources [Analysis Services], about data sources"
  - "Analysis Services, data sources"
  - "connections [Analysis Services]"
  - "SSAS, data sources"
ms.assetid: c97e0f8d-7ddd-4941-8b51-e7832f30fbbe
caps.latest.revision: 69
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Supported Data Sources (SSAS - Multidimensional)
  This topic describes the types of data sources that you can use in a multidimensional model.  
  
##  <a name="bkmk_supported_ds"></a> Supported Data Sources  
 You can retrieve data from the data sources in the following table. When you install [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)], setup does not install the providers that are listed for each data source. Some providers might already be installed with other applications on your computer; in other cases you will need to download and install the provider.  
  
> [!NOTE]  
>  Third party providers, such as the Oracle OLE DB Provider, may also be used to connect to third party databases, with support provided by those third parties.  
  
|||||  
|-|-|-|-|  
|Source|Versions|File type|Providers*|  
|Access databases|Microsoft Access  2010, 2013, 2016|.accdb or .mdb|Microsoft Jet 4.0 OLE DB provider|  
|SQL Server relational databases*|Microsoft SQL Server 2008, 2008 R2, 2012, 2014, 2016, [!INCLUDE[ssSDSFull](../../includes/sssdsfull-md.md)] , Microsoft Analytics Platform System (APS)<br /><br /> <br /><br /> Note: For more information about [!INCLUDE[ssSDS](../../includes/sssds-md.md)] on [Azure.com](http://go.microsoft.com/fwlink/?LinkID=157856).<br /><br /> Note: Analytics Platform System (APS) was formerly known as SQL Server Parallel Datawarehouse (PDW). Originally, connecting to PDW from Analysis Services required a special data provider. This provider was replaced in SQL Server 2012. Starting in SQL Server 2012, the SQL Server native client is used for connections to PDW/APS. For more information about APS, see the web site [Microsoft Analytics Platform System](http://www.microsoft.com/en-us/server-cloud/products/analytics-platform-system/resources.aspx).|(not applicable)|OLE DB Provider for SQL Server<br /><br /> SQL Server Native Client OLE DB Provider<br /><br /> SQL Server Native 11.0 Client OLE DB Provider<br /><br /> .NET Framework Data Provider for SQL Client|  
|Oracle relational databases|Oracle 9i, 10g, 11g, 12g|(not applicable)|Oracle OLE DB Provider<br /><br /> .NET Framework Data Provider for Oracle Client<br /><br /> .NET Framework Data Provider for SQL Server<br /><br /> OraOLEDB<br /><br /> MSDASQL|  
|Teradata relational databases|Teradata V2R6, V12|(not applicable)|TDOLEDB OLE DB provider<br /><br /> .Net Data Provider for Teradata|  
|Informix relational databases|V11.10|(not applicable)|Informix OLE DB provider|  
|IBM DB2 relational databases|8.1|(not applicable)|DB2OLEDB|  
|Sybase Adaptive Server Enterprise (ASE) relational databases|15.0.2|(not applicable)|Sybase OLE DB provider|  
|Other relational databases|(not applicable)|(not applicable)|An OLE DB provider|  
  
 \* ODBC data sources are not supported for multidimensional solutions. Although Analysis Services itself will handle the connection, the designers in [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] used for building solutions cannot connect to an ODBC data source, even when using the MSDASQL driver. If your business requirements include an ODBC data source, consider building a Tabular solution instead.  
  
 ** Some features require a SQL Server relational database that runs on-premises. Specifically, writeback, and ROLAP storage require that the underlying data source is a SQL Server relational database.  
  
## See Also  
 [Data Sources Supported &#40;SSAS Tabular&#41;](../../analysis-services/tabular-models/data-sources-supported-ssas-tabular.md)   
 [Data Sources in Multidimensional Models](../../analysis-services/multidimensional-models/data-sources-in-multidimensional-models.md)   
 [Data Source Views in Multidimensional Models](../../analysis-services/multidimensional-models/data-source-views-in-multidimensional-models.md)  
  
  