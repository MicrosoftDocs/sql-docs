---
title: Install big data toolkit
titleSuffix: SQL Server 2019 big data clusters
description: Discover how to install tools used with SQL Server 2019 big data clusters (preview).
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 12/04/2018
ms.topic: conceptual
ms.prod: sql
ms.custom: seodec18
---

# Install SQL Server 2019 big data tools

This article describes the client tools that should be installed for creating, managing, and using SQL Server 2019 big data clusters (preview).

[!INCLUDE [Limited public preview note](../includes/big-data-cluster-preview-note.md)]

## Big data cluster tools

The following table lists common big data cluster tools and how to install them:

| Tool | Description | Installation |
|---|---|---|
| **mssqlctl** | Command-line tool for installing and managing a big data cluster. | [Install](deploy-install-mssqlctl.md) |
| **kubectl**<sup>1</sup> | Command-line tool for monitoring the underlying Kuberentes cluster ([More info](https://kubernetes.io/docs/tasks/tools/install-kubectl/)). | [Install](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-kubectl) |
| **Azure Data Studio** | Cross-platform graphical tool for querying SQL Server ([More info](https://docs.microsoft.com/sql/azure-data-studio/what-is?view=sql-server-ver15)). | [Install](../azure-data-studio/download.md) |
| **SQL Server 2019 extension** | Extension for Azure Data Studio that supports connecting to the big data cluster. Also provides a Data Virtualization wizard. | [Install](../azure-data-studio/sql-server-2019-extension.md) |
| **Azure CLI**<sup>2</sup> | Modern command-line interface for managing Azure services. Used with AKS big data cluster deployments ([More info](https://docs.microsoft.com/en-us/cli/azure/?view=azure-cli-latest)). | [Install](https://docs.microsoft.com/cli/azure/install-azure-cli?view=azure-cli-latest) |
| **mssql-cli** | Modern command-line interface for querying SQL Server ([More info](https://github.com/dbcli/mssql-cli/blob/master/README.rst)). | [Windows](https://github.com/dbcli/mssql-cli/blob/master/doc/installation/windows.md) \| [Linux](https://github.com/dbcli/mssql-cli/blob/master/doc/installation/linux.md) |
| **sqlcmd** | Legacy command-line tool for querying SQL Server ([More info](https://docs.microsoft.com/en-us/sql/tools/sqlcmd-utility?view=sql-server-ver15)). | [Windows](https://www.microsoft.com/download/details.aspx?id=36433) \| [Linux](../linux/sql-server-linux-setup-tools.md) |
| **curl** | Command-line tool for transferring data with URLs. | [Windows](https://curl.haxx.se/windows/) \| Linux: install curl package |

<sup>1</sup> You must use Kubectl version 1.10 or later. If you want to install a specific version on kubectl client, see [Install kubectl binary via curl](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-kubectl). Also, for AKS, do not install kubectl with `az aks kubernetes install-cli`, because this installs a kubectl client with a version lower than the required 1.10. Instead, follow the link in this table to install the right version of kubectl client.

<sup>2</sup> You must be using Azure CLI version 2.0.4 or later. Run `az --version` to find the version if needed.

## Which tools are required?

The previous table provides all of the common tools that are used with big data clusters. Which tools are required depends on your scenario. But in general, the following tools are most important for managing, connecting to, and querying the cluster:

- **mssqlctl**
- **kubectl**
- **Azure Data Studio**
- **SQL Server 2019 extension**

The remaining tools are only required in certain scenarios. **Azure CLI** can be used to manage Azure services associated with AKS deployments. **mssql-cli** is an optional but useful tool that allows you to connect to the SQL Server master instance in the cluster and run queries from the command line. And **sqlcmd** and **curl** are required if you plan to install sample data with the GitHub script.

## Next steps

For More info about big data clusters, see [What are SQL Server 2019 big data clusters?](big-data-cluster-overview.md).
