---
title: "Dependencies Property (SqlService)"
description: "Dependencies Property (SqlService Class)"
author: markingmyname
ms.author: maghan
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "Dependencies property"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "Dependencies Property (SqlService Class)"
apitype: "MOFDef"
---
# Dependencies Property (SqlService Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Gets a list of services that are dependent on the referenced service.  
  
## Syntax  
  
```  
  
object.Dependencies [= value]  
```  
  
## Parts  
 *object*  
 A [SqlService Class](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/sqlservice-class.md) object that represents the service.  
  
## Property Value/Return Value  
 A string[] array that contains a list of services dependent on the referenced service.  
  
## Remarks  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
