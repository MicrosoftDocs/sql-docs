---
title: "New-PowerPivotSystemServiceInstance cmdlet | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 7ea94113-c0f1-4cca-9228-f1a034fba5db
caps.latest.revision: 10
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# New-PowerPivotSystemServiceInstance cmdlet

[!INCLUDE[ssas-appliesto-sqlas-all](../../includes/ssas-appliesto-sqlas-all.md)]

  Adds a new instance of [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service to an application server.  

  >[!NOTE] This article may contain outdated information and examples.  
>
  
 **Applies To:** SharePoint 2010 and SharePoint 2013.  
  
## Syntax  
  
```  
New-PowerPivotSystemServiceInstance [[-ParentService] <PowerPivotMidTierServicePipeBind>] [-SystemServiceInstanceName <string>] [-Provision] [<CommonParameters>]  
```  
  
## Description  
 The New-PowerPivotSystemServiceInstance cmdlet provisions a new PowerPivotSystemService object at the farm-level after you have used SQL Server Setup to install [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint on the local application server. You can only provision one service instance on each application server.  If the service is already provisioned, you cannot run this cmdlet.  
  
## Parameters  
  
### -ParentService \<PowerPivotMidTierServicePipeBind>  
 Specifies the GUID of the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service parent object in the farm. In this release, there is only one parent object allowed. You can use Get-PowerPivotSystemService to return the service object or its GUID.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|0|  
|Default value||  
|Accept pipeline input?|true|  
|Accept wildcard characters?|false|  
  
### -SystemServiceInstanceName \<string>  
 Specifies a name that identifies this object.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|1|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### Provision [\<SwitchParameter>]  
 Makes the service available on SharePoint. Valid values are $true or $false.  
  
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
C:\PS>New-PowerPivotSystemServiceInstance -Provision:$true  
```  
  
 This example shows the most common form of the cmdlet. It registers the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service on the local application server with the farm.  
  
## Example 2  
  
```  
C:\PS>New-PowerPivotSystemServiceInstance -SystemServiceInstanceName "MyPSSInstance" -provision:$false  
```  
  
 This example names the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service instance, but without provisioning it. If you do not provide a name, the default name, SQL Server Analysis Services System Service Instance, is used instead. Creating a custom name for the service is optional. You might name the service to support test scenarios, or if you have a custom tool or script that provisions the instance in a later step.  
  
  