---
title: "Remove-PowerPivotServiceApplication cmdlet | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 2742b2a3-927c-4e7c-bd7d-43c072fa01ab
caps.latest.revision: 11
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Remove-PowerPivotServiceApplication cmdlet

[!INCLUDE[ssas-appliesto-sqlas-all](../../includes/ssas-appliesto-sqlas-all.md)]

  Deletes a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application.  

>[!NOTE] 
>This article may contain outdated information and examples. Use the Get-Help cmdlet for the latest.
  
 **Applies To:** SharePoint 2010 and SharePoint 2013.  
  
## Syntax  
  
```  
Remove-PowerPivotServiceApplication [-Identity <SPGeminiServiceApplicationPipeBind>] [-DeleteAll <switch>] [-RemoveData <switch>] [-Confirm <switch>] [<CommonParameters>]  
```  
  
## Description  
 The Remove-PowerPivotServiceApplication cmdlet deletes a [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application from the farm. Use DeleteAll to delete all service applications at once, or use the Identity parameter to remove a single instance. To get instance information, run Get-PowerPivotServiceApplication to return all instances in the farm.  
  
 Use the RemoveData parameter to optionally remove service application databases and cached files. [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] workbooks remain in content libraries, but are no longer functional once the service application is removed.  
  
## Parameters  
  
### -Identity \<SPGeminiServiceApplicationPipeBind>  
 Specifies the GUID of a single [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application in the farm. You must specify the GUID if you want to remove just one application, leaving other service applications intact.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|0|  
|Default value||  
|Accept pipeline input?|true|  
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
  
### -DeleteAll \<switch>  
 Deletes all [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service applications, but does not delete the service application database, nor the service instance objects in the farm. [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] System Service and [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] Engine Service objects remain instantiated, but unusable, after you remove the service applications.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -RemoveData \<switch>  
 Removes the service application database that contains data refresh schedules, workbooks usage data, instance maps used to track which databases are loaded, and other internal data.  
  
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
C:\PS>Remove-PowerPivotServiceApplication -identity 12345678-90ab-cdef-ghijklmnop  
```  
  
 This example deletes a single [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application, but does not remove its database or cache. If you do not specify an identity, you will be prompted to provide one.  
  
## Example 2  
  
```  
C:\PS>Remove-PowerPivotServiceApplication -DeleteAll  
```  
  
 This example deletes all [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service applications in the farm. Databases and cache are not deleted.  
  
## Example 3  
  
```  
CC:\PS>Remove-PowerPivotServiceApplication -identity 12345678-90ab-cdef-ghijklmnop -RemoveData  
```  
  
 This example deletes a single [!INCLUDE[ssGemini](../../includes/ssgemini-md.md)] service application, and its database and cache files.  
  
  