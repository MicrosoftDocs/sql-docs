---
title: Create and deploy a SQL project
description: "Deploy a SQL project tutorial for SQL DevOps."
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan, randolphwest
ms.date: 08/30/2024
ms.service: sql
ms.topic: tutorial
zone_pivot_groups: sq1-sql-projects-tools
---

# Tutorial: Create and deploy a SQL project

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The development cycle of a SQL database project enables database development to be integrated into a continuous integration and continuous deployment (CI/CD) workflows familiar as a development best practice. While deployment of a SQL database project can be done manually, it's recommended to use a deployment pipeline to automate the deployment process such that ongoing deployments are run based on your continued local development without additional effort.

This article steps through creating a new SQL project, adding objects to the project, and setting up a continuous deployment pipeline for building and deploying the project with GitHub actions. The tutorial is a superset of the contents of the [SQL projects getting started](../get-started.md) article. While the tutorial implements the deployment pipeline in GitHub actions, the same concepts apply to Azure DevOps, GitLab, and other automation environments.

In this tutorial, you:

1. Create a new SQL project
2. Add objects to the project
3. Build the project locally
4. Check the project into source control
5. Add a project build step to a continuous deployment pipeline
6. Add a `.dacpac` deployment step to a continuous deployment pipeline

If you've already completed the steps in the [SQL projects getting started](../get-started.md) article, you can skip to [step 4](#step-4-check-the-project-into-source-control). At this end of this tutorial, your SQL project will be automatically building and deploying changes to a target database.

## Prerequisites

::: zone pivot="sq1-visual-studio"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022 Community, Professional, or Enterprise](https://visualstudio.microsoft.com/downloads/)
- [SQL Server Data Tools (SSDT) installed in Visual Studio 2022](../../../ssdt/download-sql-server-data-tools-ssdt.md)

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022 Community, Professional, or Enterprise](https://visualstudio.microsoft.com/downloads/)
- [SQL Server Data Tools, SDK-style (preview) installed in Visual Studio 2022](../../../ssdt/sql-server-data-tools-sdk-style.md)

::: zone-end

::: zone pivot="sq1-visual-studio-code"

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [VS Code](https://code.visualstudio.com/Download)
- [SQL Database Projects extension for Azure Data Studio](/azure-data-studio/extensions/sql-database-project-extension) or [SQL Database Projects extension for VS Code](https://marketplace.visualstudio.com/items?itemName=ms-mssql.sql-database-projects-vscode)

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

Make sure you have the following items to complete the pipeline setup in GitHub:

- A GitHub account where you can create a repository. [Create one for free](https://github.com).

- GitHub actions is [enabled](https://docs.github.com/repositories/managing-your-repositorys-settings-and-features/enabling-features-for-your-repository/managing-github-actions-settings-for-a-repository) on your repository.

> [!NOTE]  
> To complete the deployment of a SQL database project, you need access to an Azure SQL or SQL Server instance. You can develop locally for free with [SQL Server developer edition](https://www.microsoft.com/sql-server/sql-server-downloads) on Windows or in [containers](../../../linux/quickstart-install-connect-docker.md).

## Step 1: Create a new project

We start our project by creating a new SQL database project before manually adding objects to it. There are other ways to create a project that enable immediately populating the project with objects from an existing database, such as using the [schema comparison tools](../howto/compare-database-project.md).

::: zone pivot="sq1-visual-studio"

Select **File**, **New**, then **Project**.

In the **New Project** dialog box, use the term **SQL Server** in the search box. The top result should be **SQL Server Database Project**.

:::image type="content" source="media/create-deploy-sql-project/new-project-dialog.png" alt-text="Screenshot of New project dialog." lightbox="media/create-deploy-sql-project/new-project-dialog.png":::

Select **Next** to proceed to the next step. Provide a project name, which doesn't need to match a database name. Verify and modify the project location as needed.

Select **Create** to create the project. The empty project is opened and visible in the **Solution Explorer** for editing.

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

Select **File**, **New**, then **Project**.

In the **New Project** dialog box, use the term **SQL Server** in the search box. The top result should be **SQL Server Database Project, SDK-style (preview)**.

:::image type="content" source="media/create-deploy-sql-project/vs-sdk-new-project-dialog.png" alt-text="Screenshot of New project dialog." lightbox="media/create-deploy-sql-project/vs-sdk-new-project-dialog.png":::

Select **Next** to proceed to the next step. Provide a project name, which doesn't need to match a database name. Verify and modify the project location as needed.

Select **Create** to create the project. The empty project is opened and visible in the **Solution Explorer** for editing.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

In the **Database Projects** view of VS Code or Azure Data Studio, select the **New Project** button.

:::image type="content" source="media/create-deploy-sql-project/projects-viewlet.png" alt-text="Screenshot of New viewlet.":::

The first prompt determines which project template to use, primarily based on whether the target platform is SQL Server or Azure SQL. If prompted to select a specific version of SQL, choose the version that matches the target database but if the target database version is unknown, choose the latest version as the value can be modified later.

Enter a project name in the text input that appears, which doesn't need to match a database name.

In the "Select a Folder" dialog that appears, select a directory for the project's folder, `.sqlproj` file, and other contents to reside in.

When prompted whether to create an SDK-style project (preview), select **Yes**.

Once completed, the empty project is opened and visible in the **Database Projects** view for editing.

::: zone-end

::: zone pivot="sq1-command-line"

With the .NET templates for Microsoft.Build.Sql projects installed, you can create a new SQL database project from the command line. The `-n` option specifies the name of the project, and the `-tp` option specifies the project target platform.

Use the `-h` option to see all available options.

```bash
# install Microsoft.Build.Sql.Templates
dotnet new sqlproject -n MyDatabaseProject
```

::: zone-end

## Step 2: Add objects to the project

::: zone pivot="sq1-visual-studio"

In **Solution Explorer**, right-click the project node and select **Add**, then **Table**. The **Add New Item** dialog appears, where you can specify the table name. Select **Add** to create the table in the SQL project.

The table is opened in the Visual Studio table designer with the template table definition, where you can add columns, indexes, and other table properties. Save the file when you're done making the initial edits.

More database objects can be added through the **Add New Item** dialog, such as views, stored procedures, and functions. Access the dialog by right-clicking the project node in **Solution Explorer** and selecting **Add**, then the desired object type. Files in the project can be organized into folders through the **New Folder** option under **Add**.

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

In **Solution Explorer**, right-click the project node and select **Add**, then **New Item**. The **Add New Item** dialog appears, select **Show All Templates** and then **Table**. Specify the table name as the file name and select **Add** to create the table in the SQL project.

The table is opened in the Visual Studio query editor with the template table definition, where you can add columns, indexes, and other table properties. Save the file when you're done making the initial edits.

More database objects can be added through the **Add New Item** dialog, such as views, stored procedures, and functions. Access the dialog by right-clicking the project node in **Solution Explorer** and selecting **Add**, then the desired object type after **Show All Templates**. Files in the project can be organized into folders through the **New Folder** option under **Add**.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

In the **Database Projects** view of VS Code or Azure Data Studio, right-click the project node and select **Add Table**. In the dialog that appears, specify the table name.

The table is opened in the text editor with the template table definition, where you can add columns, indexes, and other table properties. Save the file when you're done making the initial edits.

More database objects can be added through the context menu on the project node, such as views, stored procedures, and functions. Access the dialog by right-clicking the project node in **Database Projects** view of VS Code or Azure Data Studio, then the desired object type. Files in the project can be organized into folders through the **New Folder** option under **Add**.

::: zone-end

::: zone pivot="sq1-command-line"

Files can be added to the project by creating them in the project directory or nested folders. The file extension should be `.sql` and organization by object type or schema and object type is recommended.

The base template for a table can be used as a starting point for creating a new table object in the project:

```sql
CREATE TABLE [dbo].[Table1]
(
  [Id] INT NOT NULL PRIMARY KEY
)
```

::: zone-end

## Step 3: Build the project

The build process validates the relationships between objects and the syntax against the target platform specified in the project file. The artifact output from the build process is a `.dacpac` file, which can be used to deploy the project to a target database and contains the compiled model of the database schema.

::: zone pivot="sq1-visual-studio"

In **Solution Explorer**, right-click the project node and select **Build**.

The output window automatically opens to display the build process. If there are errors or warnings, they're displayed in the output window. On a successful build, the build artifact (`.dacpac` file) is created its location is included in the build output (default is `bin\Debug\projectname.dacpac`).

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

In **Solution Explorer**, right-click the project node and select **Build**.

The output window automatically opens to display the build process. If there are errors or warnings, they're displayed in the output window. On a successful build, the build artifact (`.dacpac` file) is created its location is included in the build output (default is `bin\Debug\projectname.dacpac`).

::: zone-end

::: zone pivot="sq1-visual-studio-code"

In the **Database Projects** view of VS Code or Azure Data Studio, right-click the project node and select **Build**.

The output window automatically opens to display the build process. If there are errors or warnings, they're displayed in the output window. On a successful build, the build artifact (`.dacpac` file) is created its location is included in the build output (default is `bin/Debug/projectname.dacpac`).

::: zone-end

::: zone pivot="sq1-command-line"

SQL database projects can be built from the command line using the `dotnet build` command.

```bash
dotnet build

# optionally specify the project file
dotnet build MyDatabaseProject.sqlproj
```

The build output includes any errors or warnings and the specific files and line numbers where they occur. On a successful build, the build artifact (`.dacpac` file) is created its location is included in the build output (default is `bin/Debug/projectname.dacpac`).

::: zone-end

## Step 4: Check the project into source control

We will initialize our project as a Git repository and commit the project files to source control. This step is necessary to enable the project to be shared with others and to be used in a continuous deployment pipeline.

::: zone pivot="sq1-visual-studio"

1. From the **Git** menu in Visual Studio, select **Create Git Repository**.

    :::image type="content" source="media/create-deploy-sql-project/vs-git-menu-create-git-repository.png" alt-text="Screenshot of the Create Git Repository option from the Git menu in Visual Studio.":::

2. In the **Create a Git repository** dialog, under the **Push to a new remote** section, choose **GitHub**.

3. In the **Create a new GitHub repository** section of the **Create a Git repository** dialog, enter the name of the repo you want to create. (If you haven't yet signed in to your GitHub account, you can do so from this screen, too.)

    :::image type="content" source="media/create-deploy-sql-project/vs-git-create-repo-dialog.png" alt-text="Screenshot of the Create Git Repository dialog in Visual Studio with the GitHub selection highlighted." lightbox="media/create-deploy-sql-project/vs-git-create-repo-dialog.png":::

    Under **Initialize a local Git Repository**, you should use the **.gitignore template** option to specify any intentionally untracked files that you want Git to ignore. To learn more about .gitignore, see [Ignoring files](https://docs.github.com/get-started/getting-started-with-git/ignoring-files). And to learn more about licensing, see [Licensing a repository](https://docs.github.com/repositories/managing-your-repositorys-settings-and-features/customizing-your-repository/licensing-a-repository).

4. After you sign in and enter your repo info, select the **Create and Push** button to create your repo and add your app.

::: zone-end

::: zone pivot="sq1-visual-studio-sdk"

In **Solution Explorer**, right-click the project node and select **Publish...**.

The publish dialog opens, where you establish the **target database connection**. If you don't have an existing SQL instance for deployment, LocalDB (`(localdb)\MSSQLLocalDB`) is installed with Visual Studio and can be used for testing and development.

Specify a database name and select **Publish** to deploy the project to the target database or **Generate Script** to generate a script to review before executing.

::: zone-end

::: zone pivot="sq1-visual-studio-code"

You can initialize and local repository and publish it directly to GitHub from VS Code or Azure Data Studio. This action creates a new repository on your GitHub account and pushes your local code changes to the remote repository in a single step.

Use the **Publish to GitHub** button in the Source Control view in VS Code or Azure Data Studio. You're then prompted to specify a name and description for the repository, and as well as whether to make it public or private.

:::image type="content" source="media/create-deploy-sql-project/vsc-publish-to-github.png" alt-text="Screenshot of the Create Git Repository dialog in Visual Studio with the GitHub selection highlighted." lightbox="media/create-deploy-sql-project/vsc-publish-to-github.png":::

Alternatively, you can initialize a local repository and push it to GitHub following the steps provided when you create an empty [repository on GitHub](https://docs.github.com/repositories/creating-and-managing-repositories/creating-a-new-repository).

::: zone-end

::: zone pivot="sq1-command-line"

Initialize a new Git repository in the project directory and commit the project files to source control.

```bash
git init
git add .
git commit -m "Initial commit"
```

Create a new repository on GitHub and push the local repository to the remote repository.

```bash
git remote add origin <repository-url>
git push -u origin main
```

::: zone-end

## Step 5: Add a project build step to a continuous deployment pipeline

SQL projects are backed by a .NET library and as a result the projects are built with the `dotnet build` command. This command is a staple of even the most basic continuous integration and continuous deployment (CI/CD) pipelines. The build step can be added to a continuous deployment pipeline we create in GitHub actions.

1. In the root of the repository, create a new directory named `.github/workflows`. This directory will contain the workflow file that defines the continuous deployment pipeline.
2. In the `.github/workflows` directory, create a new file named `sqlproj-sample.yml`.
3. Add the following content to the `sqlproj-sample.yml` file, editing the project name to match the name and path of your project:

    ```yml
    name: sqlproj-sample

    on:
      push:
        branches: [ "main" ]

    jobs:
      build:
        runs-on: ubuntu-latest

        steps:
        - uses: actions/checkout@v4

        - name: Setup .NET
          uses: actions/setup-dotnet@v4
          with:
            dotnet-version: 8.0.x

        - name: Build
          run: dotnet build MyDatabaseProject.sqlproj
    ```

4. Commit the workflow file to the repository and push the changes to the remote repository.
5. On GitHub.com, navigate to the main page of the repository. Under your repository name, click **Actions**. In the left sidebar, select the workflow you just created. A recent run of the workflow should appear in the list of workflow runs from when you pushed the workflow file to the repository.

More information on the fundamentals of creating your first GitHub actions workflow is available in the [GitHub Actions quickstart](https://docs.github.com/actions/writing-workflows/quickstart).

## Step 6: Add a `.dacpac` deployment step to a continuous deployment pipeline

The compiled model of a database schema in a `.dacpac` file can be deployed to a target database using the `SqlPackage` command-line tool or other deployment tools. The deployment process determines the necessary steps to update the target database to match the schema defined in the `.dacpac`, creating or altering objects as needed based on the objects already existing in the database. For example, to deploy a `.dacpac` file to a target database based on a connection string:

```bash
sqlpackage /Action:Publish /SourceFile:bin/Debug/MyDatabaseProject.dacpac /TargetConnectionString:{yourconnectionstring}
```

:::image type="content" source="media/create-deploy-sql-project/dacfx-deployment-process.png" alt-text="Screenshot of DacFx source and target comparison process before deployment." lightbox="media/create-deploy-sql-project/dacfx-deployment-process.png":::

The deployment process is idempotent, meaning it can be run multiple times without causing issues. The pipeline we're creating will build and deploy our SQL project every time a change is checked into the `main` branch of our repository. Instead of executing the `SqlPackage` command directly in our deployment pipeline, we can use a deployment task that abstracts the command and provides additional features such as logging, error handling, and task configuration. The deployment task [GitHub sql-action](https://github.com/azure/sql-action) can be added to a continuous deployment pipeline in GitHub actions.

> [!NOTE]  
> Running a deployment from an automation environment requires configuring the database and environment such that the deployment can reach the database and authenticate. In Azure SQL Database or SQL Server in a VM, this might require setting up a firewall rule to allow the automation environment to connect to the database as well as providing a connection string with the necessary credentials. Guidance is provided in the [GitHub sql-action](https://github.com/azure/sql-action) documentation.

1. Open the `sqlproj-sample.yml` file in the `.github/workflows` directory.
2. Add the following step to the `sqlproj-sample.yml` file after the build step:

    ```yml
    - name: Deploy
      uses: azure/sql-action@v2
      with:
        connection-string: ${{ secrets.SQL_CONNECTION_STRING }}
        action: 'publish'
        path: 'bin/Debug/MyDatabaseProject.dacpac'
    ```

3. Before committing the changes, add a secret to the repository that contains the connection string to the target database. In the repository on GitHub.com, navigate to **Settings**, then **Secrets**. Select **New repository secret** and add a secret named `SQL_CONNECTION_STRING` with the value of the connection string to the target database.

    :::image type="content" source="media/create-deploy-sql-project/github-repository-secret.png" alt-text="Screenshot of the GitHub repository settings with the New repository secret button highlighted." lightbox="media/create-deploy-sql-project/github-repository-secret.png":::

4. Commit the changes from `sqlproj-sample.yml` to the repository and push the changes to the remote repository.
5. Navigate back to the workflow history on GitHub.com and select the most recent run of the workflow. The deployment step should be visible in the list of steps for the workflow run and the workflow returns a success code.
6. Verify the deployment by connecting to the target database and checking that the objects in the project are present in the database.

GitHub deployments can be further secured by establishing an environment relationship in a workflow and requiring approval before a deployment is run. More information on environment protection and protecting secrets is available in the [GitHub Actions documentation](https://docs.github.com/actions/security-for-github-actions/security-guides/using-secrets-in-github-actions).

## Get help

- [DacFx GitHub repository](https://github.com/microsoft/dacfx)
- [GitHub sql-action repository](https://github.com/azure/sql-action)

## Related content

- [Compare a database and a project](../howto/compare-database-project.md)
- [Convert an original SQL project to an SDK-style project](../howto/convert-original-sql-project.md)
- [SqlPackage Publish parameters, properties, and SQLCMD variables](../../sqlpackage/sqlpackage-publish.md)
