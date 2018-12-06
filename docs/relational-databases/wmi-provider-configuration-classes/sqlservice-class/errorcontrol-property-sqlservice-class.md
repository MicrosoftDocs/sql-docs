---
title: "ErrorControl Property (SqlService Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
apiname: 
  - "ErrorControl Property (SqlService Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "ErrorControl property"
ms.assetid: cbb1e0fa-5bfc-4b1b-a6ed-f7d5cfad4d73
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# ErrorControl Property (SqlService Class)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  Gets or sets the severity of the error if the service fails to start during startup.  
  
## Syntax  
  
```  
  
object.ErrorControl [= value]  
```  
  
## Parts  
 *object*  
 A [SqlService Class](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/sqlservice-class.md) object that represents the service.  
  
## Property Value/Return Value  
 A string value that specifies the error severity reported if the service fails during startup. The following table shows the possible values.  
  
 Ignore  
 User is not notified.  
  
 Normal  
 User is notified.  
  
 Severe  
 System is restarted with the last-known-good configuration.  
  
 Critical  
 System attempts to restart with a good configuration.  
  
 Unknown  
 The severity is unknown.  
  
## Remarks  
 The value indicates the action taken by the startup program if a failure occurs. All errors are logged by the computer system.  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
