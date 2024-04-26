---
title: "sys.extended_properties (Transact-SQL)"
description: Extended properties catalog views - sys.extended_properties
author: rwestMSFT
ms.author: randolphwest
ms.date: 04/25/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.extended_properties"
  - "sys.extended_properties_TSQL"
  - "extended_properties"
  - "extended_properties_TSQL"
helpviewer_keywords:
  - "sys.extended_properties catalog view"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric"
---
# Extended properties catalog views - sys.extended_properties

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW Fabric SE Fabric DW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw.md)]

Returns a row for each extended property in the current database.

| Column name | Data type | Description |
| --- | --- | --- |
| `class` | **tinyint** | Identifies the class of item on which the property exists. Can be one of the following values:<br /><br />`0` = Database<br />`1` = Object or column<br />`2` = Parameter<br />`3` = Schema<br />`4` = Database principal<br />`5` = Assembly<br />`6` = Type<br />`7` = Index<br />`8` = User defined table type column<br />`10` = XML schema collection<br />`15` = Message type<br />`16` = Service contract<br />`17` = Service<br />`18` = Remote service binding<br />`19` = Route<br />`20` = Dataspace (filegroup or partition scheme)<br />`21` = Partition function<br />`22` = Database file<br />`27` = Plan guide |
| `class_desc` | **nvarchar(60)** | Description of the class on which the extended property exists. Can be one of the following values:<br /><br />`DATABASE`<br />`OBJECT_OR_COLUMN`<br />`PARAMETER`<br />`SCHEMA`<br />`DATABASE_PRINCIPAL`<br />`ASSEMBLY`<br />`TYPE`<br />`INDEX`<br />`XML_SCHEMA_COLLECTION`<br />`MESSAGE_TYPE`<br />`SERVICE_CONTRACT`<br />`SERVICE`<br />`REMOTE_SERVICE_BINDING`<br />`ROUTE`<br />`DATASPACE`<br />`PARTITION_FUNCTION`<br />`DATABASE_FILE`<br />`PLAN_GUIDE` |
| `major_id` | **int** | ID of the item on which the extended property exists, interpreted according to its class. For most items, this is the ID that applies to what the class represents. Interpretation for nonstandard major IDs is as follows:<br /><br />If `class` is `0`, `major_id` is always `0`.<br />If `class` is `1`, `2`, or `7`, `major_id` is `object_id`. |
| `minor_id` | **int** | Secondary ID of the item on which the extended property exists, interpreted according to its class. For most items this is `0`; otherwise, the ID is as follows:<br /><br />If `class` is `1`, `minor_id` is the `column_id` if column, else `0` if object.<br />If `class` is `2`, `minor_id` is the `parameter_id`.<br />If `class` is `7`, `minor_id` is the `index_id`. |
| `name` | **sysname** | Property name, unique with `class`, `major_id`, and `minor_id`. |
| `value` | **sql_variant** | Value of the extended property. |

## Permissions

[!INCLUDE [ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../security/metadata-visibility-configuration.md).

## Related content

- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [System catalog views (Transact-SQL)](catalog-views-transact-sql.md)
- [sys.fn_listextendedproperty (Transact-SQL)](../system-functions/sys-fn-listextendedproperty-transact-sql.md)
- [sp_addextendedproperty (Transact-SQL)](../system-stored-procedures/sp-addextendedproperty-transact-sql.md)
- [sp_dropextendedproperty (Transact-SQL)](../system-stored-procedures/sp-dropextendedproperty-transact-sql.md)
- [sp_updateextendedproperty (Transact-SQL)](../system-stored-procedures/sp-updateextendedproperty-transact-sql.md)
