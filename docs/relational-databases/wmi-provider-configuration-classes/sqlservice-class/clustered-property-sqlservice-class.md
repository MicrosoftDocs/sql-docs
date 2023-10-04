---
title: "Clustered Property (SqlService)"
description: "Clustered Property (SqlService Class)"
author: markingmyname
ms.author: maghan
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "Clustered property"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "Clustered Property (SqlService Class)"
---
# Clustered Property (SqlService Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Gets the Boolean property value that specifies whether the service is part of a clustered instance.  
  
## Syntax  
  
```  
  
object.Clustered [= value]  
```  
  
## Parts  
 *object*  
 A [SqlService Class](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/sqlservice-class.md) object that represents the service.  
  
## Property Value/Return Value  
 A Boolean value that specifies whether the service is participating in a clustered instance: **true** if the service is participating in a clustered instance, or **false** if the service is not participating in a clustered instance.  
  
## Remarks  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
