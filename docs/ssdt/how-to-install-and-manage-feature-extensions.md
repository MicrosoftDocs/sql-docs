---
title: Install and Manage Feature Extensions
description: Learn how to install feature extensions so that you can increase the functionality of SQL Server Data Tools. See where to install different types of extensions.
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: install-set-up-deploy
ms.custom:
  - intro-installation
---

# How to: Install and manage feature extensions

You can add rules for analyzing database code, conditions for database unit tests and build/deployment contributors to increase the functionality that Visual Studio editions including SQL Server Data Tools offer. However, you must first install a feature extension before you can use it, whether you created the extension or you installed one that someone else created.

Where to install your extension depends on the extension type, and from where you intend to use it. In the latest editions of Visual Studio, the install location of some components has moved from the SQL Server install directory to inside the Visual Studio directory. This setup makes it easier to have different versions of the software running side by side, but it means that you might need to install your extension in multiple locations if you wish to use it in different version of SQL Server Data Tools and from the command line.

## Install extensions for use inside Visual Studio

| Extension type | Install location |
| --- | --- |
| Custom Test Condition for SQL Server Unit Tests | `<Visual Studio Install Dir>\Common7\IDE\Extensions\Microsoft\SQLDB\TestConditions` |
| Build Contributors<br /><br />Deployment Contributors<br /><br />Static Code Analysis Rules | `<Visual Studio Install Dir>\Common7\IDE\Extensions\Microsoft\SQLDB\DAC\120\Extensions` |

The `<Visual Studio Install Dir>` varies depending on which version of Visual Studio you're using and where you chose to install it. For Visual Studio 2012, it's usually `C:\Program Files (x86)\Microsoft Visual Studio 11.0`. For Visual Studio 2013, it's usually `C:\Program Files (x86)\Microsoft Visual Studio 12.0`.

Extensions can be run as part of our command-line services:

| Extension type | Command-line service | Install folder |
| --- | --- | --- |
| Custom Test Condition for SQL Server Unit Tests | MSBuild / MSTest can be used to run unit tests from the Developer Command Prompt for Visual Studio 2013 and similar command-line tools. | Same as when running inside Visual Studio. |
| Build Contributors<br /><br />Deployment Contributors | [SqlPackage](../tools/sqlpackage/sqlpackage.md), or by using MSBuild Deploy or Publish targets when building a database project. | MSBuild: Same as when running inside Visual Studio.<br /><br />[SqlPackage](../tools/sqlpackage/sqlpackage.md): If located inside Visual Studio directory, same as before.<br /><br />If SqlPackage.exe and other DacFx DLLs are located outside that directory, then extensions should either be placed in the same directory or in `C:\Program Files (x86)\Microsoft SQL Server\120\DAC\bin\Extensions`. |
| Static Code Analysis Rules | MSBuild can be used to build the project and run static code analysis.<br /><br />In addition, you can run code analysis using a CodeAnalysisService API from your own applications. The extension lookup rules function the same in this case as when SqlPackage.exe is used. | Same as for Build and Deployment Contributors |

> [!NOTE]  
> You must have administrator permissions on your computer to access any of the install directories under the Program Files folder. If you don't have the appropriate permissions, contact your network administrator.

## Security considerations

Before you install an extension that you didn't create, you should understand the following risks:

- The installation program for the extension might be malicious and gain access to protected resources based on your installation permissions.

- The extension itself might be malicious and gain control of protected resources if the user who uses the extension has sufficient permissions.

To minimize your risk, you should install an extension only if it's from a known source. If you obtain an extension from an untrusted source, you should inspect the source code for that extension and its installation program (if it has one) before you install and use the extension.

## Install a custom feature extension

Copy the signed assembly (`.dll`) to the correct install folder. Close and reopen Visual Studio. The extension should now be available.

## Related content

- [Extending the database features](extending-the-database-features.md)
