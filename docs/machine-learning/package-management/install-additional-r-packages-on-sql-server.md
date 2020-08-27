---
title: Install new R packages
description: Learn how to use sqlmlutils to install new R packages to an instance of SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning
ms.date: 06/04/2020
ms.topic: how-to
author: garyericson
ms.author: garye
ms.reviewer: davidph
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=azuresqldb-mi-current||=sqlallproducts-allversions"
---

# Install new R packages with sqlmlutils

[!INCLUDE [SQL Server 2019 SQL MI](../../includes/applies-to-version/sqlserver2019-asdbmi.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
This article describes how to use functions in the [**sqlmlutils**](https://github.com/Microsoft/sqlmlutils) package to install new R packages to an instance of [Machine Learning Services on SQL Server](../sql-server-machine-learning-services.md) and on [Big Data Clusters](../../big-data-cluster/machine-learning-services.md). The packages you install can be used in R scripts running in-database using the [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) T-SQL statement.

> [!NOTE]
> The **sqlmlutils** package described in this article is used for adding R packages to SQL Server 2019 or later. For SQL Server 2017 and earlier, see [Install packages with R tools](https://docs.microsoft.com/sql/machine-learning/package-management/install-r-packages-standard-tools?view=sql-server-2017).
::: moniker-end
::: moniker range="=azuresqldb-mi-current||=sqlallproducts-allversions"
This article describes how to use functions in the [**sqlmlutils**](https://github.com/Microsoft/sqlmlutils) package to install new R packages to an instance of [Azure SQL Managed Instance Machine Learning Services](/azure/azure-sql/managed-instance/machine-learning-services-overview). The packages you install can be used in R scripts running in-database using the [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) T-SQL statement.
::: moniker-end

## Prerequisites

- Install [R](https://www.r-project.org) and [RStudio Desktop](https://www.rstudio.com/products/rstudio/download/) on the client computer you use to connect to SQL Server. You can use any R IDE for running scripts, but this article assumes RStudio.

- Install [Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/what-is) on the client computer you use to connect to SQL Server. You can use other database management or query tools, but this article assumes Azure Data Studio.

### Other considerations

- Package installation is specific to the SQL instance, database, and user you specify in the connection information you provide to **sqlmlutils**. To use the package in multiple SQL instances or databases, or for different users, you'll need to install the package for each one. The exception is that if the package is installed by a member of `dbo`, the library is public and can be shared with all users.

- R script running in SQL Server can use only packages installed in the default instance library. SQL Server cannot load packages from external libraries, even if that library is on the same computer. This includes R libraries installed with other Microsoft products.

- On a hardened SQL Server environment, you might want to avoid the following:
  - Packages that require network access
  - Packages that require elevated file system access
  - Packages used for web development or other tasks that don't benefit by running inside SQL Server

## Install sqlmlutils on the client computer

To use **sqlmlutils**, you first need to install it on the client computer you use to connect to SQL Server.

The **sqlmlutils** package depends on the **RODBCext** package, and **RODBCext** depends on a number of other packages. The following procedures install all of these packages in the correct order.

### Install sqlmlutils online

If the client computer has Internet access, you can download and install **sqlmlutils** and its dependent packages online.

1. Download the latest **sqlmlutils** file (`.zip` for Windows, `.tar.gz` for Linux) from https://github.com/Microsoft/sqlmlutils/tree/master/R/dist to the client computer. Don't expand the file.

1. Open a **Command Prompt** and run the following commands to install the packages **RODBCext** and **sqlmlutils**. Substitute the path to the **sqlmlutils** file you downloaded. The **RODBCext** package is found online and installed.

   ::: moniker range=">=sql-server-ver15||=sqlallproducts-allversions"
   ```console
   R -e "install.packages('RODBCext', repos='https://mran.microsoft.com/snapshot/2019-02-01/')"
   R CMD INSTALL sqlmlutils_0.7.1.zip
   ```
   ::: moniker-end

   ::: moniker range=">=sql-server-linux-ver15||=sqlallproducts-allversions"
   ```console
   R -e "install.packages('RODBCext', repos='https://mran.microsoft.com/snapshot/2019-02-01/')"
   R CMD INSTALL sqlmlutils_0.7.1.tar.gz
   ```
   ::: moniker-end

### Install sqlmlutils offline

If the client computer doesn't have an Internet connection, you need to download the packages **RODBCext** and **sqlmlutils** in advance using a computer that does have Internet access. You then can copy the files to a folder on the client computer and install the packages offline.

The **RODBCext** package has a number of dependent packages, and identifying all dependencies for a package gets complicated. We recommend that you use [**miniCRAN**](https://andrie.github.io/miniCRAN/) to create a local repository folder for the package that includes all the dependent packages.
For more information, see [Create a local R package repository using miniCRAN](create-a-local-package-repository-using-minicran.md).

The **sqlmlutils** package consists of a single file that you can copy to the client computer and install.

On a computer with Internet access:

1. Install **miniCRAN**. See [Install miniCRAN](create-a-local-package-repository-using-minicran.md#install-minicran) for details.

1. In RStudio, run the following R script to create a local repository of the package **RODBCext**. This example assumes the repository will be created in the folder `rodbcext`.

   ::: moniker range=">=sql-server-ver15||=sqlallproducts-allversions"
   ```R
   CRAN_mirror <- c(CRAN = "https://mran.microsoft.com/snapshot/2019-02-01/")
   local_repo <- "rodbcext"
   pkgs_needed <- "RODBCext"
   pkgs_expanded <- pkgDep(pkgs_needed, repos = CRAN_mirror);

   makeRepo(pkgs_expanded, path = local_repo, repos = CRAN_mirror, type = "win.binary", Rversion = "3.5");
   ```
   ::: moniker-end

   ::: moniker range=">=sql-server-linux-ver15||=sqlallproducts-allversions"
   ```R
   CRAN_mirror <- c(CRAN = "https://mran.microsoft.com/snapshot/2019-02-01/")
   local_repo <- "rodbcext"
   pkgs_needed <- "RODBCext"
   pkgs_expanded <- pkgDep(pkgs_needed, repos = CRAN_mirror);

   makeRepo(pkgs_expanded, path = local_repo, repos = CRAN_mirror, type = "source", Rversion = "3.5");
   ```
   ::: moniker-end

   For the `Rversion` value, use the version of R installed on SQL Server. To verify the installed version, use the following T-SQL command.

   ```sql
   EXECUTE sp_execute_external_script @language = N'R'
    , @script = N'print(R.version)'
   ```

1. Download the latest **sqlmlutils** file (`.zip` for Windows, `.tar.gz` for Linux) from [https://github.com/Microsoft/sqlmlutils/tree/master/R/dist](https://github.com/Microsoft/sqlmlutils/tree/master/R/dist). Don't expand the file.

1. Copy the entire **RODBCext** repository folder and the **sqlmlutils** file to the client computer.

On the client computer you use to connect to SQL Server:

1. Open a command prompt.

1. Run the following commands to install **RODBCext** and then **sqlmlutils**. Substitute the full paths to the **RODBCext** repository folder and the **sqlmlutils** file you copied to this computer.

   ::: moniker range=">=sql-server-ver15||=sqlallproducts-allversions"
   ```console
   R -e "install.packages('RODBCext', repos='rodbcext')"
   R CMD INSTALL sqlmlutils_0.7.1.zip
   ```
   ::: moniker-end

   ::: moniker range=">=sql-server-linux-ver15||=sqlallproducts-allversions"
   ```console
   R -e "install.packages('RODBCext', repos='rodbcext')"
   R CMD INSTALL sqlmlutils_0.7.1.tar.gz
   ```
   ::: moniker-end

## Add an R package on SQL Server

In the following example, you'll add the [**glue**](https://cran.r-project.org/web/packages/glue/) package to SQL Server.

### Add the package online

If the client computer you use to connect to SQL Server has Internet access, you can use **sqlmlutils** to find the **glue** package and any dependencies over the Internet, and then install the package to a SQL Server instance remotely.

1. On the client computer, open RStudio and create a new **R Script** file.

1. Use the following R script to install the **glue** package using **sqlmlutils**. Substitute your own SQL Server database connection information.

   ```R
   library(sqlmlutils)
   connection <- connectionInfo(
     server   = "server",
     database = "database",
     uid      = "username",
     pwd      = "password")

   sql_install.packages(connectionString = connection, pkgs = "glue", verbose = TRUE, scope = "PUBLIC")
   ```

   > [!TIP]
   > The **scope** can be either **PUBLIC** or **PRIVATE**. Public scope is useful for the database administrator to install packages that all users can use. Private scope makes the package  available only to the user who installs it. If you don't specify the scope, the default scope is **PRIVATE**.

### Add the package offline

If the client computer doesn't have an Internet connection, you can use **miniCRAN** to download the **glue** package using a computer that does have Internet access. You then copy the package to the client computer where you can install the package offline.
See [Install miniCRAN](create-a-local-package-repository-using-minicran.md#install-minicran) for information on installing **miniCRAN**.

On a computer with Internet access:

1. Run the following R script to create a local repository for **glue**. This example creates the repository folder in `c:\downloads\glue`.

   ::: moniker range=">=sql-server-ver15||=sqlallproducts-allversions"
   ```R
   CRAN_mirror <- c(CRAN = "https://cran.microsoft.com")
   local_repo <- "c:/downloads/glue"
   pkgs_needed <- "glue"
   pkgs_expanded <- pkgDep(pkgs_needed, repos = CRAN_mirror);

   makeRepo(pkgs_expanded, path = local_repo, repos = CRAN_mirror, type = "win.binary", Rversion = "3.5");
   ```
   ::: moniker-end

   ::: moniker range=">=sql-server-linux-ver15||=sqlallproducts-allversions"
   ```R
   CRAN_mirror <- c(CRAN = "https://cran.microsoft.com")
   local_repo <- "c:/downloads/glue"
   pkgs_needed <- "glue"
   pkgs_expanded <- pkgDep(pkgs_needed, repos = CRAN_mirror);

   makeRepo(pkgs_expanded, path = local_repo, repos = CRAN_mirror, type = "source", Rversion = "3.5");
   ```
   ::: moniker-end

   For the `Rversion` value, use the version of R installed on SQL Server. To verify the installed version, use the following T-SQL command.

   ```sql
   EXECUTE sp_execute_external_script @language = N'R'
    , @script = N'print(R.version)'
   ```

1. Copy the entire **glue** repository folder (`c:\downloads\glue`) to the client computer. For example, copy it to the folder `c:\temp\packages\glue`.

On the client computer:

1. Open RStudio and create a new **R Script** file.

1. Use the following R script to install the **glue** package using **sqlmlutils**. Substitute your own SQL Server database connection information (if you don't use Windows Authentication, add `uid` and `pwd` parameters).

   ```R
   library(sqlmlutils)
   connection <- connectionInfo(
     server= "yourserver",
     database = "yourdatabase")
   localRepo = "c:/temp/packages/glue"

   sql_install.packages(connectionString = connection, pkgs = "glue", verbose = TRUE, scope = "PUBLIC", repos=paste0("file:///",localRepo))
   ```

   > [!TIP]
   > The **scope** can be either **PUBLIC** or **PRIVATE**. Public scope is useful for the database administrator to install packages that all users can use. Private scope makes the package  available only to the user who installs it. If you don't specify the scope, the default scope is **PRIVATE**.

## Use the package

Once the **glue** package is installed, you can use it in an R script in SQL Server with the T-SQL **sp_execute_external_script** command.

1. Open Azure Data Studio and connect to your SQL Server database.

1. Run the following command:

   ```sql
   EXECUTE sp_execute_external_script @language = N'R'
       , @script = N'
   library(glue)

   name <- "Fred"
   birthday <- as.Date("2020-06-14")
   text <- glue(''My name is {name} '',
   ''and my birthday is {format(birthday, "%A, %B %d, %Y")}.'')

   print(text)
         ';
   ```

    **Results**

    ```text
    My name is Fred and my birthday is Sunday, June 14, 2020.
    ```

## Remove the package

If you would like to remove the **glue** package, run the following R script. Use the same **connection** variable you defined earlier.

```R
sql_remove.packages(connectionString = connection, pkgs = "glue", scope = "PUBLIC")
```

## Next steps

- For information about installed R packages, see [Get R package information](r-package-information.md)
- For help in working with R packages, see [Tips for using R packages](tips-for-using-r-packages.md)
- For information about installing Python packages, see [Install Python packages with pip](install-additional-python-packages-on-sql-server.md)
- For more information about SQL Server Machine Learning Services, see [What is SQL Server Machine Learning Services (Python and R)?](../sql-server-machine-learning-services.md)
