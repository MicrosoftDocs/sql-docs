---
title: Azure Synapse Pathway data type mappings IBM Netezza
description: data type mappings for IBM Netezza within Azure Synapse Pathway
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
# IBM Netezza data type mapping
[!INCLUDE [Azure Synapse Analytics](../../includes/applies-to-version/asa.md)]

Azure Synapse Pathway transpiles SQL code from source systems such as IBM Netezza, Microsoft SQL Server, and Snowflake to T-SQL compliant with Azure Synapse SQL. Synapse Pathway utilizes an Abstract Syntax Tree (AST) model for mapping source data types into supported types.

The following set of links shows the source and target data type mappings for each of the supported source systems.

| IBM Netezza Data Type | Azure Synapse SQL Data Type |
|----- | ----- |
| array | *Not supported* |
| bigint | bigint |
| binary large object [ ( n [ K \| M \| G ] ) ] | nvarchar [ ( n \| max ) ] |
| blob [ ( n [ K \| M \| G ] ) ] | nvarchar [ ( n \| max ) ] |
| byte [ ( n ) ] | binary [ ( n ) ] \| varbinary(max) |
| byteint | smallint |
| char varying [ ( n ) ] | varchar [ ( n \| max) ] |
| character varying [ ( n ) ] | varchar [ ( n \| max) ] |
| char [ ( n ) ] | char [ ( n ) ] \| varchar(max) |
| character [ ( n ) ] | char [ ( n ) ] \| varchar(max) |
| character large object [ ( n [ K \| M \| G ] ) ] | varchar [ ( n \|  max ) |
| clob [ ( n [ K \| M \| G ] ) ] | varchar [ ( n \|  max ) |
| dataset | *Not supported* |
| date | date |
| dec [ ( p [ , s] ) ] | decimal [ ( p [, s] ) ] |
| decimal [ ( p [ , s] ) ] | decimal [ ( p [, s] ) ] |
| double precision | float(53) |
| float [ ( n ) ] | float [ ( n ) ] |
| graphic [ ( n ) ] | nchar [ ( n ) ] \| varchar(max) |
| interval | *Not supported* |
| json [ ( n ) ] | nvarchar [ ( n \| max) ] |
| long varchar | nvarchar(max) |
| long vargraphic | nvarchar(max) |
| mbb | *Not supported* |
| mbr | *Not supported* |
| number [ ( ( p \| *) [ , s ] ) ] | numeric [ ( p [, s] ) ] |
| numeric [ ( p [, s] ) ] | numeric [ ( p [, s] ) ] |
| period | *Not supported* |
| real | real |
| smallint | smallint |
| st_geometry | *Not supported* |
| time | time |
| time with time zone | datetimeoffset |
| timestamp | datetime2 |
| timestamp with time zone | datetimeoffset |
| varbyte | varbinary [ ( n \| max ) ] |
| varchar [ ( n ) ]| varchar [ ( n ) ] |
| vargraphic [ ( n ) ] | nvarchar [ ( n \| max ) ] |
| varray | *Not supported* |
| xml | *Not supported* |
| xmltype | *Not supported* |

## See Also
- [Azure Synapse Pathway data type mapping](data-type-mappings.md)
- [IBM Netezza data type mapping](data-type-mappings-ibm-netezza.md)
- [Microsoft SQL Server data type mapping](data-type-mappings-microsoft-sql-server.md)
- [Snowflake data type mapping](data-type-mappings-snowflake.md)

## Next steps

[Download Azure Synapse Pathway](synapse-pathway-download.md)
