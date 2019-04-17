---
title: "Using the SQL Server Native Client Header and Library Files | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "header files [SQL Server Native Client]"
  - "SQLNCLI, header files"
  - "OLE DB, header files"
  - "library files [SQL Server Native Client]"
  - "SQL Server Native Client, header files"
  - "data access [SQL Server Native Client], header files"
  - "SQL Server Native Client ODBC driver,header files"
  - "data access [SQL Server Native Client], library files"
  - "SQL Server Native Client, library files"
  - "ODBC applications, header files"
  - "SQLNCLI, library files"
ms.assetid: 69889a98-7740-4667-aecd-adfc0b37f6f0
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Using the SQL Server Native Client Header and Library Files
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../../includes/snac-deprecated.md)]

  The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client header and library files are installed with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. When developing an application, it is important to copy and install all of the required files for development to your development environment. For more information about installing and redistributing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, see [Installing SQL Server Native Client](../../../relational-databases/native-client/applications/installing-sql-server-native-client.md).  
  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client header and library files are installed in the following location:  
  
 *%PROGRAM FILES%*\Microsoft SQL Server\110\SDK  
  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client header file (sqlncli.h) can be used to add [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client data access functionality to your custom applications. The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client header file contains all of the definitions, attributes, properties, and interfaces needed to take advantage of the new features introduced in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)].  
  
 In addition to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client header file, there is also a sqlncli11.lib library file which is the export library for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Bulk Copy Program (BCP) functionality for ODBC.  
  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client header file is backwards compatible with both the sqloledb.h and odbcss.h header files used with Microsoft Data Access Components (MDAC), but does not contain CLSIDs for SQLOLEDB (the OLE DB provider for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] included with MDAC) or symbols for XML functionality (which is not supported by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client).  
  
 ODBC applications cannot reference the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client header (sqlncli.h) and odbcss.h in the same program. Even if you are not using any of the features introduced in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)], the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client header file will work in place of the older odbcss.h.  
  
 OLE DB applications which use the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB provider only need to reference sqlncli.h. If an application uses both MDAC (SQLOLEDB) and the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB provider, it can reference both sqloledb.h and sqlncli.h, but the reference to sqloledb.h must come first.  
  
## Using the SQL Server Native Client Header File  
 To use the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client header file, you must use an **include** statement within your C/C++ programming code. The following sections describe how to do this for both OLE DB and ODBC applications.  
  
> [!NOTE]  
>  The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client header and library files can only be compiled using Visual Studio C++ 2002 or later.  
  
### OLE DB  
 To use the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client header file in an OLE DB application, using the following lines of programming code:  
  
```  
#define _SQLNCLI_OLEDB_  
include "sqlncli.h";  
```  
  
> [!NOTE]  
>  The first line of code shown above should be omitted if both the OLE DB and ODBC APIs are used by the application. In addition, if the application has an **include** statement for sqloledb.h, the **include** statement for sqlncli.h must come after it.  
  
 When creating a connection to a data source through [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, use "SQLNCLI11" as the provider name string.  
  
### ODBC  
 To use the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client header file in an ODBC application, using the following lines of programming code:  
  
```  
#define _SQLNCLI_ODBC_  
include "sqlncli.h";  
```  
  
> [!NOTE]  
>  The first line of code shown above should be omitted if both OLE DB and ODBC APIs are used by the application. In addition, if the application has an `#include` statement for odbcss.h, it should be removed.  
  
 When creating a connection to a data source through [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, use "SQL Server Native Client 11.0" as the driver name string.  
  
## Component Names and Properties by Version  
  
|Property|SQL Server Native Client<br /><br /> SQL Server 2005|SQL Server Native Client 10.0<br /><br /> SQL Server 2008|SQL Server Native Client 11.0<br /><br /> [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)]|MDAC|  
|--------------|--------------------------------------------------|-------------------------------------------------------|---------------------------------------------------------------|----------|  
|ODBC driver name|SQL Native Client|SQL Server Native Client 10.0|SQL Server Native Client 11.0|SQL Server|  
|ODBC header  file name|Sqlncli.h|Sqlncli.h|Sqlncli.h|Odbcss.h|  
|ODBC driver DLL|Sqlncli.dll|Sqlncl10.dll|Sqlncl11.dll|sqlsrv32.dll|  
|ODBC lib file for BCP APIs|Sqlncli.lib|Sqlncli10.lib|Sqlncli11.lib|Odbcbcp.lib|  
|ODBC DLL for BCP APIs|Sqlncli.dll|Sqlncli10.dll|Sqlncli11.dll|Odbcbcp.dll|  
|OLE DB PROGID|SQLNCLI|SQLNCLI10|SQLNCLI11|SQLOLEDB|  
|OLE DB header file name|Sqlncli.h|Sqlncli.h|Sqlncli.h|Sqloledb.h|  
|OLE DB provider DLL|Sqlncli.dll|Sqlncli10.dll|Sqlncli11.dll|Sqloledb.dll|  
  
 sqlncli.h supports multiple version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client through the SQLNCLI_VER macro. By default, SQLNCLI_VER defaults to the latest version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client. To build an application that uses sqlncli10.dll rather than sqlncli11.dll, set SQLNCLI_VER to 10.  
  
## Static Linking and BCP Functions  
 When an application uses BCP functions, it is important for the application to specify in the connection string the driver from the same version that shipped with the header file and library used to compile the application.  
  
 For example, if you compile an application using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client, and the associated library file (sqlncli11.lib) and header file (sqlncli.h) from \Program Files\Microsoft SQL Server\110\SDK, make sure to specify (using ODBC as an example) "DRIVER={SQL Server Native Client 11.0}" in the connection string.  
  
 For more information, see Performing [Performing Bulk Copy Operations](../../../relational-databases/native-client/features/performing-bulk-copy-operations.md).  
  
## See Also  
 [Building Applications with SQL Server Native Client](../../../relational-databases/native-client/applications/building-applications-with-sql-server-native-client.md)  
  
  
