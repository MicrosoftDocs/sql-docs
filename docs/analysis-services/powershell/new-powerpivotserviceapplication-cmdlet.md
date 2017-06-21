---
title: "New-PowerPivotServiceApplication cmdlet | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 7bb2a2d2-04c8-43d4-a0fc-e8339ea22138
caps.latest.revision: 10
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# New-PowerPivotServiceApplication cmdlet

[!INCLUDE[ssas-appliesto-sqlas-all](../../includes/ssas-appliesto-sqlas-all.md)]

  Creates a new [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application.  

  >[!NOTE] This article may contain outdated information and examples.  
>
  
 **Applies To:** SharePoint 2010 and SharePoint 2013.  
  
## Syntax  
  
```  
New-PowerPivotServiceApplication [-ServiceApplicationName] <string> [-DatabaseServerName] <string> [-DatabaseName] <string> [-AddToDefaultProxyGroup <switch>] [<CommonParameters>]  
```  
  
## Description  
 The New-PowerPivotServiceApplication cmdlet creates a new [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application in the farm. You must define at least one service application, and it must be a member of the default proxy service group. Optionally, you can create additional service applications if you need to vary properties or configuration settings. Additional service applications must be assigned membership to custom service connection groups. Only one [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application can be a member of the default proxy group.  
  
 A new service application is created using a default configuration. To customize the configuration properties, use the Set-PowerPivotServiceApplication cmdlet.  
  
## Parameters  
  
### -ServiceApplicationName \<string>  
 Sets the display name of the service application.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|0|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -DatabaseServerName \<string>  
 Specifies a SQL Server relational database engine instance that hosts the application database. By default, you can use the farm's database server, or you can choose another database server for which you have create database rights.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|1|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -DatabaseName \<string>  
 Specifies the name of a SQL Server relational database that stores application data. Be sure to specify a name that corresponds to the application so that you can more easily identify its purpose. You can create a new database or specify an existing [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application database for the new application you are creating.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|2|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -AddToDefaultProxyGroup \<switch>  
 Creates a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service connection in the default service connection group. Associations between web applications and service applications are determined by membership in this group. All web applications that subscribe to the default service connection group can use the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application that you add to the group. Although you can have multiple [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service applications in a farm, only one service application can be a member of the default service connection group.  
  
 If you already have one [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application that is a member of the default proxy group, you must set AddToDefautlProxyGroup:$false for the new application you are creating. You will need to add the new service application to a custom service connection group.  You can use built-in SharePoint cmdlets for this purpose.  Get-SPServiceApplicationProxyGroup returns the list of service connection groups that are defined in the farm.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### \<CommonParameters>  
 This cmdlet supports the common parameters: Verbose, Debug, ErrorAction, ErrorVariable, WarningAction, WarningVariable,OutBuffer and OutVariable. For more information, see [About_CommonParameters](http://go.microsoft.com/fwlink/?linkID=227825).  
  
## Inputs and Outputs  
 The input type is the type of the objects that you can pipe to the cmdlet. The return type is the type of the objects that the cmdlet returns.  
  
|||  
|-|-|  
|Inputs|None.|  
|Outputs|None.|  
  
## Example 1  
  
```  
C:\PS>New-PowerPivotServiceApplication -ServiceApplicationName "PowerPivot Service Application" -DatabaseServerName "AdvWorks-SRV01\PowerPivot" -DatabaseName "PowerPivotServiceApp1" -AddtoDefaultProxyGroup:$true  
```  
  
 This example creates a new service application. The service application database is created on a database server named AdvWorks-SRV01 that was installed as a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] named instance, a common configuration for many [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint installations. You must have dbcreator permissions on the SQL Server instance to create the database. You must be db_owner on the SharePoint configuration database. Because this is the first [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application in the farm, it must be a member of the default proxy group.  
  
  