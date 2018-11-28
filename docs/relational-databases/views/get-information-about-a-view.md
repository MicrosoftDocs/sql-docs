---
title: "Get Information About a View | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "table-view-index, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: table-view-index
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.viewproperties.general.f1"
helpviewer_keywords: 
  - "views [SQL Server], status information"
  - "metadata [SQL Server], views"
  - "dependencies [SQL Server], views"
  - "displaying view information"
  - "views [SQL Server], metadata"
  - "viewing view information"
  - "status information [SQL Server], views"
  - "view dependencies"
ms.assetid: 05a73e33-8f85-4fb6-80c1-1b659e753403
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Get Information About a View
[!INCLUDE[tsql-appliesto-ss2008-asdb-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-all-md.md)]
  You can gain information about a view's definition or properties in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]. You may need to see the definition of the view to understand how its data is derived from the source tables or to see the data defined by the view.  
  
> [!IMPORTANT]  
>  If you change the name of an object referenced by a view, you must modify the view so that its text reflects the new name. Therefore, before renaming an object, display the dependencies of the object first to determine if any views are affected by the proposed change.  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To get information about a view, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Using `sp_helptext` to return the definition of a view requires membership in the **public** role. Using `sys.sql_expression_dependencies` to find all the dependencies on a view requires VIEW DEFINITION permission on the database and SELECT permission on `sys.sql_expression_dependencies` for the database. System object definitions, like the ones returned in SELECT OBJECT_DEFINITION, are publicly visible.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### Get view properties by using Object Explorer  
  
1.  In **Object Explorer**, click the plus sign next to the database that contains the view to which you want to view the properties, and then click the plus sign to expand the **Views** folder.  
  
2.  Right-click the view of which you want to view the properties and select **Properties**.  
  
     The following properties show in the **View Properties** dialog box.  
  
     **Database**  
     The name of the database containing this view.  
  
     **Server**  
     The name of the current server instance.  
  
     **User**  
     The name of the user of this connection.  
  
     **Created date**  
     Displays the date the view was created.  
  
     **Name**  
     The name of the current view.  
  
     **Schema**  
     Displays the schema that owns the view.  
  
     **System object**  
     Indicates whether the view is a system object. Values are True and False.  
  
     **ANSI NULLs**  
     Indicates if the object was created with the ANSI NULLs option.  
  
     **Encrypted**  
     Indicates whether the view is encrypted. Values are True and False.  
  
     **Quoted identifier**  
     Indicates if the object was created with the quoted identifier option.  
  
     **Schema bound**  
     Indicates whether the view is schema-bound. Values are True and False. For information about schema-bound views, see the SCHEMABINDING portion of [CREATE VIEW &#40;Transact-SQL&#41;](../../t-sql/statements/create-view-transact-sql.md).  
  
#### Getting view properties by using the View Designer tool  
  
1.  In **Object Explorer**, expand the database that contains the view to which you want to view the properties, and then expand the **Views** folder.  
  
2.  Right-click the view of which you want to view the properties and select **Design**.  
  
3.  Right-click in the blank space of the Diagram pane and click **Properties**.  
  
     The following properties show in the **Properties** pane.  
  
     **(Name)**  
     The name of the current view.  
  
     **Database Name**  
     The name of the database containing this view.  
  
     **Description**  
     A brief description of the current view.  
  
     **Schema**  
     Displays the schema that owns the view.  
  
     **Server Name**  
     The name of the current server instance.  
  
     **Bind to Schema**  
     Prevents users from modifying the underlying objects that contribute to this view in any way that would invalidate the view definition.  
  
     **Deterministic**  
     Shows whether the data type of the selected column can be determined with certainty  
  
     **Distinct Values**  
     Specifies that the query will filter out duplicates in the view. This option is useful when you are using only some of the columns from a table and those columns might contain duplicate values, or when the process of joining two or more tables produces duplicate rows in the result set. Choosing this option is equivalent to inserting the keyword DISTINCT into the statement in the SQL pane.  
  
     **GROUP BY Extension**  
     Specifies that additional options for views based on aggregate queries are available.  
  
     **Output All Columns**  
     Shows whether all columns are returned by the selected view. This is set at the time the view is created.  
  
     **SQL Comment**  
     Shows a description of the SQL statements. To see the entire description, or to edit it, click the description and then click the ellipses **(...)** to the right of the property. Your comments might include information such as who uses the view and when they use it.  
  
     **Top Specification**  
     Expands to show properties for the **Top**, **Expression**, **Percent**, and **With Ties** properties.  
  
     **(Top)**  
     Specifies that the view will include a TOP clause, which returns only the first n rows or first n percentage of rows in the result set. The default is that the view returns the first 10 rows in the result set. Use this to change the number of rows to return or to specify a different percentage  
  
     **Expression**  
     Shows what percent (if **Percent** is set to **Yes**) or records (if **Percent** is set to **No**) that the view will return.  
  
     **Percent**  
     Specifies that the query will include a **TOP** clause, returning only the first n percentage of rows in the result set  
  
     **With Ties**  
     Specifies that the view will include a **WITH TIES** clause. **WITH TIES** is useful if a view includes an **ORDER BY** clause and a **TOP** clause based on percentage. If this option is set, and if the percentage cutoff falls in the middle of a set of rows with identical values in the **ORDER BY** clause, the view is extended to include all such rows.  
  
     **Update Specification**  
     Expands to show properties for the **Update Using View Rules** and **Check Option** properties.  
  
     **(Update Using View Rules)**  
     Indicates that all updates and insertions to the view will be translated by Microsoft Data Access Components (MDAC) into SQL statements that refer to the view, rather than into SQL statements that refer directly to the view's base tables.  
  
     In some cases, MDAC manifests view update and view insert operations as updates and inserts against the view's underlying base tables. By selecting **Update Using View Rules**, you can ensure that MDAC generates update and insert operations against the view itself.  
  
     **Check Option**  
     Indicates that when you open this view and modify the **Results** pane, the data source checks whether the added or modified data satisfies the **WHERE** clause of the view definition. If your modification do not satisfy the **WHERE** clause, you will see an error with more information.  
  
#### To get dependencies on the view  
  
1.  In **Object Explorer**, expand the database that contains the view to which you want to view the properties, and then expand the **Views** folder.  
  
2.  Right-click the view of which you want to view the properties and select **View Dependencies**.  
  
3.  Select **Objects that depend on [view name]** to display the objects that refer to the view.  
  
4.  Select **Objects on which [view name] depends** to display the objects that are referenced by the view.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To get the definition and properties of a view  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste one of the following examples into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    SELECT definition, uses_ansi_nulls, uses_quoted_identifier, is_schema_bound  
    FROM sys.sql_modules  
    WHERE object_id = OBJECT_ID('HumanResources.vEmployee');   
    GO  
    ```  
  
    ```  
    USE AdventureWorks2012;   
    GO  
    SELECT OBJECT_DEFINITION (OBJECT_ID('HumanResources.vEmployee')) AS ObjectDefinition;   
    GO  
    ```  
  
    ```  
    EXEC sp_helptext 'HumanResources.vEmployee';  
    ```  
  
 For more information, see [sys.sql_modules &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-modules-transact-sql.md), [OBJECT_DEFINITION &#40;Transact-SQL&#41;](../../t-sql/functions/object-definition-transact-sql.md) and [sp_helptext &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-helptext-transact-sql.md).  
  
#### To get the dependencies of a view  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    SELECT OBJECT_NAME(referencing_id) AS referencing_entity_name,   
        o.type_desc AS referencing_desciption,   
        COALESCE(COL_NAME(referencing_id, referencing_minor_id), '(n/a)') AS referencing_minor_id,   
        referencing_class_desc, referenced_class_desc,  
        referenced_server_name, referenced_database_name, referenced_schema_name,  
        referenced_entity_name,   
        COALESCE(COL_NAME(referenced_id, referenced_minor_id), '(n/a)') AS referenced_column_name,  
        is_caller_dependent, is_ambiguous  
    FROM sys.sql_expression_dependencies AS sed  
    INNER JOIN sys.objects AS o ON sed.referencing_id = o.object_id  
    WHERE referencing_id = OBJECT_ID(N'Production.vProductAndDescription');  
    GO  
    ```  
  
 For more information, see [sys.sql_expression_dependencies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-sql-expression-dependencies-transact-sql.md) and [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md).  
  
  
