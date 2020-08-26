---
title: "Tutorial: Create dashboard extension"
description: This tutorial demonstrates how to create a dashboard extension to add custom functionality to Azure Data Studio.
ms.prod: azure-data-studio
ms.technology: azure-data-studio
ms.reviewer: alayu, maghan
ms.topic: tutorial
author: anjalia
ms.author: t-anjaga
ms.custom:
ms.date: 08/26/2020
---

# Tutorial: Create an Azure Data Studio Dashboard extension

This tutorial demonstrates how to create a new **Azure Data Studio Dashboard extension**. The extension contributes to the Azure Data Studio connection dashboard, so you can extend the functionality of Azure Data Studio in a manner easily visible to users.

During this tutorial you learn how to:
> [!div class="checklist"]
> * Install the extension generator
> * Create your extension
> * Contribute to the dashboard in your extension
> * Test your extension
> * Package your extension
> * Publish your extension to the marketplace

## Prerequisites

Azure Data Studio is built on the same framework as Visual Studio Code, so extensions for Azure Data Studio are built using Visual Studio Code. To get started, you need the following components:

- [Node.js](https://nodejs.org) installed and available in your `$PATH`. Node.js includes [npm](https://www.npmjs.com/), the Node.js Package Manager, which is used to install the extension generator.
- [Visual Studio Code](https://code.visualstudio.com) to debug the extension.
- The Azure Data Studio [Debug extension](https://marketplace.visualstudio.com/items?itemName=ms-mssql.sqlops-debug) (optional). This lets you test your extension without needing to package and install it into Azure Data Studio.
- Ensure `azuredatastudio` is in your path. For Windows, make sure you choose the `Add to Path` option in setup.exe. For Mac or Linux, run the *Install 'azuredatastudio' command in PATH* option.

## Install the extension generator

To simplify the process of creating extensions, we've built an [extension generator](https://code.visualstudio.com/docs/extensions/yocode) using Yeoman. To install it, run the following from the command prompt:

`npm install -g yo generator-azuredatastudio`

## Create your dashboard extension

### Introduction to the dashboard

The Azure Data Studio connection dashboard is a powerful tool that summarizes and provides insight into a user's connections.

There are two variations of the dashboard: the 'server dashboard' that summarizes the entire server, and the 'database dashboard' that summarizes an individual database. You can access either by right-clicking on a server or a database in the Connections viewlet of Azure Data Studio, and clicking **Manage**:

![dashboards intro](./media/tutorial-create-dashboard-extension/dashboard_summary.gif)

There are 3 key contribution points for extensions to add functionality to the dashboard:

- Graphs that run against your SQL Server
- Homepage Actions</span>: Action buttons at the top of the connection toolbar
- Full Dashboard Tab</span>: A separate tab in the dashboard for your extension. Can be added to either a server or database dashboard. Customizable with widgets, a toolbar, and a navigation section.

![contribution points](./media/tutorial-create-dashboard-extension/dashboard_contrib_points.png)

### Run the extension generator

To create an extension:

1. Launch the extension generator with the following command:

   `yo azuredatastudio`

2. Choose **New Dashboard** from the list of extension types.

3. Fill in the prompts as shown below. This will create an extension that contributes a tab to the Server Dashboard.

![extension generator](./media/tutorial-create-dashboard-extension/dashboard_generator.png)

There are a lot of prompts, so here is a little more information on what each question means:

![dashboards flowchart](./media/tutorial-create-dashboard-extension/dashboard_flowchart.png)

Completing the previous steps creates a new folder. Open the folder in Visual Studio Code and you're ready to create your own dashboard extension!

### Run the extension

Let's see what the dashboard template gives us by running the extension. Before running, ensure that the **Azure Data Studio Debug extension** is installed in Visual Studio Code.

Select **F5** in VS Code to launch Azure Data Studio in debug mode with the extension running. Then, you can view how this default template contributes to the dashboard.

Next, we will look at how to modify this default dashboard.

### Develop the dashboard

The most important files to get started with extension development is `package.json`. This is the manifest file, where the dashboard contributions are registered. Note the `dashboard.tabs`, `dashboard.insights`, and `dashboard.containers` sections.

Here are some changes to try out:

- Play around with the insights types, including 'bar', 'horizontalBar', 'timeSeries'
- Write your own queries to run against your SQL Server connection
- Refer to this [sample insight tutorial](tutorial-qds-sql-server.md) or [this one](tutorial-table-space-sql-server.md) for specific insight tutorials

## Package your extension

To share with others you need to package the extension into a single file. This can be published to the Azure Data Studio extension marketplace, or shared among your team or community. To do this, you need to install another npm package from the command line:

`npm install -g vsce`

Edit the `README.md` to your liking, then navigate to the base directory of the extension, and run `vsce package`. You can optionally link a repository with your extension or continue without one. To add one, add a similar line to your `package.json` file.

```json
"repository": {
    "type": "git",
    "url": "https://github.com/anjalia/my-test-extension.git"
}
```

After these lines were added, a my-test-extension-0.0.1.vsix file was created and ready to install in Azure Data Studio.

![install vsix](./media/tutorial-create-dashboard-extension/install-vsix.png)

## Publish your extension to the marketplace

The Azure Data Studio extension marketplace is not totally implemented yet, but the current process is to host the extension VSIX somewhere (for example, a GitHub Release page) then submit a PR updating [this JSON file](https://github.com/Microsoft/azuredatastudio/blob/release/extensions/extensionsGallery.json) with your extension info.

## Next steps

In this tutorial, you learned how to:
> [!div class="checklist"]
> * Install the extension generator
> * Create your extension
> * Contribute to the dashboard in your extension
> * Test your extension
> * Package your extension
> * Publish your extension to the marketplace

We hope after reading this you'll be inspired to build your own extension for Azure Data Studio. We have support for Dashboard Insights (pretty graphs that run against your SQL Server), a number of SQL-specific APIs, and a huge existing set of extension points inherited from Visual Studio Code.

If you have an idea but are not sure how to get started, please open an issue or tweet at the team: [azuredatastudio](https://twitter.com/azuredatastudio).

You can always refer to the [Visual Studio Code extension guide](https://code.visualstudio.com/docs/extensions/overview) because it covers all the existing APIs and patterns.

To learn how to work with T-SQL in Azure Data Studio, complete the T-SQL Editor tutorial:

> [!div class="nextstepaction"]
> [Use the Transact-SQL editor to create database objects](tutorial-sql-editor.md).
