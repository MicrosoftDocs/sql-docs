---
title: "Adding Indentation | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
ms.assetid: 9dce05c1-c52f-455d-8b8d-6f303e242760
caps.latest.revision: 25
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Lesson 2-2 - Adding Indentation
Query Editor allows you to indent large sections of code with a single step, and you can change the amount of the indentation.  
  
## Indenting Code  
  
#### To indent multiple lines of code  
  
1.  On the toolbar, click **New Query**.  
  
2.  Create a second query that selects the **BusinessEntityID**, FirstName, **MiddleName**, and **LastName** columns from the **Person** table of the **Person** schema. Place each column on a separate line so the code looks like this:  
  
    ```  
    -- Search for a contact  
    SELECT   
    BusinessEntityID,  
    FirstName,   
    MiddleName,   
    LastName  
    FROM Person.Person  
    WHERE LastName = 'Sanchez';  
    GO  
    ```  
  
3.  Select all text from `BusinessEntityID` to `LastName`.  
  
4.  On the **SQL Editor** toolbar, click **Increase Indent** to indent all the lines at once.  
  
#### To change the default indentation  
  
1.  On the **Tools** menu, click **Options**.  
  
2.  Expand **Text Editor**, expand **All Languages**, and click **Tabs** , and set indentation values as appropriate. Note that you can change the size of the indent as well as the size of tabs, and whether tabs are converted to spaces.  
  
    ![Appearance of the Tabs dialog box](./media/lesson-2-2-adding-indentation/tabsdialog.gif "Appearance of the Tabs dialog box")  
  
3.  Click **OK**.  
  
## Next Task in Lesson  
[Maximizing Query Editor](../../tools/sql-server-management-studio/lesson-2-3-maximizing-query-editor.md)  
  
  
  
