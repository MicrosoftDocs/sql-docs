---
title: "sys.sensitivity_classifications (Transact-SQL)"
description: sys.sensitivity_classifications (Transact-SQL)
author: Madhumitatripathy
ms.author: matripathy
ms.date: 04/19/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: "reference"
f1_keywords:
  - "sys.sensitivity_classifications "
helpviewer_keywords:
  - "sys.sensitivity_classifications statement"
  - "dropping labels"
  - "drop labels"
  - "removing labels"
  - "remove labels"
  - "classification [SQL]"
  - "labels [SQL]"
  - "information types"
  - "rank"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-ver15||=azuresqldb-current||=azure-sqldw-latest"
---
# sys.sensitivity_classifications (Transact-SQL)
[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

Returns a row for each classified item in the database.

|Column name|Data type|Description|
|-----------------|---------------|-----------------|  
|**class**|**int**|Identifies the class of the item on which the classification exists. Will always have the value 1 (representing a column)|  
|**class_desc**|**varchar(16)**|A description of the class of the item on which the classification exists. will always have the value *OBJECT_OR_COLUMN*|  
|**major_id**|**int**|Represents the ID of the table containing the classified column, corresponding with sys.all_objects.object_id|  
|**minor_id**|**int**|Represents the ID of the column on which the classification exists, corresponding with sys.all_columns.column_id|   
|**label**|**sysname**|The label (human readable) assigned for the sensitivity classification|  
|**label_id**|**sysname**|An ID associated with the label, which can be used by an information protection system such as Azure Information Protection (AIP)|  
|**information_type**|**sysname**|The information type (human readable) assigned for the sensitivity classification|  
|**information_type_id**|**sysname**|An ID associated with the information type, which can be used by an information protection system such as Azure Information Protection (AIP)|  
|**rank**|**int**|A numerical value of the rank: <br><br>0 for NONE<br>10 for LOW<br>20 for MEDIUM<br>30 for HIGH<br>40 for CRITICAL| 
|**rank_desc**|**sysname**|Textual representation of the rank:  <br><br>NONE, LOW, MEDIUM, HIGH, CRITICAL|  

## Remarks  

- This view provides visibility into the classification state of the database. It can be used for managing the database classifications, as well as for generating reports.
- Currently only classification of database columns is supported.
 
## Examples

### A. Listing all classified columns and their corresponding classification

The following example returns a table that lists the table name, column name, label, label ID, information type, information type ID, rank, and rank description for each classified column in the database.

> [!NOTE]
> Label is a keyword for Azure Synapse Analytics.

```sql
SELECT
    SCHEMA_NAME(sys.all_objects.schema_id) as SchemaName,
    sys.all_objects.name AS [TableName], sys.all_columns.name As [ColumnName],
    [Label], [Label_ID], [Information_Type], [Information_Type_ID], [Rank], [Rank_Desc]
FROM
          sys.sensitivity_classifications
left join sys.all_objects on sys.sensitivity_classifications.major_id = sys.all_objects.object_id
left join sys.all_columns on sys.sensitivity_classifications.major_id = sys.all_columns.object_id
                         and sys.sensitivity_classifications.minor_id = sys.all_columns.column_id
```

## Permissions  
 Requires the **VIEW ANY SENSITIVITY CLASSIFICATION** permission. 
 
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  

## See Also  

[ADD SENSITIVITY CLASSIFICATION (Transact-SQL)](../../t-sql/statements/add-sensitivity-classification-transact-sql.md)

[DROP SENSITIVITY CLASSIFICATION (Transact-SQL)](../../t-sql/statements/drop-sensitivity-classification-transact-sql.md)

[Getting started with SQL Information Protection](/azure/azure-sql/database/data-discovery-and-classification-overview)
