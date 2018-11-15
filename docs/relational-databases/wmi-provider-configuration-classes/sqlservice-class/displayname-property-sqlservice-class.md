---
title: "DisplayName Property (SqlService Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
apiname: 
  - "DisplayName Property (SqlService Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "DisplayName property"
ms.assetid: 49c408aa-6eb4-45cd-8d5c-60f065f24f5c
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# DisplayName Property (SqlService Class)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
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
  
  
