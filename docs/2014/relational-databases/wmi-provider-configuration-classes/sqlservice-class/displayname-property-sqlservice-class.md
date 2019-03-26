---
title: "DisplayName Property (SqlService Class) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
api_name: 
  - "DisplayName Property (SqlService Class)"
api_location: 
  - "sqlmgmproviderxpsp2up.mof"
topic_type: 
  - "apiref"
helpviewer_keywords: 
  - "DisplayName property"
ms.assetid: 49c408aa-6eb4-45cd-8d5c-60f065f24f5c
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# DisplayName Property (SqlService Class)
  Gets the display name of the service.  
  
## Syntax  
  
```  
  
object  
.DisplayName [= value]  
```  
  
## Parts  
 *object*  
 A [SqlService Class](sqlservice-class.md) object that represents the service.  
  
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
  
  
