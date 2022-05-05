---
title: "Quickstart: Create a local development environment for Azure SQL Database"
description: Create a local development environment for Azure SQL Database using this hands-on guide. 
services: sql-database
ms.service: sql-database
author: scoriani
ms.author: scoriani
ms.reviewer: mathoma
ms.topic: quickstart 
ms.date: 05/05/2022
---

# Quickstart: Create a local development environment for Azure SQL Database

The Azure SQL Database [local development experience](local-dev-experience-overview.md) provides a way to design, edit, build/validate, publish and run database schemas in a local Azure SQL Database emulator. When the Database Projects feature matures, developers will be able to easily publish Database Projects to the Azure SQL Database public service from the same environment, and manage the entire lifecycle of their databases (for example, manage schema drifts and such). This Quickstart teaches you the entire workflow that leverages the Azure SQL Database local development experience.

## Prerequisites

Before following this Quickstart, first complete the steps in the [Set up a local development environment for Azure SQL Database](local-dev-experience-set-up-dev-environment.md) guide.

## Getting Started with Database Projects

In this Quickstart, we will create a blank Database Project.

1. Create a new Database Project by going to the **Projects** viewlet or by searching for **Database Projects: New** in the command palette.
 
    > [!NOTE] 
    > Existing Database Projects can be opened by going to the **Projects** viewlet or by searching for **Database Projects: Open Existing** in the command palette. You can alternately start from an existing database by selecting **Create Project from Database** from the command palette or database context menu. Finally, you can start from an OpenAPI/Swagger spec by using the **Database Projects: Generate SQL Project from OpenAPI/Swagger spec** command in the command palette.
    
2. Select **SQL Database** as your project type.

    ![Screenshot of selecting the project type for a Database Project in VSCode.](./media/local-dev-experience-quickstart/database-project-select-project-type.jpg)
    
3. Provide a name for the new SQL Database Project.
 
    ![Screenshot of entering a name for a Database Project in VSCode.](./media/local-dev-experience-quickstart/database-project-enter-project-name.jpg)
    
4. Add objects to your Database Project. You can create or alter database objects like tables, views, stored procedures and scripts. For example, you can right-click on the Database Project name and select **Add Table**.
 
    ![Screenshot of adding a table from the Database Projects menu in VSCode.](./media/local-dev-experience-quickstart/database-project-add-folder.jpg)
    
5. Set the target platform for your project by right-clicking on your Database Project name and selecting **Change Target Platform**. Select **Azure SQL Database** as the target platform for your project.
     
    ![Screenshot of selecting Azure SQL Database as a target for a Database Project.](./media/local-dev-experience-quickstart/database-project-target-platform.jpg)

    Setting your target platform provides editing and build time support for your SQL Database Project objects and scripts. After selecting your target platform, VSCode will highlight syntax issues or the usage of unsupported features for the selected platform.
   
    Optionally, SQL Database Project files can be put under source control together with your application projects.

6. Build your Database Project to validate that it will work against the Azure SQL Database platform. To build your project, right-click the Database Project name and select **Build**.

    ![Screenshot of selecting build from the Database Project menu in VSCode.](./media/local-dev-experience-quickstart/database-project-build.jpg)
    
7. Once your Database Project is ready to be tested, publish it to a target. To begin the publishing process, right-click on the name of your Database Project and select **Publish**.

    ![Screenshot of selecting Publish in the SQL Database Project menu in VSCode.](./media/local-dev-experience-quickstart/database-project-publish.jpg)
    
8. When publishing, you can choose to publish to either a new or existing server. In this example, we choose **Publish to a new server in a container**.

    ![Screenshot of selecting a publishing target in VSCode.](./media/local-dev-experience-quickstart/database-project-publish-container.jpg)
    
9. When publishing to a new server in a container, you will be prompted to choose between Azure SQL Database **lite** and **full** images for the Azure SQL Database emulator. With the **lite** image, you will get compatibility with most Azure SQL Database capabilities and a lightweight image that will take less to download and instantiate. If you select **full**, you will have access to advanced features like In-Memory Optimized tables, geo-spatial data types and more, but at the expense of more required resources.

    ![Screenshot of select an Azure SQL Database emulator in VSCode.](./media/local-dev-experience-quickstart/database-project-docker-image.jpg)
    
    You can create as many local instances as necessary, based on available resources, and manage their lifecycle through VS Code Docker Extension or CLI commands.

    ![Screenshot of managing the Azure SQL Database emulator through CLI.](./media/local-dev-experience-quickstart/database-project-publish-process.jpg)
     
10. Now that instances of your Database Projects are running, you can connect from the VSCode mssql extension and test your scripts and queries, like any regular database in Azure SQL Database. 
 
    ![Screenshot of connecting to and querying an Azure SQL Database emulator.](./media/local-dev-experience-quickstart/connect-query-azure-sql-database.jpg)
     
11. Following each iteration of adding or modifying objects in your Database Project, your Database Project can be rebuilt and deployed to one of the containerized instances running on the local machine, until it’s ready.
 
     ![Screenshot of iterating on a Database Project.](./media/local-dev-experience-quickstart/deploy-dacpac-succeeded.jpg)
    
12. The final step of the Database Project lifecycle is to publish the finished artifact to a new or existing database in Azure SQL Database using the mssql extension.
 
    ![Screenshot of publishing a Database Project to Azure SQL Database.](./media/local-dev-experience-quickstart/choose-connection-profile.jpg)


## Next steps

Learn more about the local development experience for Azure SQL Database:

- [Set up a local development environment for Azure SQL Database](local-dev-experience-set-up-dev-environment.md)
- [Create a Database Project for a local Azure SQL Database development environment](local-dev-experience-create-database-project.md)
- [Publish a Database Project for Azure SQL Database to the local emulator](local-dev-experience-publish-emulator.md)
- [Quickstart: Create a local development environment for Azure SQL Database](local-dev-experience-quickstart.md)
- [VSCode and Azure Data Studio extensions for the Azure SQL Database local development experience](local-dev-experience-extensions.md)
- [Introducing the Azure SQL Database emulator](local-dev-experience-azure-sql-database-emulator.md)