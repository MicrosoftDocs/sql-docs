---
title: Start the SQL Server Import and Export Wizard
titleSuffix: Integration Services (SSIS)
description: "Start the SQL Server Import and Export Wizard"
author: chugugrace
ms.author: chugu
ms.reviewer: mathoma
ms.date: 09/27/2024
ms.service: sql
ms.subservice: integration-services
ms.topic: conceptual
helpviewer_keywords:
  - "SQL Server Import and Export Wizard"
  - "starting SQL Server Import and Export Wizard"
  - "Import and Export Wizard"
  - "starting Import and Export Wizard"
---

# Start the SQL Server Import and Export Wizard

[!INCLUDE [sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

This article describes four ways to start the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard. Use this tool to import data from, and export data to, any supported data source.

You can start the wizard:

- From [SQL Server Management Studio (SSMS)](#sql-server-management-studio-ssms).
- From the [Windows Start menu](#start-menu).
- From the [command prompt](#command-prompt).
- From [Visual Studio](#visual-studio).

> [!TIP]  
> Another way to import data is with the [SQL Server Import extension](../../azure-data-studio/extensions/sql-server-import-extension.md) extension in Azure Data Studio.

## Get the wizard

You can run the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard directly through [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] (SSMS).

To run the wizard outside of SSMS, you need to have `DTSWizard.exe`, which you get when you install one of the following:

- [SQL Server](https://www.microsoft.com/sql-server/sql-server-downloads).
- [SQL Server Data Tools (SSDT) for Visual Studio 2019 and later](../../ssdt/download-sql-server-data-tools-ssdt.md#install-extensions-for-analysis-services-integration-services-and-reporting-services) with the [SQL Server Integration Services extension](https://marketplace.visualstudio.com/items?itemName=SSIS.MicrosoftDataToolsIntegrationServices).
- [SQL Server Data Tools (SSDT) for Visual Studio 2017](../../ssdt/previous-releases-of-sql-server-data-tools-ssdt-and-ssdt-bi.md#ssdt-for-vs-2017-standalone-installer) with the SQL Server Integration Services option selected in the installation wizard.

> [!NOTE]  
> To use the 64-bit version of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard, you have to install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. SQL Server Data Tools (SSDT) and SQL Server Management Studio (SSMS) are 32-bit applications and only install 32-bit files, including the 32-bit version of the wizard.

## SQL Server Management Studio (SSMS)

To start the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard from [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], follow these steps:

1. In [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], connect to an instance of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. Expand **Databases**.
1. Right-click a database.
1. Point to **Tasks**.
1. Select one of the following options.
   - **Import Data**

   - **Export Data**

   :::image type="content" source="media/start-the-sql-server-import-and-export-wizard/start-wizard-ssms.jpg" alt-text="Screenshot of start wizard SSMS.":::

If you don't have SQL Server Management Studio installed, see [Download SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md).

## Start menu

To start the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard from the Windows Start menu,  [DTSWizard.exe](#get-the-wizard) must be installed through SQL Server. Check [Get the wizard](#get-the-wizard) for installation details.

To start the wizard from the Windows Start menu, follow these steps:

1. On the **Start** menu, find and expand folder for your SQL Server version.
1. Select the SQL Server (version) Import and Export data tool you want to use.

    Run the 64-bit version of the wizard unless you know that your data source requires a 32-bit data provider.

    :::image type="content" source="media/start-the-sql-server-import-and-export-wizard/start-wizard-start-64.png" alt-text="Screenshot of Start menu find Import and Export Wizard.":::

## Command prompt

To start the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard from the command prompt, the [DTSWizard.exe](#get-the-wizard) must be installed either through SQL Server or through SSDT for Visual Studio 2017. The wizard that comes with Visual Studio 2019 and later can't be launched from the executable, or the command prompt. Check [Get the wizard](#get-the-wizard) for installation details. 

The path for `DTSWizard.exe` is different if you installed it through SQL Server, or through Visual Studio, such as the following examples: 
- `C:\Program Files\Microsoft SQL Server\<##>\DTS\Binn`
   - For the 64-bit version installed through SQL Server
   - The <##> correlates to the version of SQL Server. For example, *160* is for SQL Server 2022. 
- `C:\Program Files (x86)\Microsoft SQL Server\<##>\DTS\Binn`
   - For the 32-bit version installed through SQL Server. 
   - The <##> correlates to the version of SQL Server. For example, *160* is for SQL Server 2022. 
- `C:\Program Files (x86)\Microsoft Visual Studio\2017\SQL\Common7\IDE\CommonExtensions\Microsoft\SSIS\140\Binn`
   - For the 32-bit version installed through [SSDT for Visual Studio 2017](../../ssdt/previous-releases-of-sql-server-data-tools-ssdt-and-ssdt-bi.md#ssdt-for-vs-2017-standalone-installer) with SQL Server Integration Services checked in the installation wizard. 

To run the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard from the command prompt, navigate to where the executable is located, and then run `DTSWizard.exe`. 

For example, to launch the 64-bit version of the wizard from the command prompt on SQL Server 2022, run the following command:

```console
C:\Program Files\Microsoft SQL Server\160\DTS\Binn> DTSWizard.exe
```


## Visual Studio

To run the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Import and Export Wizard within Visual Studios 2019 and later, you need the [Integration Services extension](../../ssdt/download-sql-server-data-tools-ssdt.md#install-extensions-for-analysis-services-integration-services-and-reporting-services).  Check [Get the wizard](#get-the-wizard) for installation details.


In Visual Studio, open an Integration Services project, and then do one of the following things: 

- From the **Project** menu, select **SSIS Import and Export Wizard**.

   :::image type="content" source="media/start-the-sql-server-import-and-export-wizard/start-wizard-project.png" alt-text="Screenshot of Start wizard Project.":::

   \- or -

- In Solution Explorer, right-click the **SSIS Packages** folder, and then select **SSIS Import and Export Wizard**.

    :::image type="content" source="media/start-the-sql-server-import-and-export-wizard/start-wizard-packages.png" alt-text="Screenshot of Start wizard Packages.":::


## Get help while the wizard is running

> [!TIP]  
> Tap the F1 key from any page or dialog box of the wizard to see documentation for the current page.

## What's next?

When you start the wizard, the first page is **Welcome to SQL Server Import and Export Wizard**. You don't have to take any action on this page. For more info, see [Welcome to SQL Server Import and Export Wizard](welcome-to-sql-server-import-and-export-wizard.md).

## Related content

- [Import and Export Data with the SQL Server Import and Export Wizard](import-and-export-data-with-the-sql-server-import-and-export-wizard.md)
- [Get started with this simple example of the Import and Export Wizard](get-started-with-this-simple-example-of-the-import-and-export-wizard.md)
- [Using the SQL Server Import and Export Wizard to Export to Excel](https://go.microsoft.com/fwlink/?linkid=829049)
- [Steps in the SQL Server Import and Export Wizard](steps-in-the-sql-server-import-and-export-wizard.md)
- [Connect to Data Sources with the SQL Server Import and Export Wizard](connect-to-data-sources-with-the-sql-server-import-and-export-wizard.md)
