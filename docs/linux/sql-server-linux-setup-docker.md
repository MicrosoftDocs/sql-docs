---
# required metadata

title: Install SQL Server on Linux (Docker) | SQL Server vNext CTP1
description: 
author: rothja 
ms.author: jroth 
manager: jhubbard
ms.date: 10-27-2016
ms.topic: article
ms.prod: sql-non-specified
ms.service: 
ms.technology: 
ms.assetid: 

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
# Install SQL Server on Linux (Docker)

This topic explains how to pull and run the mssql-server Docker image. This image can be used with the Docker Engine 1.8+ on Linux or on Docker for Mac/Windows.

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

### For Windows users:
1. Right-click on the Docker icon from the task bar.
2. Click **Settings** under that menu.
3. Click on the **Advanced** Tab.
4. Move the memory indicator to 4GB or more.
5. Click the **Apply** button.

## Pull and run the Docker image
1. Login to the preview Docker registry by using the credentials provided to you in the welcome email.

        $ sudo docker login private-repo.microsoft.com

2. You can now pull the Docker image from the Docker registry.

        $ sudo docker pull private-repo.microsoft.com/mssql-private-preview/mssql-server:latest

3. To run the Docker image, you can use the following commands:

        $ sudo docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=\<your_password_here\>" -p 1433:1433 -d private-repo.microsoft.com/mssql-private-preview/mssql-server:latest

4. To persist the data generated from your Docker container, you must map volume to the host machine. To do that, use the run command with the **-v \<host_folder\>:/var/opt/mssql** flag. This will allow the data to be restored between container executions.

        $ sudo docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=\<your_password_here\>" -p 1433:1433 -v \<host folder\>:/var/opt/mssql -d private-repo.microsoft.com/mssql-private-preview/mssql- server:latest

    > [!NOTE]
    > The **ACCEPT_EULA** and **SA_PASSWORD** environment variables are required to run the image.

## <a id="tools"></a> SQL Server command-line tools
The Docker image for SQL Server vNext comes with the SQL Server command-line tools and ODBC drivers pre-installed. For more details on these tools, see [Command-line tools and ODBC drivers](sql-server-linux-setup.md#tools).

## Next steps

After installing SQL Server on Linux, next see [how to connect to the server and run basic Transact-SQL queries](sql-server-linux-connect-and-query.md).