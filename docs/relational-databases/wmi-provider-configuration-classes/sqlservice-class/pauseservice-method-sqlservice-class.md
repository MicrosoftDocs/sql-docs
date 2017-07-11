---
title: "PauseService Method (SqlService Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "PauseService Method (SqlService Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "PauseService method"
ms.assetid: 5c3a8feb-58b8-4385-b4c8-bf33cf4d276d
caps.latest.revision: 34
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# PauseService Method (SqlService Class)
  Attempts to place the service in the paused state.  
  
## Syntax  
  
```  
  
object.PauseService()  
```  
  
## Parts  
 *object*  
 A [SqlService Class](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/sqlservice-class.md) object that represents the service.  
  
## Property Value/Return Value  
 A uint32 value, which is 0 if the service was successfully stopped, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Starting and Stopping Services](http://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  