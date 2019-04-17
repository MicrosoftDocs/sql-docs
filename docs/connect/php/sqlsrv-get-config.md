---
title: "sqlsrv_get_config | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "sqlsrv_get_config"
apitype: "NA"
helpviewer_keywords: 
  - "API Reference, sqlsrv_get_config"
  - "sqlsrv_get_config"
ms.assetid: ce2befc2-af98-45bb-8d41-60f1674dccfc
author: MightyPen
ms.author: genemi
manager: craigg
---
# sqlsrv_get_config
[!INCLUDE[Driver_PHP_Download](../../includes/driver_php_download.md)]

Returns the current value of the specified configuration setting.  
  
## Syntax  
  
```  
  
sqlsrv_get_config( string $setting )  
```  
  
#### Parameters  
*$setting*: The configuration setting for which the value is returned. For a list of configurable settings, see [sqlsrv_configure](../../connect/php/sqlsrv-configure.md).  
  
## Return Value  
The value of the setting specified by the *$setting* parameter. If an invalid setting is specified, **false** is returned and an error is added to the error collection.  
  
## Remarks  
If **false** is returned by **sqlsrv_get_config**, you must call [sqlsrv_errors](../../connect/php/sqlsrv-errors.md) to determine if an error occurred or if **false** is the value of the setting specified by the *$setting* parameter.  
  
## See Also  
[SQLSRV Driver API Reference](../../connect/php/sqlsrv-driver-api-reference.md)  

[sqlsrv_configure](../../connect/php/sqlsrv-configure.md)  

[sqlsrv_errors](../../connect/php/sqlsrv-errors.md)  
  
