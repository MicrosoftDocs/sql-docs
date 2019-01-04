---
title: "State Property (SqlService Class) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
api_name: 
  - "State Property (SqlService Class)"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "State property"
ms.assetid: 9e09f419-947c-4d4b-9a49-2d3396c847cd
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# State Property (SqlService Class)
  Gets or sets the current state of the service.  
  
## Syntax  
  
```  
  
object  
.State [= value]  
```  
  
## Parts  
 *object*  
 A [SqlService Class](sqlservice-class.md) object that represents the service.  
  
## Property Value/Return Value  
 A uint32 value that specifies the state of the service.  
  
 Values can be one of the following.  
  
 1  
 Stopped. The service is stopped.  
  
 2  
 Start Pending. The service is waiting to start.  
  
 3  
 Stop Pending. The service is waiting to stop.  
  
 4  
 Running. The service is running.  
  
 5  
 Continue Pending. The service is waiting to continue.  
  
 6  
 Pause Pending. The service is waiting to pause.  
  
 7  
 Paused. The service is paused.  
  
## Remarks  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
