---
title: "Migration guide: DB2 to SQL Server"
description: Follow this guide to migrate your DB2 server to SQL Server. 
ms.custom: ""
ms.date: "08/17/2020"
ms.prod: sql
ms.reviewer: ""
ms.technology: release-landing
ms.topic: conceptual
helpviewer_keywords: 
  - "processors [SQL Server], supported"
  - "number of processors supported"
  - "maximum number of processors supported"
author: MashaMSFT
ms.author: mathoma
---
# Migration guide: DB2 to SQL Server
[!INCLUDE[sqlserver](../../../includes/applies-to-version/sqlserver.md)]


## Prerequisites

To migrate your DB2 database to SQL Server, you need:

- [SQL Server Migration Assistant (SSMA) for DB2](https://www.microsoft.com/download/details.aspx?id=54254)


## Pre-migration

Before you begin your migration, discover the topology of your environment and assess the feasibility of your intended migration.

### Discover

The goal of the Discover phase is to identify existing data sources and details about the features that are being used to get a better understanding of and plan for the migration. This process involves scanning your network to identify all your organization’s DB2 instances together with the version and features in use.

### Assess and convert

Create an assessment using SQL Server Migration Assistant (SSMA). 

To create an assessment, follow these steps:

1. Open SQL Server Migration Assistant (SSMA) for DB2. 
1. Select **File** and then choose **New Project**. 
1. Provide a project name, a location to save your project, and then select a SQL Server migration target from the drop-down. 
1. Select **OK**. 

   :::image type="content" source="media/db2-to-sql-server/ssmadb2newproject.png" alt-text="Select new SSMA project":::

1. Fill out the DB2 connection details on the **Connect to DB2** dialog box. 
1. Select the DB2 schema you want to migrate, and then select **Create report**. 

   :::image type="content" source="media/db2-to-sql-server/createreport.png" alt-text="Select create report":::

1. 


## Migrate

## Post-migration 

## Migration assets 