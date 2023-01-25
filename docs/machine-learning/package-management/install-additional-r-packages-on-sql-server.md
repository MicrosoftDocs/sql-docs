---
title: Install R packages with sqlmlutils
description: Learn how to use sqlmlutils to install new R packages to an instance of SQL Server Machine Learning Services.
ms.service: sql
ms.subservice: machine-learning
ms.date: 12/15/2020
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom:
  - seo-lt-2019
  - intro-installation
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---

# Install R packages with sqlmlutils

[!INCLUDE [SQL Server 2019 SQL MI](../../includes/applies-to-version/sqlserver2019-asdbmi.md)]

::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15"
This article describes how to use functions in the [**sqlmlutils**](https://github.com/Microsoft/sqlmlutils) package to install R packages to an instance of [Machine Learning Services on SQL Server](../sql-server-machine-learning-services.md) and on [Big Data Clusters](../../big-data-cluster/machine-learning-services.md). The packages you install can be used in R scripts running in-database using the [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) T-SQL statement.

> [!NOTE]
> The **sqlmlutils** package described in this article is used for adding R packages to SQL Server 2019 or later. For SQL Server 2017 and earlier, see [Install packages with R tools](./install-r-packages-standard-tools.md?view=sql-server-2017&preserve-view=true).
::: moniker-end

::: moniker range="=azuresqldb-mi-current"
This article describes how to use functions in the [**sqlmlutils**](https://github.com/Microsoft/sqlmlutils) package to install R packages to an instance of [Azure SQL Managed Instance Machine Learning Services](/azure/azure-sql/managed-instance/machine-learning-services-overview). The packages you install can be used in R scripts running in-database using the [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) T-SQL statement.

> [!NOTE]
> You cannot update or uninstall packages that have been preinstalled on an instance of SQL Managed Instance Machine Learning Services. To view a list of packages currently installed, see [List all installed R packages](r-package-information.md#list-all-installed-r-packages).

::: moniker-end

## Prerequisites

- Install [R](https://www.r-project.org) and [RStudio Desktop](https://www.rstudio.com/products/rstudio/download/) on the client computer you use to connect to SQL Server. You can use any R IDE for running scripts, but this article assumes RStudio.

  The version of R on the client computer must match the version of R on the server, and packages you install must be compliant with the version of R you have.
  For information on which version of R is included with each SQL Server version, see [Python and R versions](../sql-server-machine-learning-services.md#versions).
  
  To verify the version of R on a particular SQL Server, use the following T-SQL command.

  ```sql
  EXECUTE sp_execute_external_script @language = N'R'
   , @script = N'print(R.version)'
  ```

- Install [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md) on the client computer you use to connect to SQL Server. You can use other database management or query tools, but this article assumes Azure Data Studio.

### Other considerations

- Package installation is specific to the SQL instance, database, and user you specify in the connection information you provide to **sqlmlutils**. To use the package in multiple SQL instances or databases, or for different users, you'll need to install the package for each one. The exception is that if the package is installed by a member of `dbo`, the package is *public* and is shared with all users. If a user installs a newer version of a public package, the public package is not affected but that user will have access to the newer version.

- R script running in SQL Server can use only packages installed in the default instance library. SQL Server cannot load packages from external libraries, even if that library is on the same computer. This includes R libraries installed with other Microsoft products.

- On a hardened SQL Server environment, you might want to avoid the following:
  - Packages that require network access
  - Packages that require elevated file system access
  - Packages used for web development or other tasks that don't benefit by running inside SQL Server

## Install sqlmlutils on the client computer

To use **sqlmlutils**, you first need to install it on the client computer you use to connect to SQL Server.

The **sqlmlutils** package depends on the **odbc** package, and **odbc** depends on a number of other packages. The following procedures install all of these packages in the correct order.

### Install sqlmlutils online

If the client computer has Internet access, you can download and install **sqlmlutils** and its dependent packages online.

1. Download the latest **sqlmlutils** file (`.zip` for Windows, `.tar.gz` for Linux) from https://github.com/microsoft/sqlmlutils/releases to the client computer. Don't expand the file.

1. Open a **Command Prompt** and run the following commands to install the packages **odbc** and **sqlmlutils**. Substitute the path to the **sqlmlutils** file you downloaded. The **odbc** package is found online and installed.

   ::: moniker range=">=sql-server-ver15||=azuresqldb-mi-current"
   ```console
   R.exe -e "install.packages('odbc', type='binary')"
   R.exe CMD INSTALL sqlmlutils_1.0.0.zip
   ```
   ::: moniker-end

   ::: moniker range=">=sql-server-linux-ver15"
   ```console
   R.exe -e "install.packages('odbc')"
   R.exe CMD INSTALL sqlmlutils_1.0.0.tar.gz
   ```
   ::: moniker-end

### Install sqlmlutils offline

If the client computer doesn't have an Internet connection, you need to download the **odbc** and **sqlmlutils** packages in advance using a computer that does have Internet access. You then can copy the files to a folder on the client computer and install the packages offline.

The **odbc** package has a number of dependent packages, and identifying all dependencies for a package gets complicated. We recommend that you use [**miniCRAN**](https://andrie.github.io/miniCRAN/) to create a local repository folder for the package that includes all the dependent packages.
For more information, see [Create a local R package repository using miniCRAN](create-a-local-package-repository-using-minicran.md).

The **sqlmlutils** package consists of a single file that you can copy to the client computer and install.

On a computer with Internet access:

1. Install **miniCRAN**. See [Install miniCRAN](create-a-local-package-repository-using-minicran.md#install-minicran) for details.

1. In RStudio, run the following R script to create a local repository of the package **odbc**. This example assumes the repository will be created in the folder `odbc`.

   ::: moniker range=">=sql-server-ver15||=azuresqldb-mi-current"
   ```R
   library("miniCRAN")
   CRAN_mirror <- c(CRAN = "https://cran.microsoft.com")
   local_repo <- "odbc"
   pkgs_needed <- "odbc"
   pkgs_expanded <- pkgDep(pkgs_needed, repos = CRAN_mirror);

   makeRepo(pkgs_expanded, path = local_repo, repos = CRAN_mirror, type = "win.binary", Rversion = "3.5");
   ```
   ::: moniker-end

   ::: moniker range=">=sql-server-linux-ver15"
   ```R
   library("miniCRAN")
   CRAN_mirror <- c(CRAN = "https://cran.microsoft.com")
   local_repo <- "odbc"
   pkgs_needed <- "odbc"
   pkgs_expanded <- pkgDep(pkgs_needed, repos = CRAN_mirror);

   makeRepo(pkgs_expanded, path = local_repo, repos = CRAN_mirror, type = "source", Rversion = "3.5");
   ```
   ::: moniker-end

   For the `Rversion` value, use the version of R installed on SQL Server. To verify the installed version, use the following T-SQL command.

   ```sql
   EXECUTE sp_execute_external_script @language = N'R'
    , @script = N'print(R.version)'
   ```

1. Download the latest **sqlmlutils** file (`.zip` for Windows, `.tar.gz` for Linux) from [https://github.com/microsoft/sqlmlutils/releases](https://github.com/microsoft/sqlmlutils/releases). Don't expand the file.

1. Copy the entire **odbc** repository folder and the **sqlmlutils** file to the client computer.

On the client computer you use to connect to SQL Server:

1. Open a command prompt.

1. Run the following commands to install **odbc** and then **sqlmlutils**. Substitute the full paths to the **odbc** repository folder and the **sqlmlutils** file you copied to this computer.

   ::: moniker range=">=sql-server-ver15||=azuresqldb-mi-current"
   ```console
   R.exe -e "install.packages('odbc', repos='odbc')"
   R.exe CMD INSTALL sqlmlutils_1.0.0.zip
   ```
   ::: moniker-end

   ::: moniker range=">=sql-server-linux-ver15"
   ```console
   R.exe -e "install.packages('odbc', repos='odbc')"
   R.exe CMD INSTALL sqlmlutils_1.0.0.tar.gz
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

   ::: moniker range=">=sql-server-ver15||=azuresqldb-mi-current"
   ```R
   library("miniCRAN")
   CRAN_mirror <- c(CRAN = "https://cran.microsoft.com")
   local_repo <- "c:/downloads/glue"
   pkgs_needed <- "glue"
   pkgs_expanded <- pkgDep(pkgs_needed, repos = CRAN_mirror);

   makeRepo(pkgs_expanded, path = local_repo, repos = CRAN_mirror, type = "win.binary", Rversion = "3.5");
   ```
   ::: moniker-end

   ::: moniker range=">=sql-server-linux-ver15"
   ```R
   library("miniCRAN")
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

## More sqlmlutils functions

The **sqlmlutils** package contains a number of functions for managing R packages, and for creating, managing, and running stored procedures and queries in a SQL Server. For details, see the [sqlmlutils R README file](https://github.com/microsoft/sqlmlutils/tree/master/R).

For information about any **sqlmlutils** function, use the R **help** function or **?** operator. For example:

```R
library(sqlmlutils)
help("sql_install.packages")
```

## Next steps

- For information about installed R packages, see [Get R package information](r-package-information.md)
- For help in working with R packages, see [Tips for using R packages](tips-for-using-r-packages.md)
- For information about installing Python packages, see [Install Python packages with pip](install-additional-python-packages-on-sql-server.md)
- For more information about SQL Server Machine Learning Services, see [What is SQL Server Machine Learning Services (Python and R)?](../sql-server-machine-learning-services.md)
