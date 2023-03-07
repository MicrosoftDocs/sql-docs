---
title: "AcceptPause Property (SqlService)"
description: "AcceptPause Property (SqlService Class)"
author: markingmyname
ms.author: maghan
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "AcceptPause property"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "AcceptPause Property (SqlService Class)"
---
# AcceptPause Property (SqlService Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Gets the Boolean property value that specifies whether the service can be paused.  
  
## Syntax  
  
```  
  
object.AcceptPause [= value]  
```  
  
## Parts  
 *object*  
 A [SqlService Class](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/sqlservice-class.md) object that represents the service.  
  
## Property Value/Return Value  
 A Boolean value that specifies whether the service can be paused. **true** if the service can be paused; **false** if the service cannot be paused.  
  
## Remarks  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
