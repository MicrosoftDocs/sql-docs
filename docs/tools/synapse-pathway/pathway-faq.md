---
title: Azure Synapse Pathway FAQ
description: This doc answers the most frequently asked questions. 
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

 #### Q. What is Azure Synapse Pathway?
Azure Synapse Pathway is code translation utility helps you upgrade to a modern data warehouse platform by automating the code translation of your existing data warehouse to make it complaint with T-SQL based Azure Synapse SQL.

#### Q. How can I download it?
Synapse Pathway is available to download from the [Microsoft Download Center](https://aka.ms/ast-download).
#### Q. How much does Azure Synapse Pathway cost?

There is no cost associated with downloading and running your code translations using Synapse Pathway.

#### Q. What source/target pairs does Azure Synapse Pathway currently support?

In this preview version of Synapse Pathway, following data warehouses are included as sources:
- Microsoft SQL Server
- Teradata
- Snowflake
- Netezza
#### Q. What is included as part of the code conversion?
Synpase Pathway supports code translation of tables, schemas, views and stored procedures.
#### Q. Can it also scan my environment and provide an assessment report of all the objects that need to be converted/translated?
In this preview version of Synapse Pathway, you will have to provide the link to the DDL/DML scripts that needs to be translated. Synapse Pathway will not scan your current environment to identify the objects that need to be translated.
#### Q. What telemetry does it capture about my current data warehouse instance

Since you can run Azure Synapse Pathway in your local environment, there is no data captured except your name and organization which is required when you download the MSI from the Microsoft download center.

#### Q. Where can I raise issues encountered while running Azure Synapse Pathway?

Support for Synapse Pathway is available through Microsoft support channel. You can raise the ticket either in the Azure Portal or standard (typically on-prem support) portals.
> [!NOTE] Like any other Azure Service, all preview services are eligible for support, just without SLA's in place.

 

### Troubleshooting and optimization

#### Q. Why do I see slow performance while running the code conversion?

#### Q. Translation of errors or unexpected results?
