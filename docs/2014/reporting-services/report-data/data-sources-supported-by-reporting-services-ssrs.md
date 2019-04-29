---
title: "Data Sources Supported by Reporting Services (SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server data processing extension [Reporting Services]"
  - "XML data processing extension [Reporting Services]"
  - "reports [Reporting Services], data"
  - "data processing extensions [Reporting Services], data sources"
  - "Oracle [Reporting Services]"
  - "OLE DB data processing extension"
  - "data sources [Reporting Services], about data sources"
  - "ODBC data processing extension"
  - "Reporting Services, data sources"
ms.assetid: 9d11d055-a3be-45aa-99a7-46447a94ed42
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Data Sources Supported by Reporting Services (SSRS)
  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] retrieves report data from data sources through a modular and extensible data layer that uses data processing extensions. To retrieve report data from a data source, you must select a data processing extension that supports the type of data source, the version of software running on the data source, and the data source platform (32-bit or 64-bit [!INCLUDE[vcprx64](../../includes/vcprx64-md.md)]).  
  
 When you deploy [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], a set of data processing extensions are automatically installed and registered on both the report authoring client and on the report server to provide access to a variety of data source types. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installs the following data source types:  
  
-   [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
  
-   [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] for MDX, DMX, [!INCLUDE[msCoName](../../includes/msconame-md.md)] PowerPivot, and tabular models  
  
-   [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)]  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Parallel Data Warehouse  
  
-   Oracle  
  
-   SAP NetWeaver BI  
  
-   [!INCLUDE[extEssbase](../../includes/extessbase-md.md)]  
  
-   [!INCLUDE[msCoName](../../includes/msconame-md.md)] SharePoint List  
  
-   Teradata  
  
-   OLE DB  
  
-   ODBC  
  
-   XML  
  
 In addition, custom data processing extensions and standard [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] data providers can be installed and registered by system administrators. To process and view a report, the data processing extensions and data providers must be installed and registered on the report server; to preview a report, they must be installed and registered on the report authoring client. Data processing extensions and data providers must be natively compiled for the platform where they are installed. If you deploy a data source programmatically by using the SOAP Web service, you must define the data source extension. Use data extension values from the **RSReportDesigner.config** file. By default, the file is located in the following folder:  
  
```  
<drive letter>\Program Files (x86)\Microsoft Visual Studio 10.0\Common7\IDE\PrivateAssemblies  
```  
  
 For example, the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data extension is OLEDB-MD.  
  
 Many third-party standard [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] data providers are available as downloads from the [Microsoft Download Center](https://go.microsoft.com/fwlink/?linkid=51456) and from third-party sites. You can also search the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] public forum for information about third-party data providers.  
  
> [!NOTE]  
>  Standard [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] data providers do not necessarily support all the functionality supplied by [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extensions. In addition, some OLE DB data providers and ODBC drivers can be used to author and preview reports, but are not designed to support reports published on a report server. For example, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB Provider for Jet is not supported on the report server. For more information, see [Data Processing Extensions and .NET Framework Data Providers &#40;SSRS&#41;](data-processing-extensions-and-net-framework-data-providers-ssrs.md).  
  
 For more information about custom data processing extensions, see [Implementing a Data Processing Extension](../extensions/data-processing/implementing-a-data-processing-extension.md). For more information about standard [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] data providers, see the <xref:System.Data> namespace.  
  
 For more information about data processing extensions supported by Report Builder, see [Data Connections, Data Sources, and Connection Strings in Report Builder](../data-connections-data-sources-and-connection-strings-in-report-builder.md) in the [Report Builder documentation](https://go.microsoft.com/fwlink/?LinkId=154494) on msdn.microsoft.com.  
  
## Platform Support for Report Data Sources  
 The data sources you can use in a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] deployment vary by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] edition, [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] version, and by platform. For more information about features, see [Features Supported by the Editions of SQL Server 2014](../../getting-started/features-supported-by-the-editions-of-sql-server-2014.md). The table later in this topic provides information about supported data sources by version and by platform.  
  
 Platform considerations for [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data sources are separate for the report authoring client and the report server.  
  
### On the report authoring client  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ss_dtbi](../../includes/ss-dtbi-md.md)] is a 32-bit application. [!INCLUDE[ss_dtbi](../../includes/ss-dtbi-md.md)] is not supported on an Itanium-based platform. On an x64 platform, to edit and preview reports in Report Designer, you must have 32-bit data providers installed in the (x86) platform directory.  
  
### On the report server  
 When you deploy a report to a 64-bit report server (x86), the report server must have natively compiled 64-bit data providers installed. Wrapping a 32-bit data provider in 64-bit interfaces is not supported. For more information, check the documentation for the data provider.  
  
## Supported Data Sources  
 The following table lists [!INCLUDE[msCoName](../../includes/msconame-md.md)] data processing extensions and data providers that you can use to retrieve data for report datasets and report models. For more information about an extension or data provider, click the link in the second column. The table columns are described as follows:  
  
-   Source of report data: The type of data being accessed; for example, relational database, multidimensional database, flat file, or XML. This column answers the question: "What types of data can [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] use for a report?"  
  
-   [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Data Source Type: One of the data source types you see in the drop-down list when you define a data source in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. This list is populated from installed and registered DPEs and data providers. This column answers the question: "What data source type do I select from the drop-down list when I create a report data source?"  
  
-   Name of Data Processing Extension/Data Provider: The [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension or other data provider that corresponds to the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data source type selected. This column answers the question: "When I select a data source type, which corresponding data processing extension or data provider is used?"  
  
-   Underlying Data Provider version (Optional): Some data source types support more than one data provider. These could be different versions of the same provider or different implementations by third-parties for a type of data provider. The provider name frequently appears in the connection string after you have configured a data source. This column answers the question: "After selecting the data source type, what data provider do I select in the **Connection Properties** dialog box?"  
  
-   Data Source *\<platform>*: The data source platform supported by the data processing extension or data provider for the target data source. This column answers the question: "Can this data processing extension or data provider retrieve data from a data source on this type of platform?"  
  
-   Version of data source: The version of the target data source supported by the DPE or data provider. This column answers the question: "Can this data processing extension or data provider retrieve data from this version of the data source?"  
  
-   RS *\<platform>*: The platforms for the report server and report authoring client where you can install a custom DPE or data provider. The built-in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extensions are included with any installation of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. A custom data processing extension or [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] data provider must be compiled natively for a specific platform. This column answers the question: "Can this data processing extension or data provider be installed on this type of platform?"  
  
###  <a name="DataSourcesTable"></a> Types of data sources  
  
|Source of<br /><br /> Report data|Reporting Services Data Source Type|Name of Data Processing Extension/Data Provider|Underlying Data Provider version<br /><br /> (Optional)|Data<br /><br /> Source<br /><br /> Platform x86|Data<br /><br /> Source<br /><br /> Platform x64|Version of data source|RS<br /><br /> Platform x86|RS<br /><br /> Platform x64|  
|-------------------------------|-----------------------------------------|------------------------------------------------------|-------------------------------------------------------|--------------------------------------|--------------------------------------|----------------------------|-------------------------|-------------------------|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational database|[Microsoft SQL Server](#MicrosoftSQLServer)|Built-in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension|Extends System.Data.SqlClient|Y|Y|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later.|Y|Y|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational database|[OLEDB](#OLEDBSQL)|Built-in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension|Extends System.Data.OledbClient|Y|Y|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later.|Y|Y|  
|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] relational database|[ODBC](#ODBC)|Built-in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension|Extends System.Data.OdbcClient|Y|Y|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later.|Y|Y|  
|[!INCLUDE[ssSDS](../../includes/sssds-md.md)]|[Windows Azure SQL Database](#Azure)|Built-in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension|Extends System.Data.SqlClient|N/A|N/A|[!INCLUDE[ssSDS](../../includes/sssds-md.md)]|Y|Y|  
|[!INCLUDE[ssDW](../../includes/ssdw-md.md)] appliance|[Microsoft Parallel Data Warehouse](#PWD)|Built-in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension|N/A|N/A|N/A|[!INCLUDE[ssDWfull](../../includes/ssdwfull-md.md)]|Y|Y|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] multidimensional database|[Microsoft SQL Server Analysis Services](#AnalysisServices)|Built-in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension|Uses ADOMD.NET|Y|Y|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] and later<br /><br /> [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later|Y|Y|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] multidimensional database|[OLEDB](#OLEDBAS9)|Built-in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension|Extends System.Data.OledbClient<br /><br /> Version 10.0|Y|Y|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|Y|Y|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] multidimensional database|[OLEDB](#OLEDBAS9)|Built-in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension|Extends System.Data.OledbClient<br /><br /> Version 9.0|Y|Y|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]|Y|Y|  
|[!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] multidimensional database|OLEDB|Built-in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension|Extends System.Data.OledbClient<br /><br /> Version 8.0|Y|N|N/A|Y|N|  
|SharePoint lists|[Microsoft SharePoint List](#SharePointList)|Built-in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension|Gets data from Lists.asmx or the SharePoint object model API interfaces.<br /><br /> See [Note](#SharePointList).|N|Y|SharePoint 2013 Products<br /><br /> SharePoint 2010 Products|Y|Y|  
|SharePoint lists|[Microsoft SharePoint List](#SharePointList)|Built-in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension|Gets data from Lists.asmx or the SharePoint object model API interfaces.<br /><br /> See [Note](#SharePointList).|Y|Y|[!INCLUDE[winSPServ](../../includes/winspserv-md.md)] 3.0 and [!INCLUDE[offSPServ](../../includes/offspserv-md.md)] 2007|Y|Y|  
|XML|[XML](#XML)|Built-in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension|XML data sources do not have platform dependencies.|N/A|N/A|[!INCLUDE[vstecwebservices](../../includes/vstecwebservices-md.md)] or documents|Y|Y|  
|Report Server Model|Report Model|Built-in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension for a published SMDL file|Data sources for a model use Built-in data processing extensions.<br /><br /> Oracle-based models requires Oracle client components.<br /><br /> Teradata-based models require .NET Data Provider for Teradata from Teradata.<br /><br /> See Teradata documentation for platform support.|N/A|N/A|Models can be created from:[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later.<br /><br /> [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]<br /><br /> Oracle 9.2.0.3 or later<br /><br /> Teradata V14, v13, v12, and v6.2|Y|Y|  
|SAP multidimensional database|[Sap BI NetWeaver](#SapBINetWeaver)|Built-in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension|See SAP documentation for platform support.|N/A|N/A|SAP BI NetWeaver 3.5|Y|N/A|  
|[!INCLUDE[extEssbase](../../includes/extessbase-md.md)]|[Hyperion Essbase](#Hyperion)|Built-in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension|See Hyperion documentation for platform support.|Y|N/A|[!INCLUDE[extEssbase](../../includes/extessbase-md.md)] 9.3.1|Y|N/A|  
|Oracle relational database|[Oracle](#OracleClient)|Built-in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension|Extends System.Data.OracleClient<br /><br /> Requires Oracle client components.|Y|N/A|Oracle 10g, 9, 8.1.7|Y|Y|  
|Teradata relational database|[Teradata](#Teradata)|Built-in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension|Extends .NET Data Provider for Teradata from Teradata.<br /><br /> Requires .NET Data Provider for Teradata from Teradata.<br /><br /> See Teradata documentation for platform support.|Y|N/A|Teradata v14<br /><br /> Teradata v13<br /><br /> Teradata v12<br /><br /> Teradata v6.20|Y|N|  
|DB2 relational database|Customized registered data extension name||2004 Host Integration (HI) Server<br /><br /> See [HI Server documentation](https://msdn.microsoft.com/library/gg241192\(v=bts.10\).aspx).|Y|N/A|N/A|Y|N|  
|Generic OLE DB data source|[OLEDB](#OLEDBStandard)|Built-in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension|Any data source that supports OLE DB.<br /><br /> See the data source documentation for platform support.|Y|N/A|Any data source that supports OLE DB. See [Note](#OLEDBStandard).|Y|N/A|  
|Generic ODBC data source|[ODBC](#ODBCGeneric)|Built-in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension|Any data source that supports ODBC.<br /><br /> See the data source documentation for platform support.|Y|N/A|Any data source that supports ODBC. See [Note](#ODBCGeneric).|Y|Y|  
  
 For information on using a tabular data source, see [Data Connections, Data Sources, and Connection Strings in Reporting Services](../data-connections-data-sources-and-connection-strings-in-reporting-services.md).  
  
 For information about using external data sources, see [Add Data from External Data Sources &#40;SSRS&#41;](add-data-from-external-data-sources-ssrs.md).  
  
 Many standard [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] data providers are available from third parties. For more information, search the third-party Web sites or forums.  
  
 To install and register a custom data processing extension or standard [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] data provider, you will need to refer to the data provider reference documentation. For more information, see [Register a Standard .NET Framework Data Provider &#40;SSRS&#41;](register-a-standard-net-framework-data-provider-ssrs.md).  
  
 [Return to Data sources table](#DataSourcesTable)  
  
## Reporting Services Data Processing Extensions  
 The following data processing extensions are automatically installed with [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and [!INCLUDE[ss_dtbi](../../includes/ss-dtbi-md.md)]. For more information and to verify the installation, see [RSReportDesigner Configuration File](../report-server/rsreportdesigner-configuration-file.md) and [RSReportServer Configuration File](../report-server/rsreportserver-config-configuration-file.md).  
  
> [!NOTE]  
>  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)][!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] data processing extension is not supported at this time.  
  
 For more information about data processing extensions supported by Report Builder, see [Data Connections, Data Sources, and Connection Strings in Report Builder](../data-connections-data-sources-and-connection-strings-in-report-builder.md) in the [Report Builder documentation](https://go.microsoft.com/fwlink/?LinkId=154494) on msdn.microsoft.com.  
  
###  <a name="MicrosoftSQLServer"></a> Microsoft SQL Server Data Processing Extension  
 The data source type **Microsoft SQL Server** wraps and extends the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] Data Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This data processing extension is natively compiled for and runs on x86 and [!INCLUDE[vcprx64](../../includes/vcprx64-md.md)]-based platforms.  
  
 In [!INCLUDE[ss_dtbi](../../includes/ss-dtbi-md.md)], the query designer associated with this data extension is the [Visual Database Tool Designer](../../ssms/visual-db-tools/visual-database-tool-designers.md). If you use the query designer in graphical mode, the query is analyzed and possibly rewritten. Use the text-based query designer when you want to control the exact [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax that is used for a query. For more information, see [Query and View Designer Tools &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/query-and-view-designer-tools-visual-database-tools.md) and [Graphical Query Designer User Interface](graphical-query-designer-user-interface.md).  
  
 For more information, see [SQL Server Connection Type &#40;SSRS&#41;](sql-server-connection-type-ssrs.md).  
  
 In Report Builder, the query designer associated with this data extension is the Relational Query Designer. For more information, see [Relational Query Designer User Interface](../relational-query-designer-user-interface.md).  
  
 [Return to Data sources table](#DataSourcesTable)  
  
###  <a name="Azure"></a> Windows Azure SQL Database Processing Extension  
 The data source type **[!INCLUDE[ssSDS](../../includes/sssds-md.md)]** wraps and extends the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] Data Provider for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 In [!INCLUDE[ss_dtbi](../../includes/ss-dtbi-md.md)], the graphical query designer associated with this data extension is the [Relational Query Designer User Interface](../relational-query-designer-user-interface.md), not the [Visual Database Tool Designer](../../ssms/visual-db-tools/visual-database-tool-designers.md) that you use with the **Microsoft SQL Server** data source type.  
  
 [!INCLUDE[ss_dtbi](../../includes/ss-dtbi-md.md)] automatically differentiates between **[!INCLUDE[ssSDS](../../includes/sssds-md.md)]** and **Microsoft SQL Server** data source types and opens the graphical query designer associated with the data source type.  
  
 If you use the query designer in graphical mode, the query is analyzed and possibly rewritten. A text-based query designer is also available for writing queries. Use the text-based query designer when you want to control the exact [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax that is used for a query. For more information, see [Text-based Query Designer User Interface](../text-based-query-designer-user-interface.md).  
  
 Retrieving data from [!INCLUDE[ssSDS](../../includes/sssds-md.md)] and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is similar, but there are a few requirements that apply only to [!INCLUDE[ssSDS](../../includes/sssds-md.md)]. For more information, see [SQL Azure Connection Type &#40;SSRS&#41;](sql-azure-connection-type-ssrs.md).  
  
 [Return to Data sources table](#DataSourcesTable)  
  
###  <a name="PWD"></a> Microsoft SQL Server Parallel Data Warehouse Processing Extension  
 In [!INCLUDE[ss_dtbi](../../includes/ss-dtbi-md.md)], the graphical query designer associated with this data extension is the [Relational Query Designer User Interface](../relational-query-designer-user-interface.md), not the [Visual Database Tool Designer](../../ssms/visual-db-tools/visual-database-tool-designers.md) that you use with the **Microsoft SQL Server** data source type.  
  
 [!INCLUDE[ss_dtbi](../../includes/ss-dtbi-md.md)] automatically differentiates between **SQL Server Parallel Data Warehouse** and **Microsoft SQL Server** data source types and opens the graphical query designer associated with the data source type.  
  
 If you use the query designer in graphical mode, the query is analyzed and possibly rewritten. A text-based query designer is also available for writing queries. Use the text-based query designer when you want to control the exact [!INCLUDE[tsql](../../includes/tsql-md.md)] syntax that is used for a query. For more information, see [Text-based Query Designer User Interface](../text-based-query-designer-user-interface.md).  
  
 [!INCLUDE[ssDWCurrentFull](../../includes/ssdwcurrentfull-md.md)] does not support the use of stored procedures and table-valued functions in queries. For more information, see [SQL Server Parallel Data Warehouse Connection Type &#40;SSRS&#41;](sql-server-parallel-data-warehouse-connection-type-ssrs.md).  
  
 [Return to Data sources table](#DataSourcesTable)  
  
###  <a name="AnalysisServices"></a> Microsoft SQL Server Analysis Services Data Processing Extension  
 When you select data source type **Microsoft SQL Server Analysis Services**, you are selecting a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension that extends the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] Data Provider for [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)]. This data processing extension is natively compiled for and runs on x86 and x64-based platforms.  
  
 This data provider uses the ADOMD.NET object model to create queries using XML for Analysis (XMLA) version 1.1. Results are returned as a flattened rowset. For more information, see [Analysis Services Connection Type for MDX &#40;SSRS&#41;](analysis-services-connection-type-for-mdx-ssrs.md), [Analysis Services Connection Type for DMX &#40;SSRS&#41;](analysis-services-connection-type-for-dmx-ssrs.md), [Analysis Services MDX Query Designer User Interface](analysis-services-mdx-query-designer-user-interface.md), and [Analysis Services DMX Query Designer User Interface](analysis-services-dmx-query-designer-user-interface.md).  
  
 When connecting to an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data source, the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data processing extension supports multivalue parameters and maps cell and member properties to extended properties supported by [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. For more information, see [Extended Field Properties for an Analysis Services Database &#40;SSRS&#41;](extended-field-properties-for-an-analysis-services-database-ssrs.md).  
  
 You can also create models from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data sources.  
  
###  <a name="OLEDBAll"></a> OLE DB Data Processing Extension  
 The OLE DB data processing extension requires the choice of an additional data provider layer based on the version of the data source you want to use in your report. If you do not select a specific data provider, a default is provided. Choose a specific data provider through the **Connection Properties** dialog box, accessed through the **Edit** button on the [Data Source](../data-source-properties-dialog-box-general.md) or [Shared Data Source](../shared-data-source-properties-dialog-box-general.md) dialog boxes.  
  
 For more information about the OLE DB associated query designer, see [Query and View Designer Tools &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/query-and-view-designer-tools-visual-database-tools.md) and [Graphical Query Designer User Interface](graphical-query-designer-user-interface.md). For more information about specific support for OLE DB providers, see [Visual Studio .NET Designer Tool Supports Specific OLE DB Providers](https://support.microsoft.com/default.aspx/kb/811241) in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Knowledge Base.  
  
 [Return to Data sources table](#DataSourcesTable)  
  
####  <a name="OLEDBSQL"></a> OLE DB for SQL Server  
 When you select data source type **OLE DB**, you are selecting a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension that extends the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] Data Provider for OLE DB. This data processing extension is natively compiled for and runs on x86 and x64 platforms.  
  
 For more information, see [OLE DB Connection Type &#40;SSRS&#41;](ole-db-connection-type-ssrs.md).  
  
 [Return to Data sources table](#DataSourcesTable)  
  
####  <a name="OLEDBAS9"></a> OLE DB for Analysis Services 9.0  
 To connect to [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], select [!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB Provider for [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] 9.0, select the data source type **OLE DB**, and then select the underlying data provider by name. This combination of data processing extension and data provider is natively compiled for and run on x86 and x64 platforms.  
  
> [!NOTE]  
>  This data processing extension has no support for server aggregates, no automatic mapping of extended field properties, and no support for query parameters. The recommended data provider for an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data source is **Microsoft SQL Server Analysis Services**.  
  
 For more information, see [OLE DB Connection Type &#40;SSRS&#41;](ole-db-connection-type-ssrs.md).  
  
 [Return to Data sources table](#DataSourcesTable)  
  
#### OLE DB for OLAP 7.0  
 OLE DB Provider for OLAP Services 7.0 is not supported.  
  
 [Return to Data sources table](#DataSourcesTable)  
  
####  <a name="OracleOLEDB"></a> OLE DB for Oracle  
 The data processing extension OLE DB for Oracle does not support the following Oracle data types: BLOB, CLOB, NCLOB, BFILE, UROWID.  
  
 Unnamed parameters that are position-dependent are supported. Named parameters are not supported by this extension. To use named parameters, use the [Oracle](#OracleClient) data processing extension.  
  
 For more information about configuring Oracle as a data source, see [How to use Reporting Services to configure and to access an Oracle data source](https://support.microsoft.com/kb/834305). For more information about additional permissions configuration, see [How to add permissions for the NETWORK SERVICE security principal](https://support.microsoft.com/kb/870668) in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Knowledge Base.  
  
 [Return to Data sources table](#DataSourcesTable)  
  
####  <a name="OLEDBStandard"></a> OLE DB Standard .NET Framework data provider  
 To retrieve data from a data source that supports OLE DB [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] data providers, use the **OLE DB** data source type and select the default data provider, or select from the installed data providers in the **Connection String** dialog box.  
  
> [!NOTE]  
>  Although a data provider may support previewing a report on your report authoring client, not all OLE DB data providers are designed to support reports published on a report server.  
  
 [Return to Data sources table](#DataSourcesTable)  
  
###  <a name="ODBC"></a> ODBC Data Processing Extension  
 When you select data source type **ODBC**, you are selecting a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension that extends the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] Data Provider for ODBC. This data processing extension is natively compiled for and runs on x86 and [!INCLUDE[vcprx64](../../includes/vcprx64-md.md)] platforms. Use this extension to connect to and retrieve data from any data source that has an ODBC provider.  
  
> [!NOTE]  
>  Although a data provider may support previewing a report on your report authoring client, not all ODBC data providers are designed to support reports published on a report server.  
  
 [Return to Data sources table](#DataSourcesTable)  
  
####  <a name="ODBCGeneric"></a> ODBC Standard .NET Framework data provider  
 To retrieve data from a data source that supports a standard ODBC [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] data provider, use the **ODBC** data source type and select the default data provider, or select from the installed data providers in the **Connection String** dialog box.  
  
> [!NOTE]  
>  Although a data provider may support previewing a report on your report authoring client, not all ODBC data providers are designed to support reports published on a report server.  
  
 [Return to Data sources table](#DataSourcesTable)  
  
###  <a name="OracleClient"></a> Oracle Data Processing Extension  
 When you select data source type **Oracle**, you are selecting a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension that extends the [!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] Data Provider for Oracle. The **Oracle** data source wraps and extends the <xref:System.Data.OracleClient> classes needed by [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. To retrieve report data from an Oracle database, your administrator must install Oracle client tools. This data provider uses the Oracle Call Interface (OCI) from Oracle 8i Release 3 as provided by Oracle Client software. The client application version must be 8.1.7 or later. These tools must be installed on the report authoring client to preview reports and on the report server to view published reports.  
  
 Named parameters are supported by this extension. For Oracle version 9 or later, multivalue parameters are supported. For unnamed parameters that are position-dependent, use the OLE DB data processing extension with the data provider [!INCLUDE[msCoName](../../includes/msconame-md.md)] OLE DB Provider for Oracle. For more information about configuring Oracle as a data source, see [How to use Reporting Services to configure and to access an Oracle data source](https://support.microsoft.com/kb/834305). For more information about additional permissions configuration, see [How to add permissions for the NETWORK SERVICE security principal](https://support.microsoft.com/kb/870668) in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Knowledge Base.  
  
 You can retrieve data from stored procedures with multiple input parameters, but the stored procedure must return only one output cursor. For more information, see the Oracle section in [Retrieving Data Using the DataReader](https://go.microsoft.com/fwlink/?LinkId=81758).  
  
 For more information, see [Oracle Connection Type &#40;SSRS&#41;](oracle-connection-type-ssrs.md). For more information about the associated query designer, see [Query and View Designer Tools &#40;Visual Database Tools&#41;](../../ssms/visual-db-tools/query-and-view-designer-tools-visual-database-tools.md) and [Graphical Query Designer User Interface](graphical-query-designer-user-interface.md).  
  
 You can also create models based on an Oracle database.  
  
 [Return to Data sources table](#DataSourcesTable)  
  
###  <a name="Teradata"></a> Teradata Data Processing Extension  
 When you select data source type **Teradata**, you are selecting a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] data processing extension that extends the .NET Framework Data Provider for Teradata. To retrieve report data from a Teradata database, the system administrator must install the .NET Framework Data Provider for Teradata on the report authoring client to edit and preview reports on the client and on the report server to view published reports.  
  
 For report server projects, there is not a graphical query designer available for this extension. You must use the text-based query designer to create queries.  
  
 The following table shows which versions of the .NET Data Provider for Teradata are supported for defining a data source in a report definition in [!INCLUDE[ss_dtbi](../../includes/ss-dtbi-md.md)]:  
  
|[!INCLUDE[ss_dtbi](../../includes/ss-dtbi-md.md)] version|Teradata database version|.NET Framework Data Provider for Teradata version|  
|-----------------------------------|-------------------------------|-------------------------------------------------------|  
|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]|12.00|12.00|  
|[!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]|6.20|12.00|  
|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]|12.00|12.00.01|  
|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]|6.20|12.00.01|  
|[!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)]|13.00|13.0.0.1|  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]|12.00|12.00.01|  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]|6.20|12.00.01|  
|[!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]|13.00|13.0.0.1|  
|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]|6.20|12.00.01|  
|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]|12.00|12.00.01|  
|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]|13.00|13.0.0.1|  
|[!INCLUDE[ssSQL11](../../includes/sssql11-md.md)]|14.00|14.00.01|  
  
 Multivalue parameters are supported by this extension. Macros can be specified in a query by using the EXECUTE command in query mode TEXT.  
  
 For more information, see [Teradata Connection Type &#40;SSRS&#41;](teradata-connection-type-ssrs.md).  
  
 You can also create models based on a Teradata database. For more information, see the following white paper on the Teradata site: [Microsoft SQL Server 2012 Reporting Services and Teradata Corporation](http://www.teradata.com/white-papers/Microsoft-SQL-Server-2012-Reporting-Services-and-Teradata-Corporation/?type=WP).  
  
 [Return to Data sources table](#DataSourcesTable)  
  
###  <a name="SharePointList"></a> SharePoint List Data Extension  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint List Data Extension so that you can use SharePoint lists as a source of data in a report. You can retrieve list data from the following:  
  
-   [!INCLUDE[SPS2013](../../includes/sps2013-md.md)]  
  
-   [!INCLUDE[SPF2010](../../includes/spf2010-md.md)] and [!INCLUDE[SPS2010](../../includes/sps2010-md.md)]  
  
-   [!INCLUDE[winSPServ](../../includes/winspserv-md.md)] 3.0 and [!INCLUDE[offSPServ](../../includes/offspserv-md.md)] 2007  
  
 There are three implementations of the SharePoint List data provider.  
  
1.  From a report authoring environment such as Report Builder or Report Designer in [!INCLUDE[ss_dtbi](../../includes/ss-dtbi-md.md)], or for a report server that is configured in native mode, list data comes from the Lists.asmx Web service for the SharePoint site.  
  
2.  On a report server that is configured in SharePoint integrated mode, list data comes from either the corresponding Lists.asmx Web service or from programmatic calls to the SharePoint API. In this mode, you can retrieve list data from a SharePoint farm.  
  
3.  For [!INCLUDE[SPS2010](../../includes/sps2010-md.md)] and [!INCLUDE[SPS2013](../../includes/sps2013-md.md)], the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Add-in for [!INCLUDE[msCoName](../../includes/msconame-md.md)] SharePoint Technologies enables you to retrieve list data from a Lists.asmx Web service for a SharePoint site, or from SharePoint site that is part of a SharePoint farm. This scenario is also known as *local mode* because a report server is not required.  
  
 The credentials that you can specify depend on the implementation that the client application uses. For more information, see [SharePoint List Connection Type &#40;SSRS&#41;](sharepoint-list-connection-type-ssrs.md).  
  
###  <a name="XML"></a> XML Data Processing Extension  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes an XML data processing extension so that you can use XML data in a report. The data can be retrieved from an XML document, a Web service, or a Web-based application that can be accessed by way of a URL. For more information, see [XML Connection Type &#40;SSRS&#41;](xml-connection-type-ssrs.md). For more information about the associated query designer, see the text-based query designer section in [Graphical Query Designer User Interface](graphical-query-designer-user-interface.md). For examples, see [Reporting Services: Using XML and Web Service Data Sources](https://go.microsoft.com/fwlink/?LinkId=81654).  
  
 [Return to Data sources table](#DataSourcesTable)  
  
###  <a name="SapBINetWeaver"></a> SAP NetWeaver Business Intelligence Data Processing Extension  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes a data processing extension that allows you to use data from an [!INCLUDE[SAP_DPE_BW_1](../../includes/sap-dpe-bw-1-md.md)] data source in a report.  
  
 For more information, see [SAP NetWeaver BI Connection Type &#40;SSRS&#41;](sap-netweaver-bi-connection-type-ssrs.md). For more information about the associated query designer, see [SAP NetWeaver BI Query Designer User Interface](sap-netweaver-bi-query-designer-user-interface.md).  
  
 For more information about [!INCLUDE[SAP_DPE_BW_1](../../includes/sap-dpe-bw-1-md.md)], see [Using SQL Server 2008 Reporting Services with SAP NetWeaver Business Intelligence](https://go.microsoft.com/fwlink/?LinkId=167352).  
  
 [Return to Data sources table](#DataSourcesTable)  
  
###  <a name="Hyperion"></a> Hyperion Essbase Business Intelligence Data Processing Extension  
 [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes a data processing extension that allows you to use data from a [!INCLUDE[extEssbase](../../includes/extessbase-md.md)] data source in a report.  
  
 For more information, see [Hyperion Essbase Connection Type &#40;SSRS&#41;](hyperion-essbase-connection-type-ssrs.md). For more information about the associated query designer, see [Hyperion Essbase Query Designer User Interface](hyperion-essbase-query-designer-user-interface.md).  
  
 For more information about [!INCLUDE[extEssbase](../../includes/extessbase-md.md)], see [Using SQL Server 2005 Reporting Services with Hyperion Essbase](https://go.microsoft.com/fwlink/?LinkId=81970).  
  
 [Return to Data sources table](#DataSourcesTable)  
  
## See Also  
 [Data Connections, Data Sources, and Connection Strings in Reporting Services](../data-connections-data-sources-and-connection-strings-in-reporting-services.md)   
 [Add Data to a Report &#40;Report Builder and SSRS&#41;](report-datasets-ssrs.md)  
  
  
