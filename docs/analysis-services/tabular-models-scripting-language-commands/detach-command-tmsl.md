---
title: "Detach command (TMSL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 413b49cb-ea8f-415c-a059-ce692b7771a1
caps.latest.revision: 8
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Detach command (TMSL)
  Detaches an Analysis Services database from a server.  
  
## Request  
  
```  
{   
   "detach": {    
      "database":"AdventureWorksDW2014",  
      "password": "secret"  
   }  
}  
```  
  
 The properties accepted by the JSON detach command are as follows.  
  
||||  
|-|-|-|  
|**Property**|**Default**|**Description**|  
|database|[Required]|The name of the database object to be detached.|  
|password|Empty|The password to use to encrypt secrets in the detached database.|  
  
## Response  
 Returns an empty result when the command succeeds. Otherwise, an XMLA exception is returned.  
  
## Usage (endpoints)  
 This command element is used in  a statement of the [Execute Method &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-methods-execute.md) call over an XMLA endpoint, exposed in the following ways:  
  
-   As an XMLA window in SQL Server Management Studio (SSMS)  
  
-   As an input file to the **invoke-ascmd** PowerShell cmdlet  
  
-   As an input to an SSIS task or SQL Server Agent job  
  
 You can generate a ready-made script  for this command from SSMS by clicking the Script button on the Detach dialog box.  
  
 The [\[MS-SSAS-T\]: QL Server Analysis Services Tabular (SQL Server Technical Protocol)](http://go.microsoft.com/fwlink/p/?LinkId=784855) document includes section 3.1.5.2.2 that describes the structure of JSON tabular metadata commands and objects. Currently, that document covers commands and capabilities not yet implemented in TMSL script. Refer to the topic ([Tabular Model Scripting Language &#40;TMSL&#41; Reference](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md)) for clarification on what is supported.  
  
 For instructions on how to leverage TMSL script in practical solutions, see [Script Administrative Tasks in Analysis Services](../../analysis-services/instances/script-administrative-tasks-in-analysis-services.md).  
  
## See Also  
 [Tabular Model Scripting Language &#40;TMSL&#41; Reference](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md)   
 [Attach and Detach Analysis Services Databases](../../analysis-services/multidimensional-models/attach-and-detach-analysis-services-databases.md)  
  
  