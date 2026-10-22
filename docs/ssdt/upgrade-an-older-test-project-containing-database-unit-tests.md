---
title: Upgrade an Older Test Project Containing Database Unit Tests
description: Find out how to upgrade Visual Studio 2010 test projects that contain database unit tests. See how to use SQL Server Data Tools with those projects.
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: upgrade-and-migration-article
---

# Upgrade an older test project containing database unit tests

You can upgrade an older test project, which was created in Visual Studio 2010 and that contains database unit tests, to use the new SQL Server Data Tools database unit testing runtime and tools. Once you have upgraded an older project you can add SQL Server unit tests to the project (see [Create and define SQL Server unit tests](creating-and-defining-sql-server-unit-tests.md) for more information).

> [!TIP]  
> If you're using Visual Studio 2010, after you add SQL Server unit tests to a test project, you shouldn't add unit tests using the older database unit test template. If you do, you need to convert the project again before the tests execute correctly.

If you have a test database project that was created in a release older than Visual Studio 2010, you can use the information in [How to: Upgrade Database Unit Tests from Previous Releases of Visual Studio](/previous-versions/visualstudio/visual-studio-2010/dd193412(v=vs.100)) to upgrade your database project to Visual Studio 2010, before upgrading the project to SQL Server Data Tools.

### Initiate an upgrade

- You can start a project upgrade from the context menu for a test project.

  In some cases, SQL Server Data Tools displays a dialog box from which you can initiate a test project upgrade.

- Upgrading the project removes the assembly reference to the older database testing framework and adds a reference to the new framework and an adapter assembly. The app.config file is also updated.

  > [!NOTE]  
  > If your test project has both a DatabaseSetup and a SQLDatabaseSetup code files, upgrading the project to SQL Server Data Tools excludes the DatabaseSetup file from the build. You can remove the DatabaseSetup file if it's excluded from the build.

- After conversion, existing database unit tests created with the older template uses types in the adapter assembly to access the new framework. The use of an adapter assembly means that the upgrade procedure didn't modify your test scripts and code. If you add a SQL Server unit test to the project, the new test references the new framework directly and not via an adapter. You can choose to manually update your existing code to use the new framework for consistency with new tests, but this isn't required.

## Related content

- [Verify database code by using SQL Server unit tests](verifying-database-code-by-using-sql-server-unit-tests.md)
