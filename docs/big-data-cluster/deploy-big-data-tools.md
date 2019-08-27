---
title: Install big data tools
titleSuffix: SQL Server big data clusters
description: Learn how to install tools used with [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] (preview).
author: MikeRayMSFT 
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 08/21/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Install SQL Server 2019 big data tools

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article describes the client tools that should be installed for creating, managing, and using [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)] (preview). The following section provides a list of tools and links to installation instructions. Before deploying a big data cluster, configure the tools marked required on Windows or Linux.

[!INCLUDE [Limited public preview note](../includes/big-data-cluster-preview-note.md)]

## Big data cluster tools

The following table lists common big data cluster tools and how to install them:

| Tool | Required | Description | Installation |
|---|---|---|---|
| **python** | Yes | Python is an interpreted, object-oriented, high-level programming language with dynamic semantics. Many parts of big data clusters for SQL Server use python. | [Install python](#python)|
| **azdata** | Yes | Command-line tool for installing and managing a big data cluster. | [Install](deploy-install-azdata.md) |
| **kubectl**<sup>1</sup> | Yes | Command-line tool for monitoring the underlying Kuberentes cluster ([More info](https://kubernetes.io/docs/tasks/tools/install-kubectl/)). | [Windows](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-with-powershell-from-psgallery) \| [Linux](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-kubectl-binary-using-native-package-management) |
| **Azure Data Studio - SQL Server 2019 release candidate (RC)** | Yes | Cross-platform graphical tool for querying SQL Server ([More info](#rc)). | [Install](#rc-windows) |
| **SQL Server 2019 extension** | Yes | Extension for Azure Data Studio that supports connecting to the big data cluster. Also provides a Data Virtualization wizard. | [Install](../azure-data-studio/sql-server-2019-extension.md) |
| **Azure CLI**<sup>2</sup> | For AKS | Modern command-line interface for managing Azure services. Used with AKS big data cluster deployments ([More info](https://docs.microsoft.com/cli/azure/?view=azure-cli-latest)). | [Install](https://docs.microsoft.com/cli/azure/install-azure-cli?view=azure-cli-latest) |
| **mssql-cli** | Optional | Modern command-line interface for querying SQL Server ([More info](https://github.com/dbcli/mssql-cli/blob/master/README.rst)). | [Windows](https://github.com/dbcli/mssql-cli/blob/master/doc/installation/windows.md) \| [Linux](https://github.com/dbcli/mssql-cli/blob/master/doc/installation/linux.md) |
| **sqlcmd** | For some scripts | Legacy command-line tool for querying SQL Server ([More info](https://docs.microsoft.com/sql/tools/sqlcmd-utility?view=sql-server-ver15)). | [Windows](https://www.microsoft.com/download/details.aspx?id=36433) \| [Linux](../linux/sql-server-linux-setup-tools.md) |
| **curl** <sup>3</sup> | For some scripts | Command-line tool for transferring data with URLs. | [Windows](https://curl.haxx.se/windows/) \| Linux: install curl package |

<sup>1</sup> You must use kubectl version 1.10 or later. Also, the version of kubectl should be plus or minus one minor version of your Kubernetes cluster. If you want to install a specific version on kubectl client, see [Install kubectl binary via curl](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-kubectl-binary-using-curl) (on Windows 10, use cmd.exe and not Windows PowerShell to run curl). 

> [!TIP]
> To use kubectl with a previously deployed cluster on Azure Kubernetes Service (AKS), you must set the cluster context with the following Azure CLI command:
>
>    ```azurecli
>    az aks get-credentials --name <aks_cluster_name> --resource-group <azure_resource_group_name>
>    ```

<sup>2</sup> You must be using Azure CLI version 2.0.4 or later. Run `az --version` to find the version if needed.

<sup>3</sup> If you are running on Windows 10, **curl** is already in your PATH when running from a cmd prompt. For other versions of Windows, download **curl** using the link and place it in your PATH.

## Which tools are required?

The previous table provides all of the common tools that are used with big data clusters. Which tools are required depends on your scenario. But in general, the following tools are most important for managing, connecting to, and querying the cluster:

- **azdata**
- **kubectl**
- **Azure Data Studio**
- **SQL Server 2019 extension**

The remaining tools are only required in certain scenarios. **Azure CLI** can be used to manage Azure services associated with AKS deployments. **mssql-cli** is an optional but useful tool that allows you to connect to the SQL Server master instance in the cluster and run queries from the command line. And **sqlcmd** and **curl** are required if you plan to install sample data with the GitHub script.

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

## <a id=rc></a> Download and install Azure Data Studio SQL Server 2019 release candidate (RC)

Azure Data Studio SQL Server 2019 RC provides a capabilities and features specifically for SQL Server 2019 RC.

For normal, production releases of Azure Data Studio, follow the instructions at [Download and install Azure Data Studio](../azure-data-studio/download.md).

|Platform|Download|Release date| Version |
|:---|:---|:---|:---|
|Windows|[User Installer (recommended)](https://go.microsoft.com/fwlink/?linkid=2100710)<br>[System Installer](https://go.microsoft.com/fwlink/?linkid=2100711)<br>[.zip](https://go.microsoft.com/fwlink/?linkid=2100712)|August 15, 2019 |1.10.0|
|macOS|[.zip](https://go.microsoft.com/fwlink/?linkid=2100809)|August 15, 2019 |1.10.0|
|Linux|[.deb](https://go.microsoft.com/fwlink/?linkid=2100672)<br>[.rpm](https://go.microsoft.com/fwlink/?linkid=2100810)<br>[.tar.gz](https://go.microsoft.com/fwlink/?linkid=2100714)|August 15, 2019 |1.10.0|

For details about the latest release, see the [release notes](../big-data-cluster/release-notes-big-data-cluster.md).

### <a id="rc-windows"></a> Get Azure Data Studio for Windows

This release of [!INCLUDE[name-sos](../includes/name-sos-short.md)] includes a standard Windows installer experience, and a .zip file.

The *user installer* is recommended because it does not require administrator privileges, which simplifies both installs and upgrades. The user installer does not require Administrator privileges as the location is under your user Local AppData (LOCALAPPDATA) folder. The user installer also provides a smoother background update experience. For more information, see [User setup for Windows](https://code.visualstudio.com/updates/v1_26#_user-setup-for-windows).

**User Installer** (recommended)

1. Download and run the [[!INCLUDE[name-sos](../includes/name-sos-short.md)] *user* installer for Windows](https://go.microsoft.com/fwlink/?linkid=2100710).
2. Start the [!INCLUDE[name-sos-short](../includes/name-sos-short.md)] app.

**System Installer**

1. Download and run the [[!INCLUDE[name-sos](../includes/name-sos-short.md)] *system* installer for Windows](https://go.microsoft.com/fwlink/?linkid=2100711).
2. Start the [!INCLUDE[name-sos-short](../includes/name-sos-short.md)] app.

**.zip file**

1. Download [[!INCLUDE[name-sos](../includes/name-sos-short.md)] .zip for Windows](https://go.microsoft.com/fwlink/?linkid=2100712).
2. Browse to the downloaded file and extract it.
3. Run `\azuredatastudio-windows\azuredatastudio.exe`


### Get Azure Data Studio for macOS

1. Download [[!INCLUDE[name-sos](../includes/name-sos-short.md)] for macOS](https://go.microsoft.com/fwlink/?linkid=2100809).
2. To expand the contents of the zip, double-click it.
3. To make [!INCLUDE[name-sos](../includes/name-sos-short.md)] available in the *Launchpad*, drag *Azure Data Studio.app* to the *Applications* folder.


### Get Azure Data Studio for Linux

1. Download [!INCLUDE[name-sos](../includes/name-sos-short.md)] for Linux by using one of the installers or the tar.gz archive:
    - [.deb](https://go.microsoft.com/fwlink/?linkid=2100672)
    - [.rpm](https://go.microsoft.com/fwlink/?linkid=2100810)
    - [.tar.gz](https://go.microsoft.com/fwlink/?linkid=2100714)
1. To extract the file and launch [!INCLUDE[name-sos](../includes/name-sos-short.md)], open a new Terminal window and type the following commands:

   **Debian Installation:**
   ```bash
   cd ~
   sudo dpkg -i ./Downloads/azuredatastudio-linux-<version string>.deb

   azuredatastudio
   ```

   **rpm Installation:**
   ```bash
   cd ~
   yum install ./Downloads/azuredatastudio-linux-<version string>.rpm

   azuredatastudio
   ```

   **tar.gz Installation:**
   ```bash 
   cd ~ 
   cp ~/Downloads/azuredatastudio-linux-<version string>.tar.gz ~ 
   tar -xvf ~/azuredatastudio-linux-<version string>.tar.gz 
   echo 'export PATH="$PATH:~/azuredatastudio-linux-x64"' >> ~/.bashrc
   source ~/.bashrc 
   azuredatastudio 
   ``` 

   > [!NOTE]
   > On Debian, Redhat, and Ubuntu, you may have missing dependencies. Use the following commands to install these dependencies depending on your version of Linux:
   

   **Debian:** 
   ```bash
   sudo apt-get install libunwind8
   ```

   **Redhat:** 
   ```bash
   yum install libXScrnSaver
   ```

   **Ubuntu:** 
   ```bash
   sudo apt-get install libxss1

   sudo apt-get install libgconf-2-4

   sudo apt-get install libunwind8
   ```

### Supported Operating Systems

[!INCLUDE[name-sos](../includes/name-sos-short.md)] runs on Windows, macOS, and Linux, and is supported on the following platforms:

#### Windows

- Windows 10 (64-bit)
- Windows 8.1 (64-bit)
- Windows 8 (64-bit)
- Windows 7 (SP1) (64-bit) - Requires [KB2533623](https://www.microsoft.com/download/details.aspx?id=26767)
- Windows Server 2019
- Windows Server 2016
- Windows Server 2012 R2 (64-bit)
- Windows Server 2012 (64-bit)
- Windows Server 2008 R2 (64-bit)

#### macOS

- macOS 10.13 High Sierra
- macOS 10.12 Sierra

#### Linux

- Red Hat Enterprise Linux 7.4
- Red Hat Enterprise Linux 7.3
- SUSE Linux Enterprise Server v12 SP2
- Ubuntu 16.04

## Next steps

After configuring the tools, deploy a SQL Server 2019 big data cluster to Kubernetes in the Cloud or on-premises. For more information, see the following deployment articles:

- [Quickstart: Deploy SQL Server big data cluster on Azure Kubernetes Service (AKS)](quickstart-big-data-cluster-deploy.md)
- [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md)

For More info about big data clusters, see [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md).
