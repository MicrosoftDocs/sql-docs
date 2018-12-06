---
title: "Data Mining Services and Data Sources | Microsoft Docs"
ms.date: 05/01/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: data-mining
ms.topic: conceptual
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Data Mining Services and Data Sources
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Data mining requires a connection to an instance of SQL Server Analysis Services. Data from a cube is not required for data mining and the use of relational sources is recommended; however, data mining uses components provided by the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] engine.  
  
 This topic provides information that you need to know when connecting to an instance of SQL Server [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] to create, process, deploy, or query data mining models.  
  
## Data Mining Services  
 The server component of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] is the msmdsrv.exe application, which ordinarily runs as a Windows service. This application consists of security components, an XML for Analysis (XMLA) listener component, a query processor component and numerous other internal components that perform the following functions:  
  
-   Parsing statements received from clients  
  
-   Managing metadata  
  
-   Handling transactions  
  
-   Processing calculations  
  
-   Storing dimension and cell data  
  
-   Creating aggregations  
  
-   Scheduling queries  
  
-   Caching objects  
  
-   Managing server resources  
  
### XMLA Listener  
 The XMLA listener component handles all XMLA communications between [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] and its clients. The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] **Port** configuration setting in the msmdsrv.ini file can be used to specify a port on which an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance listens. A value of 0 in this file indicates that [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] listen on the default port. Unless otherwise specified, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses the following default TCP ports:  
  
|Port|Description|  
|----------|-----------------|  
|2383|Default instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].|  
|2382|Redirector for other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].|  
|Dynamically assigned at server startup|Named instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)].|  
  
 For more information about controlling the ports used by this service, see [Configure the Windows Firewall to Allow Analysis Services Access](../../analysis-services/instances/configure-the-windows-firewall-to-allow-analysis-services-access.md).  
  
## Connecting to Data Sources  
 Whenever you create or update a data mining structure or model, you use data that is defined by a data source. The data source does not contain the data, which might include Excel workbooks, text files, and SQL Server databases; it only defines the connection information.  A data source view (DSV) serves as an abstraction layer on top of that source, modifying or mapping the data that is obtained from the source.  
  
 It is beyond the scope of this topic to describe the connection requirements for each of these sources. For more information, see the documentation for the provider. However, in general, you should be aware of the following requirements of Analysis Services, when interacting with providers:  
  
-   Because data mining is a service provided by a server, the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] instance must be given access to the data source.  There are two aspects to access: location and identity.  
  
     **Location** means that, if you build a model using data that is stored only on your computer and then deploy the model to a server, the model would fail to process because the data source cannot be found. To resolve this problem, you might need to transfer data into the same SQL Server instance where [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] is running, or move files to a shared location.  
  
     **Identity** means that the services on [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] must be able to open the data file or data source with the appropriate credentials. For example, when you built the model, you might have had unlimited permissions to view the data, but the user who is processing and updating the models on the server might have limited or no access to the data, which can cause failure to process or affect the contents of a model. At minimum, the account used for connecting to the remote data source must have read permissions to the data.  
  
-   When you move a model, the same requirements apply: you must set up appropriate access to the location of the old data source, copy the data sources, or configure a new data source. Also, you must transfer logins and roles, or set up permissions to allow data mining objects to be processed and updated in the new location.  
  
## Configuring Permissions and Server Properties  
 Data mining requires additional permissions on an [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. Most data mining properties can be set by using the [Analysis Server Properties Dialog Box &#40;Analysis Services&#41;](http://msdn.microsoft.com/library/b01ec658-c191-49c9-a6cb-549b21a368ab).  
  
 For more information about the properties that you can configure, see [Server Properties in Analysis Services](../../analysis-services/server-properties/server-properties-in-analysis-services.md).  
  
 The following server properties are of particular relevance to data mining:  
  
-   **AllowAdHocOpenRowsetQueries** Controls ad hoc access to OLE DB providers, which are loaded directly into server memory space.  
  
    > [!IMPORTANT]  
    >  To improve security, we recommend that you set this property to **false**. The default value is **false**. However, even if this property is set to **false**, users can continue to create singleton queries, and can use OPENQUERY on permitted data sources.  
  
-   **AllowedProvidersInOpenRowset** Specifies the provider, if ad hoc access is enabled. You can specify multiple providers, by entering a comma-separated list of ProgIDs.  
  
-   **MaxConcurrentPredictionQueries** Controls the load on the server caused by predictions. The default value of 0 allows unlimited queries for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Enterprise, and a maximum of five concurrent queries for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Standard. Queries above the limit are serialized and may time out.  
  
 The server provides additional properties that control which data mining algorithms are available, including any restrictions on the algorithms, and the defaults for all data mining services. However, there are no settings that allow you to control access to data mining stored procedures specifically. For more information, see [Data Mining Properties](../../analysis-services/server-properties/data-mining-properties.md).  
  
 You can also set properties that let you tune the server and control security for client usage. For more information, see [Feature Properties](../../analysis-services/server-properties/feature-properties.md).  
  
> [!NOTE]  
>  For more information about Support for plug-in algorithms by the editions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [Features Supported by the Editions of SQL Server 2012](http://go.microsoft.com/fwlink/?linkid=232473) (http://go.microsoft.com/fwlink/?linkid=232473).  
  
## Programmatic Access to Data Mining Objects  
 You can use the following object models to create a connection to an Analysis Services database and work with data mining objects:  
  
 **ADO** Uses OLE DB to connect to an Analysis Services server. When you use ADO, the client is limited to schema rowset queries and DMX statements.  
  
 **ADO.NET** Interacts with SQL Server providers better than other providers. Uses data adaptors to store dynamic rowsets. Uses the dataset object, which is a cache of the server data stored as data tables that can be updated or saved as XML.  
  
 **ADOMD.NET** A managed data provider that is optimized for working with data mining and OLAP. ADOMD.NET is faster and more memory-efficient than ADO.NET. ADOMD.NET also lets you retrieve metadata about server objects. Recommended for client applications except when .NET is not available.  
  
 **Server ADOMD** Object model for accessing Analysis Services objects directly on the server. Used by Analysis Services stored procedures; not for client use.  
  
 **AMO** Management interface for Analysis Services that replaces Decision Support Objects (DSO). Operations such as iterating objects require higher permissions when using AMO than when using other interfaces. That is because AMO accesses metadata directly, whereas ADOMD.NET and other interfaces access only the database schemas.  
  
### Browse and Query Access to Servers  
 You can perform all kinds of predictions by using a instance of Analysis Services in OLAP/Data Mining mode, with the following restrictions:  
  
-   If you use server ADOMD, you can use DMX to access the server without making a connection. You can then copy the results directly into a data table. However, you cannot use Server ADOMD with remote instances; you can query only the local server.  
  
-   ADO.NET does not support named parameters for data mining. You must use ADOMD.NET.  
  
-   ADOMD.NET lets you pass an entire table to use as the parameter; therefore, you can use data on the client, or data that is unavailable to the server. You can also use shaped tables as prediction input.  
  
### Using Data Mining Stored Procedures  
 A common use of stored procedures is to encapsulate queries for reuse. The client can use CALL to run stored procedures, including [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] system stored procedures.  
  
 If the procedure returns a dataset, the client will receive a dataset or datatable with a nested table containing the rows. For example, if you create a query against the model content, the query returns the entire model. To avoid bringing back too many rows, you can write stored procedures by using the ADOMD+ object model.  
  
 To write a server stored procedure, you must reference the Microsoft.AnalysisServices.AdomdServer namespace. For more information about how to create and use stored procedures, see [User Defined Functions and Stored Procedures](https://docs.microsoft.com/bi-reference/adomd/multidimensional-models-adomd-net-server/user-defined-functions-and-stored-procedures).  
  
> [!NOTE]  
>  Stored procedures cannot be used to change security on data server objects. When you execute a stored procedure, the user's current context is used to determine access to all server objects. Therefore, users must have appropriate permissions on any database objects that they access.  
  
## See Also  
 [Physical Architecture &#40;Analysis Services - Multidimensional Data&#41;](../../analysis-services/multidimensional-models/olap-physical/understanding-microsoft-olap-physical-architecture.md)   
 [Physical Architecture &#40;Analysis Services - Data Mining&#41;](../../analysis-services/data-mining/physical-architecture-analysis-services-data-mining.md)   
 [Management of Data Mining Solutions and Objects](../../analysis-services/data-mining/management-of-data-mining-solutions-and-objects.md)  
  
  
