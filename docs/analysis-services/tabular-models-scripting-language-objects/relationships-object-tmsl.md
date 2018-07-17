---
title: "Relationships object (TMSL) | Microsoft Docs"
ms.date: 05/07/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: tmsl
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Relationships object (TMSL)
[!INCLUDE[ssas-appliesto-sqlas-aas](../../includes/ssas-appliesto-sqlas-aas.md)]
  Defines a relationship between a source and target table, with the ability to specify cardinality, and the direction of query and security filters.  
  
## Object definition  
 All objects have a common set of properties, including name, type, description, a properties collection, and annotations. **Relationship** objects also have the following properties.  
  
 isActive  
 A Boolean that indicates whether the relationship is marked as Active or Inactive. An Active relationship is automatically used for filtering across tables. An Inactive relationship can be used explicitly by DAX calculations with the USERELATIONSHIP function.  
  
 crossFilteringBehavior  
 Indicates how relationships influence filtering of data. See [Bi-directional cross filters for tabular models in SQL Server 2016 Analysis Services](../../analysis-services/tabular-models/bi-directional-cross-filters-tabular-models-analysis-services.md) for more information. Valid values are as follows:  
  
-   OneDirection (1) - The rows selected in the "To" end of the relationship will automatically filter scans of the table in the "From" end of the relationship.  
  
-   BothDirections (2) - Filters on either end of the relationship will automatically filter the other table.  
  
-   Automatic (3) - The engine will analyze the relationships and choose one of the behaviors by using heuristics.  
  
 joinOnDateBehavior  
 When joining two date time columns, indicates whether to join on date and time parts or on date part only.  
  
-   DateAndTime (1) - When joining two date time columns, join on date and time parts.  
  
-   DatePartOnly (2) - When joining two date time columns, join on date part only.  
  
 relyOnReferentialIntegrity  
 Unused; reserved for future use.  
  
 securityFilteringBehavior  
 An enumeration that indicates how relationships influence filtering of data when evaluating row-level security expressions. Valid values are as follows:  
  
-   OneDirection (1) - The rows selected in the "To" end of the relationship will automatically filter scans of the table in the "From" end of the relationship.  
  
-   BothDirections (2) - Filters on either end of the relationship will automatically filter the other table.  
  
## Usage  
 Relationship objects are used in [Alter command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/alter-command-tmsl.md), [Create command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/create-command-tmsl.md), [CreateOrReplace command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/createorreplace-command-tmsl.md), and [Delete command &#40;TMSL&#41;](../../analysis-services/tabular-models-scripting-language-commands/delete-command-tmsl.md).  
  
 When creating, replacing, or altering a relationship object, specify all read-write properties of the object definition. Omission of a read-write property is considered a deletion.  
  
## Full Syntax  
 Below is the schema representation of a relationship object.  
  
```  
"relationships": {  
          "type": "array",  
          "items": {  
            "anyOf": [  
              {  
                "description": "SingleColumnRelationship object of Tabular Object Model (TOM)",  
                "type": "object",  
                "properties": {  
                  "name": {  
                    "type": "string"  
                  },  
                  "isActive": {  
                    "type": "boolean"  
                  },  
                  "type": {  
                    "enum": [  
                      "singleColumn"  
                    ]  
                  },  
                  "crossFilteringBehavior": {  
                    "enum": [  
                      "oneDirection",  
                      "bothDirections",  
                      "automatic"  
                    ]  
                  },  
                  "joinOnDateBehavior": {  
                    "enum": [  
                      "dateAndTime",  
                      "datePartOnly"  
                    ]  
                  },  
                  "relyOnReferentialIntegrity": {  
                    "type": "boolean"  
                  },  
                  "securityFilteringBehavior": {  
                    "enum": [  
                      "oneDirection",  
                      "bothDirections"  
                    ]  
                  },  
                  "fromCardinality": {  
                    "enum": [  
                      "none",  
                      "one",  
                      "many"  
                    ]  
                  },  
                  "toCardinality": {  
                    "enum": [  
                      "none",  
                      "one",  
                      "many"  
                    ]  
                  },  
                  "fromColumn": {  
                    "type": "string"  
                  },  
                  "fromTable": {  
                    "type": "string"  
                  },  
                  "toColumn": {  
                    "type": "string"  
                  },  
                  "toTable": {  
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
        }  
```  
  
## See Also  
 [Tabular Model Scripting Language &#40;TMSL&#41; Reference](../../analysis-services/tabular-model-scripting-language-tmsl-reference.md)   
 [Create Relationships](../../integration-services/data-flow/transformations/create-relationships.md)  
  
  
