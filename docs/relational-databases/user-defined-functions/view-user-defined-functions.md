---
title: "View User-defined Functions | Microsoft Docs"
ms.custom: ""
ms.date: "03/16/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.udfproperties.general.f1"
  - "sql13.swb.functionproperties.general.f1"
helpviewer_keywords: 
  - "displaying user-defined functions"
  - "viewing user-defined functions"
  - "user-defined functions [SQL Server], viewing"
  - "status information [SQL Server], user-defined functions"
ms.assetid: a45dfab5-6384-4311-b935-2e23a70c5c10
author: "rothja"
ms.author: "jroth"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# View User-defined Functions
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]
  You can gain information about the definition or properties of a user-defined function in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. You may need to see the definition of the function to understand how its data is derived from the source tables or to see the data defined by the function.  
  
> [!IMPORTANT]  
>  If you change the name of an object referenced by a function, you must modify that function so that its text reflects the new name. Therefore, before renaming an object, display the dependencies of the object first to determine if any functions are affected by the proposed change.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To get information about a function, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Using **sys.sql_expression_dependencies** to find all the dependencies on a function requires VIEW DEFINITION permission on the database and SELECT permission on **sys.sql_expression_dependencies** for the database. System object definitions, like the ones returned in OBJECT_DEFINITION, are publicly visible.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To show a user-defined function's properties  
  
1.  In **Object Explorer**, click the plus sign next to the database that contains the function to which you want to view the properties, and then click the plus sign to expand the **Programmability** folder.  
  
2.  Click the plus sign to expand the **Functions** folder.  
  
3.  Click the plus sign to expand the folder that contains the function to which you want to view the properties:  
  
    -   Table-valued Function  
  
    -   Scalar-valued Function  
  
    -   Aggregate Function  
  
4.  Right-click the function of which you want to view the properties and select **Properties**.  
  
     The following properties appear in the **Function Properties -** _function_name_ dialog box.  
  
     **Database**  
     The name of the database containing this function.  
  
     **Server**  
     The name of the current server instance.  
  
     **User**  
     The name of the user of this connection.  
  
     **Created date**  
     Displays the date the function was created.  
  
     **Execute As**  
     Execution context for the function.  
  
     **Name**  
     The name of the current function.  
  
     **Schema**  
     Displays the schema that owns the function.  
  
     **System object**  
     Indicates whether the function is a system object. Values are True and False.  
  
     **ANSI NULLs**  
     Indicates if the object was created with the ANSI NULLs option.  
  
     **Encrypted**  
     Indicates whether the function is encrypted. Values are True and False.  
  
     **Function Type**  
     The type of user defined function.  
  
     **Quoted identifier**  
     Indicates if the object was created with the quoted identifier option.  
  
     **Schema bound**  
     Indicates whether the function is schema-bound. Values are True and False. For information about schema-bound functions, see the SCHEMABINDING section of [CREATE FUNCTION &#40;Transact-SQL&#41;](../../t-sql/statements/create-function-transact-sql.md).  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To get the definition and properties of a function  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste one of the following examples into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    -- Get the function name, definition, and relevant properties  
    SELECT sm.object_id,   
       OBJECT_NAME(sm.object_id) AS object_name,   
       o.type,   
       o.type_desc,   
       sm.definition,  
       sm.uses_ansi_nulls,  
       sm.uses_quoted_identifier,  
       sm.is_schema_bound,  
       sm.execute_as_principal_id  
    -- using the two system tables sys.sql_modules and sys.objects  
    FROM sys.sql_modules AS sm  
    JOIN sys.objects AS o ON sm.object_id = o.object_id  
    -- from the function 'dbo.ufnGetProductDealerPrice'  
    WHERE sm.object_id = OBJECT_ID('dbo.ufnGetProductDealerPrice')  
    ORDER BY o.type;  
    GO  
  
    ```  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    -- Get the definition of the function dbo.ufnGetProductDealerPrice  
    SELECT OBJECT_DEFINITION (OBJECT_ID('dbo.ufnGetProductDealerPrice')) AS ObjectDefinition;  
    GO  
    ```  
  
 For more information, see [sys.sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md) and [OBJECT_DEFINITION &#40;Transact-SQL&#41;](../../t-sql/functions/object-definition-transact-sql.md).  
  
#### To get the dependencies of a function  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    -- Get all of the dependency information  
    SELECT OBJECT_NAME(sed.referencing_id) AS referencing_entity_name,   
        o.type_desc AS referencing_desciption,   
        COALESCE(COL_NAME(sed.referencing_id, sed.referencing_minor_id), '(n/a)') AS referencing_minor_id,   
        sed.referencing_class_desc, sed.referenced_class_desc,  
        sed.referenced_server_name, sed.referenced_database_name, sed.referenced_schema_name,  
        sed.referenced_entity_name,   
        COALESCE(COL_NAME(sed.referenced_id, sed.referenced_minor_id), '(n/a)') AS referenced_column_name,  
        sed.is_caller_dependent, sed.is_ambiguous  
    -- from the two system tables sys.sql_expression_dependencies and sys.object  
    FROM sys.sql_expression_dependencies AS sed  
    INNER JOIN sys.objects AS o ON sed.referencing_id = o.object_id  
    -- on the function dbo.ufnGetProductDealerPrice  
    WHERE sed.referencing_id = OBJECT_ID('dbo.ufnGetProductDealerPrice');  
    GO  
    ```  
  
 For more information, see [sys.sql_expression_dependencies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md) and [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md).  
  
  
