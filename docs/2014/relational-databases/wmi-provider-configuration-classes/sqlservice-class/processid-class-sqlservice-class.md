---
title: "ProcessId Class (SqlService Class) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
api_name: 
  - "ProcessId Class (SqlService Class)"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "ProcessId property"
ms.assetid: 99b5a2e9-b44a-48a0-993e-04bd15c7fef4
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# ProcessId Class (SqlService Class)
  Gets the system process ID that uniquely identifies a service.  
  
## Syntax  
  
```  
  
object  
.ProcessId [= value]  
```  
  
## Parts  
 *object*  
 A [SqlService Class](sqlservice-class.md) object that represents the service.  
  
## Property Value/Return Value  
 A `uint32` value that specifies the ID that uniquely identifies the process.  
  
## Remarks  
  
## Example  
  
```  
mysqlservice.ProcessId = 324  
```  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
