---
title: "Sequence command (TMSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/28/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 898d6ec2-9b40-441b-be2b-5728d1d2882e
caps.latest.revision: 11
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Sequence command (TMSL)
  Use the **Sequence** command to run a consecutive set of operations in batch mode on an instance of Analysis Services.  The entire command and all of its component parts must complete in order for the transaction to succeed.  
  
 The following commands can be run sequentially, except for the **Refresh** command which runs in parallel to process multiple objects concurrently.  
  
-   [Create command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/create-command-tmsl.md)  
  
-   [CreateOrReplace command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/createorreplace-command-tmsl.md)  
  
-   [Alter command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/alter-command-tmsl.md)  
  
-   [Delete command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/delete-command-tmsl.md)  
  
-   [Refresh command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/refresh-command-tmsl.md)  
  
-   [Backup command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/backup-command-tmsl.md)  
  
-   [Restore command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/restore-command-tmsl.md)  
  
-   [Attach command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/attach-command-tmsl.md)  
  
-   [Detach command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/detach-command-tmsl.md)  
  
## Request  
 **maxParallelism** is an optional property that determines whether **Refresh** operations run sequentially or in parallel.  
  
 The default behavior is to use as much parallelism as possible. By embedding **Refresh** within **Sequence**, you can control the exact number of threads used during processing, including limiting the operation to just one thread.  
  
> [!NOTE]  
>  The integer assigned to **maxParallelism** determines the maximum number of threads used during processing. Valid values are any positive integer. Setting the value to 1 equals not parallel (uses one thread).  
  
 Only **Refresh** runs in parallel. If you modify **maxParallelism** to use a fixed number of threads, be sure to review the properties on the [Refresh command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/refresh-command-tmsl.md) to understand the potential impact. It's possible to set properties in a way that undermines parallelism even when you've made multiple threads available. The following sequence of refresh types will give you the best degree of parallelism:  
  
-   First, specify Refresh for all objects using ClearValues  
  
-   Next, specify Refresh for all objects using DataOnly  
  
-   Last specify Refresh for all objects using Full, Calculate, Automatic or Add  
  
 Any variation on this will break parallelism.  
  
```  
{   
  "sequence":    
    {   
      "maxParallelism": 3,   
      "operations": [   
        {   
          "mergepartitions": {   
            "sources": [   
              {   
                "database": "salesdatabase",   
                "table": "Sales",   
                "partition": "partition1"   
              },   
              {   
                "database": "salesdatabase",   
                "table": "Sales",   
                "partition": "partition2"   
              }   
            ]   
          }   
        },   
        {   
          "refresh": {   
            "type": "calculate",   
            "object": {   
             "database": "salesdatabase"   
            }   
          }   
        }   
      ]   
    }      
}   
```  
  
## Response  
 Returns an empty result when the command succeeds. Otherwise, an XMLA exception is returned.  
  
## Usage (endpoints)  
 This command element is used in  a statement of the [Execute Method &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-methods-execute.md) call over an XMLA endpoint, exposed in the following ways:  
  
-   As an XMLA window in SQL Server Management Studio (SSMS)  
  
-   As an input file to the **invoke-ascmd** PowerShell cmdlet  
  
-   As an input to an SSIS task or SQL Server Agent job  
  
 You cannot generate a ready-made script  for this command from SSMS. Instead, you can start with an example or write your own.  
  
 The [\[MS-SSAS-T\]: SQL Server Analysis Services Tabular (SQL Server Technical Protocol)](http://go.microsoft.com/fwlink/p/?LinkId=784855) document includes section 3.1.5.2.2 that describes the structure of JSON tabular metadata commands and objects. Currently, that document covers commands and capabilities not yet implemented in TMSL script. Refer to the topic ([Tabular Model Scripting Language &#40;TMSL&#41; Reference](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md)) for clarification on what is supported.  
  
 For instructions on how to leverage TMSL script in practical solutions, see [Script Administrative Tasks in Analysis Services](../../analysis-services/instances/script-administrative-tasks-in-analysis-services.md).  
  
## See Also  
 [Tabular Model Scripting Language &#40;TMSL&#41; Reference](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md)  
  
  