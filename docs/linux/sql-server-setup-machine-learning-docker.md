---
title: Install on Docker
titleSuffix: SQL Server Machine Learning Services
description: 'Learn how to install SQL Server Machine Learning Services (Python and R) on Docker.'
author: cawrites
ms.author: chadam
ms.reviewer: davidph
manager: cgronlun
ms.date: 02/18/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: machine-learning
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Install SQL Server Machine Learning Services (Python and R) on Docker

This article explains how to install SQL Server Machine Learning Services on Docker. You can use Machine Learning Services to execute Python and R scripts in-database. We do not provide pre-built containers with Machine Learning Services, but you can create one from the SQL Server containers using [an example template available on GitHub](https://github.com/Microsoft/mssql-docker/tree/master/linux/preview/examples/mssql-mlservices)

## Prerequisites

- Git command-line interface.
- Docker Engine 1.8+ on any supported Linux distribution, or Docker for Mac/Windows. For more information, see [Install Docker](https://docs.docker.com/engine/installation/).
- Minimum of 2 gigabytes (GB) of disk space.
- Minimum of 2 GB of RAM.
- [System requirements for SQL Server on Linux](sql-server-linux-setup.md#system).

## Clone the mssql-docker repository

1. Open a Bash terminal on Linux or Mac, or open a WSL terminal on Windows.

1. Create a local directory to hold a local copy of the mssql-docker repository.

1. Run the git clone command to clone the mssql-docker repository:

    ```bash
    git clone https://github.com/microsoft/mssql-docker mssql-docker
    ```

## Build a SQL Server Linux container image with Machine Learning Services

1. Change the directory to the mssql-mlservices directory:

    ```bash
    cd mssql-docker/linux/preview/examples/mssql-mlservices
    ```

1. Run the build.sh script:

   ```bash
   ./build.sh
   ```

   > [!NOTE]
   > To build the Docker image, you must install packages that are several GBs in size. The script may take up to 20 minutes to finish running, depending on network bandwidth.

## Run the SQL Server Linux container image with Machine Learning Services

1. Set your environment variables before running the container. Set the PATH_TO_MSSQL environment variable to a host directory:

   ```bash
    export MSSQL_PID='Developer'
    export ACCEPT_EULA='Y'
    export ACCEPT_EULA_ML='Y'
    export PATH_TO_MSSQL='/home/mssql/'
   ```

1. Run the run.sh script:

   ```bash
   ./run.sh
   ```

   This command creates a SQL Server container with Machine Learning Services, using the Developer edition (default). SQL Server port **1433** is exposed on the host as port **1401**.

   > [!NOTE]
   > The process for running production SQL Server editions in containers is slightly different. For more information, see [Configure SQL Server container images on Docker](sql-server-linux-configure-docker.md). If you use the same container names and ports, the rest of this walkthrough still works with production containers.

1. To view your Docker containers, run the `docker ps` command:

   ```bash
   sudo docker ps -a
   ```

1. If the **STATUS** column shows a status of **Up**, SQL Server is running in the container and listening on the port specified in the **PORTS** column. If the **STATUS** column for your SQL Server container shows **Exited**, see the [Troubleshooting section of the configuration guide](sql-server-linux-configure-docker.md#troubleshooting).

   ```bash
   $ sudo docker ps -a
   ```
    Output:

    ```
    CONTAINER ID        IMAGE                          COMMAND                  CREATED             STATUS              PORTS                    NAMES
    941e1bdf8e1d        mcr.microsoft.com/mssql/server/mssql-server-linux   "/bin/sh -c /opt/m..."   About an hour ago   Up About an hour     0.0.0.0:1401->1433/tcp   sql1
    ```

## Run in a container

Follow the steps below to build and run SQL Server Machine Learning Services in a Docker container. For more information, see [Configure SQL Server container images on Docker](sql-server-linux-configure-docker.md).

## Next steps

Python developers can learn how to use Python with SQL Server by following these tutorials:

+ [Tutorial: Run Python in T-SQL](../advanced-analytics/tutorials/run-python-using-t-sql.md)
+ [Tutorial: In-database analytics for Python developers](../advanced-analytics/tutorials/sqldev-in-database-python-for-sql-developers.md)

To view examples of machine learning that are based on real-world scenarios, see [Machine learning tutorials](../advanced-analytics/tutorials/machine-learning-services-tutorials.md).

R developers can get started with some simple examples, and learn the basics of how R works with SQL Server. For your next step, see the following links:

+ [Tutorial: Run R in T-SQL](../advanced-analytics/tutorials/quickstart-r-create-script.md)
+ [Tutorial: In-database analytics for R developers](../advanced-analytics/tutorials/sqldev-in-database-r-for-sql-developers.md)
