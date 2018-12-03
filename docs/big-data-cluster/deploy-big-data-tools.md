---
title: Install SQL Server 2019 big data tools | Microsoft Docs
description: Learn how to connect to a SQL Server 2019 big data cluster with Azure Data Studio.
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 12/03/2018
ms.topic: conceptual
ms.prod: sql
---

# Install SQL Server 2019 big data tools

This article describes the client tools that should be installed for creating and managing SQL Server 2019 big data clusters (preview).

[!INCLUDE [Limited public preview note](../includes/big-data-cluster-preview-note.md)]

## Big data cluster tools

To manage big data clusters, install the following client tools:

| Tool | Installation |
|---|---|
| Azure Data Studio | [Install](../azure-data-studio/download.md) |
| SQL Server 2019 extension (preview) | [Install](../azure-data-studio/sql-server-2019-extension.md) |
| kubectl | [Install](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-kubectl) |
| sqlcmd | [Windows](https://www.microsoft.com/download/details.aspx?id=36433) \| [Linux](../linux/sql-server-linux-setup-tools.md) |
| curl | [Windows](https://curl.haxx.se/windows/) \| Linux: install curl package |

> [!TIP]
> **sqlcmd** and **curl** are required for some scripts and tutorials, but they are not a prerequisite for installing and using big data clusters.

## <a id="mssqlctl"></a> Install mssqlctl

**mssqlctl** is a command-line utility written in Python that enables cluster administrators to bootstrap and manage the big data cluster via REST APIs. The minimum Python version required is v3.5. You must also have `pip` that is used to download and install **mssqlctl** tool. The instructions below provide examples for Windows and Ubuntu. For installing Python on other platforms, see the [Python documentation](https://wiki.python.org/moin/BeginnersGuide/Download).

> [!IMPORTANT]
> If you installed a previous release, you must delete the cluster *before* upgrading **mssqlctl** and installing the new release. For more information, see [Upgrading to a new release](deployment-guidance.md#upgrade).

### Windows mssqlctl installation

1. On a Windows client, download the necessary Python package from [https://www.python.org/downloads/](https://www.python.org/downloads/). For python3.5.3 and later, pip3 is also installed when you install Python. 

   > [!TIP] 
   > When installing Python3, select to add Python to your path. If you do not, you can later find where pip3 is located and manually add it to your path.

1. Install **mssqlctl** with the following command:

   ```bash
   pip3 install --extra-index-url https://private-repo.microsoft.com/python/ctp-2.1 mssqlctl
   ```

### Linux mssqlctl installation

On Linux, you must install the **python3** and **python3-pip** packages and then upgrade pip. This installs the latest 3.5 version of Python and pip. The following example shows how these commands would work for Ubuntu (for other Linux platforms, see the [Python documentation](https://wiki.python.org/moin/BeginnersGuide/Download).

1. Install the necessary Python packages:

   ```bash
   sudo apt-get update && /
   sudo apt-get install -y python3 && /
   sudo apt-get install -y python3-pip && /
   sudo -H pip3 install --upgrade pip
   ```

1. Install **mssqlctl** with the following command:

   ```bash
   pip3 install --extra-index-url https://private-repo.microsoft.com/python/ctp-2.1 mssqlctl
   ```

## Next steps

For more information about big data clusters, see [What are SQL Server 2019 big data clusters?](big-data-cluster-overview.md).
