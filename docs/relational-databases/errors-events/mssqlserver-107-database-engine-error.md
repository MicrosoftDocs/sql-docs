---
title: "MSSQLSERVER_107"
description: "MSSQLSERVER_107"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "107 (Database Engine error)"
---
# MSSQLSERVER_107
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|107|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|P_NOCORRMATCH|  
|Message Text|The column prefix '%.*ls' does not match with a table name or alias name used in the query.|  
  
## Explanation  
The select list of the query contains an asterisk (*) that is incorrectly qualified with a column prefix. This error can be returned under the following conditions:  
  
-   The column prefix does not correspond to any table or alias name used in the query. For example, the following statement uses an alias name (`T1`) as a column prefix, but the alias is not defined in the FROM clause.  
  
    ```  
    SELECT T1.* FROM dbo.ErrorLog;  
    ```  
  
-   A table name is specified as a column prefix when an alias name for the table is supplied in the FROM clause. For example, the following statement uses the table name `ErrorLog` as the column prefix; however, the table has an alias (`T1`) defined in the FROM clause.  
  
    ```  
    SELECT ErrorLog.* FROM dbo.ErrorLog AS T1;  
    ```  
  
    If an alias has been provided for a table name in the FROM clause, you can only use the alias to prefix columns from the table.  
  
## User Action  
Match the column prefixes against the table names or alias names specified in the FROM clause of the query. For example, the statements above can be corrected as follows:  
  
```  
SELECT T1.* FROM dbo.ErrorLog AS T1;  
```  
  
or  
  
```  
SELECT ErrorLog.* FROM dbo.ErrorLog;  
```  
  
## See Also  
[MSSQLSERVER_4104](~/relational-databases/errors-events/mssqlserver-4104-database-engine-error.md)  
  
