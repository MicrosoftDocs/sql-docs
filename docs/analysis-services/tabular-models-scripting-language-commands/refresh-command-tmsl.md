---
title: "Refresh command (TMSL) | Microsoft Docs"
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
ms.assetid: 97ff6ba8-c236-4ba6-8220-b0fcb9e1dc5c
caps.latest.revision: 14
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Refresh command (TMSL)
  Processes objects in the current database.   
**Refresh** always runs in parallel unless you throttle it with [Sequence command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/sequence-command-tmsl.md).  
  
 You can override some properties of some objects during a data refresh operation:  
  
-   Change the **QueryDefinition** property of a **Partition** object to import data using an on-the-fly filter expression.  
  
-   Provide data source credentials as part of a **Refresh** command,  in the **ConnectionString** property of a **DataSource**  object. This  approach could be considered more secure, as credentials are provided and used temporarily for the duration of the operation, rather than stored.  
  
 See the examples in this topic for an illustration of these property overrides.  
  
> [!NOTE]  
>  Unlike multidimensional processing, there is no special handling of processing errors for Tabular processing.  

  
## Request  
 **Refresh** takes a type parameter and object definition.  
  
```  
    {  
        "refresh": {  
            "description": "Parameters of Refresh command of Analysis Services JSON API",  
            "properties": {  
            "type": {  
                "enum": [  
                "full",  
                "clearValues",  
                "calculate",  
                "dataOnly",  
                "automatic",  
                "add",  
                "defragment"  
                ]  
            },  
            "objects": {  
. . .   
```  
  
 The **type** parameter  sets a scope on the processing operation.  
  
||||  
|-|-|-|  
|**Refresh type**|**Applies to**|**Description**|  
|full|Database,<br />Table,<br />Partition|For all partitions in the specified partition, table, or database, refresh data and recalculate all dependents. For a calculation partition, recalculate the partition and all its dependents.|  
|clearValues|Database,<br />Table,<br />Partition|Clear values in this object and all its dependents.|  
|calculate|Database,<br />Table,<br />Partition|Recalculate this object and all its dependents, but only if needed. This value does not force recalculation, except for volatile formulas.|  
|dataOnly|Database,<br />Table,<br />Partition|Refresh data in this object and clear all dependents.|  
|automatic|Database,<br />Table,<br />Partition|If the object needs to be refreshed and recalculated, refresh and recalculate the object and all its dependents. Applies if the partition is in a state other than Ready.|  
|add|Partition|Append data to this partition and recalculate all dependents. This command is valid only for regular partitions and not for calculation partitions.|  
|defragment|Database,<br />Table|Defragment the data in the specified table. As data is added to or removed from a table, the dictionaries of each column can become polluted with values that no longer exist in the actual column values. The defragment option will clean up the values in the dictionaries that are no longer used.|  
  
 You can refresh the following objects:  
  
 [Database object &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-objects/database-object-tmsl.md) Process a database.  
  
```  
{  
  "refresh": {  
    "type": "automatic",  
    "objects": [  
      {  
        "database": "AdventureWorksTabular1200"  
      }  
    ]  
  }  
}  
```  
  
 [Tables object &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-objects/tables-object-tmsl.md) Process a single table.  
  
```  
{  
  "refresh": {  
    "type": "automatic",  
    "objects": [  
      {  
        "database": "AdventureWorksTabular1200",  
        "table": "Date"  
      }  
    ]  
  }  
}  
```  
  
 [Partitions object &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-objects/partitions-object-tmsl.md) Process a single partition within a table.  
  
```  
{  
  "refresh": {  
    "type": "automatic",  
    "objects": [  
      {  
        "database": "AdventureWorksTabular1200",  
        "table": "FactSalesQuota",  
        "partition": "FactSalesQuota"  
      },  
      {  
        "database": "AdventureWorksTabular1200",  
        "table": "FactSalesQuota",  
        "partition": "FactSalesQuota - 2011"  
      }  
    ]  
  }  
}  
```  
  
## Response  
 Returns an empty result when the command succeeds. Otherwise, an XMLA exception is returned.  
  
## Examples  
 Override both the **ConnectionString** and query definition of a partition.  
  
```  
{   
    "refresh": {   
        "type": "data",   
        "objects": [   
            {   
                "database": "tmtestdb",   
                "table": "sales"   
            },   
            {   
                "database": "tmtestdb",   
                "table": "customer"   
            }   
        ],   
        "overrides": [   
            {   
                "dataSources": [ // Bindings for Data Sources   
                    {   
                        "object": {   
                            "database": "tmtestdb",   
                            "dataSource": "contoso"   
                        },   
                        "dataSource": {   
                        "connectionString": "Provider=SQLNCLI11;Data Source=localhost;Initial Catalog=contoso;Integrated Security=SSPI;Persist Security Info=false"   
"   
                        }   
                    }   
                ],   
                "partitions": [ // Bindings for Partitions   
                    {   
                        "object": {   
                            "database": "tmtestdb",   
                            "table": "sales",   
                            "partition": "2015"   
                        },   
                        "partition": {   
                            "source": {   
                                "query": [  
                                     "SELECT sales.salesamount, customer.customername FROM sales",  
                                     "JOIN customer on custKey = sales.custkey",  
                                     "JOIN date on date.DateKey = customer.OrderDate",  
                                     "WHERE date.CalendarYear='2015'"  
                                  ],  
                            }   
                        }   
                    }   
                ]   
            }   
        ]   
    }   
}   
```  
  
 Scope particular overrides by setting the type parameter to a **dataOnly** refresh, metadata stays intact.  
  
```  
{   
        "refresh" : {   
            "type" : "dataOnly",   
            "objects" : [   
                {   
                    "database" : "TMTestDB",   
                    "table" : "Customer"   
                },   
                {   
                    "database" : "TMTestDB",   
                    "table" : "Sales"   
                }   
            ],   
            "overrides" : [{   
                "scope" : {   
                    "database" : "TMTestDB",   
                    "table" : "Sales"   
                },   
                "dataSources" : [{   
                    "originalObject" : {   
                        "dataSource" : "SqlServer sqlcldb2 AS_foodmart_2000"   
                    },   
                    "connectionString" : "Provider=SQLNCLI11;Data Source=sqlcldb2;Initial Catalog=AS_foodmart_2000;Integrated Security=SSPI;Persist Security Info=false"   
                }]   
            }]   
        }   
    }   
```  
  
## Usage (endpoints)  
 This command element is used in  a statement of the [Execute Method &#40;XMLA&#41;](../../analysis-services/xmla/xml-elements-methods-execute.md) call over an XMLA endpoint, exposed in the following ways:  
  
-   As an XMLA window in SQL Server Management Studio (SSMS)  
  
-   As an input file to the **invoke-ascmd** PowerShell cmdlet  
  
-   As an input to an SSIS task or SQL Server Agent job  
  
 You can generate a ready-made script  for this command from SSMS.  For example, you can click the **Script** in a Processing dialog box.  
  
 The [\[MS-SSAS-T\]: QL Server Analysis Services Tabular (SQL Server Technical Protocol)](http://go.microsoft.com/fwlink/p/?LinkId=784855) document includes section 3.1.5.2.2 that describes the structure of JSON tabular metadata commands and objects. Currently, that document covers commands and capabilities not yet implemented in TMSL script. Refer to the topic ([Tabular Model Scripting Language &#40;TMSL&#41; Reference](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md)) for clarification on what is supported.  
  
 For instructions on how to leverage TMSL script in practical solutions, see [Script Administrative Tasks in Analysis Services](../../analysis-services/instances/script-administrative-tasks-in-analysis-services.md).  
  
## See Also  
 [Tabular Model Scripting Language &#40;TMSL&#41; Reference](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md)   
 [Processing Options and Settings &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/processing-options-and-settings-analysis-services.md)  
  
  