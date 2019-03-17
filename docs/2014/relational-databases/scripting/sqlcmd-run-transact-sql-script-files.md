---
title: "Run Transact-SQL Script Files Using sqlcmd | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "transact sql scripts"
ms.assetid: 90067eb8-ca3e-44e8-bb1a-bf7d1a359423
author: MightyPen
ms.author: genemi
manager: craigg
---
# Run Transact-SQL Script Files Using sqlcmd
  You can use `sqlcmd` to run a [!INCLUDE[tsql](../../includes/tsql-md.md)] script file. A [!INCLUDE[tsql](../../includes/tsql-md.md)] script file is a text file that can contain a combination of [!INCLUDE[tsql](../../includes/tsql-md.md)] statements, `sqlcmd` commands, and scripting variables.  
  
 To create a simple [!INCLUDE[tsql](../../includes/tsql-md.md)] script file by using Notepad, follow these steps:  
  
1.  Click **Start**, point to **All Programs**, point to **Accessories**, and then click **Notepad**.  
  
2.  Copy and paste the following [!INCLUDE[tsql](../../includes/tsql-md.md)] code into Notepad:  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    SELECT p.FirstName + ' ' + p.LastName AS 'Employee Name',  
    a.AddressLine1, a.AddressLine2 , a.City, a.PostalCode   
    FROM Person.Person AS p   
       INNER JOIN HumanResources.Employee AS e   
            ON p.BusinessEntityID = e.BusinessEntityID  
        INNER JOIN Person.BusinessEntityAddress bea   
            ON bea.BusinessEntityID = e.BusinessEntityID  
        INNER JOIN Person.Address AS a   
            ON a.AddressID = bea.AddressID;  
    GO  
    ```  
  
3.  Save the file as **myScript.sql** in the C drive.  
  
### To run the script file  
  
1.  Open a command prompt window.  
  
2.  In the Command Prompt window, type: `sqlcmd -S myServer\instanceName -i C:\myScript.sql`  
  
3.  Press ENTER.  
  
 A list of [!INCLUDE[ssSampleDBCoShort](../../includes/sssampledbcoshort-md.md)] employee names and addresses is written to the command prompt window.  
  
### To save this output to a text file  
  
1.  Open a command prompt window.  
  
2.  In the Command Prompt window, type: `sqlcmd -S myServer\instanceName -i C:\myScript.sql -o C:\EmpAdds.txt`  
  
3.  Press ENTER.  
  
 No output is returned in the Command Prompt window. Instead, the output is sent to the EmpAdds.txt file. You can verify this output by opening the EmpAdds.txt file.  
  
## See Also  
 [Start the sqlcmd Utility](sqlcmd-start-the-utility.md)   
 [sqlcmd Utility](../../tools/sqlcmd-utility.md)  
  
  
