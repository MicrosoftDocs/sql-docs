---
title: "AcceptPause Property (SqlService Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
apiname: 
  - "AcceptPause Property (SqlService Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
helpviewer_keywords: 
  - "AcceptPause property"
ms.assetid: 4339e903-35ee-4395-b005-ca58b3a24a84
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# AcceptPause Property (SqlService Class)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  Gets the Boolean property value that specifies whether the service can be paused.  
  
## Syntax  
  
```  
  
object.AcceptPause [= value]  
```  
  
## Parts  
 *object*  
 A [SqlService Class](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/sqlservice-class.md) object that represents the service.  
  
## Property Value/Return Value  
 A Boolean value that specifies whether the service can be paused. **true** if the service can be paused; **false** if the service cannot be paused.  
  
## Remarks  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
