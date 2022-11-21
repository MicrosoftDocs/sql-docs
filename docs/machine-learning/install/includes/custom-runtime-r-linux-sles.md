---
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 03/16/2021
ms.topic: include
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom:
  - intro-installation
---
## Install Language Extensions

> [!NOTE]
> If you have [Machine Learning Services](../../sql-server-machine-learning-services.md) installed on SQL Server 2019, the **mssql-server-extensibility** package for Language Extensions is already installed and you can skip this step.

Run the command below to install [SQL Server Language Extensions](../../../language-extensions/language-extensions-overview.md) on SUSE Linux Enterprise Server (SLES), which is used for the R custom runtime.

```bash
# Install as root or sudo
sudo zypper install mssql-server-extensibility
```

## Install R

1. If you have [Machine Learning Services](../../sql-server-machine-learning-services.md) installed, R is already installed in `/opt/microsoft/ropen/3.5.2/lib64/R`. If you want to keep using this path as your R_HOME, you can skip this step.

    If you want to use a different runtime of R, you first need to remove `microsoft-r-open-mro` before continuing to install a new version.

    ```bash
    sudo zypper remove microsoft-r-open-mro-3.4.4
    ```

1. Install [R (3.3 or later)](https://www.r-project.org/) for SUSE Linux Enterprise Server (SLES). By default, R is installed in **/usr/lib64/R**. This path is your **R_HOME**. If you install R in a different location, take note of that path as your **R_HOME**.

    Follow these steps to install R:

    ```bash
    sudo zypper ar -f http://download.opensuse.org/repositories/devel:/languages:/R:/patched/openSUSE_12.3/ R-patched
    sudo zypper --gpg-auto-import-keys ref
    sudo zypper install R-core-libs R-core R-core-doc R-patched
    ```

    You can ignore the warnings for **R-tcltk-3.6.1**, unless you need this package.

## Install gcc-c++

Install **gcc-c++** on SUSE Linux Enterprise Server (SLES). This is used for **Rcpp**, which is installed later.

```bash
sudo zypper install gcc-c++
```
