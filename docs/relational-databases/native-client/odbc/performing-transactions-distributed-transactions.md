---
title: "Performing Distributed Transactions | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "MS DTC, performing distributed transactions"
  - "SQL Server Native Client ODBC driver, transactions"
  - "distributed transactions [ODBC]"
  - "transactions [ODBC]"
  - "ODBC, transactions"
ms.assetid: 2c17fba0-7a3c-453c-91b7-f801e7b39ccb
author: MightyPen
ms.author: genemi
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Performing Transactions - Distributed Transactions
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
[!INCLUDE[SNAC_Deprecated](../../../includes/snac-deprecated.md)]

  The Microsoft Distributed Transaction Coordinator (MS DTC) allows applications to extend transactions across two or more instances of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. It also allows applications to participate in transactions managed by transaction managers that comply with the Open Group DTP XA standard.  
  
 Normally, all transaction management commands are sent through the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver to the server. The application starts a transaction by calling [SQLSetConnectAttr](../../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md) with the autocommit mode turned off. The application then performs the updates comprising the transaction and calls [SQLEndTran](../../../relational-databases/native-client-odbc-api/sqlendtran.md) with either the SQL_COMMIT or SQL_ROLLBACK option.  
  
 When using MS DTC, however, MS DTC becomes the transaction manager and the application no longer uses **SQLEndTran**.  
  
 When enlisted in a distributed transaction, and then enlist in a second distributed transaction, the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC Driver defects from the original distributed transaction and enlists in the new transaction. For more information, see [DTC Programmer's Reference](https://msdn.microsoft.com/library/ms686108\(VS.85\).aspx).  
  
## See Also  
 [Performing Transactions &#40;ODBC&#41;](https://msdn.microsoft.com/library/f431191a-5762-4f0b-85bb-ac99aff29724)  
  
  
