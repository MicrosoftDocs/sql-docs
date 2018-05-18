---
title: "Invoke-ProcessTable cmdlet | Microsoft Docs"
ms.date: 05/02/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: powershell
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Invoke-ProcessTable cmdlet
[!INCLUDE[ssas-appliesto-sqlas](../../includes/ssas-appliesto-sqlas.md)]
  Conducts the **Process** operation on a **Table** with a specific **RefreshType**.  

>[!NOTE] 
>This article may contain outdated information and examples. Use the Get-Help cmdlet for the latest.
  
## Syntax  
 `Invoke-ProcessTable [-DatabaseName] <string> [-TableName] <string> [-RefreshType] <RefreshType> {Full |     ClearValues | Calculate | DataOnly | Automatic | Add | Defragment} [-Server <string>] [-Credential <pscredential>     [-WhatIf] [-Confirm]  [<CommonParameters>]`  
  
 `Invoke-ProcessTable -RefreshType <RefreshType> {Full | ClearValues | Calculate | DataOnly | Automatic | Add |     Defragment} -Table <Table> [-Server <string>] [-Credential <pscredential>] [-WhatIf] [-Confirm]     [<CommonParameters>]`  
  
## Parameters  
  
### -TableName \<string>  
 Name of the table to which the partition belongs that has to be processed.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|0|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -DatabaseName \<string>  
 Specifies the database to which the table belongs.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|0|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -Server\<Microsoft.AnalysisSevices.Server>  
 Optionally specifies the server instance to connect to if you are not using the **SQLAS** provider directory for context.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -RefreshType \<Microsoft.AnalysisServices.RefreshType>  
 Specifies the process type for a Tabular database.  Valid values are  Full, ClearValues, Calculate, DataOnly,  Automatic, Add, and  Defragment. See [Process Database, Table, or Partition &#40;Analysis Services&#41;](../../analysis-services/tabular-models/process-database-table-or-partition-analysis-services.md) for descriptions and guidance.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|1|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -Credential  
 If this parameter is specified, the user name and password passed will be used to connect to the Analysis Services instance. If no credentials are specified, the default Windows account of the user running the script is assumed.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -Whatif  
 Include this parameter to get information about the impact of the operation before it's executed.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -Confirm  
 Include this parameter to interactively  confirm the operation with a Yes or No response before it's executed.  
  
|||  
|-|-|  
|Required?|false|  
|Position?||  
|Default value||  
|Accept pipeline input?||  
|Accept wildcard characters?|false|  
  
## Example 1  
 `PS SQLSERVER:\SQLAS\MachineName\Instance\Databases\DB1\> Invoke-ProcessTable -TableName "myTable" -Database "DB1"  -RefreshType "Full"`  
  
 This command pipes in the identity of the table to be processed.  
  
## Example 2  
 `PS SQLSERVER:\SQLAS\MachineName\Instance\Databases\DB1\> Invoke-ProcessTable -TableName "myTable" -Database "DB1"  -RefreshType [Microsoft.AnalysisServices.Tabular.RefreshType]::Full`  
  
 This command processes a tabular metadata table using an **enum** refresh type.  
  
  
