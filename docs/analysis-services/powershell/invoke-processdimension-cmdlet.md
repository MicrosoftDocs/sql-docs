---
title: "Invoke-ProcessDimension cmdlet | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.component:
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Invoke-ProcessDimension cmdlet
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Process a dimension using a specific processing type variable.  

>[!NOTE] 
>This article may contain outdated information and examples. Use the Get-Help cmdlet for the latest.
  
## Syntax  
 `Invoke-ProcessDimension [-Name] <System.String> [-Database] <System.String> [-ProcessType] <Microsoft.AnalysisServices.ProcessType> [<CommonParameters>]`  
  
 `Invoke-ProcessDimension –DatabaseDimension <Microsoft.AnalysisServices.Dimension> [-ProcessType] <Microsoft.AnalysisServices.ProcessType> [<CommonParameters>]`  
  
## Description  
 The Invoke-ProcessDimension cmdlet processes the specified dimension. You must specify the processing type. For more information about processing types for a dimension, see [Processing Options and Settings &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/processing-options-and-settings-analysis-services.md).  
  
## Parameters  
  
### -Name \<string>  
 Specifies the dimension to be processed.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|0|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -Database \<string>  
 Specifies the database to which the dimension belongs.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|1|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -ProcessType \<Microsoft.AnalysisServices.ProcessType>  
 Specifies the process type: ProcessFull, ProcessAdd, ProcessUpdate, ProcessIndexes, ProcessData, ProcessDefault, ProcessClear, ProcessStructure, ProcessCelarStructureOnly, ProcessScriptCache, ProcessRecalc.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|2|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -DatabaseDimension \<Microsoft.AnalysisSevices.Dimension>  
 Specifies a Microsoft.AnalysisServices.Dimension object to be processed. Use this parameter if you want to pass in the dimension name via pipeline.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|True (ByPropertyName)|  
|Accept wildcard characters?|false|  
  
### \<CommonParameters>  
 This cmdlet supports the common parameters: -Verbose, -Debug, -ErrorAction, -ErrorVariable, -OutBuffer, and -OutVariable. For more information, see [About_CommonParameters](http://go.microsoft.com/fwlink/?linkID=227825).  
  
## Inputs and Outputs  
 The input type is the type of the objects that you can pipe to the cmdlet. The return type is the type of the objects that the cmdlet returns.  
  
|||  
|-|-|  
|Inputs|Microsoft.AnalysisSevices.Dimension|  
|Outputs|None|  
  
## Example 1  
 `PS SQL SERVER:\sqlas\locahost\default\Databases\AWTEST\Dimensions\Account> Get-Item .| Invoke-ProcessDimension –ProcessType:ProcessDefault`  
  
 This command retrieves the specified dimension object via the pipeline and processes it.  
  
## Example 2  
 `PS SQL SERVER:\sqlas\locahost\default\Databases\AWTEST\Dimensions > Invoke-ProcessDimension –Name “Customer” –Database “AWTEST” –ProcessType “ProcessDefault”`  
  
 This command identifies a specific dimension that will be processed.  
  
> [!NOTE]  
>  Sometimes a dimension that processed successfully appears as ‘unprocessed’ when you list the dimensions folder in the PowerShell window. To verify whether a dimension was actually processed, check the dimension properties in SQL Server Management Studio.  
  
  
  
