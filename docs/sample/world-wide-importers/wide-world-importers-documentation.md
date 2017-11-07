---
title: "Wide World Importers Documentation | Microsoft Docs"
ms.prod: "sql-non-specified"
ms.technology:
  - "samples"
ms.custom: ""
ms.date: "01/19/2017"
ms.reviewer: ""
ms.suite: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 17cabd9d-cb2f-436c-ad9c-ce02225808b7
caps.latest.revision: 3
author: "BarbKess"
ms.author: "barbkess"
manager: "jhubbard"
robots: noindex,nofollow
---
# Wide World Importers Documentation
Wide World Importers is the new sample database for SQL Server 2016 and Azure SQL Database. It illustrates the core capabilities of SQL Server 2016 and Azure SQL Database, for transaction processing (OLTP), data warehousing and analytics (OLAP) workloads, as well as hybrid transaction and analytics processing (HTAP) workloads.

## About this sample

Wide World Importers is a comprehensive database sample that both illustrates database design, and illustrates how SQL Server features can be leveraged in an application.

Note that the sample is meant to be representative of a typical database. It does not include every feature of SQL Server. The design of the database follows one common set of standards, but there are many ways one might build a database.

**Latest release**:
[wide-world-importers-release](http://go.microsoft.com/fwlink/?LinkID=800630)

**Source code for the sample**:
[wide-world-importers](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/wide-world-importers).

**Feedback**: please send to
[sqlserversamples@microsoft.com](mailto:sqlserversamples@microsoft.com).

The documentation for the sample is organized as follows:

## Overview

Overview of the sample company Wide World Importers, and the workflows addressed by the sample.

## Main OLTP Database WideWorldImporters

Instructions for the installation and configuration of the core database WideWorldImporters that is used for transaction processing (OLTP - OnLine Transaction Processing) and operational analytics (HTAP - Hybrid Transactional/Analytical Processing).

Description of the schemas and tables used in the WideWorldImporters database.  

Describes how WideWorldImporters leverages core SQL Server features.

Sample queries for the WideWorldImporters database.

## Data Warehousing and Analytics Database WideWorldImportersDW

Instructions for the installation and configuration of the OLAP database WideWorldImportersDW.

Description of the schemas and tables used in the WideWorldImportersDW database, which is the sample database for data warehousing and analytics processing (OLAP).

Workflow for the ETL (Extract, Transform, Load) process that migrates data from the transactional database WideWorldImporters to the data warehouse WideWorldImportersDW.

Describes how the WideWorldImportersDW leverages SQL Server features for analytics processing.

Sample analytics queries leveraging the WideWorldImportersDW database.

## Data generation

Describes how additional data can be generated in the sample database, for example inserting sales and purchase data up to the current date.
