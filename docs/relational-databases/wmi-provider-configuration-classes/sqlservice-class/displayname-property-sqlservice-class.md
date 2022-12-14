---
description: "DisplayName Property (SqlService Class)"
title: "DisplayName Property (SqlService)"
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
apiname: 
  - "DisplayName Property (SqlService Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "DisplayName property"
ms.assetid: 49c408aa-6eb4-45cd-8d5c-60f065f24f5c
author: markingmyname
ms.author: maghan
---
# DisplayName Property (SqlService Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Gets the display name of the service.  
  
## Syntax  
  
```  
  
object.DisplayName [= value]  
```  
  
## Parts  
 *object*  
 A [SqlService Class](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/sqlservice-class.md) object that represents the service.  
  
## Property Value/Return Value  
 A string value that specifies the display name of the service.  
  
## Remarks  
 This string has a maximum length of 256 characters. The name is case-preserved in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager. However, display name comparisons are always case-insensitive.  
  
## Example  
  
```  
mysqlservice.DisplayName = "Atdisk"  
```  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
