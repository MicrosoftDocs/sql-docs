---
title: "Create command (TMSL) | Microsoft Docs"
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
ms.assetid: e3024f89-ebfa-47e4-9893-708f379fd9b8
caps.latest.revision: 11
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Create command (TMSL)

[!INCLUDE[ssas-appliesto-sql2016-later-aas](../includes/ssas-appliesto-sql2016-later-aas.md)]

  Creates the specified object and all of the descendant objects that are specified. If the object already exists, the command raises an error.  
  
## Request  
 The structure of the request varies based on the object. An object that is a parent must include all of its children, although the full object definitions of siblings and parent(s) are not required.  
  
 [Database object &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-objects/database-object-tmsl.md) Add a database to a server.  
  
```  
{   
  "create": {   
    "database": {   
      "name": "AdventureworksDW2016",   
      "description": "<description>",   
      "tables": [   
        { },   
        { },   
        { }   
      ],   
      "relationships": [   
        { },   
        { }   
      ]   
    }   
  }   
}  
```  
  
 [DataSources object &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-objects/datasources-object-tmsl.md)  
  
```  
{  
  "create": {  
    "parentObject": {  
      "database": "AdventureworksDW2016"  
    },  
    "dataSource": {  
      "name": "SqlServer localhost AdventureworksDW2016",  
      "connectionString": "Provider=SQLNCLI11;Data Source=localhost;Initial Catalog=AdventureworksDW2016;Integrated Security=SSPI;Persist Security Info=false",  
      "impersonationMode": "impersonateAccount",  
      "account": "<account name>",  
      "annotations": [  
        {  
          "name": "ConnectionEditUISource",  
          "value": "SqlServer"  
        }  
      ]  
    }  
  }  
}  
```  
  
 [Tables object &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-objects/tables-object-tmsl.md) Add columns to a table.  
  
```  
{   
  "create": {   
    "parentObject": {   
      "database": "AdventureworksDW2016",   
      "table": "DimSales"  
    },   
    "columns": {   
      "type": ["calculated"  | "data" ]  
      "name": "\<column-name>",   
       "expression":  "<DAX expression>"  
    }   
  }   
}   
```  
  
 [Partitions object &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-objects/partitions-object-tmsl.md) Add a partition to a parent table object.  
  
```  
{  
  "create": {  
    "parentObject": {  
      "database": "AdventureWorksTabular1200",  
      "table": "Date"  
    },  
    "partition": {  
      "name": "Date 2",  
      "source": {  
        "query": "SELECT [dbo].[DimDate].* FROM [dbo].[DimDate]",  
        "dataSource": "SqlServer localhost AdventureworksDW2016"  
      }  
    }  
  }  
}  
```  
  
 [Roles object &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-objects/roles-object-tmsl.md) Minimally add a role to a database, but without membership or filters.  
  
```  
{  
  "create": {  
    "parentObject": {  
      "database": "AdventureWorksDW2016"  
    },  
    "role": {  
      "name": "DataReader",  
      "modelPermission": "read"  
    }  
  }  
}  
```  
  
 Except for the **Database** object, the object being created is  child of a specified **parentObject**. The parent of the **Database** object is always the **Server** object.  
  
 Server is assumed from the context. For example, if you run the script from Management Studio or AMO PowerShell, the server connection would be specified in the session or as a parameter.  
  
## Response  
 Returns an empty result when the command succeeds. Otherwise, an XMLA exception is returned.  
  
## Examples  
 **Example 1** - Add a role that specifies membership and a filter.  
  
```  
{   
   "create":{   
      "parentObject":{   
         "database":"AdventureWorksTabular1200"  
      },  
      "role":{  
         "name":"DataReader",  
         "modelPermission":"read",  
         "members":[   
            {  
               "memberName": "account-01",  
               "memberId":"S-1-5-21-1111111111-2222222222-33333333-444444"  
            },  
            {   
               "memberName": "account-02",  
               "memberId":"S-2-5-21-1111111111-2222222222-33333333-444444"  
            }  
         ],  
         "tablePermissions":[   
            {   
               "name":"Date",  
               "filterExpression":"CalendarYear('2011')"  
            }  
         ]  
      }  
   }  
}  
```  
  
## Usage (endpoints)  
 This command element is used in  a statement of the [Execute Method &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-methods-execute.md) call over an XMLA endpoint, exposed in the following ways:  
  
-   As an XMLA window in SQL Server Management Studio (SSMS)  
  
-   As an input file to the **invoke-ascmd** PowerShell cmdlet  
  
-   As an input to an SSIS task or SQL Server Agent job  
  
 You can generate a ready-made script  for this command from SSMS.  For example, you can right-click an existing database > **Script** > **Script Database as** > **CREATE To**.  
  
 The [\[MS-SSAS-T\]: QL Server Analysis Services Tabular (SQL Server Technical Protocol)](http://go.microsoft.com/fwlink/p/?LinkId=784855) document includes section 3.1.5.2.2 that describes the structure of JSON tabular metadata commands and objects. Currently, that document covers commands and capabilities not yet implemented in TMSL script. Refer to the topic ([Tabular Model Scripting Language &#40;TMSL&#41; Reference](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md)) for clarification on what is supported.  
  
 For instructions on how to leverage TMSL script in practical solutions, see [Script Administrative Tasks in Analysis Services](../../analysis-services/instances/script-administrative-tasks-in-analysis-services.md).  
  
## See Also  
 [Tabular Model Scripting Language &#40;TMSL&#41; Reference](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md)  
  
  