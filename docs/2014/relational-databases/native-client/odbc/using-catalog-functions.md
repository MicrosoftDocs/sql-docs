---
title: "Using Catalog Functions | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "ODBC, functions"
  - "SQLLinkedCatalogs function"
  - "SQL Server Native Client ODBC driver, catalog functions"
  - "SQLLinkedServers function"
  - "catalog functions [ODBC]"
  - "functions [ODBC]"
ms.assetid: 7773fb2e-06b5-4c4b-88e9-0ad9132ad273
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using Catalog Functions
  All databases have a structure containing the data stored in the database. A definition of this structure, along with other information such as permissions, is stored in a catalog (implemented as a set of system tables), also known as a data dictionary.  
  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver enables an application to determine the database structure through calls to ODBC catalog functions. Catalog functions return information in result sets and are implemented using catalog stored procedures to query the system tables in the catalog. For example, an application might request a result set containing information about all the tables on the system or all the columns in a particular table. The standard ODBC catalog functions are used to get catalog information from the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to which the application connected.  
  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports distributed queries in which data from multiple, heterogeneous OLE DB data sources is accessed in a single query. One of the methods of accessing a remote OLE DB data source is to define the data source as a linked server. This can be done by using [sp_addlinkedserver](/sql/relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql). After the linked server has been defined, objects in that server can be referenced in Transact-SQL statements by using a four-part name:  
  
 *linked_server_name.catalog.schema.object_name*.  
  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver supports two driver-specific functions that help get catalog information from linked servers:  
  
-   **SQLLinkedServers**  
  
     Returns a list of the linked servers defined to the local server.  
  
-   **SQLLinkedCatalogs**  
  
     Returns a list of the catalogs contained in a linked server.  
  
 After you have a linked server name and a catalog name, the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver supports getting information from the catalog by using a two-part name of _linked_server_name_**.**_catalog_ for *CatalogName* on the following ODBC catalog functions:  
  
-   **SQLColumnPrivileges**  
  
-   **SQLColumns**  
  
-   **SQLPrimaryKeys**  
  
-   **SQLStatistics**  
  
-   **SQLTablePrivileges**  
  
-   **SQLTables**  
  
 The two-part _linked_server_name_**.**_catalog_ is also supported for *FKCatalogName* and *PKCatalogName* on [SQLForeignKeys](../../native-client-odbc-api/sqlforeignkeys.md).  
  
 Using SQLLinkedServers and SQLLinkedCatalogs requires the following files:  
  
-   sqlncli.h  
  
     Includes function prototypes and constant definitions for the linked server catalog functions. sqlncli.h must be included in the ODBC application and must be in the include path when the application is compiled.  
  
-   sqlncli11.lib  
  
     Must be in the library path of the linker and specified as a file to be linked. sqlncli11.lib is distributed with the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver.  
  
-   sqlncli11.dll  
  
     Must be present at execution time. sqlncli11.dll is distributed with the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver.  
  
## See Also  
 [SQL Server Native Client &#40;ODBC&#41;](sql-server-native-client-odbc.md)   
 [SQLColumnPrivileges](../../native-client-odbc-api/sqlcolumnprivileges.md)   
 [SQLColumns](../../native-client-odbc-api/sqlcolumns.md)   
 [SQLPrimaryKeys](../../native-client-odbc-api/sqlprimarykeys.md)   
 [SQLTablePrivileges](../../native-client-odbc-api/sqltableprivileges.md)   
 [SQLTables](../../native-client-odbc-api/sqltables.md)   
 [SQLStatistics](../../statistics/statistics.md)  
  
  
