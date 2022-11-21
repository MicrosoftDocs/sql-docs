---
description: "PropertyIndex Property (SqlServiceAdvancedProperty Class)"
title: "PropertyIndex Property (SqlServiceAdvancedProperty)"
ms.custom: seo-lt-2019
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
apiname: 
  - "PropertyIndex Property (SqlServiceAdvancedProperty Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "PropertyIndex property"
ms.assetid: b18b45a2-e187-44f5-a8c9-26fd9828b6c6
author: markingmyname
ms.author: maghan
---
# PropertyIndex Property (SqlServiceAdvancedProperty Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Gets or sets the property index that specifies the position of an advanced property in an array of advanced properties that belong to a referenced service.  
  
## Syntax  
  
```  
  
object.PropertyIndex [= value]  
```  
  
## Parts  
 *object*  
 A [SqlServiceAdvancedProperty Class](../../../relational-databases/wmi-provider-configuration-classes/sqlserviceadvancedproperty-class/sqlserviceadvancedproperty-class.md) object that represents an advanced property.  
  
## Property Value/Return Value  
 A **uint32** value that specifies the position of the advanced property in the advanced property array that belongs to the referenced service.  
  
## Remarks  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
