---
description: "Dependencies Property (SqlService Class)"
title: "Dependencies Property (SqlService)"
ms.custom: seo-lt-2019
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
apiname: 
  - "Dependencies Property (SqlService Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "Dependencies property"
ms.assetid: 92d54b7e-de2f-4978-b601-0196e37cbb41
author: markingmyname
ms.author: maghan
---
# Dependencies Property (SqlService Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Gets a list of services that are dependent on the referenced service.  
  
## Syntax  
  
```  
  
object.Dependencies [= value]  
```  
  
## Parts  
 *object*  
 A [SqlService Class](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/sqlservice-class.md) object that represents the service.  
  
## Property Value/Return Value  
 A string[] array that contains a list of services dependent on the referenced service.  
  
## Remarks  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
