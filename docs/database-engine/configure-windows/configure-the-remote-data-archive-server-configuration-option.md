---
title: "Configure the remote data archive Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
ms.assetid: b5817b5a-f39a-4faf-b11e-a47b54fd9f32
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Configure the remote data archive Server Configuration Option
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  Use the **remote data archive** option to specify whether databases and tables on the server can be enabled for Stretch. For more info, see [Enable Stretch Database for a database](../../sql-server/stretch-database/enable-stretch-database-for-a-database.md).  
  
 The **remote data archive** option can have the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|0|Databases and tables on the server cannot be enabled for Stretch.|  
|1|Databases and tables on the server can be enabled for Stretch.|  
  
 Running **sp_configure** to set the value of the **remote data archive** option requires sysadmin or serveradmin permissions.  
  
## Example  
 The following example first displays the current setting of the **remote data archive** option. Then the example enables the **remote data archive** option by setting its value to 1.  
  
```  
EXEC sp_configure 'remote data archive';  
GO  
EXEC sp_configure 'remote data archive' , '1';  
GO  
RECONFIGURE;  
GO  
```  
  
 To disable the option, set the value to 0.  
  
## See Also  
 [Enable Stretch Database for a database](../../sql-server/stretch-database/enable-stretch-database-for-a-database.md)   
 [Stretch Database](../../sql-server/stretch-database/stretch-database.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)  
  
  
