---
title: Set up a local development environment for Azure SQL Database
description: How to set up a local development environment for Azure SQL Database. 
services: sql-database
ms.service: sql-database
author: scoriani
ms.author: scoriani
ms.reviewer: mathoma
ms.topic: how-to 
ms.date: 04/29/2022
---

# Set up a local development environment for Azure SQL Database

This article teaches you to set up the local development experience for Azure SQL Database. The local development experience for Azure SQL Database enables developers and database professionals to design, edit, build/validate, publish, and run database schemas for databases in Azure SQL Database using a containerized environment.

## Prerequisites

Before you configure the local development environment for Azure SQL Database, make sure you have met the following hardware and software requirements:

- Software requirements:
    - <!-- OS requirements -->
- Minimum hardware requirements:
    - <!-- RAM and disk space minimums  -->

## Install Docker Desktop

The local development environment for Azure SQL Database uses the [Azure SQL Database emulator](local-dev-experience-azure-sql-database-emulator.md), a containerized database with close fidelity to the Azure SQL Database public service. The Azure SQL Database emulator is implemented as a [Docker](https://www.docker.com/) container.

Install [Docker Desktop](https://www.docker.com/products/docker-desktop/). If you are using Windows, set up [Docker Desktop for Windows with WSL 2](/windows/wsl/tutorials/wsl-containers).

Ensure that Docker Desktop is running before using your local development environment for Azure SQL Database.

## Install VSCode or Azure Data Studio

The local development environment for Azure SQL Database works in both [VSCode](https://code.visualstudio.com/Docs) and [Azure Data Studio](/sql/azure-data-studio/download-azure-data-studio). Install either of these tools into the same desktop environment where you have installed Docker Desktop.

## Install extensions

<!--Steps to install the Azure SQL Database extension-->

<!--Steps to install the SQL Database Projects extension-->

## Begin using your local development environment

You have now set up your local development environment for Azure SQL Database. Next, [Create a database project for a local Azure SQL Database development environment](local-dev-experience-create-database-project.md).

## Next steps

Learn more about the local development experience for Azure SQL Database:

- [What is the local development experience for Azure SQL Database?](local-dev-experience-overview.md)