---
title: "Step 4: Testing the Lesson 2 Tutorial Package | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 0e8c0a25-8f79-41df-8ed2-f82a74b129cd
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Step 4: Testing the Lesson 2 Tutorial Package
  With the Foreach Loop container and the Flat File connection manager now configured, the Lesson 2 package can iterate through the collection of 14 flat files in the Sample Data folder. Each time a file name is found that matches the specified file name criteria, the Foreach Loop container populates the user-defined variable with the file name. This variable, in turn, updates the ConnectionString property of the Flat File connection manager, and a connection to the new flat file is made. The Foreach Loop container then runs the unmodified data flow task against the data in the new flat file before connecting to the next file in the folder.  
  
 Use the following procedure to test the new looping functionality that you have added to your package.  
  
> [!NOTE]  
>  If you ran the package from Lesson 1, you will need to delete the records from dbo.FactCurrency in AdventureWorksDW2012 before you run the package from this lesson or the package will fail with errors indicating a Violation of Primary Key constraint. You will receive the same errors if you run the package by selecting Debug/Start Debugging (or press F5) because both Lesson 1 and Lesson 2 will run. Lesson 2 will attempt to insert records already inserted in Lesson 1.  
  
## Checking the Package Layout  
 Before you test the package you should verify that the control and data flows in the Lesson 2 package contains the objects shown in the following diagrams. The data flow should be identical to the data flow in lesson 1.  
  
 **Control Flow**  
  
 ![Control flow in package](../../2014/tutorials/media/task4lesson2control.gif "Control flow in package")  
  
 **Data Flow**  
  
 ![Data flow in package](../../2014/tutorials/media/task9lesson1data.gif "Data flow in package")  
  
### To test the Lesson 2 tutorial package  
  
1.  In **Solution Explorer**, right-click **Lesson 2.dtsx** and click **Execute Package**.  
  
     The package will run. You can verify the status of each loop in the Output window, or by clicking on the **Progress** tab. For example, you can see that 1097 lines were added to the destination table from the file Currency_VEB.txt.  
  
2.  After the package has completed running, on the **Debug** menu, click **Stop Debugging**.  
  
## Next Lesson  
 [Lesson 5: Adding Package Configurations for the Package Deployment Model](../integration-services/lesson-5-add-ssis-package-configurations-for-the-package-deployment-model.md)  
  
## See Also  
 [Execution of Projects and Packages](packages/run-integration-services-ssis-packages.md)  
  
  
