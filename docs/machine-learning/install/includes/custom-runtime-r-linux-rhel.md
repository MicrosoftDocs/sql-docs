---
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 03/18/2021
ms.topic: include
author: dphansen
ms.author: davidph
---
## Prerequisites

Before installing a R custom runtime, install the following:

+ Install SQL Server 2019 for Linux. You can install SQL Server on Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES) version 12, and Ubuntu. For more information, see [the Installation guidance for SQL Server on Linux](../../../linux/sql-server-linux-setup.md).

+ Upgrade to Cumulative Update (CU) 3 or later for SQL Server 2019. Follow these steps:
    1. Configure the repositories for Cumulative Updates. For more information, see [Configure repositories for installing and upgrading SQL Server on Linux](../../../linux/sql-server-linux-change-repo.md).

    1. Update the **mssql-server** package to the latest Cumulative Update. For more information, see [the Update or Upgrade SQL Server section in the installation guidance for SQL Server on Linux](../../../linux/sql-server-linux-setup.md#upgrade).

+ RExtension requires GLIBCXX_3.4.20. Make sure the version of **libstdc++.so.6** on your Red Hat Enterprise Linux (RHEL) installation provides this.

## Install Language Extensions

> [!NOTE]
> If you have [Machine Learning Services](../../sql-server-machine-learning-services.md) installed on SQL Server 2019, the **mssql-server-extensibility** package for Language Extensions is already installed and you can skip this step.

Run the command below to install [SQL Server Language Extensions](../../../language-extensions/language-extensions-overview.md) on Red Hat Enterprise Linux (RHEL), which is used for the R custom runtime.

```bash
# Install as root or sudo
sudo yum install mssql-server-extensibility
```

## Install R

1. If you have [Machine Learning Services](../../sql-server-machine-learning-services.md) installed, R is already installed in `/opt/microsoft/ropen/3.5.2/lib64/R`. If you want to keep using this path as your R_HOME, you can skip this step.

    If you want to use a different runtime of R, you first need to remove `microsoft-r-open-mro` before continuing to install a new version.

    ```bash
    sudo yum erase microsoft-r-open-mro-3.5.2
    ```

1. Install [R (3.3 or later)](https://www.r-project.org/) for Red Hat Enterprise Linux (RHEL). By default, R is installed in **/usr/lib64/R**. This path is your **R_HOME**. If you install R in a different location, take note of that path as your **R_HOME**.

    ```bash
    sudo yum install -y R
    ```
