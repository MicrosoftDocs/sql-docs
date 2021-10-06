---
title: Azure Synapse Pathway data type mappings Teradata Vantage
description: Data type mappings for Teradata Vantage within Azure Synapse Pathway
author: twounder
ms.author: mausher
ms.topic: overview
ms.date: 07/15/2021
ms.prod: sql
ms.technology: tools-other
monikerRange: "=azure-sqldw-latest"
ms.custom:
  - template-overview
  - intro-overview
---
# Teradata Vantage data type mapping
[!INCLUDE [Azure Synapse Analytics](../../includes/applies-to-version/asa.md)]

Azure Synapse Pathway transpiles SQL code from source systems such as IBM Netezza, Microsoft SQL Server, Snowflake, and Teradata Vantage to T-SQL compliant with Azure Synapse SQL. Synapse Pathway utilizes an Abstract Syntax Tree (AST) model for mapping source data types into supported types.

The following set of links shows the source and target data type mappings for each of the supported source systems.

| Teradata Vantage Data Type | Azure Synapse SQL Data Type |
|----- | ----- |
| bigint | bigint |
| bool | bit |
| boolean | bit |
| byteint | tinyint |
| char [ ( *p* ) ] | char [ ( *p* ) ] |
| char varying [ ( *p* ) ] | varchar [ ( *p* ) ] |
| character [ ( *p* ) ] | char [ ( *p* ) ] |
| character varying [ ( *p* ) ] | varchar [ ( *p* ) ] |
| date | date |
| datetime | datetime |
| dec [ ( *p* [ , *s* ] ) ] | decimal [ ( *p* [ , *s* ] ) ] |
| decimal [ ( *p* [ , *s* ] ) ] | decimal [ ( *p* [ , *s* ] ) ] |
| double | float(53) |
| double precision | float(53) |
| float [ ( *p* ) ] | float [ ( *p* ) ] |
| float4 |  float(53) |
| float8 | float(53) |
| int | int |
| int1 | tinyint |
| int2 | smallint |
| int4 | int |
| int8 | bigint |
| integer | integer |
| interval | *Not supported* |
| national char varying [ ( *p* ) ] | nvarchar [ ( *p* ) ] |
| national character [ ( *p* ) ] | nchar [ ( *p* ) ] |
| national character varying [ ( *p* ) ] | nvarchar [ ( *p* ) ] |
| nchar [ ( *p* ) ] | nchar [ ( *p* ) ] |
| numeric [ ( *p* [ , *s* ] ) ] | numeric [ ( *p* [ , *s* ] ) |
| nvarchar [ ( *p* ) ] | nvarchar [ ( *p* ) ] |
| real | real |
| smallint | smallint |
| time | time |
| time with time zone | datetimeoffset |
| time without time zone | time |
| timespan | *Not supported* |
| timestamp | datetime2 |
| timetz | datetimeoffset |
| varchar [ ( *p* ) ] | varchar [ ( *p* ) ]|

## See Also
- [Azure Synapse Pathway data type mapping](data-type-mappings.md)
- [IBM Netezza data type mapping](data-type-mappings-ibm-netezza.md)
- [Microsoft SQL Server data type mapping](data-type-mappings-microsoft-sql-server.md)
- [Snowflake data type mapping](data-type-mappings-snowflake.md)
- [Teradata Vantage data type mapping](data-type-mappings-teradata-vantage.md)

## Next steps

[Download Azure Synapse Pathway](synapse-pathway-download.md)
