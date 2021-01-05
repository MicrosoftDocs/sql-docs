---
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 01/05/2021
ms.topic: include
author: dphansen
ms.author: davidph
---
## Prerequisites

Before installing a Python custom runtime, install the following:

+ Install SQL Server 2019 for Linux. You can install SQL Server on Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES), and Ubuntu. For more information, see [the Installation guidance for SQL Server on Linux](../../linux/sql-server-linux-setup.md).

+ Upgrade to Cumulative Update (CU) 3 or later for SQL Server 2019. Follow these steps:
    1. Configure the repositories for Cumulative Updates. For more information, see [Configure repositories for installing and upgrading SQL Server on Linux](../../linux/sql-server-linux-change-repo.md).

    1. Update the **mssql-server** package to the latest Cumulative Update. For more information, see [the Update or Upgrade SQL Server section in the installation guidance for SQL Server on Linux](../../linux/sql-server-linux-setup.md#upgrade).

+ Install [Python 3.7](https://www.python.org/downloads/) on the server.

    The Python language extension used for the custom Python runtime currently supports Python 3.7 only. If you would like to use a different version of Python, follow the instruction in the [Python Language Extension GitHub repo](https://github.com/microsoft/sql-server-language-extensions/tree/master/language-extensions/python) to modify and rebuild the extension.