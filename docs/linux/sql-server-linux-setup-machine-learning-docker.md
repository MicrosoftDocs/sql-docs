---
title: Install on Docker
titleSuffix: SQL Server Machine Learning Services
description: "Learn how to install SQL Server Machine Learning Services (Python and R) on Docker."
author: rothja
ms.author: jroth
ms.reviewer: randolphwest
ms.date: 06/14/2023
ms.service: sql
ms.subservice: machine-learning-services
ms.topic: how-to
ms.custom:
  - intro-installation
  - linux-related-content
monikerRange: "=sql-server-ver15||=sql-server-linux-ver15"
---
# Install SQL Server Machine Learning Services (Python and R) on Docker

[!INCLUDE [SQL Server 2019 - Linux](../includes/applies-to-version/sqlserver2019-linux.md)]

This article explains how to install [SQL Server Machine Learning Services](../machine-learning/sql-server-machine-learning-services.md) on Docker. You can use Machine Learning Services to execute Python and R scripts in-database. We do not provide pre-built containers with Machine Learning Services. You can create one from the SQL Server containers using [an example template available on GitHub](https://github.com/Microsoft/mssql-docker/tree/master/linux/preview/examples/mssql-mlservices).

## Prerequisites

- Git command-line interface.

- Docker Engine 1.8+ on any supported Linux distribution. For more information, see [Get Docker](https://docs.docker.com/get-docker/). SQL Server in containers are not supported on Windows or macOS for production use.

- See also the [system requirements for SQL Server on Linux](sql-server-linux-setup.md#system).

## Clone the mssql-docker repository

The following command clones the `mssql-docker` git repository to a local directory.

1. Open a Bash terminal on Linux or Mac.

1. Create a directory to hold a local copy of the mssql-docker repository.

1. Run the git clone command to clone the mssql-docker repository:

    ```bash
    git clone https://github.com/microsoft/mssql-docker mssql-docker
    ```

## Build a SQL Server Linux container image

Complete the following steps to build the docker image:

1. Change the directory to the mssql-mlservices directory:

    ```bash
    /mssql-docker/linux/preview/examples/mssql-mlservices
    ```

1. In the same directory, run the following command:

    ```bash
    docker build -t mssql-server-mlservices .
    ```

1. Run the command:

   > [!IMPORTANT]  
   > The `SA_PASSWORD` environment variable is deprecated. Use `MSSQL_SA_PASSWORD` instead.

   ```bash
   docker run -d -e MSSQL_PID=Developer -e ACCEPT_EULA=Y -e ACCEPT_EULA_ML=Y -e MSSQL_SA_PASSWORD=<password> -v <directory on the host OS>:/var/opt/mssql -p 1433:1433 mssql-server-mlservices
   ```

   > [!NOTE]  
   > Any of the following values can be used for MSSQL_PID: Developer (free), Express (free), Enterprise (paid), Standard (paid). If you are using a paid edition, please ensure that you have purchased a license. Replace (password) with your actual password. Volume mounting using -v is optional. Replace (directory on the host OS) with an actual directory where you want to mount the database data and log files.

1. Confirm by running the following command:

    ```bash
    docker ps -a
    ```

   > [!NOTE]  
   > To build the Docker image, you must install packages that are several GBs in size. The script might take some time to finish running, depending on network bandwidth.

## Run the SQL Server Linux container image

1. Set your environment variables before running the container. Set the PATH_TO_MSSQL environment variable to a host directory:

   ```bash
   export MSSQL_PID='Developer'
   export ACCEPT_EULA='Y'
   export ACCEPT_EULA_ML='Y'
   export PATH_TO_MSSQL='/home/mssql/'
   ```

   > [!NOTE]  
   > The process for running production SQL Server editions in containers is slightly different. For more information, see [Configure SQL Server container images on Docker](./sql-server-linux-docker-container-deployment.md). If you use the same container names and ports, the rest of this walkthrough still works with production containers.

1. To view your containers, run the `docker ps` command:

   ```bash
   sudo docker ps -a
   ```

1. If the **STATUS** column shows a status of **Up**, SQL Server is running  in the container and listening on the port specified in the **PORTS** column. If the **STATUS** column for your SQL Server container shows **Exited**, see the [Troubleshooting section of the configuration guide](./sql-server-linux-docker-container-troubleshooting.md).

    Output:

    ```output
    CONTAINER ID        IMAGE                          COMMAND                  CREATED             STATUS              PORTS                    NAMES
    941e1bdf8e1d        mcr.microsoft.com/mssql/server/mssql-server-linux   "/bin/sh -c /opt/m..."   About an hour ago   Up About an hour     0.0.0.0:1401->1433/tcp   sql1
    ```

## Enable Machine Learning Services

To enable Machine Learning Services, connect to your SQL Server instance and run the following T-SQL statement:

```sql
EXEC sp_configure  'external scripts enabled', 1;
RECONFIGURE WITH OVERRIDE
```

## Related content

Python developers can learn how to use Python with SQL Server by following these tutorials:

- [Python tutorial: Predict ski rental with linear regression in SQL Server Machine Learning Services](../machine-learning/tutorials/python-ski-rental-linear-regression-deploy-model.md)
- [Python tutorial: Categorizing customers using k-means clustering with SQL Server Machine Learning Services](../machine-learning/tutorials/python-clustering-model.md)

R developers can get started with some simple examples, and learn the basics of how R works with SQL Server. For your next step, see the following links:

- [Quickstart: Run R in T-SQL](../machine-learning/tutorials/quickstart-r-create-script.md)
- [Tutorial: In-database analytics for R developers](../machine-learning/tutorials/r-taxi-classification-introduction.md)
