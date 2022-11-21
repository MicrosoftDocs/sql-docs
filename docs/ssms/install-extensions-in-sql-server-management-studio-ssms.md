---
description: "Install Extensions in SQL Server Management Studio (SSMS)"
title: Install Extensions in SQL Server Management Studio (SSMS)
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
keywords:
  - "extensions"
  - "vsix"
  - "install extension"
  - "install vsix"
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan
ms.custom:
  - seo-lt-2019
  - intro-installation
ms.date: 07/29/2020
---

# Install Extensions in SQL Server Management Studio (SSMS)

[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW ](../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

SQL Server Management Studio (SSMS) extensions are created with C# through the "Visual Studio extension development" workload in Visual Studio. SSMS 18.x is built on the Visual Studio 2017 shell and is subject to the limitations of that environment.

Extension installation in SSMS 18.x can be achieved by deploying the VSIX through Visual Studio or an independent managed package installer.  The Visual Studio deployment is outlined below.

> [!NOTE]
> SQL Server Management Studio extensions cannot be installed via VSIXInstaller under SSMS 18.x.
  
## Visual Studio deployment of an extension for SSMS 18.x

Manual extension installation is accomplished by copying the associated extension files (VSIX) into the default SSMS extensions folder.  SSMS automatically checks this folder for extensions at launch.  The VSIX deployment can be completed by Visual Studio at project build time. 

  
1.  Locate your SSMS installation and default extensions folder.  With default SSMS installation settings, the folder location is ```C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\Extensions\```.  


2. Launch Visual Studio as administrator.

3.  The file copy process can be completed by Visual Studio at build time by checking the "Copy VSIX content to the following location" checkbox in the VSIX tab of the project's properties window. In the textbox below the checkbox, enter the folder location above with a folder for this extension appended.  For example: ```C:\Program Files (x86)\Microsoft SQL Server Management Studio 18\Common7\IDE\Extensions\SampleExtension```
  
![Project properties window VSIX settings with 3 checkboxes and a text box](./media/install-extensions/vsix_ssms.png)

4. Build the extension project, a successful build will transfer the extension files to the SSMS extension folder.

5.  Launch SSMS and test the extension's functionality.
  
