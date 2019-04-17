---
title: Deploy offline
titleSuffix: SQL Server big data clusters
description: Learn how to perform an offline deployment of a SQL Server big data cluster.
author: rothja
ms.author: jroth
manager: craigg
ms.date: 04/17/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Perform an offline deployment of a SQL Server big data cluster

This article describes how to perform an offline deployment of a SQL Server 2019 big data cluster (preview). Big data clusters must have access to a Docker repository from which to pull container images. An offline installation is one where the required images are placed into a private Docker repository. That private repository is then used as the target for a new deployment.

## Prerequisites

- Docker Engine 1.8+ on any supported Linux distribution or Docker for Mac/Windows. For more information, see [Install Docker](https://docs.docker.com/engine/installation/).

[!INCLUDE [Limited public preview note](../includes/big-data-cluster-preview-note.md)]

## Load images into a private repository

The following steps describe how to pull the big data cluster container images from the Microsoft repository and then push them into your private repository.

> [!TIP]
> The following steps explain the process. However, to simplify task, you can use the [automated script](#automated) instead of manually running these commands.

1. First log in to the Microsoft Docker registry with the **docker login** command. Use the username and password that Microsoft provided to you as part of the gated public preview.

   ```PowerShell
   docker login private-repo.microsoft.com -u  <SOURCE_DOCKER_USERNAME> -p <SOURCE_DOCKER_PASSWORD>
   ```

   > [!TIP]
   > These commands use PowerShell as an example, but you can run them from cmd, bash, or any command shell that can run docker. On Linux, add `sudo` to each command.

1. Pull the big data cluster container images by repeating the following command. Replace `<SOURCE_IMAGE_NAME>` with each [image name](#images). Replace `<SOURCE_DOCKER_TAG>` with the tag for the big data cluster release, such as **ctp2.5**.  

   ```PowerShell
   docker pull private-repo.microsoft.com/mssql-private-preview/<SOURCE_IMAGE_NAME>:<SOURCE_DOCKER_TAG>
   ```

1. Log in to the target private Docker registry.

   ```PowerShell
   docker login <TARGET_DOCKER_REGISTRY> -u <TARGET_DOCKER_USERNAME> -p <TARGET_DOCKER_PASSWORD>
   ```

1. Tag the local images with the following command for each image:

   ```PowerShell
   docker tag private-repo.microsoft.com/mssql-private-preview/<SOURCE_IMAGE_NAME>:<SOURCE_DOCKER_TAG> <TARGET_DOCKER_REGISTRY>/<TARGET_DOCKER_REPOSITORY>/<SOURCE_IMAGE_NAME>:<TARGET_DOCKER_TAG>
   ```

1. Push the local images to the private Docker repository:

   ```PowerShell
   docker push <TARGET_DOCKER_REGISTRY>/<TARGET_DOCKER_REPOSITORY>/<SOURCE_IMAGE_NAME>:<TARGET_DOCKER_TAG>
   ```

### <a id="images"></a> Big data cluster container images

The following big data cluster container images are required for an offline installation:

 - **mssql-appdeploy-init**
 - **mssql-monitor-fluentbit**
 - **mssql-monitor-collectd**
 - **mssql-server-data**
 - **mssql-hadoop**
 - **mssql-java**
 - **mssql-mlservices-pythonserver**
 - **mssql-mlservices-rserver**
 - **mssql-monitor-elasticsearch**
 - **mssql-monitor-influxdb**
 - **mssql-security-knox**
 - **mssql-mlserver-r-runtime**
 - **mssql-mlserver-py-runtime**
 - **mssql-controller**
 - **mssql-portal**
 - **mssql-server-controller**
 - **mssql-monitor-grafana**
 - **mssql-monitor-kibana**
 - **mssql-service-proxy**
 - **mssql-app-service-proxy**
 - **mssql-ssis-app-runtime**
 - **mssql-monitor-telegraf**

## <a id="automated"></a> Automated script

You can use an automated python script that will automatically pull all required container images and push them into a private repository.

> [!NOTE]
> Python is a prerequisite for using the script. For more information about how to install Python, see the [Python documentation](https://wiki.python.org/moin/BeginnersGuide/Download).

1. From bash or PowerShell, download the script with **curl**:

   ```PowerShell
   curl -o push-bdc-images-to-custom-private-repo.py "https://raw.githubusercontent.com/Microsoft/sql-server-samples/master/samples/features/sql-big-data-cluster/deployment/offline/push-bdc-images-to-custom-private-repo.py"
   ```

1. Then run the script with one of the following commands:

   **Windows:**

   ```PowerShell
   python deploy-sql-big-data-aks.py
   ```

   **Linux:**

   ```bash
   sudo python deploy-sql-big-data-aks.py
   ```

1. Follow the prompts for entering the Microsoft repository and your private repository information. After the script completes, all required images should be located in your private repository.

## Install tools offline

Big data cluster deployments require several tools, including **Python**, **mssqlctl**, and **kubectl**. Use the following steps to install these tools on an offline server.

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

### <a id="mssqlctl"></a> Install mssqlctl offline

1. On a machine with internet access and [Python](https://wiki.python.org/moin/BeginnersGuide/Download), run the following command to download all off the **mssqlctl** packages to the current folder.

   ```PowerShell
   pip download -r https://private-repo.microsoft.com/python/ctp-2.3/mssqlctl/requirements.txt
   ```

1. Download the **requirements.txt** file.

   ```PowerShell
   curl -o requirements.txt "https://private-repo.microsoft.com/python/ctp-2.3/mssqlctl/requirements.txt"
   ```

1. Copy the downloaded packages and the **requirements.txt** file to the target machine.

1. Run the following command on the target machine, specifying the folder that you copied the previous files into.

   ```PowerShell
   pip install --no-index --find-links <path-to-packages> -r <path-to-requirements.txt>
   ```

### <a id="kubectl"></a> Install kubectl offline

To install **kubectl** to an offline machine, use the following steps.

1. Use **curl** to download **kubectl** to a folder of your choice. For more information, see [Install kubectl binary using curl](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-kubectl-binary-using-curl).

1. Copy the folder to the target machine.

## Deploy with from repository

To deploy from the private repository, use the steps described in the [deployment guide](deployment-guidance.md), but customize the following environment variables to match your private Docker repository.

- **DOCKER_REGISTRY**  
- **DOCKER_REPOSITORY**
- **DOCKER_USERNAME**
- **DOCKER_PASSWORD**  
- **DOCKER_EMAIL**
- **DOCKER_IMAGE_TAG**

## Next steps

For more information about big data cluster deployments, see [How to deploy SQL Server big data clusters on Kubernetes](deployment-guidance.md).