---
title: "Remove-PowerPivotSystemServiceInstance cmdlet | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: bc46094a-5584-47ba-8883-77dc79373a5d
caps.latest.revision: 10
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Remove-PowerPivotSystemServiceInstance cmdlet

[!INCLUDE[ssas-appliesto-sqlas-all](../../includes/ssas-appliesto-sqlas-all.md)]

  Removes a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service instance from the farm.  

>[!NOTE] 
>This article may contain outdated information and examples. Use the Get-Help cmdlet for the latest.
  
 **Applies To:** SharePoint 2010 and SharePoint 2013.  
  
## Syntax  
  
```  
Remove-PowerPivotSystemServiceInstance [-Confirm <switch>] [-DeleteLocal <switch>] [-Identity <PowerPivotMidTierServiceInstancePipeBind>] [<CommonParameters>]  
```  
  
## Description  
 The Remove-PowerPivotSystemServiceInstance cmdlet removes instance information about [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service from the farm. It does not remove the program files. To permanently remove the program files, you must uninstall them.  
  
 If you remove [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service, be sure to also run Remove-PowerPivotEngineServiceInstance to remove the associated Analysis Services instance, followed by Remove-PowerPivotServiceApplication to delete any PowerPivotservice applications. The service applications will no longer run once theservices are removed.  
  
 To revert this change, you can run New-PowerPivotSystemServiceInstance -Provision:$true to re-enable instance information.  
  
## Parameters  
  
### -Identity \<PowerPivotMidTierServiceInstancePipeBind>  
 Specifies the GUID of the [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service instance you want to remove. There is one service instance on each application server that has an installation of [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] for SharePoint.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|0|  
|Default value||  
|Accept pipeline input?|true|  
|Accept wildcard characters?|false|  
  
### -DeleteLocal \<switch>  
 Deletes the instance of [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service that is installed on the local computer, allowing you to remove the instance without having to specify an object identity.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
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
 This cmdlet supports the common parameters: Verbose, Debug, ErrorAction, ErrorVariable, WarningAction, WarningVariable,OutBuffer and OutVariable. For more information, see [About_CommonParameters](http://go.microsoft.com/fwlink/?linkID=227825).  
  
## Inputs and Outputs  
 The input type is the type of the objects that you can pipe to the cmdlet. The return type is the type of the objects that the cmdlet returns.  
  
|||  
|-|-|  
|Inputs|None.|  
|Outputs|None.|  
  
## Example 1  
  
```  
C:\PS>Remove-PowerPivotSystemServiceInstance -deletelocal  
```  
  
 This example shows how to remove the instance of [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service that runs on the local application server.  
  
## Example 2  
  
```  
C:\PS>Remove-PowerPivotSystemServiceInstance -identity 1234567-890a-bcde-fghijklmn  
```  
  
 This example shows how to delete a specific [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service based on its identity.  
  
  