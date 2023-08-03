---
title: "ProcessId Class (SqlService)"
description: "ProcessId Class (SqlService Class)"
author: markingmyname
ms.author: maghan
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: wmi
ms.topic: "reference"
helpviewer_keywords:
  - "ProcessId property"
apilocation: "sqlmgmproviderxpsp2up.mof"
apiname: "ProcessId Class (SqlService Class)"
apitype: "MOFDef"
---
# ProcessId Class (SqlService Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Gets the system process ID that uniquely identifies a service.  
  
## Syntax  
  
```  
  
object.ProcessId [= value]  
```  
  
## Parts  
 *object*  
 A [SqlService Class](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/sqlservice-class.md) object that represents the service.  
  
## Property Value/Return Value  
 A **uint32** value that specifies the ID that uniquely identifies the process.  
  
## Remarks  
  
## Example  
  
```  
mysqlservice.ProcessId = 324  
```  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
