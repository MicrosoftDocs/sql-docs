---
title: "AcceptStop Property (SqlService Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
apiname: 
  - "AcceptStop Property (SqlService Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
helpviewer_keywords: 
  - "AcceptStop property"
ms.assetid: bf8ffe79-4f4c-4a2d-82e5-2ae8f5d466c5
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# AcceptStop Property (SqlService Class)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  Gets the Boolean property value that specifies whether the service can be stopped.  
  
## Syntax  
  
```  
  
object.AcceptStop [= value]  
```  
  
## Parts  
 *object*  
 A [SqlService Class](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/sqlservice-class.md) object that represents the service  
  
## Property Value/Return Value  
 A Boolean value that specifies whether the service can be stopped: **true** if the service can be stopped, or **false** if the service cannot be stopped.  
  
## Remarks  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
