---
title: "Connection String Properties (Analysis Services) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
ms.assetid: 29a00a41-5b0d-44b2-8a86-1b16fe507768
author: minewiskan
ms.author: owend
manager: craigg
---
# Connection String Properties (Analysis Services)
  This topic documents the connection string properties you might set in one of the designer or administration tools, or see in connection strings built by client applications that connect to and query Analysis Services data. As such, it covers just a subset of the available properties. The complete list includes numerous server and database properties, allowing you to customize a connection for a specific application, independent of how the instance or database is configured on the server.  
  
 Developers who build custom connection strings in application code should review the API documentation for ADOMD.NET client to view a more detailed list: <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.ConnectionString%2A>  
  
 The properties described in this topic are used by the Analysis Services client libraries, ADOMD.NET, AMO, and the OLE DB provider for Analysis Services. The majority of connection string properties can be used with all three client libraries. Exceptions are called out in the description.  
  
 This topic includes the following sections:  
  
 [Connection parameters in common use](#bkmk_common)  
  
 [Authentication and Security](#bkmk_auth)  
  
 [Special-purpose parameters](#bkmk_special)  
  
 [Reserved for future use](#bkmk_reserved)  
  
 [Example connection strings](#bkmk_examples)  
  
 [Connection string formats used in Analysis Services](#bkmk_supportedstrings)  
  
 [Encrypting Connection Strings](#bkmk_encrypt)  
  
> [!NOTE]  
>  When setting properties, if you inadvertently set the same property twice, the last one in the connection string is used.  
  
 For more information about how to specify an Analysis Services connection in existing Microsoft applications, see [Connect from client applications &#40;Analysis Services&#41;](connect-from-client-applications-analysis-services.md).  
  
##  <a name="bkmk_common"></a> Connection parameters in common use  
 The following table describes those properties most often used when building a connection string.  
  
|Property|Description|Example|  
|--------------|-----------------|-------------|  
|`Data Source` or `DataSource`|Specifies the server instance. This property is required for all connections. Valid values include the network name or IP address of the server, local or localhost for local connections, a URL if the server is configured for HTTP or HTTPS access, or the name of a local cube (.cub) file.|`Data source=AW-SRV01` for the default instance and port (TCP 2383).<br /><br /> `Data source=AW-SRV01$Finance:8081` for a named instance ($Finance) and fixed port.<br /><br /> `Data source=AW-SRV01.corp.Adventure-Works.com` for a fully qualified domain name, assuming the default instance and port.<br /><br /> `Data source=172.16.254.1` for an IP address of the server, bypassing DNS server lookup, useful for troubleshooting connection problems.|  
|`Initial Catalog` or `Catalog`|Specifies the name of the Analysis Services database to connect to. The database must be deployed on Analysis Services, and you must have permission to connect to it. This property is optional for AMO connections, but required for ADOMD.NET.|`Initial catalog=AdventureWorks2012`|  
|`Provider`|Valid values include MSOLAP or MSOLAP.\<version>, where \<version> is either 3, 4, or 5. On the file system, the data provider name is msolap110.dll for SQL Server 2012 version, msolap100.dll for SQL Server 2008 and 2008 R2, and msolap90.dll for SQL Server 2005.<br /><br /> The current version is MSOLAP.5. This property is optional. By default, the client libraries read the current version of the OLE DB provider from the registry. You only need to set this property if you require a specific version of the data provider, for example to connect to a SQL Server 2008 instance.<br /><br /> Data providers correspond to versions of SQL Server. If your organization uses current and previous versions of Analysis Services, you will most likely need to specify which provider to use on connection strings that you create by hand. You might also need to download and install specific versions of the data provider on computers that do not have the version you need. You can download the OLE DB provider from SQL Server Feature Pack pages on the download center. Go to [Microsoft SQL Server 2012 Feature Pack](https://go.microsoft.com/fwlink/?LinkId=296473) to download the Analysis Services OLE DB provider for SQL Server 2012.<br /><br /> MSOLAP.4 was released in both SQL Server 2008 and SQL Server 2008 R2. The 2008 R2 version supports PowerPivot workbooks and sometimes needs to be installed manually on SharePoint servers. To distinguish between these versions, you must check the build number in the file properties of the provider: Go to Program files\Microsoft Analysis Services\AS OLEDB\10. Right-click msolap110.dll and select **Properties**. Click **Details**. View the file version information. The version should include 10.50.\<buildnumber> for SQL Server 2008 R2. For more information, see [Install the Analysis Services OLE DB Provider on SharePoint Servers](../../sql-server/install/install-the-analysis-services-ole-db-provider-on-sharepoint-servers.md) and [Data providers used for Analysis Services connections](data-providers-used-for-analysis-services-connections.md).<br /><br /> MSOLAP.3 was released in SQL Server 2005.<br /><br /> MSOLAP.4 was released in SQL Server 2008 and again SQL Server 2008 R2<br /><br /> MSOLAP.5 was released in SQL Server 2012|`Provider=MSOLAP.3` is used for connections that require the SQL Server 2005 version of the OLE DB provider for Analysis Services.|  
|`Cube`|Cube name or perspective name. A database can contain multiple cubes and perspectives. When multiple targets are possible, include the cube or perspective name on the connection string.|`Cube=SalesPerspective` shows that you can use the Cube connection string property to specify either the name of a cube or the name of a perspective.|  
  
##  <a name="bkmk_auth"></a> Authentication and Security  
 This section includes connection string properties related to authentication and encryption. Analysis Services uses Windows Authentication only, but you can set properties on the connection string to pass in a specific user name and password.  
  
 Properties are listed in alphabetical order.  
  
|Property|Description|  
|--------------|-----------------|  
|`EffectiveUserName`|Use when an end user identity must be impersonated on the server. Specify the account in a domain\user format. To use this property, the caller must have administrative permissions in Analysis Services. For more information about using this property in an Excel workbook from SharePoint, see [Use Analysis Services EffectiveUserName in SharePoint Server 2013](https://go.microsoft.com/fwlink/?LinkId=311905). For an illustration of how this property is used with Reporting Services, see [Using EffectiveUserName To Impersonate in SSAS](http://www.artisconsulting.com/blogs/greggalloway/2010/4/1/using-effectiveusername-to-impersonate-in-ssas).<br /><br /> `EffectiveUserName` is used in a PowerPivot for SharePoint installation to capture usage information. The user identity is provided to the server so that events or errors that include user identity can be recorded in the log files. In the case of PowerPivot, it is not used for authorization purposes.|  
|**Encrypt Password**|Specifies whether a local password is to be used to encrypt local cubes. Valid values are True or False. The default is False.|  
|`Encryption Password`|The password used to decrypt an encrypted local cube. Default value is empty. This value must be explicitly set by the user.|  
|`Impersonation Level`|Indicates the level of impersonation that the server is allowed to use when impersonating the client. Valid values include:<br /><br /> **Anonymous**: The client is anonymous to the server. The server process cannot obtain information about the client, nor can the client be impersonated.<br /><br /> **Identify**: The server process can get the client identity. The server can impersonate the client identity for authorization purposes but cannot access system objects as the client.<br /><br /> **Impersonate**: This is the default value. The client identity can be impersonated, but only when the connection is established, and not on every call.<br /><br /> **Delegate**: The server process can impersonate the client security context while acting on behalf of the client. The server process can also make outgoing calls to other servers while acting on behalf of the client.|  
|`Integrated Security`|The Windows identity of the caller is used to connect to Analysis Services. Valid values are blank, SSPI, and BASIC.<br /><br /> `Integrated Security`=`SSPI` is the default value for TCP connections, allowing NTLM, Kerberos, or Anonymous authentication. Blank is the default value for HTTP connections.<br /><br /> When using `SSPI`, `ProtectionLevel` must be set to one of the following: `Connect`, `PktIntegrity`, `PktPrivacy`.|  
|`Persist Encrypted`|Set this property when the client application requires the data source object to persist sensitive authentication information, such as a password, in encrypted form. By default, authentication information is not persisted.|  
|`Persist Security Info`|Valid values are True and False. When set to True, security information, such as the user identity or password previously specified on the connection string, can be obtained from the connection after the connection is made. The default value is False.|  
|`ProtectionLevel`|Determines the security level used on the connection. Valid values are:<br /><br /> `None`. Unauthenticated or anonymous connections. Performs no authentication on data sent to the server.<br /><br /> `Connect`. Authenticated connections. Authenticates only when the client establishes a relationship with a server.<br /><br /> `PktIntegrity`. Encrypted connections. Verifies that all data is received from the client and that it has not been changed in transit.<br /><br /> `PktPrivacy`. Signed encryption, supported only for XMLA. Verifies that all data is received from the client, that it has not been changed in transit, and protects the privacy of the data by encrypting it.<br /><br /> <br /><br /> For more information, see [Establishing Secure Connections in ADOMD.NET](https://docs.microsoft.com/bi-reference/adomd/multidimensional-models-adomd-net-client/connections-in-adomd-net-establishing-secure-connections)|  
|`Roles`|Specify a comma-delimited list of predefined roles to connect to a server or database using permissions conveyed by that role. If this property is omitted, all roles are used, and the effective permissions are the combination of all roles. Setting the property to an empty value (for example, Roles=' ') the client connection has no role membership.<br /><br /> An administrator using this property connects using the permissions conveyed by the role. Some commands might fail if the role does not provide sufficient permission.|  
|`SSPI`|Explicitly specifies which security package to use for client authentication when `Integrated Security` is set to `SSPI`. SSPI supports multiple packages, but you can use this property to specify a particular package. Valid values are Negotiate, Kerberos, NTLM, and Anonymous User. If this property is not set, all packages will be available to the connection.|  
|`Use Encryption for Data`|Encrypts data transmissions. Value values are True and False.|  
|`User ID`=...; `Password`=|`User ID` and `Password` are used together. Analysis Services impersonates the user identity specified through these credentials. On an Analysis Services connection, putting credentials on the command line is used only when the server is configured for HTTP access, and you specified Basic authentication instead of integrated security on the IIS virtual directory.<br /><br /> The user name and password must be the credentials of a Windows identity, either a local or a domain user account. Notice that `User ID` has an embedded space. Other aliases for this property include `UserName` (no space), and `UID`. Alias for `Password` is `PWD`.|  
  
##  <a name="bkmk_special"></a> Special-purpose parameters  
 This section describes the remainder of the connection string parameters. These are used to ensure specific connection behaviors required by an application.  
  
 Properties are listed in alphabetical order.  
  
|Property|Description|  
|--------------|-----------------|  
|`Application Name`|Sets the name of the application associated with the connection. This value can be useful when monitoring tracing events, especially when you have several applications accessing the same databases. For example, adding Application Name='test' to a connection string causes 'test' to appear in a SQL Server Profiler trace, as shown in the following screenshot:<br /><br /> ![SSAS_AppNameExcample](../media/ssas-appnameexcample.gif "SSAS_AppNameExcample")<br /><br /> Aliases for this property include `sspropinitAppName`, `AppName`. For more information, see [Use Application Name parameter when connecting to SQL Server](https://go.microsoft.com/fwlink/?LinkId=301699).|  
|`AutoSyncPeriod`|Sets the frequency (in milliseconds) of client and server cache synchronization. ADOMD.NET provides client caching for frequently used objects that have minimal memory overhead. This helps reduce the number of round trips to the server. The default is 10000 milliseconds (or 10 seconds). When set to null or 0, automatic synchronization is turned off.|  
|`Character Encoding`|Defines how characters are encoded on the request. Valid values are Default or UTF-8 (these are equivalent), and UTF-16|  
|`CompareCaseSensitiveStringFlags`|Adjusts case-sensitive string comparisons for a specified locale. For more information about setting this property, see [CompareCaseSensitiveStringFlags Property](https://msdn.microsoft.com/library/aa237459\(v=sql.80\).aspx).|  
|`Compression Level`|If `TransportCompression` is XPRESS, you can set the compression level to control how much compression is used. Valid values are 0 through 9, with 0 having least compression, and 9 having the most compression. Increased compression slows performance. The default value is 0.|  
|`Connect Timeout`|Determines the maximum amount of time (in seconds) the client attempts a connection before timing out. If a connection does not succeed within this period, the client quits trying to connect and generates an error.|  
|`MDX Compatibility`|The purpose of this property is to ensure a consistent set of MDX behaviors for applications that issue MDX queries. Excel, which uses MDX queries to populate and calculate a PivotTable connected to Analysis Services, sets this property to 1, to ensure that placeholder members in ragged hierarchies are visible in a PivotTable. Valid values include 0, 1, 2.<br /><br /> 0 and 1 expose placeholder members; 2 does not. If this is empty, 0 is assumed.|  
|`MDX Missing Member Mode=Error`|Indicates whether missing members are ignored in MDX statements. Valid values are Default, Error, and Ignore. Default uses a server-defined value. Error generates an error when a member does not exist. Ignore specifies that missing values should be ignored.|  
|`Optimize Response`|A bitmask indicating which of the following query response optimizations are enabled.<br /><br /> 0x01: Default. Use the NormalTupleSet <br />0x02: Use when slicers are empty|  
|`Packet Size`|A network packet size (in bytes) between 512 and 32,767. The default network packet size is 4096.|  
|`Protocol Format`|Sets the format of the XML sent to the server. Valid values are Default, XML, or Binary. The protocol is XMLA. You can specify that the XML be sent in compressed form (this is the default), as raw XML, or in a binary format. Binary format encodes XML elements and attributes, making them smaller. Compression is a proprietary format that further reduces the size of requests and responses. Compression and binary formats are used to speed up data transfer requests and responses.<br /><br /> You must use a client library on the connection if using binary or compressed format. OLE DB provider can format requests and responses in binary or compressed format. AMO and ADOMD.NET format the requests as Text, but accept responses in binary or compressed format.<br /><br /> This connection string property is equivalent to the `EnableBinaryXML` and `EnableCompression` server configuration settings.|  
|`Real Time Olap`|Set this property to bypass caching, causing all partitions to actively listen for query notifications. By default, this property is not set.|  
|`Safety Options`|Sets the safety level for user-defined functions and actions. Valid values are 0, 1, 2. In an Excel connection this property is Safety Options=2. Details about this option can be found in <xref:Microsoft.AnalysisServices.AdomdClient.AdomdConnection.ConnectionString%2A>.|  
|`SQLQueryMode`|Specifies whether SQL queries include calculations. Valid values are Data, Calculated, IncludeEmpty. Data means that no calculations are allowed. Calculated allows calculations. IncludeEmpty allows calculations and empty rows to be returned in the query result.|  
|`Timeout`|Specifies how long (in milliseconds) the client library waits for a command to complete before generating an error.|  
|`Transport Compression`|Defines how client and server communications are compressed, when compression is specified via the `Protocol Format` property. Valid values are Default, None, Compressed and `gzip`. Default is no compression for TCP, or `gzip` for HTTP. None indicates that no compression is used. Compressed uses XPRESS compression (SQL Server 2008 and later). `gzip` is only valid for HTTP connections, where the HTTP request includes Accept-Encoding=gzip.|  
|`UseExistingFile`|Used when connecting to a local cube. This property specifies whether the local cube is overwritten. Valid values are True or False. If set to True, the cube file must exist. The existing file will be the target of the connection. If set to False, the cube file is overwritten.|  
|`VisualMode`|Set this property to control how members are aggregated when dimension security is applied.<br /><br /> For cube data that everyone is allowed to see, aggregating all of the members makes sense because all of the values that contribute to the total are visible. However, if you filter or restrict dimensions based on user identity, showing a total based on all the members (combining both restricted and allowed values into a single total) might be confusing or show more information than should be revealed.<br /><br /> To specify how members are aggregated when dimension security is applied, you can set this property to True to use only allowed values in the aggregation, or False to exclude restricted values from the total.<br /><br /> When set on the connection string, this value applies to the cube or perspective level. Within a model, you can control visual totals at a more granular level.<br /><br /> Valid values are 0, 1, and 2.<br /><br /> 0 is the default. Currently, the default behavior is equivalent to 2, where aggregations include values that are hidden from the user.<br /><br /> 1 excludes hidden values from the total. This is the default for Excel.<br /><br /> 2 includes hidden values in the total. This is the default value on the server.<br /><br /> <br /><br /> Aliases for this property include `Visual Total` or `Default MDX Visual Mode`.|  
  
##  <a name="bkmk_reserved"></a> Reserved for future use  
 The following properties are allowed on a connection string, but are not operational in current releases of Analysis Services.  
  
-   Authenticated User  
  
-   Cache Authentication  
  
-   Cache Mode (Use of this property was investigated in earlier releases. Although you might find blog posts recommending its usage, you should avoid setting this property unless instructed by Microsoft Support).  
  
-   Cache Policy  
  
-   Cache Ratio  
  
-   Cache Ratio2  
  
-   Dynamic Debug Limit  
  
-   Debug Mode  
  
-   Mode  
  
-   SQLCompatibility  
  
-   Use Formula Cache  
  
##  <a name="bkmk_examples"></a> Example connection strings  
 This section shows the connection string that you'll most likely use when setting up an Analysis Services connection in commonly used applications.  
  
 **Generic connection string**  
  
 You might use a connection string like this one if you are configuring a connection from Reporting Services.  
  
 `Data source=<servername>; initial catalog=<databasename>`  
  
 **Connection string in Excel**  
  
 The default ADOMD.NET connection string in Excel specifies the data provider, server, database name, Windows integrated security. The MDX Compatibility level is always set to 1. Although you can change the value for the current session, Excel will reset MDX Compatibility to1 when the file is next opened.  
  
 `Provider=MSOLAP.5;Integrated Security=SSPI;Persist Security Info=True;Initial Catalog=Adventure Works DW 2008R2;Data Source=AW-SRV01;MDX Compatibility=1;Safety Options=2;MDX Missing Member Mode=Error`  
  
 For more information, see [Data Connections, Data Sources, and Connection Strings in Reporting Services](../../reporting-services/data-connections-data-sources-and-connection-strings-in-reporting-services.md) and [Data Authentication for Excel Services in SharePoint Server 2013](https://go.microsoft.com/fwlink/?LinkId=296350).  
  
##  <a name="bkmk_supportedstrings"></a> Connection string formats used in Analysis Services  
 This section lists all of the connection string formats supported by Analysis Services. With the exception of connections to PowerPivot databases, you can specify these connections strings in applications that connect to Analysis Services.  
  
 **Native (or direct) connections to the server**  
  
 `Data Source=server[:port][\instance]` where "port" and "\instance" are optional. For example, specifying "Data Source=server1" opens a connection to the default instance (and default port 2383) on a server named "server1".  
  
 "Data Source=server1:port1" will open a connection to an Analysis Services instance running on port "port1" on "server1".  
  
 "Data Source=server1\instance1" will open a connection to SQL Browser (on its default port 2382), resolve the port for the named instance  "instance1", then open the connection to that Analysis Services port.  
  
 "Data Source=server1:port1\instance1" will open a connection to SQL Browser on "port1", resolve the port for the "instance1" named instance, then open the connection to that Analysis Services port.  
  
 **Local cube connections (.cub files)**  
  
 `Data Source=<path>`, for example "Data Source=c:\temp\a.cub"  
  
 **Http(s) connections to msmdpump.dll**  
  
 `Data Source=<URL>`, where the URL is the HTTP or HTTPS address to the virtual IIS folder that contains the msmdpump.dll. For more information, see [Configure HTTP Access to Analysis Services on Internet Information Services &#40;IIS&#41; 8.0](configure-http-access-to-analysis-services-on-iis-8-0.md).  
  
 **Http(s) connections to PowerPivot workbooks (.xlsx, .xlsb or .xlsm files)**  
  
 `Data Source=<URL>`, where the URL is the SharePoint path to a PowerPivot workbook that has been published to a SharePoint library. For example, "Data Source=<http://localhost/Shared> Documents/Sales.xlsx".  
  
 **Http(s) connections to BI Semantic Model Connection files**  
  
 `Data Source=<URL>` where the URL is the SharePoint path to the .bism file. For example, "Data Source=<http://localhost/Shared> Documents/Sales.bism".  
  
 **Embedded PowerPivot connections**  
  
 `Data Source=$Embedded$` where $embedded$ is a moniker that refers to an embedded PowerPivot data model inside the workbook. This connection string is created and managed internally. Do not modify it. Embedded connection strings are resolved by the PowerPivot for Excel add-in on client workstations, or by PowerPivot for SharePoint instances in a SharePoint farm.  
  
 **Local server context in Analysis Services stored procedures**  
  
 `Data Source=*`, where * resolves to the local instance.  
  
##  <a name="bkmk_encrypt"></a> Encrypting Connection Strings  
 [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] encrypts and stores the connection strings it uses to connect to each of its data sources. If the connection to a data source requires a user name and password, you can have [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] store the name and password with the connection string, or prompt you for the name and password each time a connection to the data source is required. Having [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] prompt you for user information means that this information does not have to be stored and encrypted. However, if you store this information in the connection string, this information does need to be encrypted and secured.  
  
 To encrypt and secure the connection string information, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses the Data Protection API. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] uses a separate encryption key to encrypt connection string information for each [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] database. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] creates this key when you create a database, and encrypts connection string information based on the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] startup account. When [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] starts, the encrypted key for each database is read, decrypted, and stored. [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] then uses the appropriate decrypted key to decrypt the data source connection string information when [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] needs to connect to a data source.  
  
## See Also  
 [Configure HTTP Access to Analysis Services on Internet Information Services &#40;IIS&#41; 8.0](configure-http-access-to-analysis-services-on-iis-8-0.md)   
 [Configure Analysis Services for Kerberos constrained delegation](configure-analysis-services-for-kerberos-constrained-delegation.md)   
 [Data providers used for Analysis Services connections](data-providers-used-for-analysis-services-connections.md)   
 [Connect to Analysis Services](connect-to-analysis-services.md)  
  
  
