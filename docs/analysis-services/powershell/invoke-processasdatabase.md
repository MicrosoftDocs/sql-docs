---
title: "Invoke-ProcessASDatabase | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 66d5d154-88ce-4c2e-b1ef-e2d2f6fb1c44
caps.latest.revision: 11
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Invoke-ProcessASDatabase

[!INCLUDE[ssas-appliesto-sqlas-all-aas](../../includes/ssas-appliesto-sqlas-all-aas.md)]

  Conducts the **Process** operation on a specified **Database** with a specific **ProcessType** or **RefreshType** depending on the underlying metadata type.  
  
 Use **ProcessType** for database with Multidimensional metadata (this includes tabular databases with compatibility level 1050, 1100, or 1103).  
  
 Use **RefreshType** for Tabular databases at compatibility level 1200 or higher.  
  
## Syntax  
 `Invoke-ProcessASDatabase [-DatabaseName] <string> [-RefreshType] <RefreshType> {Full | ClearValues | Calculate |     DataOnly | Automatic | Add | Defragment} [-Server <string>] [-Credential <pscredential>] [-WhatIf] [-Confirm]     [<CommonParameters>]`  
  
 `Invoke-ProcessASDatabase [-DatabaseName] <string> [-ProcessType] <ProcessType> {ProcessFull | ProcessAdd |     ProcessUpdate | ProcessIndexes | ProcessData | ProcessDefault | ProcessClear | ProcessStructure |     ProcessClearStructureOnly | ProcessScriptCache | ProcessRecalc | ProcessDefrag} [-Server <string>] [-Credential     <pscredential>] [-WhatIf] [-Confirm]  [<CommonParameters>]`  
  
## Description  
 The **Invoke-ProcessASDatabase** cmdlet processes a database to the level you specify. For example, for Tabular databases at compatibility level 1200, setting **RefreshType** to **Full** overwrites existing data with all new data.  
  
 The processing type (multidimensional) or refresh type (tabular) is required and can be specified before or after the database and server parameters:  
  
-   For multidimensional, see [Processing Options and Settings &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/processing-options-and-settings-analysis-services.md).  
  
-   For tabular, see [Process Database, Table, or Partition &#40;Analysis Services&#41;](../../analysis-services/tabular-models/process-database-table-or-partition-analysis-services.md).  
  
## Parameters  
  
### -DatabaseName \<string>  
 Specifies the Tabular or Multidimensional database to be processed.  
  
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
  
### -ProcessType \<Microsoft.AnalysisServices.ProcessType>  
 Specifies the process type for a Multidimensional database or a Tabular database at compatibility levels 1050-1103. Valid values include ProcessFull, ProcessAdd, ProcessUpdate, ProcessIndexes, ProcessData, ProcessDefault, ProcessClear, ProcessStructure, ProcessCelarStructureOnly, ProcessScriptCache, or ProcessRecalc. See [Processing Options and Settings &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/processing-options-and-settings-analysis-services.md) for descriptions and guidance.  
  
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
  
## -Whatif  
 Include this parameter to get information about the impact of the operation before it's executed.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
## -Confirm  
 Include this parameter to interactively  confirm the operation with a Yes or No response before it's executed.  
  
|||  
|-|-|  
|Required?|false|  
|Position?||  
|Default value||  
|Accept pipeline input?||  
|Accept wildcard characters?|false|  
  
## Example 1 (SQLAS provider)  
 This example uses the **SQLAS** provider to set context to the list of databases on a default instance.  If you list the contents of the databases directory, you will see all of the databases along with their process state and read-write mode.  
  
 From the database folder, you can run **Invoke-ProcessASDatabase** specifying just the database name.  
  
```  
PS > import-module SQLPS -DisableNameChecking  
PS  SQL Server > cd sqlas\ssas-srv-01\default\databases  
PS SQLSERVER:\sqlas\ssas-srv-01\default\databases> dir  
. . . .  
PS SQLSERVER:\sqlas\ssas-srv-01\default\databases> Invoke-ProcessASDatabase "adventureworks"  
  
```  
  
 Depending on the type of database, you will be prompted to specify a **RefreshType** or a **ProcessType**.  
  
 Proof of processing is the presence of an impact object in the return statement: Microsoft.AnalysisServices.Tabular.ObjectImpact.  
  
 Note that object state information is sometimes cached, so if you list the contents of a directory after processing succeeds, the database state retains its original 'unprocessed' descriptor. This is misleading because as long as the objectimact was returned, the database is actually processed.  
  
 You can confirm that processing succeeded by looking at the database property page in Management Studio, starting a new session, or by returning processing state from a database object (via [Microsoft.AnalysisServices.ProcessableMajorObject.LastProcessed](https://msdn.microsoft.com/library/microsoft.analysisservices.processablemajorobject.lastprocessed.aspx)).  
  
## Example 2  
 This example shows the same operation using just the cmdlet without the provider. Additional parameters are required to specify the server and process type.  
  
```  
PS > import-module SQLPS -DisableNameChecking  
PS  SQL Server >  Invoke-ProcessASDatabase -databasename "AdventureWorks" -server '\\server-name\instancename' –ProcessType "ProcessFull"  
  
```  
  
## See Also  
 [Compatibility Level for Tabular models in Analysis Services](../../analysis-services/tabular-models/compatibility-level-for-tabular-models-in-analysis-services.md)  
  
  