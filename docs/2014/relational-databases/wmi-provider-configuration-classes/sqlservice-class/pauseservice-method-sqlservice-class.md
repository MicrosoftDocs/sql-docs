---
title: "PauseService Method (SqlService Class) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
api_name: 
  - "PauseService Method (SqlService Class)"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "PauseService method"
ms.assetid: 5c3a8feb-58b8-4385-b4c8-bf33cf4d276d
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# PauseService Method (SqlService Class)
  Attempts to place the service in the paused state.  
  
## Syntax  
  
```  
  
object  
.PauseService()  
  
```  
  
## Parts  
 *object*  
 A [SqlService Class](sqlservice-class.md) object that represents the service.  
  
## Property Value/Return Value  
 A uint32 value, which is 0 if the service was successfully stopped, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
