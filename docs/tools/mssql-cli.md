---
title: mssql-cli
description: 'mssql-cli is an interactive command-line query tool for SQL Server that runs on Windows, macOS, or Linux.'
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: alayu, maghan
ms.custom: tools|mssql-cli
ms.date: 02/22/2018
monikerRange: "=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017"
---

# mssql-cli command-line query tool for SQL Server (Preview)

[!INCLUDE[appliesto-ss-asdb-asdw-xxx-md](../includes/appliesto-ss-asdb-asdw-xxx-md.md)]

mssql-cli is an interactive command-line tool for querying SQL Server and runs on Windows, macOS, or Linux.

## Install mssql-cli

For detailed installation instructions, see the [Installation Guide](https://github.com/dbcli/mssql-cli/tree/master/doc/installation). The most common install scenarios are summarized below.

### Windows and macOS Installation

mssql-cli is installed on Windows and macOS using `pip`:

```$ pip install mssql-cli```

For more detailed instructions, please see the [Installation Guide](https://github.com/dbcli/mssql-cli/tree/master/doc/installation).

### Linux Installation

After registering the Microsoft repository, mssql-cli can be installed and upgraded through package managers on several Linux distributions.

The following example applies to Ubuntu 18.04 (Bionic), more information and examples for other distributions can be found in the [Installation Guide](https://github.com/dbcli/mssql-cli/tree/master/doc/installation).

```
# Import the public repository GPG keys
curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -

# Register the Microsoft Ubuntu repository
sudo apt-add-repository https://packages.microsoft.com/ubuntu/18.04/prod

# Update the list of products
sudo apt-get update

# Install mssql-cli
sudo apt-get install mssql-cli

# Install missing dependencies
sudo apt-get install -f
```

## mssql-cli documentation

Documentation for mssql-cli is located in the [mssql-cli GitHub repository](https://github.com/dbcli/mssql-cli).

- [Main page/readme](https://github.com/dbcli/mssql-cli)
- [Installation Guide](https://github.com/dbcli/mssql-cli/tree/master/doc/installation)
- [Usage Guide](https://github.com/dbcli/mssql-cli/blob/master/doc/usage_guide.md)

Additional documentation is located in the [doc folder](https://github.com/dbcli/mssql-cli/tree/master/doc).
