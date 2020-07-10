---
title: "ServerName Property (SqlServerAlias)"
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
apiname: 
  - "ServerName Property (SqlServerAlias Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "ServerName property"
ms.assetid: 58c82b19-b548-42fa-9c5a-059b606da097
author: "CarlRabeler"
ms.author: "carlrab"
---
# ServerName Property (SqlServerAlias Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Gets the name of the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] specified by the server connection alias.  
  
## Syntax  
  
```  
  
object.ServerName [= value]  
```  
  
## Parts  
 *object*  
 A [SqlServerAlias Class](../../../relational-databases/wmi-provider-configuration-classes/sqlserveralias-class/sqlserveralias-class.md) object that represents a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] alias.  
  
## Property Value/Return Value  
 A string value that specifies the name of the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] referenced by the server connection alias.  
  
## Remarks  
  
