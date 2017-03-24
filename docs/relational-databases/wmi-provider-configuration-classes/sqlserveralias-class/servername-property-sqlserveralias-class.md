---
title: "ServerName Property (SqlServerAlias Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "reference"
apiname: 
  - "ServerName Property (SqlServerAlias Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "ServerName property"
ms.assetid: 58c82b19-b548-42fa-9c5a-059b606da097
caps.latest.revision: 30
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# ServerName Property (SqlServerAlias Class)
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
  