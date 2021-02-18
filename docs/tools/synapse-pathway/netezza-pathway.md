---
title: #migrate IBM Netezza to Azure Synapse Analytics using Azure Synapse Pathway.
description: #Required; The article describes some of the best practices when migrating from IBM Netezza to Azure Synapse Translate. 
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

# Migrating from IBM Netezza to Azure Synapse Analytics

<!-- 2. Introductory paragraph 
Required. Lead with a light intro that describes what the article covers. Answer the 
fundamental “why would I want to know this?” question. Keep it short.
-->
Azure Synapse Pathway is the next generation data warehouse code migration tool to enable migration from on-premise and cloud data warehouses to Azure Synapse SQL. 
It fully supports code conversion of **IBM Netezza's** database, schema, and tables into T-SQL complaint code Azure Synapse SQL.


<!-- 3. H2s
Required. Give each H2 a heading that sets expectations for the content that follows. 
Follow the H2 headings with a sentence about how the section contributes to the whole.
-->

## Key considerations when migrating from IBM Netezza
It generally makes sense to only migrate the tables that are in use in the existing system. Tables, which are not active can be archived rather than migrated so that the data is available if necessary in future. It’s best to use system metadata and log files to determine which tables are in use, rather than documentation as documentation may be out of date.
 
Netezza query history tables (if enabled) contain information that can be used to determine when a given table was last accessed – which in turn can be used to decide whether a table is a candidate for migration.

## Unsupported Netezza database object types
Netezza implements some database objects that are not directly supported in Azure Synapse, but there are generally methods to achieve the same functionality within the new environment: 


1. Zone maps – In Netezza zone maps **are automatically created and maintained** for some column types and are used at query time to restrict the amount of data to be scanned. They are created on the following column types:
- INTEGER columns of length 8 bytes or less
Temporal columns (that is, DATE, TIME, TIMESTAMP)
- CHAR columns if these are part of a Materialized View and mentioned in the ORDER BY clause

Azure Synapse does not include zone maps, but similar results can be achieved by using other (user-defined) index types and/or partitioning.

2. Clustered base tables (CBT) – In Netezza, CBTs are most used for fact tables that have billions of records. Scanning such a huge table requires lot of processing time as full table scan could be needed to get relevant records. 
 - Organizing records on restrictive CBT via allows Netezza to group records in same or nearby extents. This process will also create zone maps that improves the performance by reducing the amount of data to be scanned. 
 - In Azure Synapse a similar effect can be achieved by use of partitioning and/or use of other indexes.

3. Functions: 
In this version Azure Synapse Pathway, there are over 95 functions that can be translated to run on Azure Synapse Analytics. However, Netezza’s user-defined functions are written in C++/nzula and cannot be translated without manually recoding them to T-SQL equivalent in Azure Synapse.

## Scope of IBM Netezza translation in Azure Synapse Pathway

Azure Synapse Translates support code conversion for database, schemas, tables in IBM Netezza.DDL statements like Create Database, Table, schema, function , Drop are supported. Function translation is also supported.

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

