---
title: Create extensions
titleSuffix: Azure Data Studio
description: Learn about creating and adding extensions to Azure Data Studio
ms.custom: "seodec18"
ms.date: "09/24/2018"
ms.reviewer: "alayu; sstein"
ms.prod: sql
ms.technology: azure-data-studio
ms.topic: conceptual
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Extend the functionality by creating Azure Data Studio extensions

Extensions in [!INCLUDE[name-sos](../includes/name-sos-short.md)] provide an easy way to add more functionality to the base [!INCLUDE[name-sos](../includes/name-sos-short.md)] installation.

Extensions are provided by the Azure Data Studio team (Microsoft), as well as the 3rd party community (you!).


## Author an extension

If you're interested in extending Azure Data Studio, you can create your own extension and publish it to the extension gallery.

**Writing an Extension**

***Prerequisites***

To develop an extension you need Node.js installed and available in your $PATH. Node.js includes npm, the Node.js Package Manager, which will be used to install the extension generator.

To start your new extension, you can use the Azure Data Studio Extension generator. The Yeoman [extension generator](https://www.npmjs.com/package/generator-sqlops) makes it very easy to create simple extension projects. To Launch the generator, type the following in a command prompt:

`npm install -g yo generator-sqlops`

`yo sqlops`


**Extensibility References**

To learn about Azure Data Studio Extensibility see [Extensibility overview](extensibility.md). You can also see examples of how to use the API in existing [samples](https://github.com/Microsoft/azuredatastudio/tree/master/samples).


## Debug an extension

You can debug your new extension using Visual Studio Code extension [Azure Data Studio Debug](https://github.com/kevcunnane/sqlops-debug).

Steps
- Open your extension with [Visual Studio Code](https://code.visualstudio.com/)
- Install Azure Data Studio Debug extension
- Press **F5** or click the Debug icon and click **Start**.
- A new instance of Azure Data Studio starts in a special mode (Extension Development Host) and this new instance is now aware of your extension.


## Create an extension package

After writing your extension, you need to create a VSIX package to be able to install it in Azure Data Studio. You can use [vsce](https://github.com/Microsoft/vscode-vsce) to create the VSIX package.

`npm install -g vsce`

`vsce package`


## Publish an extension

To publish your new extension to Azure Data Studio:

1. Add your extension to https://github.com/Microsoft/azuredatastudio/blob/release/extensions/extensionsGallery.json
2. We currently don't have support to host third party extensions, so instead of downloading the extension, Azure Data Studio has the option to browse to a download page. To set a download page for your extension, set the value of asset "Microsoft.AzureDataStudio.DownloadPage".
3. Create a PR against release/extensions branch.
4. Send a review request to the team.

Your extension will be reviewed and added to the extension gallery.

**Publishing Extension Updates**
The process to publish updates is similar to publishing the extension. Please make sure the version is updated in package.json
