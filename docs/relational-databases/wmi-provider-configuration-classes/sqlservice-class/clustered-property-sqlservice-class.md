---
title: "Clustered Property (SqlService Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "Clustered Property (SqlService Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
helpviewer_keywords: 
  - "Clustered property"
ms.assetid: f714e7f5-c2db-45c6-9536-6ca2cb5b42aa
caps.latest.revision: 35
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# Clustered Property (SqlService Class)
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
 [Starting and Stopping Services](http://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  