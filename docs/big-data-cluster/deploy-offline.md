---
title: Deploy offline
titleSuffix: SQL Server Big Data Clusters
description: Learn how to perform an offline deployment of a SQL Server 2019 Big Data Cluster and how to load container images into a private repository.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 09/21/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
ms.custom: intro-deployment
---

# Perform an offline deployment of a SQL Server big data cluster

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

This article describes how to perform an offline deployment of a [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]. Big data clusters must have access to a Docker repository from which to pull container images. An offline installation is one where the required images are placed into a private Docker repository. That private repository is then used as the image source for a new deployment.

## Prerequisites

- Docker Engine on any supported Linux distribution or Docker for Mac/Windows. Validate the engine version against the tested configurations on the [SQL Server Big Data Clusters release notes](release-notes-big-data-cluster.md).For more information, see [Install Docker](https://docs.docker.com/engine/installation/). 

> [!WARNING]
> The parameter ```imagePullPolicy``` is required to be set as ```"Always"``` in the deployment profile control.json file.

## Load images into a private repository

The following steps describe how to pull the big data cluster container images from the Microsoft repository and then push them into your private repository.

> [!TIP]
> The following steps explain the process. However, to simplify the task, you can use the [automated script](#automated) instead of manually running these commands.

1. Pull the big data cluster container images by repeating the following command. Replace `<SOURCE_IMAGE_NAME>` with each [image name](#images). Replace `<SOURCE_DOCKER_TAG>` with the tag for the big data cluster release, such as **2019-CU12-ubuntu-20.04**.  

   ```PowerShell
   docker pull mcr.microsoft.com/mssql/bdc/<SOURCE_IMAGE_NAME>:<SOURCE_DOCKER_TAG>
   ```

1. Log in to the target private Docker registry.

   ```PowerShell
   docker login <TARGET_DOCKER_REGISTRY> -u <TARGET_DOCKER_USERNAME> -p <TARGET_DOCKER_PASSWORD>
   ```

1. Tag the local images with the following command for each image:

   ```PowerShell
   docker tag mcr.microsoft.com/mssql/bdc/<SOURCE_IMAGE_NAME>:<SOURCE_DOCKER_TAG> <TARGET_DOCKER_REGISTRY>/<TARGET_DOCKER_REPOSITORY>/<SOURCE_IMAGE_NAME>:<TARGET_DOCKER_TAG>
   ```

1. Push the local images to the private Docker repository:

   ```PowerShell
   docker push <TARGET_DOCKER_REGISTRY>/<TARGET_DOCKER_REPOSITORY>/<SOURCE_IMAGE_NAME>:<TARGET_DOCKER_TAG>
   ```

> [!WARNING]
> Do not modify the big data cluster images once they are pushed into your private repository. Performing a deployment with modified images will result in an unsupported big data cluster setup.

### <a id="images"></a> Big data cluster container images

The following big data cluster container images are required for an offline installation:
- **mssql-app-service-proxy**
- **mssql-control-watchdog**
- **mssql-controller**
- **mssql-dns**
- **mssql-hadoop**
- **mssql-mleap-serving-runtime**
- **mssql-mlserver-py-runtime**
- **mssql-mlserver-r-runtime**
- **mssql-monitor-collectd**
- **mssql-monitor-elasticsearch**
- **mssql-monitor-fluentbit**
- **mssql-monitor-grafana**
- **mssql-monitor-influxdb**
- **mssql-monitor-kibana**
- **mssql-monitor-telegraf**
- **mssql-security-knox**
- **mssql-security-support**
- **mssql-server-controller**
- **mssql-server-data**
- **mssql-ha-operator**
- **mssql-ha-supervisor**
- **mssql-service-proxy**
- **mssql-ssis-app-runtime**


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
   python push-bdc-images-to-custom-private-repo.py
   ```

   **Linux:**

   ```bash
   sudo python push-bdc-images-to-custom-private-repo.py
   ```

1. Follow the prompts for entering the Microsoft repository and your private repository information. After the script completes, all required images should be located in your private repository.

1. Follow the instructions [here](deployment-custom-configuration.md#docker) to learn how to customize the `control.json` deployment configuration file to make use of your container registry and repository. Note that you must set `DOCKER_USERNAME` and `DOCKER_PASSWORD` environment variables before deployment to enable access to your private repository.

## Install tools offline

Big data cluster deployments require several tools, including **Python**, [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)], and **kubectl**. Use the following steps to install these tools on an offline server.

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

### <a id="azdata"></a> Install azdata offline

1. On a machine with internet access and [Python](https://wiki.python.org/moin/BeginnersGuide/Download), run the following command to download all off the [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] packages to the current folder.

   ```PowerShell
   pip download -r https://aka.ms/azdata
   ```

1. Copy the downloaded packages and the `requirements.txt` file to the target machine.

1. Run the following command on the target machine, specifying the folder that you copied the previous files into.

   ```PowerShell
   pip install --no-index --find-links <path-to-packages> -r <path-to-requirements.txt>
   ```

### <a id="kubectl"></a> Install kubectl offline

To install **kubectl** to an offline machine, use the following steps.

1. Use **curl** to download **kubectl** to a folder of your choice. For more information, see [Install kubectl binary using curl](https://kubernetes.io/docs/tasks/tools/install-kubectl/#install-kubectl-binary-using-curl).

1. Copy the folder to the target machine.

## Deploy from private repository

To deploy from the private repository, use the steps described in the [deployment guide](deployment-guidance.md), but use a custom deployment configuration file that specifies your private Docker repository information. The following [!INCLUDE [azure-data-cli-azdata](../includes/azure-data-cli-azdata.md)] commands demonstrate how to change the Docker settings in a custom deployment configuration file named `control.json`:

```bash
azdata bdc config replace --config-file custom/control.json --json-values "$.spec.docker.repository=<your-docker-repository>"
azdata bdc config replace --config-file custom/control.json --json-values "$.spec.docker.registry=<your-docker-registry>"
azdata bdc config replace --config-file custom/control.json --json-values "$.spec.docker.imageTag=<your-docker-image-tag>"
```

The deployment prompts you for the docker username and password, or you can specify them in the `DOCKER_USERNAME` and `DOCKER_PASSWORD` environment variables.

## Next steps

For more information about big data cluster deployments, see [How to deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] on Kubernetes](deployment-guidance.md).
