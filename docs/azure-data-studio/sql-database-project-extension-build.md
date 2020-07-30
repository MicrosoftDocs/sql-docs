---
title: Build and Publish a Project
description: Build and Publish with SQL Server Database Projects extension
ms.custom: "seodec18"
ms.date: "06/25/2020"
ms.reviewer: "drskwier, maghan, sstein"
ms.prod: sql
ms.technology: azure-data-studio
ms.topic: conceptual
author: "dzsquared"
ms.author: "drskwier"
---
# Build and Publish a project

The build process in the SQL Database Projects extension for Azure Data Studio allows for *dacpac* creation in Windows, macOS, and Linux environments. The project can be deployed to a local or cloud environment with the publish process.

## Prerequisites
- Install and configure [SQL Database Projects extension for Azure Data Studio](sql-database-project-extension.md).


## Build a Database Project

 In the **Projects** viewlet under **Explorer**, right-click the *.sqlproj* root node and select **Build**.

 The output pane will automatically appear with the output from the build process.  A successful build will conclude with the message: 

 ``` ... exited with code: 0 ```


## Publish a Database Project

After a project is successfully compiled through the build process, the database can be published to a SQL Server instance. To publish a database project, in the **Projects** viewlet under **Explorer**, right-click the *.sqlproj* root node and select **Publish**.

In the **Publish Database** dialog that appears, specify a server connection and the database name to be created.

## Next steps

- [SQL Database Projects extension for Azure Data Studio](sql-database-project-extension.md)
- [Data Tier Applications](../relational-databases/data-tier-applications/data-tier-applications.md)


