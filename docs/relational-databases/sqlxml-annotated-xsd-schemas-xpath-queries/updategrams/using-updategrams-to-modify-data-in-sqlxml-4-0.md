---
title: "Using Updategrams to Modify Data in SQLXML 4.0"
description: View information and examples about updategrams and how they are used for modifying data in SQLXML 4.0.
author: MikeRayMSFT
ms.author: mikeray
ms.date: "03/17/2017"
ms.service: sql
ms.subservice: xml
ms.topic: "reference"
helpviewer_keywords:
  - "SQLXML, updategrams"
  - "data insertions [SQLXML]"
  - "data deletions [SQLXML]"
  - "updating data [SQLXML]"
  - "modifying data [SQLXML]"
  - "OPENXML function"
  - "updategrams [SQLXML]"
  - "database modifications [SQLXML]"
  - "data updates [SQLXML]"
  - "modifying databases"
  - "data modifications [SQLXML]"
  - "deleting data"
  - "inserting data"
ms.assetid: b8b3b892-cb73-41d0-b945-bce148d81d9b
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Using Updategrams to Modify Data in SQLXML 4.0
[!INCLUDE [SQL Server Azure SQL Database](../../../includes/applies-to-version/sql-asdb.md)]
  You can modify (insert, update, or delete) a database in [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] from an existing XML document by using an updategram or the OPENXML [!INCLUDE[tsql](../../../includes/tsql-md.md)] function.  
  
 This section provides information about updategrams and examples of their usage.  
  
## In This Section  
 [Introduction to Updategrams &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/updategrams/introduction-to-updategrams-sqlxml-4-0.md)  
 Provides basic information and examples of updategrams.  
  
 [Specifying an Annotated Mapping Schema in an Updategram &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/updategrams/specifying-an-annotated-mapping-schema-in-an-updategram-sqlxml-4-0.md)  
 Explains and provides examples of annotated mapping schemas in updategrams.  
  
 [NULL Handling &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/updategrams/null-handling-sqlxml-4-0.md)  
 Describes how to specify NULL for element and attribute values.  
  
 [Inserting Data Using XML Updategrams &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/updategrams/inserting-data-using-xml-updategrams-sqlxml-4-0.md)  
 Describes and provides examples of using updategrams to insert data.  
  
 [Deleting Data Using XML Updategrams &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/updategrams/deleting-data-using-xml-updategrams-sqlxml-4-0.md)  
 Describes and provides examples of using updategrams to delete data.  
  
 [Updating Data Using XML Updategrams &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/updategrams/updating-data-using-xml-updategrams-sqlxml-4-0.md)  
 Describes and provides examples of using updategrams to modify existing data.  
  
 [Passing Parameters to Updategrams &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/updategrams/passing-parameters-to-updategrams-sqlxml-4-0.md)  
 Describes and provides examples of passing parameters to updategrams.  
  
 [Handling Database Concurrency Issues in Updategrams &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/updategrams/handling-database-concurrency-issues-in-updategrams-sqlxml-4-0.md)  
 Describes the various levels of protection possible for handling concurrency issues in updategrams, and provides examples.  
  
 [Updategram Sample Applications &#40;SQLXML 4.0&#41;]()  
 Provides sample applications that use updategrams.  
  
 [Guidelines and Limitations of XML Updategrams &#40;SQLXML 4.0&#41;](../../../relational-databases/sqlxml-annotated-xsd-schemas-xpath-queries/updategrams/guidelines-and-limitations-of-xml-updategrams-sqlxml-4-0.md)  
 Lists some things to remember when working with updategrams.  
  
