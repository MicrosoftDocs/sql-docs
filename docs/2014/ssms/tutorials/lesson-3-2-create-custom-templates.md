---
title: "Create Custom Templates | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
helpviewer_keywords: 
  - "tql"
  - "templates [Transact-SQL], creating"
  - "templates [Transact-SQL]"
ms.assetid: 41098e78-b482-410e-bfe8-2ac10769ac4a
author: stevestein
ms.author: sstein
manager: craigg
---
# Create Custom Templates
  [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] comes with templates for many common tasks, but the real power of templates lies in the ability to create a custom template for a complex script that you must create frequently. In this practice you will create a simple script with few parameters, but templates are useful for long, repetitive scripts, too.  
  
## Using Custom Templates  
  
#### To create a custom template  
  
1.  In Template Explorer, expand **SQL Server Templates**, right-click **Stored Procedure**, point to **New**, and then click **Folder**.  
  
2.  Type **Custom** as the name of your new template folder, and then press ENTER.  
  
3.  Right-click **Custom**, point to **New**, and then click **Template**.  
  
4.  Type **WorkOrdersProc** as the name of your new template, and then press **Enter**.  
  
5.  Right-click **WorkOrdersProc**, and then click **Edit**.  
  
6.  In the **Connect to Database Engine** dialog box, verify the connection information and then click **Connect**.  
  
7.  In Query Editor, type the following script to create a stored procedure that looks up orders for a particular part, in this case the Blade. (You can copy and paste the code from the Tutorial window.)  
  
    ```  
    USE AdventureWorks20012;  
    GO  
    IF EXISTS (  
    SELECT *   
       FROM INFORMATION_SCHEMA.ROUTINES   
       WHERE SPECIFIC_NAME = 'WorkOrdersForBlade')  
       DROP PROCEDURE dbo.WorkOrdersForBlade;  
    GO  
    CREATE PROCEDURE dbo.WorkOrdersForBlade  
    AS  
    SELECT Name, WorkOrderID   
    FROM Production.WorkOrder AS WO  
    JOIN Production.Product AS Prod  
    ON WO.ProductID = Prod.ProductID  
    WHERE Name = 'Blade';  
    GO  
    ```  
  
8.  Press F5 to execute this script, creating the **WorkOrdersForBlade** procedure.  
  
9. In Object Explorer, right-click your server, and then click **New Query**. A new Query Editor window opens.  
  
10. In Query Editor, type **EXECUTE dbo.WorkOrdersForBlade**, and then press F5 to execute the query. Confirm that the **Results** pane returns a list of work orders for blades.  
  
11. Edit the template script (the script in step 7), replacing the product name Blade with the parameter <strong>*<*product_name</strong>, `nvarchar(50)`, <strong>name*>*</strong>, in four places.  
  
    > [!NOTE]  
    >  Parameters require three elements: the name of the parameter that you want to replace, the data type of the parameter, and a default value for the parameter.  
  
12. Now the script should look like:  
  
    ```  
    USE AdventureWorks20012;  
    GO  
    IF EXISTS (  
    SELECT *   
       FROM INFORMATION_SCHEMA.ROUTINES   
       WHERE SPECIFIC_NAME = 'WorkOrdersFor<product_name, nvarchar(50), name>')  
       DROP PROCEDURE dbo.WorkOrdersFor<product_name, nvarchar(50), name>;  
    GO  
    CREATE PROCEDURE dbo.WorkOrdersFor<product_name, nvarchar(50), name>  
    AS  
    SELECT Name, WorkOrderID   
    FROM Production.WorkOrder AS WO  
    JOIN Production.Product AS Prod  
    ON WO.ProductID = Prod.ProductID  
    WHERE Name = '<product_name, nvarchar(50), name>';  
    GO  
    ```  
  
13. On the **File** menu, click **Save WorkOrdersProc.sql** to save your template.  
  
#### To test the custom template  
  
1.  In Template Explorer, expand **Stored Procedure**, expand **Custom**, and then double-click **WorkOrderProc**.  
  
2.  In the **Connect to Database Engine** dialog box, complete the connection information and then click **Connect**. A new Query Editor window opens, containing the contents of the **WorkOrderProc** template.  
  
3.  On the **Query** menu, click **Specify Values for Template Parameters**.  
  
4.  In the **Replace Template Parameters** dialog box, for the `product_name` value, type **FreeWheel** (overwriting the default contents), and then click **OK** to close the **Replace Template Parameters** dialog box and modify the script in the Query Editor.  
  
5.  Press F5 to execute the query, creating the procedure.  
  
## Next Task in Lesson  
 [Save Scripts as Projects or Solutions](lesson-3-3-save-scripts-as-projects-or-solutions.md)  
  
  
