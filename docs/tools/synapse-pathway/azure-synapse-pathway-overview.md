---
title: Azure Synapse Pathway overview (Preview)
description: Azure Synapse Pathway is a tool to migrate a data warehouse to Azure Synapse Analytics.
ms.author: anrampal
ms.topic: overview 
ms.date: 02/22/2020
ms.prod: sql
ms.technology: "Azure Synapse Pathway"
ms.custom: template-overview 
---
# Azure Synapse Pathway (Preview)

## Overview

As customers consider modernizing their data warehouse systems, one of the critical blockers they face is translating their SQL code. The existing code is written and optimized for their current system but needs to be optimized for the new system they're migrating to.

Organizations worldwide want to modernize their analytics platform to enjoy both total cost of ownership (TCO) and innovation benefits. However, customers have invested thousands of working hours, millions of dollars, and written tens of thousands of lines of code for their data warehouse.
 
To translate this critical SQL code, customers have to either manually rewrite existing SQL code or invest large amounts of their budget for an outside practice to rewrite or convert their code. 

**Azure Synapse Pathway** helps you upgrade to a modern data warehouse platform by automating the code translation of your existing data warehouse. Its a free, intuitive and easy to use tool that automates the code translation enabling a quicker migration to Azure Synapse Analytics .

 ![Azure Synapse pathway overview.](./media/pathway-overview/synapse-pathway-overview.png) 

Synapse Pathway translates Data Definition Language (DDL) and Data Manipulation Language (DML) statements into T-SQL compliant language that is compatible with Azure Synapse SQL.

## Behind the scenes

To learn more about Azure Synapse Pathway, see [behind the scenes](synapse-pathway-behind-the-scenes.md) for a step-by-step process on how Azure Synapse Pathway works.

## Get Azure Synapse Pathway

Azure Synapse Pathway is currently in **Preview**.

To install Synapse Pathway, check [Azure Synapse Pathway download](synapse-pathway-download.md) for pre-requisites and the link to download the latest version.

## Supported sources

Azure Synapse Pathway supports code conversion of database, schemas, and tables for the following sources:
- **IBM Netezza** 
- **Microsoft SQL Server**
- **Teradata Vantage**
- **Snowflake**

<!-- > [!NOTE]
Check [release notes](..) for new sources added as well as feature additions.  -->

## Frequently asked questions

Review the [FAQ Page](pathway-faq.md) for additional information on Azure Synapse Pathway

## Next steps

- [Run your first translation with Azure Synapse Pathway](synapse-pathway-assessment.md)
- [Announcement Blog](links-how-to.md)


