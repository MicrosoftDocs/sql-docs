---
title: Create extensions
description: You can add functionality to Azure Data Studio with an extension. Learn how to create an extension and publish it to the extensions gallery.
author: markingmyname
ms.author: maghan
ms.reviewer: alayu
ms.date: 08/26/2020
ms.service: azure-data-studio
ms.topic: conceptual
---

# Extend functionality by creating Azure Data Studio extensions

Extensions in Azure Data Studio provide an easy way to add more functionality to the base Azure Data Studio installation.

Extensions are provided by the Azure Data Studio team (Microsoft), as well as the third-party community (you!).

## Author an extension

If you're interested in extending Azure Data Studio, you can create your own extension and publish it to the extensions gallery.

### Write an extension

#### Prerequisites

To develop an extension, you need [Node.js](https://nodejs.org/) installed and available in your `$PATH`. Node.js includes npm, the Node.js Package Manager, which is used to install the extension generator.

To create your new extension, you can use the Azure Data Studio extension generator. The Yeoman [extension generator](https://www.npmjs.com/package/generator-azuredatastudio) is a beneficial starting point for extension projects. To start the generator, enter the following command in a command prompt:

```console
npm install -g yo generator-azuredatastudio
yo azuredatastudio
```

For an in-depth guide on how to get started with your extension template, see [keymap extension](keymap-extension.md), which walks you through the creation of an extension.

### Extensibility references

To learn about Azure Data Studio extensibility, see [Extensibility overview](../extensibility.md). You can also see examples of how to use the API in existing [samples](https://github.com/Microsoft/azuredatastudio/tree/main/samples).

## Debug an extension

You can debug your new extension by using the Visual Studio Code extension [Azure Data Studio Debug](https://github.com/kevcunnane/sqlops-debug).

To debug your extension:

1. Open your extension with [Visual Studio Code](https://code.visualstudio.com/).
2. Install the Azure Data Studio Debug extension.
3. Select **F5**, or select the **Debug** icon and then select **Start**.
4. A new instance of Azure Data Studio starts in a special mode (Extension Development Host). This new instance is now aware of your extension.

## Create an extension package

After writing your extension, you need to create a VSIX package that installs in Azure Data Studio. You can use [vscode-vsce](https://github.com/Microsoft/vscode-vsce) (Visual Studio Code Extensions) to create the VSIX package.

```console
npm install -g vsce
cd myExtensionName
vsce package
# The myExtensionName.vsix file has now been generated
```

With a VSIX package, you can share your extension locally and privately by sharing the .vsix file and using the command **Extensions: Install From VSIX File** from the command palette to install the extension in Azure Data Studio.

## Publish an extension

To publish your new extension to Azure Data Studio:

1. Add your extension to the [extensions gallery](https://github.com/Microsoft/azuredatastudio/blob/release/extensions/extensionsGallery.json).
2. We currently don't have support to host third-party extensions. Instead of downloading the extension, Azure Data Studio has the option to browse to a download page. To set a download page for your extension, set the value of the asset **Microsoft.AzureDataStudio.DownloadPage**.
3. Create a PR against release/extensions branch.
4. Send a review request to the team.

Your extension will be reviewed and added to the extensions gallery.

### Publish extension updates

The process to publish updates is similar to publishing the extension. Make sure the version is updated in package.json.

## Next steps

See one of the following extension authoring tutorials for step-by-step instructions on how to get started:

- [Keymap extension tutorial](keymap-extension.md)
- [Dashboard extension tutorial](dashboard-extension.md)
- [Notebook extension tutorial](notebook-extension.md)
- [Jupyter Book extension tutorial](jupyter-book-extension.md)
- [Wizard extension tutorial](wizard-extension.md)
