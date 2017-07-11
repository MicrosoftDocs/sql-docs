---
title: "SqlServiceType Property (SqlService Class) | Microsoft Docs"
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
  - "SqlServiceType Property (SqlService Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "SqlServiceType property"
ms.assetid: dbff2968-3011-41d6-a141-52d814af0213
caps.latest.revision: 43
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# SqlServiceType Property (SqlService Class)
  Gets the type of the managed service.  
  
## Syntax  
  
```  
  
object.SqlServiceType [= value]  
```  
  
## Parts  
 *object*  
 A [SqlService Class](../../../relational-databases/wmi-provider-configuration-classes/sqlservice-class/sqlservice-class.md) object that represents the service.  
  
## Property Value/Return Value  
 A uint32 value that specifies the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service type.  
  
## Remarks  
 Return values can be one of the following:  
  
|Type|Definition|  
|----------|----------------|  
|*1*|MSSQLSERVER is the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service.|  
|*2*|SQLSERVERAGENT is the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Agent service.|  
|*3*|MSFTESQL is the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Full-text Search Engine service.|  
|*4*|MsDtsServer is the [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] service.|  
|*5*|MSSQLServerOLAPService is the [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] service.|  
|*6*|ReportServer is the [!INCLUDE[ssRSnoversion](../../../includes/ssrsnoversion-md.md)] service.|  
|*7*|SQLBrowser is the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Browser service.|  
  
## See Also  
 [Starting and Stopping Services](http://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  