---
title: sqlcmd utility - Run Transact-SQL script files
description: Learn how to use sqlcmd to run a Transact-SQL script file. It can contain Transact-SQL statements, sqlcmd commands, and scripting variables.
author: dlevy-msft
ms.author: dlevy
ms.reviewer: maghan, randolphwest
ms.date: 08/15/2023
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords:
  - "transact sql scripts"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sqlcmd - Run Transact-SQL script files

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Use **sqlcmd** to run a Transact-SQL script file. A Transact-SQL script file is a text file that can contain a combination of Transact-SQL statements, **sqlcmd** commands, and scripting variables.

## Create a script file

To create a Transact-SQL script file by using Notepad, follow these steps:

1. Select **Start**, point to **All Programs**, point to **Accessories**, and then select **Notepad**.

1. Copy and paste the following Transact-SQL code into Notepad:

   ```sql
   USE AdventureWorks2022;
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

1. Save the file as **myScript.sql** in the C drive.

## Run the script file

1. Open a command prompt window.

1. In the Command Prompt window, type: **sqlcmd -S myServer\instanceName -i C:\myScript.sql**

1. Press ENTER.

A list of [!INCLUDE [ssSampleDBCoShort](../../includes/sssampledbcoshort-md.md)] employee names and addresses is written to the command prompt window.

## Save the output to a text file

1. Open a command prompt window.

1. In the Command Prompt window, type: **sqlcmd -S myServer\instanceName -i C:\myScript.sql -o C:\EmpAdds.txt**

1. Press ENTER.

No output is returned in the Command Prompt window. Instead, the output is sent to the EmpAdds.txt file. You can verify this output by opening the EmpAdds.txt file.

## Output to Command Prompt window

1. Open a command prompt window.
2. In the Command Prompt window, type: `sqlcmd -S myServer\instanceName -U system_administrator_username -P password -i C:\myScript.sql -e`
3. Press ENTER.


## Next steps

- [Start the sqlcmd Utility](sqlcmd-start-utility.md)
- [sqlcmd utility](sqlcmd-utility.md)
