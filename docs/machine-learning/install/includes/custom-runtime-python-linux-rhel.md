---
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 02/08/2021
ms.topic: include
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom:
  - intro-installation
---
## Install Language Extensions

> [!NOTE]
> If you have [Machine Learning Services](../../sql-server-machine-learning-services.md) installed on SQL Server 2019, the **mssql-server-extensibility** package for Language Extensions is already installed and you can skip this step.

Run the command below to install [SQL Server Language Extensions](../../../language-extensions/language-extensions-overview.md) on Red Hat Enterprise Linux (RHEL), which is used for the Python custom runtime.

```bash
# Install as root or sudo
sudo yum install mssql-server-extensibility
```

## Install Python 3.7 and pandas

The Python language extension used for the custom Python runtime currently supports [Python 3.7](https://www.python.org/) only. If you would like to use a different version of Python, follow the instruction in the [Python Language Extension GitHub repo](https://github.com/microsoft/sql-server-language-extensions/tree/master/language-extensions/python) to modify and rebuild the extension.

1. Run the commands below to install Python 3.7.

    ```bash
    # Install python3.7 and the corresponding library:
    yum install gcc openssl-devel bzip2-devel libffi-devel zlib-devel
    
    cd /usr/src
    wget https://www.python.org/ftp/python/3.7.9/Python-3.7.9.tgz
    tar xzf Python-3.7.9.tgz
    
    cd Python-3.7.9
    ./configure --enable-optimizations --prefix=/usr
    make altinstall
    ```

1. Run the command below to install the pandas package

    ```bash
    # Install pandas to /usr/lib:
    sudo python3.7 -m pip install pandas -t /usr/lib/python3.7/dist-packages
    ```
