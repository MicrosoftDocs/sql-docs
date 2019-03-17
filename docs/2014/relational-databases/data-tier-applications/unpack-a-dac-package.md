---
title: "Unpack a DAC Package | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology:
ms.topic: conceptual
helpviewer_keywords: 
  - "wizard [DAC], unpack"
  - "data-tier application [SQL Server], unpack"
  - "How to [DAC], unpack"
  - "unpack DAC"
ms.assetid: 697b69b3-f157-4e22-ac4e-f65c5fc2d0ad
author: stevestein
ms.author: sstein
manager: craigg
---
# Unpack a DAC Package
  Use the Unpack Data-tier Application dialog box to unzip the scripts and files from a data-tier application (DAC) package. The scripts and files are placed in a folder where they can be reviewed before the package is used to deploy the DAC into a production system. The contents of one DAC can also be compared with the contents of another package unpacked to another folder.  
  
1.  **Before you begin:**  [Security](#Security)  
  
2.  **To unpack a DAC, using:**  [Unpack Data-tier Application Dialog](#UnpackDACDial), [Examine the Contents of a DAC Package](#ExamDACPack)  
  
##  <a name="Security"></a> Security  
 We recommend that you do not deploy a DAC package from unknown or untrusted sources. Such DACs could contain malicious code that might execute unintended [!INCLUDE[tsql](../../includes/tsql-md.md)] code or cause errors by modifying the schema. Before you use a DAC from an unknown or untrusted source, deploy it on an isolated test instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)], unpack the DAC and examine the code, such as stored procedures or other user-defined code.  
  
##  <a name="UnpackDACDial"></a> Unpack Data-tier Application Dialog  
 **To Unpack a DAC Package File**  
  
-   In **Windows Explorer**, navigate to the location of a DAC package (.dacpac) file.  
  
-   Use one of these two methods to open the Unpack Data-tier Application dialog:  
  
    1.  Right-click the DAC package (.dacpac) file and select **Unpack**.  
  
    2.  Double-click the DAC package file.  
  
-   Complete the dialogs:  
  
    -   [Unpack Microsoft SQL Server DAC Package File](#Unpack)  
  
    -   [Browse for Folder](#Browse)  
  
###  <a name="Unpack"></a> Unpack Microsoft SQL Server DAC Package File  
 Use this page to specify the destination folder in which to place the unpacked files, and then run the unpack operation.  
  
 **Files will be unpacked to this folder:** - Specify the full path to the folder for the unpacked files. If the folder exists and you know the full path, type the path in the box. If not, click the **Browse** button to navigate to a folder or create a new folder.  
  
 **Browse** - Opens the **Browse for Folder** page where you can choose a folder by navigating the file hierarchy, or create a new folder.  
  
 **Unpack** - Starts the unpack operation.  
  
 **Cancel** - Terminates the dialog box without unpacking the DAC package.  
  
###  <a name="Browse"></a> Browse for Folder  
 Use this page to choose the destination folder for the unpack operation. Optionally, you can also create a new folder.  
  
 **Folder list** - Displays the file hierarchy for your computer. Expand the nodes to navigate to the folder in which to unpack the DAC package. Click on the folder and then click **OK**.  
  
 **Make New Folder** - Opens a dialog in which you can specify the name for a new folder to be created in the folder you have currently selected in the folder hierarchy.  
  
 **OK** - Places the path to the folder you selected in the **Files will be unpacked to this folder** box of the **Unpack DAC Package File** page and returns you to that page.  
  
 **Cancel** - Terminates the dialog box without selecting a folder.  
  
##  <a name="ExamDACPack"></a> Examine the Contents of a DAC Package  
 After unpacking the package, you can examine the files produced by the **Unpack Data-tier Application** dialog. The dialog box builds the following files in the selected destination folder:  
  
1.  A Transact-SQL script that contains the statements for creating the objects defined in the DAC. The file name is *DACName*.sql, where *DACName* is the name of the DAC.  
  
2.  All XML files from the package.  
  
3.  All files from the Extra Files section of the DAC, such as the DAC pre-deployment or post-deployment files.  
  
 For more information, see [Validate a DAC Package](validate-a-dac-package.md).  
  
## See Also  
 [Data-tier Applications](data-tier-applications.md)   
 [Deploy a Data-tier Application](deploy-a-data-tier-application.md)   
 [Upgrade a Data-tier Application](upgrade-a-data-tier-application.md)  
  
  
