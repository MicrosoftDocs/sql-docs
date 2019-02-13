---
title: "Using the OLE DB driver for SQL Server header and library Files | Microsoft Docs"
description: "Using the OLE DB Driver for SQL Server header and library files"
ms.custom: ""
ms.date: "02/12/2019"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: connectivity
ms.topic: "reference"
helpviewer_keywords: 
  - "header files [OLE DB driver for SQL Server]"
  - "MSOLEDBSQL, header files"
  - "OLE DB, header files"
  - "library files [OLE DB driver for SQL Server]"
  - "OLE DB Driver for SQL Server, header files"
  - "data access [OLE DB driver for SQL Server], header files"
  - "data access [OLE DB driver for SQL Server], library files"
  - "OLE DB Driver for SQL Server, library files"
  - "MSOLEDBSQL, library files"
author: pmasl
ms.author: pelopes
manager: craigg
---
# Using the OLE DB driver for SQL Server header and library files
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

  The OLE DB Driver for SQL Server header and library files are installed when the OLE DB Driver for SQL Server SDK option is selected during installation process. When developing an application, it is important to copy and install all of the required files for development to your development environment. For more information about installing and redistributing OLE DB Driver for SQL Server, see [Installing OLE DB Driver for SQL Server](../../oledb/applications/installing-oledb-driver-for-sql-server.md).  
  
 The OLE DB Driver for SQL Server header and library files are installed in the following location:  
  
 *%PROGRAM FILES%*\Microsoft SQL Server\Client SDK\OLEDB\182\SDK  
  
 The OLE DB Driver for SQL Server header file (msoledbsql.h) can be used to add OLE DB Driver for SQL Server data access functionality to your custom applications. The OLE DB Driver for SQL Server header file contains all of the definitions, attributes, properties, and interfaces needed to take advantage of the new features introduced in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)].  
  
 In addition to the OLE DB Driver for SQL Server header file, there is also a msoledbsql.lib library file, which is the export library for [OpenSqlFilestream](../../../relational-databases/blob/access-filestream-data-with-opensqlfilestream.md) functionality.  
  
 The OLE DB Driver for SQL Server header file is backwards compatible with the sqloledb.h header file used with Microsoft Data Access Components (MDAC), but does not contain CLSIDs for SQLOLEDB (the OLE DB provider for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] included with MDAC) or symbols for XML functionality (which is not supported by OLE DB Driver for SQL Server).    
  
 OLE DB applications that use the OLE DB Driver for SQL Server only need to reference msoledbsql.h. If an application uses both MDAC (SQLOLEDB) and the OLE DB Driver for SQL Server, it can reference both sqloledb.h and msoledbsql.h, but the reference to sqloledb.h must come first.  
  
## Using the OLE DB Driver for SQL Server header file  
 To use the OLE DB Driver for SQL Server header file, you must use an **include** statement within your C/C++ programming code. The following sections describe how to do it in OLE DB applications.  
  
> [!NOTE]  
>  The OLE DB Driver for SQL Server header and library files can only be compiled using Visual Studio C++ 2012 or later.  
  
### OLE DB  
 To use the OLE DB Driver for SQL Server header file in an OLE DB application, using the following lines of programming code:  
  
```    
include "msoledbsql.h";  
```  
  
> [!NOTE]  
>  If the application has an **include** statement for sqloledb.h, the **include** statement for msoledbsql.h must come after it.  
  
 When creating a connection to a data source through OLE DB Driver for SQL Server, use "MSOLEDBSQL" as the provider name string.  

  
## Component names and properties by version  

|Property|OLE DB driver for SQL Server|MDAC|  
|--------|----------------------------|----|   
|OLE DB PROGID|MSOLEDBSQL|SQLOLEDB|  
|OLE DB header file name|msoledbsql.h|Sqloledb.h|  
|OLE DB provider DLL|msoledbsql.dll|Sqloledb.dll| 
  
  
## Static Linking and BCP Functions  
 When an application uses BCP functions, it is important for the application to specify in the connection string the driver from the same version that shipped with the header file and library used to compile the application.  
  
 For more information, see Performing [Performing Bulk Copy Operations](../../oledb/features/performing-bulk-copy-operations.md).  
  
## See Also  
 [Building Applications with OLE DB Driver for SQL Server](../../oledb/applications/building-applications-with-oledb-driver-for-sql-server.md)  
  
  
