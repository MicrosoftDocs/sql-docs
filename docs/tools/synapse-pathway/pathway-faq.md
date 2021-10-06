---
title: Azure Synapse Pathway FAQ
description: Frequently asked question for Azure Synapse Pathway
author: twounder
ms.author: mausher
ms.topic: overview 
ms.date: 09/22/2021
ms.prod: sql
ms.technology: tools-other
monikerRange: "=azure-sqldw-latest"
ms.custom:
  - template-overview
  - intro-overview
---
# Azure Synapse Pathway FAQ
[!INCLUDE [Azure Synapse Analytics](../../includes/applies-to-version/asa.md)]

In this guide, you'll find the most frequently asked questions on Azure Synapse Pathway.

## General

### Q. What is Azure Synapse Pathway?

A. Azure Synapse Pathway is code translation tool that helps you upgrade to a modern data warehouse platform by automating the code translation of your existing data warehouse to make it compliant with T-SQL based Azure Synapse SQL.

### Q. How can I download Azure Synapse Pathway?

A. Synapse Pathway is available to download from the [Microsoft Download Center](https://aka.ms/synapse-pathway-download).

### Q. How much does Azure Synapse Pathway cost?

A. There is no cost associated with downloading and running your code translations using Synapse Pathway.

### Q. What source/target pairs does Azure Synapse Pathway currently support?

A. As of version 0.4 of Synapse Pathway, following data warehouses are included as sources:
- Amazon Redshift
- Google BigQuery
- IBM Netezza
- Microsoft SQL Server
- Snowflake
- Teradata

### Q. What is included as part of the code conversion?

A. Synapse Pathway version supports code translation of databases, schemas, tables, and views.

| Source Platform| Statement Types Supported | 
|:-------------------:|:------------------|
| Amazon Redshift | Create/Alter/Drop Database<br /> Create/Alter/Drop  Schema <br /> Create/Alter/Drop Table |
| Google BigQuery | Create/Alter/Drop Database<br /> Create/Alter/Drop  Schema <br /> Create/Alter/Drop Table |
| IBM Netezza  | Create/Alter/Drop Database<br /> Create/Alter/Drop  Schema <br /> Create/Alter/Drop Table |
| Microsoft SQL Server  | Create/Alter/Drop Database<br /> Create/Alter/Drop  Schema <br /> Create/Alter/Drop Table <br /> Create/Alter/Drop View | 
| Snowflake |  Create/Alter/Drop Database<br /> Create/Alter/Drop  Schema <br /> Create/Alter/Drop Table |
| Teradata | Create/Alter/Drop Database<br /> Create/Alter/Drop  Schema <br /> Create/Alter/Drop Table |
  
### Q. Can it also scan my environment and provide an assessment report of all the objects that need to be converted/translated?

A. In this version of Synapse Pathway, you will have to provide the link to the DDL/DML scripts that needs to be translated. Synapse Pathway will not scan your current environment to identify the objects that need to be translated.

### Q. What information does Azure Synapse Pathway capture about my current data warehouse instance?

A. Since you can run Synapse Pathway in your local environment, there is no data captured except your name and organization, which is required when you download the MSI from the Microsoft download center.

### Q. Where can I raise issues encountered in Azure Synapse Pathway?

A. Support for Synapse Pathway is available through Microsoft support channel. You can raise the ticket either in the Azure portal or standard (typically on-prem support) portals.

## Next steps

[Download Azure Synapse Pathway](synapse-pathway-download.md)
