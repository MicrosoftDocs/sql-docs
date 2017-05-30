---
title: "Update-PowerPivotSystemService cmdlet | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: a90f1158-68d3-4330-98c1-fb0f81e13328
caps.latest.revision: 8
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Update-PowerPivotSystemService cmdlet

[!INCLUDE[ssas-appliesto-sqlas-all](../includes/ssas-appliesto-sqlas-all.md)]

  Upgrades the parent object of the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service in the farm.  
  
 **Applies To:** SharePoint 2010 and SharePoint 2013.  
  
## Syntax  
  
```  
Update-PowerPivotSystemService [-Confirm <switch>] [<CommonParameters>]  
```  
  
## Description  
 The **Update-PowerPivotSystemService** cmdlet runs a series of upgrade actions on the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service parent object, instances, and [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service applications in the farm. All of the middle-tier services and applications in a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint deployment must run at the same functional level. This cmdlet runs the upgrade actions on all of these objects.  
  
 Run this cmdlet after you have run SQL Server Setup to install a newer version of [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint or if you have applied a cumulative update on the server. To check whether upgrade is required, run `Get-PowerPivotSystemService` to review the **NeedsUpgrade** property. If **NeedsUpgrade** is true, you should run the cmdlet to upgrade the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] middle-tier objects in the farm.  
  
 Because a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint deployment includes middle-tier and backend data services, you must run **Update-PowerPivotEngineService** whenever you run **Update-PowerPivotSystemService** to ensure that both tiers are the same version across the farm.  
  
 Upgrade cannot be rolled back to the previous version. If you must revert to a previous version, remove [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] from your SharePoint farm and reinstall the software. To verify the upgrade operation succeeded, run `Get-PowerPivotSystemService` to review the global properties for version information and to verify that **NeedsUpgrade** is no longer set to true.  
  
## Parameters  
  
### -Confirm \<switch>  
 Prompts you for confirmation before executing the command. This value is enabled by default. To bypass the confirmation response in a command, specify Confirm:$false on the command.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### \<CommonParameters>  
 This cmdlet supports the following parameters:  
  
-   Verbose  
  
-   Debug  
  
-   ErrorAction  
  
-   ErrorVariable  
  
-   WarningAction  
  
-   WarningVariable  
  
-   OutBuffer  
  
-   OutVariable  
  
 For more information, see [About_CommonParameters](http://go.microsoft.com/fwlink/?linkID=227825).  
  
## Inputs and Outputs  
 The input type is the type of the objects that you can pipe to the cmdlet. The return type is the type of the objects that the cmdlet returns.  
  
|||  
|-|-|  
|Inputs|None.|  
|Outputs|None.|  
  
  