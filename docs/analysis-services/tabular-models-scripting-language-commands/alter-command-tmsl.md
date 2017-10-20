---
title: "Alter command (TMSL) | Microsoft Docs"
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
ms.assetid: 8bdc49f1-209e-4055-be19-c83862b81efa
caps.latest.revision: 13
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Alter command (TMSL)

[!INCLUDE[ssas-appliesto-sql2016-later-aas](../../includes/ssas-appliesto-sql2016-later-aas.md)]

  Alters an existing object, but not its children, on an Analysis Services instance in Tabular mode.  If the object does not exist, the command raises an error.  
  
 Use **Alter** command for targeted updates, like setting a property on a table without having to specify all of the columns as well. This command is similar to **CreateOrReplace**, but without the requirement of having to provide a full object definition.  
  
 For objects having read-write properties, if you specify one read-write property, you will need to specify all of them, using new or existing values. You can use AMO PowerShell to get a property list. 
  
## Request  
 **Alter** does not have any attributes. Inputs include the object to be altered, followed by the modified object definition.  
  
 The following example illustrates the syntax for changing a property on a partition object. The object path establishes which partition object is to be altered via name-value pairs of parent objects. The second input is a partition object that specifies the new property value.  
  
```  
{   
  "alter": {   
    "object": {   
      "database": "\<database-name>",   
      "table": "\<table-name>",   
      "partition": "\<partition-name>"   
    },   
    "partition": {   
      "name": "\<new-partition-name>",   
       . . .  << other properties   
    }   
  }   
}   
```  
  
 The structure of the request varies based on the object. **Alter** can be used with any of  the following objects:  
  
 [Database object &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-objects/database-object-tmsl.md) Rename a database.  
  
```  
"alter": {   
    "object": {   
      "database": "\<database-name>"  
    },   
    "database": {   
      "name": "\<new-database-name>",   
    }   
  }   
```  
  
 [DataSources object &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-objects/datasources-object-tmsl.md) Rename a connection, which is a child object of database.  
  
```  
{   
   "alter":{   
      "object":{   
         "database":"AdventureWorksTabular1200",  
         "dataSource":"SqlServer localhost AdventureworksDW2016"  
      },  
      "dataSource":{   
         "name":"A new connection name"  
      }  
   }  
}  
```  
  
 [Tables object &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-objects/tables-object-tmsl.md) See **Example 1** below.  
  
 [Partitions object &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-objects/partitions-object-tmsl.md) See **Example 2** below.  
  
 [Roles object &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-objects/roles-object-tmsl.md) Change a property on a role object.  
  
```  
{   
   "alter":{   
      "object":{   
         "database":"AdventureWorksTabular1200",  
         "role":"DataReader"  
      },  
      "role":{   
         "name":"New Name"  
      }  
   }  
}  
```  
  
## Response  
 Returns an empty result when the command succeeds. Otherwise, an XMLA exception is returned.  
  
## Examples  
 The following examples demonstrate script that you can run in an XMLA window in Management Studio or use as input in [Invoke-ASCmd cmdlet](../../analysis-services/powershell/invoke-ascmd-cmdlet.md) in AMO PowerShell.  
  
 **Example 1** - This script changes the name property on a table.  
  
```  
{   
  "alter": {   
    "object": {   
      "database": "AdventureWorksDW2016",   
      "table": "DimDate"  
    },   
    "table": {   
      "name": "DimDate2"  
    }   
  }   
}  
```  
  
 Assuming a local named instance (instance name is "tabular") and a JSON file with the alter script, this command changes a table name from DimDate to DimDate2:  
  
 `invoke-ascmd -inputfile '\\my-computer\my-shared-folder\altertablename.json' -server 'localhost\Tabular'`  
  
 **Example 2** -- This script renames a partition, for example at year end when the current year becomes the previous year. Be sure to specify all of the properties. If you leave **source** unspecified, it could break all of your existing partition definitions.  
  
 Unless you are creating, replacing, or altering the  data source object itself, any data source referenced in your script (such as in the partition script below) must be an existing **DataSource** object in your model. If you need to change the data source, do so as a separate step.  
  
```  
{   
  "alter": {   
    "object": {   
      "database": "InternetSales",   
      "table": "DimDate",  
      "partition": "CurrentYear"  
    },   
    "partition": {   
      "name": "Year2009",  
       "source": {  
             "query":  "SELECT [dbo].[DimDate].* FROM [dbo].[DimDate] WHERE [dbo].[DimDate].CalendarYear = 2009",  
              "dataSource": "SqlServer localhost AdventureworksDW2016"  
        }  
    }   
  }   
}  
```  
  
## Usage (endpoints)  
 This command element is used in  a statement of the [Execute Method &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-methods-execute.md) call over an XMLA endpoint, exposed in the following ways:  
  
-   As an XMLA window in SQL Server Management Studio (SSMS)  
  
-   As an input file to the **invoke-ascmd** PowerShell cmdlet  
  
-   As an input to an SSIS task or SQL Server Agent job  
  
 You cannot generate a ready-made script  for this command from SSMS. Instead, you can start with an example or write your own.  
  
 The [\[MS-SSAS-T\]: QL Server Analysis Services Tabular (SQL Server Technical Protocol)](http://go.microsoft.com/fwlink/p/?LinkId=784855) document includes section 3.1.5.2.2 that describes the structure of JSON tabular metadata commands and objects. Currently, that document covers commands and capabilities not yet implemented in TMSL script. Refer to the topic ([Tabular Model Scripting Language &#40;TMSL&#41; Reference](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md)) for clarification on what is supported.  

## See Also  
 [Tabular Model Scripting Language &#40;TMSL&#41; Reference](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md)  
  
  