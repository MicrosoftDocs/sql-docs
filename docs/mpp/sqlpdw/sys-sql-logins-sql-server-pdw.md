---
title: "sys.sql_logins (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: d28cc4b2-4d95-4a61-943b-e0e18ffc00e2
caps.latest.revision: 7
author: BarbKess
---
# sys.sql_logins (SQL Server PDW)
Returns one row for every SQL Server authentication login.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**<inherited columns>**|--|Inherits from **sys.server_principals**.|  
|**is_policy_checked**|**bit**|Password policy is checked.|  
|**is_expiration_checked**|**bit**|Password expiration is checked.|  
|**password_hash**|**varbinary(256)**|Hash of the SQL Server authentication login password.|  
  
For a list of columns that this view inherits, see [sys.server_principals &#40;SQL Server PDW&#41;](../sqlpdw/sys-server-principals-sql-server-pdw.md).  
  
## Permissions  
Any SQL Server authentication login can see their own login name. To see other logins, requires ALTER ANY LOGIN, or a permission on the login.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../sqlpdw/system-views-sql-server-pdw.md)  
[sys.server_principals &#40;SQL Server PDW&#41;](../sqlpdw/sys-server-principals-sql-server-pdw.md)  
[sys.database_principals &#40;SQL Server PDW&#41;](../sqlpdw/sys-database-principals-sql-server-pdw.md)  
  
