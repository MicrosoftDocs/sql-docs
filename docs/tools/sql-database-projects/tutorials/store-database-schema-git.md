---
title: Store the Database Schema in Git
description: Track changes to your database schema by storing its definition in source control.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: drskwier
ms.date: 07/03/2026
ms.service: sql
ms.subservice: sql-database-projects
ms.topic: tutorial
ms.collection:
  - data-tools
zone_pivot_groups: sq1-sql-projects-tools
---

# Tutorial: Store the database schema in Git

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance FabricSQLDB](../../../includes/applies-to-version/sql-asdb-asdbmi-fabricsqldb.md)]

Storing the database schema in Git provides several benefits, including version control, collaboration, and the ability to track changes over time. This approach is particularly useful for code-first development where you define the database schema through code and apply changes directly to a development environment by using an ORM or coding agent like GitHub Copilot.

In this tutorial, you learn how to store the database schema in Git and manage it as part of your development workflow.

> [!div class="checklist"]
> - Create a new SQL project from an existing database
> - Commit and push the database code to a Git remote repository
> - Apply changes from the database to the existing code
> - Pull changes from the Git remote repository and apply them to the local database (optional)

:::image type="content" source="media/store-database-schema-git/conceptual-outline.png" alt-text="Diagram demonstrating the tutorial flow of information." lightbox="media/store-database-schema-git/conceptual-outline.png":::

When you finish, you're ready to track your database in source control and collaborate with your team on database changes through a Git repository.

> [!NOTE]  
> To complete the tutorial, you need access to a SQL database. You can develop locally for free by using [SQL Server developer edition](https://www.microsoft.com/sql-server/sql-server-downloads) on Windows or in [containers](../../../linux/install-upgrade/quickstart-install-docker.md).

## Prerequisites

::: zone pivot="sq1-visual-studio"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022 Community, Professional, or Enterprise](https://visualstudio.microsoft.com/downloads/)
- [Install SQL Server Data Tools (SSDT) for Visual Studio](../../../ssdt/download-sql-server-data-tools-ssdt.md)

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

> [!WARNING]  
> This tutorial isn't available for the SDK-style projects (preview) in Visual Studio 2022. Use SQL Server Management Studio (SSMS) or Visual Studio Code instead.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio Code](https://code.visualstudio.com/Download)
- [SQL Database Projects extension](../../visual-studio-code-extensions/sql-database-projects/sql-database-projects-extension.md)

::: zone-end

::: zone pivot="sq1-sql-server-management-studio"

- [.NET SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [SQL Server Management Studio (SSMS)](/ssms/install/install)
- [Database DevOps workload installed in SSMS](/ssms/install/modify)

::: zone-end

::: zone pivot="sq1-command-line"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SqlPackage CLI](../../sqlpackage/sqlpackage-download.md)
- [Microsoft.Build.Sql.Templates .NET templates](https://www.nuget.org/packages/Microsoft.Build.Sql.Templates/)

```bash
# install SqlPackage CLI
dotnet tool install -g Microsoft.SqlPackage

# install Microsoft.Build.Sql.Templates
dotnet new install Microsoft.Build.Sql.Templates
```

::: zone-end

## Step 1: Create a new SQL project from an existing database

To represent your database in source control, use the [SQL database project](../sql-database-projects.md) format. The project includes `.sql` files for each database object and a `.sqlproj` with database settings. When you view the project in file explorer and source control, the structure of a SQL project mirrors browsing the database in object explorer.

::: zone pivot="sq1-visual-studio"

From the **SQL Server Object Explorer** in Visual Studio, right-click the database you want to create a project from and select **Create New Project...**.

:::image type="content" source="media/store-database-schema-git/ssdt-import-database.png" alt-text="Screenshot of Import Database dialog in Visual Studio.":::

In the **Create New Project** dialog, enter a project name. The project name doesn't need to match a database name. Verify and modify the project location as needed. The default import settings import the objects into folders by schema, then object type. You can modify the import settings to change the folder structure or to include permissions in the objects being imported. **Start** the import.

The **Import Database** dialog displays the import progress as messages. When the import finishes, you can see the imported objects in the **Solution Explorer**. The process stores the logs in a file in the project directory under `Import Schema Logs`. Select **Finish**.

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

> [!WARNING]  
> This tutorial isn't available for the SDK-style projects (preview) in Visual Studio 2022. Use SQL Server Management Studio (SSMS) or Visual Studio Code instead.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

In the database object explorer view in Visual Studio Code, select a database you want to create a project from. Right-click the database and select **Create Project from Database**.

:::image type="content" source="media/store-database-schema-git/new-project-from-database-vs-code.png" alt-text="Screenshot of Create project from database dialog in Visual Studio Code.":::

In Visual Studio Code, the **Create project from database** dialog requires the project name and location. The default import settings import the objects into folders by schema, then object type. You can select a different folder structure or choose to include permissions in the objects being imported before selecting **Create**.

Open the **Database Projects** view to see the new project and imported object definitions.

::: zone-end

::: zone pivot="sq1-sql-server-management-studio"

From the **Object Explorer** in SQL Server Management Studio, right-click the database you want to create a project from and select **Create Project from Database...** under the **Tasks** menu.

:::image type="content" source="media/store-database-schema-git/new-project-from-database-ssms.png" alt-text="Screenshot of New SQL Project from Database dialog in SQL Server Management Studio.":::

In the **Create SQL Project from Database** dialog, verify the selected database and modify the project name and location as needed. The new project is created with the objects from the selected database and arranged in folders by schema, then object type.

A solution file is automatically created in the folder above the project folder, and opens in the **Solution Explorer** after creation. If you want to add your new project to an existing solution, select **Add to solution** and browse to the solution file. Select **Create** to create the project.

::: zone-end

::: zone pivot="sq1-command-line"

Use the SqlPackage CLI to extract the schema of an existing database to a SQL project. The following SqlPackage command extracts the schema of a database to a complete SQL project. The `.sql` files are organized by nested schema and object type folders and a `.sqlproj` file is created to manage the project with properties set matching the database's options.

```bash
sqlpackage /a:Extract /ssn:localhost /sdn:MyDatabase /tf:MyDatabaseProject /p:ExtractTarget=SqlProject
```

::: zone-end

## Step 2: Commit and push the database code to a Git remote repository

After creating the SQL project, start the Git timeline for the database in a local Git repository.

::: zone pivot="sq1-visual-studio"

With the SQL project open in Visual Studio, create a local Git repository and push it to a remote.

1. Select **Git** > **Create Git Repository** from the menu bar.

1. In the **Create a Git Repository** dialog, under the **Push to a new remote** section, choose either **GitHub** or **Azure DevOps**. If you choose to push to GitHub, sign in to your GitHub account when prompted. If you choose Azure DevOps, sign in to your Azure DevOps account and select or create a project.

1. Enter a repository name and description. Ensure the **.gitignore template** is set to **VisualStudio** and select **Create and Push**.

Visual Studio creates the local Git repository, makes an initial commit with your SQL project files, and pushes it to the remote.

To verify the code was pushed successfully, open a browser and navigate to your repository on GitHub (<https://github.com>) or Azure DevOps (<https://dev.azure.com>). You should see the SQL project files, including the `.sqlproj` file and the `.sql` object definitions.

For more information, see [About Git in Visual Studio](/visualstudio/version-control/git-with-visual-studio).

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

> [!WARNING]  
> This tutorial isn't available for the SDK-style projects (preview) in Visual Studio 2022. Use SQL Server Management Studio (SSMS) or Visual Studio Code instead.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

With the SQL project folder open in Visual Studio Code, initialize a local Git repository and push it to a remote.

1. Open the **Source Control** view by selecting the Source Control icon in the Activity Bar, or press `Ctrl+Shift+G`.

1. Select **Initialize Repository** to create a new Git repository in the current folder.

1. All project files appear as untracked changes. Select the **+** icon next to **Changes** to stage all files.

1. Enter a commit message such as `Initial commit of SQL project` in the message input box and select the **Commit** button (checkmark icon).

1. Select **Publish Branch** in the Source Control view to push your local repository to a remote. Choose whether to publish to **GitHub** or **Azure DevOps** and follow the sign-in prompts. Select a repository visibility (public or private) and confirm.

To verify the code was pushed successfully, open a browser and navigate to your repository on GitHub (<https://github.com>) or Azure DevOps (<https://dev.azure.com>). You should see the SQL project files, including the `.sqlproj` file and the `.sql` object definitions.

For more information, see [Source Control in Visual Studio Code](https://code.visualstudio.com/docs/sourcecontrol/overview).

::: zone-end

::: zone pivot="sq1-sql-server-management-studio"

With the SQL project open in SQL Server Management Studio, create a local Git repository and push it to a remote.

1. Select **Git** > **Create Git Repository** from the menu bar.

1. In the **Create a Git Repository** dialog, under the **Push to a new remote** section, choose either **GitHub** or **Azure DevOps**. If you choose to push to GitHub, sign in to your GitHub account when prompted. If you choose Azure DevOps, sign in to your Azure DevOps account and select or create a project.

1. Enter a repository name and description. Ensure the **.gitignore template** is set to **VisualStudio** and select **Create and Push**.

SSMS creates the local Git repository, makes an initial commit with your SQL project files, and pushes it to the remote.

To verify the code was pushed successfully, open a browser and navigate to your repository on GitHub (<https://github.com>) or Azure DevOps (<https://dev.azure.com>). You should see the SQL project files, including the `.sqlproj` file and the `.sql` object definitions.

For more information, see [About Git in Visual Studio](/visualstudio/version-control/git-with-visual-studio).

::: zone-end

::: zone pivot="sq1-command-line"

From the SQL project directory, initialize a local Git repository, commit all files, and push to a remote.

1. Initialize a new Git repository, add a `.gitignore` file, and make an initial commit:

   ```bash
   cd MyDatabaseProject
   git init
   dotnet new gitignore
   git add .
   git commit -m "Initial commit of SQL project"
   ```

1. Create a new remote repository in GitHub or Azure DevOps:

   - **GitHub**: Go to <https://github.com/new> and create a new repository. Don't initialize it with a README, `.gitignore`, or license.
   - **Azure DevOps**: Go to your Azure DevOps project, select **Repos**, and create a new repository. Uncheck **Add a README**.

1. Add the remote and push your initial commit:

   ```bash
   git remote add origin <your-repository-url>
   git push -u origin main
   ```

   Replace `<your-repository-url>` with the URL provided by GitHub or Azure DevOps when you created the repository.

To verify the code is pushed successfully, open a browser and go to your repository. You should see the SQL project files, including the `.sqlproj` file and the `.sql` object definitions.

::: zone-end

## Step 3: Apply changes from the database to the existing code

When the database schema changes outside of the SQL project (for example, through direct modifications to a database, an ORM migration, or agentic actions), you can update the project to reflect those changes.

::: zone pivot="sq1-visual-studio"

Use schema compare to update the SQL project from the database.

1. In **SQL Server Object Explorer**, right-click the database and select **Schema Compare**.

1. In the **Schema Compare** window, the database is set as the source. Select the **Select Target** dropdown list and choose **Select Target**.

1. In the **Select Target Schema** dialog, select **Project** and choose your SQL database project. Select **OK**.

1. Select **Compare** to run the comparison. The results display the differences between the database (source) and the project (target).

1. Review the differences. Uncheck any changes you don't want to apply to the project.

1. Select **Update** to apply the selected changes to the SQL project.

The project files are updated to match the database schema. Review the changes in the **Git Changes** window, enter a commit message, and select **Commit All**. Select **Push** to push the changes to the remote repository.

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

> [!WARNING]  
> This tutorial isn't available for the SDK-style projects (preview) in Visual Studio 2022. Use SQL Server Management Studio (SSMS) or Visual Studio Code instead.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

Use the **Update Project from Database** command to update the SQL project from the database.

1. In the database object explorer, right-click the database and select **SQL Database Projects** > **Update Project from Database**.

1. Select the SQL database project to update and confirm.

1. The comparison runs and applies the changes from the database to the project. The updated `.sql` files appear in the **Source Control** view as modified files.

Review the changes in the **Source Control** view, stage the modified files, enter a commit message, and select **Commit**. Select **Sync Changes** to push the commit to the remote repository.

::: zone-end

::: zone pivot="sq1-sql-server-management-studio"

Use schema compare to update the SQL project from the database.

1. In **Object Explorer**, right-click the database and select **Tasks** > **Schema Compare (preview)**.

1. In the **Schema Compare** window, the database is set as the source. Select the target ellipsis button and choose your SQL database project as the target.

1. Select **Compare** to run the comparison. The results display the differences between the database (source) and the project (target).

1. Review the differences. Uncheck any changes you don't want to apply to the project.

1. Select **Apply** to apply the selected changes to the SQL project.

The project files are updated to match the database schema. Review the changes in the **Git Changes** window, enter a commit message, and select **Commit All**. Select **Push** to push the changes to the remote repository.

::: zone-end

::: zone pivot="sq1-command-line"

Use SqlPackage to regenerate the SQL project from the database, replacing the existing project files with the current database schema. While the other SQL project tools provide a graphical interface, SqlPackage offers a command-line approach for automation that doesn't provide an opportunity to review the file changes before applying them.

1. Remove the existing SQL object files and regenerate the project:

   ```bash
   rm -rf MyDatabaseProject
   sqlpackage /a:Extract /ssn:localhost /sdn:MyDatabase /tf:MyDatabaseProject /p:ExtractTarget=SqlProject
   ```

1. Review the changes with Git and commit:

   ```bash
   cd MyDatabaseProject
   dotnet new gitignore
   git add .
   git status
   git commit -m "Update project from database changes"
   git push
   ```

::: zone-end

## Step 4: (optional) Pull changes from the Git remote repository and apply them to the local database

When other team members push database schema changes to the remote repository, you can pull those changes and deploy them to your local development database.

::: zone pivot="sq1-visual-studio"

1. Select **Git** > **Pull** from the menu bar to pull the latest changes from the remote repository.

1. In **Solution Explorer**, right-click the SQL database project node and select **Schema Compare**.

1. In the **Schema Compare** window, the project is set as the source. Select the **Select Target** dropdown list and choose **Select Target**.

1. In the **Select Target Schema** dialog, select **Database** and choose your local development database connection. Select **OK**.

1. Select **Compare** to run the comparison. The results display the differences between the project (source) and the database (target).

1. Review the differences to verify the changes are expected.

1. Select **Update** to apply the changes to the database.

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

> [!WARNING]  
> This tutorial isn't available for the SDK-style projects (preview) in Visual Studio 2022. Use SQL Server Management Studio (SSMS) or Visual Studio Code instead.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

1. Open the **Source Control** view and select **Pull** from the **...** menu (or use the sync button in the Status Bar) to pull the latest changes from the remote repository.

1. In the **Database Projects** view, right-click the SQL database project and select **Schema Compare**.

1. In the **Schema Compare** window, the project is set as the source. Select the target ellipsis button and choose your local development database connection as the target.

1. Select **Compare** to run the comparison. The results display the differences between the project (source) and the database (target).

1. Review the differences to verify the changes are expected.

1. Select **Apply** to apply the changes to the database.

::: zone-end

::: zone pivot="sq1-sql-server-management-studio"

1. Open a terminal and run `git pull` in the project directory to pull the latest changes from the remote repository.

1. In **Solution Explorer**, right-click the SQL database project node and select **Schema Compare (preview)**.

1. In the **Schema Compare** window, the project is set as the source. Select the target and choose your local development database connection.

1. Select **Compare** to run the comparison. The results display the differences between the project (source) and the database (target).

1. Review the differences to verify the changes are expected.

1. Select **Apply** to apply the changes to the database.

::: zone-end

::: zone pivot="sq1-command-line"

1. Pull the latest changes from the remote repository:

   ```bash
   cd MyDatabaseProject
   git pull
   ```

1. Build the project to verify the changes compile successfully:

   ```bash
   dotnet build
   ```

1. Deploy the changes to your local development database using SqlPackage:

   ```bash
   sqlpackage /a:Publish /sf:bin/Debug/MyDatabaseProject.dacpac /tsn:localhost /tdn:MyDatabase
   ```

   Replace `MyDatabaseProject.dacpac` with the name of your `.dacpac` output file, and update the target server (`/tsn`) and database (`/tdn`) values as needed.

::: zone-end

## Related content

- [Compare a database and a project](../howto/compare-database-project.md)
- [Analyze T-SQL code to find defects](../howto/analyze-t-sql-code-to-find-defects.md)
- [SQL projects automation](../sql-projects-automation.md)
- [Tutorial: Create and deploy a SQL project](create-deploy-sql-project.md)
