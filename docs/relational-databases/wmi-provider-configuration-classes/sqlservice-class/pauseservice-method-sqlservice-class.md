---
description: "PauseService Method (SqlService Class)"
title: "PauseService Method (SqlService)"
ms.custom: seo-lt-2019
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
apiname: 
  - "PauseService Method (SqlService Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "PauseService method"
ms.assetid: 5c3a8feb-58b8-4385-b4c8-bf33cf4d276d
author: markingmyname
ms.author: maghan
---
# PauseService Method (SqlService Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
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
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
