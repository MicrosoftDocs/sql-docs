---
title: Create a repository with miniCRAN
titleSuffix: SQL machine learning
description: Learn how to install R packages offline by using the miniCRAN package to create a local repository of packages and dependencies.
ms.service: sql
ms.subservice: machine-learning
ms.date: 11/20/2019
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf

ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# Create a local R package repository using miniCRAN
[!INCLUDE [SQL Server 2016 SQL MI](../../includes/applies-to-version/sqlserver2016-asdbmi.md)]

This article describes how to install R packages offline by using [miniCRAN](https://cran.r-project.org/web/packages/miniCRAN/index.html) to create a local repository of packages and dependencies. **miniCRAN** identifies and downloads packages and dependencies into a single folder that you copy to other computers for offline R package installation.

You can specify one or more packages, and **miniCRAN** recursively reads the dependency tree for these packages. It then downloads only the listed packages and their dependencies from CRAN or similar repositories.

When it's done, **miniCRAN** creates an internally consistent repository consisting of the selected packages and all required dependencies. You can move this local repository to the server, and proceed to install the packages without an internet connection.

Experienced R users often look for the list of dependent packages in the DESCRIPTION file of a downloaded package. However, packages listed in **Imports** might have second-level dependencies. For this reason, we recommend **miniCRAN** for assembling the full collection of required packages.

## Why create a local repository

The goal of creating a local package repository is to provide a single location that a server administrator or other users in the organization can use to install new R packages on a server, especially one that does not have internet access. After creating the repository, you can modify it by adding new packages or upgrading the version of existing packages.

Package repositories are useful in these scenarios:

- **Security**: Many R users are accustomed to downloading and installing new R packages at will, from CRAN or one of its mirror sites. However, for security reasons, production servers running [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] typically do not have internet connectivity.

- **Easier offline installation**: To install a package to an offline server requires that you also download all package dependencies. Using miniCRAN makes it easier to get all dependencies in the correct format and avoid dependency errors.

- **Improved version management**: In a multi-user environment, there are good reasons to avoid unrestricted installation of multiple package versions on the server. Use a local repository to provide a consistent set of packages for your users.

## Install miniCRAN

The **miniCRAN** package itself is dependent on 18 other CRAN packages, among which is the **RCurl** package, which has a system dependency on the **curl-devel** package. Similarly, package **XML** has a dependency on **libxml2-devel**. To resolve dependencies, we recommend that you build your local repository initially on a machine with full internet access.

Run the following commands on a computer with a base R, R tools, and internet connection. It's assumed that this is not your SQL Server computer. The following commands install the **miniCRAN** package and the **igraph** package. This example checks whether the package is already installed, but you can bypass the `if` statements and install the packages directly.

```R
if(!require("miniCRAN")) install.packages("miniCRAN") 
if(!require("igraph")) install.packages("igraph") 
library("miniCRAN")
```

## Set the CRAN mirror and MRAN snapshot

Specify a mirror site to use in getting packages. For example, you could use the MRAN site, or any other site in your region that contains the packages you need. If a download fails, try another mirror site.

```R
CRAN_mirror <- c(CRAN = "https://cran.cnr.berkeley.edu")
```

## Create a local folder

Create a local folder in which to store the collected packages. If you repeat this often, you might want to use a descriptive name, such as "miniCRANZooPackages" or "miniCRANMyRPackageV2".

Specify the folder as the local repo. R syntax uses a forward slash for path names, which is opposite from Windows conventions.

```R
local_repo <- "C:/miniCRANZooPackages"
```

## Add packages to the local repo

After **miniCRAN** is installed and loaded, create a list that specifies the additional packages you want to download.

Do **not** add dependencies to this initial list. The **igraph** package used by **miniCRAN** generates the list of dependencies automatically. For more information about how to use the generated dependency graph, see [Using miniCRAN to identify package  dependencies](https://cran.r-project.org/web/packages/miniCRAN/vignettes/miniCRAN-dependency-graph.html).

1. Add target packages "zoo" and "forecast" to a variable.

    ```R
    pkgs_needed <- c("zoo", "forecast")
    ```

2. Optionally, plot the dependency graph. This is not necessary, but it can be informative.

    ```R
    plot(makeDepGraph(pkgs_needed))
    ```

3. Create the local repo. Be sure to change the R version, if necessary, to the version installed on your SQL Server instance. If you did a component upgrade, your version might be newer than the original version. For more information, see [Get R package information](../package-management/r-package-information.md).

    ```R
    pkgs_expanded <- pkgDep(pkgs_needed, repos = CRAN_mirror);
    makeRepo(pkgs_expanded, path = local_repo, repos = CRAN_mirror, type = "win.binary", Rversion = "3.3");
    ```

   From this information, the miniCRAN package creates the folder structure that you need to copy the packages to the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] later.

At this point you should have a folder containing the packages you need and any additional packages that are required. The folder should contain a collection of zipped packages. Do not unzip the packages or rename any files.

Optionally, run the following code to list the packages contained in the local miniCRAN repository.

```R
pdb <- as.data.frame(pkgAvail(local_repo, type = "win.binary", Rversion = "3.3"), stringsAsFactors = FALSE);
head(pdb);
pdb$Package;
pdb[, c("Package", "Version", "License")]
```

## Add packages to the instance library

After you have a local repository with the packages you need, move the package repository to the SQL Server computer. The following procedure describes how to install the packages using R tools.

::: moniker range=">sql-server-2017||>=sql-server-linux-ver15||=azuresqldb-mi-current"
> [!NOTE]
> The recommended method for installing packages is using **sqlmlutils**. See [Install new R packages with sqlmlutils](install-additional-r-packages-on-sql-server.md).
::: moniker-end

1. Copy the folder containing the miniCRAN repository, in its entirety, to the server where you plan to install the packages. The folder typically has this structure: 

   `<miniCRAN root>/bin/windows/contrib/version/<all packages>`

   In this procedure, we assume a folder off the root drive.

2. Open an R tool associated with the instance (for example, you could use Rgui.exe). Right-click and select **Run as administrator** to allow the tool to make updates to your system.

   ::: moniker range="=sql-server-2016"
   - For example, the default file location for RGUI is `C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES\bin\x64`.
   ::: moniker-end

   ::: moniker range="=sql-server-2017"
   - For example, the file location for RGUI is `C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\bin\x64`.
   ::: moniker-end

   ::: moniker range=">sql-server-2017"
   - For example, the file location for RGUI is `C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\R_SERVICES\bin\x64`.
   ::: moniker-end

3. Get the path for the instance library, and add it to the list of library paths.

   ::: moniker range="=sql-server-2016"
   For example,

   ```R
   outputlib <- "C:/Program Files/Microsoft SQL Server/MSSQL13.MSSQLSERVER/R_SERVICES/library"
   ```

   ::: moniker-end

   ::: moniker range="=sql-server-2017"
   For example,

   ```R
   outputlib <- "C:/Program Files/Microsoft SQL Server/MSSQL14.MSSQLSERVER/R_SERVICES/library"
   ```

   ::: moniker-end

   ::: moniker range=">sql-server-2017"
   For example,

   ```R
   outputlib <- "C:/Program Files/Microsoft SQL Server/MSSQL15.MSSQLSERVER/R_SERVICES/library"
   ```

   ::: moniker-end

4. Specify the new location on the server where you copied the **miniCRAN** repository as `server_repo`.

    In this example, we assume that you copied the repository to a temporary folder on the server.

    ```R
    inputlib <- "C:/miniCRANZooPackages"
    ```

5. Since you're working in a new R workspace on the server, you must also furnish the list of packages to install.

    ```R
    mypackages <- c("zoo", "forecast")
    ```

6. Install the packages, providing the path to the local copy of the miniCRAN repo.

    ```R
    install.packages(mypackages, repos = file.path("file://", normalizePath(inputlib, winslash = "/")), lib = outputlib, type = "win.binary", dependencies = TRUE);
    ```

7. From the instance library, you can view the installed packages using a command like the following:

    ```R
    installed.packages()
    ```

## See also

+ [Get R package information](../package-management/r-package-information.md)
+ [R tutorials](../tutorials/r-tutorials.md)