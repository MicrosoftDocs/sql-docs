---
title: Azure Synapse Pathway data type mappings
description: Data type mappings for source platforms within Azure Synapse Pathway
author: charlesfeddersen
ms.author: charlesf
ms.topic: overview
ms.date: 07/14/2021
ms.service: sql
ms.subservice: tools-other
monikerRange: "=azure-sqldw-latest"
ms.custom:
  - template-overview
  - intro-overview
---
# Data type mappings in Azure Synapse Pathway
[!INCLUDE [Azure Synapse Analytics](../../includes/applies-to-version/asa.md)]

Azure Synapse Pathway transpiles SQL code from source systems such as IBM Netezza, Microsoft SQL Server, and Snowflake to T-SQL compliant with Azure Synapse SQL. Synapse Pathway utilizes an Abstract Syntax Tree (AST) model for mapping source data types into supported types.

The following set of links shows the source and target data type mappings for each of the supported source systems.

## Source System Mappings
[IBM Netezza data type mapping](data-type-mappings-ibm-netezza.md)<br/>
Describes the data type mappings between IBM Netezza and Azure Synapse SQL.

[Microsoft SQL Server data type mapping](data-type-mappings-microsoft-sql-server.md)<br/>
Describes the data type mappings between Microsoft SQL Server and Azure Synapse SQL.

[Snowflake data type mapping](data-type-mappings-snowflake.md)<br/>
Describes the data type mappings between Snowflake and Azure Synapse SQL.

## Next steps

[Download Azure Synapse Pathway](synapse-pathway-download.md)
