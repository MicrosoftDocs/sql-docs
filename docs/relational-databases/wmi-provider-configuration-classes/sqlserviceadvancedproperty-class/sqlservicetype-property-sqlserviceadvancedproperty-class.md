---
description: "SqlServiceType Property (SqlServiceAdvancedProperty Class)"
title: "SqlServiceType Property (SqlServiceAdvancedProperty)"
ms.custom: seo-lt-2019
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: wmi
ms.topic: "reference"
apiname: 
  - "SqlServiceType Property (SqlServiceAdvancedProperty Class)"
apilocation: 
  - "sqlmgmproviderxpsp2up.mof"
apitype: "MOFDef"
helpviewer_keywords: 
  - "SqlServiceType property"
ms.assetid: 20f1663a-9a14-4f14-8c1b-8aa133e272c3
author: markingmyname
ms.author: maghan
---
# SqlServiceType Property (SqlServiceAdvancedProperty Class)
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]
  Gets the type of the managed service associated with the advanced property.  
  
## Syntax  
  
```  
  
object.SetBoolValue(NumValue)  
```  
  
## Parts  
 *object*  
 A [SqlServiceAdvancedProperty Class](../../../relational-databases/wmi-provider-configuration-classes/sqlserviceadvancedproperty-class/sqlserviceadvancedproperty-class.md) object that represents an advanced property.  
  
## Property Value/Return Value  
 A uint32 value that specifies the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Service type.  
  
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
|*8*|NsService is the [!INCLUDE[ssNoVersion](../../../includes/ssns-md.md)] Notification service.|  
|*9*|MSSQLFDLauncher is the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Full-text Filter Daemon Launcher service.|  
|*10*|SQLPBENGINE is the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PolyBase Engine service.|  
|*11*|SQLPBDMS is the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] PolyBase Data Movement service.|  
|*12*|MSSQLLaunchpad is the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Launchpad service.|  
  
## See Also  
 [Starting and Stopping Services](https://technet.microsoft.com/library/ms174886\(v=sql.105\).aspx)  
  
  
