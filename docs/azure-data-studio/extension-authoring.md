---
title: Create extensions for Azure Data Studio (preview) | Microsoft Docs
description: Add extensions to Azure Data Studio (preview)
ms.custom: "tools|sos"
ms.date: "09/24/2018"
ms.reviewer: "alayu; sstein"
ms.prod: sql
ms.suite: "sql"
ms.prod_service: sql-tools
ms.component: sos
ms.tgt_pltfrm: ""
ms.topic: conceptual
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Extend the functionality of [!INCLUDE[name-sos](../includes/name-sos-short.md)]

Extensions in [!INCLUDE[name-sos](../includes/name-sos-short.md)] provide an easy way to add more functionality to the base [!INCLUDE[name-sos](../includes/name-sos-short.md)] installation. 

Extensions are provided by the Azure Data Studio team (Microsoft), as well as the 3rd party community (you!).


## Extension Authoring

If you're interested in extending Azure Data Studio, you can create your own extension and publish it to the extension gallery.

**Writing an Extension**

***Prerequisites***

To develop an extension you need Node.js installed and available in your $PATH. Node.js includes npm, the Node.js Package Manager, which will be used to install the extension generator.

To start your new extension, you can use the SQL Operations Studio Extension generator. The Yeoman [extension generator](https://www.npmjs.com/package/generator-sqlops) makes it very easy to create simple extension projects. To Launch the generator, type the following in a command prompt:

`npm install -g yo generator-sqlops`

`yo sqlops`


**Extensibility References**

Please see [Extensibility References](https://github.com/Microsoft/sqlopsstudio/wiki/Getting-started-with-Extensibility) to learn about SQL Operations Studio Extensibility API. You can also see examples of how to use the API in existing [samples](https://github.com/Microsoft/sqlopsstudio/tree/master/samples).


**Debugging Extension**

You can debug your new extension using Visual Studio Code extension [SQL Operations Studio Debug](https://github.com/kevcunnane/sqlops-debug).

Steps
- Open your extension with [Visual Studio Code](https://code.visualstudio.com/)
- Install SQL Operations Studio Debug extension
- Press F5 or click the Debug icon and click Start
- A new instance of SQL Operations Studio will start in a special mode (Extension Development Host) and this new instance is now aware of your extension.


**Creating Extension Package**

After writing your extension, you need to create a VSIX package to be able to install it in SQL Operations Studio. You can use [vsce](https://github.com/Microsoft/vscode-vsce) to create VSIX package.

`npm install -g vsce`

`vsce package`


**Publishing the Extension**

To publish your new extension to SQL Ops Studio
1. Add your extension to https://github.com/Microsoft/sqlopsstudio/blob/release/extensions/extensionsGallery.json
2. We don't have support to host third party extension for now. Instead of downloading the extension, SQL Ops has the option to browses to the download page. To be able to set a download page for your extension please set the value of asset "Microsoft.SQLOps.DownloadPage". 
3. Create a PR against release/extensions branch
4. Send a review request to llali

Your extension will be reviewed and will be added to the extension gallery.

**Publishing Extension Updates**
The process to publish updates is similar to publishing the extension. Please make sure the version is updated in package.json


