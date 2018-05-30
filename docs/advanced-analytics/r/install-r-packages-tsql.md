---
title: Use T-SQL (CREATE EXTERNAL LIBRARY) to install R packages on SQL Server Machine Learning Services| Microsoft Docs
description: Add new R packages to SQL Server 2017 Machine Learning Services (In-Database)
ms.prod: sql
ms.technology: machine-learning

ms.date: 05/20/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---

# Use T-SQL (CREATE EXTERNAL LIBRARY) to install R packages on SQL Server 2017 Machine Learning Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes how to install new R packages to an instance of SQL Server where machine learning is enabled. There are multiple approaches to choose from. This approach works best for server administrators who are unfamiliar with R.

**Applies to:**  [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] [!INCLUDE[rsql-productnamenew-md](../../includes/rsql-productnamenew-md.md)]

The [CREATE EXTERNAL LIBRARY](https://docs.microsoft.com/sql/t-sql/statements/create-external-library-transact-sql) statement makes it possible to add a package or set of packages to an instance or a specific database without running R or Python code directly. However, this method requires package preparation and additional database permissions.

+ All packages must be be available as a local zipped file, rather than downloaded on demand from the internet.

+ All dependencies must be identified by name and version, and included in the zip file. The statement fails if required packages are not available, including downstream package dependencies. 

+ You must be **db_owner** or have CREATE EXTERNAL LIBRARY permission in a database role. For details, see [CREATE EXTERNAL LIBRARY](https://docs.microsoft.com/sql/t-sql/statements/create-external-library-transact-sql).

## Download packages in archive format

If you are installing a single package, download the package in zipped format.

If the package requires any other packages, you must verify that the required packages are available. You can use miniCRAN to analyze the target package and identify all its dependencies. We recommend using [**miniCRAN**](create-a-local-package-repository-using-minicran.md) or [**igraph**](http://igraph.org/r/) for analyzing packages dependencies. Installing the wrong version of package or package dependency can also cause the statement to fail. 

## Copy the file to a local folder

Copy the zipped file containing all packages to a local folder on the server. If you do not have access to the file system on the server, you can also pass a complete package as a variable, using a binary format. For more information, see [CREATE EXTERNAL LIBRARY](../../t-sql/statements/create-external-library-transact-sql.md).

## Run the statement to upload packages

Open a **Query** window, using an account with administrative privileges.

Run the T-SQL statement `CREATE EXTERNAL LIBRARY` to upload the zipped package collection to the database.

    For example, the following statement names as the package source a miniCRAN repository containing the **randomForest** package, together with its dependencies. 

    ```R
    CREATE EXTERNAL LIBRARY randomForest
    FROM (CONTENT = 'C:\Temp\Rpackages\randomForest_4.6-12.zip')
    WITH (LANGUAGE = 'R');
    ```

    You cannot use an arbitrary name; the external library name must have the same name that you expect to use when loading or calling the package.

## Verify package installation

If the library is successfully created, you can run the package in SQL Server, by calling it inside a stored procedure.
    
    ```SQL
    EXEC sp_execute_external_script
    @language =N'R',
    @script=N'
    library(randomForest)'
    ```

