---
title: "OPENQUERY (Transact-SQL)"
description: "OPENQUERY (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/16/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "OPENQUERY_TSQL"
  - "OPENQUERY"
helpviewer_keywords:
  - "DELETE statement [SQL Server], OPENQUERY function"
  - "OPENQUERY function"
  - "FROM clause, OPENQUERY function"
  - "UPDATE statement [SQL Server], OPENQUERY function"
  - "pass-through queries [SQL Server]"
  - "INSERT statement [SQL Server], OPENQUERY function"
dev_langs:
  - "TSQL"
---
# OPENQUERY (Transact-SQL)
[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

  Executes the specified pass-through query on the specified linked server. This server is an OLE DB data source. OPENQUERY can be referenced in the FROM clause of a query as if it were a table name. OPENQUERY can also be referenced as the target table of an INSERT, UPDATE, or DELETE statement. This is subject to the capabilities of the OLE DB provider. Although the query may return multiple result sets, OPENQUERY returns only the first one.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
OPENQUERY ( linked_server ,'query' )  
```  
  
## Arguments
 *linked_server*  
 Is an identifier representing the name of the linked server.  
  
 **'** *query* **'**  
 Is the query string executed in the linked server. The maximum length of the string is 8 KB.  
  
## Remarks  
 OPENQUERY does not accept variables for its arguments.  
  
 OPENQUERY cannot be used to execute extended stored procedures on a linked server. However, an extended stored procedure can be executed on a linked server by using a four-part name. For example:  
  
```sql  
EXEC SeattleSales.master.dbo.xp_msver  
```  
  
 Any call to OPENDATASOURCE, OPENQUERY, or OPENROWSET in the FROM clause is evaluated separately and independently from any call to these functions used as the target of the update, even if identical arguments are supplied to the two calls. In particular, filter or join conditions applied on the result of one of those calls have no effect on the results of the other.  
  
## Permissions  
 Any user can execute OPENQUERY. The permissions that are used to connect to the remote server are obtained from the settings defined for the linked server.  
  
## Examples  
  
### A. Executing an UPDATE pass-through query  
 The following example uses a pass-through `UPDATE` query against the linked server named `OracleSvr`.  
  
```sql  
UPDATE OPENQUERY (OracleSvr, 'SELECT name FROM joe.titles WHERE id = 101')   
SET name = 'ADifferentName';  
```  
  
### B. Executing an INSERT pass-through query  
 The following example uses a pass-through `INSERT` query against the linked server named `OracleSvr`.  
  
```sql  
INSERT OPENQUERY (OracleSvr, 'SELECT name FROM joe.titles')  
VALUES ('NewTitle');  
```  
  
### C. Executing a DELETE pass-through query  
 The following example uses a pass-through `DELETE` query to delete the row inserted in example B.  
  
```sql  
DELETE OPENQUERY (OracleSvr, 'SELECT name FROM joe.titles WHERE name = ''NewTitle''');  
```  
  
### D. Executing a SELECT pass-through query  
 The following example uses a pass-through `SELECT` query to select the row inserted in example B.  
  
```sql  
SELECT * FROM OPENQUERY (OracleSvr, 'SELECT name FROM joe.titles WHERE name = ''NewTitle''');  
```  
    
## Related content

- [DELETE (Transact-SQL)](../statements/delete-transact-sql.md)
- [FROM clause plus JOIN, APPLY, PIVOT (Transact-SQL)](../queries/from-transact-sql.md)
- [INSERT (Transact-SQL)](../statements/insert-transact-sql.md)
- [OPENDATASOURCE (Transact-SQL)](opendatasource-transact-sql.md)
- [OPENROWSET (Transact-SQL)](openrowset-transact-sql.md)
- [SELECT (Transact-SQL)](../queries/select-transact-sql.md)
- [sys.sp_addlinkedserver (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md)
- [sys.sp_serveroption (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-serveroption-transact-sql.md)
- [UPDATE (Transact-SQL)](../queries/update-transact-sql.md)
- [WHERE (Transact-SQL)](../queries/where-transact-sql.md)
