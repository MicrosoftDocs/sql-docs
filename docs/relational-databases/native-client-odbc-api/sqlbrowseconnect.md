---
description: "SQLBrowseConnect"
title: "SQLBrowseConnect | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
apitype: "DLLExport"
helpviewer_keywords: 
  - "SQLBrowseConnect function"
ms.assetid: 57faf388-c7ca-4696-9845-34e0a10cc5f7
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SQLBrowseConnect
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  **SQLBrowseConnect** uses keywords that can be categorized into three levels of connection information. For each keyword, the following table indicates whether a list of valid values is returned and whether the keyword is optional.  
  
## Level 1  
  
|Keyword|List returned?|Optional?|Description|  
|-------------|--------------------|---------------|-----------------|  
|DSN|N/A|No|Name of the data source returned by **SQLDataSources**. The DSN keyword cannot be used if the DRIVER keyword is used.|  
|DRIVER|N/A|No|Microsoft® [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver name is {[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client 11}. The DRIVER keyword cannot be used if the DSN keyword is used.|  
  
## Level 2  
  
|Keyword|List returned?|Optional?|Description|  
|-------------|--------------------|---------------|-----------------|  
|SERVER|Yes|No|Name of the server on the network on which the data source resides. The term "(local)" can be entered as the server, in which case a local copy of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can be used, even when this is a non-networked version.|  
|UID|No|Yes|User login ID.|  
|PWD|No|Yes (depends on the user)|User-specified password.|  
|APP|No|Yes|Name of the application calling **SQLBrowseConnect**.|  
|WSID|No|Yes|Workstation ID. Typically, this is the network name of the computer on which the application runs.|  
  
## Level 3  
  
|Keyword|List returned?|Optional?|Description|  
|-------------|--------------------|---------------|-----------------|  
|DATABASE|Yes|Yes|Name of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.|  
|LANGUAGE|Yes|Yes|National language used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
  
 **SQLBrowseConnect** ignores the values of the DATABASE and LANGUAGE keywords stored in the ODBC data source definitions. If the database or language specified in the connection string passed to **SQLBrowseConnect** is invalid, **SQLBrowseConnect** returns SQL_NEED_DATA and the level 3 connection attributes.  
  
 The following attributes, which are set by calling [SQLSetConnectAttr](../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md), determine the result set returned by **SQLBrowseConnect**.  
  
|Attribute|Description|  
|---------------|-----------------|  
|SQL_COPT_SS_BROWSE_CONNECT|If it is set to SQL_MORE_INFO_YES, **SQLBrowseConnect** returns an extended string of server properties.<br /><br /> The following is an example of an extended string returned by **SQLBrowseConnect**:<br /><br /> <br /><br /> `ServerName\InstanceName;Clustered:No;Version:8.00.131`<br /><br /> <br /><br /> In this string, semi-colons separate various pieces of information about the server. Use commas to separate different server instances.|  
|SQL_COPT_SS_BROWSE_SERVER|If a server name is specified, **SQLBrowseConnect** will return information for the server specified. If SQL_COPT_SS_BROWSE_SERVER is set to NULL, **SQLBrowseConnect** returns information for all servers in the domain.<br /><br /> <br /><br /> Note that due to network issues, **SQLBrowseConnect** might not receive a timely response from all servers. Therefore, the list of servers returned can vary for each request.|  
|SQL_COPT_SS_BROWSE_CACHE_DATA|When the SQL_COPT_SS_BROWSE_CACHE_DATA attribute is set to SQL_CACHE_DATA_YES, you can fetch data in chunks when the buffer length is not large enough to hold the result. This length is specified in the BufferLength argument to SQLBrowseConnect.<br /><br /> SQL_NEED_DATA is returned when more data is available. SQL_SUCCESS is returned when there is no more data to retrieve.<br /><br /> The default is SQL_CACHE_DATA_NO.|  
  
## SQLBrowseConnect Support for High Availability, Disaster Recovery  
 For more information on using **SQLBrowseConnect** to connect to an [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] cluster, see [SQL Server Native Client Support for High Availability, Disaster Recovery](../../relational-databases/native-client/features/sql-server-native-client-support-for-high-availability-disaster-recovery.md).  
  
## SQLBrowseConnect Support for Service Principal Names (SPNs)  
 When a connection is opened, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client sets SQL_COPT_SS_MUTUALLY_AUTHENTICATED and SQL_COPT_SS_INTEGRATED_AUTHENTICATION_METHOD to the authentication method used to open the connection.  
  
 For more information about SPNs, see [Service Principal Names &#40;SPNs&#41; in Client Connections &#40;ODBC&#41;](../../relational-databases/native-client/odbc/service-principal-names-spns-in-client-connections-odbc.md).  
  
## Change History  
  
|Updated content|  
|---------------------|  
|Documented SQL_COPT_SS_BROWSE_CACHE_DATA.|  
  
## See Also  
 [SQLBrowseConnect Function](../../odbc/reference/syntax/sqlbrowseconnect-function.md)   
 [ODBC API Implementation Details](../../relational-databases/native-client-odbc-api/odbc-api-implementation-details.md)  
  
