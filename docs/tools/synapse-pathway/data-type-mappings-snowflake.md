---
title: Azure Synapse Pathway Data Type Mappings Snowfalke
description: Data Type Mappings for Snowflake within Azure Synapse Pathway
author: twounder
ms.author: mausher
ms.topic: overview 
ms.date: 07/14/2021
ms.prod: sql
ms.technology: tools-other
monikerRange: "=azure-sqldw-latest"
ms.custom: template-overview 
---
# Snowflake Data Type Mapping
[!INCLUDE [Azure Synapse Analytics](../../includes/applies-to-version/asa.md)]

Azure Synapse Pathway transpiles SQL code from source systems such as IBM Netezza, Microsoft SQL Server, Snowflake, and Teradata Vantage to T-SQL compliant with Azure Synapse SQL. Synapse Pathway utilizes an Abstract Syntax Tree (AST) model for mapping source data types into supported types.

The following set of links show the source and target data type mappings for each of the supported source systems.

| Snowflake Data Type | Azure Synapse SQL Data Type |
|----- | ----- |
| array | *Not Supported* |
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
| geography | *Not Supported* |
| int | int |
| integer | integer |
| number [ ( *p* [ , *s* ] ) ] | numeric [ ( *p* [ , *s* ] ) ] |
| object | *Not Supported* |
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
| variant | *Not Supported* |

## See Also
- [Azure Synapse Pathway Data Type Mapping](data-type-mappings.md)
- [IBM Netezza Data Type Mapping](data-type-mappings-ibm-netezza.md)
- [Microsoft SQL Server Data Type Mapping](data-type-mappings-microsoft-sql-server.md)
- [Snowflake Data Type Mapping](data-type-mappings-snowflake.md)
- [Teradata Vantage Data Type Mapping](data-type-mappings-teradata-vantage.md)

## Next steps

[Download Azure Synapse Pathway](synapse-pathway-download.md)
