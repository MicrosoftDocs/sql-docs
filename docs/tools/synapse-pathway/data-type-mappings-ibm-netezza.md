---
title: Azure Synapse Pathway Data Type Mappings IBM Netezza
description: data type mappings for IBM Netezza within Azure Synapse Pathway
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: charlesf
ms.date: 12/16/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: reference
ms.collection:
  - data-tools
ms.custom:
  - template-overview
  - intro-overview
monikerRange: "=azure-sqldw-latest"
---
# IBM Netezza data type mapping

[!INCLUDE [Azure Synapse Analytics](../../includes/applies-to-version/asa.md)]

Azure Synapse Pathway transpiles SQL code from source systems such as IBM Netezza, Microsoft SQL Server, and Snowflake to T-SQL compliant with Azure Synapse SQL. Synapse Pathway utilizes an Abstract Syntax Tree (AST) model for mapping source data types into supported types.

The following set of links shows the source and target data type mappings for each of the supported source systems.

| IBM Netezza Data Type | Azure Synapse SQL Data Type |
| --- | --- |
| array | *Not supported* |
| bigint | **bigint** |
| binary large object [ ( n [ K \| M \| G ] ) ] | **nvarchar(*n*)** or **nvarchar(max)** |
| blob [ ( n [ K \| M \| G ] ) ] | **nvarchar(*n*)** or **nvarchar(max)** |
| byte [ ( n ) ] | **binary(*n*)** or **varbinary(max)** |
| byteint | **smallint** |
| char varying [ ( n ) ] | **varchar(*n*)** or **varchar(max)** |
| character varying [ ( n ) ] | **varchar(*n*)** or **varchar(max)** |
| char [ ( n ) ] | **char(*n*)** or **varchar(max)** |
| character [ ( n ) ] | **char(*n*)** or **varchar(max)** |
| character large object [ ( n [ K \| M \| G ] ) ] | **varchar(*n*)** or **varchar(max)** |
| clob [ ( n [ K \| M \| G ] ) ] | **varchar(*n*)** or **varchar(max)** |
| dataset | *Not supported* |
| date | **date** |
| dec [ ( p [ , s] ) ] | **decimal(*p*)** or **decimal(*p*, *s*)** |
| decimal [ ( p [ , s] ) ] | **decimal(*p*)** or **decimal(*p*, *s*)** |
| double precision | **float(53)** |
| float [ ( n ) ] | **float(*n*)** |
| graphic [ ( n ) ] | **nchar(*n*)** or **nvarchar(max)** |
| interval | *Not supported* |
| json [ ( n ) ] | **nvarchar(*n*)** or **nvarchar(max)** |
| long varchar | **nvarchar(max)** |
| long vargraphic | **nvarchar(max)** |
| mbb | *Not supported* |
| mbr | *Not supported* |
| number [ ( ( p \| *) [ , s ] ) ] | **numeric(*p*)** or **numeric(*p*, *s*)** |
| numeric [ ( p [, s] ) ] | **numeric(*p*)** or **numeric(*p*, *s*)** |
| period | *Not supported* |
| real | **real** |
| smallint | **smallint** |
| st_geometry | *Not supported* |
| time | **time** |
| time with time zone | **datetimeoffset** |
| timestamp | **datetime2** |
| timestamp with time zone | **datetimeoffset** |
| varbyte | **varbinary(*n*)** or **varbinary(max)** |
| varchar [ ( n ) ] | **varchar(*n*)** |
| vargraphic [ ( n ) ] | **nvarchar(*n*)** or **nvarchar(max)** |
| varray | *Not supported* |
| xml | *Not supported* |
| xmltype | *Not supported* |

## Next step

> [!div class="nextstepaction"]
> [Azure Synapse Pathway download](synapse-pathway-download.md)

## Related content

- [Data type mappings in Azure Synapse Pathway](data-type-mappings.md)
- [IBM Netezza data type mapping](data-type-mappings-ibm-netezza.md)
- [Microsoft SQL Server data type mapping](data-type-mappings-microsoft-sql-server.md)
- [Snowflake data type mapping](data-type-mappings-snowflake.md)
