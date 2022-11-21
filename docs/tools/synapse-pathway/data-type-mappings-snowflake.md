---
title: Azure Synapse Pathway data type mappings Snowflake
description: Data type mappings for Snowflake within Azure Synapse Pathway
author: charlesfeddersen
ms.author: charlesf
ms.topic: overview
ms.date: 07/15/2021
ms.service: sql
ms.subservice: tools-other
monikerRange: "=azure-sqldw-latest"
ms.custom:
  - template-overview
  - intro-overview
---
# Snowflake data type mapping
[!INCLUDE [Azure Synapse Analytics](../../includes/applies-to-version/asa.md)]

Azure Synapse Pathway transpiles SQL code from source systems such as IBM Netezza, Microsoft SQL Server, and Snowflake to T-SQL compliant with Azure Synapse SQL. Synapse Pathway utilizes an Abstract Syntax Tree (AST) model for mapping source data types into supported types.

The following set of links shows the source and target data type mappings for each of the supported source systems.

| Snowflake Data Type | Azure Synapse SQL Data Type |
|----- | ----- |
| array | *Not supported* |
| bigint | bigint |
| binary [ ( *p* ) ] | binary [ ( *p* ) ] |
| boolean | bit |
| char [ ( *p* ) ] | char [ ( *p* ) ] |
| character [ ( *p* ) ] | char [ ( *p* ) ] |
| date | date |
| datetime | datetime |
| decimal  [ ( *p* [ , *s* ] ) ] | decimal [ ( *p* [ , *s* ] ) ] |
| double | float(53) |
| double precision | float(53)  |
| float [ ( *p* ) ] | float [ ( *p* ) ] |
| float4 | float(53) |
| float8 | float(53) |
| geography | *Not supported* |
| int | int |
| integer | integer |
| number [ ( *p* [ , *s* ] ) ] | numeric [ ( *p* [ , *s* ] ) ] |
| object | *Not supported* |
| real | real |
| smallint | smallint |
| string | varchar |
| text | varchar |
| time | time |
| timestamp | datetime2 |
| timestamp_ltz | datetimeoffset |
| timestamp_ntz | datetime2 |
| timestamp_tz | datetimeoffset |
| varbinary [ ( *p* ) ] | varbinary [ ( *p* ) ] |
| varchar [ ( *p* ) ] | varchar [ ( *p* ) ] |
| variant | *Not supported* |

## See Also
- [Azure Synapse Pathway data type mapping](data-type-mappings.md)
- [IBM Netezza data type mapping](data-type-mappings-ibm-netezza.md)
- [Microsoft SQL Server data type mapping](data-type-mappings-microsoft-sql-server.md)
- [Snowflake data type mapping](data-type-mappings-snowflake.md)

## Next steps

[Download Azure Synapse Pathway](synapse-pathway-download.md)
