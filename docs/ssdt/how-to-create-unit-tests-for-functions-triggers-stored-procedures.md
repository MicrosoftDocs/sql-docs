---
title: Create SQL Server Unit Tests for Functions, Triggers, and Stored Procedures
description: Learn how to use SQL Server Object Explorer to create a SQL Server unit test from a database function, trigger, or stored procedure.
author: dzsquared
ms.author: drskwier
ms.reviewer: randolphwest
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
---

# How to: Create SQL Server unit tests for functions, triggers, and stored procedures

You can write unit tests that evaluate changes to any database object. However SQL Server Data Tools includes support for creating tests for database functions, triggers, and stored procedures from a database project node in SQL Server Object Explorer. Transact-SQL code stubs can be automatically generated for you to customize.

## Create a SQL Server unit test from a function, trigger, or stored procedure

1. See [Walkthrough: Create and run a SQL Server unit test](walkthrough-creating-and-running-a-sql-server-unit-test.md) for an example of adding a unit test for a stored procedure, in the "To create a SQL Server unit test for the stored procedures" section.

   The Inconclusive test condition is the default condition that is added to every test. This test condition is included to indicate that test verification isn't implemented yet. Delete this test condition from your test after you add other test conditions. For more information, see [How to: Add Test Conditions to SQL Server Unit Tests](how-to-add-test-conditions-to-sql-server-unit-tests.md).

## Related content

- [Create and define SQL Server unit tests](creating-and-defining-sql-server-unit-tests.md)
- [How to: Create an empty SQL Server unit test](how-to-create-an-empty-sql-server-unit-test.md)
