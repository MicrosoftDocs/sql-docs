---
title: "Get-PowerPivotSystemService cmdlet | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 33231250-3880-4d75-936b-d70662a01855
caps.latest.revision: 9
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Get-PowerPivotSystemService cmdlet

[!INCLUDE[ssas-appliesto-sqlas-all](../../includes/ssas-appliesto-sqlas-all.md)]

  Returns the global properties of the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service object in a farm. 

>[!NOTE] 
>This article may contain outdated information and examples.
  
 **Applies To:** SharePoint 2010 and SharePoint 2013.  
  
## Syntax  
  
```  
Get-PowerPivotSystemService [-Identity <PowerPivotMidTierServicePipeBind>] [<CommonParameters>]  
```  
  
## Description  
 The Get-PowerPivotSystemService cmdlet returns the global properties of the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service object. There is one parent object per farm, but each farm can have multiple instances of the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service that run on individual application servers in the farm. The parent object shows farm-level settings that do not vary by instance. If the farm includes multiple [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint installations, a comma-delimited list of the instances will indicate how many [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service instances are in the farm.  
  
## Parameters  
  
### -Identity \<PowerPivotMidTierServicePipeBind>  
 Specifies the parent object to get. The value must be a valid GUID that uniquely identifies the object in the farm.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|0|  
|Default value||  
|Accept pipeline input?|true|  
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
C:\PS>Get-PowerPivotSystemService  
```  
  
 This example returns the global properties of the parent object, showing properties that are shared by all instances of [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service in the farm.  
  
  