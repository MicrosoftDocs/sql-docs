---
title: "ConnectionString Property (SqlServerAlias Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
apiname: 
  - "ConnectionString Property (SqlServerAlias Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
helpviewer_keywords: 
  - "ConnectionString property"
ms.assetid: 8a3692b9-3a34-42e2-b0b9-28e6bd3a7aba
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# ConnectionString Property (SqlServerAlias Class)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  Gets the connection string that is used to establish the connection for the server connection alias.  
  
## Syntax  
  
```  
  
object.ConnectionString [= value]  
```  
  
## Parts  
 *object*  
 A [SqlServerAlias Class](../../../relational-databases/wmi-provider-configuration-classes/sqlserveralias-class/sqlserveralias-class.md) object that represents a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] alias.  
  
## Property Value/Return Value  
 A string that specifies the connection string that is used to establish the connection for the server connection alias.  
  
## Remarks  
  
