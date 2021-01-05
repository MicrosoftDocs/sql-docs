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
> If you have [Machine Learning Services](../sql-server-machine-learning-services.md) installed on SQL Server 2019, the **mssql-server-extensibility** package for Language Extensions is already installed and you can skip this step.

Run the command below to install [SQL Server Language Extensions](../../language-extensions/language-extensions-overview.md) on SUSE Linux Enterprise Server (SLES), which is used for the Python custom runtime.

```bash
# Install as root or sudo
sudo zypper install mssql-server-extensibility
```

## Install Python 3.7 and pandas

Follow the steps below to install Python 3.7, the libpython3.7 library, and the pandas package.:

```bash
# Install python3.7 and the corresponding library:
sudo add-apt-repository ppa:deadsnakes/ppa
sudo apt-get update
sudo apt-get install python3.7 python3-pip libpython3.7

# Install pandas to /usr/lib:
sudo python3.7 -m pip install pandas -t /usr/lib/python3.7/dist-packages
```
