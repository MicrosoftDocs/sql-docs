---
title: "Running the Analysis Services Deployment Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "analysis-services"
ms.topic: conceptual
helpviewer_keywords: 
  - "Analysis Services Deployment Wizard, running"
ms.assetid: 3a38d489-4625-4878-bd18-c6f903be33df
author: minewiskan
ms.author: owend
manager: craigg
---
# Running the Analysis Services Deployment Wizard
  When you use the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard to deploy a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project, you can run the wizard in the following ways:  
  
-   **Interactively** When run interactively, the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard generates an XML deployment script based on the input files, as modified interactively by user input. The wizard applies any user modifications only to the deployment script. The wizard does not modify the input files. For more information about the input files, see [Understanding the Input Files Used to Create the Deployment Script](deployment-script-files-input-used-to-create-deployment-script.md).  
  
-   **From the command prompt** When run at the command prompt, the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard generates an XML for Analysis (XMLA) deployment script based upon the switches that you use to run the wizard. The wizard may any one of the following: prompt you for user input and modify input files based on that input; run a silent, unattended deployment using the input files as is; or create a deployment script that you can use later.  
  
## Running the Analysis Services Deployment Wizard Interactively  
 When you run interactively, the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard reads the values from the input files and presents this information to you. You can modify these input values-such as deployment destination, configuration settings, deployment options, and connection string passwords-or leave them as is. If you change any input values, the wizard uses these changes when generating the XMLA deployment script. However, the wizard does not make any changes to the values in the input file.  
  
> [!NOTE]  
>  If you want to have the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard modify the input values, run the wizard at the command prompt and set the wizard to run in answer file mode.  
  
 After you review the input values and make any wanted changes, the wizard generates the XMLA deployment script. You can run this deployment script immediately on the destination server or save the script for later use.  
  
#### To run the Analysis Services Deployment Wizard interactively  
  
-   Click **Start**, point to **All Programs**, point to **Microsoft SQL Server**, point to **Analysis Services**, and then click **Deployment Wizard**.  
  
     -or-  
  
-   In the **Projects** folder of the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project, double-click the *\<project name>*.asdatabase file.  
  
    > [!NOTE]  
    >  If you cannot find the *\<project name>*.asdatabase file, try using Search and specify *.asdatabase.  
  
## Running the Analysis Services Deployment Wizard at the Command Prompt  
 The [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard can also be run at the command prompt. When you run the wizard at the command prompt, you provide the full path to the .asdatabase file and  run the wizard in one of the following modes:  
  
 **Answer file mode**  
 In answer file mode, the wizard lets you interactively modify the input files that were originally generated when the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] project was built in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. The wizard saves these modified input files before generating the XMLA deployment script. The modified input files become the new starting point the next time that the wizard is run.  
  
 To run the wizard in answer file mode, use the **/a** switch.  
  
 **Silent mode**  
 In silent mode, the wizard runs a silent, unattended deployment based on the information resident in the input files.  
  
 To run the wizard in silent mode, use the **/s** switch. When you run the wizard in silent mode, messages are output to the console or to a log file if one is provided.  
  
 **Output mode**  
 In output mode, the wizard generates an XMLA deployment script for later execution based on the input files.  
  
 To run the wizard in output mode, use the **/o** switch and provide an output file name.  
  
 For more information about these command line switches, see [Deploy Model Solutions with the Deployment Utility](deploy-model-solutions-with-the-deployment-utility.md).  
  
 The following procedure describes how to run the [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] Deployment Wizard at the command prompt.  
  
#### To run the Analysis Services Deployment Wizard at the command prompt  
  
1.  Open a command prompt and navigate to the C:\Program Files\Microsoft SQL Server\100\Tools\Binn\VSShell\Common7\IDE folder.  
  
2.  Type **Microsoft.AnalysisServices.Deployment.exe** followed by the switches that correspond to the mode in which you want to run the wizard.  
  
## See Also  
 [Understanding the Analysis Services Deployment Script](understanding-the-analysis-services-deployment-script.md)   
 [Deploy Model Solutions Using the Deployment Wizard](deploy-model-solutions-using-the-deployment-wizard.md)  
  
  
