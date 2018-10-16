---
title: "Step 3: Testing the Lesson 6 Package | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: c184c92d-948f-4037-a502-5fabd909c84c
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Lesson 6-3 - Testing the Lesson 6 Package
At run time, your package will obtain the value for the Directory property from the VarFolderName parameter.  
  
To verify that the package updates the Directory property with the new value during run time, simply execute the package. Because only three sample data files were copied to the new directory, the data flow will run only three times, rather than iterate through the 14 files in the original folder.  
  
## Checking the Package Layout  
Before you test the package you should verify that the control and data flows in the Lesson 6 package contains the objects shown in the following diagrams. The control flow should be identical to the control flow in lesson 5. The data flow should be identical to the data flow in lesson 5.  
  
**Control Flow**  
  
![Control Flow](../integration-services/media/task3lesson6control.jpg "Control Flow")  
  
**Data Flow**  
  
![Data Flow](../integration-services/media/task3lesson6data.jpg "Data Flow")  
  
### TO test the Lesson 6 tutorial package  
  
1.  On the Debug menu, click Start Debugging.  
  
2.  After the package has completed running, on the Debug menu, and then click Stop Debugging.  
  
## Next Task in Lesson  
[Step 4: Deploying the Lesson 6 Package](../integration-services/lesson-6-4-deploying-the-lesson-6-package.md)  
  
  
  
