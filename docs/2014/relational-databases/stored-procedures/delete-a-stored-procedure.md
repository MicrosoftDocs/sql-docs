---
title: "Delete a Stored Procedure | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.technology: stored-procedures
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "removing stored procedures"
  - "stored procedures [SQL Server], deleting"
  - "deleting stored procedures"
ms.assetid: 232dbf4d-392a-406f-af3a-579518cd8e46
author: stevestein
ms.author: sstein
manager: craigg
---
# Delete a Stored Procedure
    
##  <a name="Top"></a> This topic describes how to delete a stored procedure in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
-   **Before you begin:**  [Limitations and Restrictions](#Restrictions), [Security](#Security)  
  
-   **To delete a procedure, using:**  [SQL Server Management Studio](#SSMSProcedure), [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
 Deleting a procedure can cause dependent objects and scripts to fail when the objects and scripts are not updated to reflect the removal of the procedure. However, if a new procedure of the same name and the same parameters is created to replace the one that was deleted, other objects that reference it will still process successfully. For more information, see [View the Dependencies of a Stored Procedure](view-the-dependencies-of-a-stored-procedure.md).  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the schema to which the procedure belongs, or CONTROL permission on the procedure.  
  
##  <a name="Procedures"></a> How to Delete a Stored Procedure  
 You can use one of the following:  
  
-   [SQL Server Management Studio](#SSMSProcedure)  
  
-   [Transact-SQL](#TsqlProcedure)  
  
###  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 **To delete a procedure in Object Explorer**  
  
1.  In Object Explorer, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)] and then expand that instance.  
  
2.  Expand **Databases**, expand the database in which the procedure belongs, and then expand **Programmability**.  
  
3.  Expand **Stored Procedures**, right-click the procedure to remove, and then click **Delete**.  
  
4.  To view objects that depend on the procedure, click **Show Dependencies**.  
  
5.  Confirm the correct procedure is selected, and then click **OK**.  
  
6.  Remove references to the procedure from any dependent objects and scripts.  
  
###  <a name="TsqlProcedure"></a> Using Transact-SQL  
 **To delete a procedure in Query Editor**  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)] and then expand that instance.  
  
2.  Expand **Databases**, expand the database in which the procedure belongs, or, from the tool bar, select the database from the list of available databases.  
  
3.  On the File menu, click **New Query**.  
  
4.  Obtain the name of stored procedure to remove in the current database. From Object Explorer, expand **Programmability** and then expand **Stored Procedures**. Alternatively, in the query editor, run the following statement.  
  
    ```sql  
    SELECT name AS procedure_name   
        ,SCHEMA_NAME(schema_id) AS schema_name  
        ,type_desc  
        ,create_date  
        ,modify_date  
    FROM sys.procedures;  
    ```  
  
5.  Copy and paste the following example into the query editor and insert a stored procedure name to delete from the current database.  
  
    ```sql  
    DROP PROCEDURE <stored procedure name>;  
    GO  
    ```  
  
6.  Remove references to the procedure from any dependent objects and scripts.  
  
## See Also  
 [Create a Stored Procedure](create-a-stored-procedure.md)   
 [Modify a Stored Procedure](modify-a-stored-procedure.md)   
 [Rename a Stored Procedure](rename-a-stored-procedure.md)   
 [View the Definition of a Stored Procedure](view-the-definition-of-a-stored-procedure.md)   
 [View the Dependencies of a Stored Procedure](view-the-dependencies-of-a-stored-procedure.md)   
 [DROP PROCEDURE &#40;Transact-SQL&#41;](/sql/t-sql/statements/drop-procedure-transact-sql)  
  
  
