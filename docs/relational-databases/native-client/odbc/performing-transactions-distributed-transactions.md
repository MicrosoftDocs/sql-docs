---
title: "Create a distributed transactions | Microsoft Docs"
description: Applications can use MSDTC to extend or distribute a transaction across several instances of SQL Server. A .NET class can also distribute a transaction.
ms.custom: ""
ms.date: "05/13/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "MS DTC, performing distributed transactions"
  - "SQL Server Native Client ODBC driver, transactions"
  - "distributed transactions [ODBC]"
  - "transactions [ODBC]"
  - "ODBC, transactions"
ms.assetid: 2c17fba0-7a3c-453c-91b7-f801e7b39ccb
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create a distributed transaction

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

<!--
The following includes .md file is Empty, as of long before 2019/May/13.
/includes/snac-deprecated.md
-->

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../../includes/snac-removed-oledb-and-odbc.md)]


A distributed transaction can be created for different Microsoft SQL systems in different ways.

## ODBC driver calls the MSDTC for SQL Server on-premises

The Microsoft Distributed Transaction Coordinator (MSDTC) allows applications to extend or _distribute_ a transaction across two or more instances of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. The distributed transaction works even when the two instances are hosted on separate computers.

MSDTC is installed for Microsoft SQL Server on-premises, but isn't available for Microsoft's Azure SQL Database cloud service.

MSDTC is called by the SQL Server Native Client driver for Open Database Connectivity (ODBC), when your C++ program manages a distributed transaction. The Native Client ODBC driver has a transaction manager that is compliant with the Open Group Distributed Transaction Processing (DTP) XA standard. This compliance is required by MSDTC. Typically, all transaction management commands are sent through this Native Client ODBC driver. The sequence is as follows:

1. Your C++ Native Client ODBC application starts a transaction by calling [SQLSetConnectAttr](../../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md), with the autocommit mode turned off.

2. The application updates some data on SQL Server X on computer A.

3. The application updates some data on SQL Server Y on computer B.
    - If an update on SQL Server Y fails, all the uncommitted updates on both SQL Server instances are rolled back.

4. Finally, the application ends the transaction by calling [SQLEndTran _(1)_](../../../relational-databases/native-client-odbc-api/sqlendtran.md), with either the SQL_COMMIT or SQL_ROLLBACK option.

_(1)_ MSDTC can be invoked without ODBC. In such a case, MSDTC becomes the transaction manager, and the application no longer uses **SQLEndTran**.

### Only one distributed transaction

Suppose that your C++ Native Client ODBC application is enlisted in a distributed transaction. Next the application enlists in a second distributed transaction. In this case, the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver leaves the original distributed transaction, and enlists in the new distributed transaction.

For more information, see [DTC Programmer's Reference](/previous-versions/windows/desktop/ms686108(v=vs.85)).

## C# alternative for SQL Database in the cloud

MSDTC isn't supported for either Azure SQL Database or Azure Synapse Analytics.

However, a distributed transaction can be created for SQL Database by having your C# program use the .NET class [System.Transactions.TransactionScope](/dotnet/api/system.transactions.transactionscope).

### Other programming languages

The following other programming languages might not provide any support for distributed transactions with the SQL Database service:

- Native C++ that use ODBC drivers
- Linked server using Transact-SQL
- JDBC drivers

## See also

[Performing Transactions (ODBC)](performing-transactions-in-odbc.md)