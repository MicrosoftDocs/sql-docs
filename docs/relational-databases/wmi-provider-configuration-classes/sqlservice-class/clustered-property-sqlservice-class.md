---
description: "Clustered Property (SqlService Class)"
title: "Clustered Property (SqlService)"
ms.custom: seo-lt-2019
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
apiname: 
  - "Clustered Property (SqlService Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
helpviewer_keywords: 
  - "Clustered property"
ms.assetid: f714e7f5-c2db-45c6-9536-6ca2cb5b42aa
author: markingmyname
ms.author: maghan
---
# Clustered Property (SqlService Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Gets the Boolean property value that specifies whether the service is part of a clustered instance.  
  
## Syntax  
  
```  
  
object.Clustered [= value]  
```  
  
## Parts  
 *object*  
 A [SqlService Class](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/sqlservice-class.md) object that represents the service.  
  
## Property Value/Return Value  
 A Boolean value that specifies whether the service is participating in a clustered instance: **true** if the service is participating in a clustered instance, or **false** if the service is not participating in a clustered instance.  
  
## Remarks  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
