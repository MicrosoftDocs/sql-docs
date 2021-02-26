---
title: Azure Synapse Pathway Preview FAQ 
description: Frequently asked question for Azure Synapse Pathway
author: anshul82-ms
ms.author: anrampal
ms.topic: overview 
ms.date: 03/02/2021
ms.prod: sql
ms.technology: "Azure Synapse Pathway"
monikerRange: "=azure-sqldw-latest"
ms.custom: template-overview 
---
# Azure Synapse Pathway Preview
[!INCLUDE [Azure Synapse Analytics](../../includes/applies-to-version/asa.md)]

In this guide, you'll find the most frequently asked questions on Azure Synapse Pathway Preview.

## General

### Q. What is Azure Synapse Pathway?

A. Azure Synapse Pathway is code translation tool that helps you upgrade to a modern data warehouse platform by automating the code translation of your existing data warehouse to make it complaint with T-SQL based Azure Synapse SQL.

### Q. How can I download Azure Synapse Pathway?

A. Synapse Pathway is available to download from the [Microsoft Download Center](https://aka.ms/synapse-pathway-download).

### Q. How much does Azure Synapse Pathway cost?

A. There is no cost associated with downloading and running your code translations using Synapse Pathway.

### Q. What source/target pairs does Azure Synapse Pathway currently support?

A. In this preview version of Synapse Pathway, following data warehouses are included as sources:
- Microsoft SQL Server
- Teradata
- Snowflake
- Netezza

### Q. What is included as part of the code conversion?

A. Synapse Pathway supports code translation of tables, schemas, views, and stored procedures.

### Q. Can it also scan my environment and provide an assessment report of all the objects that need to be converted/translated?

A. In this preview version of Synapse Pathway, you will have to provide the link to the DDL/DML scripts that needs to be translated. Synapse Pathway will not scan your current environment to identify the objects that need to be translated.

### Q. What information does Azure Synapse Pathway capture about my current data warehouse instance?

A. Since you can run Synapse Pathway in your local environment, there is no data captured except your name and organization, which is required when you download the MSI from the Microsoft download center.

### Q. Where can I raise issues encountered in Azure Synapse Pathway?

A. Azure Synapse Pathway is currently in **Preview**.   Support for Synapse Pathway is available through Microsoft support channel. You can raise the ticket either in the Azure portal or standard (typically on-prem support) portals.

> [!NOTE] Like any other Azure Service, all preview services are eligible for support, just without SLA's in place.

<!-- ### Troubleshooting and optimization

#### Q. Why do I see slow performance while running the code conversion?

#### Q. Translation of errors or unexpected results? -->

## Next steps

[Download Azure Synapse Pathway](synapse-pathway-download.md)