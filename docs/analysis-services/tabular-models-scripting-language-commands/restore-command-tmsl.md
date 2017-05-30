---
title: "Restore command (TMSL) | Microsoft Docs"
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
ms.assetid: 360a1567-67ae-459d-8865-9a2bef8d4186
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Restore command (TMSL)

[!INCLUDE[ssas-appliesto-sql2016-later-aas](../includes/ssas-appliesto-sql2016-later-aas.md)]

  Restores an Analysis Services database from a backup file.  
  
## Request  
  
```  
    {  
"restore": {  
            "description": "Parameters of Restore command of Analysis Services JSON API",  
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
            "dbStorageLocation": {  
                "type": "string"  
            },  
            "allowOverwrite": {  
                "type":boolean  
            },  
            "readWriteMode": {  
                "enum": [  
                "readWrite",  
                "readOnly",  
                "readOnlyExclusive"  
                ]  
. . .   
```  
  
 **Restore** has several properties.  
  
||||  
|-|-|-|  
|**Property**|**Default**|**Description**|  
|database|[Required]|The name of the database object to be restored.|  
|file|[Required]|The backup file name/path.|  
|password|Empty|The password to use for decrypting the backup file.|  
|allowOverwrite|False|A Boolean that, when true, indicates that a backup file that already exists will be overwritten; otherwise false.|  
|readWriteMode|readWrite|An enumeration value that indicates the access modes allowed to the database.<br /><br /> **The enumeration values are as follows:**<br /><br /> readWrite – Read-write access is allowed.<br /><br /> readOnly – Read-only access is allowed.<br /><br /> readOnlyExclusive – Read-only exclusive access is allowed.|  
|dbStorageLocation|Empty|Storage location for the restored database.|  
  
## Response  
 Returns an empty result when the command succeeds. Otherwise, an XMLA exception is returned.  
  
## Example  
 **Example 1** - Restore a database from a local folder.  
  
```  
{   
   "restore": {   
      "database":"AdventureWorksDW2014",  
      "file":"c:\\awdbdwfile.abf",  
      "security":"...",  
      "allowOverwrite":"true",  
      "password":"..",  
      "locations":"d:\\SQL Server Analysis Services\\data\\",  
      "storageLocation":".."  
   }  
}  
```  
  
## Usage (endpoints)  
 This command element is used in  a statement of the [Execute Method &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-methods-execute.md) call over an XMLA endpoint, exposed in the following ways:  
  
-   As an XMLA window in SQL Server Management Studio (SSMS)  
  
-   As an input file to the **invoke-ascmd** PowerShell cmdlet  
  
-   As an input to an SSIS task or SQL Server Agent job  
  
 You can generate a ready-made script  for this command from SSMS by clicking the Script button on the Restore dialog box.  
  
 The [\[MS-SSAS-T\]: QL Server Analysis Services Tabular (SQL Server Technical Protocol)](http://go.microsoft.com/fwlink/p/?LinkId=784855) document includes section 3.1.5.2.2 that describes the structure of JSON tabular metadata commands and objects. Currently, that document covers commands and capabilities not yet implemented in TMSL script. Refer to the topic [Tabular Model Scripting Language &#40;TMSL&#41; Reference](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md) for clarification on what is supported  
  
 For instructions on how to leverage TMSL script in practical solutions, see [Script Administrative Tasks in Analysis Services](../../analysis-services/instances/script-administrative-tasks-in-analysis-services.md).  
  
## See Also  
 [Tabular Model Scripting Language &#40;TMSL&#41; Reference](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md)   
 [Backup and Restore of Analysis Services Databases](../../analysis-services/multidimensional-models/backup-and-restore-of-analysis-services-databases.md)  
  
  