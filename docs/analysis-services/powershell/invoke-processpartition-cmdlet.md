---
title: "Invoke-ProcessPartition cmdlet | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 516fab44-734e-425b-9bd0-b4aee1fd338f
caps.latest.revision: 9
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Invoke-ProcessPartition cmdlet

[!INCLUDE[ssas-appliesto-sqlas-all-aas](../includes/ssas-appliesto-sqlas-all-aas.md)]

  Process a partition using a specific processing type variable.  
  
## Syntax  
 `Invoke-ProcessPartition [-Name] <System.String> [-Database] <System.String> [-ProcessType] <Microsoft.AnalysisServices.ProcessType> [-CubeName] <System.String> [-MeasureGroupName] <System.String> [<CommonParameters>]`  
  
 `Invoke-ProcessPartition –DatabasePartition <Microsoft.AnalysisServices.Partition> [-ProcessType] <Microsoft.AnalysisServices.ProcessType> [<CommonParameters>]`  
  
## Description  
 The Invoke-ProcessParition cmdlet processes a specific partition of an Analysis Services database, for a given cube and measure group. The ProcessType value determines the scope of the operation. When processing a partition, you must specify the processing type. For more information, see [Processing Options and Settings &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/processing-options-and-settings-analysis-services.md).  
  
## Parameters  
  
### -Name \<string>  
 Specifies the partition to be processed.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|0|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -Database \<string>  
 Specifies the database to which the cube belongs.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|1|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -CubeName \<string>  
 Specifies the cube to which the measure group belongs.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|2|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -MeasureGroupName \<string>  
 Specifies the measure group to which the partition belongs.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|2|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -DatabasePartition \<Microsoft.AnalysisServices.Partition>  
 Specifies the partition to be processed.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|2|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -ProcessType \<Microsoft.AnalysisServices.ProcessType>  
 Specifies the process type: ProcessFull, ProcessAdd, ProcessUpdate, ProcessIndexes, ProcessData, ProcessDefault, ProcessClear, ProcessStructure, ProcessCelarStructureOnly, ProcessScriptCache, ProcessRecalc.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### \<CommonParameters>  
 This cmdlet supports the common parameters: -Verbose, -Debug, -ErrorAction, -ErrorVariable, -OutBuffer, and -OutVariable. For more information, see [About_CommonParameters](http://go.microsoft.com/fwlink/?linkID=227825).  
  
## Inputs and Outputs  
 The input type is the type of the objects that you can pipe to the cmdlet. The return type is the type of the objects that the cmdlet returns.  
  
|||  
|-|-|  
|Inputs|None|  
|Outputs|None|  
  
## Example 1  
 `PS SQL SERVER:\sqlas\locahost\default\Databases\AWTEST\Cubes\Adventure Works\MeasureGroups\Sales Orders\Partitions\Total_Orders_2004 > Get-Item .| Invoke-ProcessPartition –ProcessType:ProcessDefault`  
  
 This command pipes in the identity of the partition to be processed.  
  
## Example 2  
 `PS SQL SERVER:\sqlas\locahost\default\Databases\AWTEST\Cubes\Adventure Works\MeasureGroups> Invoke-ProcessPartition –Name “Total_Orders_2003” –MeasureGroupname “Sales Order” –CubeName “Adventure Works” –database “AWTEST” –ProcessType “ProcessFull”`  
  
 This command processes the partition Total_Orders_2003 in the Sales Orders measure group of the AWTEST database.  
  
## See Also  
 [PowerShell scripting in Analysis Services](../../analysis-services/instances/powershell-scripting-in-analysis-services.md)   
 [Manage Tabular Models Using PowerShell](http://go.microsoft.com/fwlink/?linkID=227685)  
  
  