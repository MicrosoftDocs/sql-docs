---
title: R and Python package management in SQL Server Machine Learning | Microsoft Docs
description: Get R and Python package information, add new packages, and enable client access on a SQL Server instance configured for machine learning.
ms.prod: sql
ms.technology: machine-learning

ms.date: 05/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# R and Python package management in SQL Server Machine Learning
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article introduces R and Python package management in SQL Server 2017 Machine Learning and SQL Server 2016 R Services. It also describes limitations on the packages that you can install on SQL Server.

## Overview of package management methods and requirements

Unlike a typical R or Python development, packages used by SQL Server must be installed to a folder under administrative control. There are many benefits to keeping packages in a single location:

+ The server administrator can monitor the addition of new files and libraries on the server, and control the growth of files used by package libraries. 
+ Packages can be more easily shared by multiple database users, as opposed to installing multiple copies of the same package in user libraries.

However, these restrictions necessarily mean some changes in the way that data scientists and analysts work:

+ Generally, package installation on SQL Server requires administrative access. You cannot arbitrarily add, update, or remove any package you want.
+ Packages are installed to an instance library and cannot be shared across instances. Individual user libraries are not supported.


## Prerequisites

Before you attempt to download or install any new package, review the requirements:

+ Make sure that there is a Windows version of the package.

+ Verify R version or Python version compatibility. The package must be compatible with the version of R or Python that is running in SQL Server.

+ Permission to install a package to the instance library requires administrative rights or membership in a database role granting permission to manage packages.

Consider the risks and benefits of installing a particular package into the SQL Server environment. Check whether the package (or any packages that it requires) contains features that would be blocked by SQL Server or by policy. Many R and Python packages are a very poor fit for a hardened SQL Server environment. Problems might include:

    - Packages that access the network
    - Packages that require Java or other frameworks not typically used in a SQL Server environment
    - Packages that require elevated file system access
    - Package is used for web development or other tasks that don't benefit by running inside SQL Server.

## Installation on servers with no Internet access

In general, servers that host production databases do not allow connection to the  Internet. Installing new R or Python packages in such environments requires that you prepare all packages and their dependencies in advance, and copy the files to a folder on the server for use in installation.

1. Identify all package dependencies. 
2. Check whether any required packages are already installed on the server. If the package is installed, verify that the version is correct.
3. Download the package and all dependencies to a separate computer.
4. Move the files to a folder accessible by the server.
5. Run a supported installation command or DDL statement to install the package into the instance library.

Identifying all dependencies can be complicated. For R, we recommend that you use [miniCRAN](create-a-local-package-repository-using-minicran.md) to prepare an offline package repository.

For Python, you must similarly prepare all dependencies and save them locally. Be sure to use Windows-compatible binaries and the WHL format.
