---
title: "Quickstart: Create a local development environment for Azure SQL Database"
description: Create a local development environment for Azure SQL Database using this hands-on guide. 
services: sql-database
ms.service: sql-database
author: scoriani
ms.author: scoriani
ms.reviewer: mathoma
ms.topic: quickstart 
ms.date: 05/24/2022
---

# Quickstart: Create a local development environment for Azure SQL Database
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

The Azure SQL Database [local development experience](local-dev-experience-overview.md) provides a way to design, edit, build/validate, publish and run database schemas in a local Azure SQL Database emulator. With the Database Projects feature, developers are able to easily publish Database Projects to the Azure SQL Database public service from their local environment, as well as manage the entire lifecycle of their databases (for example, manage schema drifts and such). This Quickstart teaches you the entire workflow that leverages the Azure SQL Database local development experience.

## Prerequisites

To complete this Quickstart, you must first [Set up a local development environment for Azure SQL Database](local-dev-experience-set-up-dev-environment.md).

## Create a blank project

To get started, either create a blank Database Project, or open an existing project. The steps in this section help you create a new blank project, but you can open an existing project by going to the **Projects** view or by searching for **Database Projects: Open Existing** in the command palette. start from an existing database by selecting **Create Project from Database** from the command palette or database context menu. Finally, you can start from an OpenAPI/Swagger spec by using the **Database Projects: Generate SQL Project from OpenAPI/Swagger spec** command in the command palette.

The steps for creating a new project using Visual Studio Code, or Azure Data Studio are the same. To create a blank project, follow these steps: 

1. Open your choice of developer tool, either Azure Data Studio, or Visual Studio Code. 
1. Select **Projects** and then choose to create a new Database Project. Alternatively, search for **Database Projects: New** in the command palette.
1. Choose **SQL Database** as your project type.

    ![Screenshot of selecting the project type for a Database Project in Visual Studio Code.](./media/local-dev-experience-quickstart/database-project-select-project-type.jpg)
    
1. Provide a name for the new SQL Database Project.
 
    ![Screenshot of entering a name for a Database Project in Visual Studio Code.](./media/local-dev-experience-quickstart/database-project-enter-project-name.jpg)
    
1. Select the SDK-style SQL Database Project project. (The [SDK-style SQL project (preview)](/sql/azure-data-studio/extensions/sql-database-project-extension-sdk-style-projects) is recommended for being more concise and manageable when working with multiple developers on a team's repository.)
 
    ![Screenshot of selecting a style for a Database Project in Visual Studio Code.](./media/local-dev-experience-quickstart/database-project-select-style.jpg)
    
1. To set the target platform for your project, right-click the the **Database Project name** and choose **Change Target Platform**. Select **Azure SQL Database** as the target platform for your project.
     
    ![Screenshot of selecting Azure SQL Database as a target for a Database Project.](./media/local-dev-experience-quickstart/database-project-target-platform.jpg)

    Setting your target platform provides editing and build time support for your SQL Database Project objects and scripts. After selecting your target platform, Visual Studio Code highlights syntax issues or indicates the select platform is using unsupported features. 
   
    Optionally, SQL Database Project files can be put under source control together with your application projects.

1. Add objects to your Database Project. You can create or alter database objects such as tables, views, stored procedures and scripts. For example, right-click the Database Project name and select **Add Table** to add a table.
 
    ![Screenshot of adding a table from the Database Projects menu in Visual Studio Code.](./media/local-dev-experience-quickstart/database-project-add-folder.jpg)
    
1. Build your Database Project to validate that it will work against the Azure SQL Database platform. To build your project, right-click the **Database Project name** and select **Build**.

    ![Screenshot of selecting build from the Database Project menu in Visual Studio Code.](./media/local-dev-experience-quickstart/database-project-build.jpg)
    
1. Once your Database Project is ready to be tested, publish it to a target. To begin the publishing process, right-click on the name of your Database Project and select **Publish**.

    ![Screenshot of selecting Publish in the SQL Database Project menu in Visual Studio Code.](./media/local-dev-experience-quickstart/database-project-publish.jpg)
    
1. When publishing, you can choose to publish to either a new or existing server. In this example, we choose **Publish to a new Azure SQL Database emulator**.

    ![Screenshot of selecting a publishing target in Visual Studio Code.](./media/local-dev-experience-quickstart/database-project-publish-container.jpg)
    
1. When publishing to a new Azure SQL Database emulator, you are prompted to choose between **Lite** and **Full** images. The **Lite** image has compatibility with most Azure SQL Database capabilities and is a lightweight image that takes less to download and instantiate. The **Full** image gives you access to advanced features like in-memory optimized tables, geo-spatial data types and more, but requires more resources. 

    ![Screenshot of select an Azure SQL Database emulator in Visual Studio Code.](./media/local-dev-experience-quickstart/database-project-docker-image.jpg)
    
    You can create as many local instances as necessary based on available resources, and manage their lifecycle through the Visual Studio Code Docker Extension or CLI commands.

    ![Screenshot of managing the Azure SQL Database emulator through the C L I.](./media/local-dev-experience-quickstart/database-project-publish-process.jpg)
     
1. Once instances of your Database Projects are running, you can connect from the Visual Studio Code mssql extension and test your scripts and queries, like any regular database in Azure SQL Database. 
 
    ![Screenshot of connecting to and querying an Azure SQL Database emulator.](./media/local-dev-experience-quickstart/connect-query-azure-sql-database.jpg)
     
1. Rebuild and deploy your Database project to one of the containerized instances running on your local machine with each iteration of adding or modifying objects in your Database Project, until it’s ready.
 
     ![Screenshot of iterating on a Database Project.](./media/local-dev-experience-quickstart/deploy-dacpac-succeeded.jpg)
    
1. The final step of the Database Project lifecycle is to publish the finished artifact to a new or existing database in Azure SQL Database using the mssql extension. Right-click the **Database Project name** and choose to **Publish**. Then select the destination where you want to publish your project, such as a new or existing [logical server in Azure](logical-servers.md). 
 
    ![Screenshot of publishing a Database Project to Azure SQL Database.](./media/local-dev-experience-quickstart/choose-connection-profile.jpg)


## Next steps

Learn more about the local development experience for Azure SQL Database:

- [Set up a local development environment for Azure SQL Database](local-dev-experience-set-up-dev-environment.md)
- [Create a Database Project for a local Azure SQL Database development environment](local-dev-experience-create-database-project.md)
- [Publish a Database Project for Azure SQL Database to the local emulator](local-dev-experience-publish-emulator.md)
- [Quickstart: Create a local development environment for Azure SQL Database](local-dev-experience-quickstart.md)
- [Introducing the Azure SQL Database emulator](local-dev-experience-sql-database-emulator.md)