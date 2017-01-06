---
title: "Grant Permissions to Copy Data Off the Appliance (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 9ec15e16-e83c-4b48-8c3a-872cc856a175
caps.latest.revision: 12
author: BarbKess
---
# Grant Permissions to Copy Data Off the Appliance (SQL Server PDW)
This topic describes how to grant permissions to a user or database role to copy data off the SQL Server PDW appliance.  
  
## <a name="PermsAdminConsole"></a>Grant Permissions to Copy Data Off the Appliance  
To move data to another location requires **SELECT** permission on the table containing the data to be moved.  
  
If the destination for the data is another SQL Server PDW, the user must have **CREATE TABLE** permission at the destination and **ALTER SCHEMA** permission on the schema that will contain the table.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
