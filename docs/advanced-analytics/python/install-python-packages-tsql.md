---
title: Use T-SQL (CREATE EXTERNAL LIBRARY) to install Python packages
description: Learn how to install new Python packages on an instance of SQL Server Machine Learning Services using T-SQL (CREATE EXTERNAL LIBRARY).
ms.prod: sql
ms.technology: machine-learning

ms.date: 08/08/2019
ms.topic: conceptual
author: garyericson
ms.author: garye
ms.reviewer: davidph
monikerRange: ">sql-server-2017||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---

# Use T-SQL to install Python packages

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This article explains how to install new Python packages on an instance of SQL Server Machine Learning Services using T-SQL. This method works best for server administrators who are unfamiliar with Python.

The [CREATE EXTERNAL LIBRARY](https://docs.microsoft.com/sql/t-sql/statements/create-external-library-transact-sql) statement makes it possible to add a package or set of packages to an instance or a specific database without running Python code directly. However, this method requires package preparation and additional database permissions.

+ All packages must be available in a local folder, rather than downloaded on demand from the internet. The statement fails if required packages are not available, including downstream package dependencies.

+ You must be **db_owner** or have CREATE EXTERNAL LIBRARY permission in a database role. For details, see [CREATE EXTERNAL LIBRARY](https://docs.microsoft.com/sql/t-sql/statements/create-external-library-transact-sql).

## Download a collection of packages

If you are installing a single package with no dependencies, just download the package WHL file and copy it to the server.

It's more common, however, that a package will be dependent on a number of other packages. When a package requires other packages, you must verify that all of them are accessible to each other during installation.

You can use the `pip download` command to collect a package and all its dependencies into a local folder which you can then copy to the server.

For example, the following command copies the **scikit-plot** package and all its dependencies to the folder `scikitplot`:

```command
pip download scikit-plot -d scikitplot
```

You can now copy the `scikitplot` folder containing all packages to a local folder on the server.

## Upload the collection file to the database

1. In SQL Server Management Studio, open a **Query** window, using an account with administrative privileges.

1. Run the T-SQL statement `CREATE EXTERNAL LIBRARY` to upload the collection folder to the database:

   ```sql
   CREATE EXTERNAL LIBRARY scikit-plot
   FROM (CONTENT = 'C:\Temp\scikitplot')
   WITH (LANGUAGE = 'Python');
   ```

   You cannot use an arbitrary name; the external library name must have the same name that you expect to use when loading or calling the package.

## Verify package installation

If the library is successfully created, you can run the package in SQL Server by calling it inside a stored procedure. For example:

```sql
EXEC sp_execute_external_script
@language =N'R',
@script=N'library(randomForest)'
```

## See also

+ [Get R package information](../package-management/r-package-information.md)
+ [R tutorials](../tutorials/sql-server-r-tutorials.md)
