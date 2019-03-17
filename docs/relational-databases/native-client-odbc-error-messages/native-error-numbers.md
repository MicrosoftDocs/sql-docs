---
title: "Native Error Numbers | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "ODBC error handling, native error numbers"
  - "SQL Server Native Client ODBC driver, errors"
  - "native error numbers [SQL Server Native Client]"
  - "messages [ODBC], native error numbers"
  - "errors [ODBC], native error numbers"
ms.assetid: 77cbc826-f47f-4803-8e7a-223d6df069b1
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Native Error Numbers
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../includes/snac-deprecated.md)]

  For errors that occur in the data source (returned by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]), the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver returns the native error number returned to it by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For errors detected by the driver, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver returns a native error number of 0. For more information about a list of native error numbers, see the error column of the **sysmessages** system table in the **master** database in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 For information about the state error codes, see [SQLSTATE &#40;ODBC Error Codes&#41;](../../relational-databases/native-client-odbc-error-messages/sqlstate-odbc-error-codes.md). For errors returned by the Net-Library, the native error number is from the underlying network software.  
  
## See Also  
 [Handling Errors and Messages](../../relational-databases/native-client-odbc-error-messages/handling-errors-and-messages.md)  
  
  
