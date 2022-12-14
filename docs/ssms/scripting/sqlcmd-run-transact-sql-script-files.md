---
title: "Run Transact-SQL Script Files Using sqlcmd"
description: Learn how to use sqlcmd to run a Transact-SQL script file. It can contain Transact-SQL statements, sqlcmd commands, and scripting variables.
ms.custom: seo-lt-2019
ms.date: "07/15/2016"
ms.service: sql
ms.subservice: ssms
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "transact sql scripts"
ms.assetid: 90067eb8-ca3e-44e8-bb1a-bf7d1a359423
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sqlcmd - Run Transact-SQL Script Files
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
 Use **sqlcmd** to run a Transact-SQL script file. A Transact-SQL script file is a text file that can contain a combination of Transact-SQL statements, **sqlcmd** commands, and scripting variables.  

## Create a script file  
 To create a simple Transact-SQL script file by using Notepad, follow these steps:  
  
1.  Click **Start**, point to **All Programs**, point to **Accessories**, and then click **Notepad**.  
  
2.  Copy and paste the following Transact-SQL code into Notepad:  
  
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
  
## Run the script file  
  
1.  Open a command prompt window.  
  
2.  In the Command Prompt window, type: **sqlcmd -S myServer\instanceName -i C:\myScript.sql**  
  
3.  Press ENTER.  
  
 A list of [!INCLUDE[ssSampleDBCoShort](../../includes/sssampledbcoshort-md.md)] employee names and addresses is written to the command prompt window.  

## Save the output to a text file
  
1.  Open a command prompt window.  
  
2.  In the Command Prompt window, type: **sqlcmd -S myServer\instanceName -i C:\myScript.sql -o C:\EmpAdds.txt**  
  
3.  Press ENTER.  
  
 No output is returned in the Command Prompt window. Instead, the output is sent to the EmpAdds.txt file. You can verify this output by opening the EmpAdds.txt file.  
  
## See Also  
 [Start the sqlcmd Utility](./sqlcmd-start-the-utility.md)   
 [sqlcmd Utility](../../tools/sqlcmd-utility.md)  
  
