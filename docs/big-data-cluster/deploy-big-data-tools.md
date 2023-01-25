---
title: Install big data tools
titleSuffix: SQL Server Big Data Clusters
description: Learn how to install tools used with SQL Server 2019 big data cluster.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 12/13/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.custom: intro-installation
---

# Install SQL Server 2019 big data tools

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

This article describes the client tools that should be installed for creating, managing, and using [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]. The following section provides a list of tools and links to installation instructions. Before deploying a big data cluster, configure the tools marked required on Windows or Linux.

## Big data cluster tools

The following table lists common big data cluster tools and how to install them:

| Tool | Required | Description | Installation |
|---|---|---|---|
| `python` | Yes | Python is an interpreted, object-oriented, high-level programming language with dynamic semantics. Many parts of big data clusters for SQL Server use python. | [Install python](#python)|
| [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] | Yes | Command-line tool for installing and managing a big data cluster. | [Install](../azdata/install/deploy-install-azdata.md) |
| `kubectl`<sup>1</sup> | Yes | Command-line tool for monitoring the underlying Kubernetes cluster ([More info](https://kubernetes.io/docs/tasks/tools/install-kubectl/)). | [Windows](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-with-powershell-from-psgallery) \| [Linux](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-using-native-package-management) |
| **Azure Data Studio** | Yes | Cross-platform graphical tool for querying SQL Server. | [Install](../azure-data-studio/download-azure-data-studio.md) |
| **Data Virtualization extension** | Yes | Extension for Azure Data Studio that provides a Data Virtualization wizard. | [Install](../azure-data-studio/extensions/data-virtualization-extension.md) |
| **Azure CLI**<sup>2</sup> | For AKS | Modern command-line interface for managing Azure services. Used with AKS big data cluster deployments ([More info](/cli/azure/)). | [Install](/cli/azure/install-azure-cli) |
| **mssql-cli** | Optional | Modern command-line interface for querying SQL Server ([More info](../tools/mssql-cli.md)). | [Windows](https://github.com/dbcli/mssql-cli/blob/master/doc/installation/windows.md) \| [Linux](https://github.com/dbcli/mssql-cli/blob/master/doc/installation/linux.md) |
| **sqlcmd** | For some scripts | Legacy command-line tool for querying SQL Server ([More info](../tools/sqlcmd-utility.md)). You might need to install the Microsoft ODBC Driver 11 for SQL Server before installing the SQLCMD package. | [Windows](https://www.microsoft.com/download/details.aspx?id=36433) \| [Linux](../linux/sql-server-linux-setup-tools.md) |
| `curl` <sup>3</sup> | For some scripts | Command-line tool for transferring data with URLs. | [Windows](https://curl.haxx.se/windows/) \| Linux: install curl package |
| `oc` | Required for Red Hat OpenShift and Azure Redhat OpenShift deployments. |`oc` is the Open Shift command line interface (CLI). | [Installing the CLI](https://docs.openshift.com/container-platform/4.4/cli_reference/openshift_cli/getting-started-cli.html#installing-the-cli)

<sup>1</sup> You must use `kubectl` version 1.13 or later. Also, the version of `kubectl` should be plus or minus one minor version of your Kubernetes cluster. If you want to install a specific version on `kubectl` client, see [Install `kubectl` binary via curl](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-kubectl-binary-using-curl) (on Windows 10 and Windows 11, use `cmd.exe` and not Windows PowerShell to run curl).

> [!TIP]
> To use `kubectl` with a previously deployed cluster on Azure Kubernetes Service (AKS), you must set the cluster context with the following Azure CLI command:
>
>    ```azurecli
>    az aks get-credentials --name <aks_cluster_name> --resource-group <azure_resource_group_name>
>    ```

<sup>2</sup> You must be using Azure CLI version 2.0.4 or later. Run `az --version` to find the version if needed.

<sup>3</sup> If you are running on Windows 10 or Windows 11, `curl` is already in your PATH when running from a cmd prompt. For other versions of Windows, download `curl` using the link and place it in your PATH.

## Which tools are required?

The previous table provides all of the common tools that are used with big data clusters. Which tools are required depends on your scenario. But in general, the following tools are most important for managing, connecting to, and querying the cluster:

- [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)]
- `kubectl`
- **Azure Data Studio**
- **Data Virtualization extension**

The remaining tools are only required in certain scenarios. **Azure CLI** can be used to manage Azure services associated with AKS deployments. **mssql-cli** is an optional but useful tool that allows you to connect to the SQL Server master instance in the cluster and run queries from the command line. And **sqlcmd** and `curl` are required if you plan to install sample data with the GitHub script.

### <a id="python"></a> Install python offline

1. On a machine with internet access, download one of the following compressed files containing Python:

   | Operating system | Download |
   |---|---|
   | Windows | [https://go.microsoft.com/fwlink/?linkid=2074021](https://go.microsoft.com/fwlink/?linkid=2074021) |
   | Linux   | [https://go.microsoft.com/fwlink/?linkid=2065975](https://go.microsoft.com/fwlink/?linkid=2065975) |
   | OSX     | [https://go.microsoft.com/fwlink/?linkid=2065976](https://go.microsoft.com/fwlink/?linkid=2065976) |

1. Copy the compressed file to the target machine and extract it to a folder of your choice.

1. For Windows only, run `installLocalPythonPackages.bat` from that folder and pass the full path to the same folder as a parameter.

   ```PowerShell
   installLocalPythonPackages.bat "C:\python-3.6.6-win-x64-0.0.1-offline\0.0.1"
   ```

## Download and install Azure Data Studio

Azure Data Studio provides capabilities and features specifically for SQL Server Big Data Clusters.

[Get the latest Azure Data Studio](../azure-data-studio/download-azure-data-studio.md).

For details about the latest release, see the [release notes](./release-notes-big-data-cluster.md).

## Next steps

After configuring the tools, deploy a SQL Server 2019 big data cluster to Kubernetes in the Cloud or on-premises. For more information, see the following deployment articles:

- [Quickstart: Deploy SQL Server big data cluster on Azure Kubernetes Service (AKS)](quickstart-big-data-cluster-deploy.md)
- [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md)

For More info about big data clusters, see [Introducing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](big-data-cluster-overview.md).
