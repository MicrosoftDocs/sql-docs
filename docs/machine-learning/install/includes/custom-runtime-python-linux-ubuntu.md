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

The Python language extension used for the custom Python runtime currently supports [Python 3.7](https://www.python.org/) only. If you would like to use a different version of Python, follow the instruction in the [Python Language Extension GitHub repo](https://github.com/microsoft/sql-server-language-extensions/tree/master/language-extensions/python) to modify and rebuild the extension.

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
