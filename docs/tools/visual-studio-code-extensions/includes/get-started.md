---
title: MSSQL Extension for Visual Studio Code Get Started
description: MSSQL extension for Visual Studio Code get started.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: roblescarlos, maghan
ms.date: 01/19/2026
ms.service: sql
ms.subservice: vs-code-sql-extensions
ms.topic: include
ms.collection:
  - data-tools
ms.custom:
  - build-2025
---

Make sure you're connected to a database and have an active editor window open with the MSSQL extension. When you connect, the `@mssql` chat participant understands the context of your database environment and can give accurate, context-aware suggestions. If you don't connect to a database, the chat participant doesn't have the schema or data context to provide meaningful responses.

The following examples use the `AdventureWorksLT2022` sample database, which you can download from the [Azure Data SQL Samples Repository](https://github.com/microsoft/sql-server-samples) GitHub repository.

For best results, adjust table and schema names to match your own environment.

Make sure the chat includes the `@mssql` prefix. For example, type `@mssql` followed by your question or prompt. This prefix ensures that the chat participant understands you're asking for SQL-related assistance.
