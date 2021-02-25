---
title: Azure Synapse Pathway overview
description: Azure Synapse Pathway is a tool to migrate a data warehouse to Azure Synapse Analytics.
ms.author: anrampal
ms.topic: overview 
ms.date: 02/22/2020
ms.custom: template-overview 
---
# Azure Synapse Pathway

## Overview

As customers consider modernizing their data warehouse systems, one of the critical blockers they face is translating their SQL code. The existing code is written and optimized for their current system but needs to be optimized for the new system they're migrating to.

Organizations worldwide want to modernize their analytics platform to enjoy both total cost of ownership (TCO) and innovation benefits. However, customers have invested thousands of working hours, millions of dollars, and written tens of thousands of lines of code for their data warehouse.
 
To translate this critical SQL code, customers have to either manually rewrite existing SQL code or invest large amounts of their budget for an outside practice to rewrite or convert their code. 

**Azure Synapse Pathway**(ASP) helps you upgrade to a modern data warehouse platform by automating the code translation of your existing data warehouse. Automating the code translation enables a quicker migration to Azure Synapse Analytics at no cost.

 ![Azure Synapse pathway overview.](./media/pathway-overview/synapse-pathway-overview.png) 

Synapse Pathway translates Data Definition Language (DDL) and Data Manipulation Language (DML) statements into T-SQL compliant language that is compatible with Azure Synapse SQL.

## Behind the scenes

To learn more about Azure Synapse Pathway, see the [behind the scenes article](synapse-pathway-behind-the-scenes.md) that describes the step-by-step process on how Azure Synapse Pathway works.

## Install ASP

To install ASP, download the latest version of the tool from the [Microsoft Download Center](https://aka.ms/asp-download), and then run the **AzureSynapsePathway.msi** file.

## Pricing

There's no cost to download and use Azure Synapse Pathway for all your code conversion needs.

See Azure policy for data ingress pricing.

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

- [Run your first translation with Azure Synapse Pathway](contribute-how-to-write-overview.md)
- [Announcement Blog](links-how-to.md)


