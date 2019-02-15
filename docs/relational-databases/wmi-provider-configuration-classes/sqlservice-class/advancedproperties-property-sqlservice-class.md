---
title: "AdvancedProperties Property (SqlService Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
apiname: 
  - "AdvancedProperties Property (SqlService Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
helpviewer_keywords: 
  - "AdvancedProperties property"
ms.assetid: 63bcb7e2-1f78-4961-b4b9-1b635a89079b
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# AdvancedProperties Property (SqlService Class)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  Gets an array of object references that contain the advanced properties for the **SqlService** object.  
  
## Syntax  
  
```  
  
object.AdvancedProperties [= value]  
```  
  
## Parts  
 *object*  
 A [SqlService Class](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/sqlservice-class.md) object that represents the service.  
  
## Property Value/Return Value  
 An array of [SqlServiceAdvancedProperty Class](../../../relational-databases/wmi-provider-configuration-classes/sqlserviceadvancedproperty-class/sqlserviceadvancedproperty-class.md) objects that contain the advanced properties for the **SqlService** object.  
  
## Remarks  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
