---
title: "Roles object (TMSL) | Microsoft Docs"
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
ms.assetid: 1812f60b-bd5f-417c-96bc-3d834bdb4d3c
caps.latest.revision: 6
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Roles object (TMSL)

[!INCLUDE[ssas-appliesto-sql2016-later-aas](../../includes/ssas-appliesto-sql2016-later-aas.md)]

  Defines a role on the model that specifies a collection of permissions. Role membership consists of Windows security principles. You can set filters on a role to restrict access to specific objects.  
  
## Object definition  
 All objects have a common set of properties, including name, type, description, a properties collection, and annotations. **Role** objects also have the following properties.  
  
 modelPermission  
 Establishes a scope of permissions on a database. Valid values are none,  
                  read,  
                  readRefresh,  
                  refresh,  
                  and administrator. See [Roles and Permissions &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/roles-and-permissions-analysis-services.md) for information about database permissions.  
  
 members  
 Members consist of both a member name and ID, where member name is the alias or friendly name of a Windows security principle, and the ID is the security identifier. Both are specified in  role definition.See [SID components](https://msdn.microsoft.com/en-us/library/windows/desktop/aa379597\(v=vs.85\).aspx) for details about identifiers.  
  
 tablePermissions  
 A table permission is a named object with permissions defined via a DAX expression. This property is optional, used to apply a security filter.  
  
## Usage  
 **Role** objects are used in [Alter command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/alter-command-tmsl.md), [Create command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/create-command-tmsl.md), [CreateOrReplace command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/createorreplace-command-tmsl.md), and [Delete command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/delete-command-tmsl.md).  
  
 A **Role** object is a property of a Model, but can also be specified as a property of a Database object given the one-to-one mapping between Model and Database.  
  
 When creating, replacing, or altering a role object, specify all read-write properties of the object definition. Omission of a read-write property is considered a deletion.  
  
## Full Syntax  
 Below is the schema representation of a role object of a model.  
  
```  
"roles": {  
          "type": "array",  
          "items": {  
            "description": "ModelRole object of Tabular Object Model (TOM)",  
            "type": "object",  
            "properties": {  
              "name": {  
                "type": "string"  
              },  
              "description": {  
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
              },  
              "modelPermission": {  
                "enum": [  
                  "none",  
                  "read",  
                  "readRefresh",  
                  "refresh",  
                  "administrator"  
                ]  
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
              },  
              "members": {  
                "type": "array",  
                "items": {  
                  "anyOf": [  
                    {  
                      "description": "WindowsModelRoleMember object of Tabular Object Model (TOM)",  
                      "type": "object",  
                      "properties": {  
                        "memberName": {  
                          "type": "string"  
                        },  
                        "memberId": {  
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
                    },  
                    {  
                      "description": "ExternalModelRoleMember object of Tabular Object Model (TOM)",  
                      "type": "object",  
                      "properties": {  
                        "memberName": {  
                          "type": "string"  
                        },  
                        "memberId": {  
                          "type": "string"  
                        },  
                        "identityProvider": {  
                          "type": "string"  
                        },  
                        "memberType": {  
                          "enum": [  
                            "auto",  
                            "user",  
                            "group"  
                          ]  
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
              "tablePermissions": {  
                "type": "array",  
                "items": {  
                  "description": "TablePermission object of Tabular Object Model (TOM)",  
                  "type": "object",  
                  "properties": {  
                    "name": {  
                      "type": "string"  
                    },  
                    "filterExpression": {  
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
              }  
            },  
            "additionalProperties": false  
          }  
        }  
```  
  
## See Also  
 [Tabular Model Scripting Language &#40;TMSL&#41; Reference](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md)   
 [Roles and Permissions &#40;Analysis Services&#41;](../../analysis-services/multidimensional-models/roles-and-permissions-analysis-services.md)  
  
  