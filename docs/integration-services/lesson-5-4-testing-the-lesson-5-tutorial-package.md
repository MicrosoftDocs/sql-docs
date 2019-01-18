---
title: "Step 4: Testing the Lesson 5 Tutorial Package | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: 5215b77d-c2ec-4b25-a3de-ca49ea197d74
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Lesson 5-4 - Testing the Lesson 5 Tutorial Package
At run time, your package will obtain the value for the **Directory** property from a variable updated at run time, rather than using the original directory name that you specified when you created the package. The value of the variable is populated by the SSISTutorial.dtsConfig file.  
  
To verify that the package updates the Directory property with the new value during run time, simply execute the package. Because only three sample data files were copied to the new directory, the data flow will run only three times, rather than iterate through the 14 files in the original folder.  
  
## Checking the Package Layout  
Before you test the package you should verify that the control and data flows in the Lesson 5 package contains the objects shown in the following diagrams. The control flow should be identical to the control flow in lesson 4. The data flow should be identical to the data flow in lessons 4.  
  
**Control Flow**  
  
![Control flow in package](../integration-services/media/task4lesson2control.gif "Control flow in package")  
  
**Data Flow**  
  
![Data flow in package](../integration-services/media/task9lesson1data.gif "Data flow in package")  
  
### To test the Lesson 5 tutorial package  
  
1.  On the **Debug** menu, click **Start Debugging**.  
  
2.  After the package has completed running, on the **Debug** menu, and then click **Stop Debugging**.  
  
## Next Lesson  
[Lesson 6: Using Parameters with the Project Deployment Model in SSIS](../integration-services/lesson-6-using-parameters-with-the-project-deployment-model-in-ssis.md)  
  
  
  
