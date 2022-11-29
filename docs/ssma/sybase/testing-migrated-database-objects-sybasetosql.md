---
description: "Testing Migrated Database Objects (SybaseToSQL)"
title: "Testing Migrated Database Objects (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "04/29/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: 4937f6b4-86bd-4070-88df-3d216306c33a
author: cpichuka 
ms.author: cpichuka 
---

# Testing Migrated Database Objects (SybaseToSQL)

Microsoft SQL Server Migration Assistant for Sybase Tester (SSMA Tester) automatically tests the database object conversion and the data migration made by SSMA. After all SSMA migration steps are finished, use SSMA Tester to verify that converted objects work the same way and that all data was transferred properly.

You can test the following object types with SSMA Tester:

- Tables
- Stored procedures
- Views
- Standalone statements

SSMA Tester executes objects selected for testing on Sybase and their counterparts in SQL Server. After that, it compares the results according to the following criteria:

- Are the changes in table data identical?
- Are the values of output parameters for procedures and functions identical?
- Do functions return the same results?
- Are the result sets identical?

> [!NOTE]
> Attention! Never use SSMA Tester on production systems. During Tester execution the source schema and data are modified. The complete restoration of the original state may be impossible for some types of tested code.

## Prerequisites

Current version of SSMA Tester does not support parallel execution by different users on the same source or target server.

## Getting Started

[Creating Test Cases &#40;SybaseToSQL&#41;](../../ssma/sybase/creating-test-cases-sybasetosql.md)

## See Also

[Project Settings &#40;Conversion&#41; &#40;SybaseToSQL&#41;](../../ssma/sybase/project-settings-conversion-sybasetosql.md)
