---
title: "Backup command (TMSL) | Microsoft Docs"
ms.custom: ""
ms.date: "05/30/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: fae1b267-0750-4dc5-9bf0-80ac4d8487a3
caps.latest.revision: 9
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Backup command (TMSL)

[!INCLUDE[ssas-appliesto-sql2016-later-aas](../includes/ssas-appliesto-sql2016-later-aas.md)]

  Backs up an Analysis Services database to an .abf backup file.  
  
## Request  
  
```  
    {  
        "backup": {  
            "description": "Parameters of Backup command of Analysis Services JSON API",  
            "properties": {  
            "database": {  
                "type": "string"  
            },  
            "file": {  
                "type": "string"  
            },  
            "password": {  
                "type": "string"  
            },  
            "allowOverwrite": {  
                "type": "boolean"  
            },  
            "applyCompression": {  
                "type": "boolean"  
            }  
            },  
. . .   
```  
  
 **Backup** has several properties.  
  
||||  
|-|-|-|  
|**Property**|**Default**|**Description**|  
|database|[Required]|The name of the database object to be backed up.|  
|file|[Required]|The backup file name/path.|  
|password|Empty|The password to use for encrypting the backup file.|  
|allowOverwrite|False|A Boolean that, when true, indicates that a backup file that already exists will be overwritten; otherwise false.|  
|applyCompression|True|A Boolean that, when true, indicates that backup files are compressed; otherwise false.|  
  
## Response  
 Returns an empty result when the command succeeds. Otherwise, an XMLA exception is returned.  
  
## Examples  
 **Example 1** - Backup a file to the default backup folder.  
  
```  
{   
   "backup": {   
      "database":"AS_AdventureWorksDW2014",  
      "file":"AS_AdventureWorksDW2014.abf",  
      "password":"secret"  
   }  
}  
```  
  
## Usage (endpoints)  
 This command element is used in  a statement of the [Execute Method &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-methods-execute.md) call over an XMLA endpoint, exposed in the following ways:  
  
-   As an XMLA window in SQL Server Management Studio (SSMS)  
  
-   As an input file to the **invoke-ascmd** PowerShell cmdlet  
  
-   As an input to an SSIS task or SQL Server Agent job  
  
 You can generate a ready-made script  for this command from SSMS by clicking the Script button on the Backup Database dialog box.  
  
 The [\[MS-SSAS-T\]: QL Server Analysis Services Tabular (SQL Server Technical Protocol)](http://go.microsoft.com/fwlink/p/?LinkId=784855) document includes section 3.1.5.2.2 that describes the structure of JSON tabular metadata commands and objects. Currently, that document covers commands and capabilities not yet implemented in TMSL script. Refer to the topic [Tabular Model Scripting Language &#40;TMSL&#41; Reference](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md) for clarification on what is supported  
  
 For instructions on how to leverage TMSL script in practical solutions, see [Script Administrative Tasks in Analysis Services](../../analysis-services/instances/script-administrative-tasks-in-analysis-services.md).  
  
## See Also  
 [Tabular Model Scripting Language &#40;TMSL&#41; Reference](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md)   
 [Backup and Restore of Analysis Services Databases](../../analysis-services/multidimensional-models/backup-and-restore-of-analysis-services-databases.md)  
  
  