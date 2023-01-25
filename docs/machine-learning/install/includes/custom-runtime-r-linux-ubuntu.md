---
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 02/08/2021
ms.topic: include
author: WilliamDAssafMSFT
ms.author: wiassaf
---
## Install Language Extensions

> [!NOTE]
> If you have [Machine Learning Services](../../sql-server-machine-learning-services.md) installed on SQL Server 2019, the **mssql-server-extensibility** package for Language Extensions is already installed and you can skip this step.

Run the commands below to install [SQL Server Language Extensions](../../../language-extensions/language-extensions-overview.md) on Ubuntu Linux, which is used for the R custom runtime.

1. If possible, run this command to refresh the packages on the system prior to the installation.

    ```bash
    # Install as root or sudo
    sudo apt-get update
    ```

1. Install **mssql-server-extensibility** with this command.

    ```bash
    # Install as root or sudo
    sudo apt-get install mssql-server-extensibility
    ```

## Install R

1. If you have [Machine Learning Services](../../sql-server-machine-learning-services.md) installed, R is already installed in `/opt/microsoft/ropen/3.5.2/lib64/R`. If you want to keep using this path as your R_HOME, you can skip this step.

    If you want to use a different runtime of R, you first need to remove `microsoft-r-open-mro` before continuing to install a new version.

    ```bash
    sudo apt remove microsoft-r-open-mro-3.5.2
    ```

1. Install [R (3.3 or later)](https://www.r-project.org/) for Ubuntu. By default, R is installed in **/usr/lib/R**. This path is your **R_HOME**. If you install R in a different location, take note of that path as your **R_HOME**.

    Below are example instructions for Ubuntu. Change the repository URL below for your version of R.

    ```bash
    export DEBIAN_FRONTEND=noninteractive
    sudo apt-get update
    sudo apt-get --no-install-recommends -y install curl zip unzip apt-transport-https libstdc++6
    
    # Add R CRAN repository. This repository works for R 4.0.x.
    #
    sudo apt-key adv --keyserver keyserver.ubuntu.com --recv-keys E298A3A825C0D65DFD57CBB651716619E084DAB9
    sudo add-apt-repository 'deb https://cloud.r-project.org/bin/linux/ubuntu xenial-cran40/'
    sudo apt-get update
    
    # Install R runtime.
    #
    sudo apt-get -y install r-base-core
    ```
