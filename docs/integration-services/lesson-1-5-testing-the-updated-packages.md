---
description: "Lesson 1-5 - Testing the Updated Packages"
title: "Step 5: Testing the Updated Packages | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: tutorial
ms.assetid: 683e52e5-1c7e-49ab-9ffe-6a450a1c5776
author: chugugrace
ms.author: chugu
---
# Lesson 1-5 - Testing the Updated Packages

[!INCLUDE[sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]


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
[Lesson 2: Create the Deployment Bundle in SSIS](../integration-services/lesson-2-create-the-deployment-bundle-in-ssis.md)  
  
  
  
