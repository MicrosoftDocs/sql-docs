---
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 01/05/2021
ms.topic: include
author: dphansen
ms.author: davidph
---
## Install Language Extensions

> [!NOTE]
> If you have [Machine Learning Services](../../sql-server-machine-learning-services.md) installed on SQL Server 2019, the **mssql-server-extensibility** package for Language Extensions is already installed and you can skip this step.

Run the commands below to install [SQL Server Language Extensions](../../../language-extensions/language-extensions-overview.md) on Ubuntu Linux, which is used for the Python custom runtime.

1. If possible, run this command to refresh the packages on the system prior to the installation.

    ```bash
    # Install as root or sudo
    sudo apt-get update
    ```

1. Ubuntu might not have the https apt transport option. To install it, run this command.

    ```bash
    # Install as root or sudo
    apt-get install apt-transport-https
    ```

1. Install **mssql-server-extensibility** with this command.

    ```bash
    # Install as root or sudo
    sudo apt-get install mssql-server-extensibility
    ```

## Install Python 3.7 and pandas

1. Run the commands below to install Python 3.7.

    ```bash
    # Install python3.7 and the corresponding library:
    sudo add-apt-repository ppa:deadsnakes/ppa
    sudo apt-get update
    sudo apt-get install python3.7 python3-pip libpython3.7
    ```

1. Run the command below to install the pandas package

    ```bash
    # Install pandas to /usr/lib:
    sudo python3.7 -m pip install pandas -t /usr/lib/python3.7/dist-packages
    ```
