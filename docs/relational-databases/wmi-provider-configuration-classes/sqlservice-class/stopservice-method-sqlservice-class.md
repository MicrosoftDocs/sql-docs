---
description: "StopService Method (SqlService Class)"
title: "StopService Method (SqlService)"
ms.custom: seo-lt-2019
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
apiname: 
  - "StopService Method (SqlService Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "StopService method"
ms.assetid: ef8e1856-4930-417a-8f52-be470fd3f15c
author: markingmyname
ms.author: maghan
---
# StopService Method (SqlService Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Attempts to place the service in the stopped state.  
  
## Syntax  
  
```  
  
object.StopService()  
```  
  
## Parts  
 *object*  
 A [SqlService Class](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/sqlservice-class.md) object that represents the service.  
  
## Property Value/Return Value  
 A **uint32** value, which is 0 if the **ResumeService** request was accepted, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
