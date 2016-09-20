---
title: "sys.numbered_procedures (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 82705b06-2904-4d66-a9c7-39b8fc434b20
caps.latest.revision: 4
author: BarbKess
---
# sys.numbered_procedures (SQL Server PDW)
Contains a row for each SQL Server stored procedure that was created as a numbered procedure. This does not show a row for the base (number = 1) stored procedure. Entries for the base stored procedures can be found in views such as **sys.objects** and **sys.procedures**.  
  
> [!IMPORTANT]  
> Numbered procedures are deprecated. Use of numbered procedures is discouraged. A DEPRECATION_ANNOUNCEMENT event is fired when a query that uses this catalog view is compiled.  
  
|Column name|Data type|Description|  
|---------------|-------------|---------------|  
|**object_id**|**int**|ID of the object of the stored procedure.|  
|**procedure_number**|**smallint**|Number of this procedure within the object, 2 or greater.|  
|**definition**|**nvarchar(max)**|The SQL Server text that defines this procedure.<br /><br />NULL = encrypted.|  
  
> [!NOTE]  
> XML and CLR parameters are not supported for numbered procedures.  
  
## Permissions  
The visibility of the metadata in catalog views is limited to securables that a user either owns or on which the user has been granted some permission. For more information, see [Metadata Visibility Configuration](http://msdn.microsoft.com/en-us/library/ms187113.aspx).  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[System Views &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/system-views-sql-server-pdw.md)  
  
