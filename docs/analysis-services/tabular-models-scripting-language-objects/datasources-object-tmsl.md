---
title: "DataSources object (TMSL) | Microsoft Docs"
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
ms.assetid: 1357ae7e-30a4-481a-831c-7b046fe15aa4
caps.latest.revision: 9
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DataSources object (TMSL)

[!INCLUDE[ssas-appliesto-sql2016-later-aas](../includes/ssas-appliesto-sql2016-later-aas.md)]

  Defines a connection to a data source used by the model either during import to add data to the model, or in pass through queries via DirectQuery mode.  Models in DirectQuery mode can only have one **DataSource** object.  
  
 Unless you are creating, replacing, or altering the  data source object itself, any data source referenced in your script (such as in partition script) must be an existing **DataSource** object in your model.  
  
## Object definition  
 All objects have a common set of properties, including name, type, description, a properties collection, and annotations. **DataSource** objects also have the following properties.  
  
 type  
 The type of DataSource. At present, the only valid value is Provider (1) - Normal connection string.  
  
 connectionString  
 The connection string that minimally specifies the server and database, but can also include other properties supported by the external RDBMS, such as a data provider or user account. This value is required. See [SqlConnectionStringBuilder Class](https://msdn.microsoft.com/en-us/library/ms254500\(v=vs.110\).aspx) for details about SQL Server database connection string properties.  
  
 impersonationMode  
 Specifies whether Analysis Services should impersonate the identity of the user requesting the query. This property is a numeric value that specifies the credentials to use for impersonation. The enumeration values are as follows:  
  
-   Default (1) - The server uses the impersonation method that it deems to be appropriate for the context in which impersonation is used.  
  
-   ImpersonateAccount (2) - The server uses the specified user account.  
  
-   ImpersonateAnonymous (3) - The server uses the anonymous user account.  This option is not recommended, but is sometimes used in HTTP access scenarios by custom applications that handle authentication.  
  
-   ImpersonateCurrentUser (4) - The server uses the user account that the client is connecting as.  
  
-   ImpersonateServiceAccount (5) - The server uses the user account that the server is running as.  
  
-   ImpersonateUnattendedAccount (6) – The server uses an unattended user account. This is used for Power Pivot or Tabular models that run in a SharePoint environment.  
  
 DirectQuery mode can use impersonateCurrentuser if Analysis Services is configured for trusted delegation, or  
                      impersonateServiceAccount if the query request is made in the security context of the Analysis Services service account. See [Configure Analysis Services for Kerberos constrained delegation](../../analysis-services/instances/configure-analysis-services-for-kerberos-constrained-delegation.md).  
  
 account  
 Used for impersonation. A Windows or database account that has a valid login with read permissions on the external database.  
  
 password  
 An encrypted string providing the password of the account.  
  
 maxConnections  
 The maximum number of connections to be opened concurrently to the data source.  
  
 isolation  
 The kind of isolation that is used when executing commands against the data source. Valid values are ReadCommitted (1) or Snapshot (2).  
  
 timeout  
 An integer that specifies the timeout in seconds for commands executed against the data source.  
  
 provider  
 An optional string that identifies the name of the managed data provider used on the connection to the relational database, if not otherwise specified on the connection string.  
  
## Usage  
 **DataSource** objects are used in [Alter command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/alter-command-tmsl.md), [Create command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/create-command-tmsl.md), [CreateOrReplace command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/createorreplace-command-tmsl.md), [Delete command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/delete-command-tmsl.md), [Refresh command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/refresh-command-tmsl.md), and [MergePartitions command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/mergepartitions-command-tmsl.md).  
  
 A **DataSource** object is a property of a Model, but can also be specified as a property of a Database object given the one-to-one mapping between Model and Database.  Partitions based on SQL queries also specify a **DataSource**, only with a reduced set of properties.  
  
 When creating, replacing, or altering a data source object, specify all read-write properties of the object definition. Omission of a read-write property is considered a deletion.  
  
## Examples  
 **Example 1** - a connection to a *FoodMart* database on a remote named instance of *Sales* on a network server named *Server01*.  
  
```  
"dataSources": [  
      {  
        "name": "SqlServer Server01 FoodMart",  
        "connectionString": "Provider=SQLNCLI11;Data Source=Server01\Sales;Initial Catalog=Foodmart;Integrated Security=SSPI;Persist Security Info=false",  
        "impersonationMode": "impersonateServiceAccount",  
      }  
]  
```  
  
## Full Syntax  
 Below is the schema representation of a data source object of a model.  
  
```  
"dataSources": {  
          "type": "array",  
          "items": {  
            "anyOf": [  
              {  
                "description": "ProviderDataSource object of Tabular Object Model (TOM)",  
                "type": "object",  
                "properties": {  
                  "name": {  
                    "type": "string"  
                  },  
                  "description": {  
                    "type": "string"  
                  },  
                  "type": {  
                    "enum": [  
                      "provider"  
                    ]  
                  },  
                  "connectionString": {  
                    "type": "string"  
                  },  
                  "impersonationMode": {  
                    "enum": [  
                      "default",  
                      "impersonateAccount",  
                      "impersonateAnonymous",  
                      "impersonateCurrentUser",  
                      "impersonateServiceAccount",  
                      "impersonateUnattendedAccount"  
                    ]  
                  },  
                  "account": {  
                    "type": "string"  
                  },  
                  "password": {  
                    "type": "string"  
                  },  
                  "maxConnections": {  
                    "type": "integer"  
                  },  
                  "isolation": {  
                    "enum": [  
                      "readCommitted",  
                      "snapshot"  
                    ]  
                  },  
                  "timeout": {  
                    "type": "integer"  
                  },  
                  "provider": {  
                    "type": "string"  
                  },  
                  "annotations": {  
                    "type": "array",  
                    "items": {  
                      "description": "Annotation object of Tabular Object Model (TOM)",  
                      "type": "object",  
                      "properties": {  
                        "name": {  
                          "type": "string"  
                        },  
                        "value": {  
                          "anyOf": [  
                            {  
                              "type": "string"  
                            },  
                            {  
                              "type": "array",  
                              "items": {  
                                "type": "string"  
                              }  
                            }  
                          ]  
                        }  
                      },  
                      "additionalProperties": false  
                    }  
                  }  
                },  
                "additionalProperties": false  
              }  
            ]  
          }  
        },  
  
```  
  
## See Also  
 [Tabular Model Scripting Language &#40;TMSL&#41; Reference](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md)   
 [DirectQuery Mode &#40;SSAS Tabular&#41;](../../analysis-services/tabular-models/directquery-mode-ssas-tabular.md)   
 [Configure HTTP Access to Analysis Services on Internet Information Services &#40;IIS&#41; 8.0](../../analysis-services/instances/configure-http-access-to-analysis-services-on-iis-8-0.md)  
  
  