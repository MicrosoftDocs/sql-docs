---
title: "OPENQUERY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "OPENQUERY_TSQL"
  - "OPENQUERY"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "DELETE statement [SQL Server], OPENQUERY function"
  - "OPENQUERY function"
  - "FROM clause, OPENQUERY function"
  - "UPDATE statement [SQL Server], OPENQUERY function"
  - "pass-through queries [SQL Server]"
  - "INSERT statement [SQL Server], OPENQUERY function"
ms.assetid: b805e976-f025-4be1-bcb0-3a57b0c57717
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# OPENQUERY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Executes the specified pass-through query on the specified linked server. This server is an OLE DB data source. OPENQUERY can be referenced in the FROM clause of a query as if it were a table name. OPENQUERY can also be referenced as the target table of an INSERT, UPDATE, or DELETE statement. This is subject to the capabilities of the OLE DB provider. Although the query may return multiple result sets, OPENQUERY returns only the first one.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
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
 The following example uses a pass-through `UPDATE` query against the linked server created in example A.  
  
```sql  
UPDATE OPENQUERY (OracleSvr, 'SELECT name FROM joe.titles WHERE id = 101')   
SET name = 'ADifferentName';  
```  
  
### B. Executing an INSERT pass-through query  
 The following example uses a pass-through `INSERT` query against the linked server created in example A.  
  
```sql  
INSERT OPENQUERY (OracleSvr, 'SELECT name FROM joe.titles')  
VALUES ('NewTitle');  
```  
  
### C. Executing a DELETE pass-through query  
 The following example uses a pass-through `DELETE` query to delete the row inserted in example C.  
  
```sql  
DELETE OPENQUERY (OracleSvr, 'SELECT name FROM joe.titles WHERE name = ''NewTitle''');  
```  
  
### D. Executing a SELECT pass-through query  
 The following example uses a pass-through `SELECT` query to select the row inserted in example C.  
  
```sql  
SELECT * FROM OPENQUERY (OracleSvr, 'SELECT name FROM joe.titles WHERE name = ''NewTitle''');  
```  
    
## See Also  
 [DELETE &#40;Transact-SQL&#41;](../../t-sql/statements/delete-transact-sql.md)   
 [FROM &#40;Transact-SQL&#41;](../../t-sql/queries/from-transact-sql.md)   
 [INSERT &#40;Transact-SQL&#41;](../../t-sql/statements/insert-transact-sql.md)   
 [OPENDATASOURCE &#40;Transact-SQL&#41;](../../t-sql/functions/opendatasource-transact-sql.md)   
 [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md)   
 [Rowset Functions &#40;Transact-SQL&#41;](../../t-sql/functions/rowset-functions-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [sp_addlinkedserver &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md)   
 [sp_serveroption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-serveroption-transact-sql.md)   
 [UPDATE &#40;Transact-SQL&#41;](../../t-sql/queries/update-transact-sql.md)   
 [WHERE &#40;Transact-SQL&#41;](../../t-sql/queries/where-transact-sql.md)  
  
  
