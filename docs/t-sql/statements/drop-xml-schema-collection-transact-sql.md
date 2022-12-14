---
title: "DROP XML SCHEMA COLLECTION (Transact-SQL)"
description: DROP XML SCHEMA COLLECTION (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "11/25/2015"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DROP XML SCHEMA COLLECTION"
  - "DROP_XML_SCHEMA_COLLECTION_TSQL"
helpviewer_keywords:
  - "deleting XML schema collections"
  - "XML schema collections [SQL Server], removing"
  - "schema collections [SQL Server], removing"
  - "removing XML schema collections"
  - "dropping XML schema collections"
  - "DROP XML SCHEMA COLLECTION statement"
dev_langs:
  - "TSQL"
---
# DROP XML SCHEMA COLLECTION (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Deletes the whole XML schema collection and all of its components.  
  
![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
DROP XML SCHEMA COLLECTION [ relational_schema. ]sql_identifier  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
*relational_schema*  
Identifies the relational schema name. If not specified, the default relational schema is assumed.  
  
*sql_identifier*  
Name of the XML schema collection to drop.  
  
## Remarks  
Dropping an XML schema collection is a transactional operation. When you drop an XML schema collection inside a transaction and later roll back the transaction, the XML schema collection isn't dropped.  
  
You can't drop an XML schema collection when it's in use. So, the collection being dropped can't be in any of the following conditions:  
  
-   Associated with any **xml** type parameter or column.  
  
-   Specified in any table constraints.  
  
-   Referenced in a schema-bound function or stored procedure. For example, the following function locks the XML schema collection `MyCollection` because the function specifies `WITH SCHEMABINDING`. If you remove it, there's no lock on the XML SCHEMA COLLECTION.  
  
    ```sql  
    CREATE FUNCTION dbo.MyFunction()  
    RETURNS int  
    WITH SCHEMABINDING  
    AS  
    BEGIN  
       /* some code may go here */
       DECLARE @x XML(MyCollection)  
       /* more code may go here */
    END;  
    ```  
  
## Permissions  
To drop an XML SCHEMA COLLECTION requires DROP permission on the collection.  
  
## Examples  
The following example shows removing an XML schema collection.  
  
```sql  
DROP XML SCHEMA COLLECTION ManuInstructionsSchemaCollection;  
GO  
```  
  
## See Also  
 [CREATE XML SCHEMA COLLECTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-xml-schema-collection-transact-sql.md)   
 [ALTER XML SCHEMA COLLECTION &#40;Transact-SQL&#41;](../../t-sql/statements/alter-xml-schema-collection-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [Compare Typed XML to Untyped XML](../../relational-databases/xml/compare-typed-xml-to-untyped-xml.md)   
 [Requirements and Limitations for XML Schema Collections on the Server](../../relational-databases/xml/requirements-and-limitations-for-xml-schema-collections-on-the-server.md)  
  
  
