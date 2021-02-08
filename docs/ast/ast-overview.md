---
title: "Overview of Azure Synapse Translate | Microsoft Docs"
description: Learn how to use Azure Synapse Translate to migrate data warehouses to Azure Synapse SQL
ms.custom: ""
ms.date: "02/02/2021"
ms.prod: sql
ms.prod_service: "ast"
ms.reviewer: ""
ms.technology: ast
ms.topic: conceptual
keywords: ""
helpviewer_keywords: 
  - "Azure Synapse Translate, overview"
ms.assetid: ""
author: mausher
ms.author: mausher
---

# Overview of Azure Synapse Translate

Azure Synapse Translate (AST) helps you upgrade to a modern cloud data warehouse platform by automating the code translation of your existing data warehouse to T-SQL for [Azure Synapse SQL](http://aka.ms/synapse). AST translates Data Definition Language (DDL) and Data Manipulation Language (DML) statements into T-SQL compliant language and  implementing best practices to reduce migration costs, eliminate human code translation errors, and to quickly migrate some or all of your existing solution.

## Get Azure Synapse Translate

### Prerequisites

AST requires the [.NET Core Desktop Runtime 3.1.11 or later](https://dotnet.microsoft.com/download/dotnet-core/3.1).

### Download

To install AST, download the latest version of the tool from the [Microsoft Download Center](https://aka.ms/ast-download), and then run the **AzureSynapseTranslate.msi** file.

## Capabilities

DDL Transaltion: Schemas
  
DML Translation


## Supported source and target versions

AST supported source and target versions are:

**Sources**

- IBM Netezza
- Microsoft SQL Server
- Teradata Vantage

**Targets**

- Azure Synapse SQL

## See also
[Azure Synapse Translate: Configuration Settings](../ast/ast-configuration.md)

[Azure Synapse Translate: Best Practices](../ast/ast-bestpractices.md)
