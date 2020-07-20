---
title: Create extensions
description: Learn about creating and publishing extensions to Azure Data Studio
ms.prod: azure-data-studio
ms.technology: 
ms.topic: conceptual
author: "markingmyname"
ms.author: "maghan"
ms.reviewer: "alayu, maghan, sstein"
ms.custom: "seodec18"
ms.date: "09/24/2018"
---

# Extend the functionality by creating Azure Data Studio extensions

Extensions in Azure Data Studio provide an easy way to add more functionality to the base Azure Data Studio installation.

Extensions are provided by the Azure Data Studio team (Microsoft), as well as the 3rd party community (you!).


## Author an extension

If you're interested in extending Azure Data Studio, you can create your own extension and publish it to the extension gallery.

**Writing an Extension**

***Prerequisites***

To develop an extension, you need [Node.js](https://nodejs.org/) installed and available in your `$PATH`. Node.js includes npm, the Node.js Package Manager, which will be used to install the extension generator.

To create your new extension, you can use the Azure Data Studio Extension Generator. The Yeoman [Extension Generator](https://www.npmjs.com/package/generator-azuredatastudio) is a beneficial starting point for extension projects. To launch the generator, type the following in a command prompt:

```
npm install -g yo generator-azuredatastudio # Install the generator
yo azuredatastudio
```

For an in-depth guide on how to get started with your extension template, see [Creating an Extension](https://docs.microsoft.com/sql/azure-data-studio/tutorial-create-extension?view=sql-server-ver15), which walks you through the creation of a keymap extension.

**Extensibility References**

To learn about Azure Data Studio Extensibility see [Extensibility overview](extensibility.md). You can also see examples of how to use the API in existing [samples](https://github.com/Microsoft/azuredatastudio/tree/main/samples).


## Debug an extension

You can debug your new extension using Visual Studio Code extension [Azure Data Studio Debug](https://github.com/kevcunnane/sqlops-debug).

Steps
1. Open your extension with [Visual Studio Code](https://code.visualstudio.com/).
1. Install the Azure Data Studio Debug extension.
1. Press **F5** or click the Debug icon and click **Start**.
1. A new instance of Azure Data Studio starts in a special mode (Extension Development Host) and this new instance is now aware of your extension.


## Create an extension package

After writing your extension, you need to create a VSIX package to be able to install it in Azure Data Studio. You can use [vsce](https://github.com/Microsoft/vscode-vsce) (Visual Studio Code Extensions) to create the VSIX package. 

```
npm install -g vsce
cd myExtensionName
vsce package
# The myExtensionName.vsix file has now been generated
```

With a VSIX package, you can share your extension locally and privately by sharing the `.vsix` file and using the command **Extensions: Install From VSIX File** from the Command Palette to install the extension into Azure Data Studio.


## Publish an extension

To publish your new extension to Azure Data Studio:

1. Add your extension to https://github.com/Microsoft/azuredatastudio/blob/release/extensions/extensionsGallery.json
2. We currently don't have support to host third party extensions, so instead of downloading the extension, Azure Data Studio has the option to browse to a download page. To set a download page for your extension, set the value of asset "Microsoft.AzureDataStudio.DownloadPage".
3. Create a PR against release/extensions branch.
4. Send a review request to the team.

Your extension will be reviewed and added to the extension gallery.

**Publishing extension updates**

The process to publish updates is similar to publishing the extension. Please make sure the version is updated in package.json.
