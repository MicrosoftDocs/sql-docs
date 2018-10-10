---
title: "Performing Bulk Copy Operations (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client ODBC driver, bulk copy"
  - "bulk copy [ODBC]"
  - "ODBC, bulk copy operations"
  - "minimally logged operations [SQL Server Native Client]"
  - "bulk copy [ODBC], about bulk copy"
ms.assetid: 5c793405-487c-4f52-88b8-0091d529afb3
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Performing Bulk Copy Operations (ODBC)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  The ODBC standard does not directly support [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] bulk copy operations. When connected to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] version 7.0 or later, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver supports the DB-Library functions that perform [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] bulk copy operations. This driver-specific extension provides an easy upgrade path for existing DB-Library applications that use bulk copy functions. The specialized bulk copy support is in the following files:  
  
-   sqlncli.h  
  
     Includes function prototypes and constant definitions for bulk copy functions. sqlncli.h must be included in the ODBC application performing bulk copy operations and must be in the application's include path when it is compiled.  
  
-   sqlncli11.lib  
  
     Must be in the library path of the linker and specified as a file to be linked. sqlncli11.lib is distributed with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver.  
  
-   sqlncli11.dll  
  
     Must be present at execution time. sqlncli11.dll is distributed with the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver.  
  
> [!NOTE]  
>  The ODBC **SQLBulkOperations** function has no relationship to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] bulk copy functions. Applications must use the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-specific bulk-copy functions to perform bulk copy operations.  
  
## Minimally Logging Bulk Copies  
 With the Full Recovery model, all row-insert operations performed by bulk load are fully logged in the transaction log. For large data loads, this can cause the transaction log to fill rapidly. Under certain conditions, minimally logging is possible. Minimal logging reduces the possibility of a bulk load operation filling the log space and is also more efficient than full logging.  
  
 For information on using minimal logging, see [Prerequisites for Minimal Logging in Bulk Import](../../relational-databases/import-export/prerequisites-for-minimal-logging-in-bulk-import.md).  
  
## Remarks  
 When using bcp.exe in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] or later, you might see errors in situations where there were no errors prior to [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)]. This is because in the later versions, bcp.exe no longer performs implicit data type conversion. Prior to [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], bcp.exe converted numeric data to a money data type, if the target table had a money data type. However, in that situation, bcp.exe simply truncated extra fields. Beginning in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], if data types do not match between the file and the target table, bcp.exe will raise an error if there is any data that would have to be truncated to fit into the target table. To resolve this error, fix the data to match the target data type. Optionally, use bcp.exe from a release prior to [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)].  
  
## In This Section  
  
-   [Using Data Files and Format Files](../../relational-databases/native-client-odbc-bulk-copy-operations/using-data-files-and-format-files.md)  
  
-   [Bulk Copying from Program Variables](../../relational-databases/native-client-odbc-bulk-copy-operations/bulk-copying-from-program-variables.md)  
  
-   [Managing Bulk Copy Batch Sizes](../../relational-databases/native-client-odbc-bulk-copy-operations/managing-bulk-copy-batch-sizes.md)  
  
-   [Bulk Copying Text and Image Data](../../relational-databases/native-client-odbc-bulk-copy-operations/bulk-copying-text-and-image-data.md)  
  
-   [Converting from DB-Library to ODBC Bulk Copy](../../relational-databases/native-client-odbc-bulk-copy-operations/converting-from-db-library-to-odbc-bulk-copy.md)  
  
## See Also  
 [SQL Server Native Client &#40;ODBC&#41;](../../relational-databases/native-client/odbc/sql-server-native-client-odbc.md)   
 [Bulk Import and Export of Data &#40;SQL Server&#41;](../../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md)  
  
  
