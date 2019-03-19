---
title: "View Plan Guide Properties | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
f1_keywords: 
  - "sql12.swb.planguideprop.general.f1"
helpviewer_keywords: 
  - "plan guides [SQL Server], view plan guide properties"
  - "viewing plan guide properties"
ms.assetid: 8c0d2f39-59c1-4168-a649-65473f6a771b
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# View Plan Guide Properties
  You can view the properties of plan guides in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)]  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Security](#Security)  
  
-   **To view the properties of plan guides, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 The visibility of the metadata in catalog views is limited to securables that either a user owns or on which the user has been granted some permission.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To view the properties of a plan guide  
  
1.  Click the plus sign to expand the database in which you want to view the properties of a plan guide, and then click the plus sign to expand the **Programmability** folder.  
  
2.  Click the plus sign to expand the **Plan Guides** folder.  
  
3.  Right-click the plan guide of which you want to view the properties and select **Properties**.  
  
     The following properties show in the **Plan Guide Properties** dialog box.  
  
     **Hints**  
     Displays the query hints or query plan to be applied to the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement. When a query plan is specified as a hint, the XML Showplan output for the plan is displayed.  
  
     **Is disabled**  
     Displays the status of the plan guide. Possible values are **True** and **False**.  
  
     **Name**  
     Displays the name of the plan guide.  
  
     **Parameters**  
     When the scope type is SQL or TEMPLATE, displays the name and data type of all parameters that are embedded in the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement.  
  
     **Scope batch**  
     Displays the batch text in which the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement appears.  
  
     **Scope object name**  
     When the scope type is OBJECT, displays the name of the [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure, user-defined scalar function, multistatement table-valued function, or DML trigger in which the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement appears.  
  
     **Scope schema name**  
     When the scope type is OBJECT, displays the name of the schema in which the object is contained.  
  
     **Scope type**  
     Displays the type of entity in which the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement appears. This specifies the context for matching the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement to the plan guide. Possible values are **OBJECT**, **SQL**, and **TEMPLATE**.  
  
     **Statement**  
     Displays the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement against which the plan guide is applied.  
  
4.  Click **OK**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To view the properties of a plan guide  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    -- If a plan guide named "Guide1" already exists in the AdventureWorks2012 database, delete it.  
    USE AdventureWorks2012;  
    GO  
    IF OBJECT_ID(N'Guide1') IS NOT NULL  
       EXEC sp_control_plan_guide N'DROP', N'Guide1';  
    GO  
    -- creates a plan guide named Guide1 based on a SQL statement  
    EXEC sp_create_plan_guide   
        @name = N'Guide1',   
        @stmt = N'SELECT TOP 1 *   
                  FROM Sales.SalesOrderHeader   
                  ORDER BY OrderDate DESC',   
        @type = N'SQL',  
        @module_or_batch = NULL,   
        @params = NULL,   
        @hints = N'OPTION (MAXDOP 1)';  
    GO  
    -- Gets the name, created date, and all other relevant property information on the plan guide created above.   
    SELECT name AS plan_guide_name,  
       create_date,  
       query_text,  
       scope_type_desc,  
       OBJECT_NAME(scope_object_id) AS scope_object_name,  
       scope_batch,  
       parameters,  
       hints,  
       is_disabled  
    FROM sys.plan_guides  
    WHERE name = N'Guide1';  
    GO  
    ```  
  
 For more information, see [sys.plan_guides &#40;Transact-SQL&#41;](/sql/relational-databases/system-catalog-views/sys-plan-guides-transact-sql).  
  
  
