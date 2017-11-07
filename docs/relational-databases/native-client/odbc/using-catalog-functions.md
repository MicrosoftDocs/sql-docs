---
title: "Using Catalog Functions | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
helpviewer_keywords: 
  - "ODBC, functions"
  - "SQLLinkedCatalogs function"
  - "SQL Server Native Client ODBC driver, catalog functions"
  - "SQLLinkedServers function"
  - "catalog functions [ODBC]"
  - "functions [ODBC]"
ms.assetid: 7773fb2e-06b5-4c4b-88e9-0ad9132ad273
caps.latest.revision: 36
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Using Catalog Functions
[!INCLUDE[SNAC_Deprecated](../../../includes/snac-deprecated.md)]

  All databases have a structure containing the data stored in the database. A definition of this structure, along with other information such as permissions, is stored in a catalog (implemented as a set of system tables), also known as a data dictionary.  
  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver enables an application to determine the database structure through calls to ODBC catalog functions. Catalog functions return information in result sets and are implemented using catalog stored procedures to query the system tables in the catalog. For example, an application might request a result set containing information about all the tables on the system or all the columns in a particular table. The standard ODBC catalog functions are used to get catalog information from the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to which the application connected.  
  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports distributed queries in which data from multiple, heterogeneous OLE DB data sources is accessed in a single query. One of the methods of accessing a remote OLE DB data source is to define the data source as a linked server. This can be done by using [sp_addlinkedserver](../../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md). After the linked server has been defined, objects in that server can be referenced in Transact-SQL statements by using a four-part name:  
  
 *linked_server_name.catalog.schema.object_name*.  
  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver supports two driver-specific functions that help get catalog information from linked servers:  
  
-   **SQLLinkedServers**  
  
     Returns a list of the linked servers defined to the local server.  
  
-   **SQLLinkedCatalogs**  
  
     Returns a list of the catalogs contained in a linked server.  
  
 After you have a linked server name and a catalog name, the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver supports getting information from the catalog by using a two-part name of *linked_server_name***.***catalog* for *CatalogName* on the following ODBC catalog functions:  
  
-   **SQLColumnPrivileges**  
  
-   **SQLColumns**  
  
-   **SQLPrimaryKeys**  
  
-   **SQLStatistics**  
  
-   **SQLTablePrivileges**  
  
-   **SQLTables**  
  
 The two-part *linked_server_name***.***catalog* is also supported for *FKCatalogName* and *PKCatalogName* on [SQLForeignKeys](../../../relational-databases/native-client-odbc-api/sqlforeignkeys.md).  
  
 Using SQLLinkedServers and SQLLinkedCatalogs requires the following files:  
  
-   sqlncli.h  
  
     Includes function prototypes and constant definitions for the linked server catalog functions. sqlncli.h must be included in the ODBC application and must be in the include path when the application is compiled.  
  
-   sqlncli11.lib  
  
     Must be in the library path of the linker and specified as a file to be linked. sqlncli11.lib is distributed with the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver.  
  
-   sqlncli11.dll  
  
     Must be present at execution time. sqlncli11.dll is distributed with the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver.  
  
## See Also  
 [SQL Server Native Client &#40;ODBC&#41;](../../../relational-databases/native-client/odbc/sql-server-native-client-odbc.md)   
 [SQLColumnPrivileges](../../../relational-databases/native-client-odbc-api/sqlcolumnprivileges.md)   
 [SQLColumns](../../../relational-databases/native-client-odbc-api/sqlcolumns.md)   
 [SQLPrimaryKeys](../../../relational-databases/native-client-odbc-api/sqlprimarykeys.md)   
 [SQLTablePrivileges](../../../relational-databases/native-client-odbc-api/sqltableprivileges.md)   
 [SQLTables](../../../relational-databases/native-client-odbc-api/sqltables.md)   
 [SQLStatistics](../../../relational-databases/native-client-odbc-api/sqlstatistics.md)  
  
  