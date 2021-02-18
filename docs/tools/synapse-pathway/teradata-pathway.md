---
title: #migrate Terdata to Azure Synapse Analytics using Azure Synapse Pathway.
description: #Required; The article describes some of the best practices when migrating from Teradata to Azure Synapse Translate. 
author: #Required; anshul82-ms.
ms.author: #anrampal
ms.service: #Azure Synapse Pathway.
ms.topic: conceptual #Required; leave this attribute/value as-is.
ms.date: #Required; 02/17/2021 format.
ms.custom: template-concept #Required; leave this attribute/value as-is.
---

<!--Remove all the comments in this template before you sign-off or merge to the 
main branch.
-->

<!--
This template provides the basic structure of a concept article.
See the [concept guidance](contribute-how-write-concept.md) in the contributor guide.

To provide feedback on this template contact 
[the templates workgroup](mailto:templateswg@microsoft.com).
-->

<!-- 1. H1
Required. Set expectations for what the content covers, so customers know the 
content meets their needs. Should NOT begin with a verb.
-->

# Migrating from Terdadata to Azure Synapse Analytics

<!-- 2. Introductory paragraph 
Required. Lead with a light intro that describes what the article covers. Answer the 
fundamental “why would I want to know this?” question. Keep it short.
-->
Azure Synapse Pathway is the next generation data warehouse code migration tool to enable migration from on-premise and cloud data warehouses to Azure Synapse SQL.

It fully supports code conversion of Teradata's database, schema and tables into T-SQL complaint code for Azure Synapse SQL.


<!-- 3. H2s
Required. Give each H2 a heading that sets expectations for the content that follows. 
Follow the H2 headings with a sentence about how the section contributes to the whole.
-->

## Key considerations when migrating from Teradata
When migrating tables between different technologies it is,only the raw data (and the metadata that describes it) that gets physically moved between the two environments. Other database elements from the source system (e.g. indexes, log files) are not directly migrated as these may not be needed, or maybe implemented differently within the new target environment. For example, there is no equivalent of the MULTISET option within Teradata’s CREATE TABLE syntax.

However, it is important to understand where performance optimizations such as indexes have been used in the source environment as this information can give useful indication of where performance optimization might be added in the new target environment.



## Teradata objects

### Functions
- In common with most database products, Teradata supports system
functions and user-defined functions within the SQL implementation.
When migrating to another database platform such as Azure Synapse
common system functions are generally available and can be migrated
without change.

-  For system functions where there is no equivalent, or for arbitrary user defined functions these may need to be re-coded using T-SQL, which is used by Azure Synapse for implementing user-defined functions.
 
### Stored procedures
- Most modern database products allow for procedures to be stored within
the database – in Teradata’s case the SPL language is provided for this
purpose. A stored procedure typically contains SQL statements and some
procedural logic and may return data or a status.
With Azure Synapse Pathway, you can translate  Teradata stored procedures to T-SQL complaint in Azure Synapse Analytics

### Triggers
- Creation of triggers is not supported within the Azure Synapse but can be
implemented within Azure Data Factory.
### Sequences
- Within Azure Synapse sequences are handled in a similar way to Teradata, i.e.via use of IDENTITY columns or using SQL code to create the next sequence
number in a series.

## Scope of Teradata translation with Azure Synapse Pathway

Azure Synapse Translates support code conversion for Database, schemas, tables in Teradata. DDL statements like Create Database, Table, schema, function , Drop are supported. Function translation is also supported.

Check [release notes](..) for upcoming additions. 

<!-- 4. Next steps
Required. Provide at least one next step and no more than three. Include some 
context so the customer can determine why they would click the link.
-->

## Next steps
<!-- Add a context sentence for the following links -->
- [Teradata Vantage migration to Azure Synapse Analytics](contribute-how-to-write-concept.md)
- [Microsoft SQL Server migration to Azure Synapse Analytics](links-how-to.md)
- [Snowflake migration to Azure Synapse Analytics]()

