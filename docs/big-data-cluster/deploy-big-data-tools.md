---
title: Install SQL Server 2019 big data tools | Microsoft Docs
description: Discover how to install tools used with SQL Server 2019 big data clusters (preview).
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 12/04/2018
ms.topic: conceptual
ms.prod: sql
---

# Install SQL Server 2019 big data tools

This article describes the client tools that should be installed for creating, managing, and using SQL Server 2019 big data clusters (preview).

[!INCLUDE [Limited public preview note](../includes/big-data-cluster-preview-note.md)]

## Big data cluster tools

The following table lists common big data cluster tools and how to intall them:

| Tool | Description | Installation |
|---|---|---|
| **mssqlctl** | Command-line tool for installing and managing a big data cluster. | [Install](deploy-install-mssqlctl.md) |
| **kubectl** | Command-line tool for monitoring the underlying Kuberentes cluster. | [Install](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-kubectl) |
| **Azure Data Studio** | Cross-platform graphical tool for querying SQL Server. | [Install](../azure-data-studio/download.md) |
| **SQL Server 2019 extension** | Extension for Azure Data Studio that supports connecting to the big data cluster. Also provides a Data Virtualization wizard. | [Install](../azure-data-studio/sql-server-2019-extension.md) |
| **mssql-cli** | Modern command-line interface for querying SQL Server. | [Install](https://github.com/dbcli/mssql-cli/blob/master/doc/installation_guide.md) |
| **sqlcmd** | Legacy command-line tool for querying SQL Server. | [Windows](https://www.microsoft.com/download/details.aspx?id=36433) \| [Linux](../linux/sql-server-linux-setup-tools.md) |
| **curl** | Command-line tool for transferring data with URLs. | [Windows](https://curl.haxx.se/windows/) \| Linux: install curl package |

## Which tools are required?

The previous table provides all of the common tools that are used with big data clusters. Which tools are required depends on your scenario. But in general, the following tools are most important for managing, connecting to, and querying the cluster:

- **mssqlctl**
- **kubectl**
- **Azure Data Studio**
- **SQL Server 2019 extension**

The remaining tools are only required in certain scenarios. **mssql-cli** is an optional but useful tool that allows you to connect to the SQL Server master instance in the cluster and run queries from the command-line. And **sqlcmd** and **curl** are required if you plan to install sample data with the GitHub script.

## Next steps

For more information about big data clusters, see [What are SQL Server 2019 big data clusters?](big-data-cluster-overview.md).
