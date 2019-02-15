---
title: "AcceptStop Property (SqlService Class) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
api_name: 
  - "AcceptStop Property (SqlService Class)"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "AcceptStop property"
ms.assetid: bf8ffe79-4f4c-4a2d-82e5-2ae8f5d466c5
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# AcceptStop Property (SqlService Class)
  Gets the Boolean property value that specifies whether the service can be stopped.  
  
## Syntax  
  
```  
  
object  
.AcceptStop [= value]  
```  
  
## Parts  
 *object*  
 A [SqlService Class](sqlservice-class.md) object that represents the service  
  
## Property Value/Return Value  
 A Boolean value that specifies whether the service can be stopped: `true` if the service can be stopped, or `false` if the service cannot be stopped.  
  
## Remarks  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
