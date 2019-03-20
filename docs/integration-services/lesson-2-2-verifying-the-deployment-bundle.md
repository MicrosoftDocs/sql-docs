---
title: "Step 2: Verifying the Deployment Bundle | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: 6c13f5c9-c75e-4e52-94dc-2d2db2c578fe
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lesson 2-2 - Verifying the Deployment Bundle
In lesson 1, you created the Deployment Tutorial project and added packages and ancillary files to the project; in the previous task you built a deployment utility for the project.  
  
In this task, you will verify the contents of the deployment bundle. The deployment bundle is the folder that you will copy to the destination computer and use to install packages. If you used the default value-bin\Deployment-as the location of the deployment utility, the deployment bundle is the Bin\Deployment folder within the Deployment Tutorial folder in the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project.  
  
### To verify the content of deployment bundle  
  
1.  Locate the bin\Deployment folder on your computer.  
  
2.  Verify that the following files are present:  
  
    -   DataTransfer.dtsx  
  
    -   datatransferconfig.dtsconfig  
  
    -   Deployment Tutorial.SSISDeploymentManifest  
  
    -   LoadXMLData.dtsx  
  
    -   loadxmldataconfig.dtsconfig  
  
    -   NewCustomers.txt  
  
    -   orders.xml  
  
    -   orders.xsd  
  
    -   Readme.txt  
  
3.  Right-click Deployment Tutorial.SSISDeploymentManifest, point **to Open With**, and then click **Internet Explorer**. You can also open the file in a text editor such as Notepad. The XML code of the file should be the following:  
  
    `<?xml version="1.0"?><DTSDeploymentManifest GeneratedBy="Domain\UserName" GeneratedFromProjectName="Deployment Tutorial" GeneratedDate="2006-02-24T13:29:02.6537669-08:00" AllowConfigurationChanges="true"><Package>DataTransfer.dtsx</Package><Package>LoadXMLData.dtsx</Package><ConfigurationFile>datatransferconfig.dtsconfig</ConfigurationFile><ConfigurationFile>loadxmldataconfig.dtsconfig</ConfigurationFile><MiscellaneousFile>Readme.txt</MiscellaneousFile><MiscellaneousFile>orders.xml</MiscellaneousFile><MiscellaneousFile>NewCustomers.txt</MiscellaneousFile><MiscellaneousFile>orders.xsd</MiscellaneousFile></DTSDeploymentManifest>`  
  
4.  Verify that the value of the **AllowConfigurationChanges** attribute is **true** and the XML includes a **Package** element for each of the two packages, a **MiscellaneousFile** element for each of the four non-package files, and a **ConfigurationFile** element for each of the two XML configuration files.  
  
5.  Exit Internet Explorer or the text editor.  
  
## Next Lesson  
[Lesson 3: Install SSIS Packages](../integration-services/lesson-3-install-ssis-packages.md)  
  
  
  
