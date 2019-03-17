---
title: "Step 5: Testing the Updated Packages | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 683e52e5-1c7e-49ab-9ffe-6a450a1c5776
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Step 5: Testing the Updated Packages
  Before you go on to the next lesson, in which you will create the deployment bundle to use to install the tutorial packages on the destination computer, you should test the packages. In this task, you will run the packages, DataTransfer.dtsx and LoadXMLData, that you added to the Deployment Tutorial project and then extended with configurations.  
  
 When the packages run, each executable in the package becomes a green color as it completes successfully. When all executables are green, the package has completed successfully. You can also view the package execution progress on the **Progress** tab.  
  
 If the packages do not run successfully, you must fix them before you go on to the next lesson.  
  
### To run the DataTransfer package  
  
1.  In Solution Explorer, click DataTransfer.dtsx.  
  
2.  On **Debug** menu, click **Start Debugging**.  
  
3.  After the package has completed running, on the **Debug** menu, click **Stop Debugging**.  
  
### To run the LoadXMLData package  
  
1.  In Solution Explorer, click LoadXMLData.dtsx.  
  
2.  On **Debug** menu, click **Start Debugging**.  
  
3.  After the package has completed running, on the **Debug** menu, click **Stop Debugging**.  
  
## Next Lesson  
 [Lesson 2: Creating the Deployment Bundle](../integration-services/lesson-2-create-the-deployment-bundle-in-ssis.md)  
  
![Integration Services icon (small)](media/dts-16.gif "Integration Services icon (small)")  **Stay Up to Date with Integration Services**<br /> For the latest downloads, articles, samples, and videos from Microsoft, as well as selected solutions from the community, visit the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] page on MSDN:<br /><br /> [Visit the Integration Services page on MSDN](https://go.microsoft.com/fwlink/?LinkId=136655)<br /><br /> For automatic notification of these updates, subscribe to the RSS feeds available on the page.  
  
  
