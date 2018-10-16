---
title: "Cursor Programming Details (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "SQL Server Native Client ODBC driver, cursors"
  - "ODBC applications, cursors"
  - "ODBC cursors, programming"
  - "cursors [ODBC], programming"
ms.assetid: 6bae29c4-7f49-419c-8712-90db734f992e
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Cursor Programming Details (ODBC)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../../includes/snac-deprecated.md)]

  Choosing the correct cursor type can improve application performance. Under certain conditions, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] may implicitly convert a cursor type when you execute an SQL statement that is not supported by the cursor type you requested.  
  
## In This Section  
  
-   [Implicit Cursor Conversions &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-cursors/programming/implicit-cursor-conversions-odbc.md)  
  
-   [Using Autofetch with ODBC Cursors](../../../relational-databases/native-client-odbc-cursors/programming/using-autofetch-with-odbc-cursors.md)  
  
-   [Fast Forward-Only Cursors &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-cursors/programming/fast-forward-only-cursors-odbc.md)  
  
## See Also  
 [Using Cursors &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-cursors/using-cursors-odbc.md)  
  
  
