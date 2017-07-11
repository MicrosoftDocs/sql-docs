---
title: "Data providers used for Analysis Services connections | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "analysis-services/multidimensional-tabular"
  - "analysis-services/data-mining"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 128f6dde-409d-4c12-9820-3305bab57b75
caps.latest.revision: 19
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Data providers used for Analysis Services connections
  Analysis Services provides three data providers for server and data access. All applications connecting to Analysis Services do so using one of these providers. Two of the providers, ADOMD.NET and Analysis Services Management Objects (AMO), are managed data providers. The Analysis Services OLE DB provider (MSOLAP DLL) is a native data provider.  
  
 In organizations that run multiple versions of Analysis Services, you might need to install more recent versions of the data providers on user workstations connecting to Analysis Services data. Connections to newer versions of Analysis Services require data providers from the same major release. For example, to connect to [!INCLUDE[ssASCurrent](../../includes/ssascurrent-md.md)], each workstation must have a data provider from the same release. Although Excel installs the data providers it needs to connect, that provider can be out of date relative to the Analysis Services instances you are using.  
  
 This topic contains the following sections:  
  
 [How to determine server version](#bkmk_ServVers)  
  
 [How to determine the version of the Analysis Services data providers](#bkmk_LibUpdate)  
  
 [Where to get newer version data providers](#bkmk_downloadsite)  
  
 [Analysis Services OLE DB Provider](#bkmk_OLE)  
  
 [ADOMD.NET](#bkmk_ADOMD)  
  
 [AMO](#blkmk_AMO)  
  
##  <a name="bkmk_ServVers"></a> How to determine server version  
 Knowing the version of the Analysis Services instance will help you determine whether you need to install newer versions of the data providers on workstations in your organization.  
  
-   In SQL Server Management Studio, connect to the Analysis Services instance. You can view version info in **Server Properties** > **General** > **Version**.  
  
 The major build number of the initial release of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] is 13.0.1601.5.  
  
  
##  <a name="bkmk_LibUpdate"></a> How to determine the version of the Analysis Services data providers  
 Data providers are installed with Analysis Services, as well as by client applications that routinely connect to Analysis Services databases, such as Excel.  
  
 Office 2010 installs data providers from SQL Server 2008. Office 2013 installs data providers from SQL Server 2012, and so on. If you are using multiple versions of Office or SQL Server, and connections or feature availability are not what you expect, you might need to install a newer version of the data providers. You can run multiple major versions of each data provider side by side on the same computer.  
  
#### Find the file version of the OLEDB provider  
  
1.  Go to \Program Files\Microsoft Analysis Services\AS OLEDB\130.  
  
2.  Right-click **msolap130.dll** > **Properties** > **Details**.  
  
 If you cannot find the file at this location, or if the folder path includes AS OLEDB\110 or AS OLEDB\90, you are using an older library and must now install a newer version (AS OLEDB\11) to connect to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
#### Find the file version of ADOMD.NET and AMO  
  
1.  Go to C:\Windows\Assembly  
  
2.  Right-click Microsoft.AnalysisServices.AdomdClient and click **Properties**. Click **Version**.  
  
     For AMO, right-click Microsoft.AnalysisServices.  
  
##  <a name="bkmk_downloadsite"></a> Where to get newer version data providers  
 The version installed on the client computer should match the major version of the server providing the data. If the server installation is newer than the data providers installed on the workstations in your network, you might need to install newer libraries.  
  
#### Find the data providers on the download site  
  
1.  Go to the [Microsoft Download Center](https://www.microsoft.com/download/details.aspx?id=52676).  
  
2.  Click **Download**. ADOMD.NET, the OLE DB provider, and AMO are included in the list as an installable package. For example, SQL_AS_OLEDB.msi. Each library is available in 32-bit or 64-bit versions. Servers and newer workstations running a 64-bit operating system will require the 64-bit version.  
  
##  <a name="bkmk_OLE"></a> Analysis Services OLE DB Provider  
 Analysis Services OLE DB Provider is the native provider for Analysis Services database connections. MSOLAP is used indirectly by both ADOMD.NET and AMO, delegating connection requests to the data provider. You can also call the OLE DB provider directly from application code, which you might do if solution requirements preclude the use of a managed API.  
  
 The Analysis Services OLE DB provider is installed automatically by SQL Server Setup, Excel, and other applications that are frequently used to access Analysis Services databases. You can also install it manually by downloading it from the download center. By default, the provider can be found in the \Program Files\Microsoft Analysis Services folder. The provider must be installed on any workstation used to access Analysis Services data.  
  
 MSOLAP130.dll is the version of the Analysis Services OLE DB provider that ships in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. Other recent previous versions include MSOLAP110.dll (for SQL Server 2008 and 2008 R2) and MSOLAP90.dll (for SQL Server 2005).  
  
 OLE DB providers are often specified in connection strings. An Analysis Services connection string uses a different nomenclature to refer to the OLE DB provider: MSOLAP.\<version>.dll  
  
 MSOLAP.5.dll is the current Analysis Services OLE DB provider installed with Excel 2013. Previous versions, such as MSOLAP.4.dll or MSOLAP.3.dll, are often found on workstations running older versions of Excel. Some Analysis Services features, such as the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] add-in, require specific versions of the OLE DB provider. See [Connection String Properties &#40;Analysis Services&#41;](../../analysis-services/instances/connection-string-properties-analysis-services.md) for more information.  
  
##  <a name="bkmk_ADOMD"></a> ADOMD.NET  
 ADOMD.NET is a managed data provider used for querying Analysis Services data. Excel uses ADOMD.NET when connecting to a specific Analysis Services cube. The connection string you see in Excel is for an ADOMD.NET connection.  
  
 ADOMD.NET is installed by SQL Server Setup and used by SQL Server client applications to connect to Analysis Services. Office installs this library to support data connections from Excel. As with other data providers included in SQL Server, you can redistribute ADOMD.NET if you are using the library in custom code. You can also download and install it manually to get the newer version (see [How to determine the version of the Analysis Services data providers](#bkmk_LibUpdate) in this topic).  
  
 To check file version information, look for ADOMD.NET in the global assembly cache, where it is listed as `Microsoft.AnalysisServices.AdomdClient`.  
  
 When connecting to a database, the connection string properties for all three libraries are largely the same. Almost any connection string you define for ADOMD.NET (<xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.ConnectionString%2A>) will also work for AMO and the Analysis Services OLE DB provider. See [Connection String Properties &#40;Analysis Services&#41;](../../analysis-services/instances/connection-string-properties-analysis-services.md) for more information.  
  
 For more information about connecting programmatically, see [Establishing Connections in ADOMD.NET](../../analysis-services/multidimensional-models-adomd-net-client/connections-in-adomd-net.md).  
  
##  <a name="blkmk_AMO"></a> AMO  
 AMO is a managed data provider used for server administration and data definition. For example, SQL Server Management Studio uses AMO to connect to Analysis Services.  
  
 AMO is installed by SQL Server Setup and used by SQL Server client applications to connect to Analysis Services. You can also download and install it manually when using AMO in custom code (see [How to determine the version of the Analysis Services data providers](#bkmk_LibUpdate) in this topic). AMO can be found in the global assembly cache, as `Microsoft.AnalysisServices`.  
  
 A connection using AMO is typically minimal, consisting of “data source=\<servername>”. After a connection is established, you use the API to work with database collections and major objects. Both SSDT and SSMS use AMO to connect to an Analysis Services instance.  
  
 For more information about connecting programmatically, see [Programming AMO Fundamental Objects](../../analysis-services/multidimensional-models/analysis-management-objects/programming-amo-fundamental-objects.md).  
  
## See Also  
 [Connect to Analysis Services](../../analysis-services/instances/connect-to-analysis-services.md)  
  
  