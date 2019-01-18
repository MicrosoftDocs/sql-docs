---
title: "sys.sensitivity_classifications (Transact-SQL) | Microsoft Docs"
ms.date: 06/17/2018
ms.reviewer: ""
ms.prod: sql
ms.technology: t-sql
ms.topic: "language-reference"
ms.custom: ""
ms.manager: craigg
ms.author: giladm
author: giladmit
f1_keywords:
  - "sys.sensitivity_classifications "
dev_langs:
  - "TSQL"
helpviewer_keywords:
  - "sys.sensitivity_classifications statement"
  - "dropping labels"
  - "drop labels"
  - "removing labels"
  - "remove labels"
  - "classification [SQL]"
  - "labels [SQL]"
  - "information types"
monikerRange: "= azuresqldb-current || = sqlallproducts-allversions"
---
# sys.sensitivity_classifications (Transact-SQL)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

Returns a row for each classified item in the database.

|Column name|Data type|Description|
|-----------------|---------------|-----------------|  
|**class**|**int**|Identifies the class of the item on which the classification exists|  
|**class_desc**|**varchar(16)**|A description of the class of the item on which the classification exists|  
|**major_id**|**int**|ID of the item on which the classification exists.<br \><br \>If class is 0, major_id is always 0.<br>If class is 1, 2, or 7 major_id is object_id.|  
|**minor_id**|**int**|Secondary ID of the item on which the classification exists, interpreted according to its class.<br><br>If class = 1, minor_id is the column_id (if column), else 0 (if object).<br>If class = 2, minor_id is the parameter_id.<br>If class = 7, minor_id is the index_id. |  
|**label**|**sysname**|The label (human readable) assigned for the sensitivity classification|  
|**label_id**|**sysname**|An ID associated with the label, which can be used by an information protection system such as Azure Information Protection (AIP)|  
|**information_type**|**sysname**|The information type (human readable) assigned for the sensitivity classification|  
|**information_type_id**|**sysname**|An ID associated with the information type, which can be used by an information protection system such as Azure Information Protection (AIP)|  
| &nbsp; | &nbsp; | &nbsp; |

## Remarks  

- This view provides visibility into the classification state of the database. It can be used for managing the database classifications, as well as for generating reports.
- Currently only classification of database columns is supported. Consequently:
    - **class** - will always have the value 1 (representing a column)
    - **class_desc** - will always have the value *OBJECT_OR_COLUMN*
    - **major_id** - represents the ID of the table containing the classified column, corresponding with sys.all_objects.object_id
    - **minor_id** - represents the ID of the column on which the classification exists, corresponding with sys.all_columns.column_id

## Examples

### A. Listing all classified columns and their corresponding classification

The following example returns a table listing the table name, column name, label, label ID, information type, information type ID for each classified column in the database.

```sql
SELECT
    sys.all_objects.name AS TableName, sys.all_columns.name As ColumnName,
    Label, Label_ID, Information_Type, Information_Type_ID
FROM
          sys.sensitivity_classifications
left join sys.all_objects on sys.sensitivity_classifications.major_id = sys.all_objects.object_id
left join sys.all_columns on sys.sensitivity_classifications.major_id = sys.all_columns.object_id
                         and sys.sensitivity_classifications.minor_id = sys.all_columns.column_id
```

## See Also  

[ADD SENSITIVITY CLASSIFICATION (Transact-SQL)](../../t-sql/statements/add-sensitivity-classification-transact-sql.md)

[DROP SENSITIVITY CLASSIFICATION (Transact-SQL)](../../t-sql/statements/drop-sensitivity-classification-transact-sql.md)

[Getting started with SQL Information Protection](https://aka.ms/sqlip)
