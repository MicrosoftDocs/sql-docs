---
title: Azure Synapse Pathway data type mappings Microsoft SQL Server
description: Data type mappings for Microsoft SQL Server within Azure Synapse Pathway
author: charlesfeddersen
ms.author: charlesf
ms.topic: overview
ms.date: 02/10/2022
ms.service: sql
ms.subservice: tools-other
monikerRange: "=azure-sqldw-latest"
ms.custom:
  - template-overview
  - intro-overview
---
# Microsoft SQL Server data type mapping
[!INCLUDE [Azure Synapse Analytics](../../includes/applies-to-version/asa.md)]

Azure Synapse Pathway transpiles SQL code from source systems such as IBM Netezza, Microsoft SQL Server, and Snowflake to T-SQL compliant with Azure Synapse SQL. Synapse Pathway utilizes an Abstract Syntax Tree (AST) model for mapping source data types into supported types.

The following set of links shows the source and target data type mappings for each of the supported source systems.

| Microsoft SQL Server Data Type | Azure Synapse SQL Data Type |
|----- | ----- |
| bigint | bigint |
| binary [ ( *p* ) ] | binary [ ( *p* ) ] |
| bit | bit |
| char [ ( *p* ) ] | char [ ( *p* ) ] |
| cursor | *Not supported* |
| date | date |
| datetime | datetime |
| datetime2 [ ( *p* ) ] | datetime2 [ ( *p* ) ] |
| datetimeoffset [ ( *p* ) ] | datetimeoffset [ ( *p* ) ] |
| decimal [ ( *p* [ , *s* ] ) ] | decimal [ ( *p* [ , *s* ] ) ] |
| float [ ( *p* ) ] | float [ ( *p* ) ] |
| geometry | *Not supported* |
| geography | *Not supported* |
| hierarchyid | *Not supported* |
| image | varbinary(max) |
| int | int |
| money | money |
| nchar [ ( *p* ) ] | nchar [ ( *p* ) ] |
| ntext | nvarchar(max) |
| numeric [ ( *p* [ , *s* ] ) ] | numeric [ ( *p* [ , *s* ] ) ] |
| nvarchar [ ( *p* ) ] | nvarchar [ ( *p* ) ] |
| real | real |
| rowversion | *Not supported* |
| smalldatetime | smalldatetime |
| smallint | smallint |
| smallmoney | smallmoney |
| sql_variant | *Not supported* |
| table | *Not supported* |
| text | varchar(max) |
| time [ ( *p* ) ] | time [ ( *p* ) ] |
| tinyint | tinyint |
| uniqueidentifier | uniqueidentifier |
| varbinary [ ( *p* \| max ) ] | varbinary [ ( *p* \| max ) ] |
| varchar [ ( *p* ) ] | varchar [ ( *p* ) ] |
| xml | nvarchar(max) |

## See Also
- [Azure Synapse Pathway data type mapping](data-type-mappings.md)
- [IBM Netezza data type mapping](data-type-mappings-ibm-netezza.md)
- [Microsoft SQL Server data type mapping](data-type-mappings-microsoft-sql-server.md)
- [Snowflake data type mapping](data-type-mappings-snowflake.md)

## Next steps

[Download Azure Synapse Pathway](synapse-pathway-download.md)
