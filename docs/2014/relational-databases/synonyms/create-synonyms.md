---
title: "Create Synonyms | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.synonym.general.f1"
helpviewer_keywords: 
  - "creating synonyms"
  - "synonyms [SQL Server], creating"
ms.assetid: fedfa7a5-d0b6-4e2b-90f4-a08122958e33
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Create Synonyms
  This topic describes how to create a synonym in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To create a synonym, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
 To create a synonym in a given schema, a user must have CREATE SYNONYM permission and either own the schema or have ALTER SCHEMA permission. The CREATE SYNONYM permission is a grantable permission.  
  
####  <a name="Permissions"></a> Permissions  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To Create a Synonym  
  
1.  In **Object Explorer**, expand the database where you want to create your new view.  
  
2.  Right-click the **Synonyms** folder, then click **New Synonym...**.  
  
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
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To Create a Synonym  
  
1.  Connect to the [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  From the Standard bar, click **New Query**.  
  
3.  Copy and paste the following examples into the query window and click **Execute**.  
  
###  <a name="TsqlExample"></a> Example (Transact-SQL)  
 The following example creates a synonym for an existing table in the [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] database. The synonym is then used in subsequent examples.  
  
```  
USE tempdb;  
GO  
CREATE SYNONYM MyAddressType  
FOR AdventureWorks2012.Person.AddressType;  
GO  
```  
  
 The following example inserts a row into the base table that is referenced by the `MyAddressType` synonym.  
  
```  
USE tempdb;  
GO  
INSERT INTO MyAddressType (Name)  
VALUES ('Test');  
GO  
```  
  
 The following example demonstrates how a synonym can be referenced in dynamic SQL.  
  
```  
USE tempdb;  
GO  
EXECUTE ('SELECT Name FROM MyAddressType');  
GO  
```  
  
  
