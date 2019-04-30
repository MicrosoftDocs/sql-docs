---
title: "Step 3: Test the Lesson 6 package | Microsoft Docs"
ms.custom: ""
ms.date: "01/11/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: tutorial
ms.assetid: c184c92d-948f-4037-a502-5fabd909c84c
author: janinezhang
ms.author: janinez
manager: craigg
---
# Lesson 6-3: Test the Lesson 6 package

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


At run time, your package gets the value for the **Directory** property from the **VarFolderName** parameter.  
  
To verify that the package updates the **Directory** property, execute the package. Because you copied three sample data files to the new directory, the data flow runs three times.
  
## Check the package layout  
Before you test the package, verify that the control and data flows in the Lesson 6 package are similar to the objects shown in the following diagrams:   
  
**Control Flow**  
  
![Control Flow](../integration-services/media/task4lesson2control.gif "Control Flow")  
  
**Data Flow**  
  
![Data Flow](../integration-services/media/task5lesson5data.gif "Data Flow")  
  
## Test the Lesson 6 package  
  
1.  On the **Debug** menu, select **Start Debugging**.  
  
2.  After the package has completed running, on the **Debug** menu, select **Stop Debugging**.  
  
## Go to next task
[Step 4: Deploy the Lesson 6 package](../integration-services/lesson-6-4-deploying-the-lesson-6-package.md)  
  
  
  
