---
title: "SetBoolValue Method (SqlServiceAdvancedProperty Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine"
ms.service: ""
ms.component: "wmi"
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "SetBoolValue Method (SqlServiceAdvancedProperty Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "SetBoolValue method"
ms.assetid: 5252b439-fce5-446a-8e57-99e3054bee69
caps.latest.revision: 33
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
ms.workload: "Inactive"
---
# SetBoolValue Method (SqlServiceAdvancedProperty Class)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  Sets the Boolean value of a property.  
  
## Syntax  
  
```  
  
object.SetBoolValue [= value]  
```  
  
## Parts  
 *object*  
 A [SqlServiceAdvancedProperty Class](../../../relational-databases/wmi-provider-configuration-classes/sqlserviceadvancedproperty-class/sqlserviceadvancedproperty-class.md) object that represents an advanced property.  
  
#### Parameters  
  
|Parameter|Description|  
|---------------|-----------------|  
|*BoolValue*|A Boolean value that specifies the value of the advanced property.|  
  
## Property Value/Return Value  
 A **uint32** value, which is 0 if the service was successfully modified, 1 if the request is not supported, and any other number to indicate an error.  
  
## Remarks  
 The property value type must be Boolean to set the property to a Boolean value.  
  
## See Also  
 [Starting and Stopping Services](http://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
