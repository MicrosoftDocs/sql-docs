---
title: "GeneralFlags Property (ServerSettings Class) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: wmi
ms.topic: "reference"
apiname: 
  - "GeneralFlags Property (ServerSettings Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "GeneralFlags property"
ms.assetid: 129bff8d-d2bc-4297-952f-d0a919d169f7
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
---
# GeneralFlags Property (ServerSettings Class)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]
  Gets the general flags associated with the instance of [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## Syntax  
  
```  
  
object.GeneralFlags [= value]  
```  
  
## Parts  
 *object*  
 A [ServerSettings Class](../../../relational-databases/wmi-provider-configuration-classes/serversettings-class/serversettings-class.md) object that represents server settings on an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## Property Value/Return Value  
 An array of [ServerSettingsGeneralFlag Class](../../../relational-databases/wmi-provider-configuration-classes/serversettingsgeneralflag-class/serversettingsgeneralflag-class.md) objects that specifies the general flags associated with the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].  
  
## Remarks  
  
## See Also  
 [Configuring Server Network Protocols and Net-Libraries](https://msdn.microsoft.com/library/ms177485\(v=sql.100\).aspx)  
  
  
