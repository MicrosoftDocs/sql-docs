---
title: "SET ANSI_PADDING (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 7f99607d-3273-46f5-ac6f-9311a83cf48c
caps.latest.revision: 7
author: BarbKess
---
# SET ANSI_PADDING (SQL Server PDW)
Specifies the way SQL Server PDW stores column values shorter than the defined size of the column, and stores column values that have trailing spaces in **char**, **varchar**, **binary**, and **varbinary** data.  
  
In this release, **ANSI_PADDING** is always ON.  
  
For more information, see the [SET ANSI_PADDING (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms187403(v=sql11)) documentation on MSDN.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SET ANSI_PADDING ON;  
```  
  
## Permissions  
Requires membership in the **public** role.  
  
## General Remarks  
When ANSI_PADDING is ON, and a column value is inserted into a column that is shorter than the defined size:  
  
-   **char (n)** values are padded with trailing spaces.  
  
-   **binary (n)** values are padded with trailing zeros.  
  
-   **varchar (n)** values are not padded with trailing spaces.  
  
-   **varbinary (n)** values are not padded with trailing zeros.  
  
When a value is trimmed to fit into a column  
  
-   Trailing spaces are trimmed for **char (n)** columns.  
  
-   Trailing zeros are trimmed for **binary (n)** columns.  
  
-   Trailing spaces are not trimmed for **varchar (n)** columns.  
  
-   Trailing blanks are not trimmed for **varbinary (n)** columns.  
  
The SQL Server Native Client ODBC driver and SQL Server Native Client OLE DB Provider for SQL Server automatically set ANSI_PADDING to ON when connecting. This can be configured in ODBC data sources, in ODBC connection attributes, or OLE DB connection properties set in the application before connecting.  
  
The SET ANSI_PADDING setting does not affect columns of type **nchar** or **nvarchar**. They always display the SET ANSI_PADDING ON behavior. This means trailing spaces and zeros are not trimmed.  
  
The setting of SET ANSI_PADDING is set at execute or run time and not at parse time.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
