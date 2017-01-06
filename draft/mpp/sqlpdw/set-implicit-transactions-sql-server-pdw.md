---
title: "SET IMPLICIT_TRANSACTIONS (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 65b60579-7989-4e6e-91eb-2a66cbe03869
caps.latest.revision: 7
author: BarbKess
---
# SET IMPLICIT_TRANSACTIONS (SQL Server PDW)
Sets the implicit transaction mode for the connection to SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SET IMPLICIT_TRANSACTIONS { ON | OFF }  
```  
  
## General Remarks  
When ON, SET IMPLICIT_TRANSACTIONS sets the connection into implicit transaction mode. When OFF, it returns the connection to the AUTOCOMMIT transaction mode.  
  
When a connection is in implicit transaction mode and the connection is not currently in a transaction, executing any of the following statements starts a transaction:  
  
-   ALTER TABLE  
  
-   BEGIN TRANSACTION  
  
-   All CREATE statements  
  
-   DELETE  
  
-   All DROP statements  
  
-   GRANT  
  
-   INSERT  
  
-   REVOKE  
  
-   SELECT  
  
-   UPDATE  
  
If the connection is already in an open transaction, the statements do not start a new transaction.  
  
After a transaction is committed, executing one of the statements above starts a new transaction.  
  
Implicit transaction mode remains in effect until the connection executes a SET IMPLICIT_TRANSACTIONS OFF statement. This returns the connection to AUTOCOMMIT mode, in which all individual statements are committed when they complete successfully.  
  
The SQL Server Native Client OLE DB Provider for SQL Server and the SQL Server Native Client ODBC driver automatically set IMPLICIT_TRANSACTIONS to OFF when connecting.  
  
The IMPLICIT_TRANSACTIONS setting defaults to OFF when connections use the .NET Framework Data Provider for SQL Server (SqlClient) managed provider.  
  
The IMPLICIT_TRANSACTIONS setting is set at run time and not at parse time.  
  
## Limitations and Restrictions  
Implicit transactions must be explicitly committed or rolled back by the user at the end of the transaction. Otherwise, the transaction and all data changes within the transaction are rolled back when the user disconnects.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Connection Strings for JDBC and Microsoft JDBC Driver 4.0 for SQL Server &#40;SQL Server PDW&#41;](../sqlpdw/connection-strings-for-jdbc-and-microsoft-jdbc-driver-4-0-for-sql-server-sql-server-pdw.md)  
[@@TRANCOUNT &#40;SQL Server PDW&#41;](../sqlpdw/trancount-sql-server-pdw.md)  
[Transactions &#40;SQL Server PDW&#41;](../sqlpdw/transactions-sql-server-pdw.md)  
  
