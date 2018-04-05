---
title: "Installing and managing machine learning packages in SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "02/19/2018"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "R"
ms.author: "heidist"
author: "HeidiSteen"
manager: "cgronlun"
ms.workload: "On Demand"
---
# Installing and managing machine learning packages in SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes how you can install new R or Python packages in SQL Server 2017 and in SQL Server 2016. It also describes limitations on the packages that you can install on SQL Server.

## Overview of package management methods and requirements

Unlike a typical R or Python development, packages used by SQL Server must be installed to a folder under administrative control. There are many benefits to keeping packages in a single location:

+ The server administrator can monitor the addition of new files and libraries on the server, and control the growth of files used by package libraries. 
+ Packages can be more easily shared by multiple database users, as opposed to installing multiple copies of the same package in user libraries.
+ Only secured, approved packages can be installed, to protect the server and its operations.

However, these restrictions necessarily mean some changes in the way that data scientists and analysts work:

+ Generally, administrative access to the server is required. In SQL Server 2017, the database administrator can use roles to give certain users the ability to install packages for private use, but the administrator still has to enable this feature.
+ Many servers do not have Internet access. Installing packages to these computers requires some additional preparation.
+ Packages are installed to an instance library. Packages cannot be shared across instances.
+ Users cannot run any package that has been installed in a user library.

## Package installation guides for R or Python

See the following articles for detailed steps on how to install new R or Python packages. 

### Install new R packages

+ [Install additional R packages on SQL Server](install-additional-r-packages-on-sql-server.md)

    You can install R packages on SQL Server 2016 or 2017 as administrator, using R tools.

    You can also install R packages from a remote R client where R Server 9.0.2 or later is installed.

    You can also install R packages in SQL Server 2017 using DDL statements.

### Install new Python packages

+ [Install new Python packages on SQL Server](../python/install-additional-python-packages-on-sql-server.md)

## Prerequisites

Before you attempt to download or install any new package, review the requirements:

+ Make sure that there is a Windows version of the package: [Getting the correct package version and format](#packageVersion)

+ Identify all package dependencies, and ascertain their compatibility with the SQL Server environment.

+ Verify R version or Python versions compatibility. The package must be compatible with the version of R or Python that is running in SQL Server.

+ Permissions. Determine whether you have rights to install the package. To install to the instance library, administrative access to the computer running SQL Server is required. If you don't have administrative access to the SQL Server computer, find a database administrator to help with package installation.

    In SQL Server 2017, you can install R packages from a remote client, if package management has been enabled on the server and you are database owner or member of a package management role.

+ Consider the risks and benefits of installing a particular package into the SQL Server environment. Check whether the package (or any packages that it requires) contains features that would be blocked by SQL Server or by policy. Many R and Python packages are a very poor fit for a hardened SQL Server environment. Problems might include:

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

For Python, you must similarly prepare all dependencies and save them locally. Be sure to use a Windows-compatible binaries and use the WHL format.
