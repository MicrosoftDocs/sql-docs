---
# required metadata

title: Run the SQL Server Docker image on Linux, Mac, or Windows | Microsoft Docs
description: Download and run the Docker image for SQL Server vNext.
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 12/16/2016
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 82737f18-f5d6-4dce-a255-688889fdde69

# optional metadata

# keywords: "" 
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---
# Run the SQL Server Docker image on Linux, Mac, or Windows

This topic explains how to pull and run the [mssql-server Docker image](https://hub.docker.com/r/microsoft/mssql-server-linux/). This image consists of SQL Server running on Linux and can be used with the Docker Engine 1.8+ on Linux or on Docker for Mac/Windows. We are currently tracking all issues with the Docker image in our [mssql-docker GitHub repository](https://github.com/Microsoft/mssql-docker).

> [!NOTE]
> This image is running SQL Server on an Ubuntu Linux base image. To run the SQL Server on Windows Containers Docker image, check out the [mssql-server-windows Docker Hub page](https://hub.docker.com/r/microsoft/mssql-server-windows/).

## Requirements for Docker
- Docker Engine 1.8+ on any supported Linux distribution or Docker for Mac/Windows.
- Minimum of 4 GB of disk space
- Minimum of 4 GB of RAM

> [!IMPORTANT]
> The default on Docker for Mac and Docker for Windows is 2 GB for the Moby VM, so you will need to change it to 4 GB. The following sections explain how.

### Docker for Mac
1. Click on the Docker logo on the top status bar.
2. Select **Preferences**.
3. Move the memory indicator to 4GB or more.
4. Click the **restart** button at the button of the screen.

### Docker for Windows:
1. Right-click on the Docker icon from the task bar.
2. Click **Settings** under that menu.
3. Click on the **Advanced** Tab.
4. Move the memory indicator to 4GB or more.
5. Click the **Apply** button.

## Pull and run the Docker image
1. Pull the Docker image from Docker Hub.

    ```bash
    sudo docker pull microsoft/mssql-server-linux
    ```

    > [!TIP]
    > If you using Docker for Windows, remove the word **sudo** from the command-line in this step and step three.

2. To run the Docker image, you can use the following commands:

    ```
    docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -d microsoft/mssql-server-linux
    ```

3. To persist the data generated from your Docker container, you must map volume to the host machine. To do that, use the run command with the **-v \<host directory\>:/var/opt/mssql** flag. This will allow the data to be restored between container executions.

    ```
    sudo docker run -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=<YourStrong!Passw0rd>' -p 1433:1433 -v <host directory>:/var/opt/mssql -d microsoft/mssql-server-linux
    ```

    > [!NOTE]
    > The **ACCEPT_EULA** and **SA_PASSWORD** environment variables are required to run the image.

    > [!IMPORTANT]
    > Volume mapping for Docker-machine on Mac with the SQL Server on Linux image is not supported at this time.

## Upgrading the Docker image
Upgrading the Docker image will require just pulling the latest version from the registry. In order to do so, you just need to execute the `pull` command:
    
```bash
    sudo docker pull microsoft/mssql-server-linux:latest
```

You can now create new containers that will have the latest version of SQL Server in Linux on Docker.

## Next steps

After installing SQL Server on Linux, next see [how to connect to the server and run basic Transact-SQL queries](sql-server-linux-connect-and-query-sqlcmd.md). Also, check out the [mssql-docker GitHub repository](https://github.com/Microsoft/mssql-docker) for resources and feedback.
