---
description: "Create Synonyms"
title: "Create Synonyms"
ms.custom: ""
ms.date: "02/01/2022"
ms.service: sql
ms.subservice: t-sql
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.synonym.general.f1"
helpviewer_keywords: 
  - "creating synonyms"
  - "synonyms [SQL Server], creating"
author: markingmyname
ms.author: maghan
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Create Synonyms
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  This article describes how to create a synonym in [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
###  <a name="Security"></a> Security  
 To create a synonym in a given schema, a user must have CREATE SYNONYM permission and either own the schema or have ALTER SCHEMA permission. The CREATE SYNONYM permission is a grantable permission.  
   
##  <a name="SSMSProcedure"></a> Use SQL Server Management Studio  
  
### Create a Synonym  
  
1.  In **Object Explorer**, expand the database where you want to create your new view.  
  
2.  Right-click the **Synonyms** folder, then select **New Synonym...**.  
  
3.  In the **Add Synonym** dialog box, enter the following information.  

     **Synonym name**  
     Type the new name you will use for this object.  
  
     **Synonym schema**  
     Type the schema of the new name you will use for this object.  
  
     **Server name**  
     Type the server instance to connect to.  
  
     **Database name**  
     Type or select the database containing the object.  
  
     **Schema**  
     Type or select the schema that owns the object.  
  
     **Object type**  
     Select the type of object.  
  
     **Object name**  
     Type the name of the object to which the synonym refers.  
  
##  <a name="TsqlProcedure"></a> Use Transact-SQL  
  
### Create a Synonym
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, select **New Query**.  
  
3.  Copy and paste the following examples into the query window and select **Execute**.  
  
###  <a name="TsqlExample"></a> Example (Transact-SQL)  
 The following example creates a synonym for an existing table in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. The synonym is then used in subsequent examples.  
  
```sql  
USE tempdb;  
GO  
CREATE SYNONYM MyAddressType  
FOR AdventureWorks2012.Person.AddressType;  
GO  
```  
  
 The following example inserts a row into the base table that is referenced by the `MyAddressType` synonym.  
  
```sql  
USE tempdb;  
GO  
INSERT INTO MyAddressType (Name)  
VALUES ('Test');  
GO  
```  
  
 The following example demonstrates how a synonym can be referenced in dynamic SQL.  
  
```sql  
USE tempdb;  
GO  
EXECUTE ('SELECT Name FROM MyAddressType');  
GO  
```  

## Next steps

- [CREATE SYNONYM (Transact-SQL)](../../t-sql/statements/create-synonym-transact-sql.md)
