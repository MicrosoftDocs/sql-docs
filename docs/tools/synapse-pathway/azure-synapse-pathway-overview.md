---
title: Azure Synapse Pathway overview
description: This doc describes what is Azure Synapse Pathway, why do you need one and how does it work. 
author: anshul82-ms
ms.author: anrampal
ms.topic: overview #Required; leave this attribute/value as-is.
ms.date: 02/22/2020
ms.custom: template-overview #Required; leave this attribute/value as-is.
---

<!--
Remove all the comments in this template before you sign-off or merge to the 
main branch.
-->

<!--
This template provides the basic structure of a service/product overview article.
See the [overview guidance](contribute-how-write-overview.md) in the contributor guide.

To provide feedback on this template contact 
[the templates workgroup](mailto:templateswg@microsoft.com).
-->

<!-- 1. H1
Required. Set expectations for what the content covers, so customers know the 
content meets their needs. H1 format is # What is <product/service>?
-->

# Azure Synapse Pathway



<!-- 2. Introductory paragraph 
Required. Lead with a light intro that describes what the article covers. Answer the 
fundamental “why would I want to know this?” question. Keep it short.
-->



<!-- 3. H2s
Required. Give each H2 a heading that sets expectations for the content that follows. 
Follow the H2 headings with a sentence about how the section contributes to the whole.
-->

## Overview

As customers consider **modernizing their data warehouse systems**, one of the critical blockers they face is translating their SQL code written and optimized for their current system to be optimized for the new system they want to migrate to.

Organizations worldwide want to modernize their analytics platform to enjoy both TCO and innovation benefits. However, customers have invested thousands of working hours, millions of dollars, and written tens of thousands of lines of code for their data warehouse. 
To translate this critical SQL code, customers have to **either manually rewrite existing SQL code or invest large amounts of their budget for an outside practice to rewrite or convert their code**. That’s why we are investing in tooling to help accelerate the migration process without upfront cost.

**Azure Synapse Pathway** helps you upgrade to a modern data warehouse platform by automating the code translation of your existing data warehouse to fast track your migration to Azure Synapse Analytics.

 ![Azure Synapse pathway overview.](./media/pathway-overview/synapse-pathway-overview.png) 

Synapse Pathway translates Data Definition Language (DDL) and Data Manipulation Language (DML) statements into T-SQL compliant language that is compatible with Azure Synapse SQL.

## Behind the scenes
This is the high level architecture of Azure Synapse Pathway.
 ![Azure Synapse pathway overview.](./media/pathway-overview/synapse-pathway-architecture.png) 

- Azure Synapse Pathway code conversion capability is based on 

## Install ASP

To install ASP, download the latest version of the tool from the [Microsoft Download Center](https://aka.ms/asp-download), and then run the **AzureSynapsePathway.msi** file.

## Pricing
There is no cost to download and use Azure Synapse Pathway for all your code conversion needs.

See Azure policy for data ingress pricing

## Supported sources
- **IBM Netezza** : Azure Synapse Pathway supports code conversion of database, schemas and tables. DDL statements like create & drop are supported. Function translation is also supported.

- **Microsoft SQL Server**
Azure Synapse Pathway supports code conversion of database, schemas and tables. 
- **Teradata Vantage**
- **Snowflake**

> [!NOTE]
Check [release notes](..) for new sources added as well as feature additions. 


## Frequently asked questions
Please review the [FAQ Page](..) for additional queries on Azure Synapse Pathway

## Next steps
<!-- Add a context sentence for the following links -->
- [Run your first translation with Azure Synapse Pathway](contribute-how-to-write-overview.md)
- [Announcement Blog](links-how-to.md)

<!--
Remove all the comments in this template before you sign-off or merge to the 
main branch.
-->