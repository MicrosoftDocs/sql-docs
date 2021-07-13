---
title: Azure Synapse Pathway data type mappings IBM Netezza
description: data type mappings for IBM Netezza within Azure Synapse Pathway
author: twounder
ms.author: mausher
ms.topic: overview 
ms.date: 07/14/2021
ms.prod: sql
ms.technology: tools-other
monikerRange: "=azure-sqldw-latest"
ms.custom: template-overview 
---
# IBM Netezza data type mapping
[!INCLUDE [Azure Synapse Analytics](../../includes/applies-to-version/asa.md)]

Azure Synapse Pathway transpiles SQL code from source systems such as IBM Netezza, Microsoft SQL Server, Snowflake, and Teradata Vantage to T-SQL compliant with Azure Synapse SQL. Synapse Pathway utilizes an Abstract Syntax Tree (AST) model for mapping source data types into supported types.

The following set of links show the source and target data type mappings for each of the supported source systems.

| IBM Netezza Data Type | Azure Synapse SQL Data Type |
|----- | ----- |
| array | *Not Supported* |
| bigint | bigint |
| binary large object / blob [ ( *p* [ K \| M \| G ] ) ] | nvarchar [ ( *p* \| max ) ] |
| byte [ ( *p* ) ] | binary [ ( *p* ) ] / varbinary(max) |
| byteint | smallint |
| char varying [ ( *p* ) ] | varchar [ ( *p* \| max) ] |
| character varying [ ( *p* ) ] | varchar [ ( *p* \| max) ] |
| char [ ( *p* ) ] | char [ ( *p* ) ] / varchar(max)|
| character [ ( *p* ) ] | char [ ( *p* ) ] / varchar(max)|
| character large object / clob [ ( *p* [ K \| M \| G ]) ] |  varchar [ ( *p* \|  max ) |
| dataset | *Not Supported* |
| date | date |
| dec/decimal [ ( *p* [ , *s*] ) ] | decimal [ ( *p* [, *s*] ) ] |
| double precision | float(53)|
| float [ ( *p* ) ] | float [ ( *p* ) ] |
| graphic [ ( *p* ) ] | nchar [ ( *p* ) ] \| varchar(max) |
| interval | *Not Supported* |
| json [ ( *p* ) ] | nvarchar [ ( *p* \| max) ] |
| long varchar | nvarchar(max) |
| long vargraphic | nvarchar(max) |
| mbb | *Not Supported* |
| mbr | *Not Supported* |
| number [ ( ( *p* \| *) [ , *s* ] )   ] | numeric [ ( *p* [, *s*] ) ] |
| numeric [ ( *p* [, *s*] ) ] | numeric [ ( *p* [, *s*] ) ] |
| period | *Not Supported* |
| real | real |
| smallint | smallint |
| st_geometry | *Not Supported* |
| time | time |
| time with time zone | datetimeoffset |
| timestamp | datetime2 |
| timestamp with time zone | datetimeoffset |
| varbyte | varbinary [ ( *p* \| max ) ] |
| varchar [ ( *p* ) ] | varchar [ ( *p* ) ] |
| vargraphic [ ( *p* ) ] | nvarchar [ ( *p* \| max ) ] |
| varray | *Not Supported* |
| xml/xmltype | *Not Supported* |

## See Also
- [Azure Synapse Pathway data type mapping](data-type-mappings.md)
- [IBM Netezza data type mapping](data-type-mappings-ibm-netezza.md)
- [Microsoft SQL Server data type mapping](data-type-mappings-microsoft-sql-server.md)
- [Snowflake data type mapping](data-type-mappings-snowflake.md)
- [Teradata Vantage data type mapping](data-type-mappings-teradata-vantage.md)

## Next steps

[Download Azure Synapse Pathway](synapse-pathway-download.md)
